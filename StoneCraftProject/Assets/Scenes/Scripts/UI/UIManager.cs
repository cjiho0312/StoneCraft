using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] Canvas PauseCanvas;
    [SerializeField] Canvas AimCanvas;
    [SerializeField] Canvas InventoryCanvas;
    [SerializeField] Canvas QuickSlotCanvas;
    [SerializeField] Canvas WorkCanvas;
    [SerializeField] Canvas StoneStorageLogCanvas;
    [SerializeField] Canvas SculptingCanvas;
    [SerializeField] Canvas GuideCanvas;

    PauseUI pauseUI;
    InventoryUI inventoryUI;
    StoneStorageLogUI stoneStorageLogUI;
    WorkUI workUI;


    bool isOpenPause;

    public bool IsPaused { get { return isOpenPause; } }

    void Awake()
    {
        PauseCanvas.gameObject.SetActive(true);
        AimCanvas.gameObject.SetActive(true);
        InventoryCanvas.gameObject.SetActive(true);
        QuickSlotCanvas.gameObject.SetActive(true);
        WorkCanvas.gameObject.SetActive(true);
        StoneStorageLogCanvas.gameObject.SetActive(true);
        SculptingCanvas.gameObject.SetActive(true);
        GuideCanvas.gameObject.SetActive(true);

        pauseUI = PauseCanvas.GetComponent<PauseUI>();
        inventoryUI = InventoryCanvas.GetComponent<InventoryUI>();
        stoneStorageLogUI = StoneStorageLogCanvas.GetComponent<StoneStorageLogUI>();
        workUI = WorkCanvas.GetComponent<WorkUI>();

        isOpenPause = false;
    }

    bool isNotOpenAnyUI()
    {
        if (PauseCanvas.enabled || InventoryCanvas.enabled || StoneStorageLogCanvas.enabled) // 추후 workCanvas도 추가해야함
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    void Update()
    {
        if (MineManager.Instance.GetIsMining && !isOpenPause)
        {
            if (Input.anyKeyDown)
            {
                MineManager.Instance.StopMining();
            }
        }

        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            // 다른 창 열려있으면 먼저 닫기
            if (SculptingCanvas.enabled)
            {
                WorkManager.Instance.StopSculpting();
            }
            else if (InventoryCanvas.enabled)
            {
                inventoryUI.CloseInvenUI();
            }
            else if (WorkCanvas.enabled)
            {
                workUI.CloseWorkUI();
            }
            else if (StoneStorageLogCanvas.enabled)
            {
                stoneStorageLogUI.CloseStoneStorageLogUI();
            }

            // 다른 열려있는 창 없으면 Pause 메뉴 출격
            else if (!PauseCanvas.enabled)
            {
                isOpenPause = true;
                pauseUI.OpenPauseMenu();
            }
            else if (PauseCanvas.enabled)
            {
                isOpenPause = false;
                pauseUI.ClosePauseMenu();
            }
        }

        else if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("Press I");

            if (isNotOpenAnyUI())
            {
                inventoryUI.OpenInvenUI();
                Debug.Log("Open Inven");
            }
            else if (InventoryCanvas.enabled)
            {
                inventoryUI.CloseInvenUI();
            }
        }

    }
}
