using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class MKPlayer : MKPlayerFormationUnit
{
    [SerializeField] private float m_speed = 7.5f;
    [SerializeField] private float m_xMin = -8.3f;
    [SerializeField] private float m_xMax = 8.3f;
    [SerializeField] private float m_yMin = -4.6f;
    [SerializeField] private float m_yMax = 4.6f;

    [SerializeField] private MKOption m_option1;
    [SerializeField] private MKOption m_option2;
    [SerializeField] private float m_rearrangeDuration = 0.25f;

    private MKPrototypeInputAction m_input;
    private bool m_prevFireButton = false;


    private void Awake()
    {
        m_input = new MKPrototypeInputAction();
        m_input.Enable();
        m_input.Player.Rearrange.performed += _ => Rearrange();
    }

    void Update()
    {
        Move();
        Shoot();
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    private void Move()
    {
        var move = m_input.Player.Move.ReadValue<Vector2>();
        var direction = new Vector3(move.x, move.y, 0f).normalized;
        transform.position += m_speed * Time.deltaTime * direction;

        // 画面外に出ないようにする。
        var x = Mathf.Clamp(transform.position.x, m_xMin, m_xMax);
        var y = Mathf.Clamp(transform.position.y, m_yMin, m_yMax);
        transform.position = new Vector3(x, y, transform.position.z);
    }

    /// <summary>
    /// 弾発射処理
    /// </summary>
    private void Shoot()
    {
        var fireButton = m_input.Player.Fire.IsPressed();
        // 前フレームで押されていなくて、今フレームで押された場合
        if (!m_prevFireButton && fireButton)
        {
            if (Kobuta.CanShoot())
            {
                Kobuta.Shoot();
            }
        }
        // 前フレームで押されていて、今フレームも押されている場合
        else if (m_prevFireButton && fireButton)
        {
            if (Kobuta.CanAutoShoot())
            {
                Kobuta.Shoot();
            }
        }
        m_prevFireButton = fireButton;
    }

    /// <summary>
    /// 隊列を並べ替えます。
    /// </summary>
    private void Rearrange()
    {
        // まず各コブタの親を付け替える。
        var playerKobuta = Kobuta;
        UpdateKobutaImage(m_option1.Kobuta);
        m_option1.UpdateKobutaImage(m_option2.Kobuta);
        m_option2.UpdateKobutaImage(playerKobuta);

        // 移動を開始する。
        StartRearrange(m_rearrangeDuration);
        m_option1.StartRearrange(m_rearrangeDuration);
        m_option2.StartRearrange(m_rearrangeDuration);

        // UIを更新する。
        MKUIManager.Instance.RearrangeKobuta(Kobuta, m_option1.Kobuta, m_option2.Kobuta);
    }
}
