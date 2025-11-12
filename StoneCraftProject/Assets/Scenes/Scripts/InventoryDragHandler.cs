using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static InventoryDragHandler Instance;

    private Slot originSlot;
    private GameObject dragIcon;
    private Image dragImage;
    private RectTransform panel;

    private Canvas rootC;

    private void Awake()
    {
        Instance = this;
        panel = GetComponentInParent<RectTransform>();
    }

    public void OnSlotClicked(Slot slot, PointerEventData eventData)
    {
        originSlot = slot;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (originSlot == null || originSlot.item == null) return;

        rootC = panel.GetComponentInParent<Canvas>();
        dragIcon = new GameObject("DragIcon", typeof(Image));
        dragIcon.transform.SetParent(rootC.transform, false);

        dragIcon.SetActive(false);
        Debug.Log("dragIcon Set Active False");

        dragImage = dragIcon.GetComponent<Image>();
        dragImage.sprite = originSlot.item.itemImage;
        dragImage.raycastTarget = false;

        Image image = originSlot.icon;
        Color c = image.color;
        c.a = 0.5f;
        image.color = c;

        UpdateDragPosition(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragIcon == null) return;

        UpdateDragPosition(eventData);

        if (!dragIcon.activeSelf)
        {
            dragIcon.SetActive(true);
            Debug.Log("dragIcon Set Active true");
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (dragIcon != null)
        { Destroy(dragIcon); }

        if (eventData.pointerEnter != null)
        {
            Slot targetSlot = eventData.pointerEnter.gameObject.GetComponentInParent<Slot>();

            if (targetSlot != null)
            { 
                SwapItems(originSlot, targetSlot); 
            }
            else
            {
                Debug.Log("target Slot이 null입니다");
            }
        }
        else
        {
            Debug.Log("eventData.pointerEnter이 null입니다");
        }

        Image image = originSlot.icon;
        Color c = image.color;
        c.a = 1f;
        image.color = c;

        originSlot = null;
    }

    private void UpdateDragPosition(PointerEventData eventData)
    {
        // 스크린 좌표를 패널 로컬 좌표로
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rootC.transform as RectTransform, eventData.position, eventData.pressEventCamera, out Vector2 localPoint))
        {
            dragIcon.GetComponent<RectTransform>().localPosition = localPoint;
        }
    }

    private void SwapItems(Slot a, Slot b)
    {
        var temp = a.item;
        a.SetItem(b.item);
        b.SetItem(temp);
        Debug.Log("Swap 완료");

        QuickSlotManager.Instance?.UpdateQuickSlotsFromInventory();
    }

    public void CancelDrag()
    {
        if (dragIcon != null)
        { Destroy(dragIcon); }

        originSlot = null;
        dragIcon = null;
        dragImage = null;
    }
}
