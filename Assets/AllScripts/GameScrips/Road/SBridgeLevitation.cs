using UnityEngine;

public class SBridgeLevitation : MonoBehaviour
{
    private float amplitude;
    private float frequency;
    private Vector3 startPos;

    private void Start()
    {
        amplitude = Random.Range(0.1f, 0.4f);
        frequency = Random.Range(0.1f, 1);
        startPos = transform.position;
    }

    private void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * frequency) * amplitude;

        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
