using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private PlayerScript playerScript = null;
    [SerializeField] private GameManager gameManager = null;
    [SerializeField] float iFrames = 0;
    bool damagable = true;
    bool insideAttack = false;

    private void Start() 
    {
        playerScript.currentHealth = playerScript.maxHealth;
        playerScript.healthBar.SetMax(playerScript.maxHealth);
    }
    private void Update()
    {
        if(insideAttack && damagable)
            StartCoroutine(TakeDamage(gameManager.bossManager.bossAttackDamage));
    }
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("BossAttack"))
            insideAttack = true; 
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BossAttack"))
            insideAttack = false;
    }

    public IEnumerator TakeDamage(float damage)
    {
        damagable = false;
        playerScript.currentHealth -= damage;

        playerScript.healthBar.SetCurrent(playerScript.currentHealth);

        if (playerScript.currentHealth <= 0)
            Perish();

        yield return new WaitForSeconds(iFrames);
        damagable = true;
    }
    
    private void Perish()
    {
        // playerScript.ChangeAnimationState("Death");
        gameObject.SetActive(false); // Custom death
    }
}
