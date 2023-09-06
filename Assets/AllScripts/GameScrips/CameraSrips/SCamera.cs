using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SCamera : MonoBehaviour
{
    public SPlatform platform;
    public Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector3 targetPosition1;
    private Quaternion targetRotation1;
    private Vector3 targetPosition2;
    private Quaternion targetRotation2;
    public float movementDuration = 1.0f;
    private bool isMoving = false;
    private float movementTimer = 0.0f;
    public bool cameraBehindPlayer;
    public SRoad road;
    public SFirstElevator firstElevator;
    public SLastElevator lastElevator;
    public CameraPosition cp = CameraPosition.Start;

    private Vector3 startPosition;
    private Quaternion startRotation;
    private Vector3 targetPosition;
    private Quaternion targetRotation;

    private void OnEnable()
    {
        SLastElevator.onSwichedToNextStage += SetStartPosition;
    }
    private void OnDisable()
    {
        SLastElevator.onSwichedToNextStage -= SetStartPosition;
    }
    private void Start()
    {
        Renderer getInfoFirstPlatform = platform.GetRenderPlatformInfo(0);
        const float distanceToPlayer = 4.3f;
        float centerPlatformZ = platform.GetPlatformPositionInfo().z;
        initialPosition = new Vector3(getInfoFirstPlatform.bounds.min.x - distanceToPlayer,
                                      getInfoFirstPlatform.bounds.max.y + 4.5f, centerPlatformZ);

        initialRotation = Quaternion.Euler(25.5f, 90.11f, 0.067f);

        targetPosition1 = new Vector3(-23, 30, -47);
        targetRotation1 = Quaternion.Euler(8.7f, 29f, 1.8f);

        targetPosition2 = initialPosition;
        targetRotation2 = initialRotation;

        transform.position = initialPosition;
        transform.rotation = initialRotation;

        // Initialize the start position and rotation
        startPosition = initialPosition;
        startRotation = initialRotation;

        // Initialize the target position and rotation
        targetPosition = initialPosition;
        targetRotation = initialRotation;
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            movementTimer += Time.fixedDeltaTime;
            float progress = Mathf.Clamp01(movementTimer / movementDuration);

            transform.position = Vector3.Lerp(startPosition, targetPosition, progress);
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, progress);

            if (progress >= 1.0f)
            {
                isMoving = false;
            }
        }
    }

    public void MoveToTarget()
    {
        if (isMoving)
            return;

        isMoving = true;
        movementTimer = 0.0f;

        if (!road.roadComplite)
        {
            targetPosition = targetPosition1;
            targetRotation = targetRotation1;
        }
        else
        {
            targetPosition = targetPosition2;
            targetRotation = targetRotation2;
        }

        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    private void Update()
    {
        ChengeCameraPos(cp);
        Debug.Log(cp);
    }

    void ChengeCameraPos(CameraPosition cp)
    {
        switch (cp)
        {
            case CameraPosition.Start:
                if (firstElevator.elevatorOnPlatform)
                    MoveToTarget();
                if (transform.position == targetPosition1)
                    this.cp = CameraPosition.Build;
                break;

            case CameraPosition.Build:
                if (road.roadComplite)
                    MoveToTarget();
                if (transform.position == targetPosition2)
                    this.cp = CameraPosition.Run;
                break;

            case CameraPosition.Run:
                cameraBehindPlayer = true;
                break;
        }
    }
    void SetStartPosition()
    {
        
        cp = CameraPosition.Start;
    }
    public enum CameraPosition
    {
        Start,
        Build,
        Run
    }
}
