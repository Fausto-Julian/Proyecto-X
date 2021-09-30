using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atack1Script : MonoBehaviour
{
    [SerializeField] private float timeLifeBullet = 1f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private GameObject diamond;

    private float currentTime;


    void Update()
    {
        float t = Time.deltaTime;

        currentTime += Time.deltaTime;
        if (currentTime >= timeLifeBullet)
        {
            Destroy(gameObject);
        }

        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Enemy")
        {
            Instantiate(diamond, collision.transform.position, collision.transform.rotation);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
