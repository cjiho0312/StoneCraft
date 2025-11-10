using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    [SerializeField] ShelfSlot slot1;
    [SerializeField] ShelfSlot slot2;
    [SerializeField] ShelfSlot slot3;
    [SerializeField] ShelfSlot slot4;
    [SerializeField] ShelfSlot slot5;
    [SerializeField] ShelfSlot slot6;

    List <ShelfSlot> SlotList;

    private void Start()
    {
        SlotList = new List<ShelfSlot>();
        SlotList.Add(slot1);
        SlotList.Add(slot2);
        SlotList.Add(slot3);
        SlotList.Add(slot4);
        SlotList.Add(slot5);
        SlotList.Add(slot6);

        CanInteract();
    }

    public bool isExistSculpure()
    {
        foreach (var slot in SlotList)
        {
            if (slot.SculptureData != null)
            {
                return true;
            }
        }
        return false;  
    }

    public ShelfSlot ChoiceOneSculpture()
    {
        foreach (var slot in SlotList)
        {
            if (slot.SculptureData != null)
            {
                return slot;
            }
        }
        return null;
    }

    public void StopInteract()
    {
        foreach (var slot in SlotList)
        {
            slot.canInteract = false;
        }
    }

    public void CanInteract()
    {
        foreach (var slot in SlotList)
        {
            slot.canInteract = true;
        }
    }
}
