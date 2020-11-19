using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private PlayerScript playerScript = null;
    [SerializeField] private GameManager gameManager = null;
    [SerializeField] private float invulTime = 0;
    public bool isDamagable = true;
    private bool insideAttack = false;

    private void Start() 
    {
        playerScript.currentHealth = playerScript.maxHealth;
        playerScript.healthBar.SetMax(playerScript.maxHealth);

        playerScript.audioSrc = GetComponent<AudioSource>();
        playerScript.audioSrc.volume = 0f;
    }

    private void Update()
    {
        print(insideAttack);
        if(insideAttack && isDamagable)
            StartCoroutine(TakeDamage(gameManager.bossManager.bossAttackDamage, invulTime));
    }

    private void LateUpdate()
    {
        //Limits colliders but necessary
        if (insideAttack)
            insideAttack = false;
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("BossAttack"))
            insideAttack = true; 
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
        // playerScript.ChangeAnimationState("Death");
        playerScript.audioSrc.volume = 0.25f;
        playerScript.audioSrc.Play();
        yield return new WaitForSeconds(1.5f);
        
        gameObject.SetActive(false); // Custom death

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
