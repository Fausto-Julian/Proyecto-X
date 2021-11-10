using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;

    public Action<float> changeHealth;
    public Action isDeath;

    private bool isAlive = true;
    public void SetHealth(float health)
    {
        if (health > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth = health;
        }
    }

    public void SetDamage(float damage)
    {
        currentHealth -= damage;

        changeHealth?.Invoke(currentHealth);
        if (currentHealth <= 0 && isAlive)
        {
            isAlive = false;
            isDeath?.Invoke();
        }
    }

    public float GetCurrentHealth()
    {
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        return currentHealth;
    }

    public float GetDefaultHealth()
    {
        return maxHealth;
    }


    public void SetDefaultHealth()
    {
        currentHealth = maxHealth;
    }

    public bool IsAlive()
    {
        return currentHealth > 0;
    }
}
