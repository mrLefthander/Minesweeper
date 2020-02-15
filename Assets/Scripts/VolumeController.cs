using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    private const string AUDIOMIXER_EXPOSED_VAR_NAME = "volume";

    [SerializeField] private AudioMixer mainMixer;

    private Slider volumeSlider;

    private void Start()
    {
        volumeSlider = GetComponentInChildren<Slider>();
        SetSliderValue();
        SetVolume(volumeSlider.value);
    }

    private void SetSliderValue()
    {
        volumeSlider.value = SettingsPlayerPrefsManager.GetSavedVolume();
    }

    public void SaveCurrentVolume()
    {
        SettingsPlayerPrefsManager.SaveVolume(volumeSlider.value);
    }

    public void SetVolume(float sliderValue)
    {
        mainMixer.SetFloat(AUDIOMIXER_EXPOSED_VAR_NAME, Mathf.Log10(sliderValue) * 20);
    }
}
