using System;
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
    [SerializeField] private Transform center;
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
    private void Start()
    {
        if (GameManager.inst.CheckFirstExecutionLife())
        {
            healthController.SetDefaultHealth();
            GameManager.inst.SetPlayerCurrentLife(healthController.GetCurrentHealth());
        }
        else
        {
            healthController.SetHealth(GameManager.inst.LoadPlayerCurrentLife());
        }
    }

    private void Update()
    {
        // Solucionar no se mueve
        if (PauseGameManager.inst.IsGameRuning())
        {
            SaveLife();

            float t = Time.deltaTime;
            movementX = Input.GetAxisRaw("Horizontal");
            movementY = Input.GetAxisRaw("Vertical");
            movement = new Vector2(movementX, movementY).normalized;

            SetAnimations();

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

    private void SaveLife()
    {
        GameManager.inst.SetPlayerCurrentLife(healthController.GetCurrentHealth());
    }

    private void SetAnimations()
    {
        anim.SetFloat("Movement X", movementX);
        anim.SetFloat("Movement Y", movementY);
        anim.SetFloat("Speed", movement.sqrMagnitude);


        if (movementY == 1f && movementX == 1f)
        {
            center.rotation = Quaternion.Euler(new Vector3(0f, 0f, 125f));
        }
        else if (movementY == -1f && movementX == 1f)
        {
            center.rotation = Quaternion.Euler(new Vector3(0f, 0f, 45f));
        }
        else if (movementY == 1f && movementX == -1f)
        {
            center.rotation = Quaternion.Euler(new Vector3(0f, 0f, 225f));
        }
        else if (movementY == -1f && movementX == -1f)
        {
            center.rotation = Quaternion.Euler(new Vector3(0f, 0f, 315f));
        }
        else if (movementY == 1f && movementX == 0f)
        {
            center.rotation = Quaternion.Euler(new Vector3(0f, 0f, 180f));
            anim.SetFloat("RotateX", 0f);
            anim.SetFloat("RotateY", 1f);
        }
        else if (movementY == -1f && movementX == 0f)
        {
            center.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
            anim.SetFloat("RotateX", 0f);
            anim.SetFloat("RotateY", -1f);
        }
        else if (movementX == 1f && movementY == 0f)
        {
            center.rotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
            anim.SetFloat("RotateX", 1f);
            anim.SetFloat("RotateY", 0f);
        }
        else if (movementX == -1f && movementY == 0f)
        {
            center.rotation = Quaternion.Euler(new Vector3(0f, 0f, 270f));
            anim.SetFloat("RotateX", -1f);
            anim.SetFloat("RotateY", 0f);
        }
    }
}
