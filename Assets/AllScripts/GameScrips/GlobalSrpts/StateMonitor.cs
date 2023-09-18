using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StateMonitor : MonoBehaviour
{
    public SRoad road;
    public SPlatform platform;
    public SBridgeSpawner bridgeSpawner;
    public SPlayerMovement playerMovement;
    public SFirstElevator firstElevator;
    public SLastElevator lastElevator;
    public SCamera mainCamera;
    public SCoins coins;
    [SerializeField] InitRoadFilling roadObjects;
    [SerializeField] SBuildMaterial buildMaterial;
    [SerializeField] SGameUi gameUi;

    internal protected static int[] stages = { 2, 2, 3, 3, 4, 4, 4, 5, 5 };
    internal protected int coinsNum;
    internal protected int materialsNum = 10;
    
    internal protected static int currentStageIndex = 0;
    public bool levelComplite = true;

    private void OnEnable()
    {
        SLastElevator.onSwichedToNextStage += ReloadStage;
    }
    private void OnDisable()
    {
        SLastElevator.onSwichedToNextStage -= ReloadStage;
    }
    void Update()
    {
        CheckCompliteLevel();
        if (!levelComplite)
        {
            if (lastElevator.playerTakenToNextLevel)
                lastElevator.playerTakenToNextLevel = false;
        }
    }
    void CheckCompliteLevel()
    {
        if (currentStageIndex == stages[SBoxPanel.SelectedLevel])
            levelComplite = true;
    }

    void ReloadStage()
    {
        currentStageIndex++;
        roadObjects.DestroyDamageObjectsAndEnemy();
        coins.ResetCoinsWay();
        buildMaterial.ResetMaterialWay();
        platform.DestroyPlatforms();
        ResetVariables();
        platform.AddNewPlatform();
        bridgeSpawner.ResetSpawner();
        firstElevator.ResetElevatorPos();
        playerMovement.SetNewPlayerPos();
        lastElevator.ResetElevatorPos();
    }
    void ResetVariables()
    {
        road.eventWasCalled = false;
        platform.currentIndexPlatform = 0;
        gameUi.GetRunTimer = 2;
        mainCamera.cameraBehindPlayer = false;
        mainCamera.transform.position = mainCamera.startPosition;
    }


}
