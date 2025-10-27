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
            slotList.Add(slot); // �ڽ� slot�� ��������
        }
    }
    // ���� ���̺� �����Ϳ��� �ҷ��� �� �����ؾ� ��

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public int GetItem(Item thisItem) // Add �� ���� ���� ��ȯ��.
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
            Debug.Log("�κ��丮�� ���� á���ϴ�!");
            return thisItemQuantity;
        }
        else
        {
            return 0;
        }
    }

    public bool DecreaseItem(int decreaseItemId, int decreaseQuantity) // �ش� �������� ���� ����ϴٸ� true, �����ϴٸ� false ��ȯ
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
            Debug.Log("�������� �����մϴ�!");
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
