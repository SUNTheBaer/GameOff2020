using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GSProjectile : MonoBehaviour
{
    public Rigidbody2D rb = null;
    private GameObject garbombzoSam = null;
    private GarbombzoSam garbombzoSamScript = null;
    private GSBombThrow garbombzoSamBombThrow = null;
    [SerializeField] private float destroyTime = 0;
    [SerializeField] private float homingDuration = 0;
    
    private float distanceBetweenPlayerAndBoss = 0;
    private float distanceBetweenProjectileAndBoss = 0;
    
    private void Start()
    {
        garbombzoSam = GameObject.FindGameObjectWithTag("Boss");
        garbombzoSamBombThrow = garbombzoSam.GetComponentInChildren<GSBombThrow>();
        garbombzoSamScript = garbombzoSam.GetComponent<GarbombzoSam>();
    }

    //Homing properties
    private void FixedUpdate()
    {
        distanceBetweenPlayerAndBoss = Mathf.Abs(Vector2.Distance(garbombzoSam.transform.position, garbombzoSamScript.player.transform.position));
        distanceBetweenProjectileAndBoss = Mathf.Abs(Vector2.Distance(garbombzoSam.transform.position, transform.position));

        if (distanceBetweenProjectileAndBoss < distanceBetweenPlayerAndBoss)
        {
            rb.velocity = Vector2.Lerp(rb.velocity, garbombzoSamScript.player.transform.position - transform.position, homingDuration * Time.deltaTime);
            rb.velocity = rb.velocity.normalized * 5;
            garbombzoSamBombThrow.knockbackDirection = rb.velocity;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Wall")
            Destroy(gameObject); 

        if (other.gameObject.CompareTag("Player"))
        {
            garbombzoSamBombThrow.garbombzoSam.playerScript.playerMovement.Knockback(garbombzoSamBombThrow.knockbackDirection,
                garbombzoSamBombThrow.knockbackForce, garbombzoSamBombThrow.knockbackTime);
            StartCoroutine(garbombzoSamBombThrow.garbombzoSam.playerScript.playerCollision.TakeDamage(garbombzoSamBombThrow.attackDamage));
            Destroy(gameObject);
        }
    }

    public IEnumerator Shoot(Vector2 velocity)
    {
        rb.velocity = velocity;
        yield return new WaitForSeconds(destroyTime);
        //Check to see if object has already been destroyed
        try { Destroy(gameObject); }
        catch(MissingReferenceException) {}
    }
}
