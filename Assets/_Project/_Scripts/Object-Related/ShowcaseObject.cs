using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShowcaseObjectData
{
    public ShowcasePrefabConfig prefabConfig;
    public Vector3 position;
    public Quaternion rotation;

    public ShowcaseObjectData(ShowcasePrefabConfig prefabConfig, Vector3 position, Quaternion rotation)
    {
        this.prefabConfig = prefabConfig;
        this.position = position;
        this.rotation = rotation;
    }
    
    public ShowcaseObjectData(ShowcasePrefabConfig prefabConfig, Transform transform)
    {
        this.prefabConfig = prefabConfig;
        position = transform.localPosition;
        rotation = transform.localRotation;
    }
}

public class ShowcaseObject : MonoBehaviour
{
    public ShowcasePrefabConfig prefabConfig;
    public GameObject highlightMaterial;
    
    public void LoadData(ShowcaseObjectData data)
    {
        transform.localPosition = data.position;
        transform.localRotation = data.rotation;
    }

    public ShowcaseObjectData GetData()
    {
        return new ShowcaseObjectData(prefabConfig, transform);
    }

    void Awake()
    {
        highlightMaterial.SetActive(false);
    }
    
    public void OnSelect()
    {
        highlightMaterial.SetActive(true);
    }

    public void OnDeselect()
    {
        highlightMaterial.SetActive(false);
    }
}
