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


    private void Start()
    {
        initialPosition = new Vector3(platform.copyPlatform[0].transform.position.x - 6f,
                                      platform.GetRender_yPos + 4, platform.copyPlatform[0].transform.position.z);

        initialRotation = Quaternion.Euler(15.5f, 90.11f, 0.067f);

        targetPosition1 = new Vector3(-23, 30, -47);
        targetRotation1 = Quaternion.Euler(8.7f, 29f, 1.8f);

        targetPosition2 = initialPosition;
        targetRotation2 = initialRotation;

        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }

    public void MoveToTarget()
    {
        if (isMoving)
            return;

        isMoving = true;
        movementTimer = 0.0f;

        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;
        Vector3 targetPosition;
        Quaternion targetRotation;

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
        StartCoroutine(MoveCamera(startPosition, startRotation, targetPosition, targetRotation));
    }

    private void Update()
    {
        Debug.Log(cp);
        ChengeCameraPos(cp);
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
                if (lastElevator.playerTakenToNextLevel)
                {
                    //исправить + 25 (сделать отношение от главной камеры)
                    //transform.position = initialPosition;
                    this.cp = CameraPosition.Start;
                }
                break;
        }
    }

    private IEnumerator MoveCamera(Vector3 startPosition, Quaternion startRotation, Vector3 targetPosition, Quaternion targetRotation)
    {
        while (movementTimer < movementDuration)
        {
            movementTimer += Time.deltaTime;
            float progress = Mathf.Clamp01(movementTimer / movementDuration);

            transform.position = Vector3.Lerp(startPosition, targetPosition, progress);
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, progress);

            yield return null;
        }

        isMoving = false;
    }
    public enum CameraPosition
    {
        Start,
        Build,
        Run
    }
}