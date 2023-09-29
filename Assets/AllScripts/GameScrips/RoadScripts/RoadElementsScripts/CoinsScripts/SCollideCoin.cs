using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCollideCoin : MonoBehaviour
{
    StateMonitor stateMonitor;
    SCoinSoundController coinSoundController;
    private void Start()
    {
        coinSoundController = FindObjectOfType<SCoinSoundController>();
        stateMonitor = FindObjectOfType<StateMonitor>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            const int oneCoinValue = 5;
            stateMonitor.coinsNum += oneCoinValue;
            gameObject.SetActive(false);
            coinSoundController.PlayGetCoinSound();
        }
    }
}
