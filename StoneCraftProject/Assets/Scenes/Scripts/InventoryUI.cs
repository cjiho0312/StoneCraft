using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;

    Canvas InvenCanvas;

    private void Awake()
    {
        InvenCanvas = GetComponent<Canvas>();
        Instance = this;
        InvenCanvas.enabled = false;
    }

    public void OpenInvenUI()
    {
        Pause.Instance.OnPause();
        InvenCanvas.enabled = true;

        QuickSlotManager.Instance.SetActive(false);
    }

    public void CloseInvenUI()
    {
        Pause.Instance.OffPause();
        InvenCanvas.enabled = false;

        if (InventoryDragHandler.Instance != null)
        {
            InventoryDragHandler.Instance.CancelDrag();
        }

        QuickSlotManager.Instance.SetActive(true);
    }

}
