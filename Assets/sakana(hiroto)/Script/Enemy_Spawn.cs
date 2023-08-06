using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawn : MonoBehaviour
{
    //ゲームオブジェクトを取得
    public GameObject EnemyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        //Spawnを一秒間間隔で実行する
        InvokeRepeating("Spawn", 3f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //Enemyを生成

    private void Spawn()
    {
        //ランダムなY軸を取得
        Vector2 randomPos= new Vector2(
           Random.Range(-1.0f,1.0f),
           transform.position.x);
        Instantiate(
            EnemyPrefab,//生成したいオブジェクト
            randomPos,//位置
            transform.rotation);//回転
    }
}
