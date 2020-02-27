using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocalizedText : MonoBehaviour
{
    public string key;
    TextMeshPro text;

    void Start()
    {
        text = GetComponent<TextMeshPro>();
        text.text = LocalizationManager.instance.GetLocalizedValue(key);
    }

    private void Update()
    {
        text.text = LocalizationManager.instance.GetLocalizedValue(key);
    }
}
