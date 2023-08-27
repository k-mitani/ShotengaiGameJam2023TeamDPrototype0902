using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MKFireball : MonoBehaviour
{
    [SerializeField] private float m_rotationSpeed = 360;
    [SerializeField] public Vector3 m_velocity = Vector3.left;

    private void Start()
    {
        Invoke(nameof(PlaySe), Random.value * 0.25f);
    }

    private void PlaySe()
    {
        MKSoundManager.Instance.PlaySeKingKobutaShoot();
    }

    internal void OnPlayerHit(MKPlayerKobuta mKPlayerKobuta)
    {
        Destroy(gameObject);
    }

    void Update()
    {
        transform.position += Time.deltaTime * m_velocity;
        transform.Rotate(Vector3.back, m_rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MKDestroyEnemyWall"))
        {
            Destroy(gameObject);
        }
    }
}
