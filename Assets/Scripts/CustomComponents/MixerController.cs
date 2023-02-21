using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixerController : MonoBehaviour
{
    [SerializeField] private AudioMixer myAudioMixer;
    [SerializeField] private Slider VolumeSlider = null;

    public void Start()
    {
        LoadVolume();
    }

    public void SetVolume(float volume)
    {
        myAudioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
        StaticItems.Volume = volume;
    }

    void LoadVolume()
    {
        float VolumeValue = StaticItems.Volume;
        VolumeSlider.value = VolumeValue;
        AudioListener.volume = VolumeValue;
    }
}
