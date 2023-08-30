using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    [SerializeField] private SceneTransitionCurtain curtain = null;
    [SerializeField] private TextMeshProUGUI pressAnyKey;
    [SerializeField] private float textBlinkDurationMax = 1f;

    [Header("Demo")]
    [SerializeField] private MKPlayer player;
    [SerializeField] private Transform wavesParent;
    [SerializeField] private MKItemSpawner itemSpawner;

    private List<IDisposable> disposables = new List<IDisposable>();

    // Start is called before the first frame update
    void Start()
    {
        var pressAnyKeyAction = new InputAction(
            type: InputActionType.PassThrough,
            binding: "*/<Button>",
            interactions: "Press");
        disposables.Add(pressAnyKeyAction);
        pressAnyKeyAction.Enable();
        pressAnyKeyAction.performed += _ =>
        {
            pressAnyKeyAction.Disable();
            StartCoroutine(LoadingSceneManager.LoadCoroutine("MKPrototypeScene", curtain));
        };

        itemSpawner.ItemSpawned += ItemSpawner_ItemSpawned;

        StartCoroutine(MKUtil.BlinkText(pressAnyKey, textBlinkDurationMax));

        StartCoroutine(DemoControl());
        StartCoroutine(DemoTimerControl());
    }

    private IEnumerator DemoTimerControl()
    {
        while (true)
        {
            yield return null;
            demoLastShootInterval += Time.deltaTime;
            demoLastRearrangeInterval += Time.deltaTime;
        }
    }


    private MKKobun demoTarget;
    private MKKobun[] demoEnemies;
    private Transform spawnedItem;
    
    private float demoLastShootInterval = 0f;
    private bool CanShoot => demoLastShootInterval > 0.25f;
    private float demoLastRearrangeInterval = 0f;
    private bool CanRearrange => demoLastRearrangeInterval > 0f;

    private void ItemSpawner_ItemSpawned(object sender, Transform e)
    {
        // �����o���Ă���Z�b�g����B
        StartCoroutine(Do());
        IEnumerator Do()
        {
            yield return new WaitForSeconds(2.0f);
            spawnedItem = e;
        }
    }

    private IEnumerator DemoControl()
    {
        yield return new WaitForSeconds(5f);
        while (true)
        {
            var playerPos = player.transform.position;

            if (spawnedItem != null)
            {
                if (spawnedItem.IsDestroyed())
                {
                    spawnedItem = null;
                    continue;
                }
                // �A�C�e����Y�ʒu, ��ʍ��[�ֈړ����đ҂�
                var targetPosition = new Vector3(player.m_xMin, spawnedItem.position.y, 0);
                
                // y�ʒu���\���߂���Αҋ@����B
                if (Mathf.Abs(targetPosition.y - playerPos.y) < 0.5f &&
                    targetPosition.x > playerPos.x)
                {
                    yield return null;
                    continue;
                }

                var dir = (targetPosition - playerPos).normalized;
                var mov = player.m_speed * Time.deltaTime * dir;
                if (!player.m_input.Player.Move.IsPressed())
                {
                    player.transform.position += mov;
                }
                yield return null;
                continue;
            }

            if (demoTarget == null || demoTarget.IsDestroyed())
            {
                demoEnemies = wavesParent.GetComponentsInChildren<MKKobun>();
                demoTarget = demoEnemies
                    .Where(e => e.transform.position.x > player.m_xMin + 1)
                    .Where(e => e.transform.position.x < player.m_xMax - 2)
                    // ���Y�ʒu���߂��G���U���ΏۂƂ���
                    .OrderBy(e =>
                        Mathf.Abs(e.transform.position.y - playerPos.y) +
                        Mathf.Abs(e.transform.position.x - playerPos.x)
                    )
                    .FirstOrDefault();

                if (demoTarget == null || demoTarget.IsDestroyed())
                {
                    yield return null;
                    continue;
                }
            }

            // �G�����ɍs���߂�������߂�B
            var targetPos = demoTarget.transform.position;
            if (targetPos.x < player.m_xMin + 2)
            {
                demoTarget = null;
                yield return null;
                continue;
            }

            // y�ʒu�̍�
            var yDiff = Mathf.Abs(targetPos.y - playerPos.y);
            var xDiff = Mathf.Abs(targetPos.x - playerPos.x);
            // ����
            var distance = Vector3.Distance(targetPos, playerPos);

            // �擪�R�u�^�ƓG�̐F������Ă���ΐ؂�ւ���B
            if ((int)player.Kobuta.Type != (int)demoTarget.m_colorType)
            {
                if (CanRearrange)
                {
                    player.Rearrange();
                }
            }

            // y�ʒu���قړ������āA�������\���߂���΁A�e�����B
            if (yDiff < 0.5f && distance < 7f)
            {
                if (CanShoot)
                {
                    player.Kobuta.Shoot();
                    demoLastShootInterval = 0f;
                }
            }

            // �G�̈ʒu�ɋ߂Â��B
            var moveTargetPos = demoTarget.transform.position + Vector3.left * 4;
            var direction = (moveTargetPos - playerPos).normalized;
            var movement = player.m_speed * Time.deltaTime * direction;
            // �������Ay�ʒu���قړ������āA�������\���߂��āA�߂����Ȃ����X�ʒu�͈ړ����Ȃ��B
            if (yDiff < 0.5f && xDiff < 4 && xDiff > 3)
            {
                movement.x = 0;
            }

            if (!player.m_input.Player.Move.IsPressed())
            {
                player.transform.position += movement;
            }

            yield return null;
        }
    }

    private void OnDestroy()
    {
        foreach (var disposable in disposables)
        {
            disposable.Dispose();
        }
    }
}
