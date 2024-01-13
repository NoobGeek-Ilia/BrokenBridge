using UnityEngine;

public class SAudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource soundEffect;
    [SerializeField] private AudioClip[] buildSound;
    private void Start()
    {
        soundEffect = GetComponent<AudioSource>();
    }
    public void PlaySound(int effect)
    {
        soundEffect.clip = buildSound[effect];
        soundEffect.PlayOneShot(soundEffect.clip);
    }
}
