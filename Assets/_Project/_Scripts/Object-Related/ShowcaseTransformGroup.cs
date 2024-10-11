using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Transform Group", menuName = "Showcase Utility/Transform Group")]
public class ShowcaseTransformGroup : ScriptableObject
{
    public string groupName;
    [SerializeField] private float height = 1f;
    [SerializeField] private float scale = 1f;

    public event Action<float> OnHeightChanged;
    public event Action<float> OnScaleChanged; 

    public float Height
    {
        get => height;
        set
        {
            height = value;
            OnHeightChanged?.Invoke(height);
        }
    }

    public float Scale
    {
        get => scale;
        set
        {
            scale = value;
            OnScaleChanged?.Invoke(scale);
        }
    }
}
