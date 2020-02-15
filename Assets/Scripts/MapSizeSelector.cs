using System;
using UnityEngine;
using UnityEngine.UI;

public class MapSizeSelector: MonoBehaviour
{
    Toggle[] toggles;

    void Awake()
    {
        toggles = GetComponentsInChildren<Toggle>();
        SetSavedMapSize();
    }

    public void SaveCurrentMapSize()
    {
        int index = Array.FindIndex(toggles, t => t.isOn);
        GameValuesController.instance.mapSize = (GameValuesController.MapSize)index;
        SettingsPlayerPrefsManager.SaveMapSize(GameValuesController.instance.mapSize);
    }

    private void SetSavedMapSize()
    {
        toggles[(int)GameValuesController.instance.mapSize].isOn = true;
    }

    public void PlayToggleClickSound()
    {
       AudioManager.instance.PlaySound(Sound.Type.ToggleClick);
    }
}

