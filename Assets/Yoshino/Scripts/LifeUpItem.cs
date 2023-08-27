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
                //MKPlayerKobuta��IsDamage��setter�ɃA�N�Z�X������
                //collision.GetComponent<MKPlayerKobuta>().IsDamaged = false;
            }
            //�����ɓ��������炱�̃I�u�W�F�N�g���폜
            Destroy(this.gameObject);
        }
    }
}
