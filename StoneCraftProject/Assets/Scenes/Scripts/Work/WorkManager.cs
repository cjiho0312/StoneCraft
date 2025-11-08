using System.Globalization;
using UnityEngine;

public class WorkManager : MonoBehaviour
{
    public static WorkManager Instance;
    [SerializeField] WorkUI workUI;
    [SerializeField] SculptingUI sculptingUI;
    [SerializeField] SculptingStoneDisplay sculptingStoneDisplay;

    [SerializeField] Item SculptureItem;
    int nextSculptureID;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        nextSculptureID = 1000;
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
        Debug.Log("도구 볼 수 없음");
    }

    public void StopSculpting()
    {
        sculptingUI.CloseSculptingUI(); // UI 끄기
        PlayerManager.Instance.CanSeeHoldingTool(true);
        Debug.Log("도구 볼 수 있음");
        CameraManager.Instance.OnMainCam();
        Pause.Instance.OffPause();
    }

    public void GetSculpture(string name, int value) // 조각품 만들기
    {
        // 프로토타입용
        Item S = Instantiate(SculptureItem);
        GameObject G = sculptingStoneDisplay.GetSculpturePrefab();

        S.holdingPrefab = G;
        S.itemName = "Sculpture";
        S.value = value;
        S.itemId = nextSculptureID++;
        
        Inventory.Instance.AddItem(S);
    }

}
