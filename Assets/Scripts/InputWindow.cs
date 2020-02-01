using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Text.RegularExpressions;

public class InputWindow : MonoBehaviour
{
    [SerializeField] private HighscoreTable highscoreTable;
    private TMP_InputField inputField;
    private Button okButton;
    private Button cancelButton;
    private TMP_Text highscoreText;
    private int score;

    private void Awake()
    {
        okButton = transform.Find("okButton").GetComponent<Button>();
        cancelButton = transform.Find("cancelButton").GetComponent<Button>();
        inputField = transform.Find("inputField").GetComponent<TMP_InputField>();
        highscoreText = transform.Find("highscoreText").GetComponent<TMP_Text>();
       // Hide();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            onOk(score, inputField.text);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            onCancel();
        }
    }

    public void Show(int score)
    {
        this.score = score;
        gameObject.SetActive(true);
        inputField.characterLimit = 3;
        inputField.onValidateInput = ValidateInput;
        highscoreText.text = score.ToString();        
        okButton.onClick.AddListener(() => onOk(score, inputField.text));
        cancelButton.onClick.AddListener(onCancel);
        transform.SetAsLastSibling();
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

    public void onOk(int score, string name)
    {
        Hide();
        highscoreTable.AddHighscoreEntry(score, name);
    }

    public void onCancel()
    {
        Hide();
    }

  
}
