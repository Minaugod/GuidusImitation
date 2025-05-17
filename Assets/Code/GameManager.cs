using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [Header("# Player Status")]

    public CharacterController player;

    public int playerMaxHp;

    public int playerHp;

    public int playerShield;

    public int playerCoin;

    public int playerAtk;

    public float playerAtkSpd;

    public int playerCopperkey;

    [Header("# UpgradeLv")]

    public int atkLv;

    public int atkSpdLv;

    public int maxHpLv;

    public int shieldBonusLv;

    [Header("# StageElememt")]

    public int StageCount;
    public int floorCount;

    public bool enteredSpecial;

    [SerializeField]
    private GameObject LobbyPrefab;

    private GameObject LobbyStage;

    public StageHandler normalStage;

    public StageHandler specialStage;

    public List<GameObject> Chpt1 = new List<GameObject>();

    public List<GameObject> ChptSpecial = new List<GameObject>();

    public RoomData nowRoomData;

    public GameObject[] stageList;

    [SerializeField]
    private GameObject bossStage;

    public GameObject copperlock;

    [SerializeField]
    private GameObject specialStair;

    [SerializeField]
    private GameObject specialRoom;

    private GameObject previousStage;

    private void Start()
    {
        stageList = Resources.LoadAll<GameObject>("Prefab/Stages");

    }
    public void DataSave()
    {
        PlayerPrefs.SetInt("AtkLv", atkLv);
        PlayerPrefs.SetInt("AtkSpdLv", atkSpdLv);
        PlayerPrefs.SetInt("MaxHpLv", maxHpLv);
        PlayerPrefs.SetInt("ShieldBonusLv", shieldBonusLv);
        PlayerPrefs.SetInt("Coin", playerCoin);
        PlayerPrefs.SetInt("Copperkey", playerCopperkey);
        PlayerPrefs.Save();

    }

    public void DataLoad()
    {


        if (PlayerPrefs.HasKey("AtkLv"))
        {
            atkLv = PlayerPrefs.GetInt("AtkLv");
            playerAtk += (atkLv - 1);
        }

        if (PlayerPrefs.HasKey("AtkSpdLv"))
        {
            atkSpdLv = PlayerPrefs.GetInt("AtkSpdLv");
            playerAtkSpd += (atkSpdLv - 1) * 0.15f;
        }

        if (PlayerPrefs.HasKey("MaxHpLv"))
        {
            maxHpLv = PlayerPrefs.GetInt("MaxHpLv");
            playerMaxHp += (maxHpLv - 1) * 2;
        }

        if (PlayerPrefs.HasKey("ShieldBonusLv"))
        {
            shieldBonusLv = PlayerPrefs.GetInt("ShieldBonusLv");
            playerShield += (shieldBonusLv - 1) * 2;

        }

        playerCoin = PlayerPrefs.GetInt("Coin");
        playerCopperkey = PlayerPrefs.GetInt("Copperkey");


    }

    public void ReturnLobby()
    {
        AbilityManager.Instance.ResetAbility();
        ScoreManager.Instance.ResetScore();
        TileLockManager.Instance.ResetLock();
        ResetGame();
        UiManager.Instance.uiResult.gameObject.SetActive(false);
    }


    private void ResetGame()
    {
        DataLoad();
        LobbyStage = Instantiate(LobbyPrefab);
        player.Tilemap = LobbyStage.GetComponentInChildren<Tilemap>();
        Chpt1.Clear();
        ChptSpecial.Clear();
        StageCount = 0;
        floorCount = 9;
        playerHp = playerMaxHp;
        playerShield = (shieldBonusLv - 1) * 2;
    }

    public void TryStageUp()    
    {

        if (nowRoomData != null)
        {
            if (nowRoomData.upStairUnlocked == false)
            {
                if (playerCopperkey > 0)
                {
                    playerCopperkey--;
                    nowRoomData.upStairUnlocked = true;
                    Destroy(nowRoomData.upStairLock);
                }

                return;
            }
        }

        int floor = floorCount - 1;
        UiManager.Instance.FadeScreen();
        UiManager.Instance.DisplayStageName(floor.ToString());
        Invoke("StageUp", 1f);
        
    }

    public void TryStageDown()
    {
        if (nowRoomData != null)
        {
            if (nowRoomData.downStairUnlocked == false)
            {
                if (playerCopperkey > 0)
                {
                    playerCopperkey--;
                    nowRoomData.downStairUnlocked = true;
                    Destroy(nowRoomData.downStairLock);
                }

                return;
            }
        }

        int floor = floorCount + 1;
        UiManager.Instance.FadeScreen();
        UiManager.Instance.DisplayStageName(floor.ToString());
        Invoke("StageDown", 1f);

    }

    public void TryEnterSpecial()
    {
        UiManager.Instance.FadeScreen();
        UiManager.Instance.DisplayStageName("??");
        Invoke("EnterSpecial", 1f);
    }

    public void StageUp()
    {
        TileLockManager.Instance.ResetLock();
        floorCount--;

        if (Chpt1.Count == 0)
        {
            ScoreManager.Instance.startAdventure = true;
            LobbyStage.SetActive(false);
        }

        else
        {
            Chpt1[StageCount].SetActive(false);
            StageCount++;
        }


        if (StageCount > Chpt1.Count - 1)
        {

            if(floorCount != 1)
            {
                bool CheckDuplicate = false;
                GameObject Stage;
                while (!CheckDuplicate)
                {
                    Stage = stageList[Random.Range(0, stageList.Length)];
                    if (previousStage != Stage)
                    {
                        previousStage = Stage;
                        Chpt1.Add(normalStage.CreateStage(Stage));
                        CheckDuplicate = true;
                    }
                }

                nowRoomData = Chpt1[StageCount].GetComponentInChildren<RoomData>();
                player.transform.localPosition = nowRoomData.upSpawn.position;

                int specialRoomRand = Random.Range(1, 11);
                if (specialRoomRand > 0)
                {
                    SpecialRoom spStairInfo = Instantiate(specialStair, nowRoomData.transform).GetComponent<SpecialRoom>();
                    spStairInfo.transform.position = nowRoomData.specialSpawn.position;
                    spStairInfo.RoomIndex = ChptSpecial.Count;

                    GameObject _specialRoom = Instantiate(specialRoom, specialStage.transform);
                    AbilityInfo ability = _specialRoom.GetComponentInChildren<AbilityInfo>();
                    ability.SetUp();

                    ChptSpecial.Add(_specialRoom);
                }

            }

            else
            {
                Chpt1.Add(normalStage.CreateStage(bossStage));
            }

        }


        nowRoomData = Chpt1[StageCount].GetComponentInChildren<RoomData>();
        player.transform.localPosition = nowRoomData.upSpawn.position;

        Chpt1[StageCount].SetActive(true);
        player.Tilemap = Chpt1[StageCount].GetComponentInChildren<Tilemap>();

    }


    public void StageDown()
    {
        TileLockManager.Instance.ResetLock();

        normalStage.gameObject.SetActive(true);
        specialStage.gameObject.SetActive(false);

        if (!enteredSpecial)
        {

            Chpt1[StageCount].SetActive(false);

            StageCount--;
            floorCount++;

            Chpt1[StageCount].SetActive(true);

            nowRoomData = Chpt1[StageCount].GetComponentInChildren<RoomData>();
            player.transform.localPosition = nowRoomData.downSpawn.position;  

        }

        else
        {
            floorCount++;
            SpecialRoom specialRoom = Chpt1[StageCount].GetComponentInChildren<SpecialRoom>();
            ChptSpecial[specialRoom.RoomIndex].SetActive(false);


            nowRoomData = Chpt1[StageCount].GetComponentInChildren<RoomData>();
            player.transform.localPosition = nowRoomData.specialSpawn.position;

        }


        enteredSpecial = false;
        player.Tilemap = Chpt1[StageCount].GetComponentInChildren<Tilemap>();

    }

    public void EnterSpecial()
    {
        TileLockManager.Instance.ResetLock();
        floorCount--;
        enteredSpecial = true;

        normalStage.gameObject.SetActive(false);
        specialStage.gameObject.SetActive(true);

        int index = Chpt1[StageCount].GetComponentInChildren<SpecialRoom>().RoomIndex;
        ChptSpecial[index].SetActive(true);

        nowRoomData = ChptSpecial[index].GetComponentInChildren<RoomData>();
        player.transform.localPosition = nowRoomData.upSpawn.position;

        player.Tilemap = ChptSpecial[index].GetComponentInChildren<Tilemap>();

    }

    public void PlayerHit(int value)
    {
        if (player.PlayerHit)
        {
            return;
        }

        StartCoroutine(player.Invincible());
        UpdateHp(value * -1);
    }

    public void UpdateHp(int value)
    {
        // 만약 쉴드가 있고 Hp가 감소되는거라면 쉴드업데이트로
        if (playerShield > 0 && value < 0)
        {
            UpdateShield(value);
            return;
        }

        playerHp += value;

        if (playerHp > playerMaxHp)
        {
            playerHp = playerMaxHp;

        }


    }
    public void UpdateShield(int value)
    {

        playerShield += value;

        if (playerShield <= 0)
        {
            playerShield = 0;
        }
    }
    public IEnumerator GameWin()
    {
        ScoreManager.Instance.startAdventure = false;
        UiManager.Instance.FadeScreen();

        yield return new WaitForSeconds(1f);

        UiManager.Instance.GameWin();

        yield return null;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 체인을 걸어서 이 함수는 매 씬마다 호출된다.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ReturnLobby();
    }

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    private void Awake()
    {
        Application.targetFrameRate = 120;
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
