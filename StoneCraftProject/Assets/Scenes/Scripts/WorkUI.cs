using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class WorkUI : MonoBehaviour
{
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

    
    List<GameObject> tempList; // Instantiate 오브젝트 삭제용 임시 리스트


    string[] StoneNameString = { "Limestone", ".", ".", ".", ".", "." };
    string[] ToolGradeString = { "Wood", "Stone", "Iron", "Diamond" };


    private void Awake()
    {
        WorkCanvas = GetComponent<Canvas>();
        WorkCanvas.enabled = false;
        tempList = new List<GameObject>();
    }


    public void OpenWorkUI()
    {
        PlayerInteract.Instance.SetCanInteract(false);
        Pause.Instance.OnPause();
        WorkCanvas.enabled = true;

        LoadData();
    }


    public void CloseWorkUI()
    {
        ClearData();
        Pause.Instance.OffPause();
        WorkCanvas.enabled = false;
        PlayerInteract.Instance.SetCanInteract(true);
    }


    public void SelectStone(int index)
    {
        // tempList[index]
        // 버튼 스크립트에서 본인의 인덱스와 stoneID 가져오기
    }



    public void SelectSculpture(int index)
    {

    }


    private void LoadData()
    {
        int toolGrade = 0;

        int[] array = StoneStorageManager.Instance.GetStonesArrayfromStorage();

        // 추후 조각품 tree에서도 받아오기
        // 현재 가지고 있는 도구 등급도 받아오기

        Debug.Log("도구 grade : " + PlayerManager.Instance.currentItem.grade);

        


        DisplayData(array, toolGrade);
    }

    
    private void DisplayData(int[] tempArray, int toolGrade) // 보유한 돌 목록, 보유한 조각품, 도구 등급 받아서 Display
    {
        if (tempArray.Length != 0) // 돌 목록 비어있지 않으면
        {
            StoneEmptyText.enabled = false;
            StoneListArea.SetActive(true);

            for (int i = 0; i < tempArray.Length; i++)
            {
                if (tempArray[i] == 0) { continue; }

                GameObject L = Instantiate(OriginStoneList);
                L.SetActive(true);
                L.transform.SetParent(OriginStoneList.GetComponentInParent<Transform>(), false);

                tempList.Add(L);

                // 버튼 스크립트 만들어서 본인의 리스트 인덱스와 stoneID를 들고 있게끔 하기

                UnityEngine.UI.Text t = L.GetComponentInChildren<UnityEngine.UI.Text>();

                string tempStr = StoneNameString[i] + " * " + tempArray[i].ToString();
                t.text = tempStr;
            }
        }
        else // 비어있으면
        {
            StoneEmptyText.enabled = true;
            StoneListArea.SetActive(false);
        }


        SelectedToolText.text = ToolGradeString[toolGrade] + " Tool";
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
        
    }
}
