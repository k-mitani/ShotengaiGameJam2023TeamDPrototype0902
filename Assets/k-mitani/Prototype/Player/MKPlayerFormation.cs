using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKPlayerFormation : MonoBehaviour
{
    [SerializeField] private MKPlayer m_player;
    [SerializeField] private MKOption m_option1;
    [SerializeField] private MKOption m_option2;
    [SerializeField] private float m_rearrangeDuration = 0.25f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) ||
            Input.GetKeyDown(KeyCode.RightShift) ||
            Input.GetKeyDown(KeyCode.Z))
        {
            Rearrange();
        }
    }

    /// <summary>
    /// �������בւ��܂��B
    /// </summary>
    private void Rearrange()
    {
        // �܂��e�摜�̐e��t���ւ���B
        var playerKobuta = m_player.Kobuta;
        m_player.UpdateKobutaImage(m_option1.Kobuta);
        m_option1.UpdateKobutaImage(m_option2.Kobuta);
        m_option2.UpdateKobutaImage(playerKobuta);

        m_player.StartRearrange(m_rearrangeDuration);
        m_option1.StartRearrange(m_rearrangeDuration);
        m_option2.StartRearrange(m_rearrangeDuration);

        // UI���X�V����B
        MKUIManager.Instance.RearrangeKobuta(m_player.Kobuta, m_option1.Kobuta, m_option2.Kobuta);
    }
}
