using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerScript playerScript = null;
    [SerializeField] private SpriteRenderer spriteRenderer = null;
    private float angle;

    private void Update()
    {
        angle = Vector2.Angle(new Vector2(playerScript.inputManager.move.x, playerScript.inputManager.move.y), Vector2.right);

        if (playerScript.inputManager.move.x == 0 && playerScript.inputManager.move.y == 0)
            playerScript.ChangeAnimationState("Idle");

        else if(playerScript.inputManager.move.y < 0 && angle <= 105 && angle >= 75)
            playerScript.ChangeAnimationState("down");
        else if (playerScript.inputManager.move.y > 0 && angle <= 105 && angle >= 75)
            playerScript.ChangeAnimationState("Up");
        else if (angle >= 165 || angle <= 15 || ((angle < 75 || angle > 105) && playerScript.inputManager.move.y < 0))
            playerScript.ChangeAnimationState("Side");
        else if (playerScript.inputManager.move.y > 0 && ((angle < 75 && angle > 15) || (angle < 165 && angle > 105)))
            playerScript.ChangeAnimationState("Up Side");


        //Flip Character

        if (angle <= 105)
            spriteRenderer.flipX = false;
        else
            spriteRenderer.flipX = true;
        
        //--------------------------------------------------------
    }

    private void FixedUpdate()
    {
        //Dividing by time scale to make it seem like character movement speed hasn't changed
        playerScript.rb.velocity = new Vector2(playerScript.inputManager.move.x, playerScript.inputManager.move.y) * playerScript.speed / Time.timeScale;
    }
}
