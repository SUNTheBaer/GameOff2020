#pragma warning disable 0414
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericBoss : MonoBehaviour
{
    [Header("Scripts and Refs")]
    [SerializeField] private GameManager gameManager = null;
    [SerializeField] private PlayerScript playerScript = null;
    [SerializeField] private ScriptableBoss bossName = null;
    [SerializeField] private Animator anim = null;
    [SerializeField] private GameObject player = null;
    private int colliderPriority;

    [Header("Attack One")]
    [SerializeField] private float attackOneDamage = 0;
    [SerializeField] private float attackOneKnockbackTime = 0;
    [SerializeField] private float attackOneKnockbackForce = 0;
    [SerializeField] private float attackOneWaitForNextAttack = 0;
    private AttackChances attackOne = new AttackChances(0.0f, "AttackOne");

    [Header("Health")]
    [SerializeField] private Bar bossHealthBar = null;
    private bool playerHitBoss = false;

    private void Start()
    {
        gameManager.bossManager.bossCurrentHealth = bossName.bossMaxHealth;
        bossHealthBar.SetMax(bossName.bossMaxHealth);

        gameManager.bossManager.playerAttackDamage = 20;

        PickAttack(attackOne);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerAttack")
            playerHitBoss = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerAttack"))
            playerHitBoss = false;
    }

    private void Update()
    {
        attackOne.chance = bossName.attackChances[0];
        colliderPriority = bossName.colliderPriority;

        if(playerHitBoss)
            StartCoroutine(TakeDamage());
    }

    private void PickAttack(AttackChances attackOne)
    {
        AttackChances[] chances = new AttackChances[] {attackOne};
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
    }

    //Attacks
    private IEnumerator AttackOne()
    {
        gameManager.bossManager.bossAttackDamage = attackOneDamage;
        gameManager.bossManager.knockbackTime = attackOneKnockbackTime;
        gameManager.bossManager.knockbackForce = attackOneKnockbackForce;
        gameManager.bossManager.knockbackDirection = Vector2.zero;

        yield return null;

        PickAttack(attackOne);
    }
    //-------------------------------------------------------------

    private IEnumerator TakeDamage()
    {
        gameManager.bossManager.bossCurrentHealth -= gameManager.bossManager.playerAttackDamage;
        yield return null;

        bossHealthBar.SetCurrent(gameManager.bossManager.bossCurrentHealth);

        if (gameManager.bossManager.bossCurrentHealth <= 0)
            StartCoroutine(Death());
    }

    private IEnumerator Death()
    {
        gameObject.SetActive(false);
        yield return null;
    }
}