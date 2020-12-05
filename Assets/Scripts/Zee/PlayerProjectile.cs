using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2d;
    public Vector2 direction;
    public Quaternion rotation;
    public float selfDestructTime;
    private bool canBeDestroyedByWall = false;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = direction;
        gameObject.transform.rotation = rotation;
        Invoke("DestroySelf", selfDestructTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Boss") || other.gameObject.CompareTag("PhysicalBossAttack"))
            StartCoroutine(DestroySelfCoroutine());
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            canBeDestroyedByWall = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall") && canBeDestroyedByWall)
            StartCoroutine(DestroySelfCoroutine());
    }

    private IEnumerator DestroySelfCoroutine()
    {
        yield return new WaitForEndOfFrame();
        DestroySelf();
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
