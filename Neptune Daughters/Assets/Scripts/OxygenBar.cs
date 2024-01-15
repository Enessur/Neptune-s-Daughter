using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxOxygen(int Oxygen)
    {
        slider.maxValue = Oxygen;
        slider.value = Oxygen;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetOxygen(int Oxygen)
    {
        slider.value = Oxygen;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}