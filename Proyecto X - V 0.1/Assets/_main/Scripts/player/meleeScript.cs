using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeScript : MonoBehaviour
{
    [SerializeField] private int damage;

    private HealthController healthController;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            healthController = collision.GetComponent<HealthController>();

            if (healthController != null)
            {
                healthController.GetDamage(damage);
            }
        }
    }
}
