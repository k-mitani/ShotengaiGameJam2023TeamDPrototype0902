using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MKKingKobutaFace : MonoBehaviour
{
    [SerializeField] private MKKobunColorType m_colorType;
    [SerializeField] public float hp = 300;
    [SerializeField] private SpriteRenderer m_renderer;
    [SerializeField] private MKFollowObject[] m_necks;
    [SerializeField] private MKPopupText m_popupTextPrefab;
    [SerializeField] private Vector3 popupOffset;
    [SerializeField] private Transform m_fireballStartPosition;
    [SerializeField] private MKFireball m_fireballPrefab;
    [SerializeField] private MKKingKobuta m_kingKobuta;
    [SerializeField] private Sprite m_deadFaceImage;
    [SerializeField] private ObjectShake os;
    private MKPlayer m_player;
    private SpriteRenderer m_spriteRenderer;
    [NonSerialized] public CircleCollider2D m_collider;

    public bool ShouldPause => m_kingKobuta.ShouldPause;
    public bool IsDead => hp <= 0;
    public event EventHandler Dead;

    private void Awake()
    {
        m_player = FindObjectOfType<MKPlayer>();
        TryGetComponent(out m_spriteRenderer);
        TryGetComponent(out m_collider);
    }

    public void OnHit(MKPlayerBullet bullet, float moveDistance)
    {
        if (MKUIManager.Instance.IsGameOver) return;

        var favorite = IsColorMatched(bullet);

        var damage = favorite ? 3 : 1;
        hp -= damage;
        var pop = Instantiate(m_popupTextPrefab, transform.position + popupOffset, Quaternion.identity);

        var score = favorite ? 500 : 100;
        if (bullet.IsWeak) score /= 2;
        if (moveDistance > 1)
        {
            var adj = (10 - (moveDistance - 1)) / 10f;
            score = (int)Mathf.Max(score * adj, 1);
        }
        if(!(os==null))
        {
            os.istime = true;
        }
        MKUIManager.Instance.AddScore(score);
        MKSoundManager.Instance.PlaySeEnemyDamaged();

        pop.SetText($"+{score}{(favorite ? "🥰" : "😋")}");
        if (IsDead)
        {
            StartCoroutine(AfterDead());
            Dead?.Invoke(this, EventArgs.Empty);
        }
    }

    private IEnumerator AfterDead()
    {
        m_collider.enabled = false;
        var neckRenderers = m_necks.Select(n => n.GetComponent<SpriteRenderer>()).ToArray();
        var durationMax = 0.2f;
        var duration = 0f;
        m_spriteRenderer.sprite = m_deadFaceImage;
        while (duration < durationMax)
        {
            duration += Time.deltaTime;
            var rate = duration / durationMax;
            var value = 1 - rate * 0.5f;
            m_renderer.color = new Color(value, value, value);
            foreach (var neckRenderer in neckRenderers)
            {
                neckRenderer.color = new Color(value, value, value);
            }
            yield return null;
        }
        //Destroy(gameObject);
    }

    private bool IsColorMatched(MKPlayerBullet bullet)
    {
        return (int)bullet.KobutaType == (int)m_colorType;
    }

    public void Shoot()
    {
        var fb = Instantiate(m_fireballPrefab, m_fireballStartPosition.position, Quaternion.identity);
        fb.m_velocity = (m_player.transform.position - fb.transform.position).normalized;
        //fireball.Initialize(m_colorType);
    }

    public void Shoot3()
    {
        var velocity = (m_player.transform.position - m_fireballStartPosition.position).normalized;
        var fb = Instantiate(m_fireballPrefab, m_fireballStartPosition.position, Quaternion.identity);
        fb.m_velocity = velocity;
        var fb2 = Instantiate(m_fireballPrefab, m_fireballStartPosition.position, Quaternion.identity);
        fb2.m_velocity = Quaternion.Euler(0, 0, +30) * velocity;
        var fb3 = Instantiate(m_fireballPrefab, m_fireballStartPosition.position, Quaternion.identity);
        fb3.m_velocity = Quaternion.Euler(0, 0, -30) * velocity;
    }

    public void ShootFast()
    {
        var fb = Instantiate(m_fireballPrefab, m_fireballStartPosition.position, Quaternion.identity);
        fb.m_velocity = (m_player.transform.position - fb.transform.position).normalized * 3;
    }
}
