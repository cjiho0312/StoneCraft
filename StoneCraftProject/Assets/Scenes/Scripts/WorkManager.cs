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
        // UI ���� �� �÷��̾� ���� ����
    }
}
