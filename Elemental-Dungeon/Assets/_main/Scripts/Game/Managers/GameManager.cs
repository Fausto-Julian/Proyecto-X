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

    private int enemysLevelCount = 0;
    private int _diamondPoints = 0;
    private float _currenHealthPlayer;
    public Action<int> diamondAction;
    private HealthController _playerHealthController;

    private int _levelIndex;

    private const string pathGame = "Game";
    private const string filenameGameState = "GameState";
    private const string fileNameLevel = "LevelIndex";
    
    private const string pathPlayer = "Player";
    private const string fileNameHealth = "HealthController";
    private const string fileNameDiamond = "DiamondPoints";

    private GameState gameState;

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

        _levelIndex = 1;
        _playerHealthController = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthController>();
        _playerHealthController.changeHealth += SetPlayerCurrentLife;
        _playerHealthController.isDeath += DefeatGameHandler;
    }

    private void Start()
    {
        gameState = SaveLoadSystemData.LoadData<GameState>(pathGame, filenameGameState);

        switch (gameState)
        {
            case GameState.newGame:
                gameState = GameState.game;
                SaveData();
                break;
            case GameState.game:
                LoadData();
                break;
            default:
                break;
        }
    }

    public GameState CheckGameState()
    {
        return gameState;
    }

    #region Player Function
    public void SetPlayerCurrentLife(float currentLife)
    {
        _currenHealthPlayer = currentLife;
    }

    public float LoadPlayerCurrentLife()
    {
        return _currenHealthPlayer;
    }
    #endregion

    #region EnemyCount Function
    public void AddEnemyCount(int count)
    {
        enemysLevelCount += count;
    }

    public void RemoveEnemy()
    {
        enemysLevelCount -= 1;
        if (enemysLevelCount < 0)
        {
            enemysLevelCount = 0;
        }
    }

    public int GetEnemyCount()
    {
        return enemysLevelCount;
    }
    #endregion

    #region DiamondPoint Function
    public void AddDiamondPoint(int diamondCount)
    {
        _diamondPoints += diamondCount;
        diamondAction?.Invoke(_diamondPoints);
    }

    public int GetDiamondPoint()
    {
        return _diamondPoints;
    }

    public void subtractDiamond(int count)
    {
        _diamondPoints -= count;
    }
    #endregion

    public void SaveData()
    {
        SaveLoadSystemData.SaveData(gameState, pathGame, filenameGameState);
        SaveLoadSystemData.SaveData(_levelIndex, pathGame, fileNameLevel);
        SaveLoadSystemData.SaveData(_currenHealthPlayer, pathPlayer, fileNameHealth);
        SaveLoadSystemData.SaveData(_diamondPoints, pathPlayer, fileNameDiamond);
    }
    public void NextLevel()
    {
        _levelIndex += 1;
        SaveData();
        SceneManager.LoadScene(_levelIndex);
    }

    public void LoadData()
    {
        _currenHealthPlayer = SaveLoadSystemData.LoadData<float>(pathPlayer, fileNameHealth);
        _diamondPoints = SaveLoadSystemData.LoadData<int>(pathPlayer, fileNameDiamond);
        _levelIndex = SaveLoadSystemData.LoadData<int>(pathPlayer, fileNameLevel);
    }

    private void DefeatGameHandler()
    {
        SceneManager.LoadScene(11);
        ResetGame();
    }

    private void ResetGame()
    {
        _playerHealthController.changeHealth -= SetPlayerCurrentLife;
        _playerHealthController.isDeath -= ResetGame;
        gameState = GameState.newGame;
        _currenHealthPlayer = 0;
        _diamondPoints = 0;
        _levelIndex = 1;
        SaveData();
        SceneManager.LoadScene(0);
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
