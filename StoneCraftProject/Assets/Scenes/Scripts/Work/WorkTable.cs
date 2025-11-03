using UnityEngine;

public class WorkTable : MonoBehaviour, IInteractable
{
    public void OnFocus()
    {
        AimSwitch.Instance.ChangeAim(AimState.ELSE);
    }

    public void OnInteract()
    {
        Debug.Log("Interacting");

        if (PlayerManager.Instance.currentItem != null && PlayerManager.Instance.currentItem.itemtype == ItemType.Tool)
        {
            WorkManager.Instance.StartWork();
        }
        else
        {
            Debug.Log("도구가 필요합니다!");
        }
    }

    public void OnLoseFocus()
    {
        AimSwitch.Instance.ChangeAim(AimState.NONE);
    }
}
