using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeeShield : MonoBehaviour
{
    [SerializeField] private PlayerScript playerScript = null;
    [SerializeField] private GameManager gameManager = null;
    [SerializeField] private GameObject shield = null;

    [SerializeField] private float blockInvincibility = 0;
    public float manaCost = 0;
    [SerializeField] private float manaGained = 0;
    [SerializeField] private float chipBlockMultiplier = 0;
    private bool chipBlocked = false;
    
    private float t;
    private float shieldHoldDelay = 0;
    
    [HideInInspector] public bool successfulBlock = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.tag == "ProjectileAttack" || other.tag == "BossAttack") && playerScript.inputManager.holdingShield)
        {
            if (t < .10)
                JustBlock();
            else if (t > 0.25f)
                ChipBlock();

            StartCoroutine(GeneralBlock());
        }
    }

    private void Update()
    {
        if (playerScript.inputManager.holdingShield)
            t += Time.deltaTime;
        else
            t = 0;
        
        if (!playerScript.inputManager.holdingShield)
        {
            shieldHoldDelay += Time.deltaTime;
            if(shieldHoldDelay > .20)
            {
                shield.SetActive(false);
                playerScript.playerCollision.isDamagable = true;
                shieldHoldDelay = 0;
            }
        }
    }

    private IEnumerator GeneralBlock()
    {
        successfulBlock = true;
        playerScript.inputManager.holdingShield = false;
        yield return new WaitForSeconds(blockInvincibility);
        if (chipBlocked)
            chipBlocked = false; //Invul is handled via PlayerCollision for ChipBlock
        else
            playerScript.playerCollision.isDamagable = true;

        playerScript.zeeMana.canDoMagic = true;
    }

    private void JustBlock()
    {
        playerScript.currentMana += manaGained;
        playerScript.manaBar.SetCurrent(playerScript.currentMana);
    }

    private void ChipBlock()
    {
        chipBlocked = true;
        StartCoroutine(playerScript.playerCollision.TakeDamage(gameManager.bossManager.bossAttackDamage * chipBlockMultiplier, blockInvincibility));
    }

    public void StartShield()
    {
        if (playerScript.currentMana > 0 && playerScript.zeeMana.canDoMagic)
        {
            playerScript.zeeMana.canDoMagic = false;
            shield.SetActive(true);
            playerScript.inputManager.holdingShield = true;
            playerScript.playerCollision.isDamagable = false;
            playerScript.currentMana -= playerScript.zeeShield.manaCost;
            playerScript.manaBar.SetCurrent(playerScript.currentMana);
        }
    }
}
