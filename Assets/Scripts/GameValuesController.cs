using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameValuesController : MonoBehaviour
{
    public enum MapSize
    {
        Small,
        Medium,
        Large
    }

    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }

    public static GameValuesController instance;

    public GameValuesController.Difficulty difficulty;
    public GameValuesController.MapSize mapSize;

    private void Awake()
    {
        SetUpSingleton();
        difficulty = SettingsPlayerPrefsManager.GetSavedDifficulty();
        mapSize = SettingsPlayerPrefsManager.GetSavedMapSize();
    }

    private void SetUpSingleton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    
    public int GetMinesToPlaceCount(Vector2Int mapDimensions)
    {
        int basicMinesCount = Mathf.RoundToInt(mapDimensions.x * mapDimensions.y / 10);

        switch (difficulty)
        {
            default:
            case GameValuesController.Difficulty.Easy:
                return basicMinesCount;
            case GameValuesController.Difficulty.Medium:
                return Mathf.RoundToInt(basicMinesCount * 1.5f);
            case GameValuesController.Difficulty.Hard:
                return basicMinesCount * 2;
        }
    }

    public Vector2Int GetMapDimensions()
    {
#if UNITY_STANDALONE || UNITY_WEBGL
        switch (mapSize)
        {
            default:
            case GameValuesController.MapSize.Small:
                return new Vector2Int(10, 10);
            case GameValuesController.MapSize.Medium:
                return new Vector2Int(12, 12);
            case GameValuesController.MapSize.Large:
                return new Vector2Int(15, 15);
        }
#elif UNITY_ANDROID
        switch (mapSize)
        {
            default:
            case GameValuesController.MapSize.Small:
                return new Vector2Int(7, 10);
            case GameValuesController.MapSize.Medium:
                return new Vector2Int(10, 15);
            case GameValuesController.MapSize.Large:
                return new Vector2Int(13, 20);
        }
#endif
    }
}
