using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //プレイヤーのList
    [SerializeField] private List<GameObject> m_pigs = new List<GameObject>();
    //移動速度
    [SerializeField] private float m_speed = 0f;
    //プレイヤー同士の間隔
    [SerializeField] private float distanceMin = 0.1f;

    //プレイヤーListの先頭
    private GameObject m_head = null;
    //プレイヤーと、フォロワーのメンバ変数
    private GameObject m_player = null;
    private GameObject m_follower1 = null;
    private GameObject m_follower2 = null;

    private Rigidbody2D m_playerRb = null;

    private Vector3 m_playerPos = Vector3.zero;

    private Transform m_followerTf1 = null;
    private Transform m_followerTf2 = null;


    // Start is called before the first frame update
    void Start()
    {
        m_player = m_pigs[0];
        m_follower1 = m_pigs[1];
        m_follower2 = m_pigs[2];
        m_playerRb = m_player.GetComponent<Rigidbody2D>();
        m_followerTf1 = m_follower1.transform;
        m_followerTf2 = m_follower2.transform;
        //プレイヤーをフォロワーのタグを変更
        m_player.tag = "Player";
        m_follower1.tag = m_follower2.tag = "Untagged";
        //プレイヤーのlayerをPlayerにそれ以外をその後ろに変更
        m_player.layer = 7;
        m_pigs[1].layer = 8;
        m_pigs[2].layer = 9;
        //Playerを最前面に、Followerをその後ろに映るようにする
        m_player.GetComponentInChildren<SpriteRenderer>().sortingOrder = 3;
        m_follower1.GetComponentInChildren<SpriteRenderer>().sortingOrder = 2;
        m_follower2.GetComponentInChildren<SpriteRenderer>().sortingOrder = 1;
    }

    // Update is called once per frame
    void Update()
    {
        m_playerPos = m_player.transform.position;
        m_playerRb.velocity = Vector2.zero;
        //プレイヤー(操作するキャラクター)を変更
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerChange();
        }
        //プレイヤーの移動処理
        if (Input.GetKey(KeyCode.A))
        {
            m_player.transform.localScale = new Vector3(-1, 1, 1);
            m_playerRb.velocity = new Vector2(-m_speed, m_playerRb.velocity.y);
        }
        if (Input.GetKey(KeyCode.D))
        {
            m_player.transform.localScale = new Vector3(1, 1, 1);
            m_playerRb.velocity = new Vector2(m_speed, m_playerRb.velocity.y);
        }
        if (Input.GetKey(KeyCode.W))
        {
            m_playerRb.velocity = new Vector2(m_playerRb.velocity.x, m_speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            m_playerRb.velocity = new Vector2(m_playerRb.velocity.x, -m_speed);
        }

        //プレイヤーに追従させる処理
        float dist1 = Vector3.Distance(m_followerTf1.position, m_playerPos);

        if (dist1 > distanceMin)
        {
            //ターゲットの方向を求める
            Vector3 dir = (m_playerPos - m_followerTf1.position).normalized;
            //ターゲットの方向に移動する
            m_followerTf1.position += dir * m_speed * Time.deltaTime;
            //追従するオブジェクトへ向ける
            if (dir.x >= 0)
            {
                m_followerTf1.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                m_followerTf1.localScale = new Vector3(-1, 1, 1);
            }
        }
        float dist2 = Vector3.Distance(m_followerTf2.position, m_followerTf1.position);
        if (dist2 > distanceMin)
        {
            //ターゲットの方向を求める
            Vector3 dir = (m_followerTf1.position - m_followerTf2.position).normalized;
            //ターゲットの方向に移動する
            m_followerTf2.position += dir * m_speed * Time.deltaTime;
            //追従するオブジェクトへ向ける
            if (dir.x >= 0)
            {
                m_followerTf2.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                m_followerTf2.localScale = new Vector3(-1, 1, 1);
            }
        }
    }

    private void PlayerChange()
    {
        m_head = m_pigs[0];
        m_pigs.Remove(m_head);
        m_pigs.Add(m_head);
        //メンバ変数に代入
        m_player = m_pigs[0];
        m_follower1 = m_pigs[1];
        m_follower2 = m_pigs[2];
        m_playerRb = m_player.GetComponent<Rigidbody2D>();
        m_followerTf1 = m_pigs[1].transform;
        m_followerTf2 = m_pigs[2].transform;
        //プレイヤーをフォロワーのタグを変更
        m_player.tag = "Player";
        m_follower1.tag = m_follower2.tag = "Untagged";
        //プレイヤーのlayerをPlayerにそれ以外をその後ろに変更
        m_player.layer = 7;
        m_pigs[1].layer = 8;
        m_pigs[2].layer = 9;
        //Playerを最前面に、Followerをその後ろに映るようにする
        m_player.GetComponentInChildren<SpriteRenderer>().sortingOrder = 3;
        m_follower1.GetComponentInChildren<SpriteRenderer>().sortingOrder = 2;
        m_follower2.GetComponentInChildren<SpriteRenderer>().sortingOrder = 1;
    }
}
