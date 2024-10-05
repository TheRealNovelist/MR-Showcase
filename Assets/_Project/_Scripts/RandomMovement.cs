using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public float floatStrength;
    private Vector2 _originalPos;

    private RectTransform _rect;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
    }

    private void Start()
    {
        _originalPos = _rect.anchoredPosition;
    }

    private void Update()
    {
        _rect.anchoredPosition = _originalPos + new Vector2(_originalPos.x, (Mathf.Sin(Time.time) * floatStrength));
    }
}
