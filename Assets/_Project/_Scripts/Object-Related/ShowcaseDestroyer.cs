using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowcaseDestroyer : MonoBehaviour
{
    private ShowcaseTransform _currentShowcase;

    private void OnDisable()
    {
        if (_currentShowcase)
        {
            _currentShowcase.OnDeselect();
            _currentShowcase = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch),
            OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch) * Vector3.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, LayerMask.GetMask("SpawnedObject")))
        {
            if (hit.collider.TryGetComponent(out ShowcaseTransform data))
            {
                if (_currentShowcase != data)
                {   
                    if (_currentShowcase)
                        _currentShowcase.OnDeselect();
                    
                    _currentShowcase = data;
                    _currentShowcase.OnSelect();
                }
            }
            else
            {
                if (!_currentShowcase) return;
                _currentShowcase.OnDeselect();
                _currentShowcase = null;
            }
        }
        else
        {
            if (!_currentShowcase) return;
            _currentShowcase.OnDeselect();
            _currentShowcase = null;
        }
        
        if (OVRInput.GetDown(OVRInput.RawButton.A))
        {
            if (_currentShowcase)
                Destroy(_currentShowcase.gameObject);
        }
    }
}
