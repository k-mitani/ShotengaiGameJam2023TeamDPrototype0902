using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using naichilab;
using NCMB;
using NCMB.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class StageClearSceneManager : MonoBehaviour
{
    public static SceneParameter Parameter { get; set; }
    public class SceneParameter
    {
        public int Score { get; set; }
    }

    private static string s_prevInputName = "";

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI yourScore;
    [SerializeField] private TextMeshProUGUI yourRank;
    [SerializeField] private TextMeshProUGUI rankCount;
    [SerializeField] private MKRankingRow[] rankingRows;
    [SerializeField] private TextMeshProUGUI pressAnyButton;
    [SerializeField] private TextMeshProUGUI comment;
    [SerializeField] private float pressAnyButtonBlinkDurationMax = 1;
    [SerializeField] private SceneTransitionCurtain curtain;
    [Header("Gojuon")]
    [SerializeField] private TextMeshProUGUI gojuonInputName;
    [SerializeField] private GameObject gojuonParent;
    [Header("Dialogs")]
    [SerializeField] private MKDialog dialogSendScore;
    [SerializeField] private MKDialog dialogInputName;
    [SerializeField] private MKDialog dialogEndGame;
    [Header("Other")]
    [SerializeField] private RankingInfo rankingInfo;
    [Header("Comments")]
    [SerializeField] private string[] commentsNo1;
    [SerializeField] private string[] commentsNo2ToNo10;
    [SerializeField] private string[] commentsNo11ToEnd;

    private bool scoreSent = false;
    private const int MaxNameLength = 15;
    private MKRankingRow myRankingRow;

    public void OnGojuonClick(Button button)
    {
        var moji = button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        var text = gojuonInputName.text;

        if (moji.Equals("削除"))
        {
            text = text.Substring(0, Mathf.Max(0, text.Length - 1));
            Debug.Log(gojuonInputName.text);
        }
        else
        {
            if (text.Length > MaxNameLength - 1)
            {
                text = text.Substring(0, MaxNameLength - 1) + moji;
            }
            else
            {
                text += moji;
            }
        }
        gojuonInputName.text = text;
        Debug.Log(moji);
    }

    private List<System.IDisposable> disposables = new List<System.IDisposable>();

    private List<InputAction> inputActionsDefault;
    private InputAction inputAction01WaitKey;

    void Start()
    {
        // 10分経ったら自動でタイトルに移動する。
        StartCoroutine(AutoMoveToTitle());
        IEnumerator AutoMoveToTitle()
        {
            yield return new WaitForSeconds(60 * 10);
            StartCoroutine(LoadingSceneManager.LoadCoroutine("TitleScene", curtain));
        }

        var score = Parameter?.Score ?? 0;

        StartCoroutine(SetupRanking());


        inputActionsDefault = InputSystem.ListEnabledActions();
        InputSystem.DisableAllEnabledActions();

        inputAction01WaitKey = new InputAction(
            type: InputActionType.PassThrough,
            binding: "*/<Button>",
            interactions: "Press");
        disposables.Add(inputAction01WaitKey);
        inputAction01WaitKey.performed += _ =>
        {
            inputAction01WaitKey.Disable();
            inputActionsDefault.ForEach(a => a.Enable());

            if (scoreSent)
            {
                dialogEndGame.message.text = "スコア送信完了！\nおつかれさまでした！";
                ShowEndGameDialog();
                return;
            }


            gojuonInputName.text = s_prevInputName;
            dialogSendScore.yesButton.Select();
            dialogSendScore.Show(res =>
            {
                switch (res)
                {
                    case MKDialogResult.Yes:
                        s_prevInputName = gojuonInputName.text;
                        var bs = dialogInputName.transform.GetComponentsInChildren<Button>();
                        var a = bs.FirstOrDefault(b => b.GetComponentInChildren<TextMeshProUGUI>().text.Equals("あ"));
                        a.Select();
                        dialogInputName.Show(r => {
                            if (r == MKDialogResult.Yes)
                            {
                                dialogEndGame.message.text = "スコア送信中...";
                                dialogEndGame.yesButton.gameObject.SetActive(false);
                                dialogEndGame.noButton.gameObject.SetActive(false);
                                dialogEndGame.cancelButton.gameObject.SetActive(false);
                                ShowEndGameDialog();
                                // スコアを送信する。
                                var name = gojuonInputName.text;
                                if (string.IsNullOrEmpty(name)) name = "ななし";
                                SendScore(score, gojuonInputName.text, errorMessage =>
                                {
                                    // スコア送信完了後
                                    if (errorMessage == null)
                                    {
                                        scoreSent = true;
                                        dialogEndGame.message.text = "スコア送信完了！\nおつかれさまでした！";
                                        if (myRankingRow != null)
                                        {
                                            myRankingRow.userName.text = name;
                                        }
                                    }
                                    else
                                    {
                                        dialogEndGame.message.text = "！スコア送信エラー！\n" + errorMessage;
                                    }
                                    dialogEndGame.yesButton.gameObject.SetActive(true);
                                    dialogEndGame.noButton.gameObject.SetActive(true);
                                    dialogEndGame.cancelButton.gameObject.SetActive(true);
                                });
                            }
                            else
                            {
                                InputSystem.DisableAllEnabledActions();
                                inputAction01WaitKey.Enable();
                            }
                        });
                        break;
                    case MKDialogResult.No:
                        dialogEndGame.message.text = "おつかれさまでした！";
                        ShowEndGameDialog();
                        break;
                    case MKDialogResult.Cancel:
                    default:
                        InputSystem.DisableAllEnabledActions();
                        inputAction01WaitKey.Enable();
                        break;
                }
            });
        };

        var gojuonButtons = gojuonParent.transform.GetComponentsInChildren<Button>(true);
        foreach (var b in gojuonButtons)
        {
            b.onClick.AddListener(() => OnGojuonClick(b));
        }

        //naichilab.RankingLoader.Instance.SendScoreAndShowRanking(score);
        StartCoroutine(BlinkPressAnyButtonText());
        inputAction01WaitKey.Enable();
    }

    private IEnumerator SetupRanking()
    {
        var myScore = Parameter?.Score ?? 0;

        yourScore.text = myScore.ToString("#,0");
        yourRank.text = "???";
        rankCount.text = "/ ???";
        comment.text = "...";
        for (int i = 0; i < rankingRows.Length; i++)
        {
            var row = rankingRows[i];
            row.Hide();
        }

        var query = new YieldableNcmbQuery<NCMBObject>(rankingInfo.ClassName);
        query.Limit = 30;
        query.OrderByDescending(RankingSceneManager.COLUMN_SCORE);
        yield return query.FindAsync();

        Debug.Log("データ取得 : " + query.Count.ToString() + "件");
        if (query.Error != null)
        {
            comment.text = "（ランキング取得エラー！）\n" + query.Error.Message;
        }
        else
        {
            var rows = query.Result.Select((r, i) => new RankingItem
            {
                rank = i + 1,
                name = (string)r[RankingSceneManager.COLUMN_NAME],
                score = (int)(long)r[RankingSceneManager.COLUMN_SCORE],
            }).ToList();

            // 自分より高いスコア4つを取得する。
            var nearAbove4 = rows
                .Where(r => r.score > myScore)
                .OrderByDescending(r => r.rank)
                .Take(4)
                .ToList();
            // 自分より低いスコア4つを取得する。
            var nearBelow4 = rows
                .Where(r => r.score <= myScore)
                .OrderBy(r => r.rank)
                .Take(4)
                .ToList();
            
            var nearAbobes = new List<RankingItem>();
            var nearBelows = new List<RankingItem>();
            var remainingCount = 4;
            while (remainingCount > 0)
            {
                if (nearAbove4.Count == 0 && nearBelow4.Count == 0) break;
                if (nearAbove4.Count > 0)
                {
                    nearAbobes.Insert(0, nearAbove4[0]);
                    nearAbove4.RemoveAt(0);
                    remainingCount--;
                }
                if (nearBelow4.Count > 0 && remainingCount > 0)
                {
                    nearBelows.Add(nearBelow4[0]);
                    nearBelow4.RemoveAt(0);
                    remainingCount--;
                }
            }
            var myRank = rows.Count(r => r.score > myScore) + 1;
            var myScoreItem = new RankingItem()
            {
                rank = myRank,
                name = "あなた",
                score = myScore,
                isMine = true,
            };

            var rankingItems = nearAbobes
                .Concat(new[] { myScoreItem } )
                .Concat(nearBelows.Select((r, i) => new RankingItem()
                {
                    // スコアの重複は面倒なので気にしない。
                    rank = myRank + 1 + i,
                    name = r.name,
                    score = r.score,
                }))
                .ToList();

            for (int i = 0; i < rankingItems.Count; i++)
            {
                var row = rankingRows[i];
                var item = rankingItems[i];
                row.SetData(item.rank, item.name, item.score, item.isMine ?
                    Color.yellow : Color.white);
                row.Show();
                if (item.isMine)
                {
                    myRankingRow = row;
                }
            }

            StartCoroutine(PlaySounds());
            IEnumerator PlaySounds()
            {
                MKSoundManager.Instance.SetBGMVolume(0.6f);
                MKSoundManager.Instance.PlaySeShowRanking();
                yield return new WaitForSeconds(0.9f);
                MKSoundManager.Instance.PlaySeCheers();
                yield return new WaitForSeconds(4.0f);
                MKSoundManager.Instance.SetBGMVolume(1f);
            }

            yourRank.text = myRank.ToString();
            rankCount.text = "/ " + (query.Count + 1).ToString();
            if (myRank == 1)
            {
                comment.text = commentsNo1[Random.Range(0, commentsNo1.Length)];
                StartCoroutine(Takeshi());
                IEnumerator Takeshi()
                {
                    yield return new WaitForSeconds(60 * 5);
                    comment.text = "このゲームで遊んでくれてありがとう";
                }
            }
            else if (myRank <= 10)
            {
                comment.text = commentsNo2ToNo10[Random.Range(0, commentsNo2ToNo10.Length)];
            }
            else
            {
                comment.text = commentsNo11ToEnd[Random.Range(0, commentsNo11ToEnd.Length)];
            }
        }
    }
    private struct RankingItem
    {
        public int rank;
        public string name;
        public int score;
        public bool isMine;
    }

    private void SendScore(int score, string name, System.Action<string> onFinished)
    {
        var calledCallback = false;

        StartCoroutine(WaitTimeout());
        IEnumerator WaitTimeout()
        {
            yield return new WaitForSeconds(10);
            if (calledCallback) yield break;
            calledCallback = true;
            onFinished?.Invoke("サーバーにアクセスできません...。");
        }

        StartCoroutine(DoWork());
        IEnumerator DoWork()
        {
            var record = new NCMBObject(rankingInfo.ClassName);
            record[RankingSceneManager.COLUMN_NAME] = name;
            record[RankingSceneManager.COLUMN_SCORE] = score;

            NCMBException errorResult = null;
            yield return record.YieldableSaveAsync(error => errorResult = error);
            if (calledCallback) yield break;
            calledCallback = true;
            if (errorResult != null)
            {
                onFinished?.Invoke(errorResult?.Message);
            }
            else
            {
                onFinished?.Invoke(null);
            }
        }
    }

    private void ShowEndGameDialog()
    {
        dialogEndGame.yesButton.Select();
        dialogEndGame.Show(res =>
        {
            switch (res)
            {
                case MKDialogResult.Yes:
                    // リトライ
                    StartCoroutine(LoadingSceneManager.LoadCoroutine("MKPrototypeScene", curtain));
                    break;
                case MKDialogResult.No:
                    // タイトルへ
                    s_prevInputName = "";
                    StartCoroutine(LoadingSceneManager.LoadCoroutine("TitleScene", curtain));
                    break;
                case MKDialogResult.Cancel:
                default:
                    InputSystem.DisableAllEnabledActions();
                    inputAction01WaitKey.Enable();
                    break;
            }
        });
    }

    private IEnumerator BlinkPressAnyButtonText()
    {
        var a = 0f;
        var originalColor = pressAnyButton.color;
        pressAnyButton.color = originalColor * new Color(1, 1, 1, a);
        while (true)
        {
            var pressAnyButtonBlinkDuration = pressAnyButtonBlinkDurationMax;
            while (true)
            {
                yield return null;
                pressAnyButtonBlinkDuration -= Time.deltaTime;
                if (pressAnyButtonBlinkDuration <= 0) break;
                a = 1 - pressAnyButtonBlinkDuration / pressAnyButtonBlinkDurationMax;
                pressAnyButton.color = originalColor * new Color(1, 1, 1, a);
            }
            yield return new WaitForSeconds(0.3f);
            pressAnyButtonBlinkDuration = pressAnyButtonBlinkDurationMax;
            while (true)
            {
                yield return null;
                pressAnyButtonBlinkDuration -= Time.deltaTime;
                if (pressAnyButtonBlinkDuration <= 0) break;
                a = pressAnyButtonBlinkDuration / pressAnyButtonBlinkDurationMax;
                pressAnyButton.color = originalColor * new Color(1, 1, 1, a);
            }
        }
    }

    private void OnDestroy()
    {
        foreach (var d in disposables)
        {
            d.Dispose();
        }
    }
}
