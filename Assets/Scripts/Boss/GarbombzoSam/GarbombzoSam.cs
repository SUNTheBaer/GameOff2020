using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbombzoSam : MonoBehaviour
{
    [SerializeField] private GameManager gameManager = null;
    [SerializeField] private ScriptableBoss garbombzoSam = null;
    [SerializeField] private Animator anim = null;
    [SerializeField] private GameObject player = null;

    [SerializeField] private GameObject whirlwindAttack = null;

    private AttackChances whirlwind = new AttackChances(0.0f, "Whirlwind");
    private AttackChances bombThrow = new AttackChances(0.8f, "BombThrow");
    private AttackChances circleZones = new AttackChances(0.2f, "CircleZones");
    private AttackChances hammerSwipe = new AttackChances(0.0f, "HammerSwipe");

    private Vector2 playerPos;
    private Vector2 bossPos;
    private float deltaX;
    private float deltaY;
    private float angle;
    private bool swingingHammer = false;

    private int colliderPriority;

    private void Start()
    {
        gameManager.bossManager.bossHealth = garbombzoSam.bossHealth;
        bossPos = transform.position;
        PickAttack(whirlwind, bombThrow, circleZones, hammerSwipe);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerAttack")
            StartCoroutine(TakeDamage());
    }

    private void Update()
    {
        if (swingingHammer)
            HammerLerp();
        else
            transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
        

        whirlwind.chance = garbombzoSam.attackChances[0];
        bombThrow.chance = garbombzoSam.attackChances[1];
        circleZones.chance = garbombzoSam.attackChances[2];
        hammerSwipe.chance = garbombzoSam.attackChances[3];
        colliderPriority = garbombzoSam.colliderPriority;
    }

    private void PickAttack(AttackChances whirlwind, AttackChances bombThrow, AttackChances circleZones, AttackChances hammerSwipe)
    {
        AttackChances[] chances = new AttackChances[] {whirlwind, bombThrow, circleZones, hammerSwipe};
        float runningChance = 0;
        float chance = Random.value;

        foreach (AttackChances probability in chances)
        {
            if (chance <= probability.chance + runningChance)
            {
                StartCoroutine(probability.attackName);
                break;
            }
            else
                runningChance += probability.chance;
        }
    }

    // Attacks
    private IEnumerator Whirlwind()
    {
        gameManager.bossManager.bossAttackDamage = 20;
        anim.SetBool("whirlwind", true);
        yield return new WaitForSeconds(1.5f);
        whirlwindAttack.SetActive(true);
        yield return new WaitForSeconds(3);
        //yield return null;
        whirlwindAttack.SetActive(false);
        anim.SetBool("whirlwind", false);
        yield return new WaitForSeconds(.75f);
        PickAttack(whirlwind, bombThrow, circleZones, hammerSwipe);
    }

    private IEnumerator BombThrow()
    {
        gameManager.bossManager.bossAttackDamage = 20;
        yield return null;
        PickAttack(whirlwind, bombThrow, circleZones, hammerSwipe);
    }

    private IEnumerator CircleZones()
    {
        gameManager.bossManager.bossAttackDamage = 20;
        yield return null;
        PickAttack(whirlwind, bombThrow, circleZones, hammerSwipe);
    }

    private IEnumerator HammerSwipe()
    {
        swingingHammer = true;
        gameManager.bossManager.bossAttackDamage = 20;
        anim.SetTrigger("hammer reel");
        yield return new WaitForSeconds(1.5f);
        anim.SetTrigger("hammer punch");
        swingingHammer = false;
        PickAttack(whirlwind, bombThrow, circleZones, hammerSwipe);
    }

    private void HammerLerp()
    {

    }
    // ------------------------------------------------------------------

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