using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericBoss : MonoBehaviour
{
    [SerializeField] private GameManager gameManager = null;
    //[SerializeField] private ScriptableBoss bossName;
    // private AttackChances attackOne = new AttackChances(0.0f, "AttackOne");
    // public Bar bossHealthBar;
    // private bool playerHitBoss = false;

    private void Start()
    {
        //gameManager.bossManager.bossHealth = bossName.bossHealth;
        // gameManager.bossManager.bossCurrentHealth = bossName.bossMaxHealth;
        // bossHealthBar.SetMax(bossName.bossMaxHealth);

        // gameManager.bossManager.playerAttackDamage = 10;
        //PickAttack(attackOne);
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.tag == "PlayerAttack")
    //         playerHitBoss = true;
    // }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.gameObject.CompareTag("PlayerAttack"))
    //         playerHitBoss = false;
    // }

    private void Update()
    {
        // if(playerHitBoss)
        //     StartCoroutine(TakeDamage());
    }

    /*private void PickAttack(AttackChances attackOne, ...)
    {
        AttackChances[] chances = new AttackChances[] {attackNames};
        float runningChance = 0;
        float chance = Random.value;

        foreach (AttackChances probability in chances)
        {
            Debug.Log(probability.attackName);
            if (chance <= probability.chance + runningChance)
            {
                StartCoroutine(probability.attackName);
                break;
            }
            else
                runningChance += probability.chance;
        }
    }*/

    private IEnumerator GenericAttack()
    {
        gameManager.bossManager.bossAttackDamage = 20;
        yield return null;
        //PickAttack(attackOne);
    }

    private IEnumerator TakeDamage()
    {
        gameManager.bossManager.bossCurrentHealth -= gameManager.bossManager.playerAttackDamage;
        yield return null;

        // bossHealthBar.SetCurrent(gameManager.bossManager.bossCurrentHealth);

        if (gameManager.bossManager.bossCurrentHealth <= 0)
            StartCoroutine(Death());
    }

    private IEnumerator Death()
    {
        gameObject.SetActive(false);
        yield return null;
    }
}