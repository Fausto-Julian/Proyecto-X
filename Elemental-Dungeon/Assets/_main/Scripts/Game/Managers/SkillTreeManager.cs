using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeManager : MonoBehaviour
{
    public static SkillTreeManager inst;

    private const string _pathGame = "Game";
    private const string _pathPlayer = "Player";
    private const string _filenameGameState = "GameState";
    private const string _filenameAbilitys = "Abilitys";

    [Header("Panel")]
    [SerializeField] private GameObject panel;
    [SerializeField] private Button buttonExitSkillTreeMenu;
    [SerializeField] private Button buttonAxie;
    [SerializeField] private Button buttonAbility;
    [SerializeField] private Button buttonExit;

    [Header("Icons")]
    [SerializeField] private GameObject iconFire;
    [SerializeField] private GameObject iconWater;
    [SerializeField] private GameObject iconRock;
    [SerializeField] private GameObject iconWind;

    [Header("Prefabs Bullets")]
    [SerializeField] private GameObject bulletFirePrefab;
    [SerializeField] private GameObject bulletWaterPrefab;
    [SerializeField] private GameObject bulletRockPrefab;
    [SerializeField] private GameObject bulletWindPrefab;

    [SerializeField] private float timeText;
    [SerializeField] private GameObject textAlert;

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

    private Ability fireAbility = new Ability();
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

    private Ability waterAbility = new Ability();
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

    private Ability rockAbility = new Ability();
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

    private Ability windAbility = new Ability();
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
        GameState game = SaveLoadSystemData.LoadData<GameState>(_pathGame, _filenameGameState);
        if (game == GameState.newGame)
        {
            ResetAbilitys();
            SaveLoadSystemData.SaveData(fireAbility, _pathPlayer, (_filenameAbilitys + "Fire"));
            SaveLoadSystemData.SaveData(waterAbility, _pathPlayer, (_filenameAbilitys + "Water"));
            SaveLoadSystemData.SaveData(rockAbility, _pathPlayer, (_filenameAbilitys + "Rock"));
            SaveLoadSystemData.SaveData(windAbility, _pathPlayer, (_filenameAbilitys + "Wind"));
        }
        else
        {
            fireAbility = SaveLoadSystemData.LoadData<Ability>(_pathPlayer, (_filenameAbilitys + "Fire"));
            waterAbility = SaveLoadSystemData.LoadData<Ability>(_pathPlayer, (_filenameAbilitys + "Water"));
            rockAbility = SaveLoadSystemData.LoadData<Ability>(_pathPlayer, (_filenameAbilitys + "Rock"));
            windAbility = SaveLoadSystemData.LoadData<Ability>(_pathPlayer, (_filenameAbilitys + "Wind"));
        }

        defaultDamageFire = bulletFirePrefab.GetComponent<BulletPlayer>().DamageDefault();
        defaultDamageWater = bulletWaterPrefab.GetComponent<BulletPlayer>().DamageDefault();
        defaultDamageRock = bulletRockPrefab.GetComponent<BulletPlayer>().DamageDefault();
        defaultDamageFire = bulletWindPrefab.GetComponent<BulletPlayer>().DamageDefault();

        buttonExitSkillTreeMenu.onClick.AddListener(DesactivePanel);

        buttonExit.onClick.AddListener(ExitHandler);
        buttonAbility.onClick.AddListener(ActiveAbilityButtonsHandler);
        buttonAxie.onClick.AddListener(ActiveAxieButtonsHandler);

        #region fireButton
        buttonLevelFire1.onClick.AddListener(UpGradeFireHandler);
        buttonLevelFire2.onClick.AddListener(UpGradeFireHandler);
        buttonLevelFire3.onClick.AddListener(UpGradeFireHandler);
        buttonLevelFire4.onClick.AddListener(UpGradeFireHandler);
        buttonLevelFire2.interactable = false;
        buttonLevelFire3.interactable = false;
        buttonLevelFire4.interactable = false;
        #endregion

        #region WaterButton
        buttonLevelWater1.onClick.AddListener(UpGradeWaterHandler);
        buttonLevelWater2.onClick.AddListener(UpGradeWaterHandler);
        buttonLevelWater3.onClick.AddListener(UpGradeWaterHandler);
        buttonLevelWater4.onClick.AddListener(UpGradeWaterHandler);
        buttonLevelWater2.interactable = false;
        buttonLevelWater3.interactable = false;
        buttonLevelWater4.interactable = false;
        #endregion

        #region RockButton
        buttonLevelRock1.onClick.AddListener(UpGradeRockHandler);
        buttonLevelRock2.onClick.AddListener(UpGradeRockHandler);
        buttonLevelRock3.onClick.AddListener(UpGradeRockHandler);
        buttonLevelRock4.onClick.AddListener(UpGradeRockHandler);
        buttonLevelRock2.interactable = false;
        buttonLevelRock3.interactable = false;
        buttonLevelRock4.interactable = false;
        #endregion

        #region WindButton
        buttonLevelWind1.onClick.AddListener(UpGradeWindHandler);
        buttonLevelWind2.onClick.AddListener(UpGradeWindHandler);
        buttonLevelWind3.onClick.AddListener(UpGradeWindHandler);
        buttonLevelWind4.onClick.AddListener(UpGradeWindHandler);
        buttonLevelWind2.interactable = false;
        buttonLevelWind3.interactable = false;
        buttonLevelWind4.interactable = false;
        #endregion
    }

    private void ActiveAxieButtonsHandler()
    {
        buttonAxie.gameObject.SetActive(false);
        buttonAbility.gameObject.SetActive(false);

        buttonExit.gameObject.SetActive(true);
    }

    private void ActiveAbilityButtonsHandler()
    {
        buttonAxie.gameObject.SetActive(false);
        buttonAbility.gameObject.SetActive(false);

        buttonExitSkillTreeMenu.gameObject.SetActive(false);
        buttonExit.gameObject.SetActive(true);

        iconFire.SetActive(true);
        iconWater.SetActive(true);
        iconRock.SetActive(true);
        iconWind.SetActive(true);

        buttonLevelFire1.gameObject.SetActive(true);
        buttonLevelFire2.gameObject.SetActive(true);
        buttonLevelFire3.gameObject.SetActive(true);
        buttonLevelFire4.gameObject.SetActive(true);
        buttonLevelWater1.gameObject.SetActive(true);
        buttonLevelWater2.gameObject.SetActive(true);
        buttonLevelWater3.gameObject.SetActive(true);
        buttonLevelWater4.gameObject.SetActive(true);
        buttonLevelRock1.gameObject.SetActive(true);
        buttonLevelRock2.gameObject.SetActive(true);
        buttonLevelRock3.gameObject.SetActive(true);
        buttonLevelRock4.gameObject.SetActive(true);
        buttonLevelWind1.gameObject.SetActive(true);
        buttonLevelWind2.gameObject.SetActive(true);
        buttonLevelWind3.gameObject.SetActive(true);
        buttonLevelWind4.gameObject.SetActive(true);
    }

    private void ExitHandler()
    {
        buttonExitSkillTreeMenu.gameObject.SetActive(true);
        buttonAxie.gameObject.SetActive(false);
        buttonAbility.gameObject.SetActive(true);

        iconFire.SetActive(false);
        iconWater.SetActive(false);
        iconRock.SetActive(false);
        iconWind.SetActive(false);

        buttonExit.gameObject.SetActive(false);
        buttonLevelFire1.gameObject.SetActive(false);
        buttonLevelFire2.gameObject.SetActive(false);
        buttonLevelFire3.gameObject.SetActive(false);
        buttonLevelFire4.gameObject.SetActive(false);
        buttonLevelWater1.gameObject.SetActive(false);
        buttonLevelWater2.gameObject.SetActive(false);
        buttonLevelWater3.gameObject.SetActive(false);
        buttonLevelWater4.gameObject.SetActive(false);
        buttonLevelRock1.gameObject.SetActive(false);
        buttonLevelRock2.gameObject.SetActive(false);
        buttonLevelRock3.gameObject.SetActive(false);
        buttonLevelRock4.gameObject.SetActive(false);
        buttonLevelWind1.gameObject.SetActive(false);
        buttonLevelWind2.gameObject.SetActive(false);
        buttonLevelWind3.gameObject.SetActive(false);
        buttonLevelWind4.gameObject.SetActive(false);
    }

    private void ResetAbilitys()
    {
        fireAbility.level = 0;
        fireAbility.levelDamage = 0;

        waterAbility.level = 0;
        waterAbility.levelDamage = 0;

        rockAbility.level = 0;
        rockAbility.levelDamage = 0;

        windAbility.level = 0;
        windAbility.levelDamage = 0;
    }

    private void Upgrader(ref Ability ability, ref Button button1, ref Button button2, ref Button button3, ref Button button4, int defaultDamage, int costLevel1, int costLevel2, int costLevel3, int costLevel4)
    {
        ability.level += 1;

        switch (ability.level)
        {
            case 1:
                if (costLevel1 <= GameManager.inst.GetDiamondPoint())
                {
                    ability.levelDamage += defaultDamage * 10 / 100;
                    button1.interactable = false;
                    button2.interactable = true;
                    GameManager.inst.subtractDiamond(costLevel1);
                }
                else
                {
                    StartCoroutine(mesaggeAlert());
                    ability.level = 0;
                }
                break;
            case 2:
                if (costLevel2 <= GameManager.inst.GetDiamondPoint())
                {
                    ability.levelDamage += defaultDamage * 20 / 100;
                    button2.interactable = false;
                    button3.interactable = true;
                    GameManager.inst.subtractDiamond(costLevel2);
                }
                else
                {
                    StartCoroutine(mesaggeAlert());
                    ability.level = 1;
                }
                break;
            case 3:
                if (costLevel3 <= GameManager.inst.GetDiamondPoint())
                {
                    ability.levelDamage += defaultDamage * 30 / 100;
                    button3.interactable = false;
                    button4.interactable = true;
                    GameManager.inst.subtractDiamond(costLevel3);
                }
                else
                {
                    StartCoroutine(mesaggeAlert());
                    ability.level = 2;
                }
                break;
            case 4:
                if (costLevel4 <= GameManager.inst.GetDiamondPoint())
                {
                    ability.levelDamage += defaultDamage * 40 / 100;
                    button4.interactable = false;
                    GameManager.inst.subtractDiamond(costLevel4);
                }
                else
                {
                    StartCoroutine(mesaggeAlert());
                    ability.level = 3;
                }
                break;
        }
    }

    private void UpgraderSword(ref int level, ref float levelDamage, ref Button button1, ref Button button2, ref Button button3, ref Button button4, int defaultDamage, int costLevel1, int costLevel2, int costLevel3, int costLevel4)
    {
        level += 1;

        switch (level)
        {
            case 1:
                if (costLevel1 <= GameManager.inst.GetDiamondPoint())
                {
                    levelDamage += defaultDamage * 10 / 100;
                    button1.interactable = false;
                    button2.interactable = true;
                    GameManager.inst.subtractDiamond(costLevel1);
                }
                else
                {
                    StartCoroutine(mesaggeAlert());
                    level = 0;
                }
                break;
            case 2:
                if (costLevel2 <= GameManager.inst.GetDiamondPoint())
                {
                    levelDamage += defaultDamage * 20 / 100;
                    button2.interactable = false;
                    button3.interactable = true;
                    GameManager.inst.subtractDiamond(costLevel2);
                }
                else
                {
                    StartCoroutine(mesaggeAlert());
                    level = 1;
                }
                break;
            case 3:
                if (costLevel3 <= GameManager.inst.GetDiamondPoint())
                {
                    levelDamage += defaultDamage * 30 / 100;
                    button3.interactable = false;
                    button4.interactable = true;
                    GameManager.inst.subtractDiamond(costLevel3);
                }
                else
                {
                    StartCoroutine(mesaggeAlert());
                    level = 2;
                }
                break;
            case 4:
                if (costLevel4 <= GameManager.inst.GetDiamondPoint())
                {
                    levelDamage += defaultDamage * 40 / 100;
                    button4.interactable = false;
                    GameManager.inst.subtractDiamond(costLevel4);
                }
                else
                {
                    StartCoroutine(mesaggeAlert());
                    level = 3;
                }
                break;
        }
    }

    private void UpGradeFireHandler()
    {
        Upgrader(ref fireAbility, ref buttonLevelFire1, ref buttonLevelFire2, ref buttonLevelFire3, ref buttonLevelFire4, defaultDamageFire, costDiamondLevelFire1, costDiamondLevelFire2, costDiamondLevelFire3, costDiamondLevelFire4);
    }
    
    public Ability GetAbilityFire()
    {
        return fireAbility;
    }

    private void UpGradeWaterHandler()
    {
        Upgrader(ref waterAbility, ref buttonLevelWater1, ref buttonLevelWater2, ref buttonLevelWater3, ref buttonLevelWater4, defaultDamageWater, costDiamondLevelWater1, costDiamondLevelWater2, costDiamondLevelWater3, costDiamondLevelWater4);
    }

    public Ability GetAbilityWater()
    {
        return waterAbility;
    }

    private void UpGradeRockHandler()
    {
        Upgrader(ref rockAbility, ref buttonLevelRock1, ref buttonLevelRock2, ref buttonLevelRock3, ref buttonLevelRock4, defaultDamageRock, costDiamondLevelRock1, costDiamondLevelRock2, costDiamondLevelRock3, costDiamondLevelRock4);
    }

    public Ability GetAbilityRock()
    {
        return rockAbility;
    }

    private void UpGradeWindHandler()
    {
        Upgrader(ref windAbility, ref buttonLevelWind1, ref buttonLevelWind2, ref buttonLevelWind3, ref buttonLevelWind4, defaultDamageWind, costDiamondLevelWind1, costDiamondLevelWind2, costDiamondLevelWind3, costDiamondLevelWind4);
    }

    public Ability GetAbilityWind()
    {
        return windAbility;
    }

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

    IEnumerator mesaggeAlert()
    {
        if (textAlert != null)
        {
            textAlert.SetActive(true);
            yield return new WaitForSecondsRealtime(timeText);
            textAlert.SetActive(false);
        }
    }

    public void DeletedManagerHandler()
    {
        Destroy(gameObject);
    }
}
