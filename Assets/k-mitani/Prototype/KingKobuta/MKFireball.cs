using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MKFireball : MonoBehaviour
{
    [SerializeField] private float m_rotationSpeed = 360;
    [SerializeField] public Vector3 m_velocity = Vector3.left;

    private CircleCollider2D m_collider;

    public float ScaleDecay { get; set; } = 1;
    public float DecayDelayTime { get; set; } = -1;
    private bool m_isDecaying = false;

    private void Start()
    {
        TryGetComponent(out m_collider);
        Invoke(nameof(PlaySe), Random.value * 0.25f);

        StartCoroutine(WaitStartDecay());
    }

    private IEnumerator WaitStartDecay()
    {
        if (DecayDelayTime < 0) yield break;
        yield return new WaitForSeconds(DecayDelayTime);
        m_isDecaying = true;
    }

    private void PlaySe()
    {
        MKSoundManager.Instance.PlaySeKingKobutaShoot();
    }

    internal void OnPlayerHit(MKPlayerKobuta mKPlayerKobuta)
    {
        Destroy(gameObject);
    }

    internal void OnPlayerBulletHit(MKPlayerBullet mKPlayerBullet)
    {
        //Destroy(gameObject);
    }

    void Update()
    {
        transform.position += Time.deltaTime * m_velocity;
        transform.Rotate(Vector3.back, m_rotationSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (!m_isDecaying) return;
        transform.localScale *= ScaleDecay * ScaleDecay;

        if (transform.localScale.x < 0.9)
        {
            if (m_collider.enabled) m_collider.enabled = false;
            transform.localScale *= ScaleDecay * ScaleDecay;
            if (transform.localScale.x < 0.2)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MKDestroyEnemyWall"))
        {
            Destroy(gameObject);
        }
    }
}
