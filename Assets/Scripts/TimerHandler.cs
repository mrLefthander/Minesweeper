using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerHandler: MonoBehaviour
{

    [SerializeField] private TMP_Text timerVisual;

    private float timer;
    private int score;

    public void HandleTimer()
    {
        timer += Time.deltaTime;
        score = Mathf.FloorToInt(timer);
        UpdateTimerVisual(score);
    }

    private void UpdateTimerVisual(int value)
    {
        timerVisual.text = value.ToString();
    }

    public int GetScore()
    {
        return score;
    }
}
