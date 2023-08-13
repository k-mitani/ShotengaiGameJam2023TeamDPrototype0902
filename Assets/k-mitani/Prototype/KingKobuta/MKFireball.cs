using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKFireball : MonoBehaviour
{
    [SerializeField] private float m_rotationSpeed = 360;
    [SerializeField] public Vector3 m_velocity = Vector3.left;

    internal void OnPlayerHit(MKPlayerKobuta mKPlayerKobuta)
    {
        Destroy(gameObject);
    }

    void Update()
    {
        transform.position += Time.deltaTime * m_velocity;
        transform.Rotate(Vector3.back, m_rotationSpeed * Time.deltaTime);
    }
}
