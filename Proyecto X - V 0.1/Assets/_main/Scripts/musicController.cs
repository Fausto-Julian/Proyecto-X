using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicController : MonoBehaviour
{
    [SerializeField] private AudioSource music;
    private AudioSource musicCombat;

    private void Start()
    {
        musicCombat = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.CompareTag("Player"))
        {
            music.Stop();
            musicCombat.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            Debug.Log("sali");
            music.Play();
            musicCombat.Stop();
        }
    }
}
