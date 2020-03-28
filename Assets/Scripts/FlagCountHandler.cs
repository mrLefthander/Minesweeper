using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlagCountHandler: MonoBehaviour
{
    private const string FLAG_COUNT_VISUAL_OBJECT_NAME = "flagCountText";

    private TMP_Text flagCountVisual;
    private int initialFlagCount;
    private Map map;

    private void Awake()
    {
        flagCountVisual = GameObject.Find(FLAG_COUNT_VISUAL_OBJECT_NAME).GetComponent<TMP_Text>();
    }

    public void Setup(Map map)
    {
        this.map = map;
        initialFlagCount = map.GetMinesCount();
        UpdateFlagCountVisual(initialFlagCount);
    }

    public void UpdateFlagCount()
    {
        int currentFlagCount = initialFlagCount - map.GetFlaggedCellsCount();
        UpdateFlagCountVisual(currentFlagCount);
    }

    private void UpdateFlagCountVisual(int amount)
    {
        flagCountVisual.text = amount.ToString();
        if (amount < 0)
        {
            flagCountVisual.color = Color.red;
        }
        if (amount >= 0)
        {
            flagCountVisual.color = Color.white;
        }
    }
}
