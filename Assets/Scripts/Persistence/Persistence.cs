// fullPath name: 
/// <see href="link">https://docs.unity3d.com/ScriptReference/Application-persistentDataPath.html</see>

using System;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public static class Persistence
{
    public static UnityAction OnSaveGame;
    public static UnityAction<SaveData> OnLoadGame;

    private static string directory = "/SaveData/";
    private static String timeStamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
    // private static String filename = $"Slot_{timeStamp}.save";
    private static String filename = $"Slot.save";

    public static bool Save(SaveData gameData)
    {
        OnSaveGame?.Invoke();
        // Windows Editor and Standalone Player: 
        // Application.persistentDataPath usually points to %userprofile%\AppData\LocalLow\<companyname>\<productname>
        string dir = Application.persistentDataPath + directory;

        // copy dir to clipboard
        GUIUtility.systemCopyBuffer = dir;

        if (!Directory.Exists(dir))
        {
            // local load folder
            Directory.CreateDirectory(dir);
        }

        string writeJson = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(dir + filename, writeJson);

        Debug.Log("> Game saving ...");

        return true;
    }

    public static SaveData Load()
    {
        string fullPath = Application.persistentDataPath + directory + filename;
        SaveData gameData = new SaveData();

        if (File.Exists(fullPath))
        {
            string readJson = File.ReadAllText(fullPath);
            gameData = JsonUtility.FromJson<SaveData>(readJson);
            Debug.Log($"Data loaded from file: {fullPath}.");
            OnLoadGame?.Invoke(gameData);
        }
        else
        {
            Debug.Log($"> Non-existent save file: {fullPath}.");
        }

        return gameData;
    }

    public static void DeleteSaveData()
    {
        string fullPath = Application.persistentDataPath + directory + filename;

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }
    }
}
