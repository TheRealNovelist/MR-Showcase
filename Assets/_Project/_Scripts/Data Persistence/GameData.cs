using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TransformGroup
{
    public string groupName;
    public float height;
    public float scale;

    public TransformGroup(string groupName, float height, float scale)
    {
        this.groupName = groupName;
        this.height = height;
        this.scale = scale;
    }

    public TransformGroup(ShowcaseTransformGroup transformGroup)
    {
        groupName = transformGroup.groupName;
        height = transformGroup.Height;
        scale = transformGroup.Scale;
    }
}

[Serializable]
public class GameData
{
    public string timeStamp;
    
    public List<ShowcaseObjectData> loadedObject;
    public List<TransformGroup> transformGroups;

    public GameData()
    {
        loadedObject = new List<ShowcaseObjectData>();
        transformGroups = new List<TransformGroup>();
    }
}
