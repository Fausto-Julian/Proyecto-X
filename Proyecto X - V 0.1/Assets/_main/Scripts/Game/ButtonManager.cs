using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{

    [SerializeField] private Button nextLevel;
    [SerializeField] private Button backMenu;
    [SerializeField] private Button exitButton;

    private void Awake()
    {
        if (nextLevel != null)
            nextLevel.onClick.AddListener(OnClickNextLevelHandler);
        if (backMenu != null)
            backMenu.onClick.AddListener(OnClickBackToMenuHandler);
        if (exitButton != null)
            exitButton.onClick.AddListener(OnClickExitGameHandler);
    }

    public void OnClickRetryLevelHandler()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickBackToMenuHandler()
    {
        SceneManager.LoadScene(0);
    }

    public void OnClickNextLevelHandler()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnClickPlayAgainHandler(int checkPoint = 1)
    {
        SceneManager.LoadScene(checkPoint);
    }

    public void OnClickExitGameHandler()
    {
        Application.Quit();
    }
}
