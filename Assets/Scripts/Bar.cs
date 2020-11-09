using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    public Slider slider;

    public void SetMax(float max)
    {
        slider.maxValue = max;
        slider.value = max;
    }

    public void SetCurrent(float current)
    {
        slider.value = current;
    }
}
