using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthController))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private Animator anim;
    [SerializeField] private Transform spawnPointAtack1;
    [SerializeField] private GameObject bulletFire;
    [SerializeField] private GameObject melee;

    [SerializeField] private AudioClip espada = null;
    private AudioSource aS;

    private Rigidbody2D rb;
    private Vector2 movement;
    private float movementX;
    private float movementY;

    private HealthController healthController;

    private void Awake()
    {
        healthController = GetComponent<HealthController>();
        rb = GetComponent<Rigidbody2D>();
        aS = GetComponent<AudioSource>();
    }

    private void Update()
    {

        if (healthController.IsAlive() == false)
        {
            Destroy(gameObject);
        }

        float t = Time.deltaTime;

        movementX = Input.GetAxisRaw("Horizontal");
        movementY = Input.GetAxisRaw("Vertical");
        movement = new Vector2(movementX, movementY).normalized;

        anim.SetFloat("Movement X", movementX);
        anim.SetFloat("Movement Y", movementY);
        anim.SetFloat("Speed", movement.sqrMagnitude);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            melee.SetActive(true);
            anim.SetBool("DownSlash", true);
            aS.clip = espada;
            aS.Play();
            Invoke("falseMele", 0.5f);
            Invoke("falseDownSlash", 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Instantiate(bulletFire, spawnPointAtack1.position, spawnPointAtack1.rotation);
            anim.SetBool("DownCast", true);
            Invoke("falseDownCast", 0.5f);
        }
    }

    private void FixedUpdate()
    {
        float t = Time.fixedDeltaTime;

        rb.MovePosition(rb.position + movement * speed * t);
    }

    private void falseDownSlash()
    {
        anim.SetBool("DownSlash", false);
    }
    private void falseDownCast()
    {
        anim.SetBool("DownCast", false);
    }

    private void falseMele()
    {
        melee.SetActive(false);
    }
}
