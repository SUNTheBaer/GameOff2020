using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private PlayerScript playerScript = null;

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
        if ((collision.gameObject.CompareTag("BossAttack") || collision.gameObject.CompareTag("ProjectileAttack")) && !alreadyHit)
        {
            alreadyHit = true;
            
            if (isDamagable)
            {
                StartCoroutine(TakeDamage(playerScript.gameManager.bossManager.bossAttackDamage, invulTime));
                StartCoroutine(Knockback(false));
            }
            else
                StartCoroutine(Knockback(true));

            if (collision.gameObject.CompareTag("ProjectileAttack"))
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
        //playerScript.ChangeAnimationState("Death");
        //playerScript.audioSrc.volume = 0.25f;
        //playerScript.audioSrc.Play();
        yield return new WaitForSeconds(1.5f);
        
        gameObject.SetActive(false); // Custom death

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
