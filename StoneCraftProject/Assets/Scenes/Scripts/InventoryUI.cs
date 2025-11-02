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

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.I) && InvenCanvas != null)
    //     {
    //         Debug.Log("I KEY");
    // 
    //         if (!isOpen)
    //         {
    //             Pause.Instance.OnPause();
    //             InvenCanvas.enabled = true;
    //             isOpen = true;
    // 
    //             QuickSlotManager.Instance?.SetActive(false);
    //         }
    //         else
    //         {
    //             Pause.Instance.OffPause();
    //             InvenCanvas.enabled = false;
    //             isOpen = false;
    // 
    //             if (InventoryDragHandler.Instance != null)
    //             {
    //                 InventoryDragHandler.Instance.CancelDrag();
    //             }
    // 
    //             QuickSlotManager.Instance?.SetActive(true);
    //         }
    //     }
    // }
}
