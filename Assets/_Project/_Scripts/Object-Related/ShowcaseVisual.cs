using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShowcaseVisual : MonoBehaviour
{
    public Animator animator;
    public float distance = 1f;
    public DistanceChecker distanceChecker;

    public bool isActive = false;

    public UnityEvent OnActivate;
    public UnityEvent OnDeactivate;

    // Start is called before the first frame update
    
    private void Update()
    {
        if (distanceChecker.currentDistance < distance)
        {
            ActivateAnimation();
        }
    }

    public void ActivateAnimation()
    {
        if (isActive) return;
        
        animator.SetBool("Activate", true);
        isActive = true;
        OnActivate.Invoke();
    }

    public void DeactivateAnimation()
    {
        if (!isActive) return;
        
        animator.SetBool("Activate", false);
        isActive = false;
        OnDeactivate.Invoke();
    }
}
