using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;

    private bool FirstExecutionLife = true;

    private int playerCurrentLife;
    private bool lifePlayer = true;

    private int diamondPoints = 0;

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
    }

    public bool CheckFirstExecutionLife()
    {
        if (FirstExecutionLife)
        {
            FirstExecutionLife = false;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetPlayerCurrentLife(int currentLife)
    {
        playerCurrentLife = currentLife;
    }

    public int LoadPlayerCurrentLife()
    {
        return playerCurrentLife;
    }

    private void checkLife(int currentLife)
    {
        playerCurrentLife = currentLife;

        if (playerCurrentLife <= 0)
        {
            lifePlayer = false;
        }
    }

    public void OnCollectionDiamondHandler(int diamondCount)
    {
        diamondPoints += diamondCount;
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public int CheckDiamondPoint()
    {
        return diamondPoints;
    }

    public void subtractDiamond(int count)
    {
        diamondPoints -= count;
    }
}
