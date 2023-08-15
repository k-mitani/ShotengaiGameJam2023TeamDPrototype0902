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
        IsDamaged = true;
        m_renderer.color = new Color(1, 1, 1, 0.25f);
        MKUIManager.Instance.SetKobutaDamaged(Type, IsDamaged);
        if (obj is MKFireball f)
        {
            f.OnPlayerHit(this);
        }
    }
}
