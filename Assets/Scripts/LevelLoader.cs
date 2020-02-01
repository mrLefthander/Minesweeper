using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadGameplay()
    {
        SceneManager.LoadScene("Gameplay Scene");
    }

    public void LoadOptionsMenu()
    {
        SceneManager.LoadScene("Options Screen");
    }

    public void LoadHighscoreMenu()
    {
        SceneManager.LoadScene("Highscore Screen");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
