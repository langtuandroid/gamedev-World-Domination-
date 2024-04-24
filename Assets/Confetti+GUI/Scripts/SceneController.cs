using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Aig.Client.Integration.Runtime.Subsystem;
public class SceneController : MonoBehaviour
{
    public static bool levelActive = true;
    public static int level = 0;
    public static int levelCount = 0;
    public static int kills = 0;
    public static int money = 0;
    public static float health = 100f;
    public static float speed = 10f;
    public Transform[] allLevels;
    List<CanvasGroup> objsInFadeIn = new List<CanvasGroup>();
    List<CanvasGroup> objsInFadeOut = new List<CanvasGroup>();
    public CanvasGroup blackout;
    public CanvasGroup blackout2;
    public CanvasGroup blackout3;
    public CanvasGroup victoryScreen;
    public CanvasGroup lossScreen;
    public CanvasGroup startButton;
    //public CanvasGroup gameplayGUI;
    public CanvasGroup moneyGUI;
    public CanvasGroup healthGUI;
    public CanvasGroup infinity;
    public CanvasGroup noAdsButton;
    public CanvasGroup settingsButton;
    public CanvasGroup settingsScreen;
    public CanvasGroup GDPR;
    public CanvasGroup shopCanvas;
    //public GameObject rewardedBtn;
    //public GameObject skipBtn;
    //public GameObject adSkinBtn;
    //public GameObject buySkinBtn;
    //public CanvasGroup GetSkinPopup;
    //public Transform shopBtn;
    public Text levelText;
    public Text levelText2;
    public Text healthText1;
    public Text healthText2;
    //public Text killsText;
    //public Text timeText;
    public Text moneyText;
    //public Text rewardText;
    public Image countdownImage;
    public Text countdownText;
    public Text lossTextTop;
    public CanvasGroup lossButtonNo;
    public CanvasGroup lossButtonRevive;
    public CanvasGroup lossButtonNext;
    public CanvasGroup lossRewardObj;
    public Text lossRewardText;
    public GameObject mapObj;
    public Image skinImage;
    public GameObject biome2Obj;
    public GameObject rateButton;
    public Image iconBiome1Image;
    public Image iconBiome2Image;
    public Image[] markers;
    public Sprite lit;
    int[] prevLevels = new int[3];
    int currentScene = 0;
    public float passedTime = 0f;
    public int roundedTime = 0;
    //private AdManager AM;
    private static int deathsInARow = 0;
    bool revived = false;
    bool inAnimation = false;
    bool showSkinScreen = false;
    int ShowGetSkin = 0;
    int continues = 0;

    public Text victoryTopText1;
    public Text victoryTopText2;
    public Text victoryRewardValue1;
    public Text victoryRewardValue2;
    public Text victoryHealthValue;
    public Text victoryRewardValueTotal;
    public Text victoryRewardValueTotal2;
    public GameObject victoryScreen1;
    public GameObject victoryScreen2;
    public GameObject victoryScreen1ButtonNext;
    public GameObject victoryScreen1ButtonGet;
    public Image victoryScreen2SkinItem;
    public Text victoryScreen2HealthValue;
    public Text victoryScreen2SpeedValue;
    public Text victoryScreen2Name;
    int reward = 0;
    public Sprite[] biomes;
    bool watchedRewarded = false;

    public int NumberOfSkins;
    public int[] PurchasedSkins;
    public int EquipedSkinNumber;

    public int NumberOfHats;
    public int[] PurchasedHats;
    public int EquipedHatNumber;

    public List<Transform> shopHolders = new List<Transform>();
    public List<Transform> shopHolders2 = new List<Transform>();
    public List<string> skinNames = new List<string>();
    public List<Sprite> skinSprites = new List<Sprite>();
    public List<Vector3> skinInfo = new List<Vector3>();
    public List<string> hatNames = new List<string>();
    public List<Sprite> hatSprites = new List<Sprite>();
    public List<Vector3> hatInfo = new List<Vector3>();
    public Text textHealthShop;
    public Text textSpeedShop;
    public Image imageShop;
    public Image imageShop2;
    public GameObject shopUnlockButton;
    public GameObject shopUnlockButton2;
    public GameObject shopLockedButton1;
    public GameObject shopLockedButton2;
    public Sprite selected;
    public Sprite notSelected;
    public Sprite tabChosenSprite;
    public Sprite tabNotChosenSprite;
    public Color tabChosenColor;
    public Color tabNotChosenColor;
    public Image tab1;
    public Image tab2;
    public ScrollRect scrollRect;
    public ScrollRect scrollRect2;
    public GameObject shopMenu1;
    public GameObject shopMenu2;
    int selectedSkin = -1;
    int selectedHat = -1;
    int loadedLevelN = 0;

    public Text[] upgradeLevelsTexts;
    public Text[] upgradeCostsTexts;
    public Text[] upgradeValuesTexts;
    public int[] upgradeLevels;
    public int[] upgradePrices;
    public int[] maxLevels;
    PlayerController PC;

    public CanvasGroup bar;
    private void Awake()
    {
        Application.targetFrameRate = 30;


        Time.timeScale = 1f;
        Time.fixedDeltaTime = (1f / 30f) * Time.timeScale;
        PC = FindObjectOfType<PlayerController>();
        levelActive = false;
        kills = 0;
        StartCoroutine(WaitForLoad());
        //AM = GameObject.FindObjectOfType<AdManager>();
    }



