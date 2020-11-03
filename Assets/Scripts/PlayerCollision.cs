using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private PlayerScript playerScript = null;

    private void Start() 
    {
        playerScript.currentHealth = playerScript.maxHealth;
        playerScript.healthBar.SetMaxHealth(playerScript.maxHealth);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("DoesDamage"))
            TakeDamage(20);
    }

    private void TakeDamage(int damage)
    {
        playerScript.currentHealth -= damage;

        playerScript.healthBar.SetHealth(playerScript.currentHealth);

        if (playerScript.currentHealth <= 0)
            Perish();
    }
    
    // PERISH FUNCTION
    private void Perish()
    {
        // playerScript.ChangeAnimationState("Death");
        gameObject.SetActive(false); // Custom death
    }
}
