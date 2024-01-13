using System;
using UnityEngine;

public class StateMonitor : MonoBehaviour
{
    public SRoad road;
    public SPlatform platform;
    public SBridgeSpawner bridgeSpawner;
    public SPlayerMovement playerMovement;
    public SFirstElevator firstElevator;
    private SLastElevator lastElevator;
    public GameObject lastElevatorGo;
    public SCamera mainCamera;
    public SCoins coins;
    public GameObject[] allCharacters;
    public bool levelComplite = true;

    internal protected Action OnLevelComplited;
    internal protected GameObject currCharacter { get; private set; }
    internal protected static int[] stages = new int[36];
    internal protected int coinsNum;
    internal protected int KilledEnemyesNum;
    internal protected int BrokeBridgeNum;
    internal protected int GetStageNum { get; private set; }
    internal protected int currentStageIndex = 0;

    [SerializeField] private InitRoadFilling roadObjects;
    [SerializeField] private SBuildMaterial buildMaterial;
    [SerializeField] private SGameUi gameUi;
    [SerializeField] private SBridgeTouchController bridgeTouchController;
    [SerializeField] private GameObject[] weaponPrefab;
    [SerializeField] private Transform[] anchorParent;
    [SerializeField] private SPlayerLifeController playerLifeController;
    [SerializeField] private SStageMonitor stageMonitor;
    [SerializeField] private SBuildMaterialController materialController;
    [SerializeField] private SHeart heart;

    private void Awake()
    {
        FillStagesArray();
        lastElevator = lastElevatorGo.GetComponent<SLastElevator>();
        SWinPanel.GetStarRecivedSum = 0;
    }
    void FillStagesArray()
    {
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

    private void Update()
    {
        CheckCompliteLevel();
        if (!levelComplite)
        {
            if (lastElevator.playerTakenToNextLevel)
                lastElevator.playerTakenToNextLevel = false;
        }
    }
    private void CheckCompliteLevel()
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
        lastElevatorGo.SetActive(true);
        lastElevator.ResetElevatorPos();
        bridgeTouchController.BridgeInit();   
    }
    private void ResetVariables()
    {
        road.eventWasCalled = false;
        platform.currentIndexPlatform = 0;
        gameUi.GetRunTimer = 2;
        mainCamera.cameraBehindPlayer = false;
        mainCamera.transform.position = mainCamera.startPosition;
    }

    private void ActivateSelectedCharacter()
    {
        foreach (var character in allCharacters)
            character.SetActive(false);
        allCharacters[SGlobalGameInfo.selectedCharacter].SetActive(true);
        currCharacter = allCharacters[SGlobalGameInfo.selectedCharacter];
    }
    private void ActivateSelectedWeapon()
    {
        GameObject prefab = weaponPrefab[SGlobalGameInfo.selectedWeapon];
        Transform anchor = anchorParent[SGlobalGameInfo.selectedCharacter];
        Instantiate(prefab, anchor.transform.position, prefab.transform.rotation, anchor);
    }
}