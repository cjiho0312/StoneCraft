using UnityEngine;

public class WorkManager : MonoBehaviour
{
    public static WorkManager Instance;
    [SerializeField] WorkUI workUI;
    [SerializeField] SculptingUI sculptingUI;

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

    public void StartSculpting(int StoneID, int SculptureID, int toolGrade)
    {
        workUI.CloseWorkUI();
        Pause.Instance.OnPause();
        StoneStorageManager.Instance.RemoveStoneInStorage(StoneID); // 돌 삭제
        CameraManager.Instance.OnSculptingCam(); // 카메라 옮기기
        sculptingUI.OpenSculptingUI(StoneID, SculptureID, toolGrade); // UI 출력
        PlayerManager.Instance.CanSeeHoldingTool(false);
    }

    public void StopSculpting()
    {
        sculptingUI.CloseSculptingUI(); // UI 끄기
        PlayerManager.Instance.CanSeeHoldingTool(true);
        CameraManager.Instance.OnMainCam();
        Pause.Instance.OffPause();
    }

    public void UpdateSculptingUI()
    {
        sculptingUI.UpdateProgressBar();
    }

}
