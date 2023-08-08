using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//弾のタイプ
public enum BulletType
{
    None, Katsu, Strawberry, Mango
}
public class Bullet : MonoBehaviour
{
    //弾オブジェクトの子のスプライトレンダラーを入れる
    [SerializeField] private SpriteRenderer m_sr;
    //弾のタイプ
    private BulletType m_bulletType = BulletType.None;
    //リジッドボディ2D
    private Rigidbody2D rb = null;
    //弾の消滅時間
    private float m_deadtime = 0f;
    //弾のタイプのゲッター
    public BulletType GetBulletType
    {
        get { return m_bulletType; }
    }
    private void Start()
    {
        //リジッドボディを取得
        rb = GetComponent<Rigidbody2D>();
        //消滅処理、消滅時間を設定
        Destroy(this.gameObject, m_deadtime);
    }
    //弾が当たった時の処理を仮で書いてます
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == "Enemy")
            {

            }
        }
    }
    //弾のセットアップ関数
    public void SetUp(BulletType type, Sprite sprite, Vector3 dir, float speed, float deadtime)
    {
        ///リジッドボディを取得
        rb = GetComponent<Rigidbody2D>();
        //弾に力を与える
        rb.AddForce(dir * speed, ForceMode2D.Impulse);
        //Launcherから弾のタイプ、画像、消滅時間を取得
        m_bulletType = type;
        m_sr.sprite = sprite;
        m_deadtime = deadtime;
    }
}
