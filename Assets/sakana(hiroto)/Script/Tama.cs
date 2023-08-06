using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Tama : MonoBehaviour
{
    private int bulletSpeed = 8;

    //�Q�[���I�u�W�F�N�g���擾
    public GameObject bulletPrefab;
    public GameObject fireingPosition;

    //Enemy���������ꂽ��En
    private void Start()
    {

    }
    void Update()
    {
        Move();
        OffScreen();
    }
    //Bullet����ɔ�΂�
    private void Move()
    {
        transform.position +=
            new Vector3(-bulletSpeed, 0, 0) * Time.deltaTime;
    }

    //Bullet����ʊO�ɏo�������
    private void OffScreen()
    {
        if(this.transform.position.z<-10f)
        {
            Destroy(this.gameObject);
        }
    }

    //Bullet�𐶐�
   /* private void Start()
    {
        Instantiate(
            bulletPrefab,//�����������I�u�W�F�N�g
            fireingPosition.transform.position,//�ʒu
            transeform.rotation);//��]
    }*/
}
