using UnityEngine;

public class WorkManager : MonoBehaviour
{
    public static WorkManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void StartWork()
    {
        // UI 열기 및 플레이어 상태 변경
    }
}
