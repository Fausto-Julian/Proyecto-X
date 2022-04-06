using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIce : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private float timingShoot;
    [Space()]
    [Header("Reference")]
    [SerializeField] private GameObject diamond;
    [SerializeField] private GameObject bulletPrefab;
    private HealthController _healthController;
    private Transform _playerTransform;
    private Rigidbody2D _body;
    private bool IsRoutineActive;
    private bool IsMoving;
    private Animation _anim;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _healthController = GetComponent<HealthController>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _healthController.SetDefaultHealth();
        _healthController.OnDeath += IsDeathHandler;
        IsMoving = true;
        _anim = GetComponent<Animation>();
    }
    
    private void IsDeathHandler()
    {
        _healthController.OnDeath -= IsDeathHandler;
        Instantiate(diamond, transform.position, transform.rotation);
        Instantiate(diamond, transform.position + transform.up * 1.5f, transform.rotation);
        Instantiate(diamond, transform.position - transform.up * 1.5f, transform.rotation);
        Instantiate(diamond, transform.position + transform.right * 1.5f, transform.rotation);
        Destroy(gameObject);
    }

    private void Update()
    {
        if (IsRoutineActive == false)
        {
            StartCoroutine(Shooting());
        }
    }

    private void FixedUpdate()
    {
        if (IsMoving)
        {
            Vector2 targetMove = (_playerTransform.position - transform.position).normalized;
            var distance = Vector2.Distance(transform.position, _playerTransform.position);

            if (distance <= 20)
            {
                var intensity = (distance / 20) * speed;
                _body.velocity = targetMove * Mathf.Clamp(intensity, 2f, speed);
            }
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

    IEnumerator Shooting()
    {
        IsRoutineActive = true;
        IsMoving = false;
        _body.velocity = Vector2.zero;
        yield return new WaitForSeconds(2f);
        _anim.Play("AtaqueAliento");
        Instantiate(bulletPrefab, transform.position, transform.rotation);
        yield return new WaitForSeconds(1f);
        _anim.Play("IdleHielo");
        IsMoving = true;
        yield return new WaitForSeconds(timingShoot);
        IsRoutineActive = false;
    }
}
