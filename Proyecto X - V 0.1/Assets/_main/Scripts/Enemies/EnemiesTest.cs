using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthController))]
public class EnemiesTest : MonoBehaviour
{
    [SerializeField] private GameObject diamond;
    private HealthController healthController;

    private LevelManager levelManager;

    private void Awake()
    {
        healthController = GetComponent<HealthController>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void Start()
    {
        healthController.SetDefaultHealth();
    }

    private void Update()
    {
        if (healthController.IsAlive() == false)
        {
            levelManager.RestEnemies();
            Instantiate(diamond, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
