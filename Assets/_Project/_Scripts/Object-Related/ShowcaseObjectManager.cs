using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowcaseObjectManager : Singleton<ShowcaseObjectManager>, IDataPersistence
{
    public List<ShowcaseObject> objects = new List<ShowcaseObject>();
    
    public void LoadGame(GameData gameData)
    {
        foreach (ShowcaseObjectData objectData in gameData.loadedObject)
        {
            CreateShowcaseObject(objectData);
        }
    }

    public void SaveGame(ref GameData gameData)
    {
        List<ShowcaseObjectData> newDataList = new();
        foreach (ShowcaseObject obj in objects)
        {
            newDataList.Add(obj.GetData());
        }

        gameData.loadedObject = new List<ShowcaseObjectData>(newDataList);
    }

    public void CreateShowcaseObject(ShowcaseObjectData objectData)
    {
        ShowcaseObject newObject = Instantiate(objectData.prefabConfig.prefab);
        newObject.LoadData(objectData);
        objects.Add(newObject);
    }
}
