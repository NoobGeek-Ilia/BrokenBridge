using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFirstElevator : MonoBehaviour
{
    public StateMonitor monitor;
    public SRoad road;
    bool isMoving = true;
    float speed = 4f; // Скорость движения объекта
    private Rigidbody rb;
    public SPlatform platform;
    protected internal bool elevatorOnPlatform;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if (isMoving)
            LowerPlatform();
    }
    void Update()
    {
        if (isMoving)
        {
            if (transform.position.y < platform.GetRender_yPos + 0.1)
            {
                isMoving = false;
                elevatorOnPlatform = true;
            }
            else
                elevatorOnPlatform = false;
        }
    }
    void LowerPlatform()
    {
        Vector3 movement = new Vector3(0f, -speed * Time.deltaTime, 0f);
        rb.MovePosition(transform.position + movement);
        //transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    public void ResetElevatorPos()
    {
        float newX = platform.copyPlatform[0].transform.position.x;
        float newY = platform.GetRender_yPos + 5;
        float newZ = platform.copyPlatform[0].transform.position.z;
        transform.position = new Vector3(newX, newY, newZ);
        isMoving = true;
    }
}
