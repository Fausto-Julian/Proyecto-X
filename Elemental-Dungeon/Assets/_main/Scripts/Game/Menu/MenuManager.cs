using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    private const string path = "Game";
    private const string fileNameGameState = "GameState";
    private const string fileNameLevel = "LevelIndex";

    private GameState gameState;

    private void Awake()
    {
        PlayButton.onClick.AddListener(OnClickPlayHandler);
        openPanelButton.onClick.AddListener(OnClickQuitPanelActivate);
        closePanelButton.onClick.AddListener(OnClickQuitPanelDesactivate);

        Debug.Log(Application.persistentDataPath + "/Data/" + path + "/");

        gameState = SaveLoadSystemData.LoadData<GameState>(path, fileNameGameState);

        switch (gameState)
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
        SceneManager.LoadScene(SaveLoadSystemData.LoadData<int>(path, fileNameLevel));
    }

    private void OnClickPlayHandler()
    {
        gameState = GameState.newGame;
        SaveLoadSystemData.SaveData(gameState, path, fileNameGameState);
        SceneManager.LoadScene(1);
    }

    private void OnClickExitGameHandler()
    {
        Application.Quit();
    }
}
