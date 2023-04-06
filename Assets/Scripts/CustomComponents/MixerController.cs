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
        LoadMasterVolume();
        LoadMusicVolume();
        LoadSFXVolume();
    }

    public void SetMasterVolume(float volume)
    {
        myAudioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        StaticItems.MasterVolume = volume;
    }
    public void SetMusicVolume(float volume)
    {
        myAudioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        StaticItems.MusicVolume = volume;
    }
    public void SetSFXVolume(float volume)
    {
        myAudioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        StaticItems.SFXVolume = volume;
    }

    void LoadMasterVolume()
    {
        float VolumeValue = StaticItems.MasterVolume;
        VolumeSlider.value = VolumeValue;
        AudioListener.volume = VolumeValue;
    }
    void LoadMusicVolume()
    {
        float VolumeValue = StaticItems.MusicVolume;
        VolumeSlider.value = VolumeValue;
        AudioListener.volume = VolumeValue;
    }
    void LoadSFXVolume()
    {
        float VolumeValue = StaticItems.SFXVolume;
        VolumeSlider.value = VolumeValue;
        AudioListener.volume = VolumeValue;
    }
}
