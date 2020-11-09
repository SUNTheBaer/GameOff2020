using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericBoss : MonoBehaviour
{
    [SerializeField] private GameManager gameManager = null;
    //[SerializeField] private ScriptableBoss bossName;
    // private AttackChances attackOne = new AttackChances(0.0f, "AttackOne");

    private void Start()
    {
        //gameManager.bossManager.bossHealth = bossName.bossHealth;
        //PickAttack(attackOne);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerAttack")
            StartCoroutine(TakeDamage());
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
        gameManager.bossManager.bossHealth -= gameManager.bossManager.playerAttackDamage;
        yield return null;
        if (gameManager.bossManager.bossHealth <= 0)
            StartCoroutine(Death());
    }

    private IEnumerator Death()
    {
        gameObject.SetActive(false);
        yield return null;
    }
}