using UnityEngine;

public class SBackground : MonoBehaviour
{
    [SerializeField] private AudioSource[] backSound;

    private void Awake()
    {
        ResetAudioSource();
    }

    private void ResetAudioSource()
    {
        for (int i = 0; i < backSound.Length; i++)
        {
            backSound[i].enabled = false;
        }
        backSound[SBoxPanel.SelectedSet].enabled = true;
    }
}