using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinesweeperGameHandler : MonoBehaviour
{
    [SerializeField] private GridPrefabVisual gridPrefabVisual;
    [SerializeField] private TMPro.TextMeshPro timerText;
    private Map map;
    private float timer;
    private bool isGameActive;
    private bool isPaused;
    private UIHandler uiHandler;

    void Start()
    {
        uiHandler = FindObjectOfType<UIHandler>();

        GameValuesController.instance.GetGameValues(out Vector2Int mapDimensions, out int minesToPlace);
        map = new Map(mapDimensions, minesToPlace);
        gridPrefabVisual.Setup(map.GetGrid());
        isGameActive = true;
        isPaused = false;

        map.OnEntireMapRevealed += Map_OnEntireMapRevealed;
    }

    private void Map_OnEntireMapRevealed(object sender, EventArgs e)
    {
        isGameActive = false;
        int timeScore = Mathf.FloorToInt(timer);

        StartCoroutine(uiHandler.WinCoroutine(timeScore));
    }

    void Update()
    {
        if (isGameActive)
        {
            if (!isPaused)
            {
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
                }

                if (Input.GetKeyDown(KeyCode.D))
                {
                    gridPrefabVisual.SetRevealMap(true);
                }
                if (Input.GetKeyUp(KeyCode.D))
                {
                    gridPrefabVisual.SetRevealMap(false);
                }

                HandleTimer();
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

    private void HandleTimer()
    {
        if (isGameActive)
        {
            timer += Time.deltaTime;
            timerText.text = Mathf.FloorToInt(timer).ToString();
        }
    }
}
