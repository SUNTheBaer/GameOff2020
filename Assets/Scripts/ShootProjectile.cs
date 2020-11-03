using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    Animator animator;
    Object projectileRef;

    // Start is called before the first frame update
    void Start()
    {
        projectileRef = Resources.Load("Basic_Projectile");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            animator.Play("Zee");
            GameObject projectile = (GameObject)Instantiate(projectileRef);
            projectile.transform.position = new Vector3(transform.position.x + .4f, transform.position.y + 2f, -1);
        }   
    }
}
