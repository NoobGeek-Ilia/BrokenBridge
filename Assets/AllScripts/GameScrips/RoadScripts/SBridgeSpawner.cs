using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SBridgeSpawner : MonoBehaviour
{
    public SPlatform platform;
    
    public bool brideComplite;
    public List<GameObject> bridges = new List<GameObject>();
    Vector3 getBoundsPlatform;
    GameObject lastBridge;
    GameObject emptyObject;
    internal protected int currBridge;
    bool newBridgeHasSet;
    internal protected Action onBridgeSet;

    void Start()
    {
        SetNewPosBridgeBody();
    }

    void Update()
    {
        emptyObject.GetComponent<SBridge>().onBridgeComplited += ResetVariable;
        if (!newBridgeHasSet)
        {
            SetNewPosBridgeBody();
            onBridgeSet?.Invoke();
        }
    }
    private void OnDisable()
    {
        foreach (var bridge in bridges)
        {
            bridge.GetComponent<SBridge>().onBridgeComplited -= ResetVariable;
        }
    }

    void ResetVariable()
    {
        newBridgeHasSet = false;
    }

    void SetNewPosBridgeBody()
    {
        int numPlatform = platform.currentIndexPlatform;
        
        if (bridges.Count > 0)
            //corutine
            numPlatform++;
        else
            currBridge = 0;
        getBoundsPlatform = platform.GetRenderPlatformInfo(numPlatform).bounds.max;
        emptyObject = new GameObject($"Bridge{currBridge}");
        emptyObject.AddComponent<SBridge>();
        emptyObject.transform.SetParent(transform, true);
        emptyObject.transform.position = getBoundsPlatform;
        currBridge++;
        bridges.Add(emptyObject);
        newBridgeHasSet = true;
        
    }
    internal protected void ResetSpawner()
    {
        int childCount = transform.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            Transform child = transform.GetChild(i);
            DestroyImmediate(child.gameObject);
        }
        currBridge = 0;
        bridges.Clear();
        SetNewPosBridgeBody();
    }
}
