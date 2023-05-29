using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class HealthBar : FloatingInfo
{
    [SerializeField]
    private Health health = null;
    [SerializeField]
    private Slider slider;

    public Color low;
    public Color high;

    public void SetHealthBarColor(float currentHealth)
    {
        float temp = currentHealth / health.maxHealth;

        if (temp > 0.5f)
        {
            Debug.Log("high");
            slider.fillRect.GetComponentInChildren<Image>().color = high;
        }
        else
        {
            Debug.Log("low");
            slider.fillRect.GetComponentInChildren<Image>().color = low;
        }
    }

}
