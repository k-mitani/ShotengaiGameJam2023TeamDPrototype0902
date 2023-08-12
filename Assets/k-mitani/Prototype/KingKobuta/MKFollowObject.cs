using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKFollowObject : MonoBehaviour
{
    [SerializeField] private Transform m_target;
    [SerializeField] private Transform m_base;
    [SerializeField] private float m_speed = 1f;
    [SerializeField] private float m_targetDistanceMin = 1f;
    [SerializeField] private float m_baseDistanceMax = 1f;

    void Update()
    {
        var distance = Vector3.Distance(transform.position, m_target.position);
        if (distance > m_targetDistanceMin)
        {
            var direction = (m_target.position - transform.position).normalized;
            var newPosition = transform.position + direction * m_speed * Time.deltaTime;
            // ベースから遠すぎないようにする。
            var baseDistance = Vector3.Distance(m_base.position, newPosition);
            if (baseDistance >= m_baseDistanceMax)
            {
                // べースから遠すぎる場合は、ベースの方向に移動する。
                var diff = baseDistance - m_baseDistanceMax;
                var baseDirection = (m_base.position - transform.position).normalized;
                newPosition = transform.position + baseDirection * diff;
            }
            transform.position = newPosition;
        }
    }
}
