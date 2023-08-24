using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //プレイヤーのList
    [SerializeField] private List<GameObject> m_pigs = new List<GameObject>();
    //弾オブジェクト
    [SerializeField] private GameObject m_bullet = null;
    //移動速度
    [SerializeField] private float m_speed = 5f;
    //プレイヤー同士の間隔
    [SerializeField] private float m_distanceMin = 2f;

    //プレイヤーListの先頭
    private GameObject m_head = null;
    //プレイヤーと、子機のメンバ変数
    private GameObject m_player = null;
    private GameObject m_element1 = null;
    private GameObject m_element2 = null;

    private Rigidbody2D m_playerRb = null;

    private Launcher m_launcher = null;
    
    public Transform GetPlayerTf
    {
        get { return m_player.transform; }
    }
    public List<GameObject> GetPigs
    {
        get { return m_pigs; }
    }
    // Start is called before the first frame update
    void Start()
    {
        //メンバ変数に代入
        m_player = m_pigs[0];
        m_element1 = m_pigs[1];
        m_element2 = m_pigs[2];
        m_playerRb = m_player.GetComponent<Rigidbody2D>();
        //プレイヤーと子機のタグを変更
        m_player.tag = "Player";
        m_element1.tag = m_element2.tag = "Untagged";
        //プレイヤーのlayerをPlayerにそれ以外をその後ろに変更
        m_player.layer = 7;
        m_pigs[1].layer = 8;
        m_pigs[2].layer = 9;
        //Playerを最前面に、子機をその後ろに映るようにする
        m_player.GetComponentInChildren<SpriteRenderer>().sortingOrder = 3;
        m_element1.GetComponentInChildren<SpriteRenderer>().sortingOrder = 2;
        m_element2.GetComponentInChildren<SpriteRenderer>().sortingOrder = 1;
    }

    // Update is called once per frame
    void Update()
    {
        m_playerRb.velocity = Vector2.zero;
        //プレイヤー(操作するキャラクター)を変更
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerChange();
        }
        PlayerMove();

        if (Input.GetKeyDown(KeyCode.B))
        {
            m_launcher = m_player.GetComponent<Launcher>();
            m_launcher.Shoot();
        }

        FollowUp(m_element1, m_player);
        FollowUp(m_element2, m_element1);
    }

    //プレイヤーの移動処理
    private void PlayerMove()
    {
        if (Input.GetKey(KeyCode.A))
        {
            m_player.transform.rotation = Quaternion.Euler(0, -180, 0);
            m_playerRb.velocity = new Vector2(-m_speed, m_playerRb.velocity.y);
        }
        if (Input.GetKey(KeyCode.D))
        {
            m_player.transform.rotation = Quaternion.Euler(0, 0, 0);
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
    }
    //操作キャラクターを変更
    private void PlayerChange()
    {
<<<<<<< HEAD
        int i = 0;
        //do while文でプレイヤー変更を少なくとも１回は通るようにする
        do
        {
            //先頭の要素のGameObjectと座標を代入
            GameObject head = m_pigs[0];
            Vector2 playerPos = head.transform.position;
            //先頭のGameObjectを最後尾に代入
            m_pigs.Remove(head);
            m_pigs.Add(head);
            //メンバ変数に代入
            m_player = m_pigs[0];
            m_element1 = m_pigs[1];
            m_element2 = m_pigs[2];
            m_playerRb = m_player.GetComponent<Rigidbody2D>();
            //前のプレイヤーの座標を、変更後のプレイヤーに適用
            m_player.transform.position = playerPos;
            //プレイヤーと子機のタグを変更
            m_player.tag = "Player";
            m_element1.tag = m_element2.tag = "Untagged";
            //プレイヤーのlayerをPlayerにそれ以外をその後ろに変更
            m_player.layer = 7;
            m_pigs[1].layer = 8;
            m_pigs[2].layer = 9;
            //Playerを最前面に、子機をその後ろに映るようにする
            m_player.GetComponentInChildren<SpriteRenderer>().sortingOrder = 3;
            m_element1.GetComponentInChildren<SpriteRenderer>().sortingOrder = 2;
            m_element2.GetComponentInChildren<SpriteRenderer>().sortingOrder = 1;
            //lifeUIを動かす関数
            m_lifeUIManager.LifeUISetUp();
            //無限ループ回避用
            if (i >= 3)
            {
                break;
            }
            i++;
            //playerが(非アクティブであればループする)
        } while (!m_player.activeSelf);


=======
        m_head = m_pigs[0];
        m_pigs.Remove(m_head);
        m_pigs.Add(m_head);
        //メンバ変数に代入
        m_player = m_pigs[0];
        m_element1 = m_pigs[1];
        m_element2 = m_pigs[2];
        m_playerRb = m_player.GetComponent<Rigidbody2D>();
        //プレイヤーと子機のタグを変更
        m_player.tag = "Player";
        m_element1.tag = m_element2.tag = "Untagged";
        //プレイヤーのlayerをPlayerにそれ以外をその後ろに変更
        m_player.layer = 7;
        m_pigs[1].layer = 8;
        m_pigs[2].layer = 9;
        //Playerを最前面に、子機をその後ろに映るようにする
        m_player.GetComponentInChildren<SpriteRenderer>().sortingOrder = 3;
        m_element1.GetComponentInChildren<SpriteRenderer>().sortingOrder = 2;
        m_element2.GetComponentInChildren<SpriteRenderer>().sortingOrder = 1;
>>>>>>> parent of d92edd1 (lifeUI縺ｮ繧｢繝九Γ繝ｼ繧ｷ繝ｧ繝ｳ繧貞ｮ溯｣�)
    }

    //追従するオブジェクト、追従するターゲット
    private void FollowUp(GameObject element, GameObject target)
    {
        Transform parentTf = target.transform;
        Transform subTf = element.transform;
        float dist = Vector3.Distance(subTf.position, parentTf.position);
        if (dist > m_distanceMin)
        {
            // ターゲットの方向を求める
            Vector3 dir = (parentTf.position - subTf.position).normalized;
            //ターゲットの方向に移動する
            element.transform.position += dir * m_speed * Time.deltaTime;
            //追従するオブジェクトへ向ける
            if (dir.x >= 0)
            {
                element.transform.rotation = Quaternion.Euler(0, 0, 0);

            }
            else
            {
                element.transform.rotation = Quaternion.Euler(0, -180, 0);
            }
        }
    }
}
