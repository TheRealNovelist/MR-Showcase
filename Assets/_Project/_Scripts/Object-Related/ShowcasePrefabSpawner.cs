using System;
using System.Collections;
using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
using UnityEngine;

public class ShowcasePrefabSpawner : MonoBehaviour
{
    public ShowcasePrefabConfig showcasePrefabConfig;
    public float rotateRate = 2;
    
    public MRUKAnchor.SceneLabels filter;

    private void Start()
    {
        _player = GameObject.FindWithTag("MainCamera").transform;
    }

    private float _angle;
    private Transform _player;
    private SceneState _currentState = SceneState.AcquiringPosition;
    private Vector3 _storedPosition;
    private GameObject _currentPreview;
    
    private enum SceneState
    {
        AcquiringPosition,
        AcquiringRotation
    }

    public void ChangePrefab(ShowcasePrefabConfig newShowcasePrefabConfig)
    {
        showcasePrefabConfig = newShowcasePrefabConfig;
        if (_currentPreview) Destroy(_currentPreview);
        _currentPreview = Instantiate(showcasePrefabConfig.previewPrefab);
        _currentPreview.SetActive(enabled);
    }
    
    private void OnEnable()
    {
        if (_currentPreview) _currentPreview.gameObject.SetActive(true);
    }
    
    private void OnDisable()
    {
        if (_currentPreview) _currentPreview.gameObject.SetActive(false);
    }

    private void Update()
    {
        float thumbstickVal = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick).x;

        Ray ray = new Ray(OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch),
            OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch) * Vector3.forward);

        switch (_currentState)
        {
            case SceneState.AcquiringPosition:
                AcquiringPosition(ray);
                break;
            case SceneState.AcquiringRotation:
                AcquiringRotation(thumbstickVal);
                break;
        }
    }

    private void AcquiringPosition(Ray ray)
    {
        if (MRUK.Instance.GetCurrentRoom().Raycast(ray, 100, LabelFilter.Included(filter), out RaycastHit hit, out MRUKAnchor anchor))
        {
            _currentPreview.gameObject.SetActive(true);
            _currentPreview.transform.position = hit.point;
            _currentPreview.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            _currentPreview.transform.rotation = Quaternion.LookRotation(new Vector3(_player.position.x, _currentPreview.transform.position.y, _player.position.z) - _currentPreview.transform.position, Vector3.up);

            if (OVRInput.GetDown(OVRInput.RawButton.A))
            {
                _storedPosition = hit.point;
                _currentState = SceneState.AcquiringRotation;
            }
        }
        else
        {
            _currentPreview.gameObject.SetActive(false);
        }
    }

    private void AcquiringRotation(float thumbstickVal)
    {
        if (Mathf.Abs(thumbstickVal) > 0)
        {
            _angle = thumbstickVal * rotateRate * Time.deltaTime;
            _currentPreview.transform.Rotate(Vector3.up, _angle);
        }
        
        if (OVRInput.GetDown(OVRInput.RawButton.B))
        {
            _currentState = SceneState.AcquiringPosition;
        }
        
        if (OVRInput.GetDown(OVRInput.RawButton.A))
        {
            Apply();
            _currentState = SceneState.AcquiringPosition;
        }
    }

    private void Apply()
    {
        ShowcaseObjectManager.Instance.CreateShowcaseObject(showcasePrefabConfig, _storedPosition, _currentPreview.transform.rotation);
        
        _storedPosition = Vector3.zero;
    }
}
