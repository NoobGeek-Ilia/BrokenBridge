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

    //быстрое решение, для того чтобы stepSize инициализировалась с актуальными значениями
    //по сути можно использовать класс sBridgeSpawner, но предварительно стоит решить проблему с очередью вызовов 
    private void SetParticlePrefab()
    {
        string[] model = { "Wood", "Ice", "Glass", "Leana" };
        bridgeParticle = Resources.Load<GameObject>($"Prefabs/BridgeParticles/Block_{model[SBoxPanel.SelectedSet]}");
    }
    void FillPlatformArray()
    {
        //int[] pattern = new int[] { 2, 2, 2, 2, 2, 2, 2, 2, 2 };
        int[] pattern = new int[] { 4, 4, 4, 8, 8, 8, 12, 12, 12 };
        int patternLength = pattern.Length;

        for (int i = 0; i < platforms.Length; i++)
        {
            platforms[i] = pattern[i % patternLength];
            Debug.Log(platforms[i]);
        }

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
        const float minScaleX = 0.2f;
        const float maxScaleX = 1.2f;

        Vector3 localScale;

        copyPlatform.Add(Instantiate(platformPrefab, Vector3.zero, Quaternion.identity, transform));
        copyPlatform[0].transform.position = new Vector3(firstPlatformCenterPos.x, firstPlatformCenterPos.y, firstPlatformCenterPos.z);

        for (int i = 1; i < platforms[SBoxPanel.SelectedLevel]; i++)
        {
            float randomScale = (Random.Range(minScaleX, maxScaleX) / stepSize) * stepSize;
            float randomDistance = Random.Range(minDistance, maxDistance);
            float roundedDistance = Mathf.Round(randomDistance / stepSize) * stepSize;
            float lastPlatformMaxPos_x = GetRenderPlatformInfo(i - 1).bounds.max.x;
            float newPlatformPos_x = lastPlatformMaxPos_x + roundedDistance + randomScale * 2;
            float newPlatformPos_y = copyPlatform[i - 1].transform.position.y;
            Vector3 newPlatformPos = new Vector3(newPlatformPos_x, newPlatformPos_y, 0);

            copyPlatform.Add(Instantiate(platformPrefab, Vector3.zero, Quaternion.identity, transform));
            if (i == platforms[SBoxPanel.SelectedLevel] - 1)
                localScale = platformPrefab.transform.localScale;
            else
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
