using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private readonly string _dataDirPath;
    private readonly string _dataFileName;
    
    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        _dataDirPath = dataDirPath;
        _dataFileName = dataFileName;
    }

    public void Save(GameData data, bool logError = true)
    {
        string fullPath = Path.Combine(_dataDirPath, _dataFileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(data, true);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
            if(logError)
                DataPersistenceManager.Instance.LogSave("Save successful");
        }
        catch (Exception e)
        {
            if (logError)
                DataPersistenceManager.Instance.LogSave("Save failed");
        }
    }

    public GameData Load(bool logError = true)
    {
        string fullPath = Path.Combine(_dataDirPath, _dataFileName);

        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
                if (logError)
                    DataPersistenceManager.Instance.LogLoad("Load successful");
            }
            catch (Exception e)
            {
                if(logError)
                    DataPersistenceManager.Instance.LogLoad("Load failed");
            }
        }
        return loadedData;
    }
}
