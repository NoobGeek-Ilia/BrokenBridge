using System.Collections;
using UnityEngine;

public class SEnemyCollider : MonoBehaviour
{
    GameObject missMessagePrefab;
    [SerializeField] ParticleSystem damageEffect;
    private StateMonitor stateMonitor;

    private void Start()
    {
        stateMonitor = FindObjectOfType<StateMonitor>();
        missMessagePrefab = Resources.Load<GameObject>("Prefabs/Enemies/MissMessage");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WeaponTag"))
        {
            StartCoroutine(DamageControll(SWeaponInfo.missHitProb[SGlobalGameInfo.selectedWeapon]));
        }
    }
    private void DamageEffect()
    {
        float offcet = transform.localScale.y / 2;
        Vector3 newPos = new Vector3(transform.position.x, transform.position.y + offcet + 1, transform.position.z);
        ParticleSystem newEffect = Instantiate(damageEffect, newPos, damageEffect.transform.rotation);
        ParticleSystem.MainModule main = newEffect.main;
        string objectName = gameObject.name;
        switch (objectName)
        {
            case "Slime":
                main.startColor = Color.green;
                break;
            case "Spider":
                main.startColor = Color.black;
                break;
            case "Ñritter":
                main.startColor = new Color(0.6f, 0.2f, 0.8f);
                break;
        }
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
            stateMonitor.KilledEnemyesNum++;
        }
        else
            MissEffect();
        yield return null;
    }
}
