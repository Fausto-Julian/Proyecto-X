using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthController))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private Animator anim;
    [SerializeField] private Transform center;
    [SerializeField] private GameObject melee;

    [SerializeField] private AudioClip espada;
    private AudioSource _audioSouce;

    private Rigidbody2D _body;
    private Vector2 _movement;
    private float _movementX;
    private float _movementY;

    private HealthController _healthController;

    private bool pausePlayer = false;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _audioSouce = GetComponent<AudioSource>();
        _healthController = GetComponent<HealthController>();
    }
    private void Start()
    {
        var gameState = SaveLoadSystemData.LoadData<GameState>("Game", "GameState");

        switch (gameState)
        {
            case GameState.newGame:
                _healthController.SetDefaultHealth();
                GameManager.inst.SetPlayerCurrentLife(_healthController.GetCurrentHealth());
                break;
            case GameState.game:
                // solucionar
                //var playerData = SaveLoadSystemData.LoadData<PlayerData>("Player", "PlayerData");
                //_healthController.SetHealth(playerData.currenHealthPlayer);
                _healthController.SetDefaultHealth();
                break;
            default:
                break;
        }
    }
    
    private void Update()
    {
        if (PauseGameManager.inst.CheckIsGameRunning() && pausePlayer != true)
        {
            SetAnimations();

            InputManager();
        }
    }
    private void FixedUpdate()
    {
        if (PauseGameManager.inst.CheckIsGameRunning())
        {
            // solucionar
            //var desiredSpeed = _movement * speed;
            //var difVel = desiredSpeed - _body.velocity;

            var force = _movement * speed;

            _body.velocity = force;
        }
    }

    public void PausePlayer()
    {
        pausePlayer = true;
    }
    public void ResumePlayer()
    {
        pausePlayer = false;
    }

    private void InputManager()
    {
        _movementX = Input.GetAxisRaw("Horizontal");
        _movementY = Input.GetAxisRaw("Vertical");
        _movement = new Vector2(_movementX, _movementY);

        if (Input.GetKeyDown(KeyCode.J))
        {
            melee.SetActive(true);
            anim.SetBool("DownSlash", true);
            _audioSouce.clip = espada;
            _audioSouce.Play();
            Invoke("falseMele", 0.5f);
            Invoke("falseDownSlash", 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SkillTreeManager.inst.ActivePanel();
        }
    }

    private void SetAnimations()
    {
        anim.SetFloat("Movement X", _movementX);
        anim.SetFloat("Movement Y", _movementY);
        anim.SetFloat("Speed", _movement.sqrMagnitude);


        if (_movementY == 1f && _movementX == 1f)
        {
            center.rotation = Quaternion.Euler(new Vector3(0f, 0f, 125f));
        }
        else if (_movementY == -1f && _movementX == 1f)
        {
            center.rotation = Quaternion.Euler(new Vector3(0f, 0f, 45f));
        }
        else if (_movementY == 1f && _movementX == -1f)
        {
            center.rotation = Quaternion.Euler(new Vector3(0f, 0f, 225f));
        }
        else if (_movementY == -1f && _movementX == -1f)
        {
            center.rotation = Quaternion.Euler(new Vector3(0f, 0f, 315f));
        }
        else if (_movementY == 1f && _movementX == 0f)
        {
            center.rotation = Quaternion.Euler(new Vector3(0f, 0f, 180f));
            anim.SetFloat("RotateX", 0f);
            anim.SetFloat("RotateY", 1f);
        }
        else if (_movementY == -1f && _movementX == 0f)
        {
            center.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
            anim.SetFloat("RotateX", 0f);
            anim.SetFloat("RotateY", -1f);
        }
        else if (_movementX == 1f && _movementY == 0f)
        {
            center.rotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
            anim.SetFloat("RotateX", 1f);
            anim.SetFloat("RotateY", 0f);
        }
        else if (_movementX == -1f && _movementY == 0f)
        {
            center.rotation = Quaternion.Euler(new Vector3(0f, 0f, 270f));
            anim.SetFloat("RotateX", -1f);
            anim.SetFloat("RotateY", 0f);
        }
    }

    private void falseDownSlash()
    {
        anim.SetBool("DownSlash", false);
    }
    private void falseMele()
    {
        melee.SetActive(false);
    }
}
