using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    [SerializeField] private float timeLifeBullet = 1f;
    [SerializeField] private int damageDefault;
    [SerializeField] private float speed = 5f;
    [SerializeField] private GameObject diamond;

    private HealthController healthController;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Destroy(gameObject, timeLifeBullet);
    }

    private void FixedUpdate()
    {
        var desiredSpeed = transform.right * speed;
        var difVel = new Vector2(desiredSpeed.x - rb.velocity.x, desiredSpeed.y - rb.velocity.y);
        var force = rb.mass * difVel;

        rb.AddForce(force, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            healthController = collision.GetComponent<HealthController>();

            if (healthController != null)
            {
                var damage = damageDefault + SkillTreeManager.inst.LevelFire();
                Debug.Log(SkillTreeManager.inst.LevelFire());
                Debug.Log(damage);
                healthController.GetDamage(damage);
                Destroy(gameObject);
            }
        }

        if (collision.gameObject.CompareTag("Wall")) 
            Destroy(gameObject);
    }

    public int DamageDefaultFire()
    {
        return damageDefault;
    }
}
