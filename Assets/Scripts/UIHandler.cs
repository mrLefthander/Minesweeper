using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    private GameObject gameOverWindow;
    private GameObject gameWinWindow;
    private GameObject blocker;
    private GameObject pauseWindow;
    private InputWindow inputWindow;
    private HighscoreHandler highscoreHandler;

    private void Awake()
    {
        gameOverWindow = transform.Find("Canvas/GameOverWindow").gameObject;
        gameWinWindow = transform.Find("Canvas/GameWinWindow").gameObject;
        blocker = transform.Find("Canvas/Blocker").gameObject;
        pauseWindow = transform.Find("Canvas/PauseWindow").gameObject;
        inputWindow = GetComponentInChildren<InputWindow>(true);
        highscoreHandler = FindObjectOfType<HighscoreHandler>();

        HideAll();
    }

    private void HideAll()
    {
        gameOverWindow.SetActive(false);
        gameWinWindow.SetActive(false);
        blocker.SetActive(false);
        pauseWindow.SetActive(false);
        inputWindow.gameObject.SetActive(false);
    }

    public IEnumerator WinCoroutine(int score)
    {
        AudioManager.instance.PlaySound(Sound.Type.GameWin);
        yield return new WaitForSecondsRealtime(0.8f);
        blocker.SetActive(true);
        if (highscoreHandler.IsHighscore(score))
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

    public void ShowPauseWindow(bool show)
    {
        blocker.SetActive(show);
        pauseWindow.SetActive(show);
    }
}
