#pragma warning disable 0414
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericBoss : MonoBehaviour
{
    [Header("Scripts and Refs")]
    public GameObject player = null;
    public PlayerScript playerScript = null;
    public Animator anim = null;
    [SerializeField] private GameManager gameManager = null;
    //[SerializeField] private ScriptableBoss bossName = null;
    [SerializeField] private DialogueTrigger dialogueTrigger = null;
    //[SerializeField] private AttackOneScript attackOneScript = null;
    private int colliderPriority;

    [Header("Attack Chances")]
    private AttackChances attackOne = new AttackChances(1.0f, "AttackOne");

    [Header("Health")]
    [SerializeField] private float bossMaxHealth = 0;
    private float bossCurrentHealth = 0;
    [SerializeField] private Bar bossHealthBar = null;
    [HideInInspector] public bool playerHitBoss = false;
    private bool battleStarted = false;

    private void Start()
    {
        dialogueTrigger.TriggerDialogue();

        bossCurrentHealth = bossMaxHealth;
        bossHealthBar.SetMax(bossMaxHealth);
    }

    private void StartBattle()
    {
        battleStarted = true;
        PickAttack();
    }

    private void Update()
    {
        //attackOne.chance = bossName.attackChances[0];
        //colliderPriority = bossName.colliderPriority;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ZeeAttack"))
            StartCoroutine(TakeDamage(playerScript.damage));
    }

    public void PickAttack()
    {
        AttackChances[] chances = new AttackChances[] {attackOne};
        float runningChance = 0;
        float chance = Random.value;

        foreach (AttackChances probability in chances)
        {
            if (chance <= probability.chance + runningChance)
            {
                Invoke(probability.attackName, 0f);
                break;
            }
            else
                runningChance += probability.chance;
        }
    }

    //Attacks
    private void AttackOne()
    {
        //StartCoroutine(attackOneScript.StartAttackOne());
    }
    //-------------------------------------------------------------

    private IEnumerator TakeDamage(float damage)
    {
        bossCurrentHealth -= damage;

        yield return null;

        bossHealthBar.SetCurrent(bossCurrentHealth);
        
        if (bossCurrentHealth <= 0)
            StartCoroutine(Death());
    }

    private IEnumerator Death()
    {
        //death anim
        //player win sihlouette or vignette or somethin
        //maybe dying dialogue
        //send player back to hub
        gameObject.SetActive(false);
        yield return null;
    }
}