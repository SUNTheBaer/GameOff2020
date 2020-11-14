﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private Inputs inputs;
    [SerializeField] private PlayerScript playerScript = null;
    [HideInInspector] public Vector2 move;
    [HideInInspector] public bool onShoot;
    [HideInInspector] public Vector2 mouseAimPosition;
    [HideInInspector] public Vector2 padAimPosition;
    [HideInInspector] public bool onController = false;
    
    private void Awake()
    {
        inputs = new Inputs();

        inputs.Player.Movement.performed += context => move = context.ReadValue<Vector2>();
        inputs.Player.Movement.canceled += context => move = Vector2.zero;

        inputs.Player.Shield.started += context => StartShield();
        inputs.Player.Shield.canceled += context => StopShield();

        inputs.Player.Shoot.started += context => onShoot = true;
        inputs.Player.Shoot.canceled += context => onShoot = false;

        inputs.Player.ManaRegenPotion.started += context => StartCoroutine(playerScript.zeeMana.DrinkPotion());

        inputs.Player.MouseAim.performed += context => mouseAimPosition = context.ReadValue<Vector2>();

        inputs.Player.PadAim.performed += context => padAimPosition = context.ReadValue<Vector2>();
        inputs.Player.PadAim.performed += context => onController = true;
    }

    private void StartShield()
    {
        playerScript.zeeShield.coroutine = playerScript.zeeShield.StartShieldCoroutine();
        //print
        StartCoroutine(playerScript.zeeShield.coroutine);
    }

    private void StopShield()
    {
        StopCoroutine(playerScript.zeeShield.coroutine);
        playerScript.playerCollision.damagable = true;
        playerScript.zeeShield.blockPhase = 0;
    }

    private void OnEnable()
    {
        inputs.Player.Enable();
    }

    private void OnDisable()
    {
        inputs.Player.Disable();
    }
}
