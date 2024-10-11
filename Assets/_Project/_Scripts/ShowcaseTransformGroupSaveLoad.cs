using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowcaseTransformGroupSaveLoad : MonoBehaviour, IDataPersistence
{
    [SerializeField] private List<ShowcaseTransformGroup> transformGroups;

    public void LoadGame(GameData gameData)
    {
        foreach (TransformGroup savedGroup in gameData.transformGroups)
        {
            ShowcaseTransformGroup foundTfGroup = transformGroups.Find(x => x.groupName == savedGroup.groupName);
            if (foundTfGroup == null) continue;
            foundTfGroup.Height = savedGroup.height;
            foundTfGroup.Scale = savedGroup.scale;
        }
    }

    public void SaveGame(ref GameData gameData)
    {
        gameData.transformGroups = new List<TransformGroup>();
        foreach (ShowcaseTransformGroup transformGroup in transformGroups)
        {
            gameData.transformGroups.Add(new TransformGroup(transformGroup));
        }
    }
}
