using UnityEngine;
using UnityEngine.UI;

public class QuickSlotManager : MonoBehaviour
{
    public static QuickSlotManager Instance;

    [Header("QuickSlot UI")]
    [SerializeField] private GameObject quickSlotPanel; // 퀵슬롯 패널
    [SerializeField] private Slot[] quickSlots;         // 퀵슬롯 슬롯 배열
    [SerializeField] private int selectedIndex = 0;     // 현재 선택된 슬롯

    [Header("Selection Highlight")]
    [SerializeField] private Image selectionHighlight;  // 선택 슬롯 하이라이트 이미지

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateSelectionUI();
        UpdatePlayerHand();
    }

    private void Update()
    {
        if (InventoryUI.Instance != null && InventoryUI.Instance.IsOpen()) return; // 인벤토리 열려있으면 무시

        if (PlayerManager.Instance.currentState == PlayerState.MINING ||
            PlayerManager.Instance.currentState == PlayerState.CRAFTING ||
            PlayerManager.Instance.currentState == PlayerState.NONE)
        {
            return;
        }
        // 마우스 스크롤로 선택
        float scroll = Input.mouseScrollDelta.y;
        if (scroll != 0)
        {
            if (scroll > 0) selectedIndex = (selectedIndex + 1) % quickSlots.Length;
            else selectedIndex = (selectedIndex - 1 + quickSlots.Length) % quickSlots.Length;

            UpdateSelectionUI();
            UpdatePlayerHand();
        }
    }

    public void UpdateSelectionUI()
    {
        if (selectionHighlight != null && quickSlots.Length > 0)
        {
            selectionHighlight.transform.position = quickSlots[selectedIndex].transform.position;
        }
    }

    private void UpdatePlayerHand()
    {
        var slot = GetSelectedSlot();
        if (slot != null && slot.item != null)
        {
            PlayerManager.Instance.SetCurrentItem(slot.item);
        }
        else
        {
            PlayerManager.Instance.SetCurrentItem(null);
        }
    }

    // 인벤토리 열림/닫힘에 따라 퀵슬롯 UI 활성화/비활성화
    public void SetActive(bool active)
    {
        if (quickSlotPanel != null)
            quickSlotPanel.SetActive(active);
    }

    public Slot GetSelectedSlot()
    {
        if (quickSlots.Length == 0) return null;
        return quickSlots[selectedIndex];
    }

    public void UpdateQuickSlotsFromInventory()
    {
        for (int i = 0; i < quickSlots.Length; i++)
        {
            Slot inventorySlot = Inventory.Instance.GetSlotByIndex(i);
            if (inventorySlot != null)
                quickSlots[i].SetItem(inventorySlot.item); // Slot.item을 그대로 전달받음.
            else
                quickSlots[i].SetItem(null);
        }

        UpdatePlayerHand();
    }
}
