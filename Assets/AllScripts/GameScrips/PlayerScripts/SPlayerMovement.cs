using UnityEngine;

public class SPlayerMovement : MonoBehaviour
{
    internal protected bool isRunning;
    internal protected bool isGrounded;
    internal protected byte jumpCount;
    private Rigidbody rb;
    Vector3 customGravity = new Vector3(0, -25f, 0);
    protected internal bool playerOnTargetPlatform;
    public SLastElevator lastElevator;
    public SFirstElevator firstElevator;
    public SCamera mainCamera;
    public StateMonitor monitor;
    public SPlayerTouchController playerTouchContr;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Physics.gravity = customGravity;
        
        SetNewPlayerPos();
    }
    private void Update()
    {
        Jumping();
        if (mainCamera.cameraBehindPlayer && monitor.timer < 1)
            isRunning = true;
        if (playerOnTargetPlatform)
        {
            isRunning = false;
        }

        if (transform.localPosition.x + 1 > lastElevator.transform.localPosition.x)
            playerOnTargetPlatform = true;
        else
            playerOnTargetPlatform = false;
    }
    private void FixedUpdate()
    {
        SideMovement();
        Running(isRunning);
    }

    void SideMovement()
    {
        float speed = 10f;
        Vector3 newPos = new Vector3(transform.position.x, transform.position.y, playerTouchContr.pos);
        if (newPos.x >= transform.position.x) // Проверка на движение только вперед
        {
            transform.position = Vector3.MoveTowards(transform.position, newPos, speed * Time.deltaTime);
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
    void Jumping()
    {
        float jumpForce = 7.5f;
        if (playerTouchContr.jump)
        {
            if (jumpCount < 2)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isGrounded = false;
                jumpCount++;
            }
            playerTouchContr.jump = false;
        }
    }

    public void SetNewPlayerPos()
    {
        float newX = firstElevator.transform.position.x;
        float newY = firstElevator.transform.position.y + 0.5f;
        float newZ = firstElevator.transform.position.z;
        rb.velocity = Vector3.zero;
        
        transform.position = new Vector3(newX, newY, newZ);
        playerTouchContr.pos = newZ;
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
