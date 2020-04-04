using System.Collections;
using UnityEngine;

public class UIHandler: MonoBehaviour
{
    private const string GAME_OVER_WINDOW_OBJECT_NAME = "GameOverWindow";
    private const string GAME_WIN_WINDOW_OBJECT_NAME = "GameWinWindow";
    private const string BLOCKER_OBJECT_NAME = "Blocker";
    private const string PAUSE_WINDOW_OBJECT_NAME = "PauseWindow";

    [SerializeField] private HighscoreHandler highscoreHandler;

    private GameObject gameOverWindow;
    private GameObject gameWinWindow;
    private GameObject blocker;
    private GameObject pauseWindow;
    private InputWindow inputWindow;
    private Canvas canvas;

    private void Awake()
    {
        canvas = GetComponentInChildren<Canvas>();

        gameOverWindow = canvas.transform.Find(GAME_OVER_WINDOW_OBJECT_NAME).gameObject;
        gameWinWindow = canvas.transform.Find(GAME_WIN_WINDOW_OBJECT_NAME).gameObject;
        blocker = canvas.transform.Find(BLOCKER_OBJECT_NAME).gameObject;
        pauseWindow = canvas.transform.Find(PAUSE_WINDOW_OBJECT_NAME).gameObject;

        inputWindow = canvas.GetComponentInChildren<InputWindow>();

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

    public void ShowLoseWindow()
    {
        blocker.SetActive(true);
        gameOverWindow.SetActive(true);
    }

    public void ShowPauseWindow(bool show)
    {
        blocker.SetActive(show);
        pauseWindow.SetActive(show);
    }
}
