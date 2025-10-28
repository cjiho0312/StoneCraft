using UnityEngine;
using UnityEngine.UI;

public class QuickSlotManager : MonoBehaviour
{
    public static QuickSlotManager Instance;

    [Header("QuickSlot UI")]
    [SerializeField] private GameObject quickSlotPanel; // ������ �г�
    [SerializeField] private Slot[] quickSlots;         // ������ ���� �迭
    [SerializeField] private int selectedIndex = 0;     // ���� ���õ� ����

    [Header("Selection Highlight")]
    [SerializeField] private Image selectionHighlight;  // ���� ���� ���̶���Ʈ �̹���

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
        if (InventoryUI.Instance != null && InventoryUI.Instance.IsOpen()) return; // �κ��丮 ���������� ����

        if (PlayerManager.Instance.currentState == PlayerState.MINING ||
            PlayerManager.Instance.currentState == PlayerState.CRAFTING ||
            PlayerManager.Instance.currentState == PlayerState.NONE)
        {
            return;
        }
        // ���콺 ��ũ�ѷ� ����
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

    // �κ��丮 ����/������ ���� ������ UI Ȱ��ȭ/��Ȱ��ȭ
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
                quickSlots[i].SetItem(inventorySlot.item); // Slot.item�� �״�� ���޹���.
            else
                quickSlots[i].SetItem(null);
        }

        UpdatePlayerHand();
    }
}
