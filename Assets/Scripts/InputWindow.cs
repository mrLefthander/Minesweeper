using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Text.RegularExpressions;

public class InputWindow: MonoBehaviour
{
    private TMP_InputField inputField;
    private Button okButton;
    private Button cancelButton;
    private TMP_Text highscoreText;
    private int score;
    private HighscoreHandler highscoreHandler;

    private void Awake()
    {
        okButton = transform.Find("okButton").GetComponent<Button>();
        cancelButton = transform.Find("cancelButton").GetComponent<Button>();
        inputField = transform.Find("inputField").GetComponent<TMP_InputField>();
        highscoreText = transform.Find("highscoreText").GetComponent<TMP_Text>();
        highscoreHandler = FindObjectOfType<HighscoreHandler>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
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
        return '\0';
    }

    public void onOk(int score, string name)
    {
        highscoreHandler.AddHighscoreEntry(score, name);
        gameObject.SetActive(false);
    }

    public void onCancel()
    {
        gameObject.SetActive(false);
    }


}
