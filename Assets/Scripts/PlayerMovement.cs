using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerScript playerScript;
    
    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        playerScript.rb.velocity = new Vector2(playerScript.inputManager.move.x, playerScript.inputManager.move.y) * playerScript.speed;
    }
}
