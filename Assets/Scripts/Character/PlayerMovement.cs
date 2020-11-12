using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerScript playerScript = null;
    [SerializeField] private SpriteRenderer spriteRenderer = null;

    private void Update()
    {
        if (playerScript.inputManager.move.x == 0 && playerScript.inputManager.move.y == 0)
        {
            playerScript.ChangeAnimationState("Idle");
            playerScript.isIdle = true;
        }
            
        else
            playerScript.isIdle = false;

        if(playerScript.inputManager.move.y < 0 && playerScript.inputManager.move.x == 0)
            playerScript.ChangeAnimationState("down");
        if (playerScript.inputManager.move.y > 0 && playerScript.inputManager.move.x == 0)
            playerScript.ChangeAnimationState("Up");
        if (playerScript.inputManager.move.x != 0 && (playerScript.inputManager.move.y == 0 || playerScript.inputManager.move.y <0))
            playerScript.ChangeAnimationState("Side");
        if (playerScript.inputManager.move.x != 0 && (playerScript.inputManager.move.y > 0))
            playerScript.ChangeAnimationState("Up Side");


        //Flip Character

        if (playerScript.inputManager.move.x > 0)
            spriteRenderer.flipX = false;
         else if (playerScript.inputManager.move.x < 0)
            spriteRenderer.flipX = true;
        
        //--------------------------------------------------------
    }

    private void FixedUpdate()
    {
        //Dividing by time scale to make it seem like character movement speed hasn't changed
        playerScript.rb.velocity = new Vector2(playerScript.inputManager.move.x, playerScript.inputManager.move.y) * playerScript.speed / Time.timeScale;
    }
}
