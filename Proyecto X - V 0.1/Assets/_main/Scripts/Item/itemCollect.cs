using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollect : MonoBehaviour
{

    [SerializeField] private ParticleSystem particleSys;
    [SerializeField] private AudioSource sound;

    private bool OnCollect = false;

    private void Update()
    {
        if (OnCollect && Input.GetKeyDown(KeyCode.F))
        {
            sound.Play();
            Invoke("destroyGameObject", 0.175f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnCollect = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnCollect = false;
        }
    }

    private void destroyGameObject()
    {
        Destroy(gameObject);
    }
}
