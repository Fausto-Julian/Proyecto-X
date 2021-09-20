using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Camera cam;

    [SerializeField] private Transform spawnPointAtack1;
    [SerializeField] private GameObject atack1;

    private Rigidbody2D rb;

    private Vector2 movement;
    private Vector2 mousePos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        float t = Time.deltaTime;

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        rb.MovePosition(rb.position + movement * speed * t);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(atack1, spawnPointAtack1.position, spawnPointAtack1.rotation);
        }
    }

}
