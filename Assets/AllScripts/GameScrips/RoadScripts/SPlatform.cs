using System.Collections.Generic;
using UnityEngine;

public class SPlatform : MonoBehaviour
{
    public int currentIndexPlatform;
    public StateMonitor monitor;
    public SRoad road;
    public GameObject platform;
    public List<GameObject> copyPlatform = new List<GameObject>();
    protected internal int[] platforms = { 6, 5, 6, 7, 8, 9, 10, 11, 12}; //количество платформ (не индексы)
    private Renderer render;
    private Renderer renderLastPlatform;
    public SBridgeSpawner bridgeSpawner;
    public SCameraBody camBody;
    protected internal float GetRender_xPos { get; private set; }
    protected internal float GetRender_yPos { get; private set; }
    protected internal float GetRender_zPos { get; private set; }

    protected internal float GetRenderLastPlatform_xPos { get; private set; }
    protected internal float GetRenderLastPlatform_yPos { get; private set; }
    protected internal float GetRenderLastPlatform_zPos { get; private set; }

    public Transform platformsTransform;


    private void Awake()
    {
        AddNewPlatform();
        RenderInit();
    }
    void Update()
    {
        if (currentIndexPlatform != platforms[SLevelPanel.levelNumber] && bridgeSpawner.brideComplite)
        {
            currentIndexPlatform++;
            RenderInit();
            bridgeSpawner.brideComplite = false;
        }
    }
    public void RenderInit()
    {
        //передали в переменную инфу о положении текущей платформы
        render = copyPlatform[currentIndexPlatform].GetComponent<Renderer>();
        GetRender_xPos = render.bounds.max.x;
        GetRender_yPos = render.bounds.max.y;
        GetRender_zPos = render.bounds.max.z;

        renderLastPlatform = copyPlatform[copyPlatform.Count - 1].GetComponent<Renderer>();
        GetRenderLastPlatform_xPos = renderLastPlatform.bounds.max.x;
        GetRenderLastPlatform_yPos = renderLastPlatform.bounds.max.y;
        GetRenderLastPlatform_zPos = renderLastPlatform.bounds.max.z;

    }
    public void AddNewPlatform()
    {
        const float firstPlatformPosX = -2.85f;
        const float firstPlatformPosY = -15f;
        const float minDistance = 5f;
        const float maxDistance = 10f;
        const float stepSize = 1f;
        const float minScaleX = 0.5f;
        const float maxScaleX = 1f;
        Vector3 localScale;

        copyPlatform.Add(Instantiate(platform, Vector3.zero, Quaternion.identity, platformsTransform));
        copyPlatform[0].transform.localPosition = new Vector3(firstPlatformPosX, firstPlatformPosY, 0);

        for (int i = 1; i < platforms[SLevelPanel.levelNumber]; i++)
        {
            float randomScale = Random.Range(minScaleX, maxScaleX);
            float randomDistance = Random.Range(minDistance, maxDistance);
            float roundedDistance = Mathf.Round(randomDistance / stepSize) * stepSize;
            float newPlatformPos_x = ((copyPlatform[i - 1].transform.localPosition.x + (copyPlatform[i - 1].transform.localScale.x / 2)) + roundedDistance);
            float newPlatformPos_y = copyPlatform[i - 1].transform.localPosition.y;
            Vector3 newPlatformPos = new Vector3(newPlatformPos_x, newPlatformPos_y, 0);
            copyPlatform.Add(Instantiate(platform, Vector3.zero, Quaternion.identity, platformsTransform));
            if (i == platforms[SLevelPanel.levelNumber] - 1)
                localScale = platform.transform.localScale;
            else
                localScale = new Vector3(randomScale, platform.transform.localScale.y, platform.transform.localScale.z);
            copyPlatform[i].transform.localScale = localScale;
            copyPlatform[i].transform.localPosition = newPlatformPos;
            
        }
    }







}
