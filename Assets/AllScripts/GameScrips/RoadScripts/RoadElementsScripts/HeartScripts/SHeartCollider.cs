using System;
using UnityEngine;

public class SHeartCollider : MonoBehaviour
{
    private SHeartController materialController;
    [SerializeField] ParticleSystem bonusEffect;
    private void Start()
    {
        materialController = FindObjectOfType<SHeartController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            materialController.PickUpMaterial();
            PlayBonusEffect();
            gameObject.SetActive(false);
        }
    }

    private void PlayBonusEffect()
    {
        Vector3 newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        ParticleSystem newEffect = Instantiate(bonusEffect, newPos, Quaternion.identity);
        newEffect.Play();
    }
}
