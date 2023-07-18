using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SPlayerMovement : MonoBehaviour
{
    private bool isRunning;
    private bool isGrounded;
    private byte jumpCount;
    private Rigidbody rb;
    Vector3 customGravity = new Vector3(0, -25f, 0);
    protected internal bool playerOnTargetPlatform;
    //public SPlatform platform;
    public SLastElevator lastElevator;
    public SFirstElevator firstElevator;
    private Animator animator;
    public SCamera mainCamera;
    public StateMonitor monitor;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Physics.gravity = customGravity;
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        Debug.Log($"jumpcount {jumpCount}");
        Jumping();
        Fighting();
        if (mainCamera.cameraBehindPlayer && monitor.timer < 1)
            isRunning = true;
        if (playerOnTargetPlatform)
        {
            isRunning = false;
            animator.SetBool("isRunning", false);
        }

        if (transform.localPosition.x + 1 > lastElevator.transform.localPosition.x)
            playerOnTargetPlatform = true;
        else
            playerOnTargetPlatform = false;
    }
    private void FixedUpdate()
    {
        Movement(isRunning);
    }
    void Movement(bool running)
    {
        float speed = 0.15f;
        if (running)
        {
            //running
            float moveStraight = -Input.GetAxis("Horizontal");
            Vector3 movement = new Vector3(1, 0f, moveStraight);
            transform.position += movement * speed;
            if (isGrounded)
            {
                jumpCount = 0;
                animator.SetBool("isRunning", true);
            }
            else
                animator.SetBool("isRunning", false);
        }
    }
    void Jumping()
    {
        float jumpForce = 7.5f;
        if (jumpCount < 2)
        {
            animator.SetBool("DoubleJump", false);
        }
        if (Input.GetButtonDown("Jump"))
        {
            if (jumpCount < 2)
            {
                animator.SetBool("isJumping", true);
                
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isGrounded = false;
                jumpCount++;
            }
            if (jumpCount > 1)
                animator.SetBool("DoubleJump", true);
        }
    }

    void Fighting()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            animator.SetTrigger("isHitting");
        }

    }

    public void SetNewPlayerPos()
    {
        float newX = firstElevator.transform.position.x;
        float newY = firstElevator.transform.position.y + 0.5f;
        float newZ = firstElevator.transform.position.z;
        transform.position = new Vector3(newX, newY, newZ);
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlatformTag") || collision.gameObject.CompareTag("BridgeParticleTag"))
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
    }
}
