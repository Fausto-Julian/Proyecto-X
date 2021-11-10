using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationRoomsScipt : MonoBehaviour
{
    [SerializeField] private bool finalLevel;
    [SerializeField] private bool levelBoos;
    [SerializeField] private GameObject[] rooms;
    [SerializeField] private GameObject roomFinal, roomBoos;

    private bool _generateUp = true;
    private bool _generateSides = true;
    private bool _levelBoosLastRoom = false;
    private bool _levelLastRoom = false;

    [SerializeField] private int maxRooms;
    private int _currentRooms = 0;

    private Vector2 _pos1;
    private Vector2 _pos2;

    private void Update()
    {
        if (_currentRooms <= maxRooms)
        {
            GeneratorUp();
            GeneratorDown();
            _currentRooms = _currentRooms + 2;

            if (levelBoos)
            {
                if (_currentRooms >= maxRooms - 1)
                {
                    _levelBoosLastRoom = true;
                }
            }
            else
            {
                if (_currentRooms >= maxRooms - 1)
                {
                    _levelLastRoom = true;
                }
            }
        }
    }

    private void GeneratorUp()
    {
        _generateUp = Random.value > 0.5;
        _generateSides = !_generateUp;
        if (_generateUp)
        {
            _pos1 = new Vector2(_pos1.x, _pos1.y + 19.99944f);

            _generateUp = false;
            _generateSides = true;
        }
        else if (_generateSides)
        {
            _pos1 = new Vector2(_pos1.x + 35.55456f, _pos1.y);

            _generateSides = false;
            _generateUp = true;
        }
        if (_levelBoosLastRoom)
        {
            Instantiate(roomBoos, _pos1, transform.rotation);
        }
        else
        {
            var i = Random.Range(0, rooms.Length);
            Instantiate(rooms[i], _pos1, transform.rotation);
        }
    }

    private void GeneratorDown()
    {
        _generateUp = Random.value > 0.5;
        _generateSides = !_generateUp;
        if (_generateUp)
        {
            _pos2 = new Vector2(_pos2.x, _pos2.y - 19.99944f);

            _generateUp = false;
            _generateSides = true;
        }
        else if (_generateSides)
        {
            _pos2 = new Vector2(_pos2.x - 35.55456f, _pos2.y);

            _generateSides = false;
            _generateUp = true;
        }

        if (_levelBoosLastRoom || _levelLastRoom)
        {
            if (finalLevel)
            {
                var room = Instantiate(roomFinal, _pos2, transform.rotation);
                room.GetComponentInChildren<WinZone>().ChangeLevelFinal(finalLevel);
            }
            else
            {
                Instantiate(roomFinal, _pos2, transform.rotation);
            }
        }
        else
        {
            var i = Random.Range(0, rooms.Length);
            Instantiate(rooms[i], _pos2, transform.rotation);
        }
    }
}
