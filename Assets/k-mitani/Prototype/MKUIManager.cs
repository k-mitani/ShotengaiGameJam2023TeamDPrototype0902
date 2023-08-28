using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private SceneTransitionCurtain curtain;

    [SerializeField] private GameObject m_pausePanel;

    public bool IsPaused { get; private set; } = false;
    public bool IsGameOver { get; private set; } = false;
    private List<IDisposable> disposables = new List<IDisposable>();

    private void UpdateScoreText()
    {
        m_scoreText.text = $"Score: {m_score:0000000}";
    }

    public void AddScore(int score)
    {
        m_score = Math.Max(m_score + score, 0);
        UpdateScoreText();
    }

    public void RearrangeKobuta(MKPlayerKobuta p1, MKPlayerKobuta p2, MKPlayerKobuta p3)
    {
        m_lifes[(int)p1.Type].rectTransform.anchoredPosition = new Vector3(+100, 0);
        m_lifes[(int)p2.Type].rectTransform.anchoredPosition = new Vector3(0, 0);
        m_lifes[(int)p3.Type].rectTransform.anchoredPosition = new Vector3(-100, 0);
    }

    public void SetKobutaDamaged(MKKobutaType type, bool damaged)
    {
        m_lifes[(int)type].color = damaged ? new Color(1, 1, 1, 0.4f) : new Color(1, 1, 1, 1);
    }

    public void ShakeCamera()
    {
        m_impulseSource.GenerateImpulse();
    }

    public void OnGameOver()
    {
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
        StartCoroutine(BlinkGameOverText());
        StartCoroutine(WaitAndMoveToTitle());
    }

    private IEnumerator BlinkGameOverText()
    {
        var a = 0f;
        var originalColor = m_gameOverText.color;
        m_gameOverText.color = originalColor * new Color(1, 1, 1, a);
        while (true)
        {
            var gameOverBlinkDuration = gameOverBlinkDurationMax;
            while (true)
            {
                yield return null;
                gameOverBlinkDuration -= Time.deltaTime;
                if (gameOverBlinkDuration <= 0) break;
                a = 1 - gameOverBlinkDuration / gameOverBlinkDurationMax;
                m_gameOverText.color = originalColor * new Color(1, 1, 1, a);
            }
            yield return new WaitForSeconds(0.3f);
            gameOverBlinkDuration = gameOverBlinkDurationMax;
            while (true)
            {
                yield return null;
                gameOverBlinkDuration -= Time.deltaTime;
                if (gameOverBlinkDuration <= 0) break;
                a = gameOverBlinkDuration / gameOverBlinkDurationMax;
                m_gameOverText.color = originalColor * new Color(1, 1, 1, a);
            }
        }
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


    void Awake()
    {
        Instance = this;
        m_lifes = new RawImage[] { m_lifeRed, m_lifeGreen, m_lifeBLue };
        TryGetComponent(out m_impulseSource);
    }

    // Start is called before the first frame update
    void Start()
    {
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
