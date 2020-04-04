using System;
using UnityEngine;

public class SettingsPlayerPrefsManager
{
    const string DIFFICULTY_PLAYERPREFS_KEY = "difficulty";
    const string MAP_SIZE_PLAYERPREFS_KEY = "map size";
    const string VOLUME_PLAYERPREFS_KEY = "volume";
    const string FULLSCREEN_PLAYERPREFS_KEY = "fullscreen";
    const string RESOLUTION_INDEX_PLAYERPREFS_KEY = "resolution index";
    const string LANGUAGE_PLAYERPREFS_KEY = "localization";

    const float MIN_VOLUME = 0.0001f;
    const float MAX_VOLUME = 1f;

    public static void SaveDifficulty(GameValuesController.Difficulty difficulty)
    {
        PlayerPrefs.SetString(DIFFICULTY_PLAYERPREFS_KEY, difficulty.ToString());
    }

    public static GameValuesController.Difficulty GetSavedDifficulty()
    {
        string difficultyString = PlayerPrefs.GetString(DIFFICULTY_PLAYERPREFS_KEY, GameValuesController.Difficulty.Easy.ToString());
        return (GameValuesController.Difficulty)Enum.Parse(typeof(GameValuesController.Difficulty), difficultyString);
    }

    public static void SaveMapSize(GameValuesController.MapSize mapSize)
    {
        PlayerPrefs.SetString(MAP_SIZE_PLAYERPREFS_KEY, mapSize.ToString());
    }

    public static GameValuesController.MapSize GetSavedMapSize()
    {
        string mapSizeString = PlayerPrefs.GetString(MAP_SIZE_PLAYERPREFS_KEY, GameValuesController.MapSize.Small.ToString());
        return (GameValuesController.MapSize)Enum.Parse(typeof(GameValuesController.MapSize), mapSizeString);
    }

    public static float GetSavedVolume()
    {
        return PlayerPrefs.GetFloat(VOLUME_PLAYERPREFS_KEY, 1f);
    }

    public static void SaveVolume(float volume)
    {
        PlayerPrefs.SetFloat(VOLUME_PLAYERPREFS_KEY, Mathf.Clamp(volume, MIN_VOLUME, MAX_VOLUME));
    }

    public static void SaveResolutionSettings(int resolutionIndex, int fullscreenIndicator)
    {
        PlayerPrefs.SetInt(FULLSCREEN_PLAYERPREFS_KEY, fullscreenIndicator);
        PlayerPrefs.SetInt(RESOLUTION_INDEX_PLAYERPREFS_KEY, resolutionIndex);
    }

    public static void GetSavedResolutionSettings(out int resolutionIndex, out int fullscreenIndicator)
    {
        fullscreenIndicator = PlayerPrefs.GetInt(FULLSCREEN_PLAYERPREFS_KEY);
        resolutionIndex = PlayerPrefs.GetInt(RESOLUTION_INDEX_PLAYERPREFS_KEY);
    }

    public static LocalizationManager.Language GetSavedLanguage()
    {
        string languageString;
        if (PlayerPrefs.HasKey(LANGUAGE_PLAYERPREFS_KEY)) 
        {
            languageString = PlayerPrefs.GetString(LANGUAGE_PLAYERPREFS_KEY, null);
            return (LocalizationManager.Language)Enum.Parse(typeof(LocalizationManager.Language), languageString);
        }
        else
        {
            switch (Application.systemLanguage)
            {
                default:
                case SystemLanguage.English:
                    return LocalizationManager.Language.English;
                case SystemLanguage.Russian:
                    return LocalizationManager.Language.Russian;
                case SystemLanguage.Ukrainian:
                    return LocalizationManager.Language.Ukrainian;
            }
        }
    }

    public static void SaveLanguage(LocalizationManager.Language language)
    {
        PlayerPrefs.SetString(LANGUAGE_PLAYERPREFS_KEY, language.ToString());
    }
}
