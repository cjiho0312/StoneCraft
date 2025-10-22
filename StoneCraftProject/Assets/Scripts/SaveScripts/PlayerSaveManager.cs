using System.IO;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int money;

    public int toolGrade;       // 도구 등급
    public int pickaxeGrade;    // 곡괭이 등급

    public float posX;
    public float posY;
    public float posZ;
}

public class PlayerSaveManager : MonoBehaviour
{

    private string filePath; // 저장 경로
    public PlayerData playerData = new PlayerData();

    private void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, "PlayerData.json");
    }

    // Save (저장)

    public void Save()
    {
        string json = JsonUtility.ToJson(playerData, true);
        File.WriteAllText(filePath, json);
        Debug.Log($"[PlayerSave] 저장 완료 → {filePath}");
    }

    // Load (불러오기)

    public void Load()
    {
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("[PlayerSave] 저장 파일 없음. 새 데이터 생성");
            playerData = new PlayerData();
            return;
        }

        string json = File.ReadAllText(filePath);
        playerData = JsonUtility.FromJson<PlayerData>(json);
        Debug.Log($"[PlayerSave] 불러오기 완료 → {filePath}");
    }

    // 게임 오브젝트에 적용 / 반영

    public void ApplyToPlayer(GameObject player)
    {
        var playerM = PlayerManager.Instance;

        player.transform.position = new Vector3(playerData.posX, playerData.posY, playerData.posZ);
        playerM.ChangePlayerState(PlayerState.IDLE);

        playerM.money = playerData.money;
        playerM.toolGrade = playerData.toolGrade;
        playerM.pickaxeGrade = playerData.pickaxeGrade;
    }


    // 현재 상태 저장용 헬퍼

    public void CaptureFromPlayer(GameObject player)
    {
        var playerM = PlayerManager.Instance;

        playerData.posX = player.transform.position.x;
        playerData.posY = player.transform.position.y;
        playerData.posZ = player.transform.position.z;

        // 예: PlayerController가 있다면
        // var controller = player.GetComponent<PlayerController>();

        playerData.money = playerM.money;
        playerData.toolGrade = playerM.toolGrade;
        playerData.pickaxeGrade = playerM.pickaxeGrade;
    }
}