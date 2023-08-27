using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUpItem : MonoBehaviour
{
    [SerializeField] List<GameObject> m_pigsList = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (m_pigsList.Find(match => match == collision))
            {
                //MKPlayerKobutaのIsDamageのsetterにアクセスしたい
                //collision.GetComponent<MKPlayerKobuta>().IsDamaged = false;
            }
            //何かに当たったらこのオブジェクトを削除
            Destroy(this.gameObject);
        }
    }
}
