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


    private void Awake()
    {
        TryGetComponent(out m_renderer);
    }

    public void Shoot()
    {
        var bullet = Instantiate(m_bulletPrefab, transform.position + m_bulletPositionOffset, Quaternion.identity);
        bullet.Initialize(Type);
    }
}
