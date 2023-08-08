using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�e�̃^�C�v
public enum BulletType
{
    None, Katsu, Strawberry, Mango
}
public class Bullet : MonoBehaviour
{
    //�e�I�u�W�F�N�g�̎q�̃X�v���C�g�����_���[������
    [SerializeField] private SpriteRenderer m_sr;
    //�e�̃^�C�v
    private BulletType m_bulletType = BulletType.None;
    //���W�b�h�{�f�B2D
    private Rigidbody2D rb = null;
    //�e�̏��Ŏ���
    private float m_deadtime = 0f;
    //�e�̃^�C�v�̃Q�b�^�[
    public BulletType GetBulletType
    {
        get { return m_bulletType; }
    }
    private void Start()
    {
        //���W�b�h�{�f�B���擾
        rb = GetComponent<Rigidbody2D>();
        //���ŏ����A���Ŏ��Ԃ�ݒ�
        Destroy(this.gameObject, m_deadtime);
    }
    //�e�������������̏��������ŏ����Ă܂�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == "Enemy")
            {

            }
        }
    }
    //�e�̃Z�b�g�A�b�v�֐�
    public void SetUp(BulletType type, Sprite sprite, Vector3 dir, float speed, float deadtime)
    {
        ///���W�b�h�{�f�B���擾
        rb = GetComponent<Rigidbody2D>();
        //�e�ɗ͂�^����
        rb.AddForce(dir * speed, ForceMode2D.Impulse);
        //Launcher����e�̃^�C�v�A�摜�A���Ŏ��Ԃ��擾
        m_bulletType = type;
        m_sr.sprite = sprite;
        m_deadtime = deadtime;
    }
}
