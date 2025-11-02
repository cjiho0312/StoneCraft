using UnityEngine;

public class StoneStorageLog : MonoBehaviour, IInteractable
{
    [SerializeField] StoneStorageLogUI ui;

    public void OnFocus()
    {
        Debug.Log("On Focus");
        AimSwitch.Instance.ChangeAim(AimState.ELSE);
    }

    public void OnInteract()
    {
        Debug.Log("Interacting");
        ui.OpenStoneStorageLogUI();
    }

    public void OnLoseFocus()
    {
        Debug.Log("On Focus");
        AimSwitch.Instance.ChangeAim(AimState.NONE);
    }
}
