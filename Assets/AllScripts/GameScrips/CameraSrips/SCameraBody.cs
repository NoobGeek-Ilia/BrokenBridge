using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    private void Update()
    {
        if (bridgeSpawner.brideComplite && !road.roadComplite)
        {
            moveToNextPlatform = true;
        }
        if (mainCamera.cameraBehindPlayer)
        {
            FollowPlayer();
        }
        if (lastElevator.playerTakenToNextLevel)
            transform.position = initPos;
    }
    private void FixedUpdate()
    {
        MoveCameraToSide();
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
            Vector2 dir = new Vector2(1, 0);
            transform.Translate(dir * speed);
        }
    }
    void FollowPlayer()
    {
        if (road.roadComplite)
            offset = transform.position - player.transform.position; // Ïîëó÷àåì íà÷àëüíóþ äèñòàíöèþ ìåæäó êàìåðîé è èãðîêîì
        float targetX = player.transform.position.x + offset.x; // Âû÷èñëÿåì íîâóþ X-êîîðäèíàòó êàìåðû ñ ó÷åòîì íà÷àëüíîé äèñòàíöèè
        Vector3 targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);
        transform.position = targetPosition;
    }
}