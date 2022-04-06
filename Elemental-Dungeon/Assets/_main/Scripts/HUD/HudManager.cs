using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    public static HudManager inst;

    [SerializeField] private Image lifeBar;
    [SerializeField] private Text diamondCountText;
    [SerializeField] private Text lifeCountText;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private HealthController playerHealthController;

    private void Awake()
    {
        if (inst == null)
        {
            inst = this;
        }
        else
        {
            Destroy(gameObject);
        }

        gameManager.OnChangeDiamond += DiamondChangeHandler;
    }

    private void Update()
    {
        if (playerHealthController != null)
        {
            var maxHealth = playerHealthController.GetDefaultHealth();
            var currentHealth = playerHealthController.GetCurrentHealth();
            float health = currentHealth / maxHealth;

            lifeBar.fillAmount = health;
        }
    }

    private void DiamondChangeHandler(int diamond)
    {
        diamondCountText.text = diamond.ToString();
    }

    public void LoadHud(int diamondCount, int life, float currentHealth)
    {
        lifeCountText.text = life.ToString();
        diamondCountText.text = diamondCount.ToString();
        lifeBar.fillAmount = currentHealth / playerHealthController.GetDefaultHealth();
    }

    public void DeletedManagerHandler()
    {
        Destroy(gameObject);
    }
}
