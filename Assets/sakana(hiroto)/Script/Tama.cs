using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Tama : MonoBehaviour
{
    private int bulletSpeed = 8;

    //ゲームオブジェクトを取得
    public GameObject bulletPrefab;
    public GameObject fireingPosition;

    //Enemyが生成されたらEn
    private void Start()
    {

    }
    void Update()
    {
        Move();
        OffScreen();
    }
    //Bulletを上に飛ばす
    private void Move()
    {
        transform.position +=
            new Vector3(-bulletSpeed, 0, 0) * Time.deltaTime;
    }

    //Bulletが画面外に出たら消滅
    private void OffScreen()
    {
        if(this.transform.position.z<-10f)
        {
            Destroy(this.gameObject);
        }
    }

    //Bulletを生成
   /* private void Start()
    {
        Instantiate(
            bulletPrefab,//生成したいオブジェクト
            fireingPosition.transform.position,//位置
            transeform.rotation);//回転
    }*/
}
