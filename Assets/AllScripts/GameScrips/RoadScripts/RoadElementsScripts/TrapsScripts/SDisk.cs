using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SDisk : MonoBehaviour
{
    public GameObject disk;
    private SPlatform platform;
    public float pointA;
    public float pointB;
    public float MovingSpeed = 0.5f;

    private float progress = 0f;

    private int spinSpeed = 400;

    private void Start()
    {
        platform = FindObjectOfType<SPlatform>();
        pointA = platform.GetMaxPlatformZ - 0.5f;
        pointB = pointA - 3f;
    }
    private void FixedUpdate()
    {
        transform.Rotate(Vector3.down, spinSpeed * Time.deltaTime);
        MovingDisk();
    }
    void MovingDisk()
    {
        progress += MovingSpeed * Time.deltaTime;
        float newZ = Mathf.Lerp(pointA, pointB, progress);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
        if (progress >= 1f)
        {
            var temp = pointA;
            pointA = pointB;
            pointB = temp;
            progress = 0f;
        }
    }
}
