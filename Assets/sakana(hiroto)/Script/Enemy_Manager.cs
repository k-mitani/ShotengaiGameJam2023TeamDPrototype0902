using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
    private int MoveSpeed = 5;

    //�Q�[���I�u�W�F�N�g���擾
    public GameObject bulletPrefab;
    public GameObject fireingPosition;

    //Enemy���������ꂽ��EnemyBullet����������
    private void Start()
    {
        Shot();
    }
    void Update()
    {
        Move();
        OffScreen();
    }
    //Enemy�����Ɉړ�
    private void Move()
    {
        transform.position +=
            new Vector3(-MoveSpeed, 0, 0) * Time.deltaTime;
    }

    //Enemy����ʊO�ɏo�������
    private void OffScreen()
    {
        if (this.transform.position.z < -10f)
        {
            Destroy(this.gameObject);
        }
    }

    //Bullet�𐶐�
    private void Shot()
    {
        Instantiate(
            bulletPrefab,//�����������I�u�W�F�N�g
            fireingPosition.transform.position,//�ʒu
            transform.rotation);//��]
    }
}
