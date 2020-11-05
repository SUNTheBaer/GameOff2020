using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    Object projectileRef;
    public InputManager inputManager;

    [SerializeField] private float cooldownTime = 0.05f;
    private float timeStamp;

    // Start is called before the first frame update
    void Start()
    {
        projectileRef = Resources.Load("Basic_Projectile");
    }

    // Update is called once per frame
    void Update()
    {
        if (inputManager.onShoot && Time.time > timeStamp)
        {
            GameObject projectile = (GameObject)Instantiate(projectileRef);
            // projectile.transform.position = new Vector3(transform.position.x + .2f, transform.position.y + 0, -1);
            projectile.transform.position = gameObject.transform.position;
            timeStamp = Time.time + cooldownTime;
        }
    }
}
