using System.Globalization;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    public PlayerData savedPlayerData = new();

    private string path;
    private string fileName = "/save";

    private void Awake()
    {
        Instance = this;

        path = Application.persistentDataPath + fileName;
        Debug.Log(path);

        LoadPlayerData();
    }

    public void SavePlayerData()
    {
        string Pdata;

        if (PlayerManager.Instance != null)
        {
            PlayerManager.Instance.playerData.pos = PlayerMoveController.Instance.GetPlayerPosition(); // ������ �����ͼ� ��ġ���� ���� ����
            Pdata = JsonUtility.ToJson(PlayerManager.Instance.playerData); // �÷��̾� �Ŵ����� playerData�� ����
        }
        else
        {
            Pdata = JsonUtility.ToJson(savedPlayerData);
        }

        File.WriteAllText(path, Pdata);
        Debug.Log("Player data saved!");
    }

    public void LoadPlayerData()
    {
        if (!File.Exists(path))
        {
            SavePlayerData();
        }

        string Pdata = File.ReadAllText(path);

        savedPlayerData = JsonUtility.FromJson<PlayerData>(Pdata);
    }
}
