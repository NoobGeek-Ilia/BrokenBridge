using UnityEngine;

public class SBuildMaterialSoundController : MonoBehaviour
{
    [SerializeField] private AudioSource materialSound;

    internal protected void PlaySound() => 
        materialSound.PlayOneShot(materialSound.clip);
}
