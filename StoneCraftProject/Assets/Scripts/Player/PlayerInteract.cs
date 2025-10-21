using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] float interactDistance = 3.2f;
    [SerializeField] LayerMask interactLayer;

    IInteractable nowFocus;

    Camera cam;

    void Start()
    {
        cam = Camera.main;
        nowFocus = null;
    }

    void Update()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.yellow);

        if (Physics.Raycast(ray, out hit, interactDistance, interactLayer))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != nowFocus)
            {
                nowFocus = interactable;
                nowFocus.OnFocus();
            }
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                nowFocus.OnInteract();
            }
        }
        else
        {
            if (nowFocus != null)
            {
                nowFocus.OnLoseFocus();
                nowFocus = null;
            }
        }
    }
}
