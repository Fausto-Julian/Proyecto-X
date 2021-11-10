using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollect : MonoBehaviour
{
    [SerializeField] private int value = 1;

    [SerializeField] private GameObject sprite;
    [SerializeField] private ParticleSystem particleSys;
    [SerializeField] private AudioSource sound;

    private bool _OnCollect = false;
    private bool _collect = false;

    private void Update()
    {
        if (_OnCollect && Input.GetKeyDown(KeyCode.F) && _collect == false)
        {
            _collect = true;
            sprite.SetActive(false);
            GameManager.inst.AddDiamondPoint(value);
            particleSys.Play();
            sound.Play();
            Destroy(gameObject, 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _OnCollect = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _OnCollect = false;
        }
    }
}
