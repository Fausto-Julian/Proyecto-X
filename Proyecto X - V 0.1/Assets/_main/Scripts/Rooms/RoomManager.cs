using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [Header("Enemys")]
    [SerializeField] private bool bossRoom;
    [SerializeField, Range(2, 4)] private int enemysRoom;
    [SerializeField] private GameObject enemyBossPrefab;
    [SerializeField] private List<GameObject> enemysPrefabs = new List<GameObject>();
    [SerializeField] private List<Transform> spawners = new List<Transform>();
    private int countEnemys = 0;

    [Header("Door")]
    [SerializeField] private DoorManagerScript doorUp;
    [SerializeField] private DoorManagerScript doorDown;
    [SerializeField] private DoorManagerScript doorLeft;
    [SerializeField] private DoorManagerScript doorRight;

    private bool x = false;
    private float time = 0;

    private List<GameObject> enemys = new List<GameObject>();
    private GameObject enemyBoss;

    private void Awake()
    {
        doorUp.OpenDoor();
        doorDown.OpenDoor();
        doorLeft.OpenDoor();
        doorRight.OpenDoor();

        if (bossRoom)
        {
            var i = Random.Range(0, spawners.Count);
            enemyBoss = Instantiate(enemyBossPrefab, spawners[i].position, spawners[i].rotation);
            enemyBoss.SetActive(false);
        }
        else
        {
            switch (enemysRoom)
            {
                case 2:
                    var indexSpawn2 = Random.Range(0, spawners.Count);
                    var indexSpawn3 = Random.Range(0, spawners.Count);
                    while (indexSpawn2 == indexSpawn3)
                    {
                        indexSpawn3 = Random.Range(0, spawners.Count);
                    }
                    var indexEnemy2 = Random.Range(0, enemysPrefabs.Count);
                    var indexEnemy3 = Random.Range(0, enemysPrefabs.Count);
                    enemys.Add(Instantiate(enemysPrefabs[indexEnemy2], spawners[indexSpawn2].position, spawners[indexSpawn2].rotation));
                    enemys.Add(Instantiate(enemysPrefabs[indexEnemy3], spawners[indexSpawn3].position, spawners[indexSpawn3].rotation));
                    break;
                case 3:
                    var indexSpawn4 = Random.Range(0, spawners.Count);
                    var indexSpawn5 = Random.Range(0, spawners.Count);
                    while (indexSpawn4 == indexSpawn5)
                    {
                        indexSpawn5 = Random.Range(0, spawners.Count);
                    }
                    var indexSpawn6 = Random.Range(0, spawners.Count);
                    while (indexSpawn6 == indexSpawn4 || indexSpawn6 == indexSpawn5)
                    {
                        indexSpawn6 = Random.Range(0, spawners.Count);
                    }
                    var indexEnemy4 = Random.Range(0, enemysPrefabs.Count);
                    var indexEnemy5 = Random.Range(0, enemysPrefabs.Count);
                    var indexEnemy6 = Random.Range(0, enemysPrefabs.Count);
                    enemys.Add(Instantiate(enemysPrefabs[indexEnemy4], spawners[indexSpawn4].position, spawners[indexSpawn4].rotation));
                    enemys.Add(Instantiate(enemysPrefabs[indexEnemy5], spawners[indexSpawn5].position, spawners[indexSpawn5].rotation));
                    enemys.Add(Instantiate(enemysPrefabs[indexEnemy6], spawners[indexSpawn6].position, spawners[indexSpawn6].rotation));
                    break;
                case 4:
                    var indexSpawn7 = Random.Range(0, spawners.Count);
                    var indexSpawn8 = Random.Range(0, spawners.Count);
                    while (indexSpawn7 == indexSpawn8)
                    {
                        indexSpawn8 = Random.Range(0, spawners.Count);
                    }
                    var indexSpawn9 = Random.Range(0, spawners.Count);
                    while (indexSpawn9 == indexSpawn7 || indexSpawn9 == indexSpawn8)
                    {
                        indexSpawn9 = Random.Range(0, spawners.Count);
                    }
                    var indexSpawn10 = Random.Range(0, spawners.Count);
                    while (indexSpawn10 == indexSpawn7 || indexSpawn10 == indexSpawn8 || indexSpawn10 == indexSpawn9)
                    {
                        indexSpawn10 = Random.Range(0, spawners.Count);
                    }
                    var indexEnemy7 = Random.Range(0, enemysPrefabs.Count);
                    var indexEnemy8 = Random.Range(0, enemysPrefabs.Count);
                    var indexEnemy9 = Random.Range(0, enemysPrefabs.Count);
                    var indexEnemy10 = Random.Range(0, enemysPrefabs.Count);
                    enemys.Add(Instantiate(enemysPrefabs[indexEnemy7], spawners[indexSpawn7].position, spawners[indexSpawn7].rotation));
                    enemys.Add(Instantiate(enemysPrefabs[indexEnemy8], spawners[indexSpawn8].position, spawners[indexSpawn8].rotation));
                    enemys.Add(Instantiate(enemysPrefabs[indexEnemy9], spawners[indexSpawn9].position, spawners[indexSpawn9].rotation));
                    enemys.Add(Instantiate(enemysPrefabs[indexEnemy10], spawners[indexSpawn10].position, spawners[indexSpawn10].rotation));
                    break;
            }

            foreach (GameObject item in enemys)
            {
                item.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            countEnemys += 1;
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
            countEnemys -= 1;
            if (countEnemys == 0)
            {
                doorUp.OpenDoor();
                doorDown.OpenDoor();
                doorLeft.OpenDoor();
                doorRight.OpenDoor();

                enemys.Clear();
            }
        }
    }

    public void ActiveEnemys()
    {
        if (bossRoom)
        {
            enemyBoss.SetActive(true);
        }
        else
        {
            foreach (GameObject item in enemys)
            {
                item.SetActive(true);
            }
        }
    }
}
