using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int enemies;

    [SerializeField] private GameObject zoneFinal;
    [SerializeField] private GameObject wallQuit;

    private void Update()
    {
        if (enemies <= 0)
        {
            zoneFinal.SetActive(true);
            wallQuit.SetActive(false);
        }
    }

    public void RestEnemies()
    {
        enemies--;
    }
}
