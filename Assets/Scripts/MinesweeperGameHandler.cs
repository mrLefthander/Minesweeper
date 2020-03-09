using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinesweeperGameHandler : MonoBehaviour
{
    [SerializeField] private GridPrefabVisual gridPrefabVisual;
    private Map map;
    private bool isGameActive;
    private bool isPaused;
    private UIHandler uiHandler;
    private TimerHandler timer;
    private FlagCountHandler flagCountHandler;

    void Start()
    {
        timer = FindObjectOfType<TimerHandler>();
        uiHandler = FindObjectOfType<UIHandler>();
        flagCountHandler = FindObjectOfType<FlagCountHandler>();

        Vector2Int mapDimensions = GameValuesController.instance.GetMapDimensions();
        int minesToPlace = GameValuesController.instance.GetMinesToPlaceCount(mapDimensions);

        map = new Map(mapDimensions, minesToPlace);
        gridPrefabVisual.Setup(map.GetGrid());
        isGameActive = true;
        isPaused = false;

        flagCountHandler.Setup(map);

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

                if (Input.GetMouseButtonDown(0))
                {
                    Vector3 position = GetMouseWorldPosition();
                    MapGridObject.Type gridObjectType = map.RevealGridPosition(position);
                    if (gridObjectType == MapGridObject.Type.Mine)
                    {
                        isGameActive = false;
                        StartCoroutine(map.RevealEntireMap(2f));
                        StartCoroutine(uiHandler.LoseCoroutine(2f));
                    }
                }
                if (Input.GetMouseButtonDown(1))
                {
                    Vector3 position = GetMouseWorldPosition();
                    map.ChangeFlaggedStateOnGridPosition(position);

                    flagCountHandler.UpdateFlagCount();
                }

               /* if (Input.GetKeyDown(KeyCode.D))
                {
                    gridPrefabVisual.SetRevealMap(true);
                }
                if (Input.GetKeyUp(KeyCode.D))
                {
                    gridPrefabVisual.SetRevealMap(false);
                }*/

            }

            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Pause))
            {
                ChangePauseState();
            }
        }

    }

    public void ChangePauseState()
    {
        AudioManager.instance.PlaySound(Sound.Type.ButtonClick);
        isPaused = !isPaused;
        uiHandler.ShowPauseWindow(isPaused);
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0f;
        return worldPosition;
    }

    
}
