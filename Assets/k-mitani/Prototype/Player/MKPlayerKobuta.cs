using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MKPlayerKobuta : MonoBehaviour
{
    [field: SerializeField] public MKKobutaType Type { get; private set; }
    [SerializeField] private MKPlayerBullet m_bulletPrefab;
    [field: SerializeField] public int BulletCountMax { get; private set; } = 3;
    [field: SerializeField] public float AutoShootIntervalMax { get; private set; } = 0.25f;

    private SpriteRenderer m_renderer;
    private Vector3 m_bulletPositionOffset = new Vector3(0.5f, 0, 0);
    public bool IsDamaged { get; private set; } = false;

    private List<MKPlayerBullet> m_bullets = new List<MKPlayerBullet>();
    private float m_autoShootInterval = 0f;

    private float m_damagedMutekiDurationMax = 2.0f;
    private float m_damagedMutekiDuration = 0f;
    private bool m_isInDamagedMuteki = false;
    public event EventHandler Damaged;
    public void StartDamagedMuteki()
    {
        StartCoroutine(DamagedMutekiLoop());
    }

    IEnumerator DamagedMutekiLoop()
    {
        var originalColor = m_renderer.color;
        var color1 = new Color(1, 1, 1, originalColor.a * 1);
        var color2 = new Color(1, 1, 1, originalColor.a * 0.5f);
        m_renderer.color = color1;

        m_isInDamagedMuteki = true;
        m_damagedMutekiDuration = m_damagedMutekiDurationMax;
        var blinkDurationMax = 0.1f;
        var blinkDuration = blinkDurationMax;
        while (m_damagedMutekiDuration > 0)
        {
            m_damagedMutekiDuration -= Time.deltaTime;
            blinkDuration -= Time.deltaTime;
            if (blinkDuration <= 0)
            {
                blinkDuration = blinkDurationMax;
                if (m_renderer.color == color1) m_renderer.color = color2;
                else m_renderer.color = color1;
            }
            yield return null;
        }
        m_isInDamagedMuteki = false;
        m_renderer.color = originalColor;
    }

    private void Awake()
    {
        TryGetComponent(out m_renderer);
    }

    private void Update()
    {
        if (m_autoShootInterval > 0)
        {
            m_autoShootInterval -= Time.deltaTime;
        }
    }

    public bool CanShoot() => m_bullets.Count < BulletCountMax;
    public bool CanAutoShoot() => CanShoot() && m_autoShootInterval <= 0;

    public void Shoot()
    {
        var bullet = Instantiate(m_bulletPrefab, transform.position + m_bulletPositionOffset, Quaternion.identity);
        bullet.Initialize(this);
        m_bullets.Add(bullet);
        m_autoShootInterval = AutoShootIntervalMax;
    }

    public void OnBulletDestroy(MKPlayerBullet bullet)
    {
        m_bullets.Remove(bullet);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // すでにダメージを受けていたら何もしない。
        if (IsDamaged)
        {
            return;
        }

        Debug.Log("Kobuta Hit with " + collision.name);
        if (collision.TryGetComponent(out MKKobun kobun))
        {
            OnDamage(kobun);
        }
        else if (collision.TryGetComponent(out MKKingKobutaFace king))
        {
            OnDamage(king);
        }
        else if (collision.TryGetComponent(out MKFireball fireball))
        {
            OnDamage(fireball);
        }
    }

    private void OnDamage(MonoBehaviour obj)
    {
        Debug.Log("Hit!!");
        if (!m_isInDamagedMuteki)
        {
            IsDamaged = true;
            m_renderer.color = new Color(1, 1, 1, 0.25f);
            MKUIManager.Instance.SetKobutaDamaged(Type, IsDamaged);
            MKUIManager.Instance.ShakeCamera();
            Damaged?.Invoke(this, EventArgs.Empty);
        }
        // ファイアーボールは無敵時間中でも接触したら消せるようにしてみる。
        if (obj is MKFireball f)
        {
            f.OnPlayerHit(this);
        }
    }
}
