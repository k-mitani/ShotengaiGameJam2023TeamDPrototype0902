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
        // �e����
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.X))
        {
            Kobuta.Shoot();
        }
    }

    /// <summary>
    /// �ړ�����
    /// </summary>
    private void Move()
    {
        // �㉺�ړ�
        var moveAmount = Vector3.zero;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveAmount += Vector3.up;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            moveAmount -= Vector3.up;
        }
        // ���E�ړ�
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveAmount += Vector3.left;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            moveAmount += Vector3.right;
        }
        var newPosition = transform.position + m_speed * Time.deltaTime * moveAmount.normalized;

        // �ړ��͈͂𐧌�����B
        newPosition.x = Mathf.Clamp(newPosition.x, m_xMin, m_xMax);
        newPosition.y = Mathf.Clamp(newPosition.y, m_yMin, m_yMax);

        // �ʒu���X�V����B
        transform.position = newPosition;
    }
}
