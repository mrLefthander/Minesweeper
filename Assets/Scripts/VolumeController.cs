using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics;
using System;

public class VolumeController : MonoBehaviour
{
    private Slider volumeSlider;
    private TMP_Text volumeText;

    private void Start()
    {
        volumeSlider = GetComponentInChildren<Slider>();
        volumeText = volumeSlider.GetComponentInChildren<TMP_Text>();

        volumeSlider.onValueChanged.AddListener(OnSliderValueChange);

        HideVolumeTextOnAndroid();

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

    public void SaveCurrentVolume()
    {
        GameValuesController.instance.volume = volumeSlider.value;
        SettingsPlayerPrefsManager.SaveVolume(volumeSlider.value);
    }

    public void PlayPointerUpSound()
    {
        AudioManager.instance.PlaySound(Sound.Type.ToggleClick);
    }

    [Conditional("UNITY_ANDROID")]
    private void HideVolumeTextOnAndroid()
    {
        volumeText.gameObject.SetActive(false);
    }

}
