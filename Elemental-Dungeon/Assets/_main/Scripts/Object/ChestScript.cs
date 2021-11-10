using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSys;
    [SerializeField] private GameObject diamond;

    private Animator _anim;

    private bool _canOpen;
    private bool _leaveOpen;

    private void Awake()
    {
        _canOpen = false;
        _leaveOpen = false;
        _anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && _leaveOpen == false) 
        {
            _canOpen = true;
            if (_canOpen)
            {
                particleSys.Play();
                _anim.SetTrigger("Open");
                diamond.SetActive(true);
                _leaveOpen = true;
            }
        }
    }
}
