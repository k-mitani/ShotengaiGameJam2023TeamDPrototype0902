using Amazon.Lambda.Core;
using Amazon.DynamoDBv2;
using Amazon;
using Amazon.DynamoDBv2.DataModel;
using System.Security.Cryptography;
using System.Text;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace SanbutaShooting;

public class Function
{
    private static readonly AmazonDynamoDBClient client = new(RegionEndpoint.APNortheast1);

    public async Task<Response> FunctionHandler(Request req, ILambdaContext context)
    {
        try
        {
            context.Logger.LogInformation($"req: {req}");
            using var dbc = new DynamoDBContext(client);

            switch (req.Operation)
            {
                case Request.OperationList:
                    var scores = await dbc.ScanAsync<SanbutaShootingScore>(new List<ScanCondition>()).GetRemainingAsync();
                    return new Response
                    {
                        Operation = req.Operation,
                        ListResult = scores
                    };
                case Request.OperationUpsert:
                    ThrowIfInvalidUpsertRequest(req, context);
                    await dbc.SaveAsync(req.UpsertParameter);
                    return new Response
                    {
                        Operation = req.Operation,
                    };
                default:
                    throw new ArgumentException($"Unknown operation: {req.Operation}");
            }
        }
        catch (Exception ex)
        {
            context.Logger.LogError($"エラー: {ex}");
            return new Response
            {
                Operation = req.Operation,
                Error = ex.ToString(),
            };
        }
    }

    private static void ThrowIfInvalidUpsertRequest(Request req, ILambdaContext context)
    {
        if (req.UpsertParameter == null) throw new ArgumentException("UpsertParameter is null");
        if (string.IsNullOrEmpty(req.UpsertParameter.Id)) throw new ArgumentException("UpsertParameter.Id is null or empty");
        if (string.IsNullOrEmpty(req.UpsertParameter.Name)) throw new ArgumentException("UpsertParameter.Name is null or empty");
        if (req.UpsertParameter.Name.Length > 200) throw new ArgumentException("UpsertParameter.Name is too long");
        if (req.UpsertParameter.Id.Length > 200) throw new ArgumentException("UpsertParameter.Id is too long");
        var plainToken = req.UpsertParameter.ToString();
        context.Logger.LogInformation($"plainToken: {plainToken}");
        var encryptedToken = Encrypt(plainToken);
        context.Logger.LogInformation($"calculatedToken: {encryptedToken}");
        if (!encryptedToken.Equals(req.Token)) throw new ArgumentException("Token is invalid");
    }

    public static string Encrypt(string plain)
    {
        using var aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("ENCRYPT_KEY") ?? throw new InvalidOperationException("ENCRYPT_KEY is null"));
        aes.IV = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("ENCRYPT_IV") ?? throw new InvalidOperationException("ENCRYPT_IV is null"));
        using var encryptor = aes.CreateEncryptor();
        var plainBytes = Encoding.UTF8.GetBytes(plain);
        var encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
        return Convert.ToBase64String(encryptedBytes);
    }
}

public class Request
{
    public const string OperationList = "list";
    public const string OperationUpsert = "upsert";
    public string? Operation { get; set; }
    public string? Token { get; set; }
    public SanbutaShootingScore? UpsertParameter { get; set; }

    public override string ToString() => $"Operation: {Operation}, UpsertParameter: {UpsertParameter}";
}

[DynamoDBTable("SanbutaShootingScore")]
public class SanbutaShootingScore
{
    [DynamoDBHashKey] public string? Id { get; set; }
    public string? Name  { get; set; }
    [DynamoDBRangeKey] public int Score  { get; set; }

    public override string ToString() => $"Id: {Id}, Name: {Name}, Score: {Score}";
}

public class Response
{
    public string? Operation { get; set; }
    public string? Error { get; set; }
    public List<SanbutaShootingScore>? ListResult { get; set; }
}
