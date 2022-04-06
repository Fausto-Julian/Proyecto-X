using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    newGame,
    game
}

public class GameManager : MonoBehaviour
{
    public static GameManager inst;

    [SerializeField] private HealthController playerHealthController;
    [SerializeField] private int lifePlayerDefault;
    private PlayerData _playerData = new PlayerData();
    public Action<int> OnChangeDiamond;

    private const string _pathGame = "Game";
    private const string _filenameGameState = "GameState";
    
    private const string _pathPlayer = "Player";
    private const string _fileNamePlayerData = "PlayerData";

    private int _enemysLevelCount = 0;

    private GameState _gameState;

    private void Awake()
    {
        if (inst == null)
        {
            GameManager.inst = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _playerData.levelIndex = 1;
        SetPlayerHealthController();
    }

    public void SetPlayerHealthController()
    {
        if (playerHealthController != null)
        {
            playerHealthController.OnChangeHealth += SetPlayerCurrentLife;
            playerHealthController.OnDeath += DefeatGameHandler;
        }
    }

    private void Start()
    {
        _gameState = SaveLoadSystemData.LoadData<GameState>(_pathGame, _filenameGameState);

        switch (_gameState)
        {
            case GameState.newGame:
                _gameState = GameState.game;
                _playerData.life = lifePlayerDefault;
                SaveData();
                break;
            case GameState.game:
                LoadData();
                HudManager.inst.LoadHud(_playerData.diamondPoints, _playerData.life, _playerData.currenHealthPlayer);
                break;
            default:
                break;
        }
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

    #region DiamondPoint Function
    public void AddDiamondPoint(int diamondCount)
    {
        _playerData.diamondPoints += diamondCount;
        OnChangeDiamond?.Invoke(_playerData.diamondPoints);
    }

    public int GetDiamondPoint()
    {
        return _playerData.diamondPoints;
    }

    public void subtractDiamond(int count)
    {
        _playerData.diamondPoints -= count;
        OnChangeDiamond?.Invoke(_playerData.diamondPoints);
    }
    #endregion

    public void SaveData()
    {
        SaveLoadSystemData.SaveData(_gameState, _pathGame, _filenameGameState);
        SaveLoadSystemData.SaveData(_playerData, _pathPlayer, _fileNamePlayerData);
    }

    public void NextLevel()
    {
        _playerData.levelIndex += 1;
        SaveData();
        SceneManager.LoadScene(_playerData.levelIndex);
    }

    public void LoadData()
    {
        _playerData = SaveLoadSystemData.LoadData<PlayerData>(_pathPlayer, _fileNamePlayerData);
    }

    private void DefeatGameHandler()
    {
        if (_playerData.life <= 0)
        {
            playerHealthController.OnChangeHealth -= SetPlayerCurrentLife;
            playerHealthController.OnDeath -= DefeatGameHandler;
            _gameState = GameState.newGame;
            _playerData.currenHealthPlayer = 0;
            _playerData.diamondPoints = 0;
            _playerData.levelIndex = 1;
            SaveData();
            SkillTreeManager.inst.DeletedManagerHandler();
            PauseGameManager.inst.DeletedManagerHandler();
            SceneManager.LoadScene("DefeatScene");
        }
        else
        {
            _playerData.life -= 1;
            playerHealthController.SetDefaultHealth();
            _playerData.currenHealthPlayer = playerHealthController.GetCurrentHealth();
            HudManager.inst.LoadHud(_playerData.diamondPoints, _playerData.life, _playerData.currenHealthPlayer);
        }
    }

    public void DeletedManagerHandler()
    {
        Destroy(gameObject);
    }
}
