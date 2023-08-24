using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //�v���C���[��List
    [SerializeField] private List<GameObject> m_pigs = new List<GameObject>();
    //�e�I�u�W�F�N�g
    [SerializeField] private GameObject m_bullet = null;
    //�ړ����x
    [SerializeField] private float m_speed = 5f;
    //�v���C���[���m�̊Ԋu
    [SerializeField] private float m_distanceMin = 2f;

    //�v���C���[List�̐擪
    private GameObject m_head = null;
    //�v���C���[�ƁA�q�@�̃����o�ϐ�
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
        //�����o�ϐ��ɑ��
        m_player = m_pigs[0];
        m_element1 = m_pigs[1];
        m_element2 = m_pigs[2];
        m_playerRb = m_player.GetComponent<Rigidbody2D>();
        //�v���C���[�Ǝq�@�̃^�O��ύX
        m_player.tag = "Player";
        m_element1.tag = m_element2.tag = "Untagged";
        //�v���C���[��layer��Player�ɂ���ȊO�����̌��ɕύX
        m_player.layer = 7;
        m_pigs[1].layer = 8;
        m_pigs[2].layer = 9;
        //Player���őO�ʂɁA�q�@�����̌��ɉf��悤�ɂ���
        m_player.GetComponentInChildren<SpriteRenderer>().sortingOrder = 3;
        m_element1.GetComponentInChildren<SpriteRenderer>().sortingOrder = 2;
        m_element2.GetComponentInChildren<SpriteRenderer>().sortingOrder = 1;
    }

    // Update is called once per frame
    void Update()
    {
        m_playerRb.velocity = Vector2.zero;
        //�v���C���[(���삷��L�����N�^�[)��ύX
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

    //�v���C���[�̈ړ�����
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
    //����L�����N�^�[��ύX
    private void PlayerChange()
    {
<<<<<<< HEAD
        int i = 0;
        //do while���Ńv���C���[�ύX�����Ȃ��Ƃ��P��͒ʂ�悤�ɂ���
        do
        {
            //�擪�̗v�f��GameObject�ƍ��W����
            GameObject head = m_pigs[0];
            Vector2 playerPos = head.transform.position;
            //�擪��GameObject���Ō���ɑ��
            m_pigs.Remove(head);
            m_pigs.Add(head);
            //�����o�ϐ��ɑ��
            m_player = m_pigs[0];
            m_element1 = m_pigs[1];
            m_element2 = m_pigs[2];
            m_playerRb = m_player.GetComponent<Rigidbody2D>();
            //�O�̃v���C���[�̍��W���A�ύX��̃v���C���[�ɓK�p
            m_player.transform.position = playerPos;
            //�v���C���[�Ǝq�@�̃^�O��ύX
            m_player.tag = "Player";
            m_element1.tag = m_element2.tag = "Untagged";
            //�v���C���[��layer��Player�ɂ���ȊO�����̌��ɕύX
            m_player.layer = 7;
            m_pigs[1].layer = 8;
            m_pigs[2].layer = 9;
            //Player���őO�ʂɁA�q�@�����̌��ɉf��悤�ɂ���
            m_player.GetComponentInChildren<SpriteRenderer>().sortingOrder = 3;
            m_element1.GetComponentInChildren<SpriteRenderer>().sortingOrder = 2;
            m_element2.GetComponentInChildren<SpriteRenderer>().sortingOrder = 1;
            //lifeUI�𓮂����֐�
            m_lifeUIManager.LifeUISetUp();
            //�������[�v���p
            if (i >= 3)
            {
                break;
            }
            i++;
            //player��(��A�N�e�B�u�ł���΃��[�v����)
        } while (!m_player.activeSelf);


=======
        m_head = m_pigs[0];
        m_pigs.Remove(m_head);
        m_pigs.Add(m_head);
        //�����o�ϐ��ɑ��
        m_player = m_pigs[0];
        m_element1 = m_pigs[1];
        m_element2 = m_pigs[2];
        m_playerRb = m_player.GetComponent<Rigidbody2D>();
        //�v���C���[�Ǝq�@�̃^�O��ύX
        m_player.tag = "Player";
        m_element1.tag = m_element2.tag = "Untagged";
        //�v���C���[��layer��Player�ɂ���ȊO�����̌��ɕύX
        m_player.layer = 7;
        m_pigs[1].layer = 8;
        m_pigs[2].layer = 9;
        //Player���őO�ʂɁA�q�@�����̌��ɉf��悤�ɂ���
        m_player.GetComponentInChildren<SpriteRenderer>().sortingOrder = 3;
        m_element1.GetComponentInChildren<SpriteRenderer>().sortingOrder = 2;
        m_element2.GetComponentInChildren<SpriteRenderer>().sortingOrder = 1;
>>>>>>> parent of d92edd1 (lifeUIのアニメーションを実装)
    }

    //�Ǐ]����I�u�W�F�N�g�A�Ǐ]����^�[�Q�b�g
    private void FollowUp(GameObject element, GameObject target)
    {
        Transform parentTf = target.transform;
        Transform subTf = element.transform;
        float dist = Vector3.Distance(subTf.position, parentTf.position);
        if (dist > m_distanceMin)
        {
            // �^�[�Q�b�g�̕��������߂�
            Vector3 dir = (parentTf.position - subTf.position).normalized;
            //�^�[�Q�b�g�̕����Ɉړ�����
            element.transform.position += dir * m_speed * Time.deltaTime;
            //�Ǐ]����I�u�W�F�N�g�֌�����
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
