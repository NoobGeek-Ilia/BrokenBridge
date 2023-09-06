using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
public class SPlayerLifeController : SCollisionController
{
    public TextMeshProUGUI HealthMonitor;
    const int startingHealthPoints = 10;
    private int healthPoints = startingHealthPoints;
    public SPlayerMovement playerMovement;
    Rigidbody playerRb;
    private void Start()
    {
        playerRb = playerMovement.rb;
    }
    private void OnEnable()
    {
        onDamageCollided += TakingDamage;
    }
    private void OnDisable()
    {
        onDamageCollided -= TakingDamage;
    }
    void Update()
    {
        HealthMonitor.text = healthPoints.ToString();
    }
    void TakingDamage()
    {
        StartCoroutine(DamageControll());
    }
    void ShowDamageEffect()
    {
        int force = 5;
        playerRb.AddForce(Vector3.up * force, ForceMode.Impulse);
        HealthMonitor.color = Color.red;
    }
    
    private IEnumerator DamageControll()
    {
        healthPoints--;
        ShowDamageEffect();
        yield return new WaitForSeconds(0.5f);
        HealthMonitor.color = Color.white;
        yield return new WaitForSeconds(1);
        
    }
}
