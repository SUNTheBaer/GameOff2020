using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    Object projectileRef;
    public InputManager inputManager;


    // Start is called before the first frame update
    void Start()
    {
        projectileRef = Resources.Load("Basic_Projectile");
    }

    // Update is called once per frame
    void Update()
    {
        if(inputManager.onShoot)
        {
            GameObject projectile = (GameObject)Instantiate(projectileRef);
            projectile.transform.position = new Vector3(transform.position.x + .2f, transform.position.y + .1f, -1);
        }
    }
}
