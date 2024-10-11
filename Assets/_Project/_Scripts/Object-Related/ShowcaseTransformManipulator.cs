using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ShowcaseTransformManipulator : MonoBehaviour
{
    [Header("Group")]
    public Button prevGroupButton;
    public Button nextGroupButton;
    public TMP_Text groupText;
    public List<ShowcaseTransformGroup> groups;
    public int index = 0;

    private ShowcaseTransformGroup _currentGroup;
    
    [Header("Height")]
    public float stepHeight = 0.1f;
    public Button upHeightButton;
    public Button downHeightButton;
    public TMP_Text heightText;
    
    [Space]
    [Header("Scale")]
    public float stepScale = 0.1f;
    public Button upScaleButton;
    public Button downScaleButton;
    public TMP_Text scaleText;
    

    private void Start()
    {
        ChangeGroup(groups[index]);

        prevGroupButton.onClick.AddListener(() => SwitchGroup(-1));
        nextGroupButton.onClick.AddListener(() => SwitchGroup(1));
        
        upHeightButton.onClick.AddListener(() =>
        {
            if (_currentGroup) _currentGroup.Height += stepHeight;
        });
        downHeightButton.onClick.AddListener(() =>
        {
            if (_currentGroup) _currentGroup.Height -= stepHeight;
        });
        
        upScaleButton.onClick.AddListener(() =>
        {
            if (_currentGroup) _currentGroup.Scale += stepScale;
        });
        downScaleButton.onClick.AddListener(() =>
        {
            if (_currentGroup) _currentGroup.Scale -= stepScale;
        });
    }

    private void OnDestroy()
    {
        if (_currentGroup) UnbindGroup(_currentGroup);
    }

    public void ChangeGroup(ShowcaseTransformGroup group)
    {
        if (_currentGroup) UnbindGroup(_currentGroup);

        _currentGroup = group;
        BindGroup(_currentGroup);
        
        groupText.text = _currentGroup.groupName;
        
        ChangeHeightText(_currentGroup.Height);
        ChangeScaleText(_currentGroup.Scale);
    }
    
    public void BindGroup(ShowcaseTransformGroup group)
    {
        group.OnHeightChanged += ChangeHeightText;
        group.OnScaleChanged += ChangeScaleText;
    }

    public void UnbindGroup(ShowcaseTransformGroup group)
    {
        group.OnHeightChanged -= ChangeHeightText;
        group.OnScaleChanged -= ChangeScaleText;
    }
    
    public void SwitchGroup(int increment)
    {
        int newIndex = index + increment;
        if (newIndex <= 0)
            newIndex = groups.Count - 1;
        else if (newIndex >= groups.Count)
            newIndex = 0;

        index = newIndex;
        
        ChangeGroup(groups[index]);
    }

    public void ChangeHeightText(float height) => heightText.text = $"{Mathf.Round(height * 100) / 100}";
    public void ChangeScaleText(float scale) => scaleText.text = $"{Mathf.Round(scale * 100) / 100}";
}
