using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemCollect : MonoBehaviour
{

    [SerializeField] private ParticleSystem particleSys;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.F))
        {
            particleSys.Play();
            Destroy(gameObject);
        }
    }
}
