using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Meta.XR.MRUtilityKit;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject menuRoot;
    [SerializeField] private List<MenuItem> menuItems;

    [SerializeField] private Button resetAllButton;
    
    private MenuItem _currentMenuItem;

    private static bool _isMenuOn;
    public static bool IsMenuOn
    {
        get => _isMenuOn;
        private set
        {
            _isMenuOn = value;
            OnMenuToggled?.Invoke(_isMenuOn);
        }
    }

    public static event Action<bool> OnMenuToggled;

    private void Awake()
    {
        resetAllButton.onClick.AddListener(ResetAll);
    }

    private void Start()
    {
        foreach (Transform child in transform)
        {
            MenuItem menuItem = child.GetComponent<MenuItem>();
            menuItem.button.onClick.AddListener(() => OnSelectMenuItem(menuItem));
            menuItem.OnDeselect();
            menuItem.tab.SetActive(false);
            menuItems.Add(menuItem);
        }
        
        TurnOffMenu();
    }
    
    private void ResetAll()
    {
        foreach (IResettable resettable in FindObjectsOfType<MonoBehaviour>().OfType<IResettable>())
        {
            resettable.OnReset();
        }
        
        AudioManager.StopAll();
        DOTween.KillAll();
    }
    
    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.Start))
        {
            if (IsMenuOn)
            {
                TurnOffMenu();
            }
            else
            {
                TurnOnMenu();
            }
        }

        if (OVRInput.GetDown(OVRInput.RawButton.X))
        {
            ResetAll();
        }
    }

    private void TurnOnMenu()
    {
        if (_currentMenuItem) _currentMenuItem.OnSelect();
        menuRoot.SetActive(true);
        
        IsMenuOn = true;
    }

    private void TurnOffMenu()
    {
        if (_currentMenuItem) _currentMenuItem.OnDeselect();
        menuRoot.SetActive(false);
        
        IsMenuOn = false;
    }

    private void OnSelectMenuItem(MenuItem menu)
    {
        if (_currentMenuItem) _currentMenuItem.OnDeselect();
        _currentMenuItem = menu;
        _currentMenuItem.OnSelect();

        foreach (MenuItem menuItem in menuItems)
        {
            menuItem.tab.SetActive(menuItem == _currentMenuItem);
        }
    }
    
    private List<T> GetAllObject<T>() where T : Component
    {
        return FindObjectsOfType<T>().ToList();
    }
}
