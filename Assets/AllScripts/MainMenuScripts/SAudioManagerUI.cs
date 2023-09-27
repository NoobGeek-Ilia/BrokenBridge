using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SAudioManagerUI : MonoBehaviour
{
    public AudioSource clickSound;
    public AudioClip[] selectCharacterSound;
    private void Start()
    {
        clickSound = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        clickSound.clip = clip;
        clickSound.PlayOneShot(clip);
    }

    public void SelectPlayer(SCharacterTab characterTab)
    {
        clickSound.clip = selectCharacterSound[SGlobalGameInfo.selectedCharacter];
        clickSound.Play();
    }
}
