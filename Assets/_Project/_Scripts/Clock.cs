using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour, IResettable
{
    public TMP_Text text;
    public float delay = 5f;
    public float interval = 60f;
    
    public int timeToTickDown = 10;
    public GameEvent onClockFinish;

    private Coroutine _cacheRoutine;
    
    public void StartClock()
    {
        _cacheRoutine = StartCoroutine(ClockRoutine());
    }

    private IEnumerator ClockRoutine()
    {
        yield return new WaitForSeconds(delay);

        int startingTime = 50;

        for (int i = 0; i <= timeToTickDown; i++)
        {
            if (i < 10)
            {
                text.text = $"14:{startingTime + i}";
                yield return new WaitForSeconds(interval);
            }
            else
            {
                text.text = $"15:00";
            }
        }
    }

    public void OnReset()
    {
        text.text = $"14:50";
        StopCoroutine(_cacheRoutine);
    }
}
