using UnityEngine;

public class WorkTable : MonoBehaviour, IInteractable
{
    public void OnFocus()
    {
        Debug.Log("On Focus");
    }

    public void OnInteract()
    {
        Debug.Log("Interacting");
        WorkManager.Instance.StartWork();
    }

    public void OnLoseFocus()
    {
        Debug.Log("Off Focus");
    }
}
