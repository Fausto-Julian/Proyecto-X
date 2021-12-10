using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeControllerScript : MonoBehaviour
{

    [Header("Stats")]
    [SerializeField] private float speed;
    [SerializeField] private float radius;
    [SerializeField] private float damage;

    [Space]
    [Header("Instaciar")]
    [SerializeField] private GameObject nexSlime;
    [SerializeField, Range(1, 2)] private int level;
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
        if (nexSlime != null)
        {
            if (level == 2)
            {
                Instantiate(nexSlime, transform.position, transform.rotation);
                Instantiate(nexSlime, transform.position + new Vector3(0f, 1.5f), transform.rotation);
                Instantiate(nexSlime, transform.position + new Vector3(0f, -1.5f), transform.rotation);
            }
            else if (level == 1)
            {
                Instantiate(nexSlime, transform.position, transform.rotation);
                Instantiate(nexSlime, transform.position + new Vector3(0f, 1.5f), transform.rotation);
            }
        }
        else
        {
            Instantiate(diamond, transform.position, transform.rotation);
        }
        Destroy(gameObject);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthController PlayerHealthController = collision.gameObject.GetComponent<HealthController>();

            if (PlayerHealthController != null)
            {
                PlayerHealthController.SetDamage(damage);
            }
        }
    }
}
