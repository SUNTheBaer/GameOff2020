using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private Inputs inputs;
    [SerializeField] private PlayerScript playerScript = null;
    [HideInInspector] public Vector2 move;
    [HideInInspector] public bool onTimeSlow;
    [HideInInspector] public bool onShoot;
    [HideInInspector] public bool onManaRegenPotion;
    [HideInInspector] public Vector2 aimPosition;
    
    private void Awake()
    {
        inputs = new Inputs();

        inputs.Player.Movement.performed += context => move = context.ReadValue<Vector2>();
        inputs.Player.Movement.canceled += context => move = Vector2.zero;

        inputs.Player.TimeSlow.started += context => onTimeSlow = true;
        inputs.Player.TimeSlow.canceled += context => onTimeSlow = false;

        inputs.Player.Shoot.started += context => onShoot = true;
        inputs.Player.Shoot.canceled += context => onShoot = false;

        inputs.Player.ManaRegenPotion.started += context => StartCoroutine(playerScript.zeeManaRegenPotion.DrinkPotion());

        inputs.Player.Aim.started += context => aimPosition = context.ReadValue<Vector2>();
        inputs.Player.Aim.performed += context => aimPosition = context.ReadValue<Vector2>();
        inputs.Player.Aim.canceled += context => aimPosition = Vector2.zero;
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
