using System;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] public Item item;
    [SerializeField] Image icon;
    [SerializeField] Text quantityText;

    void Awake()
    {
        quantityText = GetComponentInChildren<Text>();

        Color c = icon.color;
        c.a = 0;
        icon.color = c;
    }

    private void Start()
    {
        
    }

    public void SetItem(Item item)
    {
        Color c = icon.color;
        c.a = 100;
        icon.color = c;

        this.item = item;
        icon.sprite = item.itemImage;

        string q = item.quantity.ToString();
        quantityText.text = q;
    }

    public void IncreaseQuantity(int Count)
    {
        item.quantity += Count;
        string q = item.quantity.ToString();
        quantityText.text = q;
    }

    public void DecreaseQuantity(int Count)
    {
        item.quantity -= Count;

        if (item.quantity <= 0)
        {
            DeleteSlot();
        }

        string q = item.quantity.ToString();
        quantityText.text = q;
    }

    private void DeleteSlot()
    {
        Color c = icon.color;
        c.a = 0;
        icon.color = c;

        item = null;
        icon.sprite = null;
        quantityText = null;
    }
}
