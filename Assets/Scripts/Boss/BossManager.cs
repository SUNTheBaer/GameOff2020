using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [SerializeField] private PlayerScript playerScript = null;
    [SerializeField] private ScriptableBoss boss = null;

    public void DealDamage(int damage)
    {
        playerScript.currentHealth -= damage; //boss attack damage can be updated via unique boss script
    }
}
