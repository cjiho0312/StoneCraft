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

        dragIcon = new GameObject("DragIcon", typeof(Image));
        dragIcon.transform.SetParent(panel, false);

        dragImage = dragIcon.GetComponent<Image>();
        dragImage.sprite = originSlot.item.itemImage;
        dragImage.raycastTarget = false;

        UpdateDragPosition(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragIcon != null)
        { UpdateDragPosition(eventData); }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (dragIcon != null)
        { Destroy(dragIcon); }

        if (eventData.pointerEnter != null)
        {
            Slot targetSlot = eventData.pointerEnter.GetComponent<Slot>();
            if (targetSlot != null)
            { SwapItems(originSlot, targetSlot); }
        }

        originSlot = null;
    }

    private void UpdateDragPosition(PointerEventData eventData)
    {
        // 스크린 좌표를 패널 로컬 좌표로 변환
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            panel, eventData.position, eventData.pressEventCamera, out Vector2 localPoint))
        {
            dragIcon.GetComponent<RectTransform>().localPosition = localPoint;
        }
    }

    private void SwapItems(Slot a, Slot b)
    {
        var temp = a.item;
        a.SetItem(b.item);
        b.SetItem(temp);
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
