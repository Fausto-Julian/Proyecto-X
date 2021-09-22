using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atack1Script : MonoBehaviour
{
    [SerializeField] private float timeLifeBullet = 1f;
    [SerializeField] private float damage = 10f;

    private float currentTime;


    void Update()
    {
        /*
        currentTime += Time.deltaTime;
        if (currentTime >= timeLifeBullet)
        {
            Destroy(gameObject);
        }
        */
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
    }
}
