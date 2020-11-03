using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeeTimeSlow : MonoBehaviour
{
    public void SlowTime()
    {
        Time.timeScale = 0.5f;
        //Deplete mana meter
    }

    public void NormalTime()
    {
        Time.timeScale = 1.0f;
    }
}