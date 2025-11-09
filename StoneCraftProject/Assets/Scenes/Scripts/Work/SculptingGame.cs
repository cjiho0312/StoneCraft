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
    [SerializeField] SculptingStoneDisplay stoneDisplay;

    [SerializeField] ParticleSystem SculptingEffect;

    public GameCircle gameCircle;
    public GameObject checkCircle;

    public Text resultText;
    public RectTransform uiParent; // Canvas

    private bool isClicking = false;
    private bool CanClick = true;
    private bool EndSculpting = false;

    private Coroutine currentResultTextCoroutine;

    int stoneID;
    int sculptureID;
    int toolGrade;

    int value; // 총 가치(가격)
    int stoneValue; // 돌 가치 (기본 가격)
    int sculptureValue; // 조각품 가치 (기본 가격)
    int SuccessRate; // 성공률 (완성도) 10에서부터 시작
    // int sculptureSkill; // 조각품 숙련도
    // int decoValue; // 장식

    int currentStep; // 0~3(4)단계 (돌 - 큰 덩어리 떼어낸 돌 - 어느정도 윤곽 생긴 돌 - 형태 잡힌 돌 - 마감처리 완료한 돌)
    float StepForBar = 0.3333f; // 프로토타입 용

    public void Start()
    {
        SculptingEffect.gameObject.SetActive(true);
        SculptingEffect.Stop();
        var p = SculptingEffect.main;
        p.useUnscaledTime = true;
    }

    public void GetData(int StoneID, int SculptureID, int toolGrade)
    {
        InitData();

        stoneID = StoneID;
        sculptureID = SculptureID;
        this.toolGrade = toolGrade;
        
        //프로토타입용
        stoneValue = 5;
        sculptureValue = 5;
        //----

        stoneDisplay.DisplaySculpture(currentStep);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && CanClick && !EndSculpting)
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

            
            StartShowResult(diff);
            isClicking = false;
        }
    }

    public void StartShowResult(float diff) // 마우스 떼면 결과 출력 시작
    {
        CheckDiffResult(diff);
        StartCoroutine(DisableCirclesAfterDelay());
    }


    private void CheckDiffResult(float diff) // 원 크기 비교 함수
    {
        if (currentResultTextCoroutine != null)
        {
            StopCoroutine(currentResultTextCoroutine);
        }

        string result;
        int index = 0;

        if (diff < 0.03f)
        {
            result = "Perfect!";
            index = 2;
        }
        else if (diff < 0.1f)
        {
            result = "Good!";
            index = 1;
        }
        else
        {
            result = "Bad!";
        }

        currentResultTextCoroutine = StartCoroutine(ShowResultCoroutine(result));
        StartCoroutine(Result(index));
    }


    private IEnumerator ShowResultCoroutine(string ResultStr) // 결과 텍스트 출력
    {
        resultText.text = ResultStr;
        resultText.enabled = true;

        yield return new WaitForSecondsRealtime(1f);

        resultText.text = "";
        resultText.enabled = false;
        currentResultTextCoroutine = null;
    }

    private IEnumerator DisableCirclesAfterDelay() // 망치질 후 게임 딜레이
    {
        yield return new WaitForSecondsRealtime(0.5f);
        checkCircle.SetActive(false);
        gameCircle.gameObject.SetActive(false);
        CanClick = true;
        gameCircle.isStop = false;
    }


    private IEnumerator Result(int index) // 결과에 따라 정확도 수정, 완성 확인
    {
        sculptingUI.PlayToolCarvingAnim();


        if (index == 0) // Bad
        {
            SuccessRate -= 5;
        }
        else if (index == 1) // Good
        {
            SuccessRate -= 1;
        }
        else if (index == 2) // Perfect
        {

        }

        yield return new WaitForSecondsRealtime(0.3f); // 애니메이션 기다려주기
        PlayEffect();

        if (SuccessRate <= 0)
        {
            // 망가짐
            Debug.Log("망가졌습니다!");
            CanClick = false;
            EndSculpting = true;

            if (currentResultTextCoroutine != null)
            {
                StopCoroutine(currentResultTextCoroutine);
                currentResultTextCoroutine = null;
            }

            resultText.text = "";
            resultText.enabled = false;

            CanClick = false;

            StartCoroutine(DisplayFinalResult(0));

            yield break;
        }

        sculptingUI.UpdateProgressBar(StepForBar);
        stoneDisplay.DisplaySculpture(++currentStep);

        if (currentStep == 3)
        {
            // 점수 계산 및 완성
            Debug.Log("완성했습니다!");
            EndSculpting = true;

            if (currentResultTextCoroutine != null)
            {
                StopCoroutine(currentResultTextCoroutine);
                currentResultTextCoroutine = null;
            }

            resultText.text = "";
            resultText.enabled = false;

            CanClick = false;

            calculateValue();



            StartCoroutine(DisplayFinalResult(1));
        }

    }


    private void calculateValue()
    {
        //프로토타입용 가치 계산
        value = (int)((stoneValue + sculptureValue) * (SuccessRate * 0.1f));
    }

    private IEnumerator DisplayFinalResult(int index)
    {
        // 화면 보여주기
        PlayerManager.Instance.CanSeeHoldingTool(false);
        
        if (index == 0)
        {
            sculptingUI.OpenResultUI(0);
            yield return new WaitForSecondsRealtime(1f);
            WorkManager.Instance.StopSculpting();
        }

        else if (index == 1)
        {
            sculptingUI.OpenResultUI(1);
            stoneDisplay.RotateSculpture();
            yield return new WaitForSecondsRealtime(3f);
            WorkManager.Instance.GetSculpture("Sculpture", value);
            WorkManager.Instance.StopSculpting();
        }
    }
    public void StopGame()
    {
        InitData();
    }

    private void PlayEffect()
    {
        SculptingEffect.Play();
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
        //sculptureSkill = 0;
        SuccessRate = 10;
        //decoValue = 0;

        currentStep = 0;
        EndSculpting = false;
        isClicking = false;
        CanClick = true;
        stoneDisplay.StopDisplay();
    }
}
