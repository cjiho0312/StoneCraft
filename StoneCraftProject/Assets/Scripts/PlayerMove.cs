using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float walkSpeed = 12f;
    [SerializeField] float runSpeed = 20f;
    [SerializeField] float mouseSpeedX = 4f; // 마우스 회전 속도
    [SerializeField] float jump = 6f;
    [SerializeField] float gravity = 20f;

    private float verticalVelocity;
    private float mouseX;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
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
}