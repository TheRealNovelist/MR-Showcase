using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class DistanceChecker : MonoBehaviour, IResettable
{
    public TMP_Text text;
    public string trackingID = "MainCamera";
    public float range = 1f;

    public UnityEvent OnInRange;
    public UnityEvent OnOutOfRange;
    
    [Header("Debug")]
    public float currentDistance;
    
    private Transform _objectToTrack;
    private bool _isInRange;
    
    private void Awake()
    {
        _objectToTrack = GameObject.FindWithTag(trackingID).transform;
    }

    private void Update()
    {
        Vector3 trackedPosition = _objectToTrack.position;
        trackedPosition.y = 0;
        
        Vector3 thisObject = transform.position;
        thisObject.y = 0;

        currentDistance = Vector3.Distance(trackedPosition, thisObject);
        text.text = $"{currentDistance}";

        if (currentDistance <= range && !_isInRange)
        {
            _isInRange = true;
            OnInRange.Invoke();
        }
    }

    public void OnReset()
    {
        _isInRange = false;
    }
}
