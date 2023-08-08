using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    //設定したい画像を入れる
    [SerializeField] private Sprite m_sprite = null;
    //弾のタイプ
    [SerializeField] private BulletType m_bulletType = BulletType.None;
    //弾の速度
    [SerializeField] private float m_speed = 0f;
    //弾の消滅時間
    [SerializeField] private float m_deadtime = 0f;
    //弾のプレハブ
    [SerializeField] private GameObject m_bullet = null;
    public void Shoot()
    {
        //インスタンス生成
        GameObject new_bullet = Instantiate(m_bullet, transform.position, Quaternion.identity);
        //コンポーネントを取得
        Bullet bullet = new_bullet.GetComponent<Bullet>();
        
        if (bullet != null)
        {
            //弾のタイプ、画像、方向、速度、消滅時間を与える
            bullet.SetUp(m_bulletType, m_sprite, transform.right, m_speed, m_deadtime);
        }
    }
}
