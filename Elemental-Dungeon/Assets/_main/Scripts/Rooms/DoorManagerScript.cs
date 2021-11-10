using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManagerScript : MonoBehaviour
{
    [SerializeField] private Sprite[] spriteDoor;
    [SerializeField] private int DoorUpDown;
    [SerializeField] private int DoorRightLeft;
    [SerializeField] private bool openDoor;

    [SerializeField] private GameObject roomBossText;

    private SpriteRenderer _spriteRenderer;
    private Camera _cam;

    private GameObject _otherRoom;
    private GameObject _otherDoorIsBoss;

    private bool _firstDoorBoss = false;

    private RoomManager _otherRoomManager;

    PlayerController _playerController;

    private void Awake()
    {
        _cam = Camera.main;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (openDoor == true)
        {
            _spriteRenderer.sprite = spriteDoor[1];
        }
        else 
        {
            _spriteRenderer.sprite = spriteDoor[0];
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            _otherRoomManager = collision.transform.parent.transform.parent.gameObject.GetComponent<RoomManager>();
            _otherRoom = collision.transform.parent.transform.parent.transform.parent.gameObject;
        }

        if (collision.CompareTag("DoorBoss"))
        {
            _otherRoomManager = collision.transform.parent.transform.parent.gameObject.GetComponent<RoomManager>();
            _otherRoom = collision.transform.parent.transform.parent.transform.parent.gameObject;
            _otherDoorIsBoss = collision.gameObject;
        }

        if (openDoor == true)
        {
            if (collision.CompareTag("Player"))
            {
                if (_otherDoorIsBoss != null)
                {
                    if (!_firstDoorBoss)
                    {
                        roomBossText.SetActive(true);

                        _playerController = collision.GetComponent<PlayerController>();
                        _playerController.PausePlayer();
                        collision.transform.position = new Vector2(transform.position.x + 7f * DoorRightLeft, transform.position.y + 7f * DoorUpDown);
                        _cam.transform.position = new Vector3(_otherRoom.transform.position.x, _otherRoom.transform.position.y, -10);
                        _firstDoorBoss = true;
                        Invoke("ActiveRoomBoss", 5f);
                    }
                    else
                    {
                        collision.transform.position = new Vector2(transform.position.x + 7f * DoorRightLeft, transform.position.y + 7f * DoorUpDown);
                        _cam.transform.position = new Vector3(_otherRoom.transform.position.x, _otherRoom.transform.position.y, -10);
                    }
                }
                else
                {
                    _otherRoomManager.GetComponent<RoomManager>().ActiveEnemys();
                    collision.transform.position = new Vector2(transform.position.x + 7f * DoorRightLeft, transform.position.y + 7f * DoorUpDown);
                    _cam.transform.position = new Vector3(_otherRoom.transform.position.x, _otherRoom.transform.position.y, -10);
                }
            }
        }

        StartCoroutine("Check");
    }

    IEnumerator Check()
    {
        yield return new WaitForSeconds(0.2f);
        if (_otherRoom == null)
        {
            Destroy(gameObject);
        }
    }

    public void OpenDoor()
    {
        openDoor = true;
    }

    public void CloseDoor()
    {
        openDoor = false;
    }

    private void ActiveRoomBoss()
    {
        roomBossText.SetActive(false);
        _playerController.ResumePlayer();
        _otherRoomManager.GetComponent<RoomManager>().ActiveEnemys();
    }

}
