﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerScript playerScript = null;
    [SerializeField] private SpriteRenderer spriteRenderer = null;

    [HideInInspector] public bool startWalking = false;
    [HideInInspector] public bool stopWalking = false;
    private bool canWalk = true;
    private bool playingClip = false;
    private bool isTryingToWalk = false;

    private float angle;

    [SerializeField] private float shieldKnockbackMultiplier = 0;
    private bool knockback;
    private Vector2 knockbackDirection;
    private float knockbackForce;
    private float knockbackTime;
    private float t;

    private void Update()
    {
        angle = Vector2.Angle(new Vector2(playerScript.inputManager.move.x, playerScript.inputManager.move.y), Vector2.right);

        //Walk Animations
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
        //------------------------------------------------------------------------------------------------------------------------
        //Flip Character
        if (angle <= 105)
            spriteRenderer.flipX = false;
        else
            spriteRenderer.flipX = true;
        //--------------------------------------------------------
        //Walking logic
        if (!knockback && !playerScript.zeePosture.brokenPosture)
            canWalk = true;
        else
            canWalk = false;

        if (startWalking)
            isTryingToWalk = true;
        if (stopWalking)
            isTryingToWalk = false;
        //------------------------------------------------------------------------------------------
        //Sound logic
        if (canWalk && startWalking) //Normal walk
        {
            startWalking = false;
            playingClip = true;
            playerScript.gameManager.soundManager.Play("Walk");
        }
        else if (stopWalking) //Stopped walk
        {
            stopWalking = false;
            playingClip = false;
            playerScript.gameManager.soundManager.Stop("Walk");
        }
        else if (!canWalk && startWalking) //Attempt walking while can't
        {
            StartCoroutine(WaitUntilCanWalk());
        }
        else if (playingClip && !canWalk) //Interrupted walk
        {
            playingClip = false;
            playerScript.gameManager.soundManager.Stop("Walk");
        }
        else if (!playingClip && canWalk && isTryingToWalk) //Normal walk into interrupt into still holding button
        {
            playingClip = true;
            playerScript.gameManager.soundManager.Play("Walk");
        }
        //-----------------------------------------------------------------------------------------------------------
    }

    private void FixedUpdate()
    {
        if(canWalk)
            playerScript.rb.velocity = new Vector2(playerScript.inputManager.move.x, playerScript.inputManager.move.y) * playerScript.speed;
        else if (knockback)
            {
                t += Time.deltaTime;
                playerScript.rb.velocity = knockbackDirection * knockbackForce;

                if (t > knockbackTime)
                    knockback = false;
            }
    }

    public void Knockback(Vector2 direction, float force, float time)
    {
        knockback = true;
        knockbackDirection = direction;
        if (playerScript.zeeShield.successfulBlock)
            knockbackForce = force * shieldKnockbackMultiplier;
        else
            knockbackForce = force;
        knockbackTime = time;
        t = 0;
    }

    private IEnumerator WaitUntilCanWalk()
    {
        yield return new WaitUntil(() => canWalk == true);
        playerScript.gameManager.soundManager.Play("Walk");
    }
}
