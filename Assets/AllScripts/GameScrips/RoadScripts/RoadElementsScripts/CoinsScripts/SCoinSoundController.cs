using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCoinSoundController : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    internal protected void PlayGetCoinSound()
    {
        //audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(audioSource.clip);
    }
}
