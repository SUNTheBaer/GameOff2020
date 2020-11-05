using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericBoss : MonoBehaviour
{
    [SerializeField] private GameManager gameManager = null;
    //[SerializeField] private ScriptableBoss bossName;

    private void Start()
    {
        //gameManager.bossManager.bossHealth = bossName.bossHealth;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerAttack")
            StartCoroutine(TakeDamage());
    }

    private IEnumerator GenericAttack()
    {
        gameManager.bossManager.bossAttackDamage = 20;
        yield return null;
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