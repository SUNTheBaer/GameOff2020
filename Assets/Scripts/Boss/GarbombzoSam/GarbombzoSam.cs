using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbombzoSam : MonoBehaviour
{
    [Header("Scripts and Refs")]
    [SerializeField] private GameManager gameManager = null;
    [SerializeField] private PlayerScript playerScript = null;  
    [SerializeField] private ScriptableBoss garbombzoSam = null;
    [SerializeField] private Animator anim = null;
    [SerializeField] private GameObject player = null;
    private int colliderPriority;

    [Header("Whirlwind Attack")]
    [SerializeField] private float whirlwindDamage = 0;
    [SerializeField] private float whirlwindKnockbackTime = 0;
    [SerializeField] private float whirlwindKnockbackForce = 0;
    [SerializeField] private float whirlwindAttackLength = 0;
    [SerializeField] private float whirlwindWaitForNextAttack = 0;
    private AttackChances whirlwind = new AttackChances(0.0f, "Whirlwind");

    [Header("Bomb Throw Attack")]
    [SerializeField] private GameObject garbomzoSamProjectile = null;
    [SerializeField] private float bombThrowDamage = 0;
    [SerializeField] private float bombThrowKnockbackTime = 0;
    [SerializeField] private float bombThrowKnockbackForce = 0;
    [SerializeField] private float bombThrowWaitForNextAttack = 0;
    private AttackChances bombThrow = new AttackChances(1.0f, "BombThrow");

    [Header("Hammer Attack")]
    [SerializeField] private float hammerDamage = 0;
    [SerializeField] private float hammerKnockbackTime = 0;
    [SerializeField] private float hammerKnockbackForce = 0;
    [SerializeField] private float hammerReelTime = 0;
    [SerializeField] private float hammerMaxAngle = 0;
    [SerializeField] private float hammerWaitForNextAttack = 0;
    private AttackChances hammerSwipe = new AttackChances(0.0f, "HammerSwipe");

    [Header("Sit Down Attack")]
    [SerializeField] private float sitDownDamage = 0;
    [SerializeField] private float sitDownKnockbackTime = 0;
    [SerializeField] private float sitDownKnockbackForce = 0;
    [SerializeField] private float sitDownWaitForNextAttack = 0;
    private AttackChances sitDown = new AttackChances(0.0f, "SitDown");

    [Header("Tracking Bomb Attack")]
    [SerializeField] private GameObject trackingBombIndicator = null;
    [SerializeField] private CapsuleCollider2D trackingBombCollider = null;
    [SerializeField] private float trackingBombAttackDamage = 0;
    [SerializeField] private float trackingBombAttackKnockbackTime = 0;
    [SerializeField] private float trackingBombAttackKnockbackForce = 0;
    [SerializeField] private float trackingBombAttackWaitForNextAttack = 0;
    private AttackChances trackingBombAttack = new AttackChances(0.0f, "TrackingBombAttack");
    private bool trackingBombActive = false;

    [Header("Health")]
    [SerializeField] private Bar bossHealthBar = null;
    [HideInInspector] public bool playerHitBoss = false;
    
    [Header("Lerp")]
    [SerializeField] private float lerpDuration = 0;
    [SerializeField] private float reverseLerpDuration = 0;
    private float time;
    private float angleAxis;
    private bool swingingHammer = false;

    private void Start()
    {
        gameManager.bossManager.bossCurrentHealth = garbombzoSam.bossMaxHealth;
        bossHealthBar.SetMax(garbombzoSam.bossMaxHealth);

        gameManager.bossManager.playerAttackDamage = 20;

        PickAttack(whirlwind, bombThrow, hammerSwipe, sitDown, trackingBombAttack);
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
        //Hammer Lerping
        time += Time.deltaTime;
        if (swingingHammer && (Mathf.Abs(playerScript.angle + 7) < hammerMaxAngle))
        {
            angleAxis = 0;
            angleAxis = Mathf.Lerp(0, playerScript.angle + 7, time / lerpDuration);
            transform.rotation = Quaternion.AngleAxis(angleAxis, Vector3.forward);
        }
        else if (!swingingHammer)
        {
            angleAxis = Mathf.Lerp(angleAxis, 0, time / reverseLerpDuration);
            transform.rotation = Quaternion.AngleAxis(angleAxis, Vector3.forward);
        }
        else if (Mathf.Abs(playerScript.angle + 7) >= hammerMaxAngle)
        {
            if (angleAxis < 0)
                angleAxis = -hammerMaxAngle;
            else
                angleAxis = hammerMaxAngle;
        }

        // Tracking bomb attack
        if (trackingBombActive)
        {
            trackingBombIndicator.transform.position = player.transform.position;
        }
        //----------------------------------------------------------------------------

        gameManager.bossManager.bossPosition = transform.position;
        gameManager.bossManager.playerPosition = player.transform.position;

        whirlwind.chance = garbombzoSam.attackChances[0];
        bombThrow.chance = garbombzoSam.attackChances[1];
        hammerSwipe.chance = garbombzoSam.attackChances[2];
        sitDown.chance = garbombzoSam.attackChances[3];
        trackingBombAttack.chance = garbombzoSam.attackChances[4];
        colliderPriority = garbombzoSam.colliderPriority;

        if(playerHitBoss)
            StartCoroutine(TakeDamage());
    }

    private void PickAttack(AttackChances whirlwind, AttackChances bombThrow, AttackChances hammerSwipe, AttackChances sitDown, AttackChances trackingBombAttack)
    {
        playerScript.playerCollision.alreadyHit = false;
        AttackChances[] chances = new AttackChances[] {whirlwind, bombThrow, hammerSwipe, sitDown, trackingBombAttack};
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
        gameManager.bossManager.bossAttackDamage = whirlwindDamage;
        gameManager.bossManager.knockbackTime = whirlwindKnockbackTime;
        gameManager.bossManager.knockbackForce = whirlwindKnockbackForce;
        gameManager.bossManager.knockbackDirection = Vector2.zero;
        anim.SetBool("whirlwind", true);

        yield return new WaitForSeconds(whirlwindAttackLength);

        anim.SetBool("whirlwind", false);

        yield return new WaitForSeconds(whirlwindWaitForNextAttack);

        PickAttack(whirlwind, bombThrow, sitDown, hammerSwipe, trackingBombAttack);
    }

    private IEnumerator BombThrow()
    {
        gameManager.bossManager.bossAttackDamage = bombThrowDamage;
        gameManager.bossManager.knockbackTime = bombThrowKnockbackTime;
        gameManager.bossManager.knockbackForce = bombThrowKnockbackForce;
        anim.SetTrigger("throw");

        yield return new WaitForSeconds(0.5f);

        GameObject projectile = Instantiate(garbomzoSamProjectile, new Vector2(transform.position.x, transform.position.y - 1), Quaternion.identity);
        GarbombzoSamProjectile projectileScript = projectile.GetComponent<GarbombzoSamProjectile>();
        StartCoroutine(projectileScript.Shoot(new Vector2(player.transform.position.x - transform.position.x, 
            player.transform.position.y - transform.position.y + 1)));

        yield return new WaitForSeconds(bombThrowWaitForNextAttack);

        PickAttack(whirlwind, bombThrow, sitDown, hammerSwipe, trackingBombAttack);
    }

    private IEnumerator HammerSwipe()
    {
        swingingHammer = true;
        time = 0;
        gameManager.bossManager.bossAttackDamage = hammerDamage;
        gameManager.bossManager.knockbackTime = hammerKnockbackTime;
        gameManager.bossManager.knockbackForce = hammerKnockbackForce;
        anim.SetTrigger("hammer reel");

        yield return new WaitForSeconds(hammerReelTime); //Initial hit wait time
        
        anim.SetTrigger("hammer punch");
        gameManager.bossManager.knockbackDirection = new Vector2(Mathf.Cos((angleAxis - 90) * Mathf.Deg2Rad), Mathf.Sin((angleAxis - 90) * Mathf.Deg2Rad));

        yield return new WaitForSeconds(.833f); //Length of punch anim

        anim.SetTrigger("hammer reel");
        playerScript.playerCollision.alreadyHit = false;

        yield return new WaitForSeconds(.333f); //Speed up second hit
        
        anim.SetTrigger("hammer punch");
        gameManager.bossManager.knockbackDirection = new Vector2(Mathf.Cos((angleAxis - 90) * Mathf.Deg2Rad), Mathf.Sin((angleAxis - 90) * Mathf.Deg2Rad));

        yield return new WaitForSeconds(.833f); //Length of punch anim

        time = 0;
        swingingHammer = false;

        yield return new WaitForSeconds(hammerWaitForNextAttack);

        PickAttack(whirlwind, bombThrow, sitDown, hammerSwipe, trackingBombAttack);
    }

    private IEnumerator SitDown()
    {
        gameManager.bossManager.bossAttackDamage = sitDownDamage;
        gameManager.bossManager.knockbackTime = sitDownKnockbackTime;
        gameManager.bossManager.knockbackForce = sitDownKnockbackForce;
        gameManager.bossManager.knockbackDirection = Vector2.zero;

        yield return null;

        PickAttack(whirlwind, bombThrow, sitDown, hammerSwipe, trackingBombAttack);
    }

    private IEnumerator TrackingBombAttack()
    {
        gameManager.bossManager.bossAttackDamage = trackingBombAttackDamage;
        gameManager.bossManager.knockbackTime = trackingBombAttackKnockbackTime;
        gameManager.bossManager.knockbackForce = trackingBombAttackKnockbackForce;
        gameManager.bossManager.knockbackDirection = Vector2.zero;

        // yield return new WaitForSeconds(0.5f); // Might not need this? Or maybe play animation of launching bomb into air before this

        // 2 seconds of shadow tracking player
        trackingBombActive = true;
        trackingBombIndicator.SetActive(true);

        yield return new WaitForSeconds(2.0f);

        // Biggest shadow circle - Pick player's current position and wait ie. 0.25 seconds before bomb lands
        trackingBombActive = false;

        yield return new WaitForSeconds(0.25f);

        // Bomb landing Boom - beeg damages and knockback!!! (Activate collider)
        trackingBombCollider.enabled = true;
        if ((Vector2)(player.transform.position - trackingBombIndicator.transform.position) != Vector2.zero)
            gameManager.bossManager.knockbackDirection = (Vector2)(player.transform.position - trackingBombIndicator.transform.position);
        else
            gameManager.bossManager.knockbackDirection = Vector2.down;

        yield return new WaitForSeconds(0.35f); // Amount of bomb exploding time (make variable?)

        trackingBombIndicator.SetActive(false);
        trackingBombCollider.enabled = false;

        yield return new WaitForSeconds(trackingBombAttackWaitForNextAttack);

        PickAttack(whirlwind, bombThrow, sitDown, hammerSwipe, trackingBombAttack);
    }
    // ------------------------------------------------------------------

    public IEnumerator TakeDamage()
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