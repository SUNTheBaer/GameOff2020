using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //Scripts
    public InputManager inputManager;
    public PlayerMovement playerMovement;
    public PlayerCollision playerCollision;

    //Components
    [HideInInspector] public Rigidbody2D rb;
    private Animator anim;

    //Player attributes
    public float speed;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        
    }
}
