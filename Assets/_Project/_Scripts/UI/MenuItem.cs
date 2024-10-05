using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuItem : MonoBehaviour
{
    public Button button;
    public GameObject tab;

    private void Start()
    {
        OnDeselect();
    }

    public virtual void OnSelect()
    {
        button.image.color = Color.red;
        tab.gameObject.SetActive(true);
    }

    public virtual void OnDeselect()
    {
        button.image.color = Color.white;
        tab.gameObject.SetActive(false);
    }
}