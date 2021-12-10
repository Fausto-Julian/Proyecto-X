using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinZone : MonoBehaviour
{
    [SerializeField] private bool levelFinal;
    [SerializeField] private Sprite openDoor;
    [SerializeField] private Sprite closeDoor;

    private SpriteRenderer _spriteRenderer;
    private bool _open;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        //_spriteRenderer.sprite = closeDoor;
        _open = false;
    }

    private void Update()
    {
        if (GameManager.inst.GetEnemyCount() == 0)
        {
            //_spriteRenderer.sprite = openDoor;
            _spriteRenderer.color = Color.blue;
            _open = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (levelFinal)
        {
            if (collision.gameObject.CompareTag("Player") && _open)
                SceneManager.LoadScene("WinScene");
        }
        else
        {
            if (collision.gameObject.CompareTag("Player") && _open)
            {
                GameManager.inst.NextLevel();
                GameManager.inst.SetPlayerHealthController();
            }
        }
    }

    public void ChangeLevelFinal(bool final)
    {
        levelFinal = final;
    }
}
