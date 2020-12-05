using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbombzoSam : MonoBehaviour
{
    [Header("Scripts and Refs")]
    public GameObject player = null;
    public PlayerScript playerScript = null;
    public Animator anim = null;
    [SerializeField] private BossProbability bossProbability = null;
    [SerializeField] private DialogueTrigger dialogueTrigger = null;
    [SerializeField] private GSWhirlwind garbombzoSamWhirlwind = null;
    [SerializeField] private GSBombThrow garbombzoSamBombThrow = null;
    [SerializeField] private GSPunch garbombzoSamPunch = null;
    [SerializeField] private GSSitDown garbomzoSamSitDown = null;
    [SerializeField] private GSTrackingBomb garbombzoSamTrackingBomb = null;
    private int colliderPriority;

    [Header("Attack Chances")]
    private AttackChances whirlwind = new AttackChances(0.0f, "Whirlwind");
    private AttackChances bombThrow = new AttackChances(1.0f, "BombThrow");
    private AttackChances punch = new AttackChances(0.0f, "Punch");
    private AttackChances sitDown = new AttackChances(0.0f, "SitDown");
    private AttackChances trackingBombAttack = new AttackChances(0.0f, "TrackingBombAttack");

    [Header("Health")]
    [SerializeField] private float bossMaxHealth = 0;
    private float bossCurrentHealth = 0;
    [SerializeField] private Bar bossHealthBar = null;
    private bool battleStarted = false;
    
    [Header("Punch Lerp")]
    [SerializeField] private float lerpDuration = 0;
    [SerializeField] private float reverseLerpDuration = 0;
    [HideInInspector] public float time;
    [HideInInspector] public float angleAxis;
    [HideInInspector] public bool swingingHammer = false;

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
        //Waiting for dialogue
        if (!playerScript.dialogueManager.dialogueOccurring && !battleStarted)
            StartBattle();
        //----------------------------------------------------------------------------
        //Hammer Lerping
        time += Time.deltaTime;
        if (swingingHammer && (Mathf.Abs(playerScript.angle + 7) < garbombzoSamPunch.hammerMaxAngle))
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
        else if (Mathf.Abs(playerScript.angle + 7) >= garbombzoSamPunch.hammerMaxAngle)
        {
            if (angleAxis < 0)
                angleAxis = -garbombzoSamPunch.hammerMaxAngle;
            else
                angleAxis = garbombzoSamPunch.hammerMaxAngle;
        }
        //----------------------------------------------------------------------------
        //Syncing attack chances with probability colliders
        whirlwind.chance = bossProbability.attackChances[0];
        bombThrow.chance = bossProbability.attackChances[1];
        punch.chance = bossProbability.attackChances[2];
        sitDown.chance = bossProbability.attackChances[3];
        trackingBombAttack.chance = bossProbability.attackChances[4];
        colliderPriority = bossProbability.colliderPriority;
        //-------------------------------------------------------------------------------
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ZeeAttack"))
            StartCoroutine(TakeDamage(playerScript.damage));
    }

    public void PickAttack()
    {
        AttackChances[] chances = new AttackChances[] {whirlwind, bombThrow, punch, sitDown, trackingBombAttack};
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

    // Attacks
    private void Whirlwind()
    {
        garbombzoSamWhirlwind.StartWhirlwind();
    }

    private void BombThrow()
    {
        StartCoroutine(garbombzoSamBombThrow.StartBombThrow());
    }

    private void Punch()
    {
        StartCoroutine(garbombzoSamPunch.StartPunch());
    }

    private void SitDown()
    {
        StartCoroutine(garbomzoSamSitDown.StartSitDown());
    }

    private void TrackingBombAttack()
    {
        StartCoroutine(garbombzoSamTrackingBomb.StartTrackingBomb());
    }
    // ------------------------------------------------------------------

    public IEnumerator TakeDamage(float damage)
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