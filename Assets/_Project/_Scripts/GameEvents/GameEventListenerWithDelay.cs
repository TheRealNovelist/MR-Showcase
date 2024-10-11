using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GameEventListenerWithDelay : GameEventListener
{
    [SerializeField] private float delay;

    public override void RaiseEvent()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.AppendInterval(delay);

        sequence.OnComplete(() =>
        {
            OnEventRaised.Invoke();
        });
    }
}
