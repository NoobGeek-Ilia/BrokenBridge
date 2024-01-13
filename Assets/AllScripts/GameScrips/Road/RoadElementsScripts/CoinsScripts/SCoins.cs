using System.Collections.Generic;
using UnityEngine;

public class SCoins : MonoBehaviour
{
    public List<GameObject> allCoins = new List<GameObject>();
    public GameObject coinPrefab;
    public SPlatform platform;
    public Transform coins;
    public SBridgeSpawner bridgeSpawner;
    public SRoad road;

    internal protected void CreateCoinWay()
    {
        for (int i = 0; i < platform.copyPlatform.Count - 1; i++)
        {
            groupNum = Random.Range(0, 3);
            SelectCoinGroup(groupNum, bridgeSpawner.bridges[i].transform.position.x);
        }
        foreach (GameObject coin in allCoins)
        {
            coin.AddComponent<SCollideCoin>();
        }
    }

    private int groupNum;
    private float startPosY;

    private void SelectCoinGroup(int groupNum, float firstCoinPosX)
    {
        int numCoins;
        int rowsCons;
        float distBetweenCoins;
        float distBetweeRows;
        startPosY = platform.GetPlatformTop + 0.5f;
        float startPosZ = platform.copyPlatform[0].transform.position.z;
        Vector3 spawnPosition = new Vector3(firstCoinPosX, startPosY, startPosZ);

        switch (groupNum)
        {
            case 0:
                numCoins = 3;
                distBetweenCoins = 1f;
                for (int i = 0; i < numCoins; i++)
                {
                    GameObject newObject = Instantiate(coinPrefab, spawnPosition, coinPrefab.transform.rotation, coins);
                    spawnPosition += new Vector3(distBetweenCoins, 0f, 0f);
                    allCoins.Add(newObject);
                }
                break;
            case 1:
                numCoins = 5;
                rowsCons = 3;
                distBetweenCoins = 1f;
                distBetweeRows = 1f;
                spawnPosition.z -= 1;
                for (int j = 0; j < rowsCons; j++)
                {
                    for (int i = 0; i < numCoins; i++)
                    {
                        GameObject newObject = Instantiate(coinPrefab, spawnPosition, coinPrefab.transform.rotation, coins);
                        spawnPosition += new Vector3(distBetweenCoins, 0f, 0f);
                        allCoins.Add(newObject);
                    }
                    spawnPosition += new Vector3(0f, 0f, distBetweeRows);
                }
                break;
            case 2:
                numCoins = 3;
                distBetweenCoins = 1f;
                for (int i = 0; i < numCoins; i++)
                {
                    GameObject newObject = Instantiate(coinPrefab, spawnPosition, coinPrefab.transform.rotation, coins);
                    spawnPosition += new Vector3(distBetweenCoins, 0f, 0f);
                    spawnPosition.y += 0.4f;
                    allCoins.Add(newObject);
                }
                for (int i = 0; i < numCoins; i++)
                {
                    GameObject newObject = Instantiate(coinPrefab, spawnPosition, coinPrefab.transform.rotation, coins);
                    spawnPosition += new Vector3(distBetweenCoins, 0f, 0f);
                    spawnPosition.y -= 0.4f;
                    allCoins.Add(newObject);
                }
                break;

            default:
                break;
        }
    }
    internal protected void ResetCoinsWay()
    {
        foreach (GameObject go in allCoins)
        {
            if (go != null)
                Destroy(go);
        }

        allCoins.Clear();
    }
}
