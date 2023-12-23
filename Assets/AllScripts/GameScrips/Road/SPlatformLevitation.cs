using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPlatformLevitation : MonoBehaviour
{
    private float amplitude; // ��������� ��������
    private float frequency; // ������� ��������

    private Vector3 startPos;

    private void Start()
    {
        amplitude = Random.Range(0.5f, 1.5f);
        frequency = Random.Range(1, 3);
        startPos = transform.position;
    }

    private void Update()
    {
        // ��������� ����� ������� �� ��� Y �� ������ �������������� �������
        float newY = startPos.y + Mathf.Sin(Time.time * frequency) * amplitude;

        // ��������� ����� �������
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
