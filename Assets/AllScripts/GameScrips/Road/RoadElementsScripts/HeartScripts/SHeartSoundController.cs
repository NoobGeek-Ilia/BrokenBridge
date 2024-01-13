using UnityEngine;

public class SHeartSoundController : MonoBehaviour
{
    [SerializeField] private AudioSource heartSound;

    internal protected void PlaySound()
    {
        heartSound.PlayOneShot(heartSound.clip);
    }
}
