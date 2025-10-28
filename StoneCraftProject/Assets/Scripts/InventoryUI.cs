using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;

    Canvas InvenCanvas;
    bool isOpen;

    public bool GetisOpenPauseMenu { get { return isOpen; } }

    private void Awake()
    {
        InvenCanvas = GetComponent<Canvas>();
        Instance = this;
        InvenCanvas.enabled = false;
        isOpen = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && InvenCanvas != null)
        {
            Debug.Log("I KEY");

            if (!isOpen)
            {
                Pause.Instance.OnPause();
                InvenCanvas.enabled = true;
                isOpen = true;

                QuickSlotManager.Instance?.SetActive(false);
            }
            else
            {
                Pause.Instance.OffPause();
                InvenCanvas.enabled = false;
                isOpen = false;

                if (InventoryDragHandler.Instance != null)
                {
                    InventoryDragHandler.Instance.CancelDrag();
                }

                QuickSlotManager.Instance?.SetActive(true);
            }
        }
    }

    public bool IsOpen()
    {
        return isOpen;
    }
}
