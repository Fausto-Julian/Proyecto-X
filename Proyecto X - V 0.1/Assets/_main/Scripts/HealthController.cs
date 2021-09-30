using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private int maxHealth;

    [SerializeField]private int currentHealth;


    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void GetDamage(int damage)
    {
        currentHealth -= damage;
    }

    public void GetHeal(int heal)
    {
        currentHealth += heal;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    public bool IsAlive()
    {
        return currentHealth > 0;
    }
}
