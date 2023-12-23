using UnityEngine;

public class SEnemySoundController : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    internal protected void PlaySmashSound()
    {
        audioSource.pitch = Random.Range(0.8f, 1.1f);
        audioSource.PlayOneShot(audioSource.clip);
    }
}
