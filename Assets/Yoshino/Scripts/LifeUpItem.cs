using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUpItem : MonoBehaviour
{
    [SerializeField] List<GameObject> m_pigsList = new List<GameObject>();
    [SerializeField] GameObject m_effect = null;

    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            if (m_pigsList[i] == null)
            {
                switch (i)
                {
                    case 0:
                        m_pigsList[0] = GameObject.Find("KobutaRed");
                        break;
                    case 1:
                        m_pigsList[1] = GameObject.Find("KobutaGreen");
                        break;
                    case 2:
                        m_pigsList[2] = GameObject.Find("KobutaBlue");
                        break;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            //リストの中を検索
            for (int i = 0; i < 3; i++)
            {
                if (collision.gameObject == m_pigsList[i])
                {
                    //ダメージ中のコブタと当たった場合、ダメージフラグをfalseにし、色をデフォルトにする
                    if (collision.GetComponent<MKPlayerKobuta>().IsDamaged == true)
                    {
                        collision.GetComponent<MKPlayerKobuta>().IsDamaged = false;
                        collision.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                        MKPlayerKobuta kobuta = collision.gameObject.GetComponent<MKPlayerKobuta>();
                        LifeUIManager.Instance.SetKobutaDamagedImage(kobuta.Type, kobuta.IsDamaged);
                        Instantiate(m_effect, collision.transform.position, Quaternion.identity);
                        Destroy(this.gameObject);
                    }
                }
            }
        }
    }
}
