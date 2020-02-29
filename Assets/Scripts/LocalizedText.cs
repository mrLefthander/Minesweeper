using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocalizedText : MonoBehaviour
{
    public string key;
    TMP_Text titleText;
    void Start()
    {
        titleText = GetComponent<TMP_Text>();
        titleText.text = LocalizationManager.instance.GetLocalizedValue(key);
    }
}
