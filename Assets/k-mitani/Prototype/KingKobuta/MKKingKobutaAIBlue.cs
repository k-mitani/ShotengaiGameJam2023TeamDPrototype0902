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

        // �㉺�ʒu�̓v���[���[�ɍ��킹��B
        // ���݈ʒu
        var y = transform.position.y;
        // �ڕW�ʒu
        var targetY = Mathf.Min(m_playerPosition.y, m_yMax);

        // ���E�ʒu�́A������΋߂Â��A�߂���Ή�������B
        // ���݈ʒu
        var x = transform.position.x;
        // �ڕW�ʒu
        var distance = Vector3.Distance(transform.position, m_playerPosition);
        var targetX = transform.position.x;
        // �߂�
        if (distance <= m_NearDistanceMax)
        {
            targetX = m_xMax;
        }
        // ����
        else if (distance >= m_FarDistanceMin)
        {
            targetX = Mathf.Max(m_playerPosition.x, m_xMin);
        }

        // �ڕW�ʒu�܂ŋ߂Â��B
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetX, targetY, transform.position.z), m_speed * Time.deltaTime);
    }
}
