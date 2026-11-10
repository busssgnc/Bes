using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 9f;

    [Header("Zıplama Ayarları")]
    public float jumpHeight = 4f;
    public float gravity = -25f;
    public int maxJumps = 2;
    public float fallLimit = -10f;

    [Header("Kamera Referansı")]
    public Transform cameraTransform; 

    private CharacterController controller;
    private Vector3 velocity;
    private Vector3 startPosition;
    int jumpCount = 0;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (controller == null)
        {
            controller = gameObject.AddComponent<CharacterController>();
        }

        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }

        startPosition = transform.position;
    }

    void Update()
    {
        Move();
        CheckFall();
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = Vector3.zero;

        if (cameraTransform != null)
        {
       
            Vector3 camForward = cameraTransform.forward;
            Vector3 camRight = cameraTransform.right;
            camForward.y = 0f;
            camRight.y = 0f;
            camForward.Normalize();
            camRight.Normalize();

           
            move = (camForward * z + camRight * x).normalized;
        }
        else
        {
            
            move = new Vector3(x, 0f, z).normalized;
        }

        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        controller.Move(move * speed * Time.deltaTime);

       
        if (move.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                12f * Time.deltaTime
            );
        }

        if (controller.isGrounded)
        {
            if (velocity.y < 0)
            {
                velocity.y = -2f;
            }
            jumpCount = 0;
        }

        
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumpCount++;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
  
    public void Bounce(float force)
    {
        velocity.y = Mathf.Sqrt(force * -2f * gravity);
    }

    void CheckFall()
    {
        if (transform.position.y < fallLimit)
        {
            controller.enabled = false;
            transform.position = startPosition;
            controller.enabled = true;

            velocity = Vector3.zero;
            jumpCount = 0;
        }
    }
}