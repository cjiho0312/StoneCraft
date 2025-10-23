using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInteract : MonoBehaviour
{
    public static PlayerInteract Instance { get; set; }

    [SerializeField] float interactDistance = 3.2f;
    [SerializeField] LayerMask interactLayer;

    IInteractable nowFocus;

    Camera cam;

    void Start()
    {
        Instance = this;

        cam = Camera.main;
        nowFocus = null;
    }

    void Update()
    {
        ShootRaycast();
    }

    void ShootRaycast()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.yellow);

        if (Physics.Raycast(ray, out hit, interactDistance, interactLayer))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable == null)
                return;

            if (interactable != nowFocus )
            {
                nowFocus = interactable;
                nowFocus.OnFocus();
            }

            if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
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


    public void DeleteFocus()
    {
        nowFocus = null;
    }

}
