using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LocalizationSelectorToggle : MonoBehaviour, ISelectHandler
{
    [SerializeField]
    private LocalizationManager.Language language;

    void Start()
    {
        if(language == LocalizationManager.instance.CurrentLanguage)
        {
            GetComponent<Toggle>().isOn = true;
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        GetComponent<Toggle>().isOn = true;
        LocalizationManager.instance.CurrentLanguage = language;
        AudioManager.instance.PlaySound(Sound.Type.ToggleClick);
        SettingsPlayerPrefsManager.SaveLanguage(language);
    }
}
