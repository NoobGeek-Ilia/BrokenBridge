using System;
using System.Collections.Generic;
using UnityEngine;

public class SBridgeSpawner : MonoBehaviour
{
    public SPlatform platform;
    public bool brideComplite;
    public List<GameObject> bridges = new List<GameObject>();

    internal protected int currBridge;
    internal protected Action onBridgeSet;
    internal protected GameObject bridgeParticlePrefab;

    private Vector3 getBoundsPlatform;
    private GameObject emptyObject;
    private bool newBridgeHasSet;

    private void Start()
    {
        SetPrefab();
        SetNewPosBridgeBody();
    }

    private void Update()
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
    private void SetPrefab()
    {
        string[] model = { "Wood", "Ice", "Glass", "Leana" };
        bridgeParticlePrefab = Resources.Load<GameObject>($"Prefabs/BridgeParticles/Block_{model[SBoxPanel.SelectedSet]}");
    }

    private void ResetVariable()
    {
        newBridgeHasSet = false;
    }

    private void SetNewPosBridgeBody()
    {
        int numPlatform = platform.currentIndexPlatform;
        
        if (bridges.Count > 0)
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
