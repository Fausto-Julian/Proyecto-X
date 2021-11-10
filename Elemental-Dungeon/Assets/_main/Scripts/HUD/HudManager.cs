using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    public static HudManager inst;

    [SerializeField] private Image lifeBar;
    [SerializeField] private Text diamondCountText;
    private HealthController _playerHealthController;
    private GameManager _gameManager;

    private void Awake()
    {
        if (inst == null)
        {
            inst = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _gameManager.diamondAction += DiamondChangeHandler;
    }

    private void Start()
    {
        _playerHealthController = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthController>();

        var maxHealth = _playerHealthController.GetDefaultHealth();
        var currentHealth = _playerHealthController.GetCurrentHealth();
        float health = currentHealth / maxHealth;

        lifeBar.fillAmount = health;

        diamondCountText.text = GameManager.inst.GetDiamondPoint().ToString();
    }

    private void Update()
    {
        var maxHealth = _playerHealthController.GetDefaultHealth();
        var currentHealth = _playerHealthController.GetCurrentHealth();
        float health = currentHealth / maxHealth;

        lifeBar.fillAmount = health;
    }

    private void DiamondChangeHandler(int diamond)
    {
        diamondCountText.text = diamond.ToString();
    }

    public void DeletedManagerHandler()
    {
        Destroy(gameObject);
    }
}
