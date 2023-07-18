using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SRoad : MonoBehaviour
{
    public SPlatform platform;
    public SCamera cam;
    public bool roadComplite { get; private set;  }
    // Start is called before the first frame update
    private void Update()
    {
        CheckCompliteRoad();
        if (cam.cameraBehindPlayer)
            roadComplite = false;
    }
    void CheckCompliteRoad()
    {
        //if (platform.currentIndexPlatform + 1 == platform.platforms[SLevelPanel.levelNumber]) - заменить нижнее условие после 
        if (platform.currentIndexPlatform + 1 == platform.copyPlatform.Count)
        {
            roadComplite = true;
        }
    }

}
