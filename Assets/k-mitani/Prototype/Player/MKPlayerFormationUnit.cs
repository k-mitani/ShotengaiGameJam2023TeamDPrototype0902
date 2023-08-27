using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MKPlayerFormationUnit : MonoBehaviour
{
    [field: SerializeField] public MKPlayerKobuta Kobuta { get; set; }

    private Vector3 m_initialScale;

    private int m_rearrangingId = 0;

    /// <summary>
    /// ����ύX���Ȃ�true
    /// </summary>
    public bool IsRearranging { get; set; } = false;

    protected virtual void Start()
    {
        m_initialScale = Kobuta.transform.localScale;
    }

    public void UpdateKobutaImage(MKPlayerKobuta kobuta)
    {
        Kobuta = kobuta;
        kobuta.transform.parent = transform;
    }


    public void StartRearrange(float transitionDuration)
    {
        StartCoroutine(Rearrange(transitionDuration));
    }

    private IEnumerator Rearrange(float transitionDuration)
    {
        IsRearranging = true;
        var rearrangingId = ++m_rearrangingId;

        // ���݈ʒu���烆�j�b�g�{���̈ʒu�܂ňړ�����B
        var kobutaTransform = Kobuta.transform;
        var durationMax = transitionDuration;
        var duration = 0f;
        while (duration < durationMax)
        {
            duration += Time.deltaTime;
            var progress = duration / durationMax;
            kobutaTransform.position = Vector3.Lerp(kobutaTransform.position, transform.position, progress);
            kobutaTransform.localScale = Vector3.Lerp(kobutaTransform.localScale, m_initialScale, progress * 3);
            yield return null;
            
            // ���̈ړ����n�܂�����A���̈ړ��𒆒f����B
            if (rearrangingId != m_rearrangingId) yield break;
        }
        // �ړ�������������A�ʒu�����Z�b�g���ă^�C�v��؂�ւ���B
        kobutaTransform.position = transform.position;
        kobutaTransform.localScale = m_initialScale;

        IsRearranging = false;
    }
}

