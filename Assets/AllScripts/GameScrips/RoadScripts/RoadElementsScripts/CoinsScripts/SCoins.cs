using System.Collections.Generic;
using UnityEngine;

public class SCoins : MonoBehaviour
{
    public List<GameObject> allCoins = new List<GameObject>();
    public GameObject coinPrefab; // ссылка на префаб
    public SPlatform platform;
    public Transform coins; // ссылка на родительский объект
    public SBridgeSpawner bridgeSpawner;
    public SRoad road;
    private bool coinsInstalled;
    int groupNum;
    float startPosY;
    const int rotateSpeed = 3;

    private void FixedUpdate()
    {
        if (allCoins != null)
        {
            RotateCoins();
        }
    }
    private void Update()
    {
        CreateCoinWay();
        if (allCoins != null)
        {
            
            foreach (GameObject go in allCoins)
            {
                if (go.GetComponent<SCollideCoin>() == null)
                    go.AddComponent<SCollideCoin>();
            }
        }
    }
    void CreateCoinWay()
    {
        if (road.roadComplite && !coinsInstalled)
        {
            for (int i = 0; i < platform.copyPlatform.Count - 1; i++)
            {
                groupNum = Random.Range(0, 3);
                SelectCoinGroup(groupNum, bridgeSpawner.bridges[i].transform.position.x);
            }
            coinsInstalled = true;
        }
    }
    void RotateCoins()
    {
        foreach (GameObject go in allCoins)
        {
            go.transform.Rotate(0, rotateSpeed, 0, Space.World);
        }
    }
    void SelectCoinGroup(int groupNum, float firstCoinPosX)
    {
        
        int numCoins; // количество монет
        int rowsCons;
        float distBetweenCoins; // рассто€ние между монетами
        float distBetweeRows; // рассто€ние между р€дами
        startPosY = platform.GetPlatformTop + 0.5f; //рассто€ние между платформой и монетами
        float startPosZ = platform.copyPlatform[0].transform.position.z;
        Vector3 spawnPosition = new Vector3(firstCoinPosX, startPosY, startPosZ);// начальна€ позици€ дл€ первой монеты

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
    public void ResetCoinsWay()
    {
        
        foreach (GameObject go in allCoins)
        {
            if (go != null)
                Destroy(go);
        }

        allCoins.Clear();
        coinsInstalled = false;
    }
}
