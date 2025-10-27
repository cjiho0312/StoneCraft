using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }
    private List<Slot> slotList;

    private int maxQuantity = 20;

    private void Awake()
    {
        Instance = this;
        
        var slots = GetComponentsInChildren<Slot>();
        foreach( var slot in slots )
        {
            slotList.Add(slot); // 자식 slot들 가져오기
        }
    }
    // 추후 세이브 데이터에서 불러온 뒤 정렬해야 함

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public int GetItem(Item thisItem) // Add 후 남은 양을 반환함.
    {
        int thisItemQuantity = thisItem.quantity;

        for (int i = 0; i < slotList.Count; i++)
        {
            if (thisItemQuantity == 0)
            {
                break;
            }

            if (slotList[i].item.itemId == thisItem.itemId && slotList[i].item.quantity < maxQuantity)
            {
                if (slotList[i].item.quantity >= maxQuantity)
                {
                    continue;
                }
                else if (slotList[i].item.quantity + thisItemQuantity > maxQuantity)
                {
                    int increase = maxQuantity - slotList[i].item.quantity;
                    slotList[i].IncreaseQuantity(increase);
                    thisItemQuantity -= increase;
                }
                else
                {
                    slotList[i].IncreaseQuantity(thisItemQuantity);
                    thisItemQuantity = 0;
                }
            }
        }
        
        for (int i = 0; i < slotList.Count; i++)
        {
            if (thisItemQuantity == 0)
            {
                break;
            }
            if (slotList[i].item == null)
            {
                thisItem.quantity = thisItemQuantity;
                slotList[i].SetItem(thisItem);
            }
        }

        if(thisItemQuantity > 0)
        {
            Debug.Log("인벤토리가 가득 찼습니다!");
            return thisItemQuantity;
        }
        else
        {
            return 0;
        }
    }

    public bool DecreaseItem(int decreaseItemId, int decreaseQuantity) // 해당 아이템의 양이 충분하다면 true, 부족하다면 false 반환
    {
        int hasQuantity = 0;

        for (int i = 0; i < slotList.Count; i++)
        {
            if (slotList[i].item.itemId == decreaseItemId)
            {
                hasQuantity += slotList[i].item.quantity;
            }
        }

        if (hasQuantity < decreaseQuantity)
        {
            Debug.Log("아이템이 부족합니다!");
            return false;
        }

        int remainingQ = decreaseQuantity;

        for (int i = 0; i < slotList.Count; i++)
        {
            if (remainingQ == 0)
            {
                break;
            }

            if (slotList[i].item.itemId == decreaseItemId)
            {
                if (slotList[i].item.quantity < remainingQ)
                {
                    int tempQ = slotList[i].item.quantity;
                    remainingQ -= tempQ;
                    slotList[i].DecreaseQuantity(tempQ);
                }
                else
                {
                    slotList[i].DecreaseQuantity(remainingQ);
                    remainingQ = 0;
                }
            }
        }

        return true;
    }
}
