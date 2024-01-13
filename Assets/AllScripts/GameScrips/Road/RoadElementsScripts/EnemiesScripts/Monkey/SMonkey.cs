using UnityEngine;

public class SMonkey : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    private int timer;
    private const int maxTime = 500;
    private const int minTime = 100;
    private const int jumpForce = 10;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        timer = Random.Range(minTime, maxTime);
    }
    private void Update()
    {
        Jumping();
    }

    void Jumping()
    {
        timer--;
        if(timer < 1)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("isJump");
            timer = Random.Range(minTime, maxTime);
        }    
    }
}
