using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //コブタのList
    [SerializeField] private List<GameObject> m_pigs = new List<GameObject>();
    //移動速度
    [SerializeField] private float m_speed = 0f;
    //コブタListの先頭
    private GameObject m_head = null;

    private GameObject m_player = null;

    private Rigidbody2D m_playerRb = null;

    private Vector3 m_playerPos = Vector3.zero;


    private GameObject m_follow1 = null;
    private GameObject m_follow2 = null;
    // Start is called before the first frame update
    void Start()
    {
        m_player = m_pigs[0];
        m_playerRb = m_player.GetComponent<Rigidbody2D>();
        m_follow1 = m_pigs[1];
        m_follow2 = m_pigs[2];
    }

    // Update is called once per frame
    void Update()
    {
        m_playerPos = m_player.transform.position;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerChange();
        }

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
        //transform.position = Vector2.MoveTowards(
        //    transform.position,
        //    new Vector2(playerTr.position.x, playerTr.position.y),
        //    speed * Time.deltaTime);
       
    }

    private void PlayerChange()
    {
        m_head = m_pigs[0];
        m_pigs.Remove(m_head);
        m_pigs.Add(m_head);
        m_player = m_pigs[0];
        m_playerRb = m_player.GetComponent<Rigidbody2D>();
        m_follow1 = m_pigs[1];
        m_follow2 = m_pigs[2];
    }
}
