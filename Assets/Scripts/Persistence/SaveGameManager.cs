using UnityEngine;

/// <summary>
/// Active save manager to save one piece of data to the disc.
/// Needs a tick to save. Keep this modular apart from the GameManager.
/// Able to become a component of an interactable object via MonoBehaviour. 
/// </summary>
public class SaveGameManager : MonoBehaviour
{
    public static SaveData gameData;

    private void Awake()
    {
        gameData = new SaveData();
        Persistence.OnLoadGame += LoadData;
    }

    public void DeleteData()
    {
        Persistence.DeleteSaveData();
    }

    public static void SaveData()
    {
        var gameSaveData = gameData;

        Persistence.Save(gameSaveData);
    }

    public static void LoadData(SaveData _gameData)
    {
        gameData = _gameData;
    }
}
