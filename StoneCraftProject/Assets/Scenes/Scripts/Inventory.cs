using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    [SerializeField] private int maxQuantity = 20;
    [SerializeField] private List<Slot> slotList = new List<Slot>(); // ui�� ���� ����Ʈ
    private Dictionary<int, List<Slot>> itemMap = new Dictionary<int, List<Slot>>(); // ���� ���, ������id

    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
        slotList.Clear();
        slotList.AddRange(GetComponentsInChildren<Slot>());

        foreach (var s in slotList)
        {
            s.OnCleared += HandleSlotCleared;
        }
    }
    // ���� ���̺� �����Ϳ��� �ҷ��� �� �����ؾ� ��

    void Start()
    {
        
    }

    public int AddItem(Item originalItem) // Add �� ���� ���� ��ȯ��.
    {
        if (originalItem == null) return 0;
  
        int remaining = originalItem.quantity;

        // ������ �������� ���� ������ �̹� �����ϴ��� Ž��
        if (itemMap.TryGetValue(originalItem.itemId, out var sameItemSlots))
        {
            foreach (var slot in sameItemSlots)
            {
                if (slot.item == null)
                    continue;

                int current = slot.item.quantity;
                int canAdd = maxQuantity - current;

                if (canAdd > 0)
                {
                    int add = Mathf.Min(remaining, canAdd);
                    slot.IncreaseQuantity(add);
                    remaining -= add;

                    if (remaining <= 0)
                        return 0;
                }
            }
        }

        // �� ���Կ� �� ������ �߰�
        foreach (var slot in slotList)
        {
            if (remaining <= 0)
                break;

            if (slot.item == null)
            {
                // ScriptableObject �����ؼ� ����
                Item newItem = Instantiate(originalItem);
                newItem.quantity = Mathf.Min(remaining, maxQuantity);
                slot.SetItem(newItem);

                // ��ųʸ��� ���
                if (!itemMap.ContainsKey(newItem.itemId))
                    itemMap[newItem.itemId] = new List<Slot>();
                itemMap[newItem.itemId].Add(slot);

                remaining -= newItem.quantity;
            }
        }

        if (remaining > 0)
        {
            Debug.Log("�κ��丮�� ���� á���ϴ�!");
        }

        QuickSlotManager.Instance?.UpdateQuickSlotsFromInventory();

        return remaining;
    }


    public bool RemoveItem(int itemId, int quantity) // �������� ������ ����ŭ ����. ������ true ��ȯ��.
    {
        if (!itemMap.TryGetValue(itemId, out var slots))
        {
            Debug.Log("�ش� �������� �����ϴ�!");
            return false;
        }

        int total = 0;

        foreach (var s in slots)
        {
            if (s.item != null)
                total += s.item.quantity;
        }

        if (total < quantity)
        {
            Debug.Log("�������� �����մϴ�!");
            return false;
        }

        int remaining = quantity;

        for (int i = slots.Count - 1; i >= 0 && remaining > 0; i--)
        {
            var slot = slots[i];
            if (slot.item == null)
                continue;

            int decrease = Mathf.Min(slot.item.quantity, remaining);
            slot.DecreaseQuantity(decrease);
            remaining -= decrease;

            if (slot.item == null)
                slots.RemoveAt(i);
        }

        // ��ųʸ��� ���� ������ ������ �׸� ����
        if (slots.Count == 0)
        {
            itemMap.Remove(itemId);
        }

        QuickSlotManager.Instance?.UpdateQuickSlotsFromInventory();

        return true;
    }

    private void HandleSlotCleared(Slot clearedSlot, int clearedItemId)
    {
        if (clearedItemId < 0) return;

        if (!itemMap.ContainsKey(clearedItemId)) return;

        var slots = itemMap[clearedItemId];
        slots.Remove(clearedSlot);

        if (slots.Count == 0)
        {
            itemMap.Remove(clearedItemId);
        }
    }

    public Slot GetSlotByIndex(int index)
    {
        if (index < 0 || index >= slotList.Count) return null;
        return slotList[index];
    }

    private void OnDestroy()
    {
        foreach (var s in slotList)
        {
            s.OnCleared -= HandleSlotCleared;
        }
    }
}
