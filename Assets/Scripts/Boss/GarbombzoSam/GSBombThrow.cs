using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GSBombThrow : MonoBehaviour
{
    public GarbombzoSam garbombzoSam = null;
    [SerializeField] private GameObject garbomzoSamProjectile = null;
    public float attackDamage = 0;
    public float knockbackForce = 0;
    [HideInInspector] public Vector2 knockbackDirection = Vector2.zero;
    public float knockbackTime = 0;
    [SerializeField] private float waitForNextAttack = 0;

    public IEnumerator StartBombThrow()
    {
        garbombzoSam.anim.SetTrigger("throw");

        yield return new WaitForSeconds(0.5f);

        GameObject projectile = Instantiate(garbomzoSamProjectile, new Vector2(transform.position.x, transform.position.y - 1), Quaternion.identity);
        GSProjectile projectileScript = projectile.GetComponent<GSProjectile>();
        StartCoroutine(projectileScript.Shoot(new Vector2(garbombzoSam.player.transform.position.x - transform.position.x, 
            garbombzoSam.player.transform.position.y - transform.position.y + 1)));

        yield return new WaitForSeconds(waitForNextAttack);

        garbombzoSam.PickAttack();
    }
}
