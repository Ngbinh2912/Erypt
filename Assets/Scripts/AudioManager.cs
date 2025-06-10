using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource defaultMusicSource;
    [SerializeField] private AudioSource soundEffectSource;
    [SerializeField] private AudioClip shootClip;
    [SerializeField] private AudioClip reloadClip;
    [SerializeField] private AudioClip buffClip;

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
        if (defaultMusicSource != null && !defaultMusicSource.isPlaying)
            defaultMusicSource.Play();
    }
    public void StopMusic()
    {
        soundEffectSource.Stop();
        defaultMusicSource.Stop();
    }
}
