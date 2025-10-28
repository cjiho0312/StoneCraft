using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    [SerializeField] private int maxQuantity = 20;
    [SerializeField] private List<Slot> slotList = new List<Slot>(); // ui용 슬롯 리스트
    private Dictionary<int, List<Slot>> itemMap = new Dictionary<int, List<Slot>>(); // 슬롯 목록, 아이템id

    
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
    // 추후 세이브 데이터에서 불러온 뒤 정렬해야 함

    void Start()
    {
        
    }

    public int AddItem(Item originalItem) // Add 후 남은 양을 반환함.
    {
        if (originalItem == null) return 0;
  
        int remaining = originalItem.quantity;

        // 동일한 아이템을 가진 슬롯이 이미 존재하는지 탐색
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

        // 빈 슬롯에 새 아이템 추가
        foreach (var slot in slotList)
        {
            if (remaining <= 0)
                break;

            if (slot.item == null)
            {
                // ScriptableObject 복제해서 저장
                Item newItem = Instantiate(originalItem);
                newItem.quantity = Mathf.Min(remaining, maxQuantity);
                slot.SetItem(newItem);

                // 딕셔너리에 등록
                if (!itemMap.ContainsKey(newItem.itemId))
                    itemMap[newItem.itemId] = new List<Slot>();
                itemMap[newItem.itemId].Add(slot);

                remaining -= newItem.quantity;
            }
        }

        if (remaining > 0)
        {
            Debug.Log("인벤토리가 가득 찼습니다!");
        }

        QuickSlotManager.Instance?.UpdateQuickSlotsFromInventory();

        return remaining;
    }


    public bool RemoveItem(int itemId, int quantity) // 아이템을 지정한 수만큼 차감. 성공시 true 반환함.
    {
        if (!itemMap.TryGetValue(itemId, out var slots))
        {
            Debug.Log("해당 아이템이 없습니다!");
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
            Debug.Log("아이템이 부족합니다!");
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

        // 딕셔너리에 남은 슬롯이 없으면 항목 제거
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
