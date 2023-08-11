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
        // 弾発射
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.X))
        {
            Kobuta.Shoot();
        }
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    private void Move()
    {
        // 上下移動
        var moveAmount = Vector3.zero;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveAmount += Vector3.up;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            moveAmount -= Vector3.up;
        }
        // 左右移動
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveAmount += Vector3.left;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            moveAmount += Vector3.right;
        }
        var newPosition = transform.position + m_speed * Time.deltaTime * moveAmount.normalized;

        // 移動範囲を制限する。
        newPosition.x = Mathf.Clamp(newPosition.x, m_xMin, m_xMax);
        newPosition.y = Mathf.Clamp(newPosition.y, m_yMin, m_yMax);

        // 位置を更新する。
        transform.position = newPosition;
    }
}
