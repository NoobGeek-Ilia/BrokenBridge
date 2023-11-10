using System;
using UnityEngine;

public class SHeartCollider : MonoBehaviour
{
    private SHeartController materialController;
    private void Start()
    {
        materialController = FindObjectOfType<SHeartController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            materialController.PickUpMaterial();
            gameObject.SetActive(false);
        }
    }
}
