using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum bulletIndex
{
    fire,
    water,
    rock,
    wind
}

public class AbilityPlayerController : MonoBehaviour
{
    [SerializeField] private List<GameObject> bullets;
    [SerializeField] private List<Sprite> bulletsSprites;
    [SerializeField] private Animator anim;
    [SerializeField] private Transform spawnPointAtack1;
    private bulletIndex indexBullet = new bulletIndex();
    private Image _imageHUD;

    private void Start()
    {
        _imageHUD = GameObject.FindGameObjectWithTag("BulletIcon").GetComponent<Image>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Instantiate(bullets[((int)indexBullet)], spawnPointAtack1.position, spawnPointAtack1.rotation);
            anim.SetBool("DownCast", true);
            Invoke("falseDownCast", 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            indexBullet = bulletIndex.fire;
            if (_imageHUD != null)
            {
                _imageHUD.sprite = bulletsSprites[((int)indexBullet)];
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            indexBullet = bulletIndex.water;
            if (_imageHUD != null)
            {
                _imageHUD.sprite = bulletsSprites[((int)indexBullet)];
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            indexBullet = bulletIndex.rock;
            if (_imageHUD != null)
            {
                _imageHUD.sprite = bulletsSprites[((int)indexBullet)];
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            indexBullet = bulletIndex.wind;
            if (_imageHUD != null)
            {
                _imageHUD.sprite = bulletsSprites[((int)indexBullet)];
            }
        }
    }

    private void falseDownCast()
    {
        anim.SetBool("DownCast", false);
    }
}
