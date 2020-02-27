using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LocalizationSelectorToggle : MonoBehaviour, ISelectHandler
{
    [SerializeField]
    private LocalizationManager.Language language;
    public void OnSelect(BaseEventData eventData)
    {
        GetComponent<Toggle>().isOn = true;
        LocalizationManager.instance.LoadLocalizedText(language);
    }
}
