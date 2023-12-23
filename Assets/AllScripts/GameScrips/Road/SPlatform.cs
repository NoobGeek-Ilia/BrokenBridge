using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SPlatform : MonoBehaviour
{
    public StateMonitor monitor;
    public SRoad road;
    private GameObject platformPrefab;
    private GameObject bridgeParticle;
    public List<GameObject> copyPlatform = new List<GameObject>();
    public SBridgeSpawner bridgeSpawner;
    public TextMeshProUGUI materialsNumTxt;

    private Renderer render;
    public int currentIndexPlatform;
    private readonly Vector3 firstPlatformCenterPos = new Vector3(-4, -20f, 0);
    protected internal int[] platforms = new int[36]; //количество платформ (не индексы)
    protected internal float GetPlatformTop { get; private set; }
    protected internal float GetMaxPlatformZ { get; private set; }
    protected internal int GetPlatformNum { get; private set; }
    private float stepSize;

    private void Awake()
    {
        SetParticlePrefab();
        SetPrefab();
        FillPlatformArray();
        stepSize = bridgeParticle.GetComponent<Renderer>().bounds.size.y;
        AddNewPlatform();
        GetPlatformTop = GetRenderPlatformInfo(0).bounds.max.y;
        GetMaxPlatformZ = GetRenderPlatformInfo(0).bounds.max.z;
    }
    void Update()
    {
        if (currentIndexPlatform != platforms[SBoxPanel.SelectedLevel] && bridgeSpawner.brideComplite)
        {
            currentIndexPlatform++;
            bridgeSpawner.brideComplite = false;
        }
        //Debug.Log($"curr platform:{currentIndexPlatform}");
    }
    private void SetPrefab()
    {
        string[] model = { "Tree", "Glacier", "Skyscraper", "Palm" };
        platformPrefab = Resources.Load<GameObject>($"Prefabs/Platforms/Platform_{model[SBoxPanel.SelectedSet]}");
    }

    private void SetParticlePrefab()
    {
        string[] model = { "Wood", "Ice", "Glass", "Leana" };
        bridgeParticle = Resources.Load<GameObject>($"Prefabs/BridgeParticles/Block_{model[SBoxPanel.SelectedSet]}");
    }
    void FillPlatformArray()
    {
        //int[] pattern = new int[] { 2, 2, 2, 2, 2, 2, 2, 2, 2 };
        int[] pattern = new int[] { 3, 4, 4, 5, 6, 8, 4, 5, 4 };
        int patternLength = pattern.Length;

        for (int i = 0; i < platforms.Length; i++)
        {
            platforms[i] = pattern[i % patternLength];
            
        }
        GetPlatformNum = platforms[SBoxPanel.SelectedLevel];
    }
    internal protected Vector3 GetPlatformPositionInfo()
    {
        return firstPlatformCenterPos;
    }
    internal protected Renderer GetRenderPlatformInfo(int platformNum)
    {
        render = copyPlatform[platformNum].GetComponent<Renderer>();
        return render;
    }
    public void AddNewPlatform()
    {
        const float minDistance = 10f;
        const float maxDistance = 20f;
        float minScaleX = Math.Max(0, (float)(0.8 - 0.1 * SBoxPanel.SelectedSet));
        const float maxScaleX = 1.2f;

        Vector3 localScale;

        copyPlatform.Add(Instantiate(platformPrefab, Vector3.zero, Quaternion.identity, transform));
        copyPlatform[0].transform.position = new Vector3(firstPlatformCenterPos.x, firstPlatformCenterPos.y, firstPlatformCenterPos.z);

        for (int i = 1; i < platforms[SBoxPanel.SelectedLevel]; i++)
        {
            float randomScale = (UnityEngine.Random.Range(minScaleX, maxScaleX) / stepSize) * stepSize;
            float randomDistance = UnityEngine.Random.Range(minDistance, maxDistance);
            float roundedDistance = Mathf.Round(randomDistance / stepSize) * stepSize;
            float lastPlatformMaxPos_x = GetRenderPlatformInfo(i - 1).bounds.max.x;
            float newPlatformPos_x = lastPlatformMaxPos_x + roundedDistance + randomScale * 2;
            float newPlatformPos_y = copyPlatform[i - 1].transform.position.y;
            Vector3 newPlatformPos = new Vector3(newPlatformPos_x, newPlatformPos_y, 0);

            copyPlatform.Add(Instantiate(platformPrefab, Vector3.zero, Quaternion.identity, transform));
            localScale = new Vector3(randomScale, platformPrefab.transform.localScale.y, platformPrefab.transform.localScale.z);
            copyPlatform[i].transform.localScale = localScale;
            copyPlatform[i].transform.position = newPlatformPos;
        }
    }
    public void DestroyPlatforms()
    {
        List<GameObject> objectsToDelete = new List<GameObject>(copyPlatform);

        foreach (GameObject obj in objectsToDelete)
        {
            copyPlatform.Remove(obj);
            Destroy(obj);
        }

        int childCount = transform.childCount;

        for (int i = childCount - 1; i >= 0; i--)
        {
            Transform child = transform.GetChild(i);
            DestroyImmediate(child.gameObject);
        }
    }
}
