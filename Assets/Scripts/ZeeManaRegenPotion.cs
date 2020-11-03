using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeeManaRegenPotion : MonoBehaviour
{
    [SerializeField] private PlayerScript playerScript = null;
    [SerializeField] private float movementPenaltyMultiplier = 0;
    [SerializeField] private float movementPenaltyTime = 0;

    public IEnumerator DrinkPotion()
    {
        playerScript.currentMana = playerScript.maxMana;
        playerScript.speed *= movementPenaltyMultiplier;
        yield return new WaitForSeconds(movementPenaltyTime);
        playerScript.speed /= movementPenaltyMultiplier;
    }
}
