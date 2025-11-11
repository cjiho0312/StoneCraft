using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInteract : MonoBehaviour
{
    public static PlayerInteract Instance { get; set; }

    [SerializeField] float interactDistance = 3.2f;
    [SerializeField] LayerMask interactLayer;
    public Vector3 interactPos;

    IInteractable nowFocus;

    Camera cam;

    private bool canInteract = true;

    void Start()
    {
        Instance = this;

        cam = Camera.main;
        nowFocus = null;
    }

    void Update()
    {
        if (!canInteract) return;
        ShootRaycast();
    }

    void ShootRaycast()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.yellow);

        if (Physics.Raycast(ray, out hit, interactDistance, ~0))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (((1 << hitObject.layer) & interactLayer) != 0)
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();

                if (interactable != null)
                {
                    if (interactable != nowFocus)
                    {
                        if (nowFocus != null) nowFocus.OnLoseFocus();
                        nowFocus = interactable;
                        nowFocus.OnFocus();
                    }

                    if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
                    {
                        interactPos = hit.point;
                        nowFocus.OnInteract();
                        nowFocus = null;
                    }

                    return;
                }
            }

            if (nowFocus != null)
            {
                nowFocus.OnLoseFocus();
                nowFocus = null;
            }

        }
        else
        {
            // 레이캐스트가 아무것도 안 맞았을 때
            if (nowFocus != null)
            {
                nowFocus.OnLoseFocus();
                nowFocus = null;
            }
        }
    }

    public void SetCanInteract(bool value)
    {
        canInteract = value;
    }

    public void DeleteFocus()
    {
        nowFocus = null;
    }

}
