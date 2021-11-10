using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] private AudioSource music;
    private AudioSource _musicCombat;

    private void Start()
    {
        _musicCombat = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.CompareTag("Player"))
        {
            music.Stop();
            _musicCombat.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            music.Play();
            _musicCombat.Stop();
        }
    }
}
