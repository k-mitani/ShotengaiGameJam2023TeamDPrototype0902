using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //�v���C���[��List
    [SerializeField] private List<GameObject> m_pigs = new List<GameObject>();
    //�e�I�u�W�F�N�g
    [SerializeField] private GameObject m_bullet = null;
    //�ړ����x
    [SerializeField] private float m_speed = 0f;
    //�v���C���[���m�̊Ԋu
    [SerializeField] private float m_distanceMin = 0.1f;

    //�v���C���[List�̐擪
    private GameObject m_head = null;
    //�v���C���[�ƁA�q�@�̃����o�ϐ�
    private GameObject m_player = null;
    private GameObject m_sub1 = null;
    private GameObject m_sub2 = null;

    private Rigidbody2D m_playerRb = null;

    private Vector3 m_playerPos = Vector3.zero;

    private Transform m_subTf1 = null;
    private Transform m_subTf2 = null;


    // Start is called before the first frame update
    void Start()
    {
        m_player = m_pigs[0];
        m_sub1 = m_pigs[1];
        m_sub2 = m_pigs[2];
        m_playerRb = m_player.GetComponent<Rigidbody2D>();
        m_subTf1 = m_sub1.transform;
        m_subTf2 = m_sub2.transform;
        //�v���C���[�Ǝq�@�̃^�O��ύX
        m_player.tag = "Player";
        m_sub1.tag = m_sub2.tag = "Untagged";
        //�v���C���[��layer��Player�ɂ���ȊO�����̌��ɕύX
        m_player.layer = 7;
        m_pigs[1].layer = 8;
        m_pigs[2].layer = 9;
        //Player���őO�ʂɁA�q�@�����̌��ɉf��悤�ɂ���
        m_player.GetComponentInChildren<SpriteRenderer>().sortingOrder = 3;
        m_sub1.GetComponentInChildren<SpriteRenderer>().sortingOrder = 2;
        m_sub2.GetComponentInChildren<SpriteRenderer>().sortingOrder = 1;
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
        PlayerMove();


        /*
        //�v���C���[�ɒǏ]�����鏈��
        float dist1 = Vector3.Distance(m_subTf1.position, m_playerPos);
        if (dist1 > m_distanceMin)
        {
            //�^�[�Q�b�g�̕��������߂�
            Vector3 dir = (m_playerPos - m_subTf1.position).normalized;
            //�^�[�Q�b�g�̕����Ɉړ�����
            m_subTf1.position += dir * m_speed * Time.deltaTime;
            //�Ǐ]����I�u�W�F�N�g�֌�����
            if (dir.x >= 0)
            {
                m_subTf1.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                m_subTf1.localScale = new Vector3(-1, 1, 1);
            }
        }
        float dist2 = Vector3.Distance(m_subTf2.position, m_subTf1.position);
        if (dist2 > m_distanceMin)
        {
            //�^�[�Q�b�g�̕��������߂�
            Vector3 dir = (m_subTf1.position - m_subTf2.position).normalized;
            //�^�[�Q�b�g�̕����Ɉړ�����
            m_subTf2.position += dir * m_speed * Time.deltaTime;
            //�Ǐ]����I�u�W�F�N�g�֌�����
            if (dir.x >= 0)
            {
                m_subTf2.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                m_subTf2.localScale = new Vector3(-1, 1, 1);
            }
        }*/

        FollowUp(m_sub1, m_player);
        FollowUp(m_sub2, m_sub1);
    }
        
    //�v���C���[�̈ړ�����
    private void PlayerMove()
    {
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
    }
    //����L�����N�^�[��ύX
    private void PlayerChange()
    {
        m_head = m_pigs[0];
        m_pigs.Remove(m_head);
        m_pigs.Add(m_head);
        //�����o�ϐ��ɑ��
        m_player = m_pigs[0];
        m_sub1 = m_pigs[1];
        m_sub2 = m_pigs[2];
        m_playerRb = m_player.GetComponent<Rigidbody2D>();
        m_subTf1 = m_pigs[1].transform;
        m_subTf2 = m_pigs[2].transform;
        //�v���C���[�Ǝq�@�̃^�O��ύX
        m_player.tag = "Player";
        m_sub1.tag = m_sub2.tag = "Untagged";
        //�v���C���[��layer��Player�ɂ���ȊO�����̌��ɕύX
        m_player.layer = 7;
        m_pigs[1].layer = 8;
        m_pigs[2].layer = 9;
        //Player���őO�ʂɁA�q�@�����̌��ɉf��悤�ɂ���
        m_player.GetComponentInChildren<SpriteRenderer>().sortingOrder = 3;
        m_sub1.GetComponentInChildren<SpriteRenderer>().sortingOrder = 2;
        m_sub2.GetComponentInChildren<SpriteRenderer>().sortingOrder = 1;
    }

    //�Ǐ]����I�u�W�F�N�g�A�Ǐ]�̃^�[�Q�b�g
    private void FollowUp(GameObject sub, GameObject parent)
    {
        Transform parentTf = parent.transform;
        Transform subTf = sub.transform;
        float dist = Vector3.Distance(subTf.position, parentTf.position);
        if (dist > m_distanceMin)
        {
            // �^�[�Q�b�g�̕��������߂�
            Vector3 dir = (parentTf.position - subTf.position).normalized;
            //�^�[�Q�b�g�̕����Ɉړ�����
            sub.transform.position += dir * m_speed * Time.deltaTime;
            //�Ǐ]����I�u�W�F�N�g�֌�����
            if (dir.x >= 0)
            {
                sub.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                sub.transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
//    //�v���C���[�ɒǏ]�����鏈��
//    float dist1 = Vector3.Distance(m_subTf1.position, m_playerPos);
//        if (dist1 > m_distanceMin)
//        {
//            //�^�[�Q�b�g�̕��������߂�
//            Vector3 dir = (m_playerPos - m_subTf1.position).normalized;
//    //�^�[�Q�b�g�̕����Ɉړ�����
//    m_subTf1.position += dir* m_speed * Time.deltaTime;
//            //�Ǐ]����I�u�W�F�N�g�֌�����
//            if (dir.x >= 0)
//            {
//                m_subTf1.localScale = new Vector3(1, 1, 1);
//}
//            else
//{
//    m_subTf1.localScale = new Vector3(-1, 1, 1);
//}
//        }
}
