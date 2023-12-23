using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBridgeLevitation : MonoBehaviour
{
    private float amplitude; // Амплитуда движения
    private float frequency; // Частота движения

    private Vector3 startPos;

    private void Start()
    {
        amplitude = Random.Range(0.1f, 0.4f);
        frequency = Random.Range(0.1f, 1);
        startPos = transform.position;
    }

    private void Update()
    {
        // Вычисляем новую позицию по оси Y на основе синусоидальной функции
        float newY = startPos.y + Mathf.Sin(Time.time * frequency) * amplitude;

        // Применяем новую позицию
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
