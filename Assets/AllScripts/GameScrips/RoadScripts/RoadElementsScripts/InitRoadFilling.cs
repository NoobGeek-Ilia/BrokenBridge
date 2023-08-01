using UnityEngine;

public class InitRoadFilling : MonoBehaviour
{
    public SBridgeSpawner bridgeSpawner;
    public SRoad road;
    public GameObject[] roadObjects;
    bool roadHasFilled;

    GameObject bridgeBody;
    SBridge bridge;
    int cellIndex;
    int cellTipe;

    private void Start()
    {

    }
    private void Update()
    {
        if (road.roadComplite && !roadHasFilled)
        {
            FillRoad();
        }
    }
    void FillRoad()
    {
        for (int i = 0; i < bridgeSpawner.bridges.Count; i++)
        {
            cellIndex = 0;
            bridgeBody = GameObject.Find($"Bridge{i}");
            bridge = bridgeBody.GetComponent<SBridge>();
            FillBridge(bridge);
        }
        roadHasFilled = true;
    }

    void FillBridge(SBridge bridge)
    {
        const int numTipes = 20;
        for (int i = 0; i < bridge.copyBridgeParticle.Count / 3; i++)
        {
            for (int j = 0; j < SBridge.widthBridge; j++)
            {
                Vector3 spawnPosition = new Vector3(bridge.copyBridgeParticle[j].transform.position.x + i, 
                    bridge.copyBridgeParticle[j].transform.position.y + 0.5f, bridge.copyBridgeParticle[j].transform.position.z);
                if (bridge.CellIsEmpty[cellIndex])
                {
                    //continue;
                }

                cellTipe = Random.Range(0, numTipes);
                switch(cellTipe)
                {
                    case 0:
                        int objTipe = Random.Range(0, roadObjects.Length);
                        float[] distanceToBridge = { 0.6f, 0.4f, 0.5f }; //sp, sl, cr
                        spawnPosition = new Vector3(bridge.copyBridgeParticle[j].transform.position.x + i,
                            bridge.copyBridgeParticle[j].transform.position.y + distanceToBridge[objTipe], bridge.copyBridgeParticle[j].transform.position.z);
                        Instantiate(roadObjects[objTipe], spawnPosition, roadObjects[objTipe].transform.rotation);
                        break;
                    default:
                        break;
                }
            }
        }
    }

}
