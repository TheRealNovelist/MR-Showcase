using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ObjectGroup
{
    public string groupName;
    public float height = 1f;
    public float scale = 1f;

    public ObjectGroup(string groupName, float height = 1f, float scale = 1f)
    {
        this.groupName = groupName;
        this.height = height;
        this.scale = scale;
    }
}

public class ShowcaseTransformManipulator : Singleton<ShowcaseTransformManipulator>
{
    [Header("Group")]
    public Button prevGroupButton;
    public Button nextGroupButton;
    public TMP_Text groupText;
    public List<ObjectGroup> groups;
    public int index = 0;
    
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
        groupText.text = groups[index].groupName;
        
        heightText.text = $"{groups[index].height}";
        scaleText.text = $"{groups[index].scale}";
        
        prevGroupButton.onClick.AddListener(() => SwitchGroup(-1));
        nextGroupButton.onClick.AddListener(() => SwitchGroup(1));

        upHeightButton.onClick.AddListener(() => AddHeight(groups[index].groupName, stepHeight));
        downHeightButton.onClick.AddListener(() => AddHeight(groups[index].groupName, -stepHeight));
        
        upScaleButton.onClick.AddListener(() => AddScale(groups[index].groupName, stepScale));
        downScaleButton.onClick.AddListener(() => AddScale(groups[index].groupName, -stepScale));
    }

    public float GetHeight(string group)
    {
        return GetGroup(group).height;
    }
    
    public void SetHeight(string group, float newHeight)
    {
        ObjectGroup objectGroup = GetGroup(group);
        objectGroup.height = newHeight;
        heightText.text = $"{objectGroup.height}";
        foreach (ShowcaseTransform objectData in GetAllObject<ShowcaseTransform>().Where(objectData => objectData.group == group))
        {
            objectData.SetHeight(objectGroup.height);
        }
    }
    
    public void AddHeight(string group, float increment)
    {
        ObjectGroup objectGroup = GetGroup(group);
        objectGroup.height += increment;
        heightText.text = $"{objectGroup.height}";
        foreach (ShowcaseTransform objectData in GetAllObject<ShowcaseTransform>().Where(objectData => objectData.group == group))
        {
            objectData.SetHeight(objectGroup.height);
        }
    }

    public float GetScale(string group)
    {
        return GetGroup(group).scale;
    }
    
    public void SetScale(string group, float newScale)
    {
        ObjectGroup objectGroup = GetGroup(group);
        objectGroup.scale = newScale;
        scaleText.text = $"{objectGroup.scale}";
        foreach (ShowcaseTransform objectData in GetAllObject<ShowcaseTransform>().Where(objectData => objectData.group == group))
        {
            objectData.SetScale(objectGroup.scale);
        }
    }
    
    public void AddScale(string group, float increment)
    {
        ObjectGroup objectGroup = GetGroup(group);
        objectGroup.scale += increment;
        scaleText.text = $"{objectGroup.scale}";
        foreach (ShowcaseTransform objectData in GetAllObject<ShowcaseTransform>().Where(objectData => objectData.group == group))
        {
            objectData.SetScale(objectGroup.scale);
        }
    }

    public ObjectGroup GetGroup(string group)
    {
        ObjectGroup objectGroup = groups.FirstOrDefault(x => x.groupName == group);
        if (objectGroup == null)
        {
            objectGroup = new ObjectGroup(group);
            groups.Add(objectGroup);
        }
        
        return objectGroup;
    }
    
    public void SwitchGroup(int increment)
    {
        int newIndex = index + increment;
        if (newIndex <= 0)
            newIndex = groups.Count - 1;
        else if (newIndex >= groups.Count)
            newIndex = 0;

        index = newIndex;
        groupText.text = groups[index].groupName;
        heightText.text = $"{groups[index].height}";
        scaleText.text = $"{groups[index].scale}";
    }

    private List<T> GetAllObject<T>() where T : Component
    {
        return FindObjectsByType<T>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID).ToList();
    }
}
