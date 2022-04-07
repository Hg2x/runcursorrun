using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    public static event TimerDone TimerDoneEvent;

    [SerializeField]
    protected TextMeshProUGUI timeText;

    protected Stopwatch stopWatch = new Stopwatch();

    protected bool timerIsOn;

    protected int minutes = 0;
    protected int seconds = 0;

    protected TimeSpan totalDuration;

    public void StartTimer(int timerMinutes, int timerSeconds)
    {
        if (timerMinutes > 0)
        {
            minutes = timerMinutes;
        }
        if (timerSeconds > 0)
        {
            seconds = timerSeconds;
        }

        totalDuration = new TimeSpan(0, minutes, seconds);
        stopWatch.Start();
        timerIsOn = true;
    }

    public void StopTimer()
    {
        stopWatch.Stop();
        timerIsOn = false;
    }

    protected void Start()
    {
    }

    protected void Update()
    {
        if (timerIsOn)
        {
            UpdateTimer();
        }
    }

    protected void UpdateTimer()
    {
        // maybe a conditional check to see if a second has passed
        var totalSeconds = (int) (totalDuration.TotalSeconds - stopWatch.Elapsed.TotalSeconds); // 3 minutes = 180
        var minutesLeft = totalSeconds / 60;
        var SecondsLeft = totalSeconds % 60;
        timeText.text = minutesLeft + " : " + SecondsLeft;

        if (totalSeconds <= 0)
        {
            TimerDoneEvent?.Invoke();
            OnTimerDone();
            StopTimer();
            // send stage cleared event
            // dont forget to stop everything, since this is update loop
        }
    }

    protected void OnTimerDone()
    {

    }
}
