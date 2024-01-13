using UnityEngine;
using UnityEngine.UI;

public class SVolume : MonoBehaviour
{
    private bool volume = true;

    [SerializeField] private Sprite[] pic;

    public void VolumeOnOff()
    {
        AudioListener.volume = volume ? 0f : 1.0f;
        GetComponent<Image>().sprite = volume ? pic[0] : pic[1];
        volume = !volume;
    }
}