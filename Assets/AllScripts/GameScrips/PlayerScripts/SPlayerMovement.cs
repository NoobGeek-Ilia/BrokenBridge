using UnityEngine;
public class SPlayerMovement : MonoBehaviour
{
    internal protected bool isRunning;
    internal protected bool isGrounded;
    internal protected byte jumpCount;
    internal protected Rigidbody rb;
    Vector3 customGravity = new Vector3(0, -25f, 0);
    protected internal bool playerOnTargetPlatform;
    public SLastElevator lastElevator;
    public SFirstElevator firstElevator;
    public SCamera mainCamera;
    public StateMonitor monitor;
    float sideTargetPos;

    private void Start()
    {
        STouchDetection.TouchEvent += OnTouch;
        rb = GetComponent<Rigidbody>();
        Physics.gravity = customGravity;
        
        SetNewPlayerPos();
    }

    private void OnTouch(STouchDetection.ActionTipe action)
    {
        Movement(action);
    }

    private void Update()
    {
        if (mainCamera.cameraBehindPlayer && monitor.timer < 1)
            isRunning = true;
        if (playerOnTargetPlatform)
        {
            isRunning = false;
        }

        if (transform.position.x + 1 > lastElevator.transform.position.x)
            playerOnTargetPlatform = true;
        else
            playerOnTargetPlatform = false;
    }
    private void FixedUpdate()
    {
        SideMovement();
        Running(isRunning);
    }

    void Movement(STouchDetection.ActionTipe action)
    {
        const float stepSize = 1.3f;
        switch (action)
        {
            case STouchDetection.ActionTipe.Right:
                if (sideTargetPos > -stepSize)
                    sideTargetPos -= stepSize;
                break;
            case STouchDetection.ActionTipe.Left:
                if (sideTargetPos < stepSize)
                    sideTargetPos += stepSize;
                break;
            case STouchDetection.ActionTipe.Jump:
                Jumping();
                break;
            case STouchDetection.ActionTipe.Hit:

                break;
        }
    }
    void SideMovement()
    {
        float speed = 10f;
        Vector3 newPos = new Vector3(transform.position.x, transform.position.y, sideTargetPos);
        transform.position = Vector3.MoveTowards(transform.position, newPos, speed * Time.deltaTime);
    }


    void Jumping()
    {
        float jumpForce = 7.5f;

        if (jumpCount < 2)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            jumpCount++;
        }
    }

    void Running(bool running)
    {
        float speed = 0.15f;
        if (running)
        {
            Vector3 movement = new Vector3(1, 0f, 0);
            transform.position += movement * speed;
        }
    }

    public void SetNewPlayerPos()
    {
        float newX = firstElevator.transform.position.x;
        float newY = firstElevator.transform.position.y + 0.5f;
        float newZ = firstElevator.transform.position.z;
        rb.velocity = Vector3.zero;
        
        transform.position = new Vector3(newX, newY, newZ);
        sideTargetPos = 0;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlatformTag") || collision.gameObject.CompareTag("BridgeParticleTag"))
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }
}
