using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private HeatMapVisual heatMapVisual;
    [SerializeField] private TilemapVisual tilemapVisual;
    private Grid<int> grid;
    private Tilemap tilemap;
    private Tilemap.TilemapObject.TilemapSprite tilemapSprite;
    void Start()
    {
        //grid = new Grid<int>(4, 2, 10f, new Vector3(20, 0), (Grid<int> g, int x, int y) => 0);
        tilemap = new Tilemap(20, 10, 10f, new Vector3(20, 0));
        tilemap.SetTilemapVisual(tilemapVisual);
      //  heatMapVisual.SetGrid(grid);
    }


    void Update()
    {
        Vector3 position = GetMouseWorldPosition();
        if (Input.GetMouseButtonDown(0))
        {
           
            tilemap.SetTilemapSprite(position, tilemapSprite);
         //   HeatMapGridObject heatMapGridObject = grid.GetGridObject(position);
            /*            if(heatMapGridObject != null)
                        {
                            heatMapGridObject.AddValue(5);
                        }
                        else
                        {
                            Debug.Log("I am NULL");
                        }*/
            //   grid.SetGridObject(position, 5);
        }


            if (Input.GetKeyDown(KeyCode.T))
        {
            tilemapSprite = Tilemap.TilemapObject.TilemapSprite.None;
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Ground;
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Path;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Dirt;
        }

    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0f;
        return worldPosition;
    }

   
}
