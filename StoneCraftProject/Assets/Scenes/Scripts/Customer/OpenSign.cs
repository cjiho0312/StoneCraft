using UnityEngine;
using UnityEngine.UI;

public class OpenSign : MonoBehaviour, IInteractable
{
    [SerializeField] Text text;

    public void OnFocus()
    {
        AimSwitch.Instance.ChangeAim(AimState.ELSE);
    }

    public void OnInteract()
    {
        if (WorkshopManager.Instance.isWorkshopOpen)
        {
            WorkshopManager.Instance.CloseWorkshop();
            text.text = "Close";
        }
        else
        {
            WorkshopManager.Instance.OpenWorkshop();
            text.text = "Open";
        }
    }

    public void OnLoseFocus()
    {
        AimSwitch.Instance.ChangeAim(AimState.NONE);
    }
}
