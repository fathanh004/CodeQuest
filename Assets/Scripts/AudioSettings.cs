using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField]
    AudioMixer audioMixer;

    [SerializeField]
    Slider bgmSlider;

    [SerializeField]
    Slider sfxSlider;

    private void Awake()
    {
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void Start()
    {
        bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 0.75f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
    }

    public void SetBGMVolume(float volume)
    {
        if (volume == 0)
        {
            volume = 0.0001f;
        }
        audioMixer.SetFloat("BGMVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        if (volume == 0)
        {
            volume = 0.0001f;
        }
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }
}
