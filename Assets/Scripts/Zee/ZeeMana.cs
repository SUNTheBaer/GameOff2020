using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ZeeMana : MonoBehaviour
{
    [SerializeField] private PlayerScript playerScript = null;

    [SerializeField] private float duration = 0;
    private float t = 0;
    private float mana = 0;
    //[SerializeField] private float timeScale = 0;
    //[SerializeField] private float slowDownTime = 0;
    //[SerializeField] private float slowDownCost = 0;

    [SerializeField] private float movementPenaltyMultiplier = 0;
    [SerializeField] private float movementPenaltyTime = 0;

    public bool canDoMagic = true;
    private bool drinkLerp = false;

    private void Start()
    {
        playerScript.currentMana = playerScript.maxMana;
        playerScript.manaBar.SetMax(playerScript.maxMana);
    }

    /*public IEnumerator SlowTime()
    {
        if (playerScript.currentMana > 0 && canDoMagic)
        {
            canDoMagic = false;
            Time.timeScale = timeScale;
            blueTintPanel.SetActive(true);
            slowCam.Priority = 20;
            playerScript.currentMana -= slowDownCost;
            playerScript.manaBar.SetCurrent(playerScript.currentMana);
            yield return new WaitForSeconds(slowDownTime);
            canDoMagic = true;
            Time.timeScale = 1.0f;
            blueTintPanel.SetActive(false);
            slowCam.Priority = 0;
        }

        if (playerScript.currentMana < 0)
            playerScript.currentMana = 0;
    }*/
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
            playerScript.currentMana = mana;
            //playerScript.manaBar.SetCurrent(playerScript.maxMana);
            playerScript.speed *= movementPenaltyMultiplier;
            yield return new WaitForSeconds(movementPenaltyTime);
            canDoMagic = true;
            playerScript.speed /= movementPenaltyMultiplier;
        }
    }
}
