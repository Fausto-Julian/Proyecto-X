using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class test : MonoBehaviour
{
    [SerializeField] private float radius;
    private Transform playerTransform;
    private NavMeshAgent nav;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        nav.updateRotation = false;
        nav.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        var distance = Vector3.Distance(transform.position, playerTransform.position);

        if (distance < radius)
        {
            nav.isStopped = false;
            nav.SetDestination(playerTransform.position);
        }
        else if (distance > radius)
        {
            nav.isStopped = true;
        }
    }
}
