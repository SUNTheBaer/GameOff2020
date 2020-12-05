using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerScript : MonoBehaviour
{
    [Header("Scripts")]
    public InputManager inputManager;
    public DialogueManager dialogueManager;
    public PlayerMovement playerMovement;
    public PlayerCollision playerCollision;
    public GameManager gameManager;
    public ZeeMana zeeMana;
    public ZeeShield zeeShield;
    public ZeePosture zeePosture;
    public Aiming aimingScript;

    [Header("Components")]
    public Rigidbody2D rb;
    [SerializeField] private Animator anim = null;
    
    [Header("Player Attributes")]
    public float speed;
    public float damage;
    [HideInInspector] public float currentHealth;
    public float maxHealth = 100;
    public Bar healthBar;
    [HideInInspector] public float currentMana;
    public float maxMana = 0;
    public Bar manaBar;
    private string currentState;
    public GameObject aimIndicator;

    [Header("Angle")]
    [HideInInspector] public float angle;
    private float deltaX;
    private float deltaY;
    private GameObject currentBoss;

    private void Start()
    {
        currentBoss = GameObject.FindGameObjectWithTag("Boss");
    }

    private void Update()
    {
        if (currentMana > maxMana)
            currentMana = maxMana;

        deltaX = currentBoss.transform.position.x - transform.position.x;
        deltaY = currentBoss.transform.position.y - transform.position.y;

        angle = Mathf.Atan2(deltaY, deltaX) * Mathf.Rad2Deg - 90;
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
