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
    private bool m_isHit = false;
    private Vector3 m_startPosition;

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

        m_startPosition = transform.position;
    }

    void Update()
    {
        transform.position += m_speed * Time.deltaTime * Vector3.right;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���łɒ��e�����ς݂Ȃ牽�����Ȃ��B
        if (m_isHit) return;

        if (collision.CompareTag("MKOutOfScreenWall"))
        {
            Destroy(gameObject);
            m_shooter.OnBulletDestroy(this);
            m_isHit = true;
        }
        else if (collision.TryGetComponent<MKKobun>(out var kobun))
        {
            var moveDistance = Vector3.Distance(m_startPosition, transform.position);
            kobun.OnHit(this, moveDistance);
            Destroy(gameObject);
            m_shooter.OnBulletDestroy(this);
            m_isHit = true;
        }
        else if (collision.TryGetComponent<MKKingKobutaFace>(out var king))
        {
            var moveDistance = Vector3.Distance(m_startPosition, transform.position);
            king.OnHit(this, moveDistance);
            Destroy(gameObject);
            m_shooter.OnBulletDestroy(this);
            m_isHit = true;
        }
    }
}
