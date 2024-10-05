using UnityEngine;

[CreateAssetMenu(fileName = "New Showcase Prefab Config", menuName = "Showcase Utility/Prefab Config")]
public class ShowcasePrefabConfig : ScriptableObject
{
    public string itemName;
    public ShowcaseObject prefab;
    public GameObject previewPrefab;
}