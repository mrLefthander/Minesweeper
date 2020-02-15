using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPlayerPrefsManager
{
    const string DIFFICULTY_PLAYERPREFS_KEY = "difficulty";
    const string MAP_SIZE_PLAYERPREFS_KEY = "map size";
    const string VOLUME_PLAYERPREFS_KEY = "volume";

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
}
