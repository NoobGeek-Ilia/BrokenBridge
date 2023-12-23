using System.Collections;
using UnityEngine;

public class SEnemyCollider : MonoBehaviour
{
    GameObject missMessagePrefab;
    [SerializeField] ParticleSystem damageEffect;
    private StateMonitor stateMonitor;
    private SEnemySoundController soundController;

    private void Start()
    {
        stateMonitor = FindObjectOfType<StateMonitor>();
        soundController = FindObjectOfType<SEnemySoundController>();
        missMessagePrefab = Resources.Load<GameObject>("Prefabs/Enemies/Effects/MissMessage");
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
            case "CritterWood":
                main.startColor = new Color(0.6f, 0.2f, 0.8f);
                break;
            case "BigFoot":
                main.startColor = Color.white;
                break;
            case "Penguin":
                main.startColor = Color.black;
                break;
            case "CritterIce":
                main.startColor = Color.blue;
                break;
            case "Pigeon":
                main.startColor = Color.gray ;
                break;
            case "Rat":
                main.startColor = Color.white;
                break;
            case "CritterCity":
                main.startColor = new Color(0.6f, 0.2f, 0.8f);
                break;
            case "Monkey":
                main.startColor = new Color(0.6f, 0.2f, 0.1f);
                break;
            case "Crab":
                main.startColor = new Color(0.9f, 0.4f, 0.0f);
                break;
            case "CritterTropic":
                main.startColor = Color.yellow;
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
            soundController.PlaySmashSound();
        }
        else
        {
            MissEffect();
        }
        yield return null;
    }
}
