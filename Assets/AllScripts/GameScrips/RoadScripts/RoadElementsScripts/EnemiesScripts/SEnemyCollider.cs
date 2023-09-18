using System.Collections;
using UnityEngine;

public class SEnemyCollider : MonoBehaviour
{
    SWeapon weapon;
    GameObject missMessagePrefab;
    [SerializeField] ParticleSystem damageEffect;
    private void Start()
    {
        weapon = GameObject.Find("Weapons").GetComponent<SWeapon>();
        missMessagePrefab = Resources.Load<GameObject>("Prefabs/Enemies/MissMessage");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WeaponTag"))
        {
            StartCoroutine(DamageControll(weapon.missHitProb[SGlobalGameInfo.selectedWeapon]));
        }
    }
    private void DamageEffect()
    {
        float offcet = transform.localScale.y / 2;
        Vector3 newPos = new Vector3(transform.position.x, transform.position.y + offcet + 1, transform.position.z);
        ParticleSystem newEffect = Instantiate(damageEffect, newPos, damageEffect.transform.rotation);
        newEffect.Play();
    }
    private void MissEffect()
    {
        float offcet = transform.localScale.x / 2;
        Vector3 newPos = new Vector3(transform.position.x - offcet, transform.position.y, transform.position.z);
        Instantiate(missMessagePrefab, newPos, missMessagePrefab.transform.rotation, transform);
    }

    private IEnumerator DamageControll(int selectedWeapon)
    {
        int hitProbability = UnityEngine.Random.Range(0, selectedWeapon);
        if (hitProbability > 0)
        {
            DamageEffect();
            gameObject.SetActive(false);
        }
        else
            MissEffect();
        yield return null;
    }
}
