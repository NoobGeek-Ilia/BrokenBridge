using System;
using UnityEngine;

public class SCollisionController : MonoBehaviour
{
    internal protected Action onDamageCollided;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DamageObject"))
        {
            onDamageCollided?.Invoke();
        }
    }
}
