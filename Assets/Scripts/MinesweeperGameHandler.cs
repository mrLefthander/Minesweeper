using System;
using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine;

public class MinesweeperGameHandler: MonoBehaviour
{
    [SerializeField] private GridPrefabVisual gridPrefabVisual;
    [SerializeField] private UIHandler uiHandler;
    [SerializeField] private TimerHandler timer;
    [SerializeField] private FlagCountHandler flagCountHandler;

    private Map map;
    private bool isGameActive;
    private bool isPaused;
    private IInputHandler inputHandler;

    void Start()
    {
        Vector2Int mapDimensions = GameValuesController.instance.GetMapDimensions();
        int minesToPlace = GameValuesController.instance.GetMinesToPlaceCount(mapDimensions);

        map = new Map(mapDimensions, minesToPlace);
        gridPrefabVisual.Setup(map.GetGrid());
        isGameActive = true;
        isPaused = false;
        flagCountHandler.Setup(map);

        CreateInputHandler();

        map.OnEntireMapRevealed += Map_OnEntireMapRevealed;
    }

    private void Map_OnEntireMapRevealed(object sender, EventArgs e)
    {
        isGameActive = false;

        StartCoroutine(uiHandler.WinCoroutine(timer.GetScore()));
    }

    void Update()
    {
        if (isGameActive)
        {
            if (!isPaused)
            {
                timer.HandleTimer();

                HandleStandaloneInput();
                inputHandler.HandleInput();
            }

            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Pause))
            {
                ChangePauseState();
            }
        }

    }

    private void CreateInputHandler()
    {
        HandleAndroidInput();
        HandleStandaloneInput();
    }

    [Conditional("UNITY_ANDROID")]
    private void HandleAndroidInput()
    {
        inputHandler = new TouchInputHandler(RevealAtPosition, FlagAtPosition, true, gridPrefabVisual.SetRevealMap);
    }

    [Conditional("UNITY_STANDALONE"), Conditional("UNITY_WEBGL")]
    private void HandleStandaloneInput()
    {
        inputHandler = new MouseAndKeyboardInputHandler(RevealAtPosition, FlagAtPosition, true, gridPrefabVisual.SetRevealMap);
    }

    private void FlagAtPosition(Vector2 worldPosition)
    {
        map.ChangeFlaggedStateOnGridPosition(worldPosition);
        flagCountHandler.UpdateFlagCount();
    }

    private void RevealAtPosition(Vector2 worldPosition)
    {
        MapGridObject.Type gridObjectType = map.RevealGridPosition(worldPosition);
        if (gridObjectType == MapGridObject.Type.Mine)
        {
            isGameActive = false;
            StartCoroutine(map.RevealEntireMap(2f));
            StartCoroutine(uiHandler.LoseCoroutine(2f));
        }
        flagCountHandler.UpdateFlagCount();
    }

    public void ChangePauseState()
    {
        AudioManager.instance.PlaySound(Sound.Type.ButtonClick);
        isPaused = !isPaused;
        if (uiHandler == null) UnityEngine.Debug.Log("null");
        uiHandler.ShowPauseWindow(isPaused);
    }
}
