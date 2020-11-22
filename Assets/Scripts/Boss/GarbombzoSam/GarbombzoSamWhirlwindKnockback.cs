using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbombzoSamWhirlwindKnockback : MonoBehaviour
{
    [SerializeField] private GameManager gameManager = null;
    [SerializeField] private GameObject player = null;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.bossManager.knockbackDirection = (Vector2)(player.transform.position - transform.position);
        }
    }
}
