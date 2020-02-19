using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RecolutionSelector : MonoBehaviour
{
    List<Resolution> resolutionsList;
    TMP_Dropdown resolutionDropdown;

    void Start()
    {
        InitializeResolutionDropdown();
    }

    private void InitializeResolutionDropdown()
    {
        resolutionsList = Screen.resolutions.Where(r => r.width >= 640)
            .Select(r => new Resolution { width = r.width, height = r.height })
                .Distinct().Reverse().ToList(); 

        resolutionDropdown = GetComponentInChildren<TMP_Dropdown>();
        resolutionDropdown.ClearOptions();

        List<string> resolutionStrings = resolutionsList.Select(r => r.width + " x " + r.height).ToList();
        //int currentResolutionIndex = 0;
        //for (int i = 0; i < resolutionsList.Count; i++)
        //{
        //    resolutionStrings.Add(resolutionsList[i].width + " x " + resolutionsList[i].height);
        //    if (resolutionsList[i].width == Screen.currentResolution.width && resolutionsList[i].height == Screen.currentResolution.height)
        //    {
        //        currentResolutionIndex = i;
        //    }
        //}

        resolutionDropdown.AddOptions(resolutionStrings);
        resolutionDropdown.value = resolutionsList.IndexOf(Screen.currentResolution);
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
       // GameValuesController.instance.difficulty = (GameValuesController.Difficulty);
        SettingsPlayerPrefsManager.SaveResolutionSettings(resolutionDropdown.value);
    }
}
