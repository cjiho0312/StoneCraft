using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class SculptingUI : MonoBehaviour
{
    // 돌 정보, 조각품 정보, 도구 정보 불러오기
    // 이에 따른 돌 매터리얼 변경, 단계별 프리팹 불러오기

    // 1. 돌 등장 애니메이션
    // 2. 클릭시 동그란 원 생기고, 마우스 떼면 정확도 체크
    // 3. 정확도에 따라 점수 매기기

    // 4. 일정 횟수 두드림이 끝나면 Work Manager에서 완성 UI 출력
    // 5. 인벤토리에 들어옴

    Canvas SculptingUICanvas;
    [SerializeField] SculptingGame game;

    int stoneID;
    int sculptureID;
    int toolGrade;

    int value; // 총 가치(가격)
    int stoneValue;
    int sculptureValue;
    int sculptureSkill;
    int SuccessRate;
    int decoValue;

    int currentStep; // 1~5단계 (돌 - 큰 덩어리 떼어낸 돌 - 어느정도 윤곽 생긴 돌 - 형태 잡힌 돌 - 마감처리 완료한 돌)

    GameObject t;
    [SerializeField] Light SculptureSpotLight;
    [SerializeField] Transform SculptureArea;
    [SerializeField] Transform ToolArea;
    [SerializeField] Item tool;
    Slider ProgressBar;

    private void Awake()
    {
        SculptingUICanvas = GetComponent<Canvas>();
        ProgressBar = GetComponentInChildren<Slider>();
    }

    private void Start()
    {
        game.gameObject.SetActive(false);
        SculptingUICanvas.enabled = false;
        SculptureSpotLight.enabled = false;
    }

    public void OpenSculptingUI(int StoneID, int SculptureID, int toolGrade)
    {
        InitData();

        stoneID = StoneID;
        sculptureID = SculptureID;
        this.toolGrade = toolGrade;

        // tool grade에 알맞은 도구를 인스턴스화해서 ToolArea에 넣어야 함.
        // 이건 프로토타입용 임시 코드
        t = Instantiate(tool.holdingPrefab);
        t.transform.parent = ToolArea;
        //

        AimSwitch.Instance.EmptyAim(true);
        SculptureSpotLight.enabled = true;
        SculptingUICanvas.enabled = true;
        game.gameObject.SetActive(true);
    }

    public void CloseSculptingUI()
    {
        game.gameObject.SetActive(false);
        InitData();
        AimSwitch.Instance.EmptyAim(false);

        SculptureSpotLight.enabled = false;
        SculptingUICanvas.enabled = false;
    }

    public void UpdateProgressBar()
    {
        ProgressBar.value = currentStep;
    }

    private void InitData()
    {
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
        ProgressBar.value = 0;

        if (t != null)
        {
            t.transform.SetParent(null);
            Destroy(t);
            t = null;
        }
    }
}
