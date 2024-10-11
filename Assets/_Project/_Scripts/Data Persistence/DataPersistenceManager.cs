using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DataPersistenceManager : Singleton<DataPersistenceManager>
{
    [Header("File Storage Configs")] 
    [SerializeField] private bool toLocal;
    [SerializeField] private string localDirectoryName = "/SaveFile";
    [SerializeField] private string directoryName = "/MRSaveFile";
    [SerializeField] private string fileName = "SaveFile.json";
    
    private GameData _gameData;
    private List<IDataPersistence> _dataPersistenceObjects;
    private FileDataHandler _dataHandler;

    public TMP_Text latestSave;
    public TMP_Text saveLog;
    public TMP_Text loadLog;

    public Button saveButton;
    public Button loadButton;

    private void Awake()
    {
        saveButton.onClick.AddListener(SaveGame);
        loadButton.onClick.AddListener(LoadGame);
    }

    private void Start()
    {
        _dataHandler = new FileDataHandler(toLocal ? Application.dataPath + localDirectoryName : Application.persistentDataPath + directoryName, fileName);
        _dataPersistenceObjects = FindAllDataPersistenceObject();
        
        _gameData = _dataHandler.Load(false);

        if (_gameData != null)
        {
            latestSave.text = "Latest: " + _gameData.timeStamp;
            
            LogLoad("Load file available");
        }
        else
        {
            LogLoad("Load file not available");
            latestSave.text = "No file save";
        }
    }

    public void LogSave(string error)
    {
        saveLog.text = "[Error] " + error;
    }
    
    public void LogLoad(string error)
    {
        loadLog.text = "[Error] " + error;
    }
    
    public void NewGame()
    {
        _gameData = new GameData();
    }
    
    public void LoadGame()
    {
        _gameData = _dataHandler.Load();

        if (_gameData == null) return;

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

        _gameData.timeStamp = DateTime.Now.ToString("H:mm:ss dd/MM/yyyy");
        _dataHandler.Save(_gameData);
        
        latestSave.text = "Latest: " + _gameData.timeStamp;
    }
    
    private List<IDataPersistence> FindAllDataPersistenceObject()
    {
        return FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>().ToList();
    }
}
