using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private PlayerScript playerScript = null;
    [SerializeField] private CinemachineVirtualCamera deathCam = null;
    //[SerializeField] private GameObject deathText = null;
    [SerializeField] private GameObject deathTransition = null;

    [HideInInspector] public bool isDamagable = true;
    [SerializeField] private float invulTime = 0;
    [SerializeField] private float shieldKnockbackMultiplier = 0;
    [HideInInspector] public bool knockback = false;
    public bool alreadyHit = false;


    private void Start() 
    {
        playerScript.currentHealth = playerScript.maxHealth;
        playerScript.healthBar.SetMax(playerScript.maxHealth);

        //playerScript.audioSrc = GetComponent<AudioSource>();
        //playerScript.audioSrc.volume = 0f;
    }
    
    private IEnumerator Knockback(bool shieldOnHit)
    {
        if (shieldOnHit)
            playerScript.gameManager.bossManager.knockbackForce *= shieldKnockbackMultiplier;

        knockback = true;
        yield return new WaitForSeconds(playerScript.gameManager.bossManager.knockbackTime);
        knockback = false;
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if ((collision.gameObject.CompareTag("PhysicalBossAttack") || collision.gameObject.CompareTag("ProjectileBossAttack")
            || collision.gameObject.CompareTag("DisjointedBossAttack")) && !alreadyHit)
        {
            alreadyHit = true;
            
            if (isDamagable)
            {
                StartCoroutine(TakeDamage(playerScript.gameManager.bossManager.bossAttackDamage, invulTime));
                StartCoroutine(Knockback(false));
            }
            else
                StartCoroutine(Knockback(true));

            if (collision.gameObject.CompareTag("ProjectileBossAttack"))
                Destroy(collision.gameObject);
        }
    }

    public IEnumerator TakeDamage(float damage, float invulTime)
    {
        isDamagable = false;
        playerScript.currentHealth -= damage;

        playerScript.healthBar.SetCurrent(playerScript.currentHealth);

        if (playerScript.currentHealth <= 0)
            StartCoroutine(Perish());

        yield return new WaitForSeconds(invulTime);
        isDamagable = true;
    }
    
    private IEnumerator Perish()
    {
        deathCam.Priority = 11;
        //playerScript.ChangeAnimationState("Death");
        
        yield return new WaitForSeconds(0.5f); //A bit into the animation

        //deathText.SetActive(true); //bloody death text

        yield return new WaitForSeconds(1.5f); //hold
        
        deathTransition.SetActive(true); //fade

        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //change to load to hub
    }
}