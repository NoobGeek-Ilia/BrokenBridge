using System.Collections.Generic;
using UnityEngine;

public class SHeart : MonoBehaviour
{
    [SerializeField] GameObject heartPrefab;
    [SerializeField] Transform heartContainer;
    List<GameObject> allHearts = new List<GameObject>();

    private void FixedUpdate()
    {
        if (allHearts != null)
        {
            RotateHearts();
        }
    }
    internal protected void CreateHeartsWay(SBridge bridge)
    {
        for (int currRow = 0; currRow < bridge.copyBridgeParticle.Count / 3; currRow++)
        {
            for (int i = 0; i < SBridge.widthBridge; i++)
            {
                int currCell = (currRow * 3) + i;
                int probCellFill = 40; // probability of filling a cell
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
        allHearts.Add(Instantiate(heartPrefab, spawnPosition, heartPrefab.transform.rotation, heartContainer));
    }
    void RotateHearts()
    {
        int rotateSpeed = 2;
        foreach (GameObject go in allHearts)
        {
            go.transform.Rotate(0, rotateSpeed, 0, Space.World);
        }
    }
    internal protected void ResetHeartsWay()
    {

        foreach (GameObject go in allHearts)
        {
            if (go != null)
                Destroy(go);
        }

        allHearts.Clear();
    }
}
