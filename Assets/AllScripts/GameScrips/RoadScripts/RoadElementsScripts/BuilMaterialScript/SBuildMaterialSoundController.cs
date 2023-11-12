using UnityEngine;

public class SBuildMaterialSoundController : MonoBehaviour
{
    [SerializeField] AudioSource materialSound;

    internal protected void PlaySound()
    {
        materialSound.PlayOneShot(materialSound.clip);
    }
}
