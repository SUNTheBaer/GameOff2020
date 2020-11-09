﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerScript : MonoBehaviour
{
    [Header("Scripts")]
    public InputManager inputManager;
    public PlayerMovement playerMovement;
    public PlayerCollision playerCollision;
    public ZeeMana zeeMana = null;
    public Aiming aimingScript = null;

    [Header("Components")]
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    private Animator anim;
    public GameObject aimIndicator;
    [SerializeField] private GameObject blueTintPanel = null;
    [SerializeField] private CinemachineVirtualCamera slowCam = null;

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
            zeeMana.SlowTime();
        }
        else
        {
            slowCam.Priority = 0;
            blueTintPanel.SetActive(false);
            zeeMana.NormalTime();
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
