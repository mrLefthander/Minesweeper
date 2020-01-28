using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    public event EventHandler OnEntireMapRevealed;
    private Grid<MapGridObject> grid;
    private int minesCount = 0;
    private int revealedCellsCount = 0;
    private int flaggedCellsCount = 0;

    public Map()
    {
        grid = new Grid<MapGridObject>(10, 10, 10f, new Vector3(-50, -55, 0), (Grid<MapGridObject> g, int x, int y) => new MapGridObject(g, x, y));

        while (minesCount < 10)
        {
            int x = UnityEngine.Random.Range(0, grid.GetWidth());
            int y = UnityEngine.Random.Range(0, grid.GetHeight());

            MapGridObject mapGridObject = grid.GetGridObject(x, y);
            if (mapGridObject.GetGridObjectType() != MapGridObject.Type.Mine)
            {
                mapGridObject.SetGridObjectType(MapGridObject.Type.Mine);
                minesCount++;
            }
        }

        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                MapGridObject mapGridObject = grid.GetGridObject(x, y);
                if(mapGridObject.GetGridObjectType() == MapGridObject.Type.Empty)
                {
                    List<MapGridObject> neighbourList = GetNeighbourList(x, y);
                    int mineCount = 0;
                    foreach(MapGridObject neighbour in neighbourList)
                    {
                        if (neighbour.GetGridObjectType() == MapGridObject.Type.Mine)
                        {
                            mineCount++;
                        }
                    }

                    switch(mineCount)
                    {
                        case 1: mapGridObject.SetGridObjectType(MapGridObject.Type.MineNum_1); break;
                        case 2: mapGridObject.SetGridObjectType(MapGridObject.Type.MineNum_2); break;
                        case 3: mapGridObject.SetGridObjectType(MapGridObject.Type.MineNum_3); break;
                        case 4: mapGridObject.SetGridObjectType(MapGridObject.Type.MineNum_4); break;
                        case 5: mapGridObject.SetGridObjectType(MapGridObject.Type.MineNum_5); break;
                        case 6: mapGridObject.SetGridObjectType(MapGridObject.Type.MineNum_6); break;
                        case 7: mapGridObject.SetGridObjectType(MapGridObject.Type.MineNum_7); break;
                        case 8: mapGridObject.SetGridObjectType(MapGridObject.Type.MineNum_8); break;
                    }
                }
            }
        }
    }

    public MapGridObject.Type RevealGridPosition(Vector3 position)
    {
        MapGridObject mapGridObject = grid.GetGridObject(position);
        if (mapGridObject != null)
        {
            return RevealGridPosition(mapGridObject);
        }
        return default;
    }

    public MapGridObject.Type RevealGridPosition(MapGridObject mapGridObject)
    {
        //Reveal this object
        RevealGridObject(mapGridObject);
        //Is this an Empty grid object?
        if (mapGridObject.GetGridObjectType() == MapGridObject.Type.Empty)
        {
            //Is Empty, reveal connected nodes

            //Keep track of nodes already checked
            List<MapGridObject> alreadyCheckedNeighbourList = new List<MapGridObject>();
            //Nodes queues up for checking
            List<MapGridObject> checkNeighbourList = new List<MapGridObject>();
            checkNeighbourList.Add(mapGridObject);
            //Whole we have nodes to check
            while (checkNeighbourList.Count > 0)
            {
                //Grab first one
                MapGridObject checkMapGridObject = checkNeighbourList[0];
                //Remove from queue
                checkNeighbourList.RemoveAt(0);
                alreadyCheckedNeighbourList.Add(checkMapGridObject);

                //Cycle through all neighbours
                foreach (MapGridObject neighbour in GetNeighbourList(checkMapGridObject))
                {
                    if (neighbour.GetGridObjectType() != MapGridObject.Type.Mine)
                    {
                        //Reveal if not a Mine
                        RevealGridObject(neighbour);
                        if (neighbour.GetGridObjectType() == MapGridObject.Type.Empty)
                        {
                            if (!alreadyCheckedNeighbourList.Contains(neighbour))
                            {
                                //If Empty and not checked yet add it to queue
                                checkNeighbourList.Add(neighbour);
                            }
                        }
                    }
                }
            }
        }
        if (IsEntireMapRevealed())
        {

            OnEntireMapRevealed?.Invoke(this, EventArgs.Empty);
        }
        return mapGridObject.GetGridObjectType();
    }

    private void RevealGridObject(MapGridObject mapGridObject)
    {
        if (!mapGridObject.IsRevealed())
        {
            mapGridObject.Reveal();
            revealedCellsCount++;
        }
    }
    
    public void ChangeFlaggedStateOnGridPosition(Vector3 worldPosition)
    {
        MapGridObject mapGridObject = grid.GetGridObject(worldPosition);
        if (mapGridObject != null && !mapGridObject.IsRevealed())
        {
            mapGridObject.ChangeFlaggedState();
            if (mapGridObject.IsFlagged())
            {
                flaggedCellsCount++;
            }
            else
            {
                flaggedCellsCount--;
            }
        }
        if (IsEntireMapRevealed())
        {
            OnEntireMapRevealed?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool IsEntireMapRevealed()
    {
        int emptyCellsCount = grid.GetWidth() * grid.GetHeight() - minesCount;
        return (revealedCellsCount == emptyCellsCount && flaggedCellsCount == minesCount) ? true : false;
    }

    public void RevealEntireMap()
    {
        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                MapGridObject mapGridObject = grid.GetGridObject(x, y);
                mapGridObject.Reveal();
            }
        }
    }

    private List<MapGridObject> GetNeighbourList(MapGridObject gridObject)
    {
        return GetNeighbourList(gridObject.GetX(), gridObject.GetY());
    }

    private List<MapGridObject> GetNeighbourList(int x, int y)
    {
        List<MapGridObject> neighbourList = new List<MapGridObject>();
        if(x - 1 >= 0)
        {
            //Left
            neighbourList.Add(grid.GetGridObject(x - 1, y));
            //Left Down
            if(y - 1 >= 0) neighbourList.Add(grid.GetGridObject(x - 1, y - 1));
            //Left Up
            if(y + 1 < grid.GetHeight()) neighbourList.Add(grid.GetGridObject(x - 1, y + 1));
        }
        if (x + 1 < grid.GetWidth())
        {
            //Right
            neighbourList.Add(grid.GetGridObject(x + 1, y));
            //Right Down
            if (y - 1 >= 0) neighbourList.Add(grid.GetGridObject(x + 1, y - 1));
            //Right Up
            if (y + 1 < grid.GetHeight()) neighbourList.Add(grid.GetGridObject(x + 1, y + 1));
        }
        //Up
        if(y - 1 >= 0) neighbourList.Add(grid.GetGridObject(x, y - 1));
        //Down
        if (y + 1 < grid.GetHeight()) neighbourList.Add(grid.GetGridObject(x, y + 1));

        return neighbourList;
    }

    public Grid<MapGridObject> GetGrid()
    {
        return grid;
    }

   
}
