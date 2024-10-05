using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    public List<ShowcaseObjectData> loadedObject;

    public GameData()
    {
        loadedObject = new List<ShowcaseObjectData>();
    }
}
