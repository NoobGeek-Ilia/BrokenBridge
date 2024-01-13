using System;
using UnityEngine;

public class SRoad : MonoBehaviour
{
    public SPlatform platform;
    public SCamera cam;
    public bool roadComplite { get; private set;  }

    internal protected Action onRoadComplited;
    internal protected bool eventWasCalled;


    private void Update()
    {
        CheckCompliteRoad();
        if (cam.cp == SCamera.CameraPosition.Run)
            roadComplite = false;
    }
    private void CheckCompliteRoad()
    {
        if (!eventWasCalled)
        {
            if (platform.currentIndexPlatform + 1 == platform.copyPlatform.Count)
            {
                onRoadComplited?.Invoke();
                eventWasCalled = true;
            }
        }
        if (platform.currentIndexPlatform + 1 == platform.copyPlatform.Count)
            roadComplite = true;
    }
}
