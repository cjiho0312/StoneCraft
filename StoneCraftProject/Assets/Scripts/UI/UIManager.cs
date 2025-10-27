using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] Canvas PauseCanvas;
    [SerializeField] Canvas AimCanvas;
    [SerializeField] Canvas InventoryCanvas;

    void Awake()
    {
        PauseCanvas.gameObject.SetActive(true);
        AimCanvas.gameObject.SetActive(true);
        //InventoryCanvas.gameObject.SetActive(true);
    }

    void Update()
    {
        
    }
}
