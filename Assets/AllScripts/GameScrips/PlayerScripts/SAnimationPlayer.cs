using UnityEngine;

public class SAnimationPlayer : MonoBehaviour
{
    internal protected Animator animator;
    public SPlayerMovement playerMovement;
    public SPlayerTouchController playerTouchContr;
    internal protected bool hitAnimOn;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {

        animator.SetBool("isRunning", playerMovement.isRunning);
        JumpAnimation(playerMovement.jumpCount);
        Hitting(playerTouchContr.hit);
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
                animator.SetBool("DoubleJump", true);
                animator.SetBool("isJumping", false);
                break;
            default:
                animator.SetBool("isJumping", false);
                animator.SetBool("DoubleJump", false);
                break;
        }
    }

    void Hitting(bool isHitting)
    {
        if (isHitting)
        {
            animator.SetTrigger("isHitting");
            playerTouchContr.hit = false;
            hitAnimOn = true;
        }

    }

    public void OnHittingAnimationEnd()
    {
        playerTouchContr.hit = false;
    }
}
