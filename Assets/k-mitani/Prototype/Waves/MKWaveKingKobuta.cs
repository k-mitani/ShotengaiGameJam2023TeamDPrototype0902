using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKWaveKingKobuta : MKWaveBase
{
    [SerializeField] private MKKingKobuta m_kingKobuta;
    [SerializeField] private bool m_skipInitialMove = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        StartCoroutine(MoveToX0());
    }

    private IEnumerator MoveToX0()
    {
        m_kingKobuta.Pause();
        if (!m_skipInitialMove)
        {
            while (transform.position.x > 0)
            {
                yield return null;
                transform.position += 1f * Time.deltaTime * Vector3.left;
            }
        }
        else
        {
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
        }
        yield return new WaitForSeconds(1f);
        m_kingKobuta.Resume();
    }

    protected override bool IsWaveClear()
    {
        // 3つの頭全てがやられたらクリア。
        return m_kingKobuta.IsAllDead;
    }
}
