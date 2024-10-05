using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GenericMenuItem : MenuItem
{
    public UnityEvent OnSelectEvent;
    public UnityEvent OnDeselectEvent;
    
    public override void OnSelect()
    {
        base.OnSelect();
        OnSelectEvent.Invoke();
    }

    public override void OnDeselect()
    {
        base.OnDeselect();
        OnDeselectEvent.Invoke();
    }
}
