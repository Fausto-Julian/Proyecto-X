using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager inst;

    [SerializeField] private HealthController playerHealthController;
    private PlayerData _playerData = new PlayerData();
    private int _enemysLevelCount = 0;

    private void Awake()
    {
        if (inst == null)
        {
            inst = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        playerHealthController.OnChangeHealth += SetPlayerCurrentLife;
        playerHealthController.OnDeath += DefeatGameHandler;
    }

    private void Update()
    {
        
    }

    private void DefeatGameHandler()
    {
        playerHealthController.OnChangeHealth -= SetPlayerCurrentLife;
        playerHealthController.OnDeath -= DefeatGameHandler;
        _playerData.currenHealthPlayer = 0;
        _playerData.diamondPoints = 0;
        _playerData.levelIndex = 1;
    }

    #region Player Function
    public void SetPlayerCurrentLife(float currentLife)
    {
        _playerData.currenHealthPlayer = currentLife;
    }

    public float LoadPlayerCurrentLife()
    {
        return _playerData.currenHealthPlayer;
    }
    #endregion

    #region EnemyCount Function
    public void AddEnemyCount(int count)
    {
        _enemysLevelCount += count;
    }

    public void RemoveEnemy()
    {
        _enemysLevelCount -= 1;
        if (_enemysLevelCount < 0)
        {
            _enemysLevelCount = 0;
        }
    }

    public int GetEnemyCount()
    {
        return _enemysLevelCount;
    }
    #endregion
}
