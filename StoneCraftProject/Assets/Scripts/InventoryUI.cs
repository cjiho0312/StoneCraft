using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;

    Canvas InvenCanvas;
    bool isOpenInvenMenu;

    public bool GetisOpenPauseMenu { get { return isOpenInvenMenu; } }

    private void Awake()
    {
        InvenCanvas = GetComponent<Canvas>();
        Instance = this;
        InvenCanvas.enabled = false;
        isOpenInvenMenu = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && InvenCanvas != null)
        {
            Debug.Log("ESC KEY");

            if (!isOpenInvenMenu)
            {
                Pause.Instance.OnPause();
                InvenCanvas.enabled = true;
                isOpenInvenMenu = true;
            }
            else
            {
                Pause.Instance.OffPause();
                InvenCanvas.enabled = false;
                isOpenInvenMenu = false;
            }
        }
    }
}
