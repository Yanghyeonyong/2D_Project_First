using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    //BGM 사운드
    [SerializeField] float volumeSize = 1;
    [SerializeField] AudioSource myAudio;
    [SerializeField] string slideTag;
    Slider mySlide;

    private void Start()
    {
        myAudio = GetComponent<AudioSource>();
        myAudio.volume = volumeSize;
    }

    //슬라이더 찾아서 오디오 볼륨과 연동
    public void ChangeVolume()
    {
        if (mySlide == null)
        {
            mySlide = GameObject.FindGameObjectWithTag(slideTag).GetComponent<Slider>();
        }

        if (mySlide != null)
        {
            volumeSize = mySlide.value;
            myAudio.volume = volumeSize;
        }
        else
        {
            Debug.Log("No Audio Slide : "+slideTag);
        }
    }
}
