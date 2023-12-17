using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SRoad : MonoBehaviour
{
    public SPlatform platform;
    public SCamera cam;
    public bool roadComplite { get; private set;  }
    // Start is called before the first frame update
    internal protected Action onRoadComplited;
    internal protected bool eventWasCalled;
    [SerializeField] GameObject firstElevator;

    private void Update()
    {
        CheckCompliteRoad();
        if (cam.cp == SCamera.CameraPosition.Run)
            roadComplite = false;
    }
    void CheckCompliteRoad()
    {
        if (!eventWasCalled)
        {
            if (platform.currentIndexPlatform + 1 == platform.copyPlatform.Count)
            {
                onRoadComplited?.Invoke();
                eventWasCalled = true;
                firstElevator.SetActive(true);
            }
        }
        if (platform.currentIndexPlatform + 1 == platform.copyPlatform.Count)
            roadComplite = true;
    }
}
