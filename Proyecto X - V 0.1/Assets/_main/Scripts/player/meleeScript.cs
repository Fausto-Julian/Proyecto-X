using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeScript : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private GameObject diamond;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Enemy")
        {
            Instantiate(diamond, collision.transform.position, collision.transform.rotation);
            Destroy(collision.gameObject);
        }
    }
}
