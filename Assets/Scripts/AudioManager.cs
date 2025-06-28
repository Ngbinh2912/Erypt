using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource defaultMusicSource;
    [SerializeField] private AudioSource BossMusicSource;
    [SerializeField] private AudioSource soundEffectSource;
    [SerializeField] private AudioSource GameOverSource;

    [SerializeField] private AudioClip shootClip;
    [SerializeField] private AudioClip reloadClip;
    [SerializeField] private AudioClip buffClip;
    [SerializeField] private AudioClip winningClip;
    [SerializeField] private AudioClip defeatClip;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayShootSound()
    {
        if (shootClip != null)
        {
            soundEffectSource.PlayOneShot(shootClip);
        }
    }
    public void PlayReloadSound()
    {
        if (reloadClip != null)
        {
            soundEffectSource.PlayOneShot(reloadClip);
        }
    }
    public void PlayBuffSound()
    {
        if (buffClip != null)
        {
            soundEffectSource.PlayOneShot(buffClip);
        }
    }
    public void PlayWinningSound()
    {
        if (winningClip != null)
        {
            soundEffectSource.PlayOneShot(winningClip);
        }
    }
    public void PlayDefeatSound()
    {
        if (defeatClip != null)
        {
            GameOverSource.PlayOneShot(defeatClip);
        }
    }
    public void PlayBossMusic()
    {
        GameOverSource.Stop();
        defaultMusicSource.Stop();
        if (BossMusicSource != null && !BossMusicSource.isPlaying)
        {
            BossMusicSource.Play();
        }
    }
    public void PlayDefaultMusic()
    {
        GameOverSource.Stop();
        if (BossMusicSource != null && BossMusicSource.isPlaying)
        {
            BossMusicSource.Stop();
        }
        if (defaultMusicSource != null && !defaultMusicSource.isPlaying)
            defaultMusicSource.Play();
    }
    public void PlayGameOverMusic()
    {
        defaultMusicSource.Stop();
        BossMusicSource.Stop();
        if (GameOverSource != null && !GameOverSource.isPlaying)
        {
            GameOverSource.Play();
        }
    }
    public void StopMusic()
    {
        soundEffectSource.Stop();
        defaultMusicSource.Stop();
        BossMusicSource.Stop();
        GameOverSource.Stop();
    }
}
