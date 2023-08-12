using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKKingKobutaAIBlue : MonoBehaviour
{
    [SerializeField] private Transform m_player;
    [SerializeField] private float m_speed = 1f;
    [SerializeField] private float m_xMin = 1f;
    [SerializeField] private float m_xMax = 1f;
    [SerializeField] private float m_yMax = 1f;
    [SerializeField] private float m_NearDistanceMax = 2f;
    [SerializeField] private float m_FarDistanceMin = 5f;
    [SerializeField] private float m_playerPositionUpdateInterval = 3f;
    
    private MKKingKobutaFace m_face;
    private Vector3 m_playerPosition;

    void Start()
    {
        TryGetComponent(out m_face);
        StartCoroutine(UpdatePlayerPosition());
    }

    private IEnumerator UpdatePlayerPosition()
    {
        while (true)
        {
            m_playerPosition = m_player.position;
            yield return new WaitForSeconds(m_playerPositionUpdateInterval);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_face.hp <= 0) return;

        // 上下位置はプレーヤーに合わせる。
        // 現在位置
        var y = transform.position.y;
        // 目標位置
        var targetY = Mathf.Min(m_playerPosition.y, m_yMax);

        // 左右位置は、遠ければ近づき、近ければ遠ざかる。
        // 現在位置
        var x = transform.position.x;
        // 目標位置
        var distance = Vector3.Distance(transform.position, m_playerPosition);
        var targetX = transform.position.x;
        // 近い
        if (distance <= m_NearDistanceMax)
        {
            targetX = m_xMax;
        }
        // 遠い
        else if (distance >= m_FarDistanceMin)
        {
            targetX = Mathf.Max(m_playerPosition.x, m_xMin);
        }

        // 目標位置まで近づく。
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetX, targetY, transform.position.z), m_speed * Time.deltaTime);
    }
}
