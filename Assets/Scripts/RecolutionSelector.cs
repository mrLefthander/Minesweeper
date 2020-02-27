using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecolutionSelector : MonoBehaviour
{
    List<Resolution> resolutionsList;
    TMP_Dropdown resolutionDropdown;
    Toggle fullscreenToggle;


    void Start()
    {
        fullscreenToggle = GetComponentInChildren<Toggle>();
        InitializeResolutionDropdown();
        SetUpInitialValues();
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

        resolutionDropdown = GetComponentInChildren<TMP_Dropdown>();
        resolutionDropdown.ClearOptions();

        List<string> resolutionStrings = resolutionsList.Select(r => r.width + " x " + r.height).ToList();

        resolutionDropdown.AddOptions(resolutionStrings);
        
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutionsList[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SaveCurrentResolutionSettings()
    {
        int fullscreenIndicator = Screen.fullScreen ? 1 : 0;
        SettingsPlayerPrefsManager.SaveResolutionSettings(resolutionDropdown.value, fullscreenIndicator);
    }
}
