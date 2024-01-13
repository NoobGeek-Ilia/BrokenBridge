using System;
using UnityEngine;

public class SLastElevator : MonoBehaviour
{
    public StateMonitor monitor;
    public SRoad road;
    public SPlayerMovement playerMovement;
    public Action onSwichedToNextStage;
    public SPlatform platform;

    protected internal bool playerTakenToNextLevel;

    private bool isMoving = false;
    private float speed = 4f;

    private void Start()
    {
        ResetElevatorPos();
    }
    private void Update()
    {
        if (playerTakenToNextLevel)
            isMoving = false;
        if (playerMovement.playerOnTargetPlatform && !playerTakenToNextLevel)
            isMoving = true;
        else
            isMoving = false;
        if (isMoving)
        {
            if (transform.localPosition.y > 5f)
            {
                gameObject.SetActive(false);
                onSwichedToNextStage?.Invoke();
                playerTakenToNextLevel = true;
                
            }
            RisePlatform();
        }
    }
    private void RisePlatform()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
    public void ResetElevatorPos()
    {
        float lastPlatformPosX = platform.GetRenderPlatformInfo(platform.copyPlatform.Count - 1).bounds.max.x;
        float lastPlatformPosY = platform.GetRenderPlatformInfo(platform.copyPlatform.Count - 1).bounds.max.y;
        float lastPlatformPosZ = platform.GetRenderPlatformInfo(platform.copyPlatform.Count - 1).bounds.max.z;

        transform.position = new Vector3(lastPlatformPosX + (transform.localScale.x / 2),
    lastPlatformPosY, lastPlatformPosZ - (transform.localScale.z / 2));
        isMoving = false;
    }
}
