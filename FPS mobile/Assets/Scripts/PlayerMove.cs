using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] Joystick joystick;
    [SerializeField] float speed = 5;
    [SerializeField] float jumpForce = 30;
    Rigidbody rb;
    Vector3 direction;
    bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        direction = transform.TransformDirection(horizontal, 0, vertical);

    }

    void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, -Vector3.up, 1f))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        rb.MovePosition(transform.position + speed * direction * Time.deltaTime);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }
    }

    public void Run()
    {
        if (speed == 5)
        {
            speed = 10;
        }
        else
        {
            speed = 5;
        }
        
    }
}