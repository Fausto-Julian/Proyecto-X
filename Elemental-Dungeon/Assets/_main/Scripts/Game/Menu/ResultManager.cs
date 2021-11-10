using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    [Space()]
    [SerializeField] private GameObject panelExit;

    [Space()]
    [Header("Button")]
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button backMenuButton;
    [SerializeField] private Button activePanelExitButton;
    [SerializeField] private Button desactivePanelExitButton;
    [SerializeField] private Button exitButton;

    private GameState gameState;
    private const string path = "Game";
    private const string fileName = "GameState";

    private void Awake()
    {
        panelExit.SetActive(false);
        playAgainButton.onClick.AddListener(playAgainButtonHandler);
        backMenuButton.onClick.AddListener(BackToMenuHandler);
        activePanelExitButton.onClick.AddListener(ActivePanelExitHandler);
        desactivePanelExitButton.onClick.AddListener(DesactivePanelExitHandler);
        exitButton.onClick.AddListener(ExitAplicationHandler);

        gameState = GameState.newGame;
        SaveLoadSystemData.SaveData(gameState, path, fileName);
    }

    private void playAgainButtonHandler()
    {
        SceneManager.LoadScene(1);
    }

    private void BackToMenuHandler()
    {
        SceneManager.LoadScene(0);
    }

    private void ActivePanelExitHandler()
    {
        panelExit.SetActive(true);
    }

    private void DesactivePanelExitHandler()
    {
        panelExit.SetActive(false);
    }

    private void ExitAplicationHandler()
    {
        Application.Quit();
    }
}
