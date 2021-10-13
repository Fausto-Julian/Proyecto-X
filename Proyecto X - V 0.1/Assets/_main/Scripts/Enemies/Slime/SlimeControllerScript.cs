using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeControllerScript : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float radius;

    [SerializeField] private GameObject diamond;

    private Transform playerTransform;
    private Animator anim;
    private HealthController healthController;

    private void Awake()
    {
        healthController = GetComponent<HealthController>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        healthController.SetDefaultHealth();
    }

    
    private void Update()
    {
        if (PauseGameManager.inst.IsGameRuning())
        {
            if (healthController.IsAlive() == false)
            {
                Instantiate(diamond, transform.position, transform.rotation);
                Destroy(gameObject);
            }

            var distance = Vector2.Distance(transform.position, playerTransform.position);

            if (distance <= radius)
            {
                var intensity = (distance / radius) * speed;
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, Mathf.Clamp(intensity, 2f, speed) * Time.deltaTime);
            }
        }
    }
}
