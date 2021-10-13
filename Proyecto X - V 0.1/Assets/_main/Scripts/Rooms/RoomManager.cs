using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{

    [SerializeField] private DoorManagerScript doorUp;
    [SerializeField] private DoorManagerScript doorDown;
    [SerializeField] private DoorManagerScript doorLeft;
    [SerializeField] private DoorManagerScript doorRight;

    private int enemys = 0;

    private void Awake()
    {
        doorUp.OpenDoor();
        doorDown.OpenDoor();
        doorLeft.OpenDoor();
        doorRight.OpenDoor();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemys += 1;
            doorUp.CloseDoor();
            doorDown.CloseDoor();
            doorLeft.CloseDoor();
            doorRight.CloseDoor();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemys -= 1;
            if (enemys == 0)
            {
                doorUp.OpenDoor();
                doorDown.OpenDoor();
                doorLeft.OpenDoor();
                doorRight.OpenDoor();
            }
        }

    }
}
