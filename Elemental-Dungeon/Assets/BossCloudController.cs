using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCloudController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damageForAttractionForSecond;
    [SerializeField] private float forceAttraction;
    [SerializeField] private float timeShooting;
    [SerializeField] private float timeAttraction;
    [SerializeField] private float timeAttractionForNext;
    [SerializeField] private GameObject diamond;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPoint;

    private HealthController _healthController;
    private Rigidbody2D _body;
    private Animation _anim;
    private Transform _playerTransform;
    private Rigidbody2D _playerBody;

    private bool right;
    private bool shoot;
    private bool attraction;
    private bool attractionStart;
    private bool start;
    private bool aux;
    private bool halfLife;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animation>();
        _healthController = GetComponent<HealthController>();
        
    }

    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _playerBody = _playerTransform.GetComponent<Rigidbody2D>();
        _healthController.OnDeath += IsDeathHandler;
    }

    private void Update()
    {
        if (start)
        {
            if (!shoot && !attraction)
            {
                if (_playerTransform.position.x > (transform.position.x - 10f) && _playerTransform.position.x < (transform.position.x - 2f))
                {
                    StartCoroutine(Shooting());
                }
            }
            else if (halfLife)
            {
                if (!attractionStart)
                {
                    StartCoroutine(Attraction());
                }
                else if (attraction)
                {
                    if (_playerBody != null)
                    {
                        Vector3 playerDir = ((transform.position - (new Vector3(4f, 6f))) - _playerTransform.position).normalized;
                        _playerTransform.position += (playerDir * forceAttraction * Time.deltaTime);
                    }
                }
            }

            if (_healthController.GetCurrentHealth() < (_healthController.GetDefaultHealth() * 50 / 100))
            {
                halfLife = true;
            }
        }
        else
        {
            if (!aux)
                StartCoroutine(StartCorrutine());
        }
        
    }

    private void FixedUpdate()
    {
        if (!attraction)
        {
            if (right)
            {
                var dir = Vector2.right * speed;
                _body.velocity = dir;
            }
            else
            {
                var dir = -(Vector2.right * speed);
                _body.velocity = dir;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            right = !right;
        }
    }

    IEnumerator Shooting()
    {
        _anim.Play("NubeAngry");
        shoot = true;
        Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        yield return new WaitForSeconds(timeShooting);
        shoot = false;
        _anim.Play("NubeIdle");
    }

    IEnumerator Attraction()
    {
        _anim.Play("NubeAtaqueAtraccion");
        attractionStart = true;
        attraction = true;
        yield return new WaitForSeconds(1f);
        HealthController player = _playerTransform.GetComponent<HealthController>();
        
        if (player != null)
            player.SetDamage(damageForAttractionForSecond);
        yield return new WaitForSeconds(timeAttraction);
        Debug.Log("hola");
        attraction = false;
        _anim.Play("NubeIdle");
        yield return new WaitForSeconds(timeAttractionForNext);
        attractionStart = false;
    }

    IEnumerator StartCorrutine()
    {
        aux = true;
        yield return new WaitForSecondsRealtime(4f);
        start = true;
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
}
