using System;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;

public class DifficultySelector : MonoBehaviour
{
    Toggle[] toggles;

    void Start()
    {
        //SetHorizontalLayout();
      //  SetVerticalLayout();


        toggles = GetComponentsInChildren<Toggle>(true);
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

    //[Conditional("UNITY_ANDROID")]
    //private void SetVerticalLayout()
    //{
    //    VerticalLayoutGroup layout = gameObject.AddComponent<VerticalLayoutGroup>();
    //    layout.spacing = 15;
    //    layout.childControlHeight = false;
    //    layout.childControlWidth = false;
    //    GetComponent<RectTransform>().anchoredPosition = new Vector2(-125, 250);
    //}

    //[Conditional("UNITY_STANDALONE"), Conditional("UNITY_WEBGL")]
    //private void SetHorizontalLayout()
    //{
    //    HorizontalLayoutGroup layout = gameObject.AddComponent<HorizontalLayoutGroup>();
    //    layout.spacing = 25;
    //}




}
