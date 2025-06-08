using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource defaultMusic;
    [SerializeField] private AudioSource bossMusic;
    [SerializeField] private AudioSource soundEffectSource;
    [SerializeField] private AudioClip shootClip;
    [SerializeField] private AudioClip reloadClip;
    [SerializeField] private AudioClip buffClip;

    public static AudioManager Instance { get; private set; }

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
    public void PlayDefaultMusic()
    {
        bossMusic.Stop();
        defaultMusic.Play();
    }
    public void PlayBossMusic()
    {
        defaultMusic.Stop();
        bossMusic.Play();
    }
    public void StopMusic()
    {
        soundEffectSource.Stop();
        defaultMusic.Stop();
        bossMusic.Stop();
    }
}
