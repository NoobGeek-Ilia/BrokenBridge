using System;
using UnityEngine;

public class SFirstElevator : MonoBehaviour
{
    public StateMonitor monitor;
    public SRoad road;
    public SPlatform platform;

    internal protected bool elevatorOnPlatform;
    internal protected Action onElevatorLanded;

    private bool isMoving = true;
    private float speed = 4f;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
            ResetElevatorPos();
    }
    private void FixedUpdate()
    {
        if (isMoving)
            LowerPlatform();
    }
    private void Update()
    {
        if (isMoving)
        {
            if (transform.position.y < platform.GetPlatformTop + 0.1)
            {
                isMoving = false;
                elevatorOnPlatform = true;
                onElevatorLanded?.Invoke();
            }
            else
                elevatorOnPlatform = false;
        }
    }
    private void LowerPlatform()
    {
        Vector3 movement = new Vector3(0f, -speed * Time.deltaTime, 0f);
        rb.MovePosition(transform.position + movement);
    }

    public void ResetElevatorPos()
    {
        float newX = platform.copyPlatform[0].transform.position.x;
        float newY = platform.GetPlatformTop + 7;
        float newZ = platform.copyPlatform[0].transform.position.z;

        transform.position = new Vector3(newX, newY, newZ);
        isMoving = true;
    }
}
