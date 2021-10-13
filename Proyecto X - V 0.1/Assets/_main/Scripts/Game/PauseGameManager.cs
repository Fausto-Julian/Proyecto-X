using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseGameManager : MonoBehaviour
{
    public static PauseGameManager inst;

    [SerializeField] private GameObject panelPause, panelExit;

    [SerializeField] private Button continueGame;
    [SerializeField] private Button backMenu;
    [SerializeField] private Button exitPanel;
    [SerializeField] private Button noExit;
    [SerializeField] private Button exitGame;

    private bool isGameRuning = true;
    private bool stayMenu = false;

    private void Awake()
    {
        isGameRuning = true;

        if (inst == null)
        {
            inst = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        continueGame.onClick.AddListener(OnClickContinueGameHandler);
        backMenu.onClick.AddListener(OnClickBackToMenuHandler);
        exitGame.onClick.AddListener(OnClickExitGameHandler);
        exitPanel.onClick.AddListener(OnClickExitPanelHandler);
        noExit.onClick.AddListener(OnClickNoExitPanelHandler);
    }

    private void Update()
    {
        if (stayMenu != true)
        {
            if (isGameRuning)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    isGameRuning = false;
                    /*
                    AudioSource[] audios = FindObjectsOfType<AudioSource>();
                    foreach (AudioSource a in audios)
                    {
                        a.Pause();
                    }
                    */
                    panelPause.SetActive(true);
                    Time.timeScale = 0;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    ContinueGame();
                }
            }
        }
    }

    private void ContinueGame()
    {
        isGameRuning = true;
        Time.timeScale = 1;
        panelPause.SetActive(false);
    }

    public void OnClickContinueGameHandler()
    {
        ContinueGame();
    }

    public void OnClickExitPanelHandler()
    {
        panelExit.SetActive(true);
    }

    public void OnClickNoExitPanelHandler()
    {
        panelExit.SetActive(false);
    }

    public void OnClickBackToMenuHandler()
    {
        SceneManager.LoadScene(0);
        panelPause.SetActive(false);
    }

    public void OnClickExitGameHandler()
    {
        Application.Quit();
    }

    public bool IsGameRuning()
    {
        return isGameRuning;
    }
}
