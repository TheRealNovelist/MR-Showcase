using System.Collections;
using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
using UnityEngine;

public class ShowcaseObjectManager : Singleton<ShowcaseObjectManager>, IDataPersistence
{
    public List<ShowcaseObject> objects = new List<ShowcaseObject>();
    public MRUKRoom room;
    public MRUKAnchor floorAnchor;

    public void OnRoomLoaded()
    {
        room = MRUK.Instance.GetCurrentRoom();
        floorAnchor = room.Anchors.Find(x => x.HasAnyLabel(MRUKAnchor.SceneLabels.FLOOR));

        if (floorAnchor)
        {
            transform.position = floorAnchor.transform.position;
            transform.rotation = floorAnchor.transform.rotation;
        }
    }
    
    public void LoadGame(GameData gameData)
    {
        foreach (ShowcaseObject obj in objects)
        {
            Destroy(obj.gameObject);
        }

        objects = new List<ShowcaseObject>();
        
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

    public void RemoveShowcaseObject(ShowcaseObject obj)
    {
        if (objects.Contains(obj)) objects.Remove(obj);
        Destroy(obj.gameObject);
    }

    public void CreateShowcaseObject(ShowcaseObjectData objectData)
    {
        ShowcaseObject newObject = Instantiate(objectData.prefabConfig.prefab, transform, true);
        newObject.LoadData(objectData);
        objects.Add(newObject);
    }
    
    public void CreateShowcaseObject(ShowcasePrefabConfig prefabConfig, Vector3 position, Quaternion rotation)
    {
        ShowcaseObject newObject = Instantiate(prefabConfig.prefab, position, rotation);
        newObject.transform.parent = transform;
        objects.Add(newObject);
    }
}
