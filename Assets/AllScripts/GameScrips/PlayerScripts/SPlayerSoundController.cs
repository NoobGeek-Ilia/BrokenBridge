using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPlayerSoundController : MonoBehaviour
{
    [SerializeField] AudioSource[] charSound;
    [SerializeField] AudioClip[] jumpClip;
    [SerializeField] AudioClip[] gettingHitClip;
    [SerializeField] AudioClip[] fallingClip;


    [SerializeField] AudioSource hitSource;

    int selectedCharacter = SGlobalGameInfo.selectedCharacter;

    internal protected void PlayHitSound()
    {
        hitSource.pitch = Random.Range(0.9f, 1.1f);
        hitSource.PlayOneShot(hitSource.clip);
    }

    internal protected void PlayCharacterJumpSound()
    {
        charSound[selectedCharacter].clip = jumpClip[selectedCharacter];
        charSound[selectedCharacter].PlayOneShot(charSound[selectedCharacter].clip);
    }

    internal protected void PlayCharacterGettingHitSound()
    {
        charSound[selectedCharacter].clip = gettingHitClip[selectedCharacter];
        charSound[selectedCharacter].PlayOneShot(charSound[selectedCharacter].clip);
    }

    internal protected void PlayCharacterFallingSound()
    {
        charSound[selectedCharacter].clip = fallingClip[selectedCharacter];
        charSound[selectedCharacter].PlayOneShot(charSound[selectedCharacter].clip);
    }
}
