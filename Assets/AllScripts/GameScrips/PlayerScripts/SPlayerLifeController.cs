using System;
using System.Collections;
using TMPro;
using UnityEngine;
public class SPlayerLifeController : SCollisionController
{
    public TextMeshProUGUI HealthMonitor;
    private int currHelthPoint;
    private int healthPoints
    {
        get { return currHelthPoint; }
        set
        {
            if (value < 0)
                currHelthPoint = 0;
            else
                currHelthPoint = value;
        }
    }
    internal protected bool playerDied { get; private set; }
    public SPlayerMovement playerMovement;
    Rigidbody playerRb;
    [SerializeField] ParticleSystem damageEffect;
    [SerializeField] ParticleSystem deadEffect;
    internal protected Action OnPlayerDied;
    [SerializeField] SGameOverPanel gameOverPanel;
    [SerializeField] StateMonitor stateMonitor;
    [SerializeField] SPlayerSoundController soundController;

    bool gotWound;
    private void Start()
    {
        currHelthPoint = SCharacterTab.maxCurrCharacterHp;
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
        onDamageCollided -= () => TakingDamage(1, true);
        playerMovement.onPlayerFell -= () => TakingDamage(5, false);
    }
    void Update()
    {
        HealthMonitor.text = healthPoints.ToString();
        if (healthPoints < 1 && !playerDied)
        {
            stateMonitor.currCharacter.SetActive(false);
            deadEffect.Play();
            StartCoroutine(KillPlayer());
        }
    }
    void TakingDamage(int damage, bool gotWound)
    {
        StartCoroutine(DamageControll(damage, gotWound));
        soundController.PlayCharacterGettingHitSound();
    }
    void ShowDamageEffect(bool gotWound)
    {
        if (gotWound)
            damageEffect.Play();
        int force = 2;
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
    private IEnumerator KillPlayer()
    {
        playerDied = true;
        yield return new WaitForSeconds(2f);
        gameOverPanel.OpenPanel();
        OnPlayerDied?.Invoke();
        
    }
    internal protected void ResetPlayerHealsPoint()
    {
        playerDied = false;
        stateMonitor.allCharacters[SGlobalGameInfo.selectedCharacter].SetActive(true);
        //currHelthPoint = SCharacterTab.maxCurrCharacterHp;
    }
}
