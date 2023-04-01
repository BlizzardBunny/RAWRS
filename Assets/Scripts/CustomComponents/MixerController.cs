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

    public void SetMasterVolume(float volume)
    {
        myAudioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        StaticItems.Volume = volume;
    }
    public void SetMusicVolume(float volume)
    {
        myAudioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        StaticItems.Volume = volume;
    }
    public void SetSFXVolume(float volume)
    {
        myAudioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        StaticItems.Volume = volume;
    }

    void LoadVolume()
    {
        float VolumeValue = StaticItems.Volume;
        VolumeSlider.value = VolumeValue;
        AudioListener.volume = VolumeValue;
    }
}
