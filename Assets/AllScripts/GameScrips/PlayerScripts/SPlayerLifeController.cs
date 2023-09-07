using System.Collections;
using TMPro;
using UnityEngine;
public class SPlayerLifeController : SCollisionController
{
    public TextMeshProUGUI HealthMonitor;
    const int startingHealthPoints = 10;
    private int healthPoints = startingHealthPoints;
    public SPlayerMovement playerMovement;
    Rigidbody playerRb;
    [SerializeField] ParticleSystem damageEffect;
    bool gotWound;
    private void Start()
    {
        damageEffect.Stop();
        playerRb = playerMovement.rb;
    }
    private void OnEnable()
    {
        onDamageCollided += () => TakingDamage(1, true);
        playerMovement.onPlayerFell += () => TakingDamage(5, false);
    }
    private void OnDisable()
    {
        onDamageCollided += () => TakingDamage(1, true);
        playerMovement.onPlayerFell += () => TakingDamage(5, false);
    }
    void Update()
    {
        HealthMonitor.text = healthPoints.ToString();

    }
    void TakingDamage(int damage, bool gotWound)
    {
        StartCoroutine(DamageControll(damage, gotWound));
    }
    void ShowDamageEffect(bool gotWound)
    {
        if (gotWound)
            damageEffect.Play();
        int force = 5;
        playerRb.AddForce(Vector3.up * force, ForceMode.Impulse);
        HealthMonitor.color = Color.red;
    }

    private IEnumerator DamageControll(int damageValue, bool gotWound)
    {
        int invulnerabilityTime = 2;
        healthPoints -= damageValue;
        ShowDamageEffect(gotWound);
        yield return new WaitForSeconds(0.5f);
        HealthMonitor.color = Color.white;
        yield return new WaitForSeconds(invulnerabilityTime);
    }
}
