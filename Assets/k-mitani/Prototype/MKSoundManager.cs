using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKSoundManager : MonoBehaviour
{
    public static MKSoundManager Instance { get; private set; }

    [SerializeField] private bool autoPlayBgm = true;
    [SerializeField] private MKSoundPlayerInstant playerPrefab;
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioClip bgm1;
    [SerializeField] private AudioClip bgm2;

    [SerializeField] private AudioClip sePlayerShoot;
    public void PlaySePlayerShoot() => PlaySe(sePlayerShoot);

    [SerializeField] private AudioClip sePlayerDamaged;
    public void PlaySePlayerDamaged() => PlaySe(sePlayerDamaged);

    [SerializeField] private AudioClip seEnemyDamaged;
    public void PlaySeEnemyDamaged() => PlaySe(seEnemyDamaged);

    [SerializeField] private AudioClip seKingKobutaShoot;
    public void PlaySeKingKobutaShoot() => PlaySe(seKingKobutaShoot);

    [SerializeField] private AudioClip seSandwichBurned;
    public void PlaySeSandwichBurned() => PlaySe(seSandwichBurned);

    [SerializeField] private AudioClip sePlayerFormationChanged;
    public void PlaySePlayerFormationChanged() => PlaySe(sePlayerFormationChanged);

    private void Awake()
    {
        if (Instance != null)
        {
            if (Instance.autoPlayBgm)
            {
                Instance.PlayBgm();
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
            PlayBgm();
        }
    }

    public void PlayBgm()
    {
        if (bgmSource.isPlaying) return;
        bgmSource.Play();
    }

    public void StopBgm()
    {
        bgmSource.Stop();
    }

    private void PlaySe(AudioClip clip)
    {
        var player = Instantiate(playerPrefab);
        player.Play(clip, false, true);
    }
}
