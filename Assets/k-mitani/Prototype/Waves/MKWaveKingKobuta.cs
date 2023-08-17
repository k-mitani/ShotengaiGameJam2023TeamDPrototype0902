using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKWaveKingKobuta : MKWaveBase
{
    [SerializeField] private MKKingKobuta m_kingKobuta;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveToX0());
    }

    private IEnumerator MoveToX0()
    {
        m_kingKobuta.ShouldPause = true;
        while (transform.position.x > 0)
        {
            yield return null;
            transform.position += 1f * Time.deltaTime * Vector3.left;
        }
        yield return new WaitForSeconds(1f);
        m_kingKobuta.ShouldPause = false;
    }

    protected override bool IsWaveClear()
    {
        // 3つの頭全てがやられたらクリア。
        return m_kingKobuta.IsAllDead;
    }
}
