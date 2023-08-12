using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MKPlayerKobuta : MonoBehaviour
{
    [field: SerializeField] public MKKobutaType Type { get; private set; }
    [SerializeField] private MKPlayerBullet m_bulletPrefab;
    private SpriteRenderer m_renderer;
    private Vector3 m_bulletPositionOffset = new Vector3(0.5f, 0, 0);
    private bool m_damaged = false;

    private void Awake()
    {
        TryGetComponent(out m_renderer);
    }

    public void Shoot()
    {
        var bullet = Instantiate(m_bulletPrefab, transform.position + m_bulletPositionOffset, Quaternion.identity);
        bullet.Initialize(Type, m_damaged);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
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
        m_damaged = true;
        m_renderer.color = new Color(1, 1, 1, 0.25f);
        MKUIManager.Instance.SetKobutaDamaged(Type, m_damaged);
        if (obj is MKFireball f)
        {
            f.OnPlayerHit(this);
        }
    }
}
