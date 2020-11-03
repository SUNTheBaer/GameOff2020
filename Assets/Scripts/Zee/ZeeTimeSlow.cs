using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class ZeeTimeSlow : MonoBehaviour
{
    [SerializeField] private PlayerScript playerScript = null;
    [SerializeField] private float manaPerSecond = 0;
    public void SlowTime()
    {
        Time.timeScale = 0.5f;
        playerScript.currentMana -= manaPerSecond / 60;
    }

    public void NormalTime()
    {
        Time.timeScale = 1.0f;
    }
}