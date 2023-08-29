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
        StartCoroutine(Alert());
        m_kingKobuta.AllFaceDead += kingKobuta_AllFaceDead;
    }

    private IEnumerator Alert()
    {
        MKSoundManager.Instance.SetBGMVolume(0.25f);
        MKSoundManager.Instance.PlaySeBossAlert();
        yield return new WaitForSeconds(4);
        MKSoundManager.Instance.PlaySeBossAlert();
        yield return new WaitForSeconds(4);
        MKSoundManager.Instance.SetBGMVolume(1);
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

    private void kingKobuta_AllFaceDead(object sender, EventArgs e)
    {
        MKUIManager.Instance.OnGameClear();
    }

    protected override bool IsWaveClear()
    {
        //// 3つの頭全てがやられたらクリア。
        //return m_kingKobuta.IsAllDead;

        // このウェーブの中でステージクリア画面に遷移するのでクリア状態にはしない。
        return false;
    }
}
