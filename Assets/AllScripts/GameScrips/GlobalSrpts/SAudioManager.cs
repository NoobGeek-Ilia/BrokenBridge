using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SAudioManager : MonoBehaviour
{
    [SerializeField] AudioSource soundEffect;
    [SerializeField] AudioClip[] buildSound;
    private void Start()
    {
        soundEffect = GetComponent<AudioSource>();
    }
    public void PlaySound(int effect)
    {
        soundEffect.clip = buildSound[effect];
        soundEffect.PlayOneShot(soundEffect.clip);
    }
    public void PlaySound(int effect, float minPitch, float maxPitch)
    {
        soundEffect.clip = buildSound[effect];
        soundEffect.pitch = Random.Range(minPitch, maxPitch);
        soundEffect.PlayOneShot(soundEffect.clip);
    }
}
