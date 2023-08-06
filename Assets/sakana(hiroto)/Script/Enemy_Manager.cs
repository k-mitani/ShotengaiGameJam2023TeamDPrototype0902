using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
    private int MoveSpeed = 5;

    //ゲームオブジェクトを取得
    public GameObject bulletPrefab;
    public GameObject fireingPosition;

    //Enemyが生成されたらEnemyBulletも生成する
    private void Start()
    {
        Shot();
    }
    void Update()
    {
        Move();
        OffScreen();
    }
    //Enemyを横に移動
    private void Move()
    {
        transform.position +=
            new Vector3(-MoveSpeed, 0, 0) * Time.deltaTime;
    }

    //Enemyが画面外に出たら消滅
    private void OffScreen()
    {
        if (this.transform.position.z < -10f)
        {
            Destroy(this.gameObject);
        }
    }

    //Bulletを生成
    private void Shot()
    {
        Instantiate(
            bulletPrefab,//生成したいオブジェクト
            fireingPosition.transform.position,//位置
            transform.rotation);//回転
    }
}
