using System;
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
    [SerializeField] private TextMeshPro m_text;

    private CircleCollider2D m_collider;

    private void Awake()
    {
        TryGetComponent(out m_collider);
        m_animator.SetInteger("KobunColorType", (int)m_colorType);
    }

    public void OnHit(MKPlayerBullet bullet)
    {
        var damage = IsColorMatched(bullet) ? 3 : 1;
        hp -= damage;
        if (hp <= 0)
        {
            StartCoroutine(AfterDead());
        }
    }

    private IEnumerator AfterDead()
    {
        m_text.gameObject.SetActive(true);
        m_collider.enabled = false;
        var durationMax = 0.2f;
        var duration = 0f;
        while (duration < durationMax)
        {
            duration += Time.deltaTime;
            var rate = duration / durationMax;
            m_renderer.color = new Color(1, 1, 1, 1 - rate);
            m_text.transform.position += Vector3.up * Time.deltaTime * 1f;
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
