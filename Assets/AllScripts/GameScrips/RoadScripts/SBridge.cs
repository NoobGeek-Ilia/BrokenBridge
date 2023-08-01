using System.Collections.Generic;
using UnityEngine;

public class SBridge : MonoBehaviour
{
    public List<GameObject> copyBridgeParticle = new List<GameObject>();
    public List<GameObject> brokenBridgePart = new List<GameObject>();
    public List<GameObject> brokenBodyBridgePart = new List<GameObject>();
    internal protected List<bool> CellIsEmpty = new List<bool>();

    public GameObject bridgeParticle;
    public GameObject bridgeBody;
    public SPlatform platform;
    public SBridgeSpawner bridgeSpawner;
    public Transform bridgeBodyTransform;
    private int newTopBridge;
    public bool bridgeIsFalling;
    internal protected const int widthBridge = 3;
    const int heightBridge = 1;

    private float timerBuildBridge = 0f;
    private float intervalBuilding = 0.05f;


    private void Start()
    {
        bridgeSpawner = FindObjectOfType<SBridgeSpawner>();
        bridgeParticle = Resources.Load<GameObject>("Prefabs/WoodenBlock");
        bridgeBody = GameObject.Find($"Bridge{bridgeSpawner.currBridge - 1}");
        platform = FindObjectOfType<SPlatform>();
        bridgeBodyTransform = bridgeBody.transform;
    }
    private void Update()
    {
        LowerBridge();
    }

    public void BuildBringe()
    {
        timerBuildBridge += Time.deltaTime;
        if (timerBuildBridge >= intervalBuilding)
        {
            Vector3 bridgeParticleSize = bridgeParticle.GetComponent<MeshRenderer>().bounds.size;
            float bridgeParticleWidth = bridgeParticleSize.x;
            float bridgeParticleHeight = bridgeParticleSize.y;
            float bridgeParticleDepth = bridgeParticleSize.z;

            for (int i = newTopBridge; i < newTopBridge + heightBridge; i++)
            {
                for (int j = 0; j < widthBridge; j++)
                {
                    float newPartPos_x = transform.position.x;
                    float newPartPos_y = platform.GetRender_yPos + (bridgeParticleHeight / 2) + (i * bridgeParticleHeight);
                    float newPartPos_z = (platform.GetRender_zPos - (j * bridgeParticleDepth)) - (bridgeParticleDepth / 2);

                    Vector3 newStartPosBridgeParticle = new Vector3(newPartPos_x, newPartPos_y, newPartPos_z);
                    copyBridgeParticle.Add(Instantiate(bridgeParticle, newStartPosBridgeParticle, bridgeParticle.transform.rotation));
                }
            }

            for (int i = 0; i < copyBridgeParticle.Count; i++)
            {
                copyBridgeParticle[i].transform.SetParent(bridgeBodyTransform, true);
            }

            newTopBridge += heightBridge;
            timerBuildBridge = 0;
        }
    }




    public void PushBridgeBody()
    {
        bridgeIsFalling = true;
        for (int i = widthBridge * 2; i < copyBridgeParticle.Count; ++i)
        {
            if (copyBridgeParticle[i].GetComponent<SParticle>() == null)
                copyBridgeParticle[i].AddComponent<SParticle>();
        }
    }
    void LowerBridge()
    {
        const float speed = 150;
        if (bridgeIsFalling)
            bridgeBody.transform.Rotate(Vector3.back, speed * Time.deltaTime);
    }

    public void CheckBridgeColl()
    {
        
        double lastParticleMaxPos_x = copyBridgeParticle[copyBridgeParticle.Count - 1].transform.position.x + 
            copyBridgeParticle[copyBridgeParticle.Count - 1].transform.localScale.x + 1;
        double lastParticleMinPos_x = copyBridgeParticle[copyBridgeParticle.Count - 1].transform.position.x - 
            copyBridgeParticle[copyBridgeParticle.Count - 1].transform.localScale.x - 1;
        double platformMaxPos_x = platform.copyPlatform[platform.currentIndexPlatform + 1].transform.position.x +
            (platform.transform.localScale.x / 2);
        double platformMinPos_x = platformMaxPos_x - platform.transform.localScale.x;

        if (lastParticleMinPos_x > platformMaxPos_x || lastParticleMaxPos_x < platformMinPos_x)
            SplitBringe();
        else
            CutBridgeResetList();
        
    }
    void SplitBringe()
    {
        foreach (GameObject part in copyBridgeParticle)
        {
            Destroy(part);
        }
        copyBridgeParticle.Clear();
        ResetBridge();
    }
    void ResetBridge()
    {
        newTopBridge = 0;
        bridgeBody.transform.localRotation = Quaternion.identity;
    }
    void CutBridgeResetList()
    {
        double platformMaxPos_x = platform.copyPlatform[platform.currentIndexPlatform + 1].transform.position.x +
            platform.transform.localScale.x;
        double platformMinPos_x = platform.copyPlatform[platform.currentIndexPlatform + 1].transform.position.x - 
            platform.transform.localScale.x;

        foreach (GameObject part in copyBridgeParticle)
        {
            double currParticleMaxPos = part.transform.position.x;
            if (currParticleMaxPos < platformMaxPos_x && currParticleMaxPos > platformMinPos_x)
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
    void BreakBodyClomplitedBridge(List<GameObject> cbp)
    {
        for (int i = 0; i < cbp.Count;  i++)
            CellIsEmpty.Add(false);
        int randNum;
        int probability = 15;
        for (int i = 0; i < cbp.Count; i++)
        {
            randNum = Random.Range(0, probability);
            if (randNum == 0)
            {
                brokenBodyBridgePart.Add(cbp[i]);
                CellIsEmpty[i] = true;
            }
        }
    }
    void PushBrokenBodyPartsDown()
    {
        foreach (GameObject obj in brokenBodyBridgePart)
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.None;
            rb.AddForce(Vector3.up * 12, ForceMode.Impulse);
        }
    }
}
