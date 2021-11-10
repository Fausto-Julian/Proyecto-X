
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [Header("Enemys")]
    [SerializeField] private bool roomDefault;
    [SerializeField] private bool bossRoom;
    [SerializeField, Range(2, 4)] private int enemysRoom;
    [SerializeField] private GameObject enemyBossPrefab;
    [SerializeField] private List<GameObject> enemysPrefabs = new List<GameObject>();
    [SerializeField] private List<Transform> spawners = new List<Transform>();
    [SerializeField] private List<Transform> spawnersWall = new List<Transform>();
    private int _countEnemys = 0;

    [Header("Door")]
    [SerializeField] private DoorManagerScript doorUp;
    [SerializeField] private DoorManagerScript doorDown;
    [SerializeField] private DoorManagerScript doorLeft;
    [SerializeField] private DoorManagerScript doorRight;

    private List<GameObject> _enemys = new List<GameObject>();
    private GameObject _enemyBoss;

    private void Awake()
    {
        doorUp.OpenDoor();
        doorDown.OpenDoor();
        doorLeft.OpenDoor();
        doorRight.OpenDoor();
        if (roomDefault)
        {
            return;
        }
        else if (bossRoom)
        {
            GameManager.inst.AddEnemyCount(1);
            var i = Random.Range(0, spawners.Count);
            _enemyBoss = Instantiate(enemyBossPrefab, spawners[i].position, spawners[i].rotation);
            _enemyBoss.SetActive(false);
        }
        else
        {
            switch (enemysRoom)
            {
                case 2:
                    var indexSpawn1 = Random.Range(0, spawners.Count);
                    var indexSpawn2 = Random.Range(0, spawners.Count);
                    while (indexSpawn1 == indexSpawn2)
                    {
                        indexSpawn2 = Random.Range(0, spawners.Count);
                    }
                    var indexEnemy1 = Random.Range(0, enemysPrefabs.Count);
                    var indexEnemy2 = Random.Range(0, enemysPrefabs.Count);
                    _enemys.Add(Instantiate(enemysPrefabs[indexEnemy1], spawners[indexSpawn1].position, spawners[indexSpawn1].rotation));
                    _enemys.Add(Instantiate(enemysPrefabs[indexEnemy2], spawners[indexSpawn2].position, spawners[indexSpawn2].rotation));
                    GameManager.inst.AddEnemyCount(2);
                    break;
                case 3:
                    var indexEnemy3 = Random.Range(0, enemysPrefabs.Count);
                    var indexEnemy4 = Random.Range(0, enemysPrefabs.Count);
                    var indexEnemy5 = Random.Range(0, enemysPrefabs.Count);

                    var indexSpawn3 = Random.Range(0, spawners.Count);
                    var indexSpawn4 = Random.Range(0, spawners.Count);
                    while (indexSpawn3 == indexSpawn4)
                    {
                        indexSpawn4 = Random.Range(0, spawners.Count);
                    }
                    var indexSpawn5 = Random.Range(0, spawners.Count);
                    while (indexSpawn5 == indexSpawn3 || indexSpawn5 == indexSpawn4)
                    {
                        indexSpawn5 = Random.Range(0, spawners.Count);
                    }

                    var indexSpawnWall3 = Random.Range(0, spawnersWall.Count);
                    var indexSpawnWall4 = Random.Range(0, spawnersWall.Count);
                    while (indexSpawnWall3 == indexSpawnWall4)
                    {
                        indexSpawnWall4 = Random.Range(0, spawnersWall.Count);
                    }
                    var indexSpawnWall5 = Random.Range(0, spawnersWall.Count);
                    while (indexSpawnWall5 == indexSpawnWall3 || indexSpawnWall5 == indexSpawnWall4)
                    {
                        indexSpawnWall5 = Random.Range(0, spawnersWall.Count);
                    }

                    if (indexEnemy3 == 0 || indexEnemy4 == 0 || indexEnemy5 == 0)
                    {
                        if (indexEnemy3 == 0)
                        {
                            _enemys.Add(Instantiate(enemysPrefabs[indexEnemy3], spawnersWall[indexSpawnWall3].position, spawnersWall[indexSpawnWall3].rotation));
                        }
                        else
                        {
                            _enemys.Add(Instantiate(enemysPrefabs[indexEnemy3], spawners[indexSpawn3].position, spawners[indexSpawn3].rotation));
                        }
                        if (indexEnemy4 == 0)
                        {
                            _enemys.Add(Instantiate(enemysPrefabs[indexEnemy4], spawnersWall[indexSpawnWall4].position, spawnersWall[indexSpawnWall4].rotation));
                        }
                        else
                        {
                            _enemys.Add(Instantiate(enemysPrefabs[indexEnemy4], spawners[indexSpawn4].position, spawners[indexSpawn3].rotation));
                        }
                        if (indexEnemy5 == 0)
                        {
                            _enemys.Add(Instantiate(enemysPrefabs[indexEnemy5], spawnersWall[indexSpawnWall5].position, spawnersWall[indexSpawnWall5].rotation));
                        }
                        else
                        {
                            _enemys.Add(Instantiate(enemysPrefabs[indexEnemy5], spawners[indexSpawn5].position, spawners[indexSpawn3].rotation));
                        }
                    }
                    else
                    {
                        _enemys.Add(Instantiate(enemysPrefabs[indexEnemy3], spawners[indexSpawn3].position, spawners[indexSpawn3].rotation));
                        _enemys.Add(Instantiate(enemysPrefabs[indexEnemy4], spawners[indexSpawn4].position, spawners[indexSpawn4].rotation));
                        _enemys.Add(Instantiate(enemysPrefabs[indexEnemy5], spawners[indexSpawn5].position, spawners[indexSpawn5].rotation));
                    }
                    GameManager.inst.AddEnemyCount(3);
                    break;
                case 4:
                    var indexEnemy6 = Random.Range(0, enemysPrefabs.Count);
                    var indexEnemy7 = Random.Range(0, enemysPrefabs.Count);
                    var indexEnemy8 = Random.Range(0, enemysPrefabs.Count);
                    var indexEnemy9 = Random.Range(0, enemysPrefabs.Count);

                    var indexSpawn6 = Random.Range(0, spawners.Count);
                    var indexSpawn7 = Random.Range(0, spawners.Count);
                    while (indexSpawn6 == indexSpawn7)
                    {
                        indexSpawn7 = Random.Range(0, spawners.Count);
                    }
                    var indexSpawn8 = Random.Range(0, spawners.Count);
                    while (indexSpawn8 == indexSpawn6 || indexSpawn8 == indexSpawn7)
                    {
                        indexSpawn8 = Random.Range(0, spawners.Count);
                    }
                    var indexSpawn9 = Random.Range(0, spawners.Count);
                    while (indexSpawn9 == indexSpawn6 || indexSpawn9 == indexSpawn7 || indexSpawn9 == indexSpawn8)
                    {
                        indexSpawn9 = Random.Range(0, spawners.Count);
                    }

                    var indexSpawnWall6 = Random.Range(0, spawnersWall.Count);
                    var indexSpawnWall7 = Random.Range(0, spawnersWall.Count);
                    while (indexSpawnWall6 == indexSpawnWall7)
                    {
                        indexSpawnWall7 = Random.Range(0, spawnersWall.Count);
                    }
                    var indexSpawnWall8 = Random.Range(0, spawnersWall.Count);
                    while (indexSpawnWall8 == indexSpawn6 || indexSpawnWall8 == indexSpawn7)
                    {
                        indexSpawnWall8 = Random.Range(0, spawnersWall.Count);
                    }
                    var indexSpawnWall9 = Random.Range(0, spawnersWall.Count);
                    while (indexSpawnWall9 == indexSpawnWall6 || indexSpawnWall9 == indexSpawnWall7 || indexSpawnWall9 == indexSpawnWall8)
                    {
                        indexSpawnWall9 = Random.Range(0, spawnersWall.Count);
                    }

                    if (indexEnemy6 == 0 || indexEnemy7 == 0 || indexEnemy8 == 0 || indexEnemy9 == 0)
                    {
                        if (indexEnemy6 == 0)
                        {
                            _enemys.Add(Instantiate(enemysPrefabs[indexEnemy6], spawnersWall[indexSpawnWall6].position, spawnersWall[indexSpawnWall6].rotation));
                        }
                        else
                        {
                            _enemys.Add(Instantiate(enemysPrefabs[indexEnemy6], spawners[indexSpawn6].position, spawners[indexSpawn6].rotation));

                        }
                        if (indexEnemy7 == 0)
                        {
                            _enemys.Add(Instantiate(enemysPrefabs[indexEnemy7], spawnersWall[indexSpawnWall7].position, spawnersWall[indexSpawnWall7].rotation));
                        }
                        else
                        {
                            _enemys.Add(Instantiate(enemysPrefabs[indexEnemy7], spawners[indexSpawn7].position, spawners[indexSpawn7].rotation));
                        }
                        if (indexEnemy8 == 0)
                        {
                            _enemys.Add(Instantiate(enemysPrefabs[indexEnemy8], spawnersWall[indexSpawnWall8].position, spawnersWall[indexSpawnWall8].rotation));
                        }
                        else
                        {
                            _enemys.Add(Instantiate(enemysPrefabs[indexEnemy8], spawners[indexSpawn8].position, spawners[indexSpawn8].rotation));
                        }
                        if (indexEnemy9 == 0)
                        {
                            _enemys.Add(Instantiate(enemysPrefabs[indexEnemy9], spawnersWall[indexSpawnWall9].position, spawnersWall[indexSpawnWall9].rotation));
                        }
                        else
                        {
                            _enemys.Add(Instantiate(enemysPrefabs[indexEnemy9], spawners[indexSpawn9].position, spawners[indexSpawn9].rotation));
                        }
                    }
                    else
                    {
                        _enemys.Add(Instantiate(enemysPrefabs[indexEnemy6], spawners[indexSpawn6].position, spawners[indexSpawn6].rotation));
                        _enemys.Add(Instantiate(enemysPrefabs[indexEnemy7], spawners[indexSpawn7].position, spawners[indexSpawn7].rotation));
                        _enemys.Add(Instantiate(enemysPrefabs[indexEnemy8], spawners[indexSpawn8].position, spawners[indexSpawn8].rotation));
                        _enemys.Add(Instantiate(enemysPrefabs[indexEnemy9], spawners[indexSpawn9].position, spawners[indexSpawn9].rotation));
                    }
                    GameManager.inst.AddEnemyCount(4);
                    break;
            }

            for (int i = 0; i < _enemys.Count; i++)
            {
                _enemys[i].SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            _countEnemys += 1;
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
            _countEnemys -= 1;
            GameManager.inst.RemoveEnemy();
            if (_countEnemys == 0)
            {
                doorUp.OpenDoor();
                doorDown.OpenDoor();
                doorLeft.OpenDoor();
                doorRight.OpenDoor();

                _enemys.Clear();
            }
        }
    }

    public void ActiveEnemys()
    {
        if (bossRoom)
        {
            _enemyBoss.SetActive(true);
        }
        else
        {
            for (int i = 0; i < _enemys.Count; i++)
            {
                _enemys[i].SetActive(true);
            }
        }
    }
}
