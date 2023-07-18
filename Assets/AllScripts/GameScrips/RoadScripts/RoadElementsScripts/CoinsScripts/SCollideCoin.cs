using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCollideCoin : MonoBehaviour
{
    StateMonitor sm;
    private void Start()
    {
        sm = FindObjectOfType<StateMonitor>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sm.coinsNum++;
            gameObject.SetActive(false);
        }
    }
}
