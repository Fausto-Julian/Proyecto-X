using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthController))]
public class EnemiesTest : MonoBehaviour
{
    [SerializeField] private GameObject diamond;
    private HealthController healthController;

    void Start()
    {
        healthController = GetComponent<HealthController>();
    }

    void Update()
    {
        if (healthController.IsAlive() == false)
        {
            Instantiate(diamond, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
