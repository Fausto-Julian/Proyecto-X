using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeManager : MonoBehaviour
{
    public static SkillTreeManager inst;

    [Header("Panel")]
    [SerializeField] private GameObject panel;
    [SerializeField] private Button buttonExitSkillTreeMenu;

    [Header("Prefabs Bullets")]
    [SerializeField] private GameObject bulletFirePrefab;


    #region LevelUp Fire
    [Header("LevelUp Fire")]
    [SerializeField] private int costDiamondLevelFire1 = 5;
    [SerializeField] private int costDiamondLevelFire2 = 5;
    [SerializeField] private int costDiamondLevelFire3 = 5;
    [SerializeField] private int costDiamondLevelFire4 = 5;

    [SerializeField] private Button buttonLevelFire1;
    [SerializeField] private Button buttonLevelFire2;
    [SerializeField] private Button buttonLevelFire3;
    [SerializeField] private Button buttonLevelFire4;

    private int levelFire = 0;
    private int levelDamageFire = 0;
    private int defaultDamageFire;
    #endregion

    #region LevelUp Water
    [Header("LevelUp Water")]
    [SerializeField] private int costDiamondLevelWater1 = 5;
    [SerializeField] private int costDiamondLevelWater2 = 5;
    [SerializeField] private int costDiamondLevelWater3 = 5;
    [SerializeField] private int costDiamondLevelWater4 = 5;

    [SerializeField] private Button buttonLevelWater1;
    [SerializeField] private Button buttonLevelWater2;
    [SerializeField] private Button buttonLevelWater3;
    [SerializeField] private Button buttonLevelWater4;

    private int levelWater = 0;
    private int levelDamageWater = 0;
    private int defaultDamageWater;
    #endregion

    #region LevelUp Rock
    [Header("LevelUp Rock")]
    [SerializeField] private int costDiamondLevelRock1 = 5;
    [SerializeField] private int costDiamondLevelRock2 = 5;
    [SerializeField] private int costDiamondLevelRock3 = 5;
    [SerializeField] private int costDiamondLevelRock4 = 5;

    [SerializeField] private Button buttonLevelRock1;
    [SerializeField] private Button buttonLevelRock2;
    [SerializeField] private Button buttonLevelRock3;
    [SerializeField] private Button buttonLevelRock4;

    private int levelRock = 0;
    private int levelDamageRock = 0;
    private int defaultDamageRock;
    #endregion

    #region LevelUp Wind
    [Header("LevelUp Wind")]
    [SerializeField] private int costDiamondLevelWind1 = 5;
    [SerializeField] private int costDiamondLevelWind2 = 5;
    [SerializeField] private int costDiamondLevelWind3 = 5;
    [SerializeField] private int costDiamondLevelWind4 = 5;

    [SerializeField] private Button buttonLevelWind1;
    [SerializeField] private Button buttonLevelWind2;
    [SerializeField] private Button buttonLevelWind3;
    [SerializeField] private Button buttonLevelWind4;


    private int levelWind = 0;
    private int levelDamageWind = 0;
    private int defaultDamageWind;
    #endregion

    private void Awake()
    {
        if(inst == null)
        {
            inst = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        defaultDamageFire = bulletFirePrefab.GetComponent<BulletPlayer>().DamageDefaultFire();

        buttonExitSkillTreeMenu.onClick.AddListener(DesactivePanel);

        #region LevelUp Fire
        buttonLevelFire1.onClick.AddListener(UpGradeFireHandler);
        buttonLevelFire2.onClick.AddListener(UpGradeFireHandler);
        buttonLevelFire3.onClick.AddListener(UpGradeFireHandler);
        buttonLevelFire4.onClick.AddListener(UpGradeFireHandler);
        #endregion

        #region LevelUp Water
        buttonLevelWater1.onClick.AddListener(UpGradeWaterHandler);
        buttonLevelWater2.onClick.AddListener(UpGradeWaterHandler);
        buttonLevelWater3.onClick.AddListener(UpGradeWaterHandler);
        buttonLevelWater4.onClick.AddListener(UpGradeWaterHandler);
        #endregion

        #region LevelUp Rock
        buttonLevelRock1.onClick.AddListener(UpGradeRockHandler);
        buttonLevelRock2.onClick.AddListener(UpGradeRockHandler);
        buttonLevelRock3.onClick.AddListener(UpGradeRockHandler);
        buttonLevelRock4.onClick.AddListener(UpGradeRockHandler);
        #endregion

        #region LevelUp Wind
        buttonLevelWind1.onClick.AddListener(UpGradeWindHandler);
        buttonLevelWind2.onClick.AddListener(UpGradeWindHandler);
        buttonLevelWind3.onClick.AddListener(UpGradeWindHandler);
        buttonLevelWind4.onClick.AddListener(UpGradeWindHandler);
        #endregion
    }

    #region LevelUp Fire
    private void UpGradeFireHandler()
    {
        levelFire += 1;

        switch (levelFire)
        {
            case 1:

                if (costDiamondLevelFire1 <= GameManager.inst.CheckDiamondPoint())
                {
                    levelDamageFire += defaultDamageFire * 10 / 100;
                    buttonLevelFire1.interactable = false;
                    GameManager.inst.subtractDiamond(costDiamondLevelFire1);
                }
                else
                {
                    // Text No puedes desbloquear
                    levelFire = 0;
                }
                break;
            case 2:
                if (costDiamondLevelFire2 <= GameManager.inst.CheckDiamondPoint())
                {
                    levelDamageFire += defaultDamageFire * 20 / 100;
                    buttonLevelFire2.interactable = false;
                    GameManager.inst.subtractDiamond(costDiamondLevelFire2);
                }
                else
                {
                    // Text No puedes desbloquear
                    levelFire = 1;
                }
                break;
            case 3:
                if (costDiamondLevelFire3 <= GameManager.inst.CheckDiamondPoint())
                {
                    levelDamageFire += defaultDamageFire * 30 / 100;
                    buttonLevelFire3.interactable = false;
                    GameManager.inst.subtractDiamond(costDiamondLevelFire3);
                }
                else
                {
                    // Text No puedes desbloquear
                    levelFire = 2;
                }
                break;
            case 4:
                if (costDiamondLevelFire4 <= GameManager.inst.CheckDiamondPoint())
                {
                    levelDamageFire += defaultDamageFire * 40 / 100;
                    buttonLevelFire4.interactable = false;
                    GameManager.inst.subtractDiamond(costDiamondLevelFire4);
                }
                else
                {
                    // Text No puedes desbloquear
                    levelFire = 3;
                }
                break;
        }
    }
    
    public int LevelFire()
    {
        return levelDamageFire;
    }
    #endregion

    #region LevelUp Water
    private void UpGradeWaterHandler()
    {
        levelWater += 1;

        switch (levelWater)
        {
            case 1:

                if (costDiamondLevelWater1 <= GameManager.inst.CheckDiamondPoint())
                {
                    levelDamageWater += defaultDamageWater * 10 / 100;
                    buttonLevelWater1.interactable = false;
                    GameManager.inst.subtractDiamond(costDiamondLevelWater1);
                }
                else
                {
                    // Text No puedes desbloquear
                    levelWater = 0;
                }
                break;
            case 2:
                if (costDiamondLevelWater2 <= GameManager.inst.CheckDiamondPoint())
                {
                    levelDamageWater += defaultDamageWater * 20 / 100;
                    buttonLevelWater2.interactable = false;
                    GameManager.inst.subtractDiamond(costDiamondLevelWater2);
                }
                else
                {
                    // Text No puedes desbloquear
                    levelWater = 1;
                }
                break;
            case 3:
                if (costDiamondLevelWater3 <= GameManager.inst.CheckDiamondPoint())
                {
                    levelDamageWater += defaultDamageWater * 30 / 100;
                    buttonLevelWater3.interactable = false;
                    GameManager.inst.subtractDiamond(costDiamondLevelWater3);
                }
                else
                {
                    // Text No puedes desbloquear
                    levelWater = 2;
                }
                break;
            case 4:
                if (costDiamondLevelWater4 <= GameManager.inst.CheckDiamondPoint())
                {
                    levelDamageWater += defaultDamageWater * 40 / 100;
                    buttonLevelWater4.interactable = false;
                    GameManager.inst.subtractDiamond(costDiamondLevelWater4);
                }
                else
                {
                    // Text No puedes desbloquear
                    levelWater = 3;
                }
                break;
        }
    }

    public int LevelWater()
    {
        return levelDamageWater;
    }
    #endregion

    #region LevelUp Rock
    private void UpGradeRockHandler()
    {
        levelRock += 1;

        switch (levelRock)
        {
            case 1:

                if (costDiamondLevelRock1 <= GameManager.inst.CheckDiamondPoint())
                {
                    levelDamageRock += defaultDamageRock * 10 / 100;
                    buttonLevelRock1.interactable = false;
                    GameManager.inst.subtractDiamond(costDiamondLevelRock1);
                }
                else
                {
                    // Text No puedes desbloquear
                    levelRock = 0;
                }
                break;
            case 2:
                if (costDiamondLevelRock2 <= GameManager.inst.CheckDiamondPoint())
                {
                    levelDamageRock += defaultDamageRock * 20 / 100;
                    buttonLevelRock2.interactable = false;
                    GameManager.inst.subtractDiamond(costDiamondLevelRock2);
                }
                else
                {
                    // Text No puedes desbloquear
                    levelRock = 1;
                }
                break;
            case 3:
                if (costDiamondLevelRock3 <= GameManager.inst.CheckDiamondPoint())
                {
                    levelDamageRock += defaultDamageRock * 30 / 100;
                    buttonLevelRock3.interactable = false;
                    GameManager.inst.subtractDiamond(costDiamondLevelRock3);
                }
                else
                {
                    // Text No puedes desbloquear
                    levelRock = 2;
                }
                break;
            case 4:
                if (costDiamondLevelRock4 <= GameManager.inst.CheckDiamondPoint())
                {
                    levelDamageRock += defaultDamageRock * 40 / 100;
                    buttonLevelRock4.interactable = false;
                    GameManager.inst.subtractDiamond(costDiamondLevelRock4);
                }
                else
                {
                    // Text No puedes desbloquear
                    levelRock = 3;
                }
                break;
        }
    }

    public int LevelRock()
    {
        return levelDamageRock;
    }
    #endregion

    #region LevelUp Wind
    private void UpGradeWindHandler()
    {
        levelWind += 1;

        switch (levelWind)
        {
            case 1:

                if (costDiamondLevelWind1 <= GameManager.inst.CheckDiamondPoint())
                {
                    levelDamageWind += defaultDamageWind * 10 / 100;
                    buttonLevelWind1.interactable = false;
                    GameManager.inst.subtractDiamond(costDiamondLevelWind1);
                }
                else
                {
                    // Text No puedes desbloquear
                    levelWind = 0;
                }
                break;
            case 2:
                if (costDiamondLevelWind2 <= GameManager.inst.CheckDiamondPoint())
                {
                    levelDamageWind += defaultDamageWind * 20 / 100;
                    buttonLevelWind2.interactable = false;
                    GameManager.inst.subtractDiamond(costDiamondLevelWind2);
                }
                else
                {
                    // Text No puedes desbloquear
                    levelWind = 1;
                }
                break;
            case 3:
                if (costDiamondLevelWind3 <= GameManager.inst.CheckDiamondPoint())
                {
                    levelDamageWind += defaultDamageWind * 30 / 100;
                    buttonLevelWind3.interactable = false;
                    GameManager.inst.subtractDiamond(costDiamondLevelWind3);
                }
                else
                {
                    // Text No puedes desbloquear
                    levelWind = 2;
                }
                break;
            case 4:
                if (costDiamondLevelFire4 <= GameManager.inst.CheckDiamondPoint())
                {
                    levelDamageWind += defaultDamageWind * 40 / 100;
                    buttonLevelWind4.interactable = false;
                    GameManager.inst.subtractDiamond(costDiamondLevelWind4);
                }
                else
                {
                    // Text No puedes desbloquear
                    levelWind = 3;
                }
                break;
        }
    }

    public int LevelWind()
    {
        return levelDamageWind;
    }
    #endregion

    public void ActivePanel()
    {
        PauseGameManager.inst.IsGamePause();
        panel.SetActive(true);
    }

    public void DesactivePanel()
    {
        PauseGameManager.inst.IsGameRunning();
        panel.SetActive(false);
    }
}
