using UnityEngine;

public class SPlayerSoundController : MonoBehaviour
{
    [SerializeField] private AudioSource[] charSound;
    [SerializeField] private AudioClip[] jumpClip;
    [SerializeField] private AudioClip[] gettingHitClip;
    [SerializeField] private AudioClip[] fallingClip;
    [SerializeField] private AudioSource hitSource;

    private int selectedCharacter = SGlobalGameInfo.selectedCharacter;

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
