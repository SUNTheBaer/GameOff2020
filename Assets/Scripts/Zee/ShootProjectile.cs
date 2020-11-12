using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    Object projectileRef;
    [SerializeField] private PlayerScript playerScript = null;
    [SerializeField] private PlayerProjectile projectileScript = null;
    [SerializeField] private float cooldownTime = 0.05f;
    [SerializeField] private float speed = 0;
    private bool isShooting = false;

    void Start()
    {
        projectileRef = Resources.Load("Basic_Projectile");
    }

    void Update()
    {
        if (playerScript.inputManager.onShoot && !isShooting)
            StartCoroutine(OnShoot());
    }

    private IEnumerator OnShoot()
    {
        isShooting = true;
        projectileScript.direction = Vector3.Normalize(playerScript.aimingScript.direction) * speed;
        projectileScript.rotation = Quaternion.AngleAxis(playerScript.aimingScript.angle, Vector3.forward); /*new Quaternion (0, 0, playerScript.aimingScript.angle, 1);*/
        GameObject projectile = (GameObject)Instantiate(projectileRef);
        projectile.transform.position = gameObject.transform.position;
        yield return new WaitForSeconds(cooldownTime);
        isShooting = false;
    }
}
