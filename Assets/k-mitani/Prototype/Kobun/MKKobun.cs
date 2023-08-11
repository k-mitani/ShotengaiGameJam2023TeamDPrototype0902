﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MKKobun : MonoBehaviour
{
    [SerializeField] private MKKobunColorType m_colorType;
    [SerializeField] private float hp = 3;
    [SerializeField] private Animator m_animator;
    [SerializeField] private SpriteRenderer m_renderer;
    [SerializeField] private MKPopupText m_popupTextPrefab;

    private CircleCollider2D m_collider;

    private void Awake()
    {
        TryGetComponent(out m_collider);
        m_animator.SetInteger("KobunColorType", (int)m_colorType);
    }

    public void OnHit(MKPlayerBullet bullet)
    {
        var favorite = IsColorMatched(bullet);

        var damage = favorite ? 3 : 1;
        hp -= damage;
        var pop = Instantiate(m_popupTextPrefab, transform.position, Quaternion.identity);
        pop.SetText(favorite ? "+500🥰" : "+100😋");
        if (hp <= 0)
        {
            StartCoroutine(AfterDead());
        }
    }

    private IEnumerator AfterDead()
    {
        m_collider.enabled = false;
        var durationMax = 0.2f;
        var duration = 0f;
        while (duration < durationMax)
        {
            duration += Time.deltaTime;
            var rate = duration / durationMax;
            m_renderer.color = new Color(1, 1, 1, 1 - rate);
            yield return null;
        }
        Destroy(gameObject);
    }

    private bool IsColorMatched(MKPlayerBullet bullet)
    {
        return (int)bullet.KobutaType == (int)m_colorType;
    }
}

public enum MKKobunColorType
{
    Red = 0,
    Green = 1,
    Blue = 2,
}