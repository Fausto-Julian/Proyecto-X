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
    private NavMeshAgent nav;
    private Animator anim;
    private HealthController healthController;
    private LevelManager levelManager;
    private PauseGameManager pauseGameManager;

    private void Awake()
    {
        healthController = GetComponent<HealthController>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        levelManager = FindObjectOfType<LevelManager>();
        pauseGameManager = FindObjectOfType<PauseGameManager>();
    }

    private void Start()
    {
        healthController.SetDefaultHealth();
        nav.speed = speed;
        nav.updateRotation = false;
        nav.updateUpAxis = false;
    }

    
    private void Update()
    {
        if (pauseGameManager.IsGameRuning())
        {
            if (healthController.IsAlive() == false)
            {
                levelManager.RestEnemies();
                Instantiate(diamond, transform.position, transform.rotation);
                Destroy(gameObject);
            }

            var distance = Vector2.Distance(transform.position, playerTransform.position);

            if (distance <= radius)
            {

                var intensity = (distance / radius) * speed;

                nav.speed = Mathf.Clamp(intensity, 2f, speed);

                nav.isStopped = false;
                anim.SetBool("Move", true);
                nav.SetDestination(playerTransform.position);

            }
            else if (distance > radius)
            {
                nav.speed = speed;
                nav.isStopped = true;
                anim.SetBool("Move", false);
            }
        }
    }
}
