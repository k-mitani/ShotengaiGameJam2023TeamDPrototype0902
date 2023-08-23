using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
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

    private int m_score = 0;

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
}
