using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : MonoSingleton<SaveManager>
{
    private string SAVE_PATH = "";
    private string SAVE_FILENAME = "/SaveFile.json";
    private string default_save = @"{
    ""money"": 0,
}";

    private void Awake()
    {
        SAVE_PATH = Application.dataPath + "/Json";
        if (!Directory.Exists(SAVE_PATH))
        {
            Directory.CreateDirectory(SAVE_PATH);
            File.WriteAllText(SAVE_PATH + SAVE_FILENAME, default_save, System.Text.Encoding.UTF8);
        }
        LoadFromJson();
    }

    private void LoadFromJson()
    {
        if(File.Exists(SAVE_PATH + SAVE_FILENAME))
        {
            string json = File.ReadAllText(SAVE_PATH + SAVE_FILENAME);
            Debug.Log("Loading Complete!");
        }
    }

    public void Save(string saveFileName)
    {
        if (!Directory.Exists(SAVE_PATH))
        {
            Directory.CreateDirectory(SAVE_PATH);
            Debug.Log("CreateNewSaveFile");
        }
        //string saveJson = JsonUtility.ToJson(GameManager.Instance.CurrentGameData);
        string saveFilePath = SAVE_PATH + saveFileName + ".json";
        //File.WriteAllText(saveFilePath, saveJson);
        Debug.Log("세이브 완료!");
    }

}
