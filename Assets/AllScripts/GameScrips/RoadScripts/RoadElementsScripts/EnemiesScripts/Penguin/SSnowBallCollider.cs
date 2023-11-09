using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSnowBallCollider : MonoBehaviour
{
    internal protected Action onCollided;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            onCollided?.Invoke();
    }
}
