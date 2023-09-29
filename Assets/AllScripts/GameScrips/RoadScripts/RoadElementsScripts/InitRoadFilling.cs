using Unity.VisualScripting;
using UnityEngine;

public class InitRoadFilling : MonoBehaviour
{
    public SBridgeSpawner bridgeSpawner;
    public SRoad road;
    public GameObject[] enemy;
    public GameObject[] damageObject;
    public SPlatform platform;
    [SerializeField] Transform damageObjectsContainer;
    [SerializeField] Transform enemyContainer;
    [SerializeField] SCoins coins;
    GameObject bridgeBody;
    SBridge bridge;
    [SerializeField] SBuildMaterial bridgeMaterial;
    internal protected int allEnemiesOnLvlNum;
    private void OnEnable()
    {
        road.onRoadComplited += FillRoad;
    }

    private void OnDisable()
    {
        road.onRoadComplited -= FillRoad;
    }
    void FillRoad()
    {
        //coins
        coins.CreateCoinWay();
        //enemy + damage obj
        for (int i = 0; i < bridgeSpawner.bridges.Count; i++)
        {
            bridgeBody = GameObject.Find($"Bridge{i}");
            bridge = bridgeBody.GetComponent<SBridge>();
            FillBridge(bridge);
            bridgeMaterial.CreateMaterialsWay(bridge);
        }
        allEnemiesOnLvlNum += enemyContainer.childCount;
    }

    void FillBridge(SBridge bridge)
    {
        for (int currRow = 0; currRow < bridge.copyBridgeParticle.Count / 3; currRow++)
        {
            int probRowFill = 5; // probability of filling a row
            int randRow = Random.Range(0, probRowFill);
            bool RawIsEmpty = EmptyCellSum(currRow) == SBridge.widthBridge - 1;
            bool RawIsNone = randRow < 1;
            if (RawIsEmpty || RawIsNone) //continue to next row
                continue;
            //fill row
            bool rowTipe = Random.value > 0.5f;
            if (rowTipe) //fill row with damage objects
            {
                CreateDamageObjects(currRow);
                continue;
            }
            else //fill row with enemies or none
            {
                for (int i = 0; i < SBridge.widthBridge; i++)
                {
                    int currCell = (currRow * 3) + i;
                    int probCellFill = 2; // probability of filling a cell
                    int randCell = Random.Range(0, probCellFill);
                    bool cellIsEmpty = bridge.CellIsEmpty[currCell];
                    bool cellIsNone = randCell < 1;
                    if (cellIsEmpty || cellIsNone) //continue if cell is empty or none
                        continue;
                    else
                        CreateEnemy(currCell);
                }
            }
        }
    }

    void CreateDamageObjects(int currRow)
    {
        float[] distanceToBridge = { 0f, 0.8f, 0.2f }; //disk, flame, peak
        Vector3[] cellPosition = new Vector3[SBridge.widthBridge];
        int selectedObject = SelectedDamageObject(currRow);
        for (int i = 0; i < SBridge.widthBridge; i++)
        {
            
            int currCell = (currRow * 3) + i;
            bool cellIsEmpty = bridge.CellIsEmpty[currCell];
            if(cellIsEmpty)
                continue;
            cellPosition[i] = bridge.copyBridgeParticle[currCell].transform.position;
            SetPosAndInstObject(cellPosition[i], selectedObject, distanceToBridge[selectedObject]);
            if (selectedObject != 2)
                break;
        }
    }
    void SetPosAndInstObject(Vector3 cellPosition, int selectedObject, float distanceToBridge)
    {
        float NewX = cellPosition.x;
        float NewY = cellPosition.y + distanceToBridge;
        float NewZ = cellPosition.z;
        Quaternion rotation = damageObject[selectedObject].transform.rotation;
        int rand = Random.Range(0, 2);
        switch (selectedObject)
        {
            case 0:
                //disk
                float[] cellPosZ = bridge.GetCellPosZ;
                float[,] cellsPos = { { cellPosZ[0], cellPosZ[1] }, { cellPosZ[1], cellPosZ[2] } };
                NewZ = cellsPos[ConnectionTipe, rand];
                break;
            case 1:
                //fire
                Bounds platformRenderZ = platform.GetRenderPlatformInfo(0).bounds;
                int[] rotationY = { 90, -90 };
                float[] positionZ = { platformRenderZ.min.z, platformRenderZ.max.z };
                rotation = Quaternion.Euler(rotationY[rand], rotation.y, rotation.z);
                NewZ = positionZ[rand];
                break;
            case 2:
                //peak

                break;
        }
        Vector3 SpawnPos = new Vector3(NewX, NewY, NewZ);
        Instantiate(damageObject[selectedObject], SpawnPos, rotation, damageObjectsContainer);
    }

    int SelectedDamageObject(int currRow)
    {
        int objTipe;
        do
        {
            objTipe = Random.Range(0, damageObject.Length);
        } while (objTipe == 0 && !CheckingCellsNearby(currRow));

        return objTipe;
    }
    void CreateEnemy(int currCell)
    {
        int enemyTipe = Random.Range(0, enemy.Length);
        float[] distanceToBridge = { 0.6f, 0.5f, 0.5f }; //sp, sl, cr
        float spawnX = bridge.copyBridgeParticle[currCell].transform.position.x;
        float spawnY = bridge.copyBridgeParticle[currCell].transform.position.y + distanceToBridge[enemyTipe];
        float spawnZ = bridge.copyBridgeParticle[currCell].transform.position.z;

        Vector3 spawnPosition = new Vector3(spawnX, spawnY, spawnZ);
        GameObject currEnemy = Instantiate(enemy[enemyTipe], spawnPosition, enemy[enemyTipe].transform.rotation, enemyContainer);
        currEnemy.name = enemy[enemyTipe].name;
    }

    int ConnectionTipe;
    bool CheckingCellsNearby(int currRow)
    {
        bool[] cell = new bool[SBridge.widthBridge];
        for (int i = 0; i < SBridge.widthBridge; i++)
        {
            int currCell = (currRow * 3) + i;
            if(bridge.CellIsEmpty[currCell])
                cell[i] = true;
        }
        ConnectionTipe = !cell[0] && !cell[1] ? 0 : 
            (!cell[1] && !cell[2] ? 1 : ConnectionTipe);
        bool cellsHaveConnection = !cell[0] && !cell[1] || !cell[1] && !cell[2];
        if (cellsHaveConnection)
            return true;
        else
            return false;
    }
    internal protected int EmptyCellSum(int currRow)
    {
        int sum = 0;
        for (int i = 0; i < SBridge.widthBridge; i++)
        {
            int currCell = (currRow * 3) + i;
            if (bridge.CellIsEmpty[currCell])
                sum++;
        }
        return sum;
    }

    public void DestroyDamageObjectsAndEnemy()
    {
        Transform[] container = { damageObjectsContainer, enemyContainer };
        for (int j = 0; j < container.Length; j++)
        {
            int childNum = container[j].childCount;
            for (int i = childNum - 1; i >= 0; i--)
            {
                Transform child = container[j].GetChild(i);
                DestroyImmediate(child.gameObject);
            }
        }
    }
}
