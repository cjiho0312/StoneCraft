using UnityEngine;
using System.Collections;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("�Ŵ��� ����")]
    [SerializeField] public SaveManager saveManager;
    public GameObject player;

    private bool isLoaded = false;  // �ߺ� �ε� ����

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadGameData());
    }


    // ���� ������ �ε�

    private IEnumerator LoadGameData()
    {
        yield return null; // �� ������ ��� (�� ������Ʈ �ʱ�ȭ �Ϸ� ���)

        if (saveManager == null)
        {
            Debug.LogError("[GameManager] SaveManager�� �������� �ʽ��ϴ�!");
            yield break;
        }

        if (player == null)
            player = GameObject.FindWithTag("Player");

        saveManager.LoadAll(player);
        isLoaded = true;
        Debug.Log("[GameManager] ���� ������ �ε� �Ϸ�");
    }


    // ���� ����

    public void SaveGame()
    {
        if (!isLoaded)
        {
            Debug.LogWarning("[GameManager] ���� �����Ͱ� �ε���� �ʾҽ��ϴ�. ���� ���");
            return;
        }

        saveManager.SaveAll(player);
        Debug.Log("[GameManager] ���� ���� �Ϸ�");
    }


    // ��� ���� (��: �� ��ȯ, ���� ��)

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
            SaveGame();
    }
}
