using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthController))]
public class DamageManager : MonoBehaviour
{
    private HealthController _healthController;

    private bool _onFire;

    private void Awake()
    {
        _healthController = GetComponent<HealthController>();
        _onFire = false;
    }

    public void ActiveSetFire(float damegeForSecond, float timeout)
    {
        if (_onFire == false)
        {
            _onFire = true;
            StartCoroutine(SetOnFire(damegeForSecond, timeout));
        }
        
    }

    IEnumerator SetOnFire(float damageForSecond, float timeout)
    {
        var currentTime = 0f;
        while (currentTime < timeout)
        {
            yield return new WaitForSeconds(1f);
            _healthController.SetDamage(damageForSecond);
            currentTime += 1;
        }
        _onFire = false;
    }
}
