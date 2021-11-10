using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    [SerializeField] private float timeLifeBullet = 1f;
    [SerializeField] private int damageDefault;
    [SerializeField] private float speed = 5f;

    private Rigidbody2D _body;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Destroy(gameObject, timeLifeBullet);
    }

    private void FixedUpdate()
    {
        var desiredSpeed = transform.right * speed;
        var difVel = new Vector2(desiredSpeed.x - _body.velocity.x, desiredSpeed.y - _body.velocity.y);
        var force = _body.mass * difVel;

        _body.AddForce(force, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            HealthController healthController = collision.GetComponent<HealthController>();

            if (healthController != null)
            {
                var damage = damageDefault + SkillTreeManager.inst.LevelFire();
                healthController.SetDamage(damage);
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
