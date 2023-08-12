using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKFireball : MonoBehaviour
{
    [SerializeField] private float m_speed = 1;
    [SerializeField] private float m_rotationSpeed = 360;

    internal void OnPlayerHit(MKPlayerKobuta mKPlayerKobuta)
    {
        Destroy(gameObject);
    }

    void Update()
    {
        transform.position += m_speed * Time.deltaTime * Vector3.left;
        transform.Rotate(Vector3.back, m_rotationSpeed * Time.deltaTime);
    }
}
