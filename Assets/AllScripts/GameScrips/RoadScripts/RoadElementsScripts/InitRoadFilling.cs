using Unity.VisualScripting;
using UnityEngine;

public class InitRoadFilling : MonoBehaviour
{
    public SBridgeSpawner bridgeSpawner;
    public SRoad road;
    public GameObject[] enemy;
    public GameObject[] damageObject;
    bool roadHasFilled;
    public SPlatform platform;

    GameObject bridgeBody;
    SBridge bridge;

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
            bridgeBody = GameObject.Find($"Bridge{i}");
            bridge = bridgeBody.GetComponent<SBridge>();
            FillBridge(bridge);
        }
        roadHasFilled = true;
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
        float[] distanceToBridge = { 0f, 0.4f, 0.2f }; //disk, flame, peak
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
        Instantiate(damageObject[selectedObject], SpawnPos, rotation);
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
        float[] distanceToBridge = { 0.6f, 0.4f, 0.5f }; //sp, sl, cr
        float spawnX = bridge.copyBridgeParticle[currCell].transform.position.x;
        float spawnY = bridge.copyBridgeParticle[currCell].transform.position.y + distanceToBridge[enemyTipe];
        float spawnZ = bridge.copyBridgeParticle[currCell].transform.position.z;

        Vector3 spawnPosition = new Vector3(spawnX, spawnY, spawnZ);
        Instantiate(enemy[enemyTipe], spawnPosition, enemy[enemyTipe].transform.rotation);
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
    int EmptyCellSum(int currRow)
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
    //рабочий варик, но без учета фаера и диска
    /*    void FillBridge(SBridge bridge)
        {
            const int numTipes = 5;
            foreach (GameObject part in bridge.copyBridgeParticle)
            {

                if (bridge.CellIsEmpty[bridge.copyBridgeParticle.IndexOf(part)])
                    continue;
                cellTipe = Random.Range(0, numTipes);

                switch (cellTipe)
                {
                    case 0:
                        int objTipe = Random.Range(0, roadObjects.Length);
                        float[] distanceToBridge = { 0.6f, 0.4f, 0.5f }; //sp, sl, cr
                        Vector3 spawnPosition = new Vector3(part.transform.position.x,
                            part.transform.position.y + distanceToBridge[objTipe], part.transform.position.z);
                        Instantiate(roadObjects[objTipe], spawnPosition, roadObjects[objTipe].transform.rotation);
                        break;
                    default:
                        break;
                }
            }

        }*/
}
