using UnityEngine;

public class SCameraBody : MonoBehaviour
{
    public SBridgeSpawner bridgeSpawner;
    public SRoad road;
    public SPlatform platform;
    public SCamera mainCamera;
    public bool moveToNextPlatform;
    public SLastElevator lastElevator;
    public GameObject player;
    public Vector3 offset;
    public Vector3 initPos = new Vector3(-23, 30, -47);
    Vector3 targetPosition;
    Vector2 dir;

    private void OnEnable()
    {
        SLastElevator.onSwichedToNextStage += InitPos;
    }
    private void OnDisable()
    {
        SLastElevator.onSwichedToNextStage -= InitPos;
    }
    private void Update()
    {
        if (bridgeSpawner.brideComplite && !road.roadComplite)
        {
            moveToNextPlatform = true;
        }

    }
    void InitPos()
    {
        transform.position = initPos;
    }
    private void FixedUpdate()
    {
        MoveCameraToSide();
        if (mainCamera.cameraBehindPlayer)
        {
            FollowPlayer();
        }
    }

    void MoveCameraToSide()
    {
        if (transform.position.x > platform.copyPlatform[platform.currentIndexPlatform].transform.position.x - 20)
        {
            moveToNextPlatform = false;
        }

        if (moveToNextPlatform && !road.roadComplite)
        {
            const float speed = 0.5f;
            dir = new Vector2(1, 0);
            transform.Translate(dir * speed);
        }
    }
    void FollowPlayer()
    {
        if (road.roadComplite)
            offset = transform.position - player.transform.position; 
        float targetX = player.transform.position.x + offset.x; 
        targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);
        transform.position = targetPosition;
    }
}