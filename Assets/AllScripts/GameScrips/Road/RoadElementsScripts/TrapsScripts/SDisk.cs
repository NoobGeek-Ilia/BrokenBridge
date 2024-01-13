using UnityEngine;

public class SDisk : MonoBehaviour
{
    public GameObject disk;
    public float pointA;
    public float pointB;
    public float MovingSpeed = 0.5f;

    private SPlatform platform;
    private float progress = 0f;
    private int spinSpeed = 400;
    private bool moveFromAToB;

    private void Start()
    {
        platform = FindObjectOfType<SPlatform>();
        pointA = platform.GetMaxPlatformZ - 0.5f;
        pointB = pointA - 3f;

        moveFromAToB = Random.Range(0, 2) == 0;
    }

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.down, spinSpeed * Time.deltaTime);
        MovingDisk();
    }

    void MovingDisk()
    {
        progress += MovingSpeed * Time.deltaTime;

        float newZ = Mathf.Lerp(
            moveFromAToB ? pointA : pointB,
            moveFromAToB ? pointB : pointA,
            progress
        );

        transform.position = new Vector3(transform.position.x, transform.position.y, newZ);

        if (progress >= 1f)
        {
            moveFromAToB = !moveFromAToB;
            progress = 0f;
        }
    }
}
