using System;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField] protected GameEvent gameEvent;
    [SerializeField] protected UnityEvent OnEventRaised;

    private void Awake() => gameEvent.Register(this);

    private void OnDestroy() => gameEvent.Deregister(this);

    public virtual void RaiseEvent()
    { 
        OnEventRaised.Invoke();
    }
}