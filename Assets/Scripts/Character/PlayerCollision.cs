using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    public bool isDamagable = true;

    [SerializeField] private PlayerScript playerScript = null;
    [SerializeField] private GameManager gameManager = null;
    [SerializeField] private float invulTime = 0;
    [SerializeField] private float knockbackDuration = 0;
    private float t = 0;
    private Vector2 startPos;

    public bool knockback = false;


    private void Start() 
    {
        playerScript.currentHealth = playerScript.maxHealth;
        playerScript.healthBar.SetMax(playerScript.maxHealth);

        //playerScript.audioSrc = GetComponent<AudioSource>();
        //playerScript.audioSrc.volume = 0f;
    }

    private void Update()
    {
        if (knockback)
        {
            print(startPos);
            t += Time.deltaTime;

            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, startPos + new Vector2(0,-2), t / knockbackDuration);
        }
        if (t > 2)
        {
            knockback = false;
            t = 0;
        }
    }
    private void Knockback()
    {
        startPos = gameObject.transform.position;
        knockback = true;
    }
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("BossAttack") && isDamagable)
        {
            Knockback();
            StartCoroutine(TakeDamage(gameManager.bossManager.bossAttackDamage, invulTime));
        }
        if (collision.gameObject.CompareTag("ProjectileAttack"))
        {
            if(isDamagable)
                StartCoroutine(TakeDamage(gameManager.bossManager.bossAttackDamage, invulTime));
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
        // playerScript.ChangeAnimationState("Death");
      //  playerScript.audioSrc.volume = 0.25f;
       // playerScript.audioSrc.Play();
        yield return new WaitForSeconds(1.5f);
        
        gameObject.SetActive(false); // Custom death

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
