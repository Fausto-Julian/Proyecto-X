using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atack1Script : MonoBehaviour
{
    [SerializeField] private float timeLifeBullet = 1f;
    [SerializeField] private int damage;
    [SerializeField] private float speed = 5f;
    [SerializeField] private GameObject diamond;

    private float currentTime;

    private HealthController healthController;

    void Update()
    {
        float t = Time.deltaTime;

        currentTime += Time.deltaTime;
        if (currentTime >= timeLifeBullet)
        {
            Destroy(gameObject);
        }

        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            healthController = collision.GetComponent<HealthController>();

            if (healthController != null)
            {
                healthController.GetDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
