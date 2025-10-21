using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    public static PlayerMoveController Instance { get; private set; }

    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float runSpeed = 8f;
    [SerializeField] float mouseSpeedX = 4f;
    [SerializeField] float mouseSpeedY = 4f;
    [SerializeField] float jump = 6f;
    [SerializeField] float gravity = 20f;

    private float verticalVelocity;
    private float mouseX;

    private float mouseY = 0f;
    private float mouseLimitU = 50f;
    private float mouseLimitD = -55f;

    private CharacterController controller;
    Camera cam;

    void Start()
    {
        Instance = this;
        controller = GetComponent<CharacterController>();
        cam = Camera.main;

        transform.position = SaveManager.Instance.savedPlayerData.pos;
    }

    void Update()
    {
        PlayerMove();
        PlayerCamMoveY();
    }

    private void PlayerMove() // 전반적인 플레이어 이동 제어 함수
    {
        mouseX += Input.GetAxis("Mouse X") * mouseSpeedX;
        transform.localEulerAngles = new Vector3(0, mouseX, 0);

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * currentSpeed;
        move = controller.transform.TransformDirection(move);

        if (controller.isGrounded)
        {
            if (verticalVelocity < 0)
                verticalVelocity = -1f;

            if (Input.GetKeyDown(KeyCode.Space))
                verticalVelocity = jump;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        move.y = verticalVelocity;
        controller.Move(move * Time.deltaTime);
    }

    private void PlayerCamMoveY() // Y축 카메라 회전 함수
    {
        mouseY += Input.GetAxis("Mouse Y") * mouseSpeedY;

        mouseY = Mathf.Clamp(mouseY, mouseLimitD, mouseLimitU);

        cam.transform.localEulerAngles = new Vector3(-mouseY, 0, 0);
    }
    public Vector3 GetPlayerPosition()
    {
        Vector3 p = transform.position;
        return p;
    }

}