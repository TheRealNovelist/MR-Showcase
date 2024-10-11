using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Event", menuName = "Utility/Game Event")]
public class GameEvent : ScriptableObject
{
    private HashSet<GameEventListener> _listeners = new();

    public void Invoke()
    {
        foreach (GameEventListener listener in _listeners)
        {
            listener.RaiseEvent();
        }
    }

    public void Register(GameEventListener eventListener) => _listeners.Add(eventListener);

    public void Deregister(GameEventListener eventListener) => _listeners.Remove(eventListener);
}
