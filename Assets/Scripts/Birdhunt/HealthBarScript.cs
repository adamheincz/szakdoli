using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : FloatingInfo
{
    [SerializeField]
    private Health health = null;
    [SerializeField]
    private Slider slider;

    //offset

    public Color low;
    public Color high;


    public void SetHealth(int health, int maxHealth)
    {
        //slider.gameObject.SetActive(true);
        slider.value = health;
        slider.maxValue = maxHealth;

        float temp = ((float)health) / ((float)maxHealth);

        if ( temp > 0.5f)
        {
            Debug.Log("health/maxHealth: " + temp);
            slider.fillRect.GetComponentInChildren<Image>().color = high;
        } else
        {
            Debug.Log("else health/maxHealth: " + temp);
            slider.fillRect.GetComponentInChildren<Image>().color = low;
        }
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void LoseHealth(int damage, int currentHealth, int maxHealth)
    {
        SetHealth(currentHealth - damage, maxHealth);
    }
}
