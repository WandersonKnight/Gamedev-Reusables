using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Camera playerCamera;
    Ray cameraRay;
    Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

    Rigidbody playerRigidbody;
    Collider playerCollider;

    Vector3 gravity = Physics.gravity;
    Vector3 mouseDirection;
    Vector3 movementDirection;
    Quaternion rotationDirection;

    public float dashSpeed = 2200f;
    public float moveSpeed = 170f;
    public float jumpForce = 1500f;
    public float rotationSpeed = 2000f;
    float axisX;
    float axisZ;
    float rayLenght;

    bool canJump = false;
    bool isDashing = false;
    bool isJumping = false;
    bool isShooting = false;

    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();
        playerCamera = Camera.main;
    }

    bool AllowJump()
    {
        return Physics.Raycast(playerCollider.bounds.center, Vector3.down, playerCollider.bounds.extents.y + 0.02f);
    }

    public void ListenMousePosition()
    {
        if (Input.GetMouseButton(0))
        {
            cameraRay = playerCamera.ScreenPointToRay(Input.mousePosition);

            if (groundPlane.Raycast(cameraRay, out rayLenght))
            {
                mouseDirection = cameraRay.GetPoint(rayLenght);
                mouseDirection = new Vector3(mouseDirection.x, transform.position.y, mouseDirection.z);
                transform.LookAt(mouseDirection);
            }

            isShooting = true;
        }
        else
        {
            isShooting = false;
        }
    }

    public void ListenMoveInput()
    {
        axisX = Input.GetAxisRaw("Horizontal");
        axisZ = Input.GetAxisRaw("Vertical");
        movementDirection = new Vector3(axisX, 0, axisZ).normalized;

        if (movementDirection != Vector3.zero && isShooting == false)
        {
            rotationDirection = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationDirection, rotationSpeed * Time.deltaTime);
        }
    }

    public void Move()
    {
        if (isDashing == false)
        {
            playerRigidbody.AddForce(Time.deltaTime * movementDirection * moveSpeed, ForceMode.VelocityChange);
        }
    }
   
    public void ListenJumpInput()
    {
        canJump = AllowJump();

        if (Input.GetKeyDown(KeyCode.Space) && canJump == true)
        {
            isJumping = true;
        }
    }

    public void Jump()
    {
        if (isJumping)
        {
            playerRigidbody.AddForce(jumpForce * Vector3.up);
            isJumping = false;
        }
    }

    public void ListenDashInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isDashing = true;
        }
    }

    public void StartDash()
    {
        if (isDashing == true)
        {
            Physics.gravity = Vector3.zero;
            playerRigidbody.AddForce(Time.deltaTime * transform.forward * dashSpeed, ForceMode.VelocityChange);
            Physics.gravity = gravity;
            isDashing = false;
        }
    }

}
