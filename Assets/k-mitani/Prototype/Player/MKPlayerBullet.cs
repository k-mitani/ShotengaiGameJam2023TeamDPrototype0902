using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKPlayerBullet : MonoBehaviour
{
    [SerializeField] private Sprite[] m_bulletImages;
    [SerializeField] private SpriteRenderer m_bulletRenderer;
    [SerializeField] private float m_speed = 1;
    public MKKobutaType KobutaType { get; private set; }

    private ParticleSystem m_particleSystem;

    public void Initialize(MKKobutaType type)
    {
        KobutaType = type;
        TryGetComponent(out m_particleSystem);
        var color = MKUtil.GetColor(type);
        var main = m_particleSystem.main;
        main.startColor = color;
        m_bulletRenderer.sprite = m_bulletImages[Random.Range(0, m_bulletImages.Length)];
    }

    void Update()
    {
        transform.position += m_speed * Time.deltaTime * Vector3.right;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MKOutOfScreenWall"))
        {
            Destroy(gameObject);
        }
        else if (collision.TryGetComponent<MKKobun>(out var kobun))
        {
            kobun.OnHit(this);
            Destroy(gameObject);
        }
    }
}
