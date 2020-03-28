using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerHandler: MonoBehaviour
{
    private const string TIMER_VISUAL_OBJECT_NAME = "timerText";

    private TMP_Text timerVisual;
    private float timer;
    private int score;

    private void Awake()
    {
        timerVisual = GameObject.Find(TIMER_VISUAL_OBJECT_NAME).GetComponent<TMP_Text>();
    }

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
