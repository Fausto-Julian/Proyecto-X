using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManagerScript : MonoBehaviour
{
    [SerializeField] private Sprite[] spriteDoor;

    [SerializeField] private int DoorUpDown;
    [SerializeField] private int DoorRightLeft;
    [SerializeField] private bool openDoor;

    private SpriteRenderer spriteRenderer;

    private Camera cam;

    private GameObject otherRoom;

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
            otherRoom = collision.transform.parent.transform.parent.gameObject;
        }

        if (openDoor == true)
        {
            if (collision.CompareTag("Player"))
            {
                collision.transform.position = new Vector2(transform.position.x + 3f * DoorRightLeft, transform.position.y + 3f * DoorUpDown);
                cam.transform.position = new Vector3(otherRoom.transform.position.x, otherRoom.transform.position.y, -10);
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
}
