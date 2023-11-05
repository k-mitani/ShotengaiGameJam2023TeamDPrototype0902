using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MKWavesManager : MonoBehaviour
{
    public static MKWavesManager Instance { get; private set; }

    [SerializeField] private bool m_autoStart = true;
    [SerializeField] private MKWaveBase m_startWave = null;
    private MKWaveBase[] m_waves;

    private MKWaveBase _CurrentWave;
    public MKWaveBase CurrentWave
    {
        get => _CurrentWave != null ? _CurrentWave : _CurrentWave = m_waves.FirstOrDefault(x => x.gameObject.activeSelf);
        private set { _CurrentWave = value; }
    }

    private void Awake()
    {
        Instance = this;
    }

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
            CurrentWave = m_startWave ?? m_waves[0];
        }
        else
        {
            CurrentWave = m_waves.FirstOrDefault(x => x.gameObject.activeSelf);
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

        // Wave4のクリア時の場合、ボーナスウェーブ(Wave5)に進むか判定する。
        if (wave.name.Equals("Wave4"))
        {
            var nomiss = MKUIManager.Instance.IsNoMiss;
            var goodScore = MKUIManager.Instance.GoodScore;
            if (nomiss && goodScore)
            {
                Debug.Log($"ボーナスウェーブに進みます。");
                MKUIManager.Instance.BonusWaveGained = true;
            }
            else
            {
                nextIndex = index + 2;
                Debug.Log($"条件を満たしていないため、ボーナスウェーブには進みません。nomiss:{nomiss} goodScore:{goodScore}");
            }
        }

        // まだ次のWaveがあるなら、そのWaveを開始する。
        if (nextIndex < m_waves.Length)
        {
            m_waves[nextIndex].gameObject.SetActive(true);
            CurrentWave = m_waves[nextIndex];
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
