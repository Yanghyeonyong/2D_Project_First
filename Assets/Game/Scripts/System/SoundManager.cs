using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource bgmAudio;
    [SerializeField] private AudioSource effectAudio;

    public void PlayBGM(AudioClip audio)
    {
        bgmAudio.clip= audio;
        bgmAudio.Play();
    }
    public void PlayEffect(AudioClip audio)
    {
        if (effectAudio.clip == null)
        {
            effectAudio.clip = audio;
            effectAudio.Play();
        }
        else
        {
            effectAudio.PlayOneShot(audio);
        }
    }

}
