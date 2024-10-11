using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowcaseTransform : MonoBehaviour
{
    public ShowcaseTransformGroup transformGroup;
    public GameObject root;

    private void Awake()
    {
        transformGroup.OnHeightChanged += SetHeight;
        transformGroup.OnScaleChanged += SetScale;
    }

    private void OnDestroy()
    {
        transformGroup.OnHeightChanged -= SetHeight;
        transformGroup.OnScaleChanged -= SetScale;
    }

    private void Start()
    {
        SetHeight(transformGroup.Height);
        SetScale(transformGroup.Scale);
    }
    
    public void SetHeight(float value)
    {
        var position = root.transform.localPosition;
        position.y = value;
        root.transform.localPosition = position;
    }

    public void SetScale(float value)
    {
        root.transform.localScale = Vector3.one * value;
    }
}
