using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class WorkUI : MonoBehaviour
{
    public static WorkUI Instance;
    // 마우스 휠 스크롤, 선택 가능. 각각 하나씩만 선택 가능하게.

    // 돌 : StoneStorageManager에서 리스트 불러오기. 배열을 List로 옮기고 정렬 후 디스플레이.
    // 조각품 : SculptureTree에서 Unlock 리스트 불러오기. Unlock 상태값 옮기고 정렬 후 디스플레이.

    Canvas WorkCanvas;
    [SerializeField] GameObject StoneListArea;
    [SerializeField] GameObject SculptureListArea;

    [SerializeField] GameObject OriginStoneList;
    [SerializeField] GameObject OriginSculptureList;

    [SerializeField] UnityEngine.UI.Text StoneEmptyText;
    [SerializeField] UnityEngine.UI.Text SculptureEmptyText;

    [SerializeField] UnityEngine.UI.Text SelectedStoneText;
    [SerializeField] UnityEngine.UI.Text SelectedSculptureText;
    [SerializeField] UnityEngine.UI.Text SelectedToolText;

    int SelectedStoneID;
    int SelectedSculptureID;
    List<GameObject> tempList; // Instantiate 오브젝트 삭제용 임시 리스트


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        WorkCanvas = GetComponent<Canvas>();
        WorkCanvas.enabled = false;
        tempList = new List<GameObject>();
        SelectedStoneID = -1;
        SelectedSculptureID = -1;
    }

    private void Start()
    {
        StoneEmptyText.enabled = true;
        SculptureEmptyText.enabled = true;
    }

    public void OpenWorkUI()
    {
        PlayerInteract.Instance.SetCanInteract(false);

        QuickSlotManager.Instance.SetActive(false);

        Pause.Instance.OnPause();
        WorkCanvas.enabled = true;

        LoadData();
    }


    public void CloseWorkUI()
    {
        ClearData();
        Pause.Instance.OffPause();
        WorkCanvas.enabled = false;

        QuickSlotManager.Instance.SetActive(true);

        PlayerInteract.Instance.SetCanInteract(true);
    }


    public void SelectStone(int index)
    {
        SelectedStoneText.text = NameStrData.Instance.GetStoneName(index);
        SelectedStoneID = index + 101;
    }

    public void SelectSculpture(int index)
    {
        SelectedSculptureText.text = "Duck"; ///// 임시
        SelectedSculptureID = index;         ///// 임시
    }

    public void StartSculpture()
    {
        if (SelectedStoneID == -1)
        {
            Debug.Log("돌을 골라주세요");
            return;
        }
        if (SelectedSculptureID == -1)
        {
            Debug.Log("조각품을 골라주세요");
            return;
        }

        // 조각 실행
        // WorkManager에게 카메라 이동 및 UI 출력 시키기
        // 인벤토리 꽉 차있으면 로그 띄우고 Return 시키기
        // 돌 창고에서 돌 빼기

        Debug.Log("조각 시작");
    }


    private void LoadData()
    {
        if (PlayerManager.Instance.currentItem.itemtype != ItemType.Tool) return;
        int toolGrade = PlayerManager.Instance.currentItem.grade;

        int[] array = StoneStorageManager.Instance.GetStonesArrayfromStorage();

        // 추후 조각품 tree에서도 받아오기

        DisplayData(array, toolGrade);
    }

    
    private void DisplayData(int[] tempArray, int toolGrade)
    {
        bool isStoneEmpty = true;

        for (int i = 0; i < tempArray.Length; i++)
        {
            if (tempArray[i] != 0)
            {
                isStoneEmpty = false;
                break;
            }
        }

        if (!isStoneEmpty) // 돌 목록 비어있지 않으면
        {
            StoneEmptyText.enabled = false;
            StoneListArea.SetActive(true);

            for (int i = 0; i < tempArray.Length; i++)
            {
                if (tempArray[i] == 0) { continue; }

                GameObject L = Instantiate(OriginStoneList); // 버튼 생성
                L.transform.SetParent(StoneListArea.transform, false);

                Button btn = L.GetComponent<Button>();
                StoneSelectButton b = L.GetComponent<StoneSelectButton>();
                b.SaveStoneIndex(i);

                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(() => b.OnClick());
                UnityEngine.UI.Text t = L.GetComponentInChildren<UnityEngine.UI.Text>();

                L.SetActive(true);
                tempList.Add(L);

                string tempStr = NameStrData.Instance.GetStoneName(i) + " * " + tempArray[i].ToString();
                t.text = tempStr;
            }
        }
        else // 비어있으면
        {
            StoneEmptyText.enabled = true;
            StoneListArea.SetActive(false);
        }


        /////// 조각품 목록 불러오기
        SculptureEmptyText.enabled = false; ///임시


        SelectedToolText.text = NameStrData.Instance.GetToolGrade(toolGrade) + " Tool";
    }


    private void ClearData()
    {
        foreach (GameObject L in tempList)
        {
            if (L != null)
            {
                Destroy(L);
            }
        }

        tempList.Clear();

        SelectedStoneText.text = "";
        SelectedSculptureText.text = "";
        SelectedToolText.text = "";
        SelectedStoneID = -1;
    }
}
