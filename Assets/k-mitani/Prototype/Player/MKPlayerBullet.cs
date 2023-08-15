using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKPlayerBullet : MonoBehaviour
{
    [SerializeField] private Sprite[] m_bulletImages;
    [SerializeField] private SpriteRenderer m_bulletRenderer;
    [SerializeField] private float m_speed = 1;

    private MKPlayerKobuta m_shooter;
    public MKKobutaType KobutaType { get; private set; }
    public bool IsWeak { get; private set; } = false;

    private ParticleSystem m_particleSystem;

    public void Initialize(MKPlayerKobuta shooter)
    {
        m_shooter = shooter;
        KobutaType = shooter.Type;
        IsWeak = shooter.IsDamaged;

        TryGetComponent(out m_particleSystem);
        
        var color = MKUtil.GetColor(KobutaType);
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
            m_shooter.OnBulletDestroy(this);
        }
        else if (collision.TryGetComponent<MKKobun>(out var kobun))
        {
            kobun.OnHit(this);
            Destroy(gameObject);
            m_shooter.OnBulletDestroy(this);
        }
        else if (collision.TryGetComponent<MKKingKobutaFace>(out var king))
        {
            king.OnHit(this);
            Destroy(gameObject);
            m_shooter.OnBulletDestroy(this);
        }
    }
}
