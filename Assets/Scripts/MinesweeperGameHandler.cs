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
 //   [SerializeField] private CameraControlsAndroid cameraControls;

    private Map map;
    private bool isGameActive;
    private bool isPaused;
    private IInputHandler inputHandler;

    //float touchTime = 0f;
    //bool newTouch = false;
    //Vector2 touchZeroStartWorldPosition;
    //float touchDeltaPositionThreshold = 10f;

    void Start()
    {
        Vector2Int mapDimensions = GameValuesController.instance.GetMapDimensions();
        int minesToPlace = GameValuesController.instance.GetMinesToPlaceCount(mapDimensions);

        map = new Map(mapDimensions, minesToPlace);
        gridPrefabVisual.Setup(map.GetGrid());
        isGameActive = true;
        isPaused = false;
        flagCountHandler.Setup(map);

        SetInputHandler();
        HandleAndroidInput();

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
                inputHandler.HandleInput(RevealAtPosition, FlagAtPosition, true, gridPrefabVisual.SetRevealMap);
            }

            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Pause))
            {
                ChangePauseState();
            }
        }

    }

    private void SetInputHandler()
    {
        HandleAndroidInput();
        HandleStandaloneInput();

    }

    [Conditional("UNITY_ANDROID")]
    private void HandleAndroidInput()
    {
        inputHandler = gameObject.AddComponent<TouchInputHandler>();
    }

    [Conditional("UNITY_STANDALONE"), Conditional("UNITY_WEBGL")]
    private void HandleStandaloneInput()
    {
        inputHandler = gameObject.AddComponent<TouchInputHandler>();
    }

    [Conditional("UNITY_STANDALONE"), Conditional("UNITY_WEBGL")]
    private void EnableStandaloneDebug(bool isEnabled)
    {
        if (isEnabled)
        {
            if (Input.GetKeyDown(KeyCode.D))
                gridPrefabVisual.SetRevealMap(true);
            
            if (Input.GetKeyUp(KeyCode.D)) 
                gridPrefabVisual.SetRevealMap(false);
        }
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

    private Vector3 GetInputWorldPosition(Vector3 position)
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
        worldPosition.z = 0f;
        return worldPosition;
    }

    private Vector3 GetMouseWorldPosition()
    {
        return GetInputWorldPosition(Input.mousePosition);
    }

    public void ChangePauseState()
    {
        AudioManager.instance.PlaySound(Sound.Type.ButtonClick);
        isPaused = !isPaused;
        if (uiHandler == null) UnityEngine.Debug.Log("null");
        uiHandler.ShowPauseWindow(isPaused);
    }
}
