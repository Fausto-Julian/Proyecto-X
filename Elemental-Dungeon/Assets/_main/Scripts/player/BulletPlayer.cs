using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    [SerializeField] private float timeLifeBullet = 1f;
    [SerializeField] private int damageDefault;
    [SerializeField] private float speed = 5f;

    [SerializeField] private bool fireBullet;
    [SerializeField] private bool waterBullet;
    [SerializeField] private bool rockBullet;
    [SerializeField] private bool windBullet;
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
            DamageManager damageManager = collision.GetComponent<DamageManager>();

            if (healthController != null)
            {
                if (fireBullet)
                {
                    Ability fire = SkillTreeManager.inst.GetAbilityFire();
                    var damage = damageDefault + fire.levelDamage;
                    healthController.SetDamage(damage);
                    if (damageManager != null)
                    {
                        damageManager.ActiveSetFire(1.2f, 6f);
                    }
                    Destroy(gameObject);
                }

                if (waterBullet)
                {
                    Ability water = SkillTreeManager.inst.GetAbilityWater();
                    var damage = damageDefault + water.levelDamage;
                    healthController.SetDamage(damage);
                    if (damageManager != null)
                    {
                        
                    }
                    Destroy(gameObject);
                }

                if (rockBullet)
                {
                    Ability rock = SkillTreeManager.inst.GetAbilityRock();
                    var damage = damageDefault + rock.levelDamage;
                    healthController.SetDamage(damage);
                    if (damageManager != null)
                    {
                        
                    }
                    Destroy(gameObject);
                }

                if (windBullet)
                {
                    Ability wind = SkillTreeManager.inst.GetAbilityWind();
                    var damage = damageDefault + wind.levelDamage;
                    healthController.SetDamage(damage);
                    if (damageManager != null)
                    {
                        
                    }
                    Destroy(gameObject);
                }

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
