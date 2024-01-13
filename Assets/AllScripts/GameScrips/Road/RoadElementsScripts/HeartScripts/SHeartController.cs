using UnityEngine;

public class SHeartController : MonoBehaviour
{
    const int _heartBonusNum = 3;
    
    [SerializeField] private SPlayerLifeController _lifeController;
    [SerializeField] private SHeartSoundController soundController;
    internal protected void PickUpMaterial()
    {
        _lifeController.HealthPoints += _heartBonusNum;
        soundController.PlaySound();
    }
}