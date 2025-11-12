using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;

    Canvas InvenCanvas;
    [SerializeField] Text MoneyText;
    [SerializeField] Sprite EmptyIcon;

    private void Awake()
    {
        InvenCanvas = GetComponent<Canvas>();
        Instance = this;
        InvenCanvas.enabled = false;
    }

    public void OpenInvenUI()
    {
        Pause.Instance.OnPause();
        UpdateMoney();
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

    public Sprite GetEmptyIcon()
    {
        return EmptyIcon;
    }

    void UpdateMoney()
    {
        string m = PlayerManager.Instance.money.ToString(); 
        MoneyText.text = m;
    }

}
