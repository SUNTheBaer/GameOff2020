using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbombzoSamProjectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private float destroyTime = 0;
    [SerializeField] private float homingDuration = 0;

    private GameObject gameManager;
    private GameManager gameManagerScript;
    
    private float distanceBetweenPlayerAndBoss;
    private float distanceBetweenProjectileAndBoss;
    
    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    private void FixedUpdate()
    {
        distanceBetweenPlayerAndBoss = Mathf.Abs(Vector2.Distance(gameManagerScript.bossManager.bossPosition, gameManagerScript.bossManager.playerPosition));
        distanceBetweenProjectileAndBoss = Mathf.Abs(Vector2.Distance(gameManagerScript.bossManager.bossPosition, transform.position));

        if (distanceBetweenProjectileAndBoss < distanceBetweenPlayerAndBoss)
        {
            rb.velocity = Vector2.Lerp(rb.velocity, gameManagerScript.bossManager.playerPosition - (Vector2)transform.position, homingDuration * Time.deltaTime);
            rb.velocity = rb.velocity.normalized * 5;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Wall")
            Destroy(gameObject);
    }

    public IEnumerator Shoot(Vector2 velocity)
    {
        rb.velocity = velocity;
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
}
