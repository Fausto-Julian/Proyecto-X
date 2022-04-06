using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;

    public Action<float> OnChangeHealth;
    public Action OnDeath;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

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

        OnChangeHealth?.Invoke(currentHealth);
        if (currentHealth <= 0)
        {
            OnDeath?.Invoke();
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
}
