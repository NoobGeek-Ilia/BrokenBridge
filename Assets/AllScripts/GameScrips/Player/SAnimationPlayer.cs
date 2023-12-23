using UnityEngine;

public class SAnimationPlayer : MonoBehaviour
{
    internal protected Animator animator;
    public SPlayerMovement playerMovement;
    [SerializeField] StateMonitor monitor;
    [SerializeField] STouchDetection touchDetection;
    private void Start()
    {
        animator = GetComponent<Animator>();
        touchDetection.TouchEvent += OnTouch;
    }

    private void OnDisable()
    {
        touchDetection.TouchEvent -= OnTouch;
    }

    private void OnTouch(STouchDetection.ActionTipe action)
    {
        if (action == STouchDetection.ActionTipe.Hit)
            animator.SetTrigger("isHitting");
    }

    private void Update()
    {

        animator.SetBool("isRunning", playerMovement.isRunning);
        JumpAnimation(playerMovement.jumpCount);
        
    }

    void JumpAnimation(int jumpCount)
    {
        switch (jumpCount)
        {
            case 1:
                animator.SetBool("DoubleJump", false);
                animator.SetBool("isJumping", true);
                break;
            case 2:
                animator.SetBool("isJumping", false);
                animator.SetBool("DoubleJump", true);
                
                break;
            default:                animator.SetBool("isJumping", false);
                animator.SetBool("DoubleJump", false);
                break;
        }
    }
}
