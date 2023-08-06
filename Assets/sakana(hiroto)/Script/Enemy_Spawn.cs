using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawn : MonoBehaviour
{
    //ゲームオブジェクトを取得
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
    //Enemyを生成
    private void Spawn()
    {
        Instantiate(
            EnemyPrefab,//生成したいオブジェクト
            fireingPosition.transform.position,//位置
            transform.rotation);//回転
    }
}
