using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MKSoundManager : MonoBehaviour
{
    public static MKSoundManager Instance { get; private set; }

    [SerializeField] private bool autoPlayBgm = true;
    [SerializeField] private MKSoundPlayerInstant playerPrefab;
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioClip[] bgms;

    [SerializeField] private AudioClip sePlayerShoot;
    public void PlaySePlayerShoot() => PlayBattleSe(sePlayerShoot);

    [SerializeField] private AudioClip sePlayerDamaged;
    public void PlaySePlayerDamaged() => PlayBattleSe(sePlayerDamaged);

    [SerializeField] private AudioClip seEnemyDamaged;
    public void PlaySeEnemyDamaged() => PlayBattleSe(seEnemyDamaged);

    [SerializeField] private AudioClip seKingKobutaShoot;
    public void PlaySeKingKobutaShoot() => PlayBattleSe(seKingKobutaShoot);

    [SerializeField] private AudioClip seSandwichBurned;
    public void PlaySeSandwichBurned() => PlayBattleSe(seSandwichBurned);

    [SerializeField] private AudioClip sePlayerFormationChanged;
    public void PlaySePlayerFormationChanged() => PlayBattleSe(sePlayerFormationChanged);

    [SerializeField] private AudioClip sePlayerHealed;
    public void PlaySePlayerHealed() => PlayBattleSe(sePlayerHealed);

    private void Awake()
    {
        if (Instance != null)
        {
            if (Instance.autoPlayBgm)
            {
                Instance.PlayBgmRandom();
            }
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (autoPlayBgm)
        {
            PlayBgmRandom();
        }
    }

    public void PlayBgmRandom()
    {
        //if (bgmSource.isPlaying) return;
        bgmSource.clip = bgms[Random.Range(0, bgms.Length)];
        bgmSource.Play();
    }

    public void StopBgm()
    {
        bgmSource.Stop();
    }

    private void PlayBattleSe(AudioClip clip)
    {
        // 戦闘SEは、ゲームオーバー中なら鳴らさない。
        if (SceneManager.GetActiveScene().name.StartsWith("MKPrototypeScene"))
        {
            if (MKUIManager.Instance?.IsGameOver ?? false)
            {
                return;
            }
        }
        PlaySe(clip);
    }

    private void PlaySe(AudioClip clip)
    {
        var player = Instantiate(playerPrefab);
        player.Play(clip, false, true);
    }
}