    private void InitCallback()
    {
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }
 

    private void Start()
    {

    }

    void LoadLevel()
    {
        RateGame.Instance.ShowRatePopup();

        //  if (allLevels.Length > currentScene)
        //      allLevels[currentScene].gameObject.SetActive(true);

        PC.PickLevel();

        loadedLevelN = currentScene + 1;
     
        int l = level + 1;
        int n = Mathf.FloorToInt(l / 7f) % 9;

        iconBiome1Image.sprite = biomes[n];
        iconBiome2Image.sprite = biomes[(n + 1) % biomes.Length];

        AddToFadeOut(blackout);
    }

    IEnumerator WaitForLoad()
    {
        yield return 0;
        yield return 0;
        yield return 0;

        SelectLevel();
        yield break;
    }

    public void SendFinishEventAfterGameClose()
    {
        if (PlayerPrefs.GetInt("needsFinishEvent") == 1)
        {
            Debug.Log("Not finished event");
            /*
            LevelInfo li = new LevelInfo()
            {
                LevelNumber = level,
                LevelName = "level_" + loadedLevelN.ToString(),
                LevelDiff = "normal",
                LevelType = "normal",
                LevelCount = levelCount,
                LevelRandom = (level <= 25 ? false : true)
            };
            IntegrationSubsystem.Instance.AnalyticsService.LevelFinish(li, "", "game_closed", 0, continues);
            */
            PlayerPrefs.SetInt("needsFinishEvent", 0);
            PlayerPrefs.Save();
        }
    }
    int prRoundedTime = -1;
    int ScreenN = -1;
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            ScreenCapture.CaptureScreenshot("Screenshot" + ScreenN.ToString() + ".png");
            ScreenN++;
        }
        
        AnimateGUI();
        /*
        if (levelActive)
        {
            if (Input.GetMouseButtonDown(0) || (Input.touches.Length > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                AddToFadeOut(infinity);
            }
        }
        */
        victoryRewardValueTotal.text = reward.ToString();
        //victoryRewardValueTotal2.text = "+" + reward.ToString();

        if (!levelActive)
            return;

        prRoundedTime = Mathf.RoundToInt(passedTime);
        passedTime += Time.deltaTime;
        roundedTime = Mathf.RoundToInt(passedTime);

        if (prRoundedTime != roundedTime)
        {
            PlayerPrefs.SetInt("closed_time", roundedTime);
            PlayerPrefs.Save();
        }

        healthText1.text = Mathf.RoundToInt(health).ToString() + "%";
        /*
        if (Input.GetKeyDown(KeyCode.L))
        {
            EndLevelLoss();
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            EndLevelVictory();
        }
        */
    }

    public void LoadData()
    {
        level = PlayerPrefs.GetInt("Level");
        levelCount = PlayerPrefs.GetInt("levelCount");

        if (levelCount < level)
            levelCount = level - 1;

        money = PlayerPrefs.GetInt("Money");

        if (level == 0)
        {
            StartNewGame();
        }
        else if (level >= 11) {
            StartNewGame();
        }
        currentScene = PlayerPrefs.GetInt("CurrentScene");
        prevLevels[0] = PlayerPrefs.GetInt("prevLvl");
        prevLevels[1] = PlayerPrefs.GetInt("prevLvl1");
        prevLevels[2] = PlayerPrefs.GetInt("prevLvl2");

        for (int i = 0; i < upgradeLevels.Length; i++)
        {
            upgradeLevels[i] = PlayerPrefs.GetInt("upgradeLevel" + i.ToString());
        }

        moneyText.text = money.ToString();

        int n = 0;

        if (level <= 5)
        {
            mapObj.transform.localPosition += new Vector3(54f, 0f, 0f);
            skinImage.transform.localPosition -= new Vector3(108f, 0f, 0f);
            biome2Obj.transform.localPosition -= new Vector3(108f, 0f, 0f);

            markers[5].gameObject.SetActive(false);
            markers[6].gameObject.SetActive(false);

            n = level;
        }
        else
        {
            n = (level - 5) % 7;
        }

        if (n == 0)
            n = markers.Length;

        for (int i = 0; i < n; i++)
        {
            markers[i].sprite = lit;
        }

        for (int i = 0; i < NumberOfSkins; i++)
        {
            PurchasedSkins[i] = PlayerPrefs.GetInt("Skin" + i.ToString());
        }

        for (int i = 0; i < NumberOfHats; i++)
        {
            PurchasedHats[i] = PlayerPrefs.GetInt("Hat" + i.ToString());
        }

        if (PurchasedSkins[0] == 0)
        {
            PurchasedSkins[0] = 2;

            List<int> OwnedSkins = new List<int>();
            for (int i = 0; i < NumberOfSkins; i++)
            {
                if (PurchasedSkins[i] > 0)
                    OwnedSkins.Add(i);
            }

            if (OwnedSkins.Count == 1)
                PurchasedSkins[0] = 1;
        }

        if (PurchasedHats[0] == 0)
        {
            PurchasedHats[0] = 2;

            List<int> OwnedHats = new List<int>();
            for (int i = 0; i < NumberOfHats; i++)
            {
                if (PurchasedHats[i] > 0)
                    OwnedHats.Add(i);
            }

            if (OwnedHats.Count == 1)
                PurchasedHats[0] = 1;
        }

        EquipedHat();
        EquipedSkin();
        UpgradeUpdateTexts();

        List<int> unOwnedSkins = new List<int>();
        for (int i = 0; i < NumberOfSkins; i++)
        {
            if (PurchasedSkins[i] == 0)
                unOwnedSkins.Add(i);
        }

        if (unOwnedSkins.Count == 0)
            skinImage.gameObject.SetActive(false);
    }

    public void SaveSkinsInfo()
    {
        for (int i = 0; i < NumberOfSkins; i++)
        {
            PlayerPrefs.SetInt("Skin" + i.ToString(), PurchasedSkins[i]);
        }

        for (int i = 0; i < NumberOfHats; i++)
        {
            PlayerPrefs.SetInt("Hat" + i.ToString(), PurchasedHats[i]);
        }

        PlayerPrefs.Save();

        EquipedHat();
        EquipedSkin();
    }

    public void EquipedHat()
    {
        EquipedHatNumber = -1;
        for (int i = 0; i < NumberOfHats; i++)
        {
            if (PurchasedHats[i] == 1)
            {
                EquipedHatNumber = i;

                if (reward < 1)
                {

                }

                SetupShop();
                return;
            }
        }
    }

    public void EquipedSkin()
    {
        EquipedSkinNumber = -1;
        for (int i = 0; i < NumberOfSkins; i++)
        {
            if (PurchasedSkins[i] == 1)
            {
                EquipedSkinNumber = i;

                if (reward < 1)
                {
                    speed = skinInfo[EquipedSkinNumber].z;
                }

                SetupShop();
                return;
            }
        }
    }

    void SetupShop()
    {
        for (int i = 0; i < NumberOfSkins; i++)
        {
            shopHolders[i].Find("Name").GetComponent<Text>().text = skinNames[i];
            shopHolders[i].Find("TextHealth").GetComponent<Text>().text = Mathf.RoundToInt(skinInfo[i].y).ToString() + "%";
            shopHolders[i].Find("TextSpeed").GetComponent<Text>().text = (skinInfo[i].z).ToString("F1");
            shopHolders[i].Find("Icon").GetComponent<Image>().sprite = skinSprites[i];

            if (PurchasedSkins[i] == 0)
            {
                shopHolders[i].Find("IconUsed").gameObject.SetActive(false);
                shopHolders[i].Find("IconLocked").gameObject.SetActive(true);
            }

            if (PurchasedSkins[i] == 1)
            {
                shopHolders[i].Find("IconUsed").gameObject.SetActive(true);
                shopHolders[i].Find("IconLocked").gameObject.SetActive(false);
            }

            if (PurchasedSkins[i] == 2)
            {
                shopHolders[i].Find("IconUsed").gameObject.SetActive(false);
                shopHolders[i].Find("IconLocked").gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < NumberOfHats; i++)
        {
            shopHolders2[i].Find("Name").GetComponent<Text>().text = hatNames[i];
            shopHolders2[i].Find("Icon").GetComponent<Image>().sprite = hatSprites[i];

            if (PurchasedHats[i] == 0)
            {
                shopHolders2[i].Find("IconUsed").gameObject.SetActive(false);
                shopHolders2[i].Find("IconLocked").gameObject.SetActive(true);
            }

            if (PurchasedHats[i] == 1)
            {
                shopHolders2[i].Find("IconUsed").gameObject.SetActive(true);
                shopHolders2[i].Find("IconLocked").gameObject.SetActive(false);
            }

            if (PurchasedHats[i] == 2)
            {
                shopHolders2[i].Find("IconUsed").gameObject.SetActive(false);
                shopHolders2[i].Find("IconLocked").gameObject.SetActive(false);
            }
        }
    }

    public void SelectHat(int n)
    {
        if (n < 0)
            n = 0;

        selectedHat = n;

        if (selectedHat < 1)
            imageShop2.gameObject.SetActive(false);
        else
            imageShop2.gameObject.SetActive(true);


        for (int i = 0; i < NumberOfHats; i++)
        {
            if (i == n)
            {
                imageShop2.GetComponent<Image>().sprite = hatSprites[i];
                shopHolders2[i].Find("BG").GetComponent<Image>().sprite = selected;

                if (PurchasedHats[i] != 0)
                {
                    shopUnlockButton2.SetActive(false);
                    shopLockedButton2.SetActive(false);

                    if (PurchasedHats[i] == 2)
                    {
                        PurchasedHats[i] = 1;
                        for (int j = 0; j < NumberOfHats; j++)
                        {
                            if (j != i && PurchasedHats[j] == 1)
                                PurchasedHats[j] = 2;
                        }
                        SaveSkinsInfo();
                    }
                }
                else
                {
                    if (money >= Mathf.RoundToInt(hatInfo[i].x))
                    {
                        shopUnlockButton2.SetActive(true);
                        shopLockedButton2.SetActive(false);
                        shopUnlockButton2.transform.Find("Price").GetComponent<Text>().text = Mathf.RoundToInt(hatInfo[i].x).ToString();
                    }
                    else
                    {
                        shopUnlockButton2.SetActive(false);
                        shopLockedButton2.SetActive(true);
                        shopLockedButton2.transform.Find("Price").GetComponent<Text>().text = Mathf.RoundToInt(hatInfo[i].x).ToString();
                    }
                }
            }
            else
                shopHolders2[i].Find("BG").GetComponent<Image>().sprite = notSelected;
        }
    }

    public void SelectSkin(int n)
    {
        if (n < 0)
            n = 0;

        selectedSkin = n;

        for (int i = 0; i < NumberOfSkins; i++)
        {
            if (i == n)
            {
                textHealthShop.GetComponent<Text>().text = Mathf.RoundToInt(skinInfo[i].y) + " % ";
                textSpeedShop.GetComponent<Text>().text = (skinInfo[i].z).ToString("F1");
                imageShop.GetComponent<Image>().sprite = skinSprites[i];
                shopHolders[i].Find("BG").GetComponent<Image>().sprite = selected;

                if (PurchasedSkins[i] != 0)
                {
                    shopUnlockButton.SetActive(false);
                    shopLockedButton1.SetActive(false);

                    if (PurchasedSkins[i] == 2)
                    {
                        PurchasedSkins[i] = 1;
                        for (int j = 0; j < NumberOfSkins; j++)
                        {
                            if (j != i && PurchasedSkins[j] == 1)
                                PurchasedSkins[j] = 2;
                        }
                        SaveSkinsInfo();
                    }
                }
                else
                {
                    if (money >= Mathf.RoundToInt(skinInfo[i].x))
                    {
                        shopUnlockButton.SetActive(true);
                        shopLockedButton1.SetActive(false);
                        shopUnlockButton.transform.Find("Price").GetComponent<Text>().text = Mathf.RoundToInt(skinInfo[i].x).ToString();
                    }
                    else
                    {
                        shopUnlockButton.SetActive(false);
                        shopLockedButton1.SetActive(true);
                        shopLockedButton1.transform.Find("Price").GetComponent<Text>().text = Mathf.RoundToInt(skinInfo[i].x).ToString();
                    }
                }
            }
            else
                shopHolders[i].Find("BG").GetComponent<Image>().sprite = notSelected;
        }
    }

    public void BuySkin(bool useMoney)
    {
        if (useMoney)
        {
            if (money < Mathf.RoundToInt(skinInfo[selectedSkin].x))
                return;

            money -= Mathf.RoundToInt(skinInfo[selectedSkin].x);
            SaveData();
        }

        if (PurchasedSkins[selectedSkin] == 0)
        {
            PurchasedSkins[selectedSkin] = 1;
            for (int i = 0; i < NumberOfSkins; i++)
            {
                if (i != selectedSkin && PurchasedSkins[i] == 1)
                    PurchasedSkins[i] = 2;
            }
            SaveSkinsInfo();
        }

        SelectSkin(selectedSkin);

        //IntegrationSubsystem.Instance.AnalyticsService.SkinUnlock("blob_skin", skinNames[selectedSkin].Replace(". ", "_"), "normal", (useMoney ? "buy" : "reward"));

        List<int> unOwnedSkins = new List<int>();
        for (int i = 0; i < NumberOfSkins; i++)
        {
            if (PurchasedSkins[i] == 0)
                unOwnedSkins.Add(i);
        }

        if (unOwnedSkins.Count == 0)
            skinImage.gameObject.SetActive(false);
    }

    public void BuyHat(bool useMoney)
    {
        if (useMoney)
        {
            if (money < Mathf.RoundToInt(hatInfo[selectedHat].x))
                return;

            money -= Mathf.RoundToInt(hatInfo[selectedHat].x);
            SaveData();
        }

        if (PurchasedHats[selectedHat] == 0)
        {
            PurchasedHats[selectedHat] = 1;
            for (int i = 0; i < NumberOfHats; i++)
            {
                if (i != selectedHat && PurchasedHats[i] == 1)
                    PurchasedHats[i] = 2;
            }
            SaveSkinsInfo();
        }

        SelectHat(selectedHat);

        //IntegrationSubsystem.Instance.AnalyticsService.SkinUnlock("hat", hatNames[selectedSkin], "normal", (useMoney ? "buy" : "reward"));
    }

    public void RateEvent(int starsN)
    {
        //IntegrationSubsystem.Instance.AnalyticsService.RateUs((settingsScreen.alpha > 0.5f ? "requested_by_player" : "session_time"), starsN);
    }

    private void StartNewGame()
    {
        level = 1;
        money = 50;
        SaveData();
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("Level", level);
        PlayerPrefs.SetInt("Money", money);
        PlayerPrefs.SetInt("CurrentScene", currentScene);

        for (int i = 0; i < upgradeLevels.Length; i++)
        {
            PlayerPrefs.SetInt("upgradeLevel" + i.ToString(), upgradeLevels[i]);
        }

        PlayerPrefs.Save();

        moneyText.text = money.ToString();
    }

    void SelectLevel()
    {
        LoadData();

        if (level > allLevels.Length)
        {
            if (currentScene == 0)
                GetNewScene();
        }
        else
            currentScene = level - 1;

        levelText.text = "LEVEL " + level.ToString();
        levelText2.text = "LEVEL " + level.ToString();


        LoadLevel();
    }

    private void GetNewScene()
    {
        currentScene = Random.Range(2, allLevels.Length);
        int c = 10;
        while (prevLevels[0] == currentScene || prevLevels[1] == currentScene || prevLevels[2] == currentScene)
        {
            c--;
            currentScene = Random.Range(2, allLevels.Length);
            if (c <= 0)
                break;
        }

        PlayerPrefs.SetInt("CurrentScene", currentScene);
        PlayerPrefs.Save();
    }

    public void StartLevel()
    {
        if (levelActive)
            return;

        levelActive = true;
        AddToFadeOut(startButton);
        AddToFadeOut(moneyGUI);
        AddToFadeOut(noAdsButton);
        AddToFadeOut(settingsButton);
        AddToFadeIn(bar);
        //AddToFadeIn(healthGUI);
        //AddToFadeIn(infinity);
        Debug.Log("Start event");

        levelCount++;
        PlayerPrefs.SetInt("levelCount", levelCount);
        PlayerPrefs.Save();


        LevelInfo li = new LevelInfo()
        {
            LevelNumber = level,
            LevelName = "level_" + loadedLevelN.ToString(),
            LevelDiff = "normal",
            LevelType = "normal",
            LevelCount = levelCount,
            LevelRandom = false//(level <= 25 ? false : true)
        };

   

        IntegrationSubsystem.Instance.AnalyticsService.LevelStart(li, "");
  
        /*
        var metrica = AppMetrica.Instance;
        string report = "level_start";
        Dictionary<string, object> par = new Dictionary<string, object>();
        par.Add("level", level);

        metrica.ReportEvent(report, par);
        metrica.SendEventsBuffer();
        */
        PlayerPrefs.SetInt("needsFinishEvent", 1);
        PlayerPrefs.SetInt("closed_time", 0);
        PlayerPrefs.Save();
    }

    public void AddToFadeIn(CanvasGroup obj)
    {
        obj.gameObject.SetActive(true);

        if (!objsInFadeIn.Contains(obj))
            objsInFadeIn.Add(obj);

        obj.blocksRaycasts = true;

        if (objsInFadeOut.Contains(obj))
            objsInFadeOut.Remove(obj);
    }

    public void AddToFadeOut(CanvasGroup obj)
    {
        if (!objsInFadeOut.Contains(obj))
            objsInFadeOut.Add(obj);

        obj.blocksRaycasts = false;

        if (objsInFadeIn.Contains(obj))
            objsInFadeIn.Remove(obj);
    }

    public void DelayedFadeOut(CanvasGroup obj, float t)
    {
        StartCoroutine(DelayedFadeOutAnim(obj, t));
    }

    IEnumerator DelayedFadeOutAnim(CanvasGroup obj, float t)
    {
        yield return new WaitForSeconds(t);
        AddToFadeOut(obj);
    }

    void AnimateGUI()
    {
        for (int i = objsInFadeIn.Count - 1; i >= 0; i--)
        {
            if (objsInFadeIn[i].alpha < 0.99f)
                objsInFadeIn[i].alpha = Mathf.Lerp(objsInFadeIn[i].alpha, 1f, 15f * Time.unscaledDeltaTime);
            else
                objsInFadeIn.Remove(objsInFadeIn[i]);
        }

        for (int i = objsInFadeOut.Count - 1; i >= 0; i--)
        {
            if (objsInFadeOut[i].alpha > 0.01f)
                objsInFadeOut[i].alpha = Mathf.Lerp(objsInFadeOut[i].alpha, 0f, 15f * Time.unscaledDeltaTime);
            else
            {
                objsInFadeOut[i].gameObject.SetActive(false);
                objsInFadeOut.Remove(objsInFadeOut[i]);
            }
        }
    }

    IEnumerator ExtraReward()
    {
        inAnimation = true;

        float t = health / 100f;
        float tMax = t;
        int extra = Mathf.RoundToInt(health);
        int extraCur = 0;
        int rew = reward;

        while (t > 0f)
        {
            t -= Time.deltaTime * 2f;
            t = Mathf.Clamp(t, 0f, 1000f);
            extraCur = Mathf.RoundToInt(((tMax - t) / tMax) * extra);
            victoryRewardValue2.text = extraCur.ToString();

            reward = rew + extraCur;
            moneyText.text = (money + reward).ToString();

            healthText2.text = Mathf.RoundToInt((t / tMax) * tMax * 100f).ToString() + "%";
            yield return 0;
        }

        victoryRewardValue2.text = extra.ToString();
        healthText2.text = "0%";

        reward = rew + extra;
        moneyText.text = (money + reward).ToString();

        inAnimation = false;

        if (showSkinScreen)
        {
            yield return new WaitForSeconds(1.25f);
            StartCoroutine(SkinScreen());
        }

        yield break;
    }

    IEnumerator SkinScreen()
    {
        inAnimation = true;

        for (int i = 0; i < NumberOfSkins; i++)
        {
            if (PurchasedSkins[i] == 0)
            {
                selectedSkin = i;

                victoryScreen2Name.text = skinNames[i];
                victoryScreen2HealthValue.text = Mathf.RoundToInt(skinInfo[i].y).ToString() + "%";
                victoryScreen2SpeedValue.text = (skinInfo[i].z).ToString("F1");
                victoryScreen2SkinItem.sprite = skinSprites[i];

                break;
            }
        }

        float t = 1f;
        float pos = victoryScreen2.transform.localPosition.x;
        victoryScreen2.gameObject.SetActive(true);

        while (t > 0f)
        {
            t -= Time.deltaTime * 2f;

            victoryScreen2.transform.localPosition = new Vector3(Mathf.SmoothStep(0f, pos, t), victoryScreen2.transform.localPosition.y, victoryScreen2.transform.localPosition.z);
            victoryScreen1.transform.localPosition = new Vector3(Mathf.SmoothStep(-pos, 0f, t), victoryScreen2.transform.localPosition.y, victoryScreen2.transform.localPosition.z);

            yield return 0;
        }

        victoryScreen2.transform.localPosition = new Vector3(0f, victoryScreen2.transform.localPosition.y, victoryScreen2.transform.localPosition.z);
        victoryScreen1.transform.localPosition = new Vector3(-pos, victoryScreen2.transform.localPosition.y, victoryScreen2.transform.localPosition.z);

        inAnimation = false;

        yield break;
    }

    bool HasInternet ()
    {
        if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork || Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
            return true;
        else
            return false;
    }

    public void EndLevelVictory()
    {
        if (!levelActive)
            return;

        List<int> unOwnedSkins = new List<int>();
        for (int i = 0; i < NumberOfSkins; i++)
        {
            if (PurchasedSkins[i] == 0)
                unOwnedSkins.Add(i);
        }
        /*
        if ((level == 5 || (level - 5) % 7 == 0) && AM.IsRewardedReady() && unOwnedSkins.Count > 0 && HasInternet())
        {
            victoryScreen1ButtonGet.SetActive(false);
            victoryScreen1ButtonNext.SetActive(false);

            showSkinScreen = true;
        }
        else
        {
            if (!AM.IsRewardedReady() || !HasInternet())
            {
        */
                victoryScreen1ButtonGet.SetActive(false);
                victoryScreen1ButtonNext.transform.localPosition = new Vector3(0f, victoryScreen1ButtonNext.transform.localPosition.y, victoryScreen1ButtonNext.transform.localPosition.z);
                //IntegrationSubsystem.Instance.AnalyticsService.VideoAdsAvailable("rewarded", "victory_screen", "not_available");
        //    }
        //}

        healthText2.text = health.ToString() + "%";
        deathsInARow = 0;
        StartCoroutine(Victory());
        levelActive = false;
        //killsText.text = "KILLS: " + kills.ToString();
        //timeText.text = "TIME : " + roundedTime.ToString() + "s";

        int n = 50 + ((level-1)*50);
        victoryRewardValue1.text = n.ToString();
        victoryRewardValue2.text = Mathf.RoundToInt(n * (upgradeLevels[2] * 0.05f)).ToString();
        healthText2.text = "+" + Mathf.RoundToInt(upgradeLevels[2] * 5).ToString() + "%";

        reward = n + Mathf.RoundToInt(n * (upgradeLevels[2] * 0.05f));

        PlayerPrefs.SetInt("prevLvl2", PlayerPrefs.GetInt("prevLvl1"));
        PlayerPrefs.SetInt("prevLvl1", PlayerPrefs.GetInt("prevLvl"));
        if (level <= allLevels.Length)
            PlayerPrefs.SetInt("prevLvl", level - 1);
        else
            PlayerPrefs.SetInt("prevLvl", currentScene);

        prevLevels[0] = PlayerPrefs.GetInt("prevLvl");
        prevLevels[1] = PlayerPrefs.GetInt("prevLvl1");
        prevLevels[2] = PlayerPrefs.GetInt("prevLvl2");
        currentScene = 0;
        PlayerPrefs.Save();

        Debug.Log("Victory event");
      
        LevelInfo li = new LevelInfo()
        {
            LevelNumber = level,
            LevelName = "level_" + loadedLevelN.ToString(),
            LevelDiff = "normal",
            LevelType = "normal",
            LevelCount = levelCount,
            LevelRandom = false//(level <= 25 ? false : true)
        };
        IntegrationSubsystem.Instance.AnalyticsService.LevelFinish(li, "", "win", 100, continues);
        
        PlayerPrefs.SetInt("needsFinishEvent", 0);
        PlayerPrefs.Save();
        /*
        var metrica = AppMetrica.Instance;
        string report = "level_finish";
        Dictionary<string, object> par = new Dictionary<string, object>();

        par.Add("level", level);
        par.Add("result", "win");
        //par.Add("time", roundedTime);
        par.Add("progress", 100f);

        metrica.ReportEvent(report, par);
        metrica.SendEventsBuffer();
        */
    }

    IEnumerator Victory()
    {
        //yield return new WaitForSecondsRealtime(1f);
        Camera.main.transform.GetComponent<Confetti>().Launch();
        yield return new WaitForSecondsRealtime(2f);
        AddToFadeOut(healthGUI);
        AddToFadeOut(bar);
        AddToFadeIn(blackout2);
        AddToFadeIn(victoryScreen);
        AddToFadeIn(moneyGUI);
        //yield return new WaitForSecondsRealtime(0.33f);
        //StartCoroutine(ExtraReward());
        yield break;
    }

    public void EndLevelLoss()
    {
        if (!levelActive)
            return;

        deathsInARow++;
        StartCoroutine(Loss());
        levelActive = false;

        /*
        var metrica = AppMetrica.Instance;
        string report = "level_finish";
        Dictionary<string, object> par = new Dictionary<string, object>();

        par.Add("level", level);
        par.Add("result", "lose");
        //par.Add("time", roundedTime);
        par.Add("progress", 0f);
        
        metrica.ReportEvent(report, par);
        metrica.SendEventsBuffer();
        */
    }

    bool sentLoss = false;

    public void SendLossEvent()
    {
        if (sentLoss)
            return;

        sentLoss = true;
        Debug.Log("Loss event");
        
        LevelInfo li = new LevelInfo()
        {
            LevelNumber = level,
            LevelName = "level_" + loadedLevelN.ToString(),
            LevelDiff = "normal",
            LevelType = "normal",
            LevelCount = levelCount,
            LevelRandom = false//(level <= 25 ? false : true)
        };
        IntegrationSubsystem.Instance.AnalyticsService.LevelFinish(li, "", "lose", 0, continues);
        
        PlayerPrefs.SetInt("needsFinishEvent", 0);
        PlayerPrefs.Save();
    }

    IEnumerator Countdown()
    {
        float t = 9f;

        while (t > 0f && !watchedRewarded)
        {
            t -= Time.deltaTime;

            countdownImage.fillAmount = t / 9f;
            countdownText.text = Mathf.CeilToInt(t).ToString();

            yield return 0;
        }

        if (!watchedRewarded)
            SendLossEvent();
        ShowLossScreenPart2(true);

        yield break;
    }

    public void ShowLossScreenPart2(bool slowly)
    {
        if (slowly)
        {
            AddToFadeOut(lossButtonNo);
            AddToFadeOut(lossButtonRevive);
            AddToFadeOut(countdownImage.GetComponent<CanvasGroup>());
            AddToFadeIn(lossButtonNext);
            AddToFadeIn(lossRewardObj);
        }
        else
        {
            lossButtonNo.alpha = 0f;
            lossButtonNo.gameObject.SetActive(false);
            lossButtonRevive.alpha = 0f;
            lossButtonRevive.gameObject.SetActive(false);
            countdownImage.GetComponent<CanvasGroup>().alpha = 0f;
            countdownImage.gameObject.SetActive(false);
            lossButtonNext.alpha = 1f;
            lossButtonNext.gameObject.SetActive(true);
            lossRewardObj.alpha = 1f;
            lossRewardObj.gameObject.SetActive(true);
        }

        int lossReward = 25;
        lossRewardText.text = lossReward.ToString();
        lossTextTop.text = "YOU LOSE";

        reward = lossReward;
    }

    IEnumerator Loss()
    {
        /*
        if (AM.IsRewardedReady() && !revived && HasInternet())
            StartCoroutine(Countdown());
        else
        {
        */
            SendLossEvent();
            ShowLossScreenPart2(false);
        /*
            if ((!AM.IsRewardedReady() || !HasInternet()) && !revived)
            {
                IntegrationSubsystem.Instance.AnalyticsService.VideoAdsAvailable("rewarded", "loss_screen", "not_available");
            }
        }
        */
        AddToFadeOut(healthGUI);
        AddToFadeOut(settingsButton);
        AddToFadeIn(blackout2);
        AddToFadeIn(lossScreen);

        yield break;
    }

    public void OpenShop()
    {
        AddToFadeIn(shopCanvas);
        AddToFadeIn(blackout2);
        OpenTab(0);
    }

    public void OpenTab(int n)
    {
        SelectSkin(EquipedSkinNumber);
        SelectHat(EquipedHatNumber);

        if (n == 0)
        {
            shopMenu1.SetActive(true);
            shopMenu2.SetActive(false);

            tab1.sprite = tabChosenSprite;
            tab1.GetComponentInChildren<Text>().color = tabChosenColor;

            tab2.sprite = tabNotChosenSprite;
            tab2.GetComponentInChildren<Text>().color = tabNotChosenColor;

            if (EquipedHatNumber < 1)
                imageShop2.gameObject.SetActive(false);
            else
                imageShop2.gameObject.SetActive(true);
        }
        else
        {
            shopMenu1.SetActive(false);
            shopMenu2.SetActive(true);

            tab2.sprite = tabChosenSprite;
            tab2.GetComponentInChildren<Text>().color = tabChosenColor;

            tab1.sprite = tabNotChosenSprite;
            tab1.GetComponentInChildren<Text>().color = tabNotChosenColor;

            if (EquipedHatNumber < 1)
                imageShop2.gameObject.SetActive(false);
            else
                imageShop2.gameObject.SetActive(true);
        }

        scrollRect.horizontalNormalizedPosition = Mathf.Clamp01((float)EquipedSkinNumber / (NumberOfSkins - 1));
        scrollRect2.horizontalNormalizedPosition = Mathf.Clamp01((float)EquipedHatNumber / (NumberOfHats - 1));
    }

    public void CloseShop()
    {
        if (inAnimation)
            return;

        AddToFadeOut(shopCanvas);
        AddToFadeOut(blackout2);
    }
    /*
    public void SkinButton()
    {
        if (!AM.IsRewardedReady() || !HasInternet())
            return;
        
        LevelInfo li = new LevelInfo()
        {
            LevelNumber = level,
            LevelName = "level_" + loadedLevelN.ToString(),
            LevelDiff = "normal",
            LevelType = "normal",
            LevelCount = levelCount,
            LevelRandom = (level <= 25 ? false : true)
        };
        AM.ShowRewardedSkin("skin", li);
        
        watchedRewarded = true;
    }
    */
    public void SkinCallback()
    {
        BuySkin(false);
        RestartScene(true);
    }

    public void RestartScene(bool won)
    {
        if (inAnimation)
            return;
        /*
        LevelInfo li = new LevelInfo()
        {
            LevelNumber = level,
            LevelName = "level_" + loadedLevelN.ToString(),
            LevelDiff = "normal",
            LevelType = "normal",
            LevelCount = levelCount,
            LevelRandom = (level <= 25 ? false : true)
        };
        */
        if (won)
        {
            level++;
            //if (!watchedRewarded)
                //AM.ShowInter("victory", li);
        }
        else
        {
            //if (!watchedRewarded)
                //AM.ShowInter("loss", li);
        }

        money += reward;
        SaveData();

        levelActive = false;
        StartCoroutine(ReloadScene());
    }
    /*
    public void ReviveBtn()
    {
        if (!AM.IsRewardedReady() || !HasInternet())
            return;
        
        LevelInfo li = new LevelInfo()
        {
            LevelNumber = level,
            LevelName = "level_" + loadedLevelN.ToString(),
            LevelDiff = "normal",
            LevelType = "normal",
            LevelCount = levelCount,
            LevelRandom = (level <= 25 ? false : true)
        };
        AM.ShowRewarded("revive", li);
        
        watchedRewarded = true;
        StartCoroutine(DelayedLossPart2());
    }
    */
    IEnumerator DelayedLossPart2()
    {
        lossButtonRevive.GetComponent<Button>().interactable = false;
        yield return new WaitForSecondsRealtime(2f);
        ShowLossScreenPart2(false);
        lossButtonRevive.GetComponent<Button>().interactable = true;
        yield break;
    }


    public void Revive()
    {       
        levelActive = true;
        AddToFadeOut(lossScreen);
        AddToFadeOut(blackout2);
        AddToFadeIn(healthGUI);
        AddToFadeIn(settingsButton);
        revived = true;
        continues++;
    }

    public void RestartButton()
    {
        if (!levelActive)
            return;

        Debug.Log("Restart event");

        levelActive = false;
        StartCoroutine(ReloadScene());

        /*
        var metrica = AppMetrica.Instance;
        string report = "level_finish";
        Dictionary<string, object> par = new Dictionary<string, object>();

        par.Add("level", level);

        par.Add("result", "restart");

        //par.Add("time", roundedTime);
        par.Add("progress", 0f);

        metrica.ReportEvent(report, par);
        metrica.SendEventsBuffer();
        */
    }

    IEnumerator ReloadScene()
    {
        AddToFadeIn(blackout);
        yield return new WaitForSecondsRealtime(0.75f);
        SceneManager.LoadScene(0);
    }

    public void OpenSettings()
    {
        AddToFadeIn(settingsScreen);  
        AddToFadeIn(blackout3);

        if (PlayerPrefs.GetInt("Rated") == 1)
            rateButton.SetActive(false);

        Time.timeScale = 0f;
        Time.fixedDeltaTime = (1f / 60f) * Time.timeScale;
    }

    public void CloseSettings()
    {
        AddToFadeOut(settingsScreen);
        AddToFadeOut(blackout3);

        Time.timeScale = 1f;
        Time.fixedDeltaTime = (1f / 60f) * Time.timeScale;
    }

    public void CloseGDPR()
    {
        if (PlayerPrefs.GetInt("GDPRevent") == 1)
        {
            AddToFadeOut(GDPR);
            AddToFadeIn(settingsScreen);
        }
        else
        {
            PlayerPrefs.SetInt("GDPRevent", 1);
            PlayerPrefs.Save();

            //MaxSdk.SetHasUserConsent(true);

            AddToFadeOut(GDPR);
            AddToFadeOut(blackout3);
            AddToFadeOut(blackout);
        }
    }

    public void TermsOfService()
    {
        Application.OpenURL("https://aigames.ae/policy");
    }

    public void PrivacyPolicy()
    {
        Application.OpenURL("https://aigames.ae/policy");
    }

    public void RateUs()
    {
        RateGame.Instance.ForceShowRatePopup();
    }
    /*
    public void ExtraRewardButton()
    {
        if (!AM.IsRewardedReady() || !HasInternet())
            return;
        
        LevelInfo li = new LevelInfo()
        {
            LevelNumber = level,
            LevelName = "level_" + loadedLevelN.ToString(),
            LevelDiff = "normal",
            LevelType = "normal",
            LevelCount = levelCount,
            LevelRandom = (level <= 25 ? false : true)
        };
        AM.ShowRewardedMoney("extra_money", li);
        
        watchedRewarded = true;
    }
    */
    public void ExtraRewardCallback()
    {
        reward += 300;
        RestartScene(true);
    }

    public void Upgrade(int i)
    {
        if (money < upgradePrices[i] || upgradeLevels[i] >= maxLevels[i])
            return;

        money -= upgradePrices[i];
        upgradeLevels[i]++;
        SaveData();

        UpgradeUpdateTexts();
    }

    void UpgradeUpdateTexts()
    {
        for (int i = 0; i < upgradeLevels.Length; i++)
        {
            upgradeLevelsTexts[i].text = "LEVEL " + (upgradeLevels[i] + 1).ToString();
            upgradePrices[i] = (upgradeLevels[i] + 1) * 100;

            if (upgradeLevels[i] < maxLevels[i])
                upgradeCostsTexts[i].text = upgradePrices[i].ToString();
            else
                upgradeCostsTexts[i].text = "MAX";

            if (i == 0)
            {
                upgradeValuesTexts[i].text = (20+upgradeLevels[i]).ToString() + "/city";
            }
            if (i == 1)
            {if(upgradeLevels[i]<10)
                upgradeValuesTexts[i].text =  "2."+(upgradeLevels[i]).ToString() + "/s";
            else
                upgradeValuesTexts[i].text =  "3." + (upgradeLevels[i]-10).ToString() + "/s";
            }
            if (i == 2)
            {
                upgradeValuesTexts[i].text = "+" + (upgradeLevels[i] * 5).ToString() + "%";
            }
        }
    }
}
