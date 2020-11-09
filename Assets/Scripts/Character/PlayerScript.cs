using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerScript : MonoBehaviour
{
    [Header("Scripts")]
    public InputManager inputManager;
    public PlayerMovement playerMovement;
    public PlayerCollision playerCollision;
    [SerializeField] private ZeeTimeSlow zeeTimeSlow = null;
    public ZeeManaRegenPotion zeeManaRegenPotion = null;

    [Header("Components")]
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    private Animator anim;
    [SerializeField] GameObject blueTintPanel;
    [SerializeField] CinemachineVirtualCamera slowCam;

    [Header("Player Attributes")]
    public float speed;
    public float currentMana;
    [HideInInspector] public bool isIdle;
    [HideInInspector] public bool isMovingRight;
    private string currentState;
    public float maxHealth = 100;
    public float currentHealth;
    public Bar healthBar;
    public Bar manaBar;
    public float maxMana = 0;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        currentMana = maxMana;
    }

    private void Update()
    {
        if (inputManager.onTimeSlow && currentMana > 0)
        {
            slowCam.Priority = 20;
            blueTintPanel.SetActive(true);
            zeeManaRegenPotion.SlowTime();
        }
        else
        {
            slowCam.Priority = 0;
            blueTintPanel.SetActive(false);
            zeeManaRegenPotion.NormalTime();
        }    
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
