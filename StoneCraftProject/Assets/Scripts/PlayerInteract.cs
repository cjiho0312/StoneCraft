using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] float interactDistance = 3.2f;
    [SerializeField] LayerMask interactLayer;

    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.yellow);

        if (Physics.Raycast(ray, out hit, interactDistance, interactLayer))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                // IInteractable 인터페이스 구현체 탐색
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    interactable.OnInteract();
                }
            }
        }
    }
}
