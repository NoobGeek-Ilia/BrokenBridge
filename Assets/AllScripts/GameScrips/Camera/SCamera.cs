using System.Collections;
using UnityEngine;

public class SCamera : MonoBehaviour
{
    public bool cameraBehindPlayer;
    public SRoad road;
    public SFirstElevator firstElevator;
    public SLastElevator lastElevator;
    public SPlatform platform;
    internal protected CameraPosition cp;
    internal protected Vector3 startPosition;

    private Vector3 targetPosition1;
    private Quaternion targetRotation1;
    private AudioSource audioSource;
    private Quaternion startRotation;
    private Vector3 newTargetPosition;
    private Quaternion newTargetRotation;
    private Vector3 newStartPosition;
    private Quaternion newStartRotation;

    [SerializeField] private SBridgeSpawner bridgeSpawner;
    [SerializeField] private GameObject sideCameraDriver;
    [SerializeField] private SPlayerMovement playerMovement;
    [SerializeField] private SPlayerLifeController sPlayerLifeController;

    const float distanceToPlayerX = 7.5f;

    private void OnEnable()
    {
        lastElevator.onSwichedToNextStage += () => StartCoroutine(HandleCameraBehavior());
        road.onRoadComplited += () => StartCoroutine(HandleCameraBehavior());
        firstElevator.onElevatorLanded += () => StartCoroutine(HandleCameraBehavior());
        bridgeSpawner.onBridgeSet += () => StartCoroutine(MoveCameraToSide());

        sPlayerLifeController.OnPlayerDied += () =>
        {
            cp = CameraPosition.Run;
            StartCoroutine(HandleCameraBehavior());
        };

    }
    private void OnDisable()
    {
        lastElevator.onSwichedToNextStage -= () => StartCoroutine(HandleCameraBehavior());
        road.onRoadComplited -= () => StartCoroutine(HandleCameraBehavior());
        firstElevator.onElevatorLanded -= () => StartCoroutine(HandleCameraBehavior());
        bridgeSpawner.onBridgeSet -= () => StartCoroutine(MoveCameraToSide());
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Renderer getInfoFirstPlatform = platform.GetRenderPlatformInfo(0);
        float centerPlatformZ = platform.GetPlatformPositionInfo().z;
        float distanceToPlayerY = 5.5f;
        //start
        startPosition = new Vector3(playerMovement.transform.position.x - distanceToPlayerX,
            getInfoFirstPlatform.bounds.max.y + distanceToPlayerY, centerPlatformZ);
        startRotation = Quaternion.Euler(20f, 90.11f, 0.067f);

        //build
        targetPosition1 = new Vector3(-23, 30, -47);
        targetRotation1 = Quaternion.Euler(8.7f, 29f, 1.8f);

        transform.position = startPosition;
        transform.rotation = startRotation;
    }
    private void FixedUpdate()
    {
        if (cp == CameraPosition.Run)
            FollowPlayer();
    }


    IEnumerator HandleCameraBehavior()
    {
        audioSource.Play();
        float offset = 0.01f;
        progress = 0;

        switch (cp)
        {
            case CameraPosition.Start:
                newStartPosition = transform.position;
                newStartRotation = transform.rotation;
                newTargetPosition = targetPosition1;
                newTargetRotation = targetRotation1;
                break;

            case CameraPosition.Build:
                newStartPosition = transform.position;
                newStartRotation = transform.rotation;
                newTargetPosition = startPosition;
                newTargetRotation = startRotation;
                break;

            case CameraPosition.Run:
                sideCameraDriver.transform.position = new Vector3( -23f, -20f, -7f);
                newStartRotation = transform.rotation;
                break;
        }

        while (Vector3.Distance(transform.position, newTargetPosition) > offset)
        {
            MoveCameraToTarget(newStartPosition, newStartRotation, newTargetPosition, newTargetRotation);
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);

        if (cp != CameraPosition.Run)
            cp++;
        else
            cp = CameraPosition.Start;
    }

    float progress;
    float speed = 0.7f;
    void MoveCameraToTarget(Vector3 startPos, Quaternion startRot, Vector3 targetPos, Quaternion targetRot)
    {
        progress += Time.deltaTime * speed;
        transform.position = Vector3.Lerp(startPos, targetPos, Mathf.SmoothStep(0, 1, progress));
        transform.rotation = Quaternion.Lerp(startRot, targetRot, Mathf.SmoothStep(0, 1, progress));
    }

    IEnumerator MoveCameraToSide()
    {
        int distance = 20;
        int platformIndex = platform.currentIndexPlatform + 1;
        float nextPlatformPosX = platform.copyPlatform[platformIndex].transform.position.x - distance;
        float currentVelocity = 0;

        if (platform.currentIndexPlatform != platform.platforms[SBoxPanel.SelectedLevel] - 1)
        {
            while (transform.position.x < nextPlatformPosX)
            {
                float targetPosX = Mathf.SmoothDamp(sideCameraDriver.transform.position.x, nextPlatformPosX, ref currentVelocity, 0.3f);
                sideCameraDriver.transform.position = new Vector3(targetPosX, sideCameraDriver.transform.position.y, 
                    sideCameraDriver.transform.position.z);
                yield return null;
            }
        }
    }

    void FollowPlayer()
    {
        float newCameraPos = playerMovement.transform.position.x - distanceToPlayerX;
        float smoothSpeed = 10.0f; // Подберите подходящее значение
        float newPositionX = Mathf.Lerp(transform.position.x, newCameraPos, smoothSpeed * Time.deltaTime);

        transform.position = new Vector3(newPositionX, startPosition.y, startPosition.z);
    }
    internal protected enum CameraPosition
    {
        Start,
        Build,
        Run
    }
}