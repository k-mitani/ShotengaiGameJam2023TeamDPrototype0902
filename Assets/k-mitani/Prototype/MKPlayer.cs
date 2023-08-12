using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MKPlayer : MKPlayerFormationUnit
{
    [SerializeField] private float m_speed = 1;
    [SerializeField] private float m_xMin = -8.3f;
    [SerializeField] private float m_xMax = 8.3f;
    [SerializeField] private float m_yMin = -4.6f;
    [SerializeField] private float m_yMax = 4.6f;

    void Update()
    {
        // �ړ�
        Move();
    }

    /// <summary>
    /// �ړ�����
    /// </summary>
    private void Move()
    {
        // �㉺�ړ�
        var newPosition = transform.position;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            newPosition.y += m_speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            newPosition.y -= m_speed * Time.deltaTime;
        }
        // ���E�ړ�
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition.x -= m_speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            newPosition.x += m_speed * Time.deltaTime;
        }

        // �ړ��͈͂𐧌�����B
        newPosition.x = Mathf.Clamp(newPosition.x, m_xMin, m_xMax);
        newPosition.y = Mathf.Clamp(newPosition.y, m_yMin, m_yMax);

        // �ʒu���X�V����B
        transform.position = newPosition;
    }
}
