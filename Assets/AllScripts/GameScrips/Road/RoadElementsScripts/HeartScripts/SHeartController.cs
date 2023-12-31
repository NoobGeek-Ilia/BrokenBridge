using System;
using UnityEngine;

public class SHeartController : MonoBehaviour
{
    const int _heartBonusNum = 3;
    [SerializeField] SPlayerLifeController _lifeController;
    [SerializeField] SHeartSoundController soundController;
    internal protected void PickUpMaterial()
    {
        _lifeController.HealthPoints += _heartBonusNum;
        soundController.PlaySound();
    }
}