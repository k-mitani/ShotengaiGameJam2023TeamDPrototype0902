using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawn : MonoBehaviour
{
    //�Q�[���I�u�W�F�N�g���擾
    public GameObject EnemyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        //Spawn����b�ԊԊu�Ŏ��s����
        InvokeRepeating("Spawn", 3f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //Enemy�𐶐�

    private void Spawn()
    {
        //�����_����Y�����擾
        Vector2 randomPos= new Vector2(
           Random.Range(-1.0f,1.0f),
           transform.position.x);
        Instantiate(
            EnemyPrefab,//�����������I�u�W�F�N�g
            randomPos,//�ʒu
            transform.rotation);//��]
    }
}
