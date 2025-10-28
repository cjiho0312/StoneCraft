using UnityEngine;
using System.Collections;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("매니저 연결")]
    [SerializeField] public SaveManager saveManager;
    public GameObject player;

    private bool isLoaded = false;  // 중복 로드 방지

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


    // 게임 데이터 로드

    private IEnumerator LoadGameData()
    {
        yield return null; // 한 프레임 대기 (씬 오브젝트 초기화 완료 대기)

        if (saveManager == null)
        {
            Debug.LogError("[GameManager] SaveManager가 존재하지 않습니다!");
            yield break;
        }

        if (player == null)
            player = GameObject.FindWithTag("Player");

        saveManager.LoadAll(player);
        isLoaded = true;
        Debug.Log("[GameManager] 게임 데이터 로드 완료");
    }


    // 게임 저장

    public void SaveGame()
    {
        if (!isLoaded)
        {
            Debug.LogWarning("[GameManager] 아직 데이터가 로드되지 않았습니다. 저장 취소");
            return;
        }

        saveManager.SaveAll(player);
        Debug.Log("[GameManager] 게임 저장 완료");
    }


    // 긴급 저장 (예: 씬 전환, 종료 시)

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
