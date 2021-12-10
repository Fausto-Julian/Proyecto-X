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

    private int _enemysLevelCount = 0;

    private PlayerData _playerData = new PlayerData();
    public Action<int> OnChangeDiamond;
    private HealthController _playerHealthController;

    private const string _pathGame = "Game";
    private const string _filenameGameState = "GameState";
    
    private const string _pathPlayer = "Player";
    private const string _fileNamePlayerData = "PlayerData";

    private GameState _gameState;

    private void Awake()
    {
        if (inst == null)
        {
            GameManager.inst = this;
            DontDestroyOnLoad(gameObject);
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
        _playerHealthController = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthController>();
        _playerHealthController.OnChangeHealth += SetPlayerCurrentLife;
        _playerHealthController.OnDeath += DefeatGameHandler;
    }

    private void Start()
    {
        _gameState = SaveLoadSystemData.LoadData<GameState>(_pathGame, _filenameGameState);

        switch (_gameState)
        {
            case GameState.newGame:
                _gameState = GameState.game;
                SaveData();
                break;
            case GameState.game:
                LoadData();
                HudManager.inst.LoadHud(_playerData.diamondPoints, _playerData.currenHealthPlayer);
                break;
            default:
                break;
        }
    }

    public GameState CheckGameState()
    {
        return _gameState;
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
        
        _playerHealthController.OnChangeHealth -= SetPlayerCurrentLife;
        _playerHealthController.OnDeath -= DefeatGameHandler;
        _gameState = GameState.newGame;
        _playerData.currenHealthPlayer = 0;
        _playerData.diamondPoints = 0;
        _playerData.levelIndex = 1;
        SaveData();
        SceneManager.LoadScene("DefeatScene");
        SkillTreeManager.inst.DeletedManagerHandler();
        HudManager.inst.DeletedManagerHandler();
        PauseGameManager.inst.DeletedManagerHandler();
        Destroy(gameObject, 1f);
    }

    public void DeletedManagerHandler()
    {
        Destroy(gameObject);
    }
}
