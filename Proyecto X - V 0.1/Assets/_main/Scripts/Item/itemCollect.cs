using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollect : MonoBehaviour
{
    [SerializeField] private int value = 1;

    [SerializeField] private ParticleSystem particleSys;
    [SerializeField] private AudioSource sound;

    private bool OnCollect = false;
    //private GameManager gameManager;
    private void Awake()
    {
        //gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (OnCollect && Input.GetKeyDown(KeyCode.F))
        {
            //gameManager.OnCollectionDiamondHandler(value);
            GameManager.inst.OnCollectionDiamondHandler(value);
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
