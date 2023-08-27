using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKWavesManager : MonoBehaviour
{
    [SerializeField] private bool m_autoStart = true;
    [SerializeField] private MKWaveBase m_startWave = null;
    private MKWaveBase[] m_waves;

    // Start is called before the first frame update
    void Start()
    {
        m_waves = GetComponentsInChildren<MKWaveBase>(true);
        foreach (var wave in m_waves)
        {
            wave.WaveClear += OnWaveClear;
        }
        
        if (m_autoStart)
        {
            // �ŏ���Wave���J�n����B
            foreach (var wave in m_waves) wave.gameObject.SetActive(false);
            (m_startWave ?? m_waves[0]).gameObject.SetActive(true);
        }
    }

    private void OnWaveClear(object sender, EventArgs e)
    {
        var wave = sender as MKWaveBase;
        if (wave == null) return;
        wave.gameObject.SetActive(false);

        var index = Array.IndexOf(m_waves, wave);
        if (index < 0) return;
        var nextIndex = index + 1;
        
        // �܂�����Wave������Ȃ�A����Wave���J�n����B
        if (nextIndex < m_waves.Length)
        {
            m_waves[nextIndex].gameObject.SetActive(true);
        }
        // �Ō��Wave���I�������A�Q�[���N���A�ɂ���B
        else
        {
            Debug.Log("�Q�[���N���A");
        }

        if (wave.name == "WaveBoss")
        {
            naichilab.RankingLoader.Instance.SendScoreAndShowRanking(MKUIManager.Instance.m_score);
        }
    }
}
