using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;
using TMPro;

    [System.Serializable]
    class SaveData
    {
    public string playerName;
        public int highScore;
    }

    [System.Serializable]
    class SaveLastName
    {
        public string lastPlayerName;
    }
public class NameManager : MonoBehaviour
{
    public static NameManager instance;
    public string lastPlayerName;
    public string playerName;
    public int highScore;
    // Start is called before the first frame update
    private void Awake()
    {
        // start of new code
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        instance = this;
        DontDestroyOnLoad(gameObject);
        LoadData();
    }


    public void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.playerName = playerName;
        data.highScore = highScore;

        string json = JsonUtility.ToJson(data);
        string path = Application.persistentDataPath + "/savefile.json";


        File.WriteAllText(path, json);
    }
    public void SaveLastNameScore()
    {
        SaveLastName data = new SaveLastName();
        data.lastPlayerName = lastPlayerName;
        string json = JsonUtility.ToJson(data);
        string path = Application.persistentDataPath + "/savelastnamefile.json";

        File.WriteAllText(path, json);
    }
    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        string lastNamePath = Application.persistentDataPath + "/savelastnamefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerName = data.playerName;
            highScore = data.highScore;

        }
        if (File.Exists(lastNamePath))
        {
            string json = File.ReadAllText(lastNamePath);
            SaveLastName data = JsonUtility.FromJson<SaveLastName>(json);
            lastPlayerName = data.lastPlayerName;

        }
        else 
        {
            Debug.LogWarning("Save file not found.");
        }
    }
}
