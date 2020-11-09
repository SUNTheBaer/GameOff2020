using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeeMana : MonoBehaviour
{
    [SerializeField] private PlayerScript playerScript = null;
    [SerializeField] private float movementPenaltyMultiplier = 0;
    [SerializeField] private float movementPenaltyTime = 0;
    [SerializeField] private float manaPerSecond = 0;
    [SerializeField] private float timeMultiplier = 0;

    private void Start()
    {
        playerScript.currentMana = playerScript.maxMana;
        playerScript.manaBar.SetMax(playerScript.maxMana);
    }
    public void SlowTime()
    {
        Time.timeScale = timeMultiplier;
        playerScript.currentMana -= manaPerSecond / 60;
        playerScript.manaBar.SetCurrent(playerScript.currentMana);
    }

    public void NormalTime()
    {
        Time.timeScale = 1.0f;
    }

    public IEnumerator DrinkPotion()
    {
        playerScript.currentMana = playerScript.maxMana;
        playerScript.manaBar.SetCurrent(playerScript.maxMana);
        playerScript.speed *= movementPenaltyMultiplier;
        yield return new WaitForSeconds(movementPenaltyTime);
        playerScript.speed /= movementPenaltyMultiplier;
    }
}
