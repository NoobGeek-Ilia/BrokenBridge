using UnityEngine;

public class SMonkey : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    int timer;
    const int maxTime = 500;
    const int minTime = 100;
    const int jumpForce = 10;

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
