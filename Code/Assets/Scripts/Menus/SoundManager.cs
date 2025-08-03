using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour {
    public const string SFX_VOLUME_PREF = "BGM";
    public const string MUSIC_VOLUME_PREF = "SFX";
    public const float DEFAULT_VOLUME = 80f;
    private const float VOLUME_DECIBEL_SCALE_FACTOR = 20f;

    [SerializeField]
    AudioMixer audioMixer;

    [SerializeField]
    public Slider sfxSlider;
    [SerializeField]
    public Slider musicSlider;
    [SerializeField]
    public float sfxVolume;
    [SerializeField]
    public float musicVolume;

    private void Start() {
        sfxVolume = PlayerPrefs.GetFloat(SFX_VOLUME_PREF, DEFAULT_VOLUME);
        sfxSlider.value = sfxVolume;
        audioMixer.SetFloat("SFX", Mathf.Log10(sfxVolume / 100) * VOLUME_DECIBEL_SCALE_FACTOR);

        musicVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME_PREF, DEFAULT_VOLUME);
        musicSlider.value = musicVolume;
        audioMixer.SetFloat("BGM", Mathf.Log10(musicVolume / 100) * VOLUME_DECIBEL_SCALE_FACTOR);

    }

    public void SetSFXVolume(float volume) { 
        sfxVolume = volume;
        PlayerPrefs.SetFloat(SFX_VOLUME_PREF, sfxVolume);
        if (sfxVolume == 0) {
            audioMixer.SetFloat("SFX", -80f);
        }
        else {
            audioMixer.SetFloat("SFX", Mathf.Log10(sfxVolume / 100) * VOLUME_DECIBEL_SCALE_FACTOR);
        }
    }

    public void SetMusicVolume(float volume) {
        musicVolume = volume;
        PlayerPrefs.SetFloat(MUSIC_VOLUME_PREF, musicVolume);
        if (musicVolume == 0) {
            audioMixer.SetFloat("BGM", -80f);
        }
        else {
            audioMixer.SetFloat("BGM", Mathf.Log10(musicVolume / 100) * VOLUME_DECIBEL_SCALE_FACTOR);
        }
    }
}
