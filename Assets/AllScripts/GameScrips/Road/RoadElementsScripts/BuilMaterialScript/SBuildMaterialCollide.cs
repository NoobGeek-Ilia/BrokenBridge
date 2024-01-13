using UnityEngine;

public class SBuildMaterialCollide : MonoBehaviour
{
    [SerializeField] private ParticleSystem bonusEffect;

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
            PlayBonusEffect();
        }
    }
    private void PlayBonusEffect()
    {
        Vector3 newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        ParticleSystem newEffect = Instantiate(bonusEffect, newPos, Quaternion.identity);
        newEffect.Play();
    }
}
