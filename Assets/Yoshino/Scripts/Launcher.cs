using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    //�ݒ肵�����摜������
    [SerializeField] private Sprite m_sprite = null;
    //�e�̃^�C�v
    [SerializeField] private BulletType m_bulletType = BulletType.None;
    //�e�̑��x
    [SerializeField] private float m_speed = 0f;
    //�e�̏��Ŏ���
    [SerializeField] private float m_deadtime = 0f;
    //�e�̃v���n�u
    [SerializeField] private GameObject m_bullet = null;
    public void Shoot()
    {
        //�C���X�^���X����
        GameObject new_bullet = Instantiate(m_bullet, transform.position, Quaternion.identity);
        //�R���|�[�l���g���擾
        Bullet bullet = new_bullet.GetComponent<Bullet>();
        
        if (bullet != null)
        {
            //�e�̃^�C�v�A�摜�A�����A���x�A���Ŏ��Ԃ�^����
            bullet.SetUp(m_bulletType, m_sprite, transform.right, m_speed, m_deadtime);
        }
    }
}
