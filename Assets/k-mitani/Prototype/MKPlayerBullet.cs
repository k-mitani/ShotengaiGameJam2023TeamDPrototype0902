using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKPlayerBullet : MonoBehaviour
{
    [SerializeField] private Sprite[] m_bulletImages;
    [SerializeField] private SpriteRenderer m_bulletRenderer;
    [SerializeField] private float m_speed = 1;

    private ParticleSystem m_particleSystem;

    public void Initialize(MKKobutaType type)
    {
        TryGetComponent(out m_particleSystem);
        var color = MKUtil.GetColor(type);
        m_particleSystem.startColor = color;
        m_bulletRenderer.sprite = m_bulletImages[Random.Range(0, m_bulletImages.Length)];
    }

    void Update()
    {
        transform.position += m_speed * Time.deltaTime * Vector3.right;
    }
}
