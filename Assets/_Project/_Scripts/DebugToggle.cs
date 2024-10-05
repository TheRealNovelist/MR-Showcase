using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DebugToggle : MonoBehaviour
{
    public UnityEvent TurnOnEvent;
    public UnityEvent TurnOffEvent;

    private void Awake()
    {
        Menu.OnMenuToggled += SetDebugActive;
        SetDebugActive(Menu.IsMenuOn);
    }

    private void OnDestroy()
    {
        Menu.OnMenuToggled -= SetDebugActive;
    }
    
    public void SetDebugActive(bool isDebugOn)
    {
        if (isDebugOn)
        {
            TurnOnEvent.Invoke();
        }
        else
        {
            TurnOffEvent.Invoke();
        }
    }
}
