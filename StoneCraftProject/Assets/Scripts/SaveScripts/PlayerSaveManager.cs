using System.IO;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int money;

    public int toolGrade;       // ���� ���
    public int pickaxeGrade;    // ��� ���

    public float posX;
    public float posY;
    public float posZ;
}

public class PlayerSaveManager : MonoBehaviour
{

    private string filePath; // ���� ���
    public PlayerData playerData = new PlayerData();

    private void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, "PlayerData.json");
    }

    // Save (����)

    public void Save()
    {
        string json = JsonUtility.ToJson(playerData, true);
        File.WriteAllText(filePath, json);
        Debug.Log($"[PlayerSave] ���� �Ϸ� �� {filePath}");
    }

    // Load (�ҷ�����)

    public void Load()
    {
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("[PlayerSave] ���� ���� ����. �� ������ ����");
            playerData = new PlayerData();
            return;
        }

        string json = File.ReadAllText(filePath);
        playerData = JsonUtility.FromJson<PlayerData>(json);
        Debug.Log($"[PlayerSave] �ҷ����� �Ϸ� �� {filePath}");
    }

    // ���� ������Ʈ�� ���� / �ݿ�

    public void ApplyToPlayer(GameObject player)
    {
        var playerM = PlayerManager.Instance;

        player.transform.position = new Vector3(playerData.posX, playerData.posY, playerData.posZ);
        playerM.ChangePlayerState(PlayerState.IDLE);

        playerM.money = playerData.money;
        playerM.toolGrade = playerData.toolGrade;
        playerM.pickaxeGrade = playerData.pickaxeGrade;
    }


    // ���� ���� ����� ����

    public void CaptureFromPlayer(GameObject player)
    {
        var playerM = PlayerManager.Instance;

        playerData.posX = player.transform.position.x;
        playerData.posY = player.transform.position.y;
        playerData.posZ = player.transform.position.z;

        // ��: PlayerController�� �ִٸ�
        // var controller = player.GetComponent<PlayerController>();

        playerData.money = playerM.money;
        playerData.toolGrade = playerM.toolGrade;
        playerData.pickaxeGrade = playerM.pickaxeGrade;
    }
}