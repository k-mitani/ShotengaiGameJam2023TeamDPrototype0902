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

    [SerializeField] private AudioClip seBossAlert;
    public void PlaySeBossAlert() => PlayBattleSe(seBossAlert, 0.6f);

    [SerializeField] private AudioClip seStageClear;
    public void PlaySeStageClear() => PlaySe(seStageClear, dontDestroyOnLoad: true);

    [SerializeField] private AudioClip seShowRanking;
    public void PlaySeShowRanking() => PlaySe(seShowRanking);

    [SerializeField] private AudioClip seCheers;
    public void PlaySeCheers() => PlaySe(seCheers);

    [SerializeField] private AudioClip seSceneChange;
    public void PlaySeSceneChange() => PlaySe(seSceneChange, dontDestroyOnLoad: true);


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

    public void SetBGMPitch(float pitch)
    {
        bgmSource.pitch = pitch;
    }

    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = volume;
    }

    public void StopBgm()
    {
        bgmSource.Stop();
    }

    private void PlayBattleSe(AudioClip clip, float? volume = null)
    {
        // 戦闘SEは、ゲームオーバー中なら鳴らさない。
        if (SceneManager.GetActiveScene().name.StartsWith("MKPrototypeScene"))
        {
            if (MKUIManager.Instance.IsGameOver)
            {
                return;
            }
        }
        // タイトルシーンでも鳴らさない。
        if (SceneManager.GetActiveScene().name.StartsWith("TitleScene"))
        {
            if (MKUIManager.Instance.IsDemo)
            {
                return;
            }
        }

        PlaySe(clip, volume);
    }

    private void PlaySe(AudioClip clip, float? volume = null, bool dontDestroyOnLoad = false)
    {
        var player = Instantiate(playerPrefab);
        if (dontDestroyOnLoad) DontDestroyOnLoad(player);
        player.Play(clip, false, true);
        if (volume != null)
        {
            player.source.volume = volume.Value;
        }
    }
}
