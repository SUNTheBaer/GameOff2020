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
    [HideInInspector] public bool onManaRegenPotion;
    
    private void Awake()
    {
        inputs = new Inputs();

        inputs.Player.Movement.performed += context => move = context.ReadValue<Vector2>();
        inputs.Player.Movement.canceled += context => move = Vector2.zero;

        inputs.Player.TimeSlow.started += context => onTimeSlow = true;
        inputs.Player.TimeSlow.canceled += context => onTimeSlow = false;

        inputs.Player.ManaRegenPotion.started += context => StartCoroutine(playerScript.zeeManaRegenPotion.DrinkPotion());
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
