using UnityEngine;

public class Cart : MonoBehaviour, IInteractable
{
    bool isPulling;
    [SerializeField] private GameObject CreationArea;
    public Vector3 GetCreationAreaPos() { return CreationArea.transform.position; }


    void Start()
    {
        isPulling = false;
    }

    public void OnFocus()
    {
        
    }

    public void OnInteract()
    {
        if (isPulling)
        {
            return;
        }
        else
        {
            StartPullCart();
        }
    }

    public void OnLoseFocus()
    {
        
    }

    void StartPullCart()
    {
        var Player = PlayerMoveController.Instance;

        // Vector3 holdPos = HoldArea.transform.position;
        //transform.position = new Vector3(0, 0, 0);
        // transform.rotation = Quaternion.identity;
    }

    


}
