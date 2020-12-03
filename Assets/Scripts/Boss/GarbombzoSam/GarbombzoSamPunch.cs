using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbombzoSamPunch : MonoBehaviour
{
    [SerializeField] private GarbombzoSam garbombzoSam = null;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerAttack"))
            garbombzoSam.playerHitBoss = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerAttack"))
            garbombzoSam.playerHitBoss = false;
    }
}
