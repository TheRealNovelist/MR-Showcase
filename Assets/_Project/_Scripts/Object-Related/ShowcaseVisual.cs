using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class ShowcaseVisual : MonoBehaviour, IResettable
{
    public Animator animator;

    public bool isActive;

    public UnityEvent OnPlayAnimation;
    public UnityEvent OnResetAnimation;
    public UnityEvent OnCompleteAnimation;
    
    private Sequence _sequence;

    // Start is called before the first frame update

    private void Start()
    {

    }

    public void PlayAnimation()
    {
        if (isActive) return;
        
        isActive = true;
        OnPlayAnimation.Invoke();

        animator.SetBool("Activate", true);
    }

    public void ResetAnimation()
    {
        isActive = false;
        OnResetAnimation.Invoke();
        
        animator.SetBool("Activate", false);
    }

    public void ToggleDebug(bool isOn)
    {
        
    }

    public void OnReset()
    {
        ResetAnimation();
    }

    public void OnCompleteAnimationInAnimator()
    {
        OnCompleteAnimation.Invoke();
    }
}
