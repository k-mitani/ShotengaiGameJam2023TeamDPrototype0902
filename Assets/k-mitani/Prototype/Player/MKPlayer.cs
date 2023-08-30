using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class MKPlayer : MKPlayerFormationUnit
{
    public static MKPlayer Instance { get; private set; }

    [SerializeField] private float m_speed = 7.5f;
    [SerializeField] public float m_xMin = -8.3f;
    [SerializeField] public float m_xMax = 8.3f;
    [SerializeField] public float m_yMin = -4.6f;
    [SerializeField] public float m_yMax = 4.6f;

    [SerializeField] public MKOption m_option1;
    [SerializeField] public MKOption m_option2;
    [SerializeField] private float m_rearrangeDuration = 0.25f;

    [field: SerializeField] public int BulletCountMax { get; private set; } = 3;
    private List<MKPlayerBullet> m_bullets = new List<MKPlayerBullet>();
    public bool CanShoot => m_bullets.Count < BulletCountMax;
    public void OnBulletShoot(MKPlayerBullet bullet) => m_bullets.Add(bullet);
    public void OnBulletDestroy(MKPlayerBullet bullet) => m_bullets.Remove(bullet);

    private MKPrototypeInputAction m_input;
    private bool m_prevFireButton = false;


    private void Awake()
    {
        Instance = this;
        m_input = new MKPrototypeInputAction();
        m_input.Enable();
        m_input.Player.Rearrange.performed += _ => Rearrange();
        m_input.Player.Pause.performed += _ => MKUIManager.Instance.TogglePause();
        m_input.UI.Pause.performed += _ => MKUIManager.Instance.TogglePause();
        SetUiMode(false);
    }

    public void SetUiMode(bool on)
    {
        if (on)
        {
            m_input.Player.Disable();
            m_input.UI.Enable();
        }
        else
        {
            m_input.Player.Enable();
            m_input.UI.Disable();
        }
    }


    protected override void Start()
    {
        base.Start();
        Kobuta.Damaged += Kobuta_Damaged;
        m_option1.Kobuta.Damaged += Kobuta_Damaged;
        m_option2.Kobuta.Damaged += Kobuta_Damaged;
        Kobuta.HealItemGained += Kobuta_HealItemGained;
        m_option1.Kobuta.HealItemGained += Kobuta_HealItemGained;
        m_option2.Kobuta.HealItemGained += Kobuta_HealItemGained;
    }

    void Update()
    {
        Move();
        Shoot();
    }

    /// <summary>
    /// �ړ�����
    /// </summary>
    private void Move()
    {
        var move = m_input.Player.Move.ReadValue<Vector2>();
        var direction = new Vector3(move.x, move.y, 0f).normalized;
        transform.position += m_speed * Time.deltaTime * direction;

        // ��ʊO�ɏo�Ȃ��悤�ɂ���B
        var x = Mathf.Clamp(transform.position.x, m_xMin, m_xMax);
        var y = Mathf.Clamp(transform.position.y, m_yMin, m_yMax);
        transform.position = new Vector3(x, y, transform.position.z);
    }

    /// <summary>
    /// �e���ˏ���
    /// </summary>
    private void Shoot()
    {
        var fireButton = m_input.Player.Fire.IsPressed();
        // �O�t���[���ŉ�����Ă��Ȃ��āA���t���[���ŉ����ꂽ�ꍇ
        if (!m_prevFireButton && fireButton)
        {
            if (Kobuta.CanShoot())
            {
                Kobuta.Shoot();
            }
        }
        // �O�t���[���ŉ�����Ă��āA���t���[����������Ă���ꍇ
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
    /// �������בւ��܂��B
    /// </summary>
    private void Rearrange()
    {
        // �܂��e�R�u�^�̐e��t���ւ���B
        var playerKobuta = Kobuta;
        UpdateKobutaImage(m_option1.Kobuta);
        
        m_option1.UpdateKobutaImage(m_option2.Kobuta);
        m_option2.UpdateKobutaImage(playerKobuta);

        // �ړ����J�n����B
        StartRearrange(m_rearrangeDuration);
        m_option1.StartRearrange(m_rearrangeDuration);
        m_option2.StartRearrange(m_rearrangeDuration);

        // UI���X�V����B
        MKUIManager.Instance.RearrangeKobuta(Kobuta, m_option1.Kobuta, m_option2.Kobuta);
        MKSoundManager.Instance.PlaySePlayerFormationChanged();
    }

    private void Kobuta_Damaged(object sender, EventArgs e)
    {
        if (MKUIManager.Instance.IsGameOver) return;

        // �S���̃R�u�^�����ꂽ��Q�[���I�[�o�[
        var gameOver = Kobuta.IsDamaged && m_option1.Kobuta.IsDamaged && m_option2.Kobuta.IsDamaged;
        if (gameOver)
        {
            MKUIManager.Instance.OnGameOver();
            return;
        }

        // �S�ẴR�u�^�̖��G���Ԃ��J�n����B
        Kobuta.StartDamagedMuteki();
        m_option1.Kobuta.StartDamagedMuteki();
        m_option2.Kobuta.StartDamagedMuteki();
    }

    private LifeUpItem prevGainedItem;
    private void Kobuta_HealItemGained(object sender, LifeUpItem item)
    {
        if (MKUIManager.Instance.IsGameOver) return;

        // ���܂ɕ����̃R�u�^�œ����ɓ����蔻�肪�����Ȃ��邱�Ƃ�����̂ő΍􂷂�B
        if (item == prevGainedItem) return;
        prevGainedItem = item;

        var receiver = sender as MKPlayerKobuta;
        var kobutas = new[] { receiver, Kobuta, m_option1.Kobuta, m_option2.Kobuta };
        foreach (var kobuta in kobutas)
        {
            if (!kobuta.IsDamaged) continue;
            kobuta.Heal();
            item.OnPlayerHit(kobuta);
            MKSoundManager.Instance.PlaySePlayerHealed();
            break;
        }
        // �_���[�W�𕉂����R�u�^�����Ȃ���΂Ƃ肠�����A�C�e���̍폜�����s���B
        item.OnPlayerHit(receiver);
    }

    private void OnDestroy()
    {
        m_input.Dispose();
        if (Instance == this) Instance = null;
    }
}
