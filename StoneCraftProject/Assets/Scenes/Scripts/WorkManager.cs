using UnityEngine;

public class WorkManager : MonoBehaviour
{
    public static WorkManager Instance;
    [SerializeField] WorkUI workUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void StartWork()
    {
        workUI.OpenWorkUI();
    }
}
