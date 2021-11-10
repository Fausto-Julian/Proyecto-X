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

    private bool _isGameRunning = true;
    private bool _stayMenu = false;


    private void Awake()
    {
        _isGameRunning = true;

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
        if (_stayMenu != true)
        {
            if (_isGameRunning)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    _isGameRunning = false;
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
        _isGameRunning = true;
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
        GameManager.inst.SaveData();
        SceneManager.LoadScene(0);
        panelPause.SetActive(false);
        SkillTreeManager.inst.DeletedManagerHandler();
        HudManager.inst.DeletedManagerHandler();
        GameManager.inst.DeletedManagerHandler();
        DeletedManagerHandler();
    }

    public void OnClickExitGameHandler()
    {
        Application.Quit();
    }

    public void IsGamePause()
    {
        _isGameRunning = false;
        Time.timeScale = 0;
    }

    public void IsGameRunning()
    {
        _isGameRunning = true;
        Time.timeScale = 1;
    }

    public bool CheckIsGameRunning()
    {
        return _isGameRunning;
    }

    public void DeletedManagerHandler()
    {
        Destroy(gameObject);
    }
}
