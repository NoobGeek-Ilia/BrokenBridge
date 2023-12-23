using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SHeartSoundController : MonoBehaviour
{
    [SerializeField] AudioSource heartSound;

    internal protected void PlaySound()
    {
        heartSound.PlayOneShot(heartSound.clip);
    }
}
