using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SculptingGame : MonoBehaviour
{
    // 프로토타입용. 아마 튜토리얼 만들 때 재사용 할 수 있을 듯.

    // 작동 방식
    // 화면에 대고 마우스 오른쪽 버튼을 꾹 누르고 있으면 커서가 있는 위치에 체크용 원 생성
    // 이어 그 위에 커졌다 작아졌다 하는 게임용 원도 생성.
    // 화면에서 마우스를 떼어내면 체크용 원과 게임용 원의 크기를 비교.
    // 일정한 기준보다 차이가 크면 점수 깎고, 비슷하게 만들었으면 높은 점수 부여.

    [SerializeField] SculptingUI sculptingUI;

    public GameCircle gameCircle;
    public GameObject checkCircle;

    public Text resultText;
    public RectTransform uiParent; // Canvas

    private bool isClicking = false;
    private bool CanClick = true;

    private Coroutine currentCoroutine;

    int stoneID;
    int sculptureID;
    int toolGrade;

    int value; // 총 가치(가격)
    int stoneValue; // 돌 가치 (기본 가격)
    int sculptureValue; // 조각품 가치 (기본 가격)
    int sculptureSkill; // 조각품 숙련도
    int SuccessRate; // 성공률 (완성도)
    int decoValue; // 장식

    int currentStep; // 1~5단계 (돌 - 큰 덩어리 떼어낸 돌 - 어느정도 윤곽 생긴 돌 - 형태 잡힌 돌 - 마감처리 완료한 돌)


    public void GetData(int StoneID, int SculptureID, int toolGrade)
    {
        InitData();

        stoneID = StoneID;
        sculptureID = SculptureID;
        this.toolGrade = toolGrade;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && CanClick)
        {
            // 클릭 시 체크 원 생성
            checkCircle.SetActive(true);
            RectTransform checkRect = checkCircle.GetComponent<RectTransform>();
            checkRect.position = Input.mousePosition;
            // checkRect.sizeDelta = new Vector2( defaultSize, defaultSize );

            gameCircle.gameObject.SetActive(true);
            gameCircle.gameObject.GetComponent<RectTransform>().position = checkRect.position;
            isClicking = true;
        }

        if (Input.GetMouseButtonUp(0) && isClicking) // 마우스 떼면
        {
            CanClick = false;
            gameCircle.isStop = true;

            float gameSize = gameCircle.GetCurrentSize();
            float checkSize = checkCircle.GetComponent<RectTransform>().sizeDelta.x;

            float diff = Mathf.Abs(gameSize - checkSize) / (gameCircle.maxSize - gameCircle.minSize);
            ShowResult(diff);

            isClicking = false;
        }
    }

    public void ShowResult(float diff)
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        currentCoroutine = StartCoroutine(ShowResultCoroutine(diff));
        StartCoroutine(DisableCirclesAfterDelay());
    }

    private void Result(int index)
    {
        sculptingUI.PlayToolCarvingAnim();



    }

    private IEnumerator ShowResultCoroutine(float diff)
    {
        string result;

        if (diff < 0.03f)
        { 
            result = "Perfect!";
            Result(0);
        }
        else if (diff < 0.1f)
        { 
            result = "Good!";
            Result(1);
        }
        else
        {
            result = "Miss!";
            Result(2);
        }

        resultText.text = result;
        resultText.enabled = true;

        yield return new WaitForSecondsRealtime(1f);

        resultText.enabled = false;

        currentCoroutine = null;
    }

    private IEnumerator DisableCirclesAfterDelay()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        checkCircle.SetActive(false);
        gameCircle.gameObject.SetActive(false);
        CanClick = true;
        gameCircle.isStop = false;
    }



    private void InitData()
    {
        resultText.enabled = false;

        stoneID = -1;
        sculptureID = -1;
        toolGrade = -1;

        value = 0;
        stoneValue = 0;
        sculptureValue = 0;
        sculptureSkill = 0;
        SuccessRate = 0;
        decoValue = 0;

        currentStep = 0;
    }
}
