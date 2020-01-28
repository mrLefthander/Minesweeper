using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Text.RegularExpressions;

public class InputWindow : MonoBehaviour
{
    private TMP_InputField inputField;
    private Button okButton;
    private Button cancelButton;

    private void Awake()
    {
        okButton = transform.Find("okButton").GetComponent<Button>();
        cancelButton = transform.Find("cancelButton").GetComponent<Button>();
        inputField = transform.Find("inputField").GetComponent<TMP_InputField>();
        Hide();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            onOk();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            onCancel();
        }
    }

    public void Show()
    {
        inputField.characterLimit = 3;
        inputField.onValidateInput = ValidateInput;
        gameObject.SetActive(true);
        okButton.onClick.AddListener(onOk);
        cancelButton.onClick.AddListener(onCancel);
    }

    private char ValidateInput(string text, int charIndex, char addedChar)
    {
        if (Char.IsLetter(addedChar))
        {
            return addedChar;
        }
        else
        {
            return '\0';
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void onOk()
    {
        Hide();
    }

    public void onCancel()
    {
        Hide();
    }

  
}
