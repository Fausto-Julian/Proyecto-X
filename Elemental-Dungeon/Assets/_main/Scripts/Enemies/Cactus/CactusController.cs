using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusController : MonoBehaviour
{
    [SerializeField] private float timeShooting;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private LayerMask mask;
    [SerializeField] private GameObject diamond;

    private HealthController _healthController;
    private bool _hard;
    private float _time;

    private void Awake()
    {
        _healthController = GetComponent<HealthController>();
        _hard = false;
        _time = 0;

        _healthController.SetDefaultHealth();
        _healthController.isDeath += IsDeathHandler;
    }

    private void Update()
    {
        if (!_hard)
        {
            
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, 40f, mask);

            Debug.DrawRay(transform.position, -transform.up * 20f, Color.red);

            if (hit.collider != null)
            {
                Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
                _hard = true;
            }
        }
        else
        {
            _time += Time.deltaTime;

            if (_time >= timeShooting)
            {
                _hard = false;
                _time = 0;
            }
        }
    }

    private void IsDeathHandler()
    {
        _healthController.isDeath -= IsDeathHandler;
        Instantiate(diamond, transform.position, Quaternion.Euler(Vector3.zero));
        Destroy(gameObject);
    }
}
