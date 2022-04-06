using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StatsBoat
{
    public int speed;
    public Sprite sprite;
}

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Button PlayButton;
    [SerializeField] private Text playText;
    [SerializeField] private GameObject continueObject;
    [SerializeField] private Button continueButton;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject panelCreditos;
    [SerializeField] private Button openPanelButton;
    [SerializeField] private Button closePanelButton;
    [SerializeField] private Button openPanelCreditosButton;
    [SerializeField] private Button closePanelCreditosButton;
    [SerializeField] private Button exitButton;


 

    private const string _path = "Game";
    private const string _pathPlayer = "Player";
    private const string _fileNameGameState = "GameState";
    private const string _fileNamePlayerData = "PlayerData";

    private GameState _gameState;
    private PlayerData _playerData;

    private void Awake()
    {
        PlayButton.onClick.AddListener(OnClickPlayHandler);
        openPanelButton.onClick.AddListener(OnClickQuitPanelActivate);
        closePanelButton.onClick.AddListener(OnClickQuitPanelDesactivate);
        exitButton.onClick.AddListener(OnClickExitGameHandler);
        openPanelCreditosButton.onClick.AddListener(OnClickQuitPanelCreditActivate);
        closePanelCreditosButton.onClick.AddListener(OnClickQuitPanelCreditDesactivate);
        _gameState = SaveLoadSystemData.LoadData<GameState>(_path, _fileNameGameState);
        _playerData = SaveLoadSystemData.LoadData<PlayerData>(_pathPlayer, _fileNamePlayerData);

        switch (_gameState)
        {
            case GameState.newGame:
                continueObject.SetActive(false);
                playText.text = "JUGAR";
                break;
            case GameState.game:
                continueButton.onClick.AddListener(OnClickContinueGameHandler);
                continueObject.SetActive(true);
                playText.text = "NUEVO JUEGO";
                break;
            default:
                _gameState = GameState.newGame;
                continueObject.SetActive(false);
                playText.text = "Play";
                break;
        }
    }

    private void OnClickQuitPanelActivate()
    {
        panel.SetActive(true);
    }

    private void OnClickQuitPanelDesactivate()
    {
        panel.SetActive(false);
    }

    private void OnClickQuitPanelCreditActivate()
    {
        panelCreditos.SetActive(true);
    }

    private void OnClickQuitPanelCreditDesactivate()
    {
        panelCreditos.SetActive(false);
    }

    private void OnClickContinueGameHandler()
    {
        SceneManager.LoadScene(_playerData.levelIndex);
    }

    private void OnClickPlayHandler()
    {
        _gameState = GameState.newGame;
        SaveLoadSystemData.SaveData(_gameState, _path, _fileNameGameState);
        SceneManager.LoadScene(1);
    }

    private void OnClickExitGameHandler()
    {
        Application.Quit();
    }
}
