using UnityEngine;

public class SLastElevator : MonoBehaviour
{
    public StateMonitor monitor;
    public SRoad road;
    public SPlayerMovement playerMovement;
    bool isMoving = false;
    float speed = 4f; // Скорость движения объекта
    public SPlatform platform;
    protected internal bool playerTakenToNextLevel;

    private void Start()
    {
        ResetElevatorPos();
    }
    void Update()
    {
        if (playerTakenToNextLevel)
            isMoving = false;
        if (playerMovement.playerOnTargetPlatform && !playerTakenToNextLevel)
            isMoving = true;
        else
            isMoving = false;
        if (isMoving)
        {
            if (transform.localPosition.y > 2.4f)
                playerTakenToNextLevel = true;
            RisePlatform();
        }
    }
    void RisePlatform()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
    public void ResetElevatorPos()
    {
        transform.position = new Vector3(platform.GetRenderLastPlatform_xPos - (transform.localScale.x / 2),
platform.GetRenderLastPlatform_yPos + 0.1f, platform.GetRenderLastPlatform_zPos - (transform.localScale.z / 2));
        isMoving = false;
    }
}
