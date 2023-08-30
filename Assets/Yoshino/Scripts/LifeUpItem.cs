using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUpItem : MonoBehaviour
{
    [SerializeField] List<GameObject> m_pigsList = new List<GameObject>();
    [SerializeField] GameObject m_effect = null;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            //���X�g�̒�������
            for (int i = 0; i < 3; i++)
            {
                if (collision.gameObject == m_pigsList[i])
                {
                    //�_���[�W���̃R�u�^�Ɠ��������ꍇ�A�_���[�W�t���O��false�ɂ��A�F���f�t�H���g�ɂ���
                    if (collision.GetComponent<MKPlayerKobuta>().IsDamaged == true)
                    {
                        collision.GetComponent<MKPlayerKobuta>().IsDamaged = false;
                        collision.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                        OnPlayerHit(collision.GetComponent<MKPlayerKobuta>());
                        MKPlayerKobuta kobuta = collision.gameObject.GetComponent<MKPlayerKobuta>();
                        LifeUIManager.Instance.SetKobutaDamagedImage(kobuta.Type, kobuta.IsDamaged);
                    }
                }
            }
        }
    }

    public void OnPlayerHit(MKPlayerKobuta kobuta)
    {
        Instantiate(m_effect, kobuta.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
