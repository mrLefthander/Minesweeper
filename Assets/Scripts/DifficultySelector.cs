using System;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySelector : MonoBehaviour
{
    Toggle[] toggles;

    void Awake()
    {
        toggles = GetComponentsInChildren<Toggle>();
        SetSavedDifficulty();
    }

    public void SaveCurrentDifficulty()
    {
        int index = Array.FindIndex(toggles, t => t.isOn);
        GameValuesController.instance.difficulty = (GameValuesController.Difficulty)index;
        SettingsPlayerPrefsManager.SaveDifficulty(GameValuesController.instance.difficulty);
    }

    private void SetSavedDifficulty()
    {
        toggles[(int)GameValuesController.instance.difficulty].isOn = true;
    }

    public void PlayToggleClickSound()
    {
        AudioManager.instance.PlaySound(Sound.Type.ToggleClick);
    }
}
