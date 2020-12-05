using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeeShield : MonoBehaviour
{
    [SerializeField] private PlayerScript playerScript = null;
    [SerializeField] private GameObject shield = null;

    [HideInInspector] public bool holdingShield = false;
    [HideInInspector] public bool successfulBlock = false;
    [HideInInspector] public bool chipBlocked = false;

    public float manaCost = 0;
    [SerializeField] private float manaGained = 0;
    public float blockInvincibility = 0;
    public float chipBlockMultiplier = 0;
    private float t;
    private float shieldHoldDelay = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.CompareTag("PhysicalBossAttack") || other.gameObject.CompareTag("DisjointedBossAttack")) && holdingShield)
        {
            if (t < .10)
                JustBlock();
            else if (t > 0.25f)
            {
                playerScript.playerCollision.isDamagable = true;
                ChipBlock();
            }

            StartCoroutine(GeneralBlock());
        }
    }

    private void Update()
    {
        if (holdingShield)
            t += Time.deltaTime;
        else
            t = 0;
        
        if (!holdingShield)
        {
            shieldHoldDelay += Time.deltaTime;
            if(shieldHoldDelay > .20)
            {
                shield.SetActive(false);
                shieldHoldDelay = 0;
            }
        }
    }

    private IEnumerator GeneralBlock()
    {
        successfulBlock = true;
        playerScript.zeePosture.OnBlock();

        holdingShield = false;
        yield return new WaitForSeconds(blockInvincibility);
        if (chipBlocked)
            chipBlocked = false; //Invul is handled via PlayerCollision for ChipBlock
        else
            playerScript.playerCollision.isDamagable = true;

        playerScript.zeeMana.canDoMagic = true;
        successfulBlock = false;
    }

    private void JustBlock()
    {
        playerScript.currentMana += manaGained;
        playerScript.manaBar.SetCurrent(playerScript.currentMana);
    }

    private void ChipBlock()
    {
        chipBlocked = true;
    }

    public void StartShield()
    {
        if (playerScript.currentMana > 0 && playerScript.zeeMana.canDoMagic)
        {
            playerScript.zeeMana.canDoMagic = false;
            shield.SetActive(true);
            holdingShield = true;
            playerScript.playerCollision.isDamagable = false;
            playerScript.currentMana -= playerScript.zeeShield.manaCost;
            playerScript.manaBar.SetCurrent(playerScript.currentMana);
        }
    }

    public void StopShield()
    {
        holdingShield = false;
        playerScript.playerCollision.isDamagable = true;
        playerScript.zeeMana.canDoMagic = true;
    }
}
