using System.Collections.Generic;
using UnityEngine;

public class SBuildMaterial : MonoBehaviour
{
    [SerializeField] InitRoadFilling roadFilling;
    [SerializeField] GameObject materialPrefab;
    [SerializeField] Transform materialContainer;
    List<GameObject> allMaterials = new List<GameObject>();

    private void FixedUpdate()
    {
        if (allMaterials != null)
        {
            RotateCoins();
        }
    }
    internal protected void CreateMaterialsWay(SBridge bridge)
    {
        for (int currRow = 0; currRow < bridge.copyBridgeParticle.Count / 3; currRow++)
        {
            for (int i = 0; i < SBridge.widthBridge; i++)
            {
                int currCell = (currRow * 3) + i;
                int probCellFill = 20; // probability of filling a cell
                int randCell = Random.Range(0, probCellFill);
                bool cellIsEmpty = bridge.CellIsEmpty[currCell];
                bool cellIsNone = randCell < 1;
                if (cellIsEmpty || !cellIsNone) //continue if cell is empty or none
                    continue;
                else
                    SetPosAndInst(currCell, bridge);
            }
        }
    }
    void SetPosAndInst(int currCell, SBridge bridge)
    {
        float distanceToBridge = 2;
        float spawnX = bridge.copyBridgeParticle[currCell].transform.position.x;
        float spawnY = bridge.copyBridgeParticle[currCell].transform.position.y + distanceToBridge;
        float spawnZ = bridge.copyBridgeParticle[currCell].transform.position.z;
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, spawnZ);
        allMaterials.Add(Instantiate(materialPrefab, spawnPosition, materialPrefab.transform.rotation, materialContainer));
    }
    void RotateCoins()
    {
        int rotateSpeed = 2;
        foreach (GameObject go in allMaterials)
        {
            go.transform.Rotate(0, rotateSpeed, 0, Space.World);
        }
    }
    internal protected void ResetMaterialWay()
    {

        foreach (GameObject go in allMaterials)
        {
            if (go != null)
                Destroy(go);
        }

        allMaterials.Clear();
    }
}
