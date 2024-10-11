using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public string trackingID = "MainCamera";
    private Transform _objectToTrack;
    
    private void Awake()
    {
        _objectToTrack = GameObject.FindWithTag(trackingID).transform;
    }

    private void Update()
    {
        Vector3 target = _objectToTrack.position;
        target.y = transform.position.y;
        transform.LookAt(target);
    }
}
