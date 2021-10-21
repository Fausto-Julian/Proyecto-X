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

    private SpriteRenderer spriteRenderer;
    private Camera cam;

    private GameObject otherRoom;
    private GameObject otherDoorIsBoss;

    private bool firstDoorBoss = false;
    private float time = 0;

    private RoomManager otherRoomManager;

    [SerializeField] PlayerController playerController;

    private void Awake()
    {
        cam = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (openDoor == true)
        {
            spriteRenderer.sprite = spriteDoor[1];
        }
        else 
        {
            spriteRenderer.sprite = spriteDoor[0];
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            otherRoomManager = collision.transform.parent.transform.parent.gameObject.GetComponent<RoomManager>();
            otherRoom = collision.transform.parent.transform.parent.transform.parent.gameObject;
        }

        if (collision.CompareTag("DoorBoss"))
        {
            otherRoomManager = collision.transform.parent.transform.parent.gameObject.GetComponent<RoomManager>();
            otherRoom = collision.transform.parent.transform.parent.transform.parent.gameObject;
            otherDoorIsBoss = collision.gameObject;
        }

        if (openDoor == true)
        {
            if (collision.CompareTag("Player"))
            {
                if (otherDoorIsBoss != null && !firstDoorBoss)
                {
                    roomBossText.SetActive(true);

                    playerController = collision.GetComponent<PlayerController>();
                    playerController.PausePlayer();

                    collision.transform.position = new Vector2(transform.position.x + 3.5f * DoorRightLeft, transform.position.y + 3.5f * DoorUpDown);
                    cam.transform.position = new Vector3(otherRoom.transform.position.x, otherRoom.transform.position.y, -10);

                    Invoke("ActiveRoomBoss", 5f);
                }
                else
                {
                    otherRoomManager.GetComponent<RoomManager>().ActiveEnemys();
                    collision.transform.position = new Vector2(transform.position.x + 3.5f * DoorRightLeft, transform.position.y + 3.5f * DoorUpDown);
                    cam.transform.position = new Vector3(otherRoom.transform.position.x, otherRoom.transform.position.y, -10);
                }
            }
        }

        StartCoroutine("Check");
    }

    IEnumerator Check()
    {
        yield return new WaitForSeconds(0.2f);
        if (otherRoom == null)
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
        playerController.ResumePlayer();
        otherRoomManager.GetComponent<RoomManager>().ActiveEnemys();
    }

}
