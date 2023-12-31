using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MKUIManager : MonoBehaviour
{
    public static MKUIManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI m_scoreText;
    [SerializeField] private RawImage m_lifeRed;
    [SerializeField] private RawImage m_lifeGreen;
    [SerializeField] private RawImage m_lifeBLue;
    private RawImage[] m_lifes;
    private CinemachineImpulseSource m_impulseSource;

    [NonSerialized] public int m_score = 0;

    [SerializeField] private GameObject m_gameOverPanel;
    [SerializeField] private TextMeshProUGUI m_gameOverText;
    [SerializeField] private float gameOverBlinkDurationMax = 1f;
    [SerializeField] public SceneTransitionCurtain curtain;

    [SerializeField] private GameObject m_pausePanel;

    [SerializeField] private GameObject m_stageClearPanel;

    [field: SerializeField] public bool IsDemo { get; private set; } = false;

    [field: SerializeField] public int BonusWaveScoreThreshold { get; private set; } = 25000;

    [SerializeField] private GameObject m_stageClearPanel2;

    public bool GoodScore { get; private set; } = false;
    public bool IsNoMiss { get; private set; } = true;
    private HashSet<string> m_setWavesBeforeBonus = new() { "Wave1", "Wave2", "Wave3", "Wave4", };
    [field: SerializeField] public bool BonusWaveGained { get; set; } = false;

    public bool IsPaused { get; private set; } = false;
    public bool IsGameOver { get; private set; } = false;
    private List<IDisposable> disposables = new List<IDisposable>();

    private void UpdateScoreText()
    {
        m_scoreText.text = $"Score: {m_score:0000000}";
    }

    public void AddScore(int score)
    {
        if (IsDemo) return;

        m_score = Math.Max(m_score + score, 0);
        UpdateScoreText();

        var wave = MKWavesManager.Instance.CurrentWave;
        if (!GoodScore && IsNoMiss & wave != null &&
            m_setWavesBeforeBonus.Contains(wave.name) &&
            m_score > BonusWaveScoreThreshold)
        {
            Debug.Log("GOOOOOD");
            GoodScore = true;
            MKSoundManager.Instance.PlaySePlayerHealed();
            m_scoreText.color = Color.yellow;
        }
    }

    public void RearrangeKobuta(MKPlayerKobuta p1, MKPlayerKobuta p2, MKPlayerKobuta p3)
    {
        if (IsDemo) return;

        m_lifes[(int)p1.Type].rectTransform.anchoredPosition = new Vector3(+100, 0);
        m_lifes[(int)p2.Type].rectTransform.anchoredPosition = new Vector3(0, 0);
        m_lifes[(int)p3.Type].rectTransform.anchoredPosition = new Vector3(-100, 0);

        // 吉野さんのUIを更新する。
        var lifeUi = FindObjectOfType<LifeUIManager>();
        if (lifeUi != null)
        {
            lifeUi.LifeUISetUp();
        }
    }

    public void SetKobutaDamaged(MKKobutaType type, bool damaged)
    {
        if (IsDemo) return;

        if (damaged)
        {
            IsNoMiss = false;
            var wave = MKWavesManager.Instance.CurrentWave;
            if (wave != null && m_setWavesBeforeBonus.Contains(wave.name))
            {
                m_scoreText.color = Color.white;
            }
        }

        m_lifes[(int)type].color = damaged ? new Color(1, 1, 1, 0.4f) : new Color(1, 1, 1, 1);

        // 吉野さんのUIを更新する。
        var lifeUi = FindObjectOfType<LifeUIManager>();
        if (lifeUi != null)
        {
            lifeUi.SetKobutaDamagedImage(type, damaged);
        }

        //var healthyCount = m_lifes.Count(c => c.color.a == 1);
        //MKSoundManager.Instance.SetBGMPitch(healthyCount == 1 ? 1.25f : 1f);
    }

    public void ShakeCamera()
    {
        if (IsDemo) return;

        m_impulseSource.GenerateImpulse();
    }

    public void OnGameOver()
    {
        if (IsDemo) return;

        if (IsGameOver) return;
        StartCoroutine(DoGameOver());
    }

    private IEnumerator DoGameOver()
    {
        yield return new WaitForSeconds(0.2f);
        Time.timeScale = 0.04f;
        IsGameOver = true;
        MKSoundManager.Instance.StopBgm();

        yield return new WaitForSeconds(0.04f);
        Time.timeScale = 1f;

        var pressAnyKeyAction = new InputAction(
            type: InputActionType.PassThrough,
            binding: "*/<Button>",
            interactions: "Press");
        disposables.Add(pressAnyKeyAction);
        pressAnyKeyAction.Enable();
        pressAnyKeyAction.performed += _ =>
        {
            pressAnyKeyAction.Disable();
            StartCoroutine(LoadingSceneManager.LoadCoroutine("TitleScene", curtain));
        };

        m_gameOverPanel.SetActive(true);
        StartCoroutine(MKUtil.BlinkText(m_gameOverText, gameOverBlinkDurationMax));
        StartCoroutine(WaitAndMoveToTitle());
    }

    private IEnumerator WaitAndMoveToTitle()
    {
        yield return new WaitForSeconds(30);
        StartCoroutine(LoadingSceneManager.LoadCoroutine("TitleScene", curtain));
    }

    public void GoToTitle()
    {
        IsGameOver = true;
        Time.timeScale = 1;
        StartCoroutine(LoadingSceneManager.LoadCoroutine("TitleScene", curtain));
    }

    public void RestartGame()
    {
        IsGameOver = true;
        Time.timeScale = 1;
        curtain.Close(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });
    }

    public void TogglePause()
    {
        if (IsPaused) Resume();
        else Pause();
    }

    public void Pause()
    {
        IsPaused = true;
        Time.timeScale = 0;
        m_pausePanel.SetActive(true);
        if (MKPlayer.Instance != null) MKPlayer.Instance.SetUiMode(true);
    }

    public void Resume()
    {
        IsPaused = false;
        Time.timeScale = 1;
        m_pausePanel.SetActive(false);
        if (MKPlayer.Instance != null) MKPlayer.Instance.SetUiMode(false);
    }

    internal void OnGameClear()
    {
        if (IsGameOver) return;
        StartCoroutine(GameClear());
    }

    private IEnumerator GameClear()
    {
        IsGameOver = true;
        yield return new WaitForSeconds(1);
        MKSoundManager.Instance.PlaySeStageClear();
        
        if (BonusWaveGained) m_stageClearPanel2.SetActive(true);
        else m_stageClearPanel.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        StageClearSceneManager.Parameter = new StageClearSceneManager.SceneParameter()
        {
            Score = m_score,
            BonusWaveGained = BonusWaveGained,
        };
        StartCoroutine(LoadingSceneManager.LoadCoroutine("StageClearScene", curtain));
    }

    void Awake()
    {
        Instance = this;
        m_lifes = new RawImage[] { m_lifeRed, m_lifeGreen, m_lifeBLue };
        TryGetComponent(out m_impulseSource);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (IsDemo) return;

        UpdateScoreText();
    }

    private void OnDestroy()
    {
        foreach (var item in disposables)
        {
            item.Dispose();
        }
        Instance = null;
    }
}
