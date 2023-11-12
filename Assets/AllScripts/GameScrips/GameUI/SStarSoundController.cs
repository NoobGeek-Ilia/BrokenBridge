using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SStarSoundController : MonoBehaviour
{
    [SerializeField] AudioSource starSound;

    internal protected void PlaySound()
    {
        starSound.PlayOneShot(starSound.clip);
    }
}
