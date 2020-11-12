using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ZeeMana : MonoBehaviour
{
    [SerializeField] private PlayerScript playerScript = null;
    [SerializeField] private GameObject blueTintPanel = null;
    [SerializeField] private CinemachineVirtualCamera slowCam = null;

    [SerializeField] private float timeScale = 0;
    [SerializeField] private float slowDownTime = 0;
    [SerializeField] private float slowDownCost = 0;

    [SerializeField] private float movementPenaltyMultiplier = 0;
    [SerializeField] private float movementPenaltyTime = 0;

    private bool canDoMagic = true;

    private void Start()
    {
        playerScript.currentMana = playerScript.maxMana;
        playerScript.manaBar.SetMax(playerScript.maxMana);
    }

    public IEnumerator SlowTime()
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
    }

    public IEnumerator DrinkPotion()
    {
        if (playerScript.currentMana < playerScript.maxMana && canDoMagic)
        {
            canDoMagic = false;
            playerScript.currentMana = playerScript.maxMana;
            playerScript.manaBar.SetCurrent(playerScript.maxMana);
            playerScript.speed *= movementPenaltyMultiplier;
            yield return new WaitForSeconds(movementPenaltyTime);
            canDoMagic = true;
            playerScript.speed /= movementPenaltyMultiplier;
        }
    }
}
