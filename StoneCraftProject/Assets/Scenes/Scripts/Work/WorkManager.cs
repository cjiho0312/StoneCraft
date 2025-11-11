using System.Globalization;
using Unity.VisualScripting;
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
    }

    public void StopSculpting()
    {
        sculptingUI.CloseSculptingUI(); // UI 끄기
        PlayerManager.Instance.CanSeeHoldingTool(true);
        CameraManager.Instance.OnMainCam();
        Pause.Instance.OffPause();
    }

    public void GetSculpture(string name, int value, int stoneID) // 조각품 만들기
    {
        // 프로토타입용
        Item S = Instantiate(SculptureItem);
        GameObject G = sculptingStoneDisplay.GetSculpturePrefab(stoneID);

        S.holdingPrefab = G;
        S.holdingPrefab.SetActive(true);
        S.itemName = "Sculpture";
        S.value = value;
        S.itemId = nextSculptureID++;

        Inventory.Instance.AddSculptureItem(S);

        GuideTextManager.Instance.MakeGuide(GuideSub.GETITEM, "Sculpture + 1");
    }
}
