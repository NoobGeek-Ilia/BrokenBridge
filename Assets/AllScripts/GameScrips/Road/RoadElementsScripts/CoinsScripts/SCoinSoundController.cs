using UnityEngine;

public class SCoinSoundController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    internal protected void PlayGetCoinSound()
    {
        audioSource.PlayOneShot(audioSource.clip);
    }
}