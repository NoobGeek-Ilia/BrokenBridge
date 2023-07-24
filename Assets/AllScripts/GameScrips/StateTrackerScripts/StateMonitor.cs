using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StateMonitor : MonoBehaviour
{
    public SRoad road;
    public SPlatform platform;
    public bool levelComplite = true;
    public SBridgeSpawner bridgeSpawner;
    public GameObject StageCompliteWindow;
    public SPlayerMovement playerMovement;
    public SFirstElevator firstElevator;
    public SLastElevator lastElevator;
    private int currentStageIndex = 0;
    int[] stages = { 2, 2, 3, 3, 4, 4, 4, 5, 5 };
    public TextMeshProUGUI stagesNumTxt;
    public TextMeshProUGUI currStagesTxt;
    public TextMeshProUGUI coinsNumTxt;
    internal protected int coinsNum;
    public Transform Platforms;
    public Transform BridgeSpawner;
    public SCamera mainCamera;
    public SCameraBody bodyCamera;
    public SCoins coins;


    void Update()
    {
        //Debug.Log($"road complite: {road.roadComplite}, lvl: {SLevelPanel.levelNumber}, lvl complite: {levelComplite}");
        ShowMainState();
        CheckCompliteLevel();
        ShowRunTimer(road.roadComplite);
        if (levelComplite)
        {
            ShowLevelStatistic();
        }

        else
        {
            if (lastElevator.playerTakenToNextLevel)
            {
                ReloadStage();
                lastElevator.playerTakenToNextLevel = false;
            }
        }
    }
    void ShowMainState()
    {
        coinsNumTxt.text = coinsNum.ToString();
        stagesNumTxt.text = stages[SLevelPanel.levelNumber].ToString();
        currStagesTxt.text = (currentStageIndex + 1).ToString();
    }
    void ShowLevelStatistic()
    {
        StageCompliteWindow.SetActive(true);
    }

    void CheckCompliteLevel()
    {
        if (currentStageIndex == stages[SLevelPanel.levelNumber])
        {
            levelComplite = true;

        }
    }
    void ReloadStage()
    {
        currentStageIndex++;
        DestroyPlatforms();
        DestroyBridges();
        ResetVariables();
        firstElevator.ResetElevatorPos();
        playerMovement.SetNewPlayerPos();
        lastElevator.ResetElevatorPos();

    }
    void ResetVariables()
    {
        //platform
        platform.currentIndexPlatform = 0;
        platform.AddNewPlatform();
        platform.RenderInit();

        //bridge
        bridgeSpawner.currBridge = 1;
        bridgeSpawner.CreateFirstBridge();
        mainCamera.cameraBehindPlayer = false;
        mainCamera.transform.position = mainCamera.initialPosition;
        coins.ResetCoinsWay();
    }

    void DestroyPlatforms()
    {
        List<GameObject> objectsToDelete = new List<GameObject>(platform.copyPlatform);

        foreach (GameObject obj in objectsToDelete)
        {
            platform.copyPlatform.Remove(obj);
            Destroy(obj);
        }

        int childCount = Platforms.childCount;

        for (int i = childCount - 1; i >= 0; i--)
        {
            Transform child = Platforms.GetChild(i);
            DestroyImmediate(child.gameObject);
        }
    }
    void DestroyBridges()
    {
        int childCount = BridgeSpawner.childCount;
        bridgeSpawner.bridges.Clear();
        for (int i = childCount - 1; i >= 0; i--)
        {
            Transform child = BridgeSpawner.GetChild(i);
            DestroyImmediate(child.gameObject);
        }
    }


    float currTime = 2;
    internal protected int timer;
    int RunTimer()
    {
        if (timer > 0)
        {
            currTime -= Time.deltaTime;
            if (currTime < 0)
            {
                timer--;
                currTime = 2;
            }
        }
        return timer;
    }
    public TextMeshProUGUI RunTimerTxt;
    void ShowRunTimer(bool setTimer)
    {
        if (setTimer && timer < 1)
            timer = 3;
        if (RunTimer() > 0)
        {
            if (!RunTimerTxt.gameObject.activeSelf)
                RunTimerTxt.gameObject.SetActive(true);
            RunTimerTxt.text = RunTimer().ToString();
        }
        else
        {
            if (RunTimerTxt.gameObject.activeSelf)
                RunTimerTxt.gameObject.SetActive(false);
            timer = 0;
        }
    }
}
