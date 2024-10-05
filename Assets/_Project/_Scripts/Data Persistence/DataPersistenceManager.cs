using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataPersistenceManager : Singleton<DataPersistenceManager>
{
    [Header("File Storage Configs")] 
    [SerializeField] private bool toLocal;
    [SerializeField] private string localDirectoryName = "/SaveFile";
    [SerializeField] private string directoryName = "/sdcard/MRSaveFile";
    [SerializeField] private string fileName = "SaveFile.json";
    
    private GameData _gameData;
    private List<IDataPersistence> _dataPersistenceObjects;
    private FileDataHandler _dataHandler;

    private void Start()
    {
        _dataHandler = new FileDataHandler(toLocal ? Application.dataPath + localDirectoryName : directoryName, fileName);
        _dataPersistenceObjects = FindAllDataPersistenceObject();
    }

    public void NewGame()
    {
        _gameData = new GameData();
    }
    
    public void LoadGame()
    {
        _gameData = _dataHandler.Load();
        
        if (_gameData == null) NewGame();
        
        foreach (IDataPersistence dataPersistenceObj in _dataPersistenceObjects)
        {
            dataPersistenceObj.LoadGame(_gameData);
        }
    }

    public void SaveGame()
    {
        if (_gameData == null) NewGame();
        
        foreach (IDataPersistence dataPersistenceObj in _dataPersistenceObjects)
        {
            dataPersistenceObj.SaveGame(ref _gameData);
        }
        
        _dataHandler.Save(_gameData);
    }
    
    private List<IDataPersistence> FindAllDataPersistenceObject()
    {
        return FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>().ToList();
    }
}
