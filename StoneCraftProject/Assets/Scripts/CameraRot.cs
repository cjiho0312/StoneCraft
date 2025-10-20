using UnityEngine;

public class CameraRot : MonoBehaviour
{
    [SerializeField] private float mouseSpeedY = 4f; //ȸ���ӵ�

    private float mouseY = 0f; //���Ʒ� ȸ������ ���� ����
    private float mouseLimitU = 50f;
    private float mouseLimitD = -55f;

    void Update()
    {
        mouseY += Input.GetAxis("Mouse Y") * mouseSpeedY;

        mouseY = Mathf.Clamp(mouseY, mouseLimitD, mouseLimitU);

        this.transform.localEulerAngles = new Vector3(-mouseY, 0, 0);
    }
}