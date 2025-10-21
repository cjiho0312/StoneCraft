using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    public static PlayerMoveController Instance { get; set; }

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

    public bool isCanMove;

    private CharacterController controller;
    Camera cam;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main;
        isCanMove = true;
    }

    void Update()
    {
        if (!isCanMove)
        {
            return;
        }

        PlayerMove();
        PlayerCamMoveY();

    }

    public void MoveTo(float x, float y, float z)
    {
        transform.position = new Vector3(x, y, z);
        Debug.Log("�̵� �Ϸ� : " + x + ", " + y + ", " + z);
    }

    private void PlayerMove() // �������� �÷��̾� �̵� ���� �Լ�
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

    private void PlayerCamMoveY() // Y�� ī�޶� ȸ�� �Լ�
    {
        mouseY += Input.GetAxis("Mouse Y") * mouseSpeedY;

        mouseY = Mathf.Clamp(mouseY, mouseLimitD, mouseLimitU);

        cam.transform.localEulerAngles = new Vector3(-mouseY, 0, 0);
    }
    public Vector3 GetPlayerPosition()
    {
        return transform.position;
    }

}