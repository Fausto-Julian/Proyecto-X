using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeScript : MonoBehaviour
{
    [SerializeField] private int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            HealthController healthController = collision.GetComponent<HealthController>();

            if (healthController != null)
            {
                healthController.SetDamage(damage);
            }
        }
        
        if (collision.gameObject.CompareTag("EnemyNoTarget"))
        {
            HealthController healthController = collision.GetComponent<HealthController>();

            if (healthController != null)
            {
                healthController.SetDamage(damage);
            }
        }
    }
}
