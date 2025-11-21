using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    //BGM »ç¿îµå
    [SerializeField] float volumeSize = 1;
    [SerializeField] AudioSource myAudio;
    [SerializeField] string slideTag;
    Slider mySlide;

    private void Start()
    {
        myAudio = GetComponent<AudioSource>();
        myAudio.volume = volumeSize;
    }

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
