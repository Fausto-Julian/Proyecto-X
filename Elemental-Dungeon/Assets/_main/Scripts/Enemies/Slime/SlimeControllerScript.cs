using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeControllerScript : MonoBehaviour
{

    [Header("Stats")]
    [SerializeField] private bool fire;
    [SerializeField] private bool water;
    [SerializeField] private bool rock;
    [SerializeField] private bool wind;
    [SerializeField] private float speed;
    [SerializeField] private float radius;
    [SerializeField] private float damage;

    [Space, Header("Stats Fire")]
    [SerializeField] private float damageForSecondFire;
    [SerializeField] private float timeoutFire;

    [Space]
    [Header("Instaciar")]
    [SerializeField] private GameObject nexSlime;
    [SerializeField, Range(1, 2)] private int level;
    [SerializeField] private GameObject diamond;
    [Space]
    [SerializeField] private Animator anim;
    private Rigidbody2D _body;
    private HealthController _healthController;

    private Transform _playerTransform;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _healthController = GetComponent<HealthController>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
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
        //_body.velocity = Vector2.zero;
        //anim.SetBool("Death", true);
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (_playerTransform != null)
        {
            Vector2 targetMove = (_playerTransform.position - transform.position).normalized;
            var distance = Vector2.Distance(transform.position, _playerTransform.position);

            if (distance <= radius)
            {
                var intensity = (distance / radius) * speed;
                _body.velocity = targetMove * Mathf.Clamp(intensity, 2f, speed);
                anim.SetBool("Move", true);
            }
            else
            {
                anim.SetBool("Move", false);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthController PlayerHealthController = collision.gameObject.GetComponent<HealthController>();
            DamageManager playerDamageManager = collision.gameObject.GetComponent<DamageManager>();

            if (PlayerHealthController != null)
            {
                PlayerHealthController.SetDamage(damage);

                if (playerDamageManager != null)
                {
                    if (fire)
                    {
                        playerDamageManager.ActiveSetFire(damageForSecondFire, timeoutFire);
                    }
                }
            }
        }
    }
}
