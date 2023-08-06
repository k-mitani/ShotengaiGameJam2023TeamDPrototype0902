using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //�R�u�^��List
    [SerializeField] private List<GameObject> m_pigs = new List<GameObject>();
    //�ړ����x
    [SerializeField] private float m_speed = 0f;
    //�v���C���[���m�̊Ԋu
    [SerializeField] private float distanceMin = 0.1f;


    //�R�u�^List�̐擪
    private GameObject m_head = null;

    private GameObject m_player = null;
    private GameObject m_follow1 = null;
    private GameObject m_follow2 = null;

    private Rigidbody2D m_playerRb = null;

    private Vector3 m_playerPos = Vector3.zero;

    private Transform m_followTf1 = null;
    private Transform m_followTf2 = null;


    // Start is called before the first frame update
    void Start()
    {
        m_player = m_pigs[0];
        m_follow1 = m_pigs[1];
        m_follow2 = m_pigs[2];
        m_playerRb = m_player.GetComponent<Rigidbody2D>();
        m_followTf1 = m_follow1.transform;
        m_followTf2 = m_follow2.transform;
        m_player.layer = 7;
        m_pigs[1].layer = 8;
        m_pigs[2].layer = 9;
    }

    // Update is called once per frame
    void Update()
    {
        m_playerPos = m_player.transform.position;
        m_playerRb.velocity = Vector2.zero;
        //�v���C���[(���삷��L�����N�^�[)��ύX
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerChange();
        }
        //�v���C���[�̈ړ�����
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

        //�v���C���[�ɒǏ]�����鏈��
        float dist1 = Vector3.Distance(m_followTf1.position, m_playerPos);

        if (dist1 > distanceMin)
        {
            //�^�[�Q�b�g�̕��������߂�
            Vector3 dir = (m_playerPos - m_followTf1.position).normalized;
            //�^�[�Q�b�g�̕����Ɉړ�����
            m_followTf1.position += dir * m_speed * Time.deltaTime;
            //�Ǐ]����I�u�W�F�N�g�֌�����
            if (dir.x >= 0)
            {
                m_followTf1.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                m_followTf1.localScale = new Vector3(-1, 1, 1);
            }
        }
        float dist2 = Vector3.Distance(m_followTf2.position, m_followTf1.position);
        if (dist2 > distanceMin)
        {
            //�^�[�Q�b�g�̕��������߂�
            Vector3 dir = (m_followTf1.position - m_followTf2.position).normalized;
            //�^�[�Q�b�g�̕����Ɉړ�����
            m_followTf2.position += dir * m_speed * Time.deltaTime;
            //�Ǐ]����I�u�W�F�N�g�֌�����
            if (dir.x >= 0)
            {
                m_followTf2.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                m_followTf2.localScale = new Vector3(-1, 1, 1);
            }
        }
    }

    private void PlayerChange()
    {
        m_head = m_pigs[0];
        m_pigs.Remove(m_head);
        m_pigs.Add(m_head);
        //�Q�[���I�u�W�F�N�g����
        m_player = m_pigs[0];
        m_follow1 = m_pigs[1];
        m_follow2 = m_pigs[2];
        m_playerRb = m_player.GetComponent<Rigidbody2D>();
        m_followTf1 = m_pigs[1].transform;
        m_followTf2 = m_pigs[2].transform;
        //�v���C���[��layer��Player�ɂ���ȊO��Follow1,2�ɕύX
        m_player.layer = 7;
        m_pigs[1].layer = 8;
        m_pigs[2].layer = 9;
        m_player.GetComponentInChildren<SpriteRenderer>().sortingOrder = 3;
        m_pigs[1].GetComponentInChildren<SpriteRenderer>().sortingOrder = 3;
    }
}
