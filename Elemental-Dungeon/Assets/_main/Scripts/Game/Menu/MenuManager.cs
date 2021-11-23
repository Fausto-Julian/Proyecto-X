using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Localization;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Button PlayButton;
    [SerializeField] private Text playText;
    [SerializeField] private GameObject continueObject;
    [SerializeField] private Button continueButton;
    [SerializeField] private GameObject panel;
    [SerializeField] private Button openPanelButton;
    [SerializeField] private Button closePanelButton;
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

            Debug.Log(Application.persistentDataPath + "/Data/" + _path + "/");

            _gameState = SaveLoadSystemData.LoadData<GameState>(_path, _fileNameGameState);
            _playerData = SaveLoadSystemData.LoadData<PlayerData>(_pathPlayer, _fileNamePlayerData);

        

            switch (_gameState)
            {
                case GameState.newGame:
                    continueObject.SetActive(false);
                    playText.text = "Play";
                    break;
                case GameState.game:
                    continueButton.onClick.AddListener(OnClickContinueGameHandler);
                    continueObject.SetActive(true);
                    playText.text = "New Game";
                    break;
                default:
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
