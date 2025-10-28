using System.Globalization;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [Header("���� ���̺� �Ŵ���")]
    [SerializeField] public PlayerSaveManager playerSave;

    private void Awake()
    {

    }

    // ��ü ����

    public void SaveAll(GameObject player)
    {
        playerSave.CaptureFromPlayer(player);
        playerSave.Save();

        Debug.Log("[SaveManager] ��� ������ ���� �Ϸ�");
    }

    // ��ü �ҷ�����
    public void LoadAll(GameObject player)
    {
        playerSave.Load();
        playerSave.ApplyToPlayer(player);

        Debug.Log("[SaveManager] ��� ������ �ҷ����� �Ϸ�");
    }
}
