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
    /// 隊列変更中ならtrue
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

        // 現在位置からユニット本来の位置まで移動する。
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
            
            // 他の移動が始まったら、この移動を中断する。
            if (rearrangingId != m_rearrangingId) yield break;
        }
        // 移動が完了したら、位置をリセットしてタイプを切り替える。
        kobutaTransform.position = transform.position;
        kobutaTransform.localScale = m_initialScale;

        IsRearranging = false;
    }
}

