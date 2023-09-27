using System;
using UnityEngine;

public class SBuildMaterialCollide : MonoBehaviour
{
    private SBuildMaterialController materialController;
    private void Start()
    {
        materialController = FindObjectOfType<SBuildMaterialController>();
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
