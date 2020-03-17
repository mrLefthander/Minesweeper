using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecolutionSelector : MonoBehaviour
{
    private List<Resolution> resolutionsList;
    private TMP_Dropdown resolutionDropdown;
    private Toggle fullscreenToggle;

    private bool isPlayingSound = false;


    void Start()
    {
        fullscreenToggle = GetComponentInChildren<Toggle>();
        resolutionDropdown = GetComponentInChildren<TMP_Dropdown>();
        StartResolutionSelector();
        DontStartResolutionSelector();
    }

    [Conditional("UNITY_WEBGL"), Conditional("UNITY_ANDROID")]
    private void DontStartResolutionSelector()
    {
        fullscreenToggle.gameObject.SetActive(false);
        resolutionDropdown.gameObject.SetActive(false);
        GetComponentInChildren<TMP_Text>().gameObject.SetActive(false);
    }

    [Conditional("UNITY_EDITOR"), Conditional("UNITY_STANDALONE")]
    private void StartResolutionSelector()
    {
        InitializeResolutionDropdown();
        SetUpInitialValues();
        isPlayingSound = true;
    }

    private void SetUpInitialValues()
    {
        SettingsPlayerPrefsManager.GetSavedResolutionSettings(out int resolutionIndex, out int fullscreenIndicator);
        resolutionDropdown.value = resolutionIndex;
        fullscreenToggle.isOn = (fullscreenIndicator < 1) ? false : true;
    }

    private void InitializeResolutionDropdown()
    {
        resolutionsList = Screen.resolutions.Where(r => r.width >= 640)
            .Select(r => new Resolution { width = r.width, height = r.height })
                .Distinct().Reverse().ToList(); 

        resolutionDropdown.ClearOptions();

        List<string> resolutionStrings = resolutionsList.Select(r => r.width + " x " + r.height).ToList();

        resolutionDropdown.AddOptions(resolutionStrings);
        
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutionsList[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        if (isPlayingSound)
        {
            PlayClickSound();
        }
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        if (isPlayingSound)
        {
            PlayClickSound();
        }
    }

    public void SaveCurrentResolutionSettings()
    {
        int fullscreenIndicator = Screen.fullScreen ? 1 : 0;
        SettingsPlayerPrefsManager.SaveResolutionSettings(resolutionDropdown.value, fullscreenIndicator);
    }

    public void PlayClickSound()
    {
        AudioManager.instance.PlaySound(Sound.Type.ToggleClick);
    }
}
