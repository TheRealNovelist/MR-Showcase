
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectorMenu : MonoBehaviour
{
    public Button prevButton;
    public Button nextButton;

    public TMP_Text displayText;

    public ShowcasePrefabSpawner spawner; 

    public List<ShowcasePrefabConfig> items;
    public int index;

    private void Awake()
    {
        prevButton.onClick.AddListener(OnPrevButton);
        nextButton.onClick.AddListener(OnNextButton);
    }

    private void Start()
    {
        index = 0;
        OnSelectItem(items[index]);
    }

    void OnPrevButton()
    {
        int newIndex = index - 1;
        index = newIndex < 0 ? items.Count - 1 : newIndex;
        OnSelectItem(items[index]);
    }

    void OnNextButton()
    {
        int newIndex = index + 1;
        index = newIndex >= items.Count ? 0 : newIndex;
        OnSelectItem(items[index]);
    }

    void OnSelectItem(ShowcasePrefabConfig showcasePrefabConfig)
    {
        displayText.text = showcasePrefabConfig.itemName;
        spawner.ChangePrefab(showcasePrefabConfig);
    }
}
