using UnityEngine;

public class SBridgeMaterials : MonoBehaviour
{
    private float amplitude = 0.1f; // ��������� ��������
    private float frequency = 3f; // ������� ��������
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * frequency) * amplitude;

        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
