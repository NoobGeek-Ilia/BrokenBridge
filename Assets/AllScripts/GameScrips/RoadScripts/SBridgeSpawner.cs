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
    void Start() => CreateFirstBridge();

    void Update()
    {
        if (brideComplite)
        {
            GameObject emptyObject = new GameObject($"Bridge{currBridge}");
            emptyObject.AddComponent<SBridge>();
            emptyObject.transform.SetParent(SBS.transform, true);
            emptyObject.transform.position = new Vector3(platform.copyPlatform[platform.currentIndexPlatform + 1].transform.position.x + 
                platform.copyPlatform[platform.currentIndexPlatform + 1].transform.localScale.x, platform.GetRender_yPos, platform.GetRender_zPos);
            currBridge++;
            bridges.Add(emptyObject);
        }
    }
    public void CreateFirstBridge()
    {
        GameObject emptyObject = new GameObject($"Bridge{0}");
        emptyObject.AddComponent<SBridge>();
        emptyObject.transform.SetParent(SBS.transform, true);
        emptyObject.transform.position = new Vector3(platform.GetRender_xPos, platform.GetRender_yPos, platform.GetRender_zPos);
        bridges.Add(emptyObject);
    }
}
