using UnityEngine;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetVolume(float volNum)
    {
        audioMixer.SetFloat("Music", volNum);
    }
}
