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
    [HideInInspector] public SpriteRenderer spriteRenderer;
    private Animator anim;

    //Player attributes
    public float speed;
    public bool isIdle;
    public bool isMovingRight;
    private string currentState;
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //Movement Attributes
        
        if (inputManager.move.x > 0)
            isMovingRight = true;
        else
            isMovingRight = false;

        if (inputManager.move.x == 0 && inputManager.move.y == 0)
            isIdle = true;
        else
            isIdle = false;

        //--------------------------------------------------------

    }
    public void ChangeAnimationState(string newState)
    {
        //Stops current anim from interrupting itself
        if (currentState == newState) return;

        //Play anim
        anim.Play(newState);

        //Reassign current state
        currentState = newState;
    }
}
