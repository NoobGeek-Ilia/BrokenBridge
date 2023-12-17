using System;
using UnityEngine;

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
    [SerializeField] SBridgeTouchController bridgeTouchController;
    public GameObject[] allCharacters;
    [SerializeField] GameObject[] weaponPrefab;
    [SerializeField] Transform[] anchorParent;
    [SerializeField] SPlayerLifeController playerLifeController;
    [SerializeField] SStageMonitor stageMonitor;
    [SerializeField] SBuildMaterialController materialController;
    [SerializeField] SHeart heart;
    internal protected Action OnLevelComplited;
    internal protected GameObject currCharacter { get; private set; }
    internal protected static int[] stages = new int[36];
    internal protected int coinsNum;
    internal protected int KilledEnemyesNum;
    internal protected int BrokeBridgeNum;
    protected internal int GetStageNum { get; private set; }

    internal protected int currentStageIndex = 0;
    public bool levelComplite = true;

    private void Awake()
    {
        FillStagesArray();
    }
    void FillStagesArray()
    {
        //int[] pattern = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        int[] pattern = new int[] { 2, 2, 3, 3, 3, 3, 4, 6, 8 };
        int patternLength = pattern.Length;

        for (int i = 0; i < stages.Length; i++)
        {
            stages[i] = pattern[i % patternLength];
        }
        GetStageNum = stages[SBoxPanel.SelectedLevel];
    }
    private void OnEnable()
    {
        lastElevator.onSwichedToNextStage += () => Reload(false);
        ActivateSelectedCharacter();
        ActivateSelectedWeapon();
    }
    private void OnDisable()
    {
        lastElevator.onSwichedToNextStage -= () => Reload(false);
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
        if (currentStageIndex == stages[SBoxPanel.SelectedLevel] && !levelComplite)
        {
            levelComplite = true;
            OnLevelComplited?.Invoke();
        }
    }

    internal protected void Reload(bool resetStageIndex)
    {
        if (resetStageIndex)
            currentStageIndex = 0;
        else
            currentStageIndex++;
        stageMonitor.InitSatge();
        roadObjects.DestroyDamageObjectsAndEnemy();
        coins.ResetCoinsWay();
        materialController.ResetMaterial();
        buildMaterial.ResetMaterialWay();
        heart.ResetHeartsWay();
        platform.DestroyPlatforms();
        ResetVariables();
        platform.AddNewPlatform();
        bridgeSpawner.ResetSpawner();
        firstElevator.ResetElevatorPos();
        playerLifeController.ResetPlayerHealsPoint();
        playerMovement.SetNewPlayerPos();
        lastElevator.ResetElevatorPos();
        bridgeTouchController.BridgeInit();
        Debug.Log($"currStage: {currentStageIndex}");
    }
    void ResetVariables()
    {
        road.eventWasCalled = false;
        platform.currentIndexPlatform = 0;
        gameUi.GetRunTimer = 2;
        mainCamera.cameraBehindPlayer = false;
        mainCamera.transform.position = mainCamera.startPosition;
    }

    void ActivateSelectedCharacter()
    {
        foreach (var character in allCharacters)
            character.SetActive(false);
        allCharacters[SGlobalGameInfo.selectedCharacter].SetActive(true);
        currCharacter = allCharacters[SGlobalGameInfo.selectedCharacter];
    }
    void ActivateSelectedWeapon()
    {
        GameObject prefab = weaponPrefab[SGlobalGameInfo.selectedWeapon];
        Transform anchor = anchorParent[SGlobalGameInfo.selectedCharacter];
        Instantiate(prefab, anchor.transform.position, prefab.transform.rotation, anchor);
    }
}
