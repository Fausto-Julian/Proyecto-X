using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationRoomsScipt : MonoBehaviour
{
    [SerializeField] private GameObject[] rooms;
    [SerializeField] private GameObject roomFinal, roomBoos;

    private Vector2 pos1;
    private Vector2 pos2;

    [SerializeField] private bool levelBoos;
    private bool generateUp = true;
    private bool generateSides = true;
    private bool levelBoosLastRoom = false;
    private bool levelLastRoom = false;

    [SerializeField] private int maxRooms;
    private int currentRooms = 0;

    private void Update()
    {
        if (currentRooms <= maxRooms)
        {
            GeneratorUp();
            GeneratorDown();
            currentRooms = currentRooms + 2;

            if (levelBoos)
            {
                if (currentRooms >= maxRooms - 1)
                {
                    levelBoosLastRoom = true;
                }
            }
            else
            {
                if (currentRooms >= maxRooms - 1)
                {
                    levelLastRoom = true;
                }
            }
        }
    }

    private void GeneratorUp()
    {
        generateUp = Random.value > 0.5;
        generateSides = !generateUp;
        if (generateUp)
        {
            pos1 = new Vector2(pos1.x, pos1.y + 10);

            generateUp = false;
            generateSides = true;
        }
        else if (generateSides)
        {
            pos1 = new Vector2(pos1.x + 17.78f, pos1.y);

            generateSides = false;
            generateUp = true;
        }
        if (levelBoosLastRoom)
        {
            Instantiate(roomBoos, pos1, transform.rotation);
        }
        else
        {
            var i = Random.Range(0, rooms.Length);
            Instantiate(rooms[i], pos1, transform.rotation);
        }
    }

    private void GeneratorDown()
    {
        generateUp = Random.value > 0.5;
        generateSides = !generateUp;
        if (generateUp)
        {
            pos2 = new Vector2(pos2.x, pos2.y - 10);

            generateUp = false;
            generateSides = true;
        }
        else if (generateSides)
        {
            pos2 = new Vector2(pos2.x - 17.78f, pos2.y);

            generateSides = false;
            generateUp = true;
        }

        if (levelBoosLastRoom || levelLastRoom)
        {
            Instantiate(roomFinal, pos2, transform.rotation);
        }
        else
        {
            var i = Random.Range(0, rooms.Length);
            Instantiate(rooms[i], pos2, transform.rotation);
        }
    }
}
