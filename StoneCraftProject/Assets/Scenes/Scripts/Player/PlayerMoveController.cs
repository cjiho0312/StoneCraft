using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

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
    PlayerManager playerManager;
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
        playerManager = PlayerManager.Instance;
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

    private void PlayerMove() // 전반적인 플레이어 이동 제어 함수
    {
        mouseX += Input.GetAxis("Mouse X") * mouseSpeedX;
        transform.localEulerAngles = new Vector3(0, mouseX, 0);

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        bool isMoving = (Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f);
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        Vector3 move = new Vector3(h, 0, v) * currentSpeed;
        move = controller.transform.TransformDirection(move);

        if (controller.isGrounded)
        {
            if (verticalVelocity < -1)
            {
                verticalVelocity = -1f;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Jump Key");
                verticalVelocity = jump;
                playerManager.ChangePlayerState(PlayerState.JUMPING);
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        move.y = verticalVelocity;
        controller.Move(move * Time.deltaTime);

        if (controller.isGrounded)
        {
            if (!isMoving)
                playerManager.ChangePlayerState(PlayerState.IDLE);
            else
                playerManager.ChangePlayerState(currentSpeed == walkSpeed ? PlayerState.WALKING : PlayerState.RUNNING);
        }
    }

    private void PlayerCamMoveY() // Y축 카메라 회전 함수
    {
        mouseY += Input.GetAxis("Mouse Y") * mouseSpeedY;

        mouseY = Mathf.Clamp(mouseY, mouseLimitD, mouseLimitU);

        cam.transform.localEulerAngles = new Vector3(-mouseY, 0, 0);
    }

    public Vector3 GetPlayerPosition()
    {
        return transform.position;
    }

    public void MoveTo(float x, float y, float z)
    {
        transform.position = new Vector3(x, y, z);
        Debug.Log($"{gameObject.name} 이동 {x}, {y}, {z}");
        Debug.Log($"현재 위치 : {transform.position}");
    }

    public IEnumerator ApplyGravityUntilGrounded()
    {
        if (controller.isGrounded)
            yield break;

        float tempVelocity = verticalVelocity;

        while (!controller.isGrounded)
        {
            tempVelocity -= gravity * Time.deltaTime;
            controller.Move(Vector3.up * tempVelocity * Time.deltaTime);
            yield return null;
        }

        // 착지 후 속도 초기화
        verticalVelocity = -1f;
    }
}