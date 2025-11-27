using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource bgmAudio;
    [SerializeField] private AudioSource effectAudio;

    //BGM 실행
    public void PlayBGM(AudioClip audio)
    {
        bgmAudio.clip= audio;
        bgmAudio.Play();
    }
    //효과음은 한 번에 여러개 들릴 수 있으니 PlayOneShot 활용
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
