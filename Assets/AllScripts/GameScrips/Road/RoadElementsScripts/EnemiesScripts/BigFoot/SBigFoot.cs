using UnityEngine;

public class SBigFoot : MonoBehaviour
{
    private Animator animator;
    private SPlayerMovement playerMovement;
    private Vector3 playerPosition;
    private const int offset = 10;

    [SerializeField] private GameObject damageArea;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = FindObjectOfType<SPlayerMovement>();
    }
    private void Update()
    {
        playerPosition = playerMovement.transform.position;
        if (playerPosition.x > transform.position.x - offset)
        {
            animator.SetTrigger("isHit");
        }
        else
            damageArea.SetActive(false);
    }
    public void HitAnimComplited()
    {
        damageArea.SetActive(true);
    }
}
