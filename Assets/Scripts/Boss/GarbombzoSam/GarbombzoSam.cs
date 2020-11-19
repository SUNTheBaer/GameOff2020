using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbombzoSam : MonoBehaviour
{
    [SerializeField] private GameManager gameManager = null;
    [SerializeField] private ScriptableBoss garbombzoSam = null;
    [SerializeField] private Animator anim = null;
    [SerializeField] private GameObject player = null;
    [SerializeField] private GameObject garbomzoSamProjectile = null;

    [SerializeField] private GameObject whirlwindAttack = null;

    private AttackChances whirlwind = new AttackChances(0.0f, "Whirlwind");
    private AttackChances bombThrow = new AttackChances(0.8f, "BombThrow");
    private AttackChances circleZones = new AttackChances(0.2f, "CircleZones");
    private AttackChances hammerSwipe = new AttackChances(0.0f, "HammerSwipe");

    [SerializeField] private float duration = 0;
    [SerializeField] private float reverseDuration = 0;
    private Vector2 playerPos;
    private Vector2 bossPos;
    private float time;
    private float deltaX;
    private float deltaY;
    private float angle;
    private float angleAxis;
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
        //Hammer Lerping
        time += Time.deltaTime;

        if (swingingHammer)
            HammerLerp();
        else
        {
            angleAxis = Mathf.Lerp(angle, 0, time / reverseDuration);

            transform.rotation = Quaternion.AngleAxis(angleAxis, Vector3.forward);
        }
        //----------------------------------------------------------------------------

        //Player and Boss positions
        gameManager.bossManager.playerPosition = player.transform.position;
        gameManager.bossManager.bossPosition = transform.position;

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
        anim.SetTrigger("throw");
        yield return new WaitForSeconds(0.5f);
        GameObject projectile = Instantiate(garbomzoSamProjectile, new Vector2(transform.position.x, transform.position.y - 1), Quaternion.identity);
        StartCoroutine(projectile.GetComponent<GarbombzoSamProjectile>().Shoot(new Vector2
            (player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y + 1)));
        yield return new WaitForSeconds(2.0f);
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
        time = 0;
        gameManager.bossManager.bossAttackDamage = 20;
        anim.SetTrigger("hammer reel");
        yield return new WaitForSeconds(1.5f);
        anim.SetTrigger("hammer punch");
        yield return new WaitForSeconds(.9f);
        swingingHammer = false;
        time = 0;
        yield return new WaitForSeconds(1f);
        PickAttack(whirlwind, bombThrow, circleZones, hammerSwipe);
    }

    private void HammerLerp()
    {
        angleAxis = 0;
        angleAxis = Mathf.Lerp(0, angle, time/duration);
        playerPos = player.transform.position;
        deltaX = bossPos.x - playerPos.x;
        deltaY = bossPos.y - playerPos.y;

        angle = Mathf.Atan2(deltaY, deltaX) * Mathf.Rad2Deg - 83;

        transform.rotation = Quaternion.AngleAxis(angleAxis, Vector3.forward);
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