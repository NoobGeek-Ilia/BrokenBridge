using UnityEngine;

public class SDamageArea : MonoBehaviour
{
    private SPlayerMovement playerMovement;
    private void Start()
    {
        playerMovement = FindObjectOfType<SPlayerMovement>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerMovement.transform.position = new Vector3(playerMovement.transform.position.x - 1, playerMovement.transform.position.y,
                playerMovement.transform.position.z);
            gameObject.SetActive(false);
        }
    }
}
