using UnityEngine;

public class CameraManager : MonoBehaviour
{
    static public CameraManager Instance;

    [SerializeField] Camera MainCamera;
    [SerializeField] Camera SculptingCamera;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        MainCamera.gameObject.SetActive(true);
        SculptingCamera.gameObject.SetActive(true);
    }

    void Start()
    {
        SculptingCamera.enabled = false;
    }

    public void OnSculptingCam()
    {
        SculptingCamera.enabled = true;
        MainCamera.enabled = false;
    }

    public void OnMainCam()
    {
        if (MainCamera.enabled) { return; }

        MainCamera.enabled = true;
        SculptingCamera.enabled = false;
    }
}
