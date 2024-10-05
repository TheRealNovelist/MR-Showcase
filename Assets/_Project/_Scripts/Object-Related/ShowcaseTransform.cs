using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowcaseTransform : MonoBehaviour
{
    public string group = "Ground Image";
    public GameObject root;
    public GameObject highlightMaterial;
    
    private void Start()
    {
        SetHeight(ShowcaseTransformManipulator.Instance.GetHeight(group));
        SetScale(ShowcaseTransformManipulator.Instance.GetHeight(group));
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
