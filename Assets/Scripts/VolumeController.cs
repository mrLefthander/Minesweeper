using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics;
using System;

public class VolumeController : MonoBehaviour
{
#if UNITY_STANDALONE || UNITY_WEBGL
    private Slider volumeSlider;
    private TMP_Text volumeText;
#elif UNITY_ANDROID
    private Toggle muteToggle;
#endif

    private void Start()
    {
        InitializeControls();
    }

    private void InitializeControls()
    {
#if UNITY_STANDALONE || UNITY_WEBGL
        LoadStandaloneControls();
#elif UNITY_ANDROID
        LoadAndroidControls();
#endif
    }

    public void SaveCurrentVolume()
    {
        SettingsPlayerPrefsManager.SaveVolume(AudioManager.instance.volume);
    }

    public void PlayPointerUpSound()
    {
        AudioManager.instance.PlaySound(Sound.Type.ToggleClick);
    }


#if UNITY_STANDALONE || UNITY_WEBGL
    private void LoadStandaloneControls()
    {
        volumeSlider = GetComponentInChildren<Slider>();
        volumeText = volumeSlider.GetComponentInChildren<TMP_Text>();
        volumeSlider.onValueChanged.AddListener(OnSliderValueChange);
        SetSliderValue();
        SetVolumeTextValue();
    }
    
    private void SetVolumeTextValue()
    {
        float scaledVolumeValue = volumeSlider.value * 100;
        volumeText.text = ((int)scaledVolumeValue).ToString();
    }

    private void SetSliderValue()
    {
        volumeSlider.value = GameValuesController.instance.volume;
    }

    public void OnSliderValueChange(float volume)
    {
        GameValuesController.instance.SetVolume(volume);
        SetVolumeTextValue();
    }

#elif UNITY_ANDROID
    private void LoadAndroidControls()
    {
        muteToggle = GetComponentInChildren<Toggle>();
        muteToggle.onValueChanged.AddListener(OnMuteToggleChange);
        SetToggleValue();
    }

    private void SetToggleValue()
    {
        muteToggle.isOn = AudioManager.instance.volume != 0.0001f ? false : true;
    }

    public void OnMuteToggleChange(bool isOn)
    {
        if (isOn)
        {
            AudioManager.instance.SetVolume(0.0001f);
        }
        else
        {
            AudioManager.instance.SetVolume(1f);
        }
    }
#endif

}
