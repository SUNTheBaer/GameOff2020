using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ZeeMana : MonoBehaviour
{
    [SerializeField] private PlayerScript playerScript = null;

    [SerializeField] private float duration = 0;
    private float t = 0;

    [SerializeField] private float movementPenaltyMultiplier = 0;
    [SerializeField] private float movementPenaltyTime = 0;

    public bool canDoMagic = true;
    private bool drinkLerp = false;

    private void Start()
    {
        playerScript.currentMana = playerScript.maxMana;
        playerScript.manaBar.SetMax(playerScript.maxMana);
    }

    private void Update()
    {
        //it's all to lerp and it's all ugly and bad but idk it wasn't working and now it kinda is
        if (playerScript.currentMana >= playerScript.maxMana - 5)
        {
            playerScript.manaBar.SetCurrent(playerScript.maxMana);
            t = 0;
            drinkLerp = false;
        }
        else if (drinkLerp)
            {
                t = Time.deltaTime;
                playerScript.currentMana = Mathf.Lerp(playerScript.currentMana, playerScript.maxMana, t/duration);
                playerScript.manaBar.SetCurrent(playerScript.currentMana);
            }
    }

    public IEnumerator DrinkPotion()
    {
        if (playerScript.currentMana < playerScript.maxMana && canDoMagic)
        {
            drinkLerp = true;
            canDoMagic = false;
            //playerScript.manaBar.SetCurrent(playerScript.maxMana);
            playerScript.speed *= movementPenaltyMultiplier;
            yield return new WaitForSeconds(movementPenaltyTime);
            canDoMagic = true;
            playerScript.speed /= movementPenaltyMultiplier;
        }
    }
}
