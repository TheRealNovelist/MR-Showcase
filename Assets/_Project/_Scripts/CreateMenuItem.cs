using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateMenuItem : MenuItem
{
    public ShowcasePrefabSpawner spawner;

    public override void OnSelect()
    {
        base.OnSelect();
        
        spawner.enabled = true;
    }

    public override void OnDeselect()
    {
        base.OnDeselect();
        
        spawner.enabled = false;
    }
}
