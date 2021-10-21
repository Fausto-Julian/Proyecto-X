using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeControllerScript : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float radius;
    [SerializeField] private int damage;

    [SerializeField] private GameObject diamond;

    private Rigidbody2D rb;
    private Animator anim;
    private HealthController healthController;

    private HealthController PlayerHealthController;
    private Transform playerTransform;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        healthController = GetComponent<HealthController>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        healthController.SetDefaultHealth();
    }

    
    private void Update()
    {
        if (PauseGameManager.inst.CheckIsGameRunning())
        {
            if (healthController.IsAlive() == false)
            {
                Instantiate(diamond, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 targetMove = (playerTransform.position - transform.position).normalized;
        var distance = Vector2.Distance(transform.position, playerTransform.position);

        if (distance <= radius)
        {
            var intensity = (distance / radius) * speed;
            rb.velocity = targetMove * Mathf.Clamp(intensity, 2f, speed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealthController = collision.gameObject.GetComponent<HealthController>();

            if (PlayerHealthController != null)
            {
                PlayerHealthController.GetDamage(damage);
            }
        }
    }
}
