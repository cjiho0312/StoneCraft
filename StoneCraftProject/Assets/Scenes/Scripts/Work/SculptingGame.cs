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

    public GameCircle gameCircle;
    public GameObject checkCircle;

    public Text resultText;
    public RectTransform uiParent; // Canvas

    private bool isClicking = false;

    private Coroutine currentCoroutine;

    private void Start()
    {
        resultText.enabled = false;
    }

    void Update()
    {
        Vector3 mousePos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(uiParent, Input.mousePosition, null, out mousePos);

        if (Input.GetMouseButtonDown(0))
        {
            // 클릭 시 체크 원 생성

            checkCircle.SetActive(true);
            checkCircle.transform.position = Input.mousePosition;
            checkCircle.transform.localScale = Vector3.one;

            gameCircle.gameObject.SetActive(true);
            gameCircle.SetCurrentScaleMax();
            gameCircle.gameObject.transform.position = checkCircle.transform.position;
            isClicking = true;
        }

        if (Input.GetMouseButtonUp(0) && isClicking) // 마우스 떼면
        {
            float gameScale = gameCircle.GetCurrentScale();
            float checkScale = checkCircle.transform.localScale.x;

            float diff = Mathf.Abs(gameScale - checkScale);
            ShowResult(diff);

            checkCircle.SetActive(false);
            gameCircle.gameObject.SetActive(false);
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
    }


    private IEnumerator ShowResultCoroutine(float diff)
    {
        string result;

        if (diff < 0.1f)
            result = "Perfect!";
        else if (diff < 0.2f)
            result = "Good!";
        else
            result = "Miss!";

        resultText.text = result;
        resultText.enabled = true;

        yield return new WaitForSeconds(1f);

        resultText.enabled = false;

        currentCoroutine = null;
    }
}
