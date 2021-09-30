using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSys;
    [SerializeField] private GameObject diamond;

    private Animator anim;

    private bool canOpen;
    private bool leaveOpen;

    private void Start()
    {
        canOpen = false;
        leaveOpen = false;
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && leaveOpen == false) 
        {
            canOpen = true;
            if (canOpen)
            {
                particleSys.Play();
                anim.SetTrigger("Open");
                diamond.SetActive(true);
                leaveOpen = true;
            }
        }
    }
}
