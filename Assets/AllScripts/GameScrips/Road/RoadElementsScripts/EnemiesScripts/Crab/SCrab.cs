using UnityEngine;

public class SCrab : MonoBehaviour
{
    private SPlayerMovement playerMovement;
    private Vector3 playerPosition;
    private const int offset = 15;
    private float speed = 1f;

    private void Start()
    {
        playerMovement = FindObjectOfType<SPlayerMovement>();
    }
    private void Update()
    {
        playerPosition = playerMovement.transform.position;
        if (playerPosition.x > transform.position.x - offset)
            CrabMovement();
    }
    private void CrabMovement()
    {
        Vector3 startPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 endPos = new Vector3(transform.position.x, transform.position.y, playerPosition.z);

        transform.position = Vector3.Lerp(startPos, endPos, Time.deltaTime * speed);
    }
}
