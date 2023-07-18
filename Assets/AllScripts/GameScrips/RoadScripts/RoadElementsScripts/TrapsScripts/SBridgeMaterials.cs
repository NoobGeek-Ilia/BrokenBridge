using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBridgeMaterials : MonoBehaviour
{
    private float amplitude = 0.1f; // Амплитуда движения
    private float frequency = 3f; // Частота движения

    private Vector3 startPos;

    private void Start()
    {
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
