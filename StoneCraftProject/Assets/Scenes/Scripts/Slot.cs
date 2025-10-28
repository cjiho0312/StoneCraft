using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] public Item item;
    [SerializeField] public Image icon;
    [SerializeField] public Text quantityText;

    public int slotIndex;

    public event Action<Slot, int> OnCleared;

    void Awake()
    {
        quantityText = GetComponentInChildren<Text>();
        SetIconAlpha(0f);
    }

    private void Start()
    {
        
    }

    public void SetItem(Item newItem)
    {
        item = newItem;

        if (item == null)
        {
            ClearSlot();
            return;
        }

        if (icon != null)
        {
            icon.sprite = item.itemImage;
            SetIconAlpha(1f);
        }

        quantityText.text = item.quantity.ToString();
    }

    public void IncreaseQuantity(int count)
    {
        if (item == null)
            return;

        item.quantity += count;
        quantityText.text = item.quantity.ToString();
    }

    public void DecreaseQuantity(int count)
    {
        if (item == null)
            return;

        item.quantity -= count;

        if (item.quantity <= 0)
        {
            ClearSlot();
        }
        else
        {
            quantityText.text = item.quantity.ToString();
        }
    }

    private void ClearSlot()
    {
        int clearedItemId = item != null ? item.itemId : -1;

        OnCleared?.Invoke(this, clearedItemId);

        item = null;
        icon.sprite = null;
        SetIconAlpha(0f);
        quantityText.text = "";

    }

    private void SetIconAlpha(float a)
    {
        if (icon == null) return;
        var c = icon.color;
        c.a = a;
        icon.color = c;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        InventoryDragHandler.Instance.OnSlotClicked(this, eventData);
    }
}
