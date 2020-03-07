using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlagCountHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text timerVisual;
    private int initialFlagCount;
    private Map map;

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

    public void UpdateFlagCountVisual(int amount)
    {
        timerVisual.text = amount.ToString();
    }
}
