using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void GetGameValues(out Vector2Int mapDimesions, out int minesToPlace)
    {
        mapDimesions = GetMapDimensions();
        minesToPlace = GetMinesToPlaceCount(mapDimesions);
    }
    
    private int GetMinesToPlaceCount(Vector2Int mapDimensions)
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

    private Vector2Int GetMapDimensions()
    {
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
    }
}
