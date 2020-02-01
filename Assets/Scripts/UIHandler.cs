using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    private GameObject gameOverWindow;
    private GameObject gameWinWindow;
    private GameObject blocker;
    private InputWindow inputWindow;
    private HighscoreTable highscoreTable;

    private void Awake()
    {
        gameOverWindow = transform.Find("Canvas/GameOverWindow").gameObject;
        gameWinWindow = transform.Find("Canvas/GameWinWindow").gameObject;
        blocker = transform.Find("Canvas/Blocker").gameObject;
        inputWindow = GetComponentInChildren<InputWindow>(true);
        highscoreTable = GetComponentInChildren<HighscoreTable>(true);

        HideAll();
    }

    private void HideAll()
    {
        gameOverWindow.SetActive(false);
        gameWinWindow.SetActive(false);
        blocker.SetActive(false);
        inputWindow.Hide();
        highscoreTable.gameObject.SetActive(false);
    }

    public IEnumerator WinCoroutine(int score)
    {
        yield return new WaitForSeconds(0.8f);
        blocker.SetActive(true);
        if (highscoreTable.IsHighscore(score))
        {
            inputWindow.Show(score);
        }
        gameWinWindow.SetActive(true);
    }

    public IEnumerator LoseCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        blocker.SetActive(true);
        gameOverWindow.SetActive(true);
    }
}
