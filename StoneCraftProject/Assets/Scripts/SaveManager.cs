using System.Globalization;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    public PlayerData savedPlayerData = new();

    private string path;
    private string fileName = "/save.json";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        path = Application.persistentDataPath + fileName;
        Debug.Log(path);
    }

    void Start()
    {
        LoadPlayerData();
    }

    public void SavePlayerData()
    {
        if (PlayerManager.Instance == null)
        {
            Debug.LogError("Save failed: PlayerManager not found!");
            return;
        }

        PlayerData temp = PlayerManager.Instance.playerData;
        Vector3 pos = PlayerMoveController.Instance.GetPlayerPosition();
        temp.posX = pos.x;
        temp.posY = pos.y;
        temp.posZ = pos.z;

        string json = JsonUtility.ToJson(temp, true);
        File.WriteAllText(path, json);

        savedPlayerData = temp;

        Debug.Log("Player data saved!");
    }

    public void LoadPlayerData()
    {
        if (!File.Exists(path))
        {
            Debug.LogWarning("No save file found. Creating a new one...");
            SavePlayerData();
            return;
        }

        string json = File.ReadAllText(path);
        savedPlayerData = JsonUtility.FromJson<PlayerData>(json);

        Debug.Log("LoadPlayerData()");
        Debug.Log("Player data : " + savedPlayerData.posX + " " + savedPlayerData.posY + " " + savedPlayerData.posZ);
    }

}
