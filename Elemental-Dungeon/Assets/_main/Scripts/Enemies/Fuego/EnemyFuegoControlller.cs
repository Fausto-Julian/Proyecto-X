using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFuegoControlller : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float speed;
    [SerializeField] private float radius;
    [SerializeField] private int damage;

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
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        _healthController.SetDefaultHealth();
        _healthController.OnDeath += IsDeathHandler;
    }

    private void IsDeathHandler()
    {
        _healthController.OnDeath -= IsDeathHandler;
        Instantiate(diamond, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2 targetMove = (_playerTransform.position - transform.position).normalized;
        var distance = Vector2.Distance(transform.position, _playerTransform.position);

        if (distance <= radius)
        {
            var intensity = (distance / radius) * speed;
            _body.velocity = targetMove * Mathf.Clamp(intensity, 2f, speed);
        }
    }
}
