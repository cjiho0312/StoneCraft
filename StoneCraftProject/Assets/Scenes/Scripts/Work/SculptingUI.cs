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

    GameObject t; // 임시
    [SerializeField] Light SculptureSpotLight;
    [SerializeField] Transform SculptureArea;
    [SerializeField] Transform ToolArea;
    [SerializeField] Item tool;
    Animator ToolAnimator;
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

        // tool grade에 알맞은 도구를 인스턴스화해서 ToolArea에 넣어야 함.
        // 이건 프로토타입용 임시 코드
        t = Instantiate(tool.holdingPrefab);
        t.transform.parent = ToolArea;
        t.tag = "UIAnim";
        ToolAnimator = t.GetComponent<Animator>();
        //---

        AimSwitch.Instance.EmptyAim(true);
        SculptureSpotLight.enabled = true;
        SculptingUICanvas.enabled = true;
        game.gameObject.SetActive(true);

        game.GetData(StoneID, SculptureID, toolGrade);
    }


    public void PlayToolCarvingAnim()
    {
        ToolAnimator.SetTrigger("Carving");
    }

    public void CloseSculptingUI()
    {
        game.gameObject.SetActive(false);
        InitData();
        AimSwitch.Instance.EmptyAim(false);

        SculptureSpotLight.enabled = false;
        SculptingUICanvas.enabled = false;
    }

    public void UpdateProgressBar(int currentStep)
    {
        ProgressBar.value = currentStep;
    }

    private void InitData()
    {
        ProgressBar.value = 0;

        if (t != null)
        {
            t.transform.SetParent(null);
            ToolAnimator = null;
            Destroy(t);
            t = null;
        }
    }
}
