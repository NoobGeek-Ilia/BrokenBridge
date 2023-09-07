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
    public Action onRoadComplited;
    internal protected bool eventWasCalled;
    private void Update()
    {
        CheckCompliteRoad();
        if (cam.cameraBehindPlayer)
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
            }
        }
        //if (platform.currentIndexPlatform + 1 == platform.platforms[SLevelPanel.levelNumber]) - �������� ������ ������� ����� 
        if (platform.currentIndexPlatform + 1 == platform.copyPlatform.Count)
        {

            roadComplite = true;
        }
    }

}
