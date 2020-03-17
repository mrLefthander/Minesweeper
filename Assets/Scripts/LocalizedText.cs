using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class LocalizedText: MonoBehaviour
{
    public string key;
    TMP_Text titleText;
    bool updateText;
    void Start()
    {
        titleText = GetComponent<TMP_Text>();
        LocalizeText();
        LocalizationManager.instance.OnDataLoaded += UpdateText;
    }

    private void Update()
    {
        if (updateText)
        {
            LocalizeText();
            updateText = false;

        }
    }

    private void UpdateText()
    {
        updateText = true;
    }

    private void LocalizeText()
    {
        titleText.text = LocalizationManager.instance.GetLocalizedValue(key);
    }

    private void OnDestroy()
    {
        LocalizationManager.instance.OnDataLoaded -= UpdateText;
    }
}
