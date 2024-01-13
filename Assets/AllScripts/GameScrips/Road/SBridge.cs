using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBridge : MonoBehaviour
{
    public List<GameObject> copyBridgeParticle = new List<GameObject>();
    public List<GameObject> brokenBridgePart = new List<GameObject>();
    public List<GameObject> brokenBodyBridgePart = new List<GameObject>();
    public GameObject bridgeBody;
    public SPlatform platform;
    public SBridgeSpawner bridgeSpawner;
    public Transform bridgeBodyTransform;
    public bool bridgeIsFalling;

    internal protected List<bool> CellIsEmpty = new List<bool>();
    internal protected const int widthBridge = 3;
    internal protected Action onBridgeComplited;

    private StateMonitor stateMonitor;
    private int newTopBridge;
    private const int heightBridge = 1;
    private float timerBuildBridge = 0f;
    private float intervalBuilding = 0.05f;
    private SBuildMaterialController buildMaterialController;
    private SAudioManager audioManager;

    private void Start()
    {
        stateMonitor = FindObjectOfType<StateMonitor>();
        buildMaterialController = FindObjectOfType<SBuildMaterialController>();
        bridgeSpawner = FindObjectOfType<SBridgeSpawner>();
        bridgeBody = GameObject.Find($"Bridge{bridgeSpawner.currBridge - 1}");
        platform = FindObjectOfType<SPlatform>();
        bridgeBodyTransform = bridgeBody.transform;
        audioManager = FindObjectOfType<SAudioManager>();
    }
    private void Update()
    {
        LowerBridge();
    }

    public void BuildBridge()
    {
        timerBuildBridge += Time.deltaTime;
        if (timerBuildBridge >= intervalBuilding)
        {
            Vector3 bridgeParticleSize = bridgeSpawner.bridgeParticlePrefab.GetComponent<Renderer>().bounds.size;
            float bridgeParticleWidth = bridgeParticleSize.x;
            float bridgeParticleHeight = bridgeParticleSize.y;
            float bridgeParticleDepth = bridgeParticleSize.z;

            for (int i = newTopBridge; i < newTopBridge + heightBridge; i++)
            {
                for (int j = 0; j < widthBridge; j++)
                {
                    float newPartPos_x = transform.position.x + (bridgeParticleWidth / 2);
                    float newPartPos_y = transform.position.y + 0.4f + (i * bridgeParticleHeight);
                    float newPartPos_z = (transform.position.z - (j * bridgeParticleDepth)) - (bridgeParticleDepth / 2);

                    Vector3 newStartPosBridgeParticle = new Vector3(newPartPos_x, newPartPos_y, newPartPos_z);
                    copyBridgeParticle.Add(Instantiate(bridgeSpawner.bridgeParticlePrefab, newStartPosBridgeParticle, 
                        bridgeSpawner.bridgeParticlePrefab.transform.rotation));
                }
            }

            for (int i = 0; i < copyBridgeParticle.Count; i++)
            {
                copyBridgeParticle[i].transform.SetParent(bridgeBodyTransform, true);
            }

            newTopBridge += heightBridge;
            timerBuildBridge = 0;
            if (audioManager != null)
                audioManager.PlaySound(0);
        }

    }


    public void PushBridgeBody()
    {
        bridgeIsFalling = true;
        if (audioManager != null)
            audioManager.PlaySound(1);
        for (int i = 0; i < copyBridgeParticle.Count; ++i)
        {
            if (copyBridgeParticle[i].GetComponent<SParticle>() == null)
                copyBridgeParticle[i].AddComponent<SParticle>();
        }
    }

    private void LowerBridge()
    {
        const float speed = 150;
        if (bridgeIsFalling)
            bridgeBody.transform.Rotate(Vector3.back, speed * Time.deltaTime);
    }

    internal protected float[] GetCellPosZ { get; private set; } = new float[widthBridge];

    private void SetCellPosZ()
    {
        for (int i = 0; i < widthBridge; i++)
        {
            GetCellPosZ[i] = copyBridgeParticle[i].transform.position.z;
        }
    }
    public void CheckBridgeColl()
    {
        Renderer render = copyBridgeParticle[copyBridgeParticle.Count - 1].GetComponent<Renderer>();
        
        float lastParticleMaxPos_x = render.bounds.max.x;
        float lastParticleMinPos_x = render.bounds.min.x;
        float nextPlatformMaxPos_x = platform.GetRenderPlatformInfo(platform.currentIndexPlatform + 1).bounds.max.x;
        float nextPformMinPos_x = platform.GetRenderPlatformInfo(platform.currentIndexPlatform + 1).bounds.min.x;

        if (lastParticleMinPos_x > nextPlatformMaxPos_x || lastParticleMaxPos_x < nextPformMinPos_x)
        {
            if (audioManager != null)
                audioManager.PlaySound(3);
            SplitBringe();
            stateMonitor.BrokeBridgeNum++;
        }
        else
        {
            if (audioManager != null)
                audioManager.PlaySound(2);
            onBridgeComplited?.Invoke();
            SetCellPosZ();
            CutBridgeResetList();
        }
        buildMaterialController.LoseMaterial();
    }
    internal protected void SplitBringe()
    {     
        foreach (GameObject part in copyBridgeParticle)
        {
            Destroy(part);
        }
        copyBridgeParticle.Clear();
        ResetBridge();
    }
    private void ResetBridge()
    {
        newTopBridge = 0;
        bridgeBody.transform.localRotation = Quaternion.identity;
    }
    private void CutBridgeResetList()
    {
        float nextPformMinPos_x = platform.GetRenderPlatformInfo(platform.currentIndexPlatform + 1).bounds.min.x;
        foreach (GameObject part in copyBridgeParticle)
        {
            Renderer render = part.GetComponent<Renderer>();
            float currParticleMinPos_x = render.bounds.min.x + 0.1f;
            if (currParticleMinPos_x > nextPformMinPos_x)
                brokenBridgePart.Add(part);
        }
        foreach (GameObject part in brokenBridgePart)
        {
            copyBridgeParticle.Remove(part);
        }
        BreakBodyClomplitedBridge(copyBridgeParticle);
        DeliteBridgePart();
        PushBrokenBodyPartsDown();
        
        bridgeSpawner.brideComplite = true;
        transform.localRotation = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, -90f);
    }

    private void DeliteBridgePart()
    {
        foreach (GameObject obj in brokenBridgePart)
        {
            Destroy(obj);
        }
        brokenBridgePart.Clear();
    }
    private void BreakBodyClomplitedBridge(List<GameObject> cbp)
    {
        for (int i = 0; i < cbp.Count;  i++)
            CellIsEmpty.Add(false);
        int randNum;
        int probability = -3 * SBoxPanel.SelectedSet + 13;
        for (int i = 0; i < cbp.Count; i++)
        {
            randNum = UnityEngine.Random.Range(0, probability);
            if (randNum == 0)
            {
                brokenBodyBridgePart.Add(cbp[i]);
                CellIsEmpty[i] = true;
            }
        }
        StartCoroutine(DestroyBrokenComplitedPart());
    }
    private IEnumerator DestroyBrokenComplitedPart()
    {
        float centerPlatfomY = platform.GetPlatformPositionInfo().y;
        bool anyPartFeltDown = false;
        if (brokenBodyBridgePart.Count > 0)
        {
            while (!anyPartFeltDown)
            {
                for (int i = 0; i < brokenBodyBridgePart.Count; i++)
                {
                    if (brokenBodyBridgePart[i].transform.position.y < -20)
                        anyPartFeltDown = true;
                }
                yield return null;
            }
        }
        if (anyPartFeltDown)
        {
            foreach (GameObject obj in brokenBodyBridgePart)
            {
                Destroy(obj);
            }
        }
    }
    private void PushBrokenBodyPartsDown()
    {
        foreach (GameObject obj in brokenBodyBridgePart)
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.AddForce(Vector3.up * 12, ForceMode.Impulse);
        }
    }
}
