using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private void PlayClickSound()
    {
        AudioManager.instance.PlaySound(Sound.Type.ButtonClick);
    }

    public void LoadMainMenu()
    {
        PlayClickSound();
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadGameplay()
    {
        PlayClickSound();
        SceneManager.LoadScene("Gameplay Scene");
    }

    public void LoadSettingsMenu()
    {
        PlayClickSound();
        SceneManager.LoadScene("Settings Screen");
    }

    public void LoadHighscoreMenu()
    {
        PlayClickSound();
        SceneManager.LoadScene("Highscores Screen");
    }

    public void QuitGame()
    {
        PlayClickSound();
        Application.Quit();
    }
}
