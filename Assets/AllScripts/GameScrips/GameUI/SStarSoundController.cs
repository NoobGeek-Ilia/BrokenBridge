using UnityEngine;

public class SStarSoundController : MonoBehaviour
{
    [SerializeField] private AudioSource starSound;

    internal protected void PlaySound() => starSound.PlayOneShot(starSound.clip);
}
