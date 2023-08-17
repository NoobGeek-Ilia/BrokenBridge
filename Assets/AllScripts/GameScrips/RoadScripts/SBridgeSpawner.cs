using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SBridgeSpawner : MonoBehaviour
{
    public SBridgeSpawner SBS;
    public SPlatform platform;
    public int currBridge;
    public bool brideComplite;
    public List<GameObject> bridges = new List<GameObject>();
    void Start()
    {
        CreateFirstBridge();
    }

    void Update()
    {
        if (brideComplite)
        {
            Vector3 getBoundsNextPlatform = platform.GetRenderPlatformInfo(platform.currentIndexPlatform + 1).bounds.max;
            GameObject emptyObject = new GameObject($"Bridge{currBridge}");
            emptyObject.AddComponent<SBridge>();
            emptyObject.transform.SetParent(SBS.transform, true);
            emptyObject.transform.position = new Vector3(getBoundsNextPlatform.x,
                getBoundsNextPlatform.y, getBoundsNextPlatform.z);
            currBridge++;
            bridges.Add(emptyObject);
        }
    }
    public void CreateFirstBridge()
    {
        Vector3 getBoundsCurrentPlatform = platform.GetRenderPlatformInfo(platform.currentIndexPlatform).bounds.max;
        GameObject emptyObject = new GameObject($"Bridge{0}");
        emptyObject.AddComponent<SBridge>();
        emptyObject.transform.SetParent(SBS.transform, true);
        emptyObject.transform.position = new Vector3(getBoundsCurrentPlatform.x, getBoundsCurrentPlatform.y, getBoundsCurrentPlatform.z);
        bridges.Add(emptyObject);
    }
}
