using System.Globalization;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [Header("하위 세이브 매니저")]
    [SerializeField] public PlayerSaveManager playerSave;

    private void Awake()
    {

    }

    // 전체 저장

    public void SaveAll(GameObject player)
    {
        playerSave.CaptureFromPlayer(player);
        playerSave.Save();

        Debug.Log("[SaveManager] 모든 데이터 저장 완료");
    }

    // 전체 불러오기
    public void LoadAll(GameObject player)
    {
        playerSave.Load();
        playerSave.ApplyToPlayer(player);

        Debug.Log("[SaveManager] 모든 데이터 불러오기 완료");
    }
}
