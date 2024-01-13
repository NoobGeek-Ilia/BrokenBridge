using System;
using UnityEngine;
public class SPlayerMovement : MonoBehaviour
{
    public SLastElevator lastElevator;
    public SFirstElevator firstElevator;
    public SCamera mainCamera;
    public StateMonitor monitor;

    internal protected bool isRunning;
    internal protected bool isGrounded;
    internal protected byte jumpCount;
    internal protected Action onPlayerFell;
    internal protected Rigidbody rb;
    internal protected bool playerOnTargetPlatform;
    internal protected int PlayerFellNum { get; private set; }

    private Vector3 customGravity = new Vector3(0, -25f, 0);
    private float sideTargetPos;

    [SerializeField] private SPlayerLifeController liveController;
    [SerializeField] private SPlatform platform;
    [SerializeField] private SGameUi gameUi;  
    [SerializeField] private SPlayerLifeController playerLifeController;
    [SerializeField] private STouchDetection touchDetection;
    [SerializeField] private SPlayerSoundController soundController;

    private void Start()
    {
        touchDetection.TouchEvent += OnTouch;
        rb = GetComponent<Rigidbody>();
        Physics.gravity = customGravity;
        SetNewPlayerPos();
    }
    private void OnDisable()
    {
        touchDetection.TouchEvent -= OnTouch;
    }
    
    private void Update()
    {
        KeyControll();
        if (gameUi.GetRunTimer < 1)
            isRunning = true;
        if (playerOnTargetPlatform || playerLifeController.playerDied)
            isRunning = false;
        if (transform.position.x + 1 > lastElevator.transform.position.x && 
            transform.position.y > lastElevator.transform.position.y)
            playerOnTargetPlatform = true;
        else
            playerOnTargetPlatform = false;
        CheckFallPlayer();
    }
    private void FixedUpdate()
    {
        if (!playerLifeController.playerDied)
        {
            SideMovement();
            Running(isRunning);
        }
    }
    private void OnTouch(STouchDetection.ActionTipe action) => Movement(action);
    private void KeyControll()
    {
        const float stepSize = 1.3f;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            soundController.PlayCharacterJumpSound();
            Jumping();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (sideTargetPos < stepSize)
                sideTargetPos += stepSize;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (sideTargetPos > -stepSize)
                sideTargetPos -= stepSize;
        }
    }


    private void Movement(STouchDetection.ActionTipe action)
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
                soundController.PlayCharacterJumpSound();
                Jumping();
                break;
            case STouchDetection.ActionTipe.Hit:
                soundController.PlayHitSound();
                break;
        }
    }
    private void SideMovement()
    {
        float speed = 10f;
        Vector3 newPos = new Vector3(transform.position.x, transform.position.y, sideTargetPos);

        transform.position = Vector3.MoveTowards(transform.position, newPos, speed * Time.deltaTime);
    }


    private void Jumping()
    {
        float jumpForce = 8.5f;

        if (jumpCount < 2 && !playerLifeController.playerDied)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            jumpCount++;
        }
    }

    private void Running(bool running)
    {
        float speed = 0.15f;

        if (running)
        {
            Vector3 movement = new Vector3(1, 0, 0);
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
    private void CheckFallPlayer()
    {
        float playerPosY = transform.position.y;
        float platformPosMaxY = platform.GetRenderPlatformInfo(1).bounds.max.y;
        float distance = 5;

        if (playerPosY < platformPosMaxY - distance)
        {
            onPlayerFell?.Invoke();
            SetNewPlayerPos();
            PlayerFellNum++;
            soundController.PlayCharacterFallingSound();
        }
    }
}
