using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFuegoControlller : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float speed;
    [SerializeField] private float radius;
    [SerializeField] private float radiusExp;
    [SerializeField] private float damage;
    [SerializeField] private float damageForSecond;
    [SerializeField] private float timeout;
    [SerializeField] private float damageExp;

    [Space]
    [Header("Instaciar")]
    [SerializeField] private GameObject diamond;

    
    private Rigidbody2D _body;
    private Animator _anim;
    private HealthController _healthController;

    private Transform _playerTransform;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _healthController = GetComponent<HealthController>();
        _anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        _healthController.SetDefaultHealth();
        _healthController.OnDeath += IsDeathHandler;
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void IsDeathHandler()
    {
        Instantiate(diamond, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (_playerTransform != null) { 
            Vector2 targetMove = (_playerTransform.position - transform.position).normalized;
            var distance = Vector2.Distance(transform.position, _playerTransform.position);

            if (distance <= radius)
            {
                var intensity = (distance / radius) * speed;
                _body.velocity = targetMove * Mathf.Clamp(intensity, 2f, speed);
            }
        }
        else
        {
            _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthController layerHealthController = collision.gameObject.GetComponent<HealthController>();
            DamageManager playerDamageManager = GameObject.FindGameObjectWithTag("Player").GetComponent<DamageManager>();

            if (layerHealthController != null)
            {
                layerHealthController.SetDamage(damage);
            }
            if (playerDamageManager != null)
            {
                playerDamageManager.ActiveSetFire(damageForSecond, timeout);
            }
        }
    }
}
