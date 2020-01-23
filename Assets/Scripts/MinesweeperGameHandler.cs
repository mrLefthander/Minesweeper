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

    void Start()
    {
        map = new Map();
        gridPrefabVisual.Setup(map.GetGrid());
        isGameActive = true;

        map.OnEntireMapRevealed += Map_OnEntireMapRevealed;
    }

    private void Map_OnEntireMapRevealed(object sender, EventArgs e)
    {
        Debug.Log("Win!");
        isGameActive = false;
        int timeScore = Mathf.FloorToInt(timer);
    }

    void Update()
    {
        Vector3 position = GetMouseWorldPosition();
        if (Input.GetMouseButtonDown(0))
        {
            
            MapGridObject.Type gridObjectType =  map.RevealGridPosition(position);
            if(gridObjectType == MapGridObject.Type.Mine)
            {
                Debug.Log("GameOver");
                isGameActive = false;
                map.RevealEntireMap();
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
