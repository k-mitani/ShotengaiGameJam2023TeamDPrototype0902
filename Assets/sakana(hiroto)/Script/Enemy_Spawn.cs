using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawn : MonoBehaviour
{
    //�Q�[���I�u�W�F�N�g���擾
    public GameObject EnemyPrefab;
    public GameObject fireingPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Enemy�𐶐�
    private void Spawn()
    {
        Instantiate(
            EnemyPrefab,//�����������I�u�W�F�N�g
            fireingPosition.transform.position,//�ʒu
            transform.rotation);//��]
    }
}
