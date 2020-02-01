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
    private UIHandler uiHandler;

    void Start()
    {
        uiHandler = FindObjectOfType<UIHandler>();

        map = new Map();
        gridPrefabVisual.Setup(map.GetGrid());
        isGameActive = true;

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

            Vector3 position = GetMouseWorldPosition();
            if (Input.GetMouseButtonDown(0))
            {

                MapGridObject.Type gridObjectType = map.RevealGridPosition(position);
                if (gridObjectType == MapGridObject.Type.Mine)
                {
                    isGameActive = false;
                    StartCoroutine(map.RevealEntireMap(1.3f));
                    StartCoroutine(uiHandler.LoseCoroutine(1.3f));
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
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
