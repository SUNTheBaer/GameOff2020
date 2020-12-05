using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GSTrackingBomb : MonoBehaviour
{
    [SerializeField] private GarbombzoSam garbombzoSam = null;
    [SerializeField] private GameObject trackingBombIndicator = null;
    [SerializeField] private float attackDamage = 0;
    [SerializeField] private float knockbackForce = 0;
    private Vector2 knockbackDirection = Vector2.zero;
    [SerializeField] private float knockbackTime = 0;
    [SerializeField] private float waitForNextAttack = 0;
    [SerializeField] private float timeToStopTrack = 0;
    private bool trackingBombActive = false;

    private void Update()
    {
        if (trackingBombActive)
            trackingBombIndicator.transform.position = garbombzoSam.player.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            garbombzoSam.playerScript.playerMovement.Knockback(knockbackDirection, knockbackForce, knockbackTime);
            StartCoroutine(garbombzoSam.playerScript.playerCollision.TakeDamage(attackDamage));
        }
    }

    public IEnumerator StartTrackingBomb()
    {
        //shadow tracking player
        trackingBombActive = true;
        trackingBombIndicator.SetActive(true);

        yield return new WaitForSeconds(timeToStopTrack);

        // Biggest shadow circle - Pick player's current position and wait ie. 0.25 seconds before bomb lands
        trackingBombActive = false;

        yield return new WaitForSeconds(1.4f - timeToStopTrack);

        if ((Vector2)(garbombzoSam.player.transform.position - trackingBombIndicator.transform.position) != Vector2.zero)
            knockbackDirection = (Vector2)(garbombzoSam.player.transform.position - trackingBombIndicator.transform.position);
        else
            knockbackDirection = Vector2.down;

        yield return new WaitForSeconds(0.833f);

        trackingBombIndicator.SetActive(false);

        yield return new WaitForSeconds(waitForNextAttack);

        garbombzoSam.PickAttack();
    }
}
