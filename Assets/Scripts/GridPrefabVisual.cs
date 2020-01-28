using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridPrefabVisual : MonoBehaviour
{
    [SerializeField] private Transform pfGridVisualNode;
    [SerializeField] private Sprite flagSprite;
    [SerializeField] private Sprite mineSprite;

    //constants for string refs
    const string GRID_OBJECTS_PARENT_NAME = "Grid";
    const string GRID_OBJECT_ICON_SPRITE_NAME = "IconSprite";
    const string GRID_OBJECT_MINE_INDICATOR_NAME = "MineText";
    const string GRID_OBJECT_TOP_SPRITE_NAME = "TopSprite";

    GameObject gridParent;
    private Grid<MapGridObject> grid;
    private List<Transform> visualNodeList;
    private Transform[,] visualNodeArray;
    private bool revealMap;
    private bool updateVisual;

    private void Awake()
    {
        visualNodeList = new List<Transform>();
        gridParent = GameObject.Find(GRID_OBJECTS_PARENT_NAME);
        if (!gridParent)
        {
            gridParent = new GameObject(GRID_OBJECTS_PARENT_NAME);
        }
    }

    public void SetRevealMap(bool revealMap)
    {
        this.revealMap = revealMap;
        UpdateVisual(grid);
    }

    public void Setup(Grid<MapGridObject> grid)
    {
        this.grid = grid;
        visualNodeArray = new Transform[grid.GetWidth(), grid.GetHeight()];

        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                Vector3 gridPosition = new Vector3(x, y) * grid.GetCellSize() + Vector3.one * grid.GetCellSize() / 2f + grid.GetOriginPosistion();
                Transform visualNode = CreateVisualNode(gridPosition);
                visualNodeArray[x, y] = visualNode;
                visualNodeList.Add(visualNode);
            }
        }

        HideNodeVisuals();
        UpdateVisual(grid);

        grid.OnGridObjectChanged += Grid_OnGridObjectChanged;
    }
    private void Update()
    {
        if (updateVisual)
        {
            updateVisual = false;
            UpdateVisual(grid);
        }
    }
    private void Grid_OnGridObjectChanged(object sender, Grid<MapGridObject>.OnGridObjectChangedEventArgs e)
    {
        updateVisual = true;
    }

    public void UpdateVisual(Grid<MapGridObject> grid)
    {
        HideNodeVisuals();

        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                MapGridObject gridObject = grid.GetGridObject(x, y);

                Transform visualNode = visualNodeArray[x, y];
                visualNode.gameObject.SetActive(true);
                SetupVisualNode(visualNode, gridObject);
            }
        }
    }

    private void SetupVisualNode(Transform visualNode, MapGridObject mapGridObject)
    {
        SpriteRenderer iconSpriteRenderer = visualNode.Find(GRID_OBJECT_ICON_SPRITE_NAME).GetComponent<SpriteRenderer>();
        TextMeshPro indicatorText = visualNode.Find(GRID_OBJECT_MINE_INDICATOR_NAME).GetComponent<TextMeshPro>();
        Transform nodeTopTransform = visualNode.Find(GRID_OBJECT_TOP_SPRITE_NAME);

        if(mapGridObject.IsRevealed() || revealMap)
        {
            nodeTopTransform.gameObject.SetActive(false);

            switch (mapGridObject.GetGridObjectType())
            {
                default:
                case MapGridObject.Type.Empty:
                    indicatorText.gameObject.SetActive(false);
                    iconSpriteRenderer.gameObject.SetActive(false);
                    break;
                case MapGridObject.Type.Mine:
                    indicatorText.gameObject.SetActive(false);
                    iconSpriteRenderer.gameObject.SetActive(true);
                    iconSpriteRenderer.sprite = mineSprite;
                    break;
                case MapGridObject.Type.MineNum_1:
                case MapGridObject.Type.MineNum_2:
                case MapGridObject.Type.MineNum_3:
                case MapGridObject.Type.MineNum_4:
                case MapGridObject.Type.MineNum_5:
                case MapGridObject.Type.MineNum_6:
                case MapGridObject.Type.MineNum_7:
                case MapGridObject.Type.MineNum_8:
                    indicatorText.gameObject.SetActive(true);
                    iconSpriteRenderer.gameObject.SetActive(false);
                    switch (mapGridObject.GetGridObjectType())
                    {
                        default:
                        case MapGridObject.Type.MineNum_1: indicatorText.SetText("1"); break;
                        case MapGridObject.Type.MineNum_2: indicatorText.SetText("2"); break;
                        case MapGridObject.Type.MineNum_3: indicatorText.SetText("3"); break;
                        case MapGridObject.Type.MineNum_4: indicatorText.SetText("4"); break;
                        case MapGridObject.Type.MineNum_5: indicatorText.SetText("5"); break;
                        case MapGridObject.Type.MineNum_6: indicatorText.SetText("6"); break;
                        case MapGridObject.Type.MineNum_7: indicatorText.SetText("7"); break;
                        case MapGridObject.Type.MineNum_8: indicatorText.SetText("8"); break;
                    }
                    break;
            }
        }
        else
        {
            nodeTopTransform.gameObject.SetActive(true);
            if (mapGridObject.IsFlagged())
            {
                iconSpriteRenderer.gameObject.SetActive(true);
                iconSpriteRenderer.sortingOrder = 4;
                iconSpriteRenderer.sprite = flagSprite;
            }
            else
            {
                iconSpriteRenderer.gameObject.SetActive(false);
            }
        }
        
    }

    private Transform CreateVisualNode(Vector3 position)
    {
        Transform visualNodeTransform = Instantiate(pfGridVisualNode, position, Quaternion.identity, gridParent.transform);
        return visualNodeTransform;
    }

    private void HideNodeVisuals()
    {
        foreach (Transform visualNodeTransform in visualNodeList)
        {
            visualNodeTransform.gameObject.SetActive(false);
        }
    }
}
