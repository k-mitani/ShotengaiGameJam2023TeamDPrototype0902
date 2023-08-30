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
        var childCount = transform.childCount;
        var childWaves = new List<MKWaveBase>();
        for (int i = 0; i < childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent<MKWaveBase>(out var childWave))
            {
                childWaves.Add(childWave);
            }
        }
        m_waves = childWaves.ToArray();
        foreach (var wave in m_waves)
        {
            wave.WaveClear += OnWaveClear;
        }
        
        if (m_autoStart)
        {
            // 最初のWaveを開始する。
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
        
        // まだ次のWaveがあるなら、そのWaveを開始する。
        if (nextIndex < m_waves.Length)
        {
            m_waves[nextIndex].gameObject.SetActive(true);
        }
        // 最後のWaveが終わったら、ゲームクリアにする。
        else
        {
            Debug.Log("ゲームクリア");
            if (MKUIManager.Instance.IsDemo)
            {
                // タイトル画面を再読込みする。
                StartCoroutine(LoadingSceneManager.LoadCoroutine("TitleScene", MKUIManager.Instance.curtain));
            }
        }
    }
}
