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
        // 移動
        Move();
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    private void Move()
    {
        // 上下移動
        var newPosition = transform.position;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            newPosition.y += m_speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            newPosition.y -= m_speed * Time.deltaTime;
        }
        // 左右移動
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition.x -= m_speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            newPosition.x += m_speed * Time.deltaTime;
        }

        // 移動範囲を制限する。
        newPosition.x = Mathf.Clamp(newPosition.x, m_xMin, m_xMax);
        newPosition.y = Mathf.Clamp(newPosition.y, m_yMin, m_yMax);

        // 位置を更新する。
        transform.position = newPosition;
    }
}
