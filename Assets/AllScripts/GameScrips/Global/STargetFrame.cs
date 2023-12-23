using UnityEngine;

public class STargetFrame : MonoBehaviour
{
    public int targetFrameRate = 100;

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFrameRate;
    }
}
