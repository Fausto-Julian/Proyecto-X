using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private Animator anim;
    [SerializeField] private Transform spawnPointAtack1;
    [SerializeField] private GameObject atack1;
    [SerializeField] private GameObject melee;

    private Rigidbody2D rb;
    private Vector2 movement;
    private float movementX;
    private float movementY;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        float t = Time.deltaTime;

        movementX = Input.GetAxisRaw("Horizontal");
        movementY = Input.GetAxisRaw("Vertical");
        movement = new Vector2(movementX, movementY).normalized;

        anim.SetFloat("Movement X", movementX);
        anim.SetFloat("Movement Y", movementY);
        anim.SetFloat("Speed", movement.sqrMagnitude);

        //transform.position += ;



        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //Instantiate(atack1, spawnPointAtack1.position, spawnPointAtack1.rotation);
            melee.SetActive(true);
            anim.SetBool("DownSlash", true);
            Invoke("falseMele", 0.5f);
            Invoke("falseDownSlash", 0.5f);
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

    private void falseMele()
    {
        melee.SetActive(false);
    }
}
