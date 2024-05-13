using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Aig.Client.Integration.Runtime.Subsystem;
public class SceneControllerWD : MonoBehaviour
{
    public static bool levelActive = true;
    public static int level = 0;
    public static int levelCount = 0;
    public static int kills = 0;
    public static int money = 0;
    public static float health = 100f;
    public static float speed = 10f;
    public Transform[] allLevels;
    private readonly List<CanvasGroup> objsInFadeInN = new List<CanvasGroup>();
    private readonly List<CanvasGroup> objsInFadeOutT = new List<CanvasGroup>();
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
  
    public Text levelText;
    public Text levelText2;
    public Text healthText1;
    public Text healthText2;
  
    public Text moneyText;
    
    //public Image countdownImage;
    public Text countdownText;
    public Text lossTextTop;
    //public CanvasGroup lossButtonNo;
    //public CanvasGroup lossButtonRevive;
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
    private int[] prevLevels = new int[3];
    private int currentScene = 0;
    public float passedTime = 0f;
    public int roundedTime = 0;
    
    private static int _deathsInARow = 0;
    private bool revivedD = false;
    private bool inAnimation = false;
    private bool showSkinScreen = false;
    private int showGetSkinN = 0;
    private int continuesS = 0;

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
    private int rewardD = 0;
    public Sprite[] biomes;
    private readonly bool watchedRewardedD = false;

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
    private int selectedSkinN = -1;
    private int selectedHatT = -1;
    private int loadedLevelNn = 0;

    public Text[] upgradeLevelsTexts;
    public Text[] upgradeCostsTexts;
    public Text[] upgradeValuesTexts;
    public int[] upgradeLevels;
    public int[] upgradePrices;
    public int[] maxLevels;
    private PlayerControllerWD pc;

    public CanvasGroup bar;
    
    private int prRoundedTimeE = -1;
    private int screenN = -1;
    
    private void Awake()
    {
        Application.targetFrameRate = 30;
        
        Time.timeScale = 1f;
        Time.fixedDeltaTime = (1f / 30f) * Time.timeScale;
        pc = FindObjectOfType<PlayerControllerWD>();
        levelActive = false;
        kills = 0;
        StartCoroutine(WaitForLoadD());
        //AM = GameObject.FindObjectOfType<AdManager>();
    }



    private void InitCallbackK()
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
    

    private void LoadLevelL()
    {
        RateGame.Instance.ShowRatePopup();

        //  if (allLevels.Length > currentScene)
        //      allLevels[currentScene].gameObject.SetActive(true);

        pc.PickLevelL();

        loadedLevelNn = currentScene + 1;
     
        int l = level + 1;
        int n = Mathf.FloorToInt(l / 7f) % 9;

        iconBiome1Image.sprite = biomes[n];
        iconBiome2Image.sprite = biomes[(n + 1) % biomes.Length];

        AddToFadeOutT(blackout);
    }

    private IEnumerator WaitForLoadD()
    {
        yield return 0;
        yield return 0;
        yield return 0;

        SelectLevelL();
        yield break;
    }

    public void SendFinishEventAfterGameCloseE()
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
    
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            ScreenCapture.CaptureScreenshot("Screenshot" + screenN.ToString() + ".png");
            screenN++;
        }
        
        AnimateGUIi();

        victoryRewardValueTotal.text = rewardD.ToString();

        if (!levelActive)
            return;

        prRoundedTimeE = Mathf.RoundToInt(passedTime);
        passedTime += Time.deltaTime;
        roundedTime = Mathf.RoundToInt(passedTime);

        if (prRoundedTimeE != roundedTime)
        {
            PlayerPrefs.SetInt("closed_time", roundedTime);
            PlayerPrefs.Save();
        }

        healthText1.text = Mathf.RoundToInt(health).ToString() + "%";
    }

    public void LoadDataA()
    {
        level = PlayerPrefs.GetInt("Level");
        levelCount = PlayerPrefs.GetInt("levelCount");

        if (levelCount < level)
            levelCount = level - 1;

        money = PlayerPrefs.GetInt("Money");

        if (level == 0)
        {
            StartNewGameE();
        }
        else if (level >= 11) {
            StartNewGameE();
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

        EquipedHatT();
        EquipedSkinN();
        UpgradeUpdateTextsS();

        List<int> unOwnedSkins = new List<int>();
        for (int i = 0; i < NumberOfSkins; i++)
        {
            if (PurchasedSkins[i] == 0)
                unOwnedSkins.Add(i);
        }

        if (unOwnedSkins.Count == 0)
            skinImage.gameObject.SetActive(false);
    }

    public void SaveSkinsInfoO()
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

        EquipedHatT();
        EquipedSkinN();
    }

    public void EquipedHatT()
    {
        EquipedHatNumber = -1;
        for (int i = 0; i < NumberOfHats; i++)
        {
            if (PurchasedHats[i] == 1)
            {
                EquipedHatNumber = i;

                if (rewardD < 1)
                {

                }

                SetupShopP();
                return;
            }
        }
    }

    public void EquipedSkinN()
    {
        EquipedSkinNumber = -1;
        for (int i = 0; i < NumberOfSkins; i++)
        {
            if (PurchasedSkins[i] == 1)
            {
                EquipedSkinNumber = i;

                if (rewardD < 1)
                {
                    speed = skinInfo[EquipedSkinNumber].z;
                }

                SetupShopP();
                return;
            }
        }
    }

    private void SetupShopP()
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

        selectedHatT = n;

        if (selectedHatT < 1)
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
                        SaveSkinsInfoO();
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

        selectedSkinN = n;

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
                        SaveSkinsInfoO();
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

    public void BuySkinN(bool useMoney)
    {
        if (useMoney)
        {
            if (money < Mathf.RoundToInt(skinInfo[selectedSkinN].x))
                return;

            money -= Mathf.RoundToInt(skinInfo[selectedSkinN].x);
            SaveDataA();
        }

        if (PurchasedSkins[selectedSkinN] == 0)
        {
            PurchasedSkins[selectedSkinN] = 1;
            for (int i = 0; i < NumberOfSkins; i++)
            {
                if (i != selectedSkinN && PurchasedSkins[i] == 1)
                    PurchasedSkins[i] = 2;
            }
            SaveSkinsInfoO();
        }

        SelectSkin(selectedSkinN);

        List<int> unOwnedSkins = new List<int>();
        for (int i = 0; i < NumberOfSkins; i++)
        {
            if (PurchasedSkins[i] == 0)
                unOwnedSkins.Add(i);
        }

        if (unOwnedSkins.Count == 0)
            skinImage.gameObject.SetActive(false);
    }

    public void BuyHatT(bool useMoney)
    {
        if (useMoney)
        {
            if (money < Mathf.RoundToInt(hatInfo[selectedHatT].x))
                return;

            money -= Mathf.RoundToInt(hatInfo[selectedHatT].x);
            SaveDataA();
        }

        if (PurchasedHats[selectedHatT] == 0)
        {
            PurchasedHats[selectedHatT] = 1;
            for (int i = 0; i < NumberOfHats; i++)
            {
                if (i != selectedHatT && PurchasedHats[i] == 1)
                    PurchasedHats[i] = 2;
            }
            SaveSkinsInfoO();
        }

        SelectHat(selectedHatT);

        //IntegrationSubsystem.Instance.AnalyticsService.SkinUnlock("hat", hatNames[selectedSkin], "normal", (useMoney ? "buy" : "reward"));
    }

    public void RateEventT(int starsN)
    {
        //IntegrationSubsystem.Instance.AnalyticsService.RateUs((settingsScreen.alpha > 0.5f ? "requested_by_player" : "session_time"), starsN);
    }

    private void StartNewGameE()
    {
        level = 1;
        money = 1000000000;
        //money = 50;
        SaveDataA();
    }

    public void SaveDataA()
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

    private void SelectLevelL()
    {
        LoadDataA();

        if (level > allLevels.Length)
        {
            if (currentScene == 0)
                GetNewSceneE();
        }
        else
            currentScene = level - 1;

        levelText.text = "LEVEL " + level.ToString();
        levelText2.text = "LEVEL " + level.ToString();


        LoadLevelL();
    }

    private void GetNewSceneE()
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

    public void StartLevelL()
    {
        if (levelActive)
            return;

        levelActive = true;
        AddToFadeOutT(startButton);
        AddToFadeOutT(moneyGUI);
        AddToFadeOutT(noAdsButton);
        AddToFadeOutT(settingsButton);
        AddToFadeInN(bar);
        //AddToFadeIn(healthGUI);
        //AddToFadeIn(infinity);
        Debug.Log("Start event");

        levelCount++;
        PlayerPrefs.SetInt("levelCount", levelCount);
        PlayerPrefs.Save();
        
        LevelInfo li = new LevelInfo()
        {
            LevelNumber = level,
            LevelName = "level_" + loadedLevelNn.ToString(),
            LevelDiff = "normal",
            LevelType = "normal",
            LevelCount = levelCount,
            LevelRandom = false//(level <= 25 ? false : true)
        };

   

        IntegrationSubsystem.Instance.AnalyticsService.LevelStart(li, "");
  
        PlayerPrefs.SetInt("needsFinishEvent", 1);
        PlayerPrefs.SetInt("closed_time", 0);
        PlayerPrefs.Save();
    }

    public void AddToFadeInN(CanvasGroup obj)
    {
        obj.gameObject.SetActive(true);

        if (!objsInFadeInN.Contains(obj))
            objsInFadeInN.Add(obj);

        obj.blocksRaycasts = true;

        if (objsInFadeOutT.Contains(obj))
            objsInFadeOutT.Remove(obj);
    }

    public void AddToFadeOutT(CanvasGroup obj)
    {
        if (!objsInFadeOutT.Contains(obj))
            objsInFadeOutT.Add(obj);

        obj.blocksRaycasts = false;

        if (objsInFadeInN.Contains(obj))
            objsInFadeInN.Remove(obj);
    }

    public void DelayedFadeOutT(CanvasGroup obj, float t)
    {
        StartCoroutine(DelayedFadeOutAnimM(obj, t));
    }

    private IEnumerator DelayedFadeOutAnimM(CanvasGroup obj, float t)
    {
        yield return new WaitForSeconds(t);
        AddToFadeOutT(obj);
    }

    private void AnimateGUIi()
    {
        for (int i = objsInFadeInN.Count - 1; i >= 0; i--)
        {
            if (objsInFadeInN[i].alpha < 0.99f)
                objsInFadeInN[i].alpha = Mathf.Lerp(objsInFadeInN[i].alpha, 1f, 15f * Time.unscaledDeltaTime);
            else
                objsInFadeInN.Remove(objsInFadeInN[i]);
        }

        for (int i = objsInFadeOutT.Count - 1; i >= 0; i--)
        {
            if (objsInFadeOutT[i].alpha > 0.01f)
                objsInFadeOutT[i].alpha = Mathf.Lerp(objsInFadeOutT[i].alpha, 0f, 15f * Time.unscaledDeltaTime);
            else
            {
                objsInFadeOutT[i].gameObject.SetActive(false);
                objsInFadeOutT.Remove(objsInFadeOutT[i]);
            }
        }
    }

    private IEnumerator ExtraRewardD()
    {
        inAnimation = true;

        float t = health / 100f;
        float tMax = t;
        int extra = Mathf.RoundToInt(health);
        int extraCur = 0;
        int rew = rewardD;

        while (t > 0f)
        {
            t -= Time.deltaTime * 2f;
            t = Mathf.Clamp(t, 0f, 1000f);
            extraCur = Mathf.RoundToInt(((tMax - t) / tMax) * extra);
            victoryRewardValue2.text = extraCur.ToString();

            rewardD = rew + extraCur;
            moneyText.text = (money + rewardD).ToString();

            healthText2.text = Mathf.RoundToInt((t / tMax) * tMax * 100f).ToString() + "%";
            yield return 0;
        }

        victoryRewardValue2.text = extra.ToString();
        healthText2.text = "0%";

        rewardD = rew + extra;
        moneyText.text = (money + rewardD).ToString();

        inAnimation = false;

        if (showSkinScreen)
        {
            yield return new WaitForSeconds(1.25f);
            StartCoroutine(SkinScreenN());
        }

        yield break;
    }

    private IEnumerator SkinScreenN()
    {
        inAnimation = true;

        for (int i = 0; i < NumberOfSkins; i++)
        {
            if (PurchasedSkins[i] == 0)
            {
                selectedSkinN = i;

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

    private bool HasInternetT()
    {
        if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork || Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
            return true;
        else
            return false;
    }

    public void EndLevelVictoryY()
    {
        if (!levelActive)
            return;

        List<int> unOwnedSkins = new List<int>();
        for (int i = 0; i < NumberOfSkins; i++)
        {
            if (PurchasedSkins[i] == 0)
                unOwnedSkins.Add(i);
        }
   
        victoryScreen1ButtonGet.SetActive(false);
        victoryScreen1ButtonNext.transform.localPosition = new Vector3(0f, victoryScreen1ButtonNext.transform.localPosition.y, victoryScreen1ButtonNext.transform.localPosition.z);

        healthText2.text = health.ToString() + "%";
        _deathsInARow = 0;
        StartCoroutine(VictoryY());
        levelActive = false;

        int n = 50 + ((level-1)*50);
        victoryRewardValue1.text = n.ToString();
        victoryRewardValue2.text = Mathf.RoundToInt(n * (upgradeLevels[2] * 0.05f)).ToString();
        healthText2.text = "+" + Mathf.RoundToInt(upgradeLevels[2] * 5).ToString() + "%";

        rewardD = n + Mathf.RoundToInt(n * (upgradeLevels[2] * 0.05f));

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
            LevelName = "level_" + loadedLevelNn.ToString(),
            LevelDiff = "normal",
            LevelType = "normal",
            LevelCount = levelCount,
            LevelRandom = false//(level <= 25 ? false : true)
        };
        IntegrationSubsystem.Instance.AnalyticsService.LevelFinish(li, "", "win", 100, continuesS);
        
        PlayerPrefs.SetInt("needsFinishEvent", 0);
        PlayerPrefs.Save();
    }

    private IEnumerator VictoryY()
    {
        //yield return new WaitForSecondsRealtime(1f);
        Camera.main.transform.GetComponent<ConfettiWD>().LaunchH();
        yield return new WaitForSecondsRealtime(2f);
        AddToFadeOutT(healthGUI);
        AddToFadeOutT(bar);
        AddToFadeInN(blackout2);
        AddToFadeInN(victoryScreen);
        AddToFadeInN(moneyGUI);
        //yield return new WaitForSecondsRealtime(0.33f);
        //StartCoroutine(ExtraReward());
        yield break;
    }

    public void EndLevelLossS()
    {
        if (!levelActive)
            return;

        _deathsInARow++;
        StartCoroutine(LossS());
        levelActive = false;
    }

    private bool sentLossS = false;

    public void SendLossEventT()
    {
        if (sentLossS)
            return;

        sentLossS = true;
        Debug.Log("Loss event");
        
        LevelInfo li = new LevelInfo()
        {
            LevelNumber = level,
            LevelName = "level_" + loadedLevelNn.ToString(),
            LevelDiff = "normal",
            LevelType = "normal",
            LevelCount = levelCount,
            LevelRandom = false//(level <= 25 ? false : true)
        };
        IntegrationSubsystem.Instance.AnalyticsService.LevelFinish(li, "", "lose", 0, continuesS);
        
        PlayerPrefs.SetInt("needsFinishEvent", 0);
        PlayerPrefs.Save();
    }

    private IEnumerator CountdownN()
    {
        float t = 9f;

        while (t > 0f && !watchedRewardedD)
        {
            t -= Time.deltaTime;

            //countdownImage.fillAmount = t / 9f;
            countdownText.text = Mathf.CeilToInt(t).ToString();

            yield return 0;
        }

        if (!watchedRewardedD)
            SendLossEventT();
        ShowLossScreenPart2(true);

        yield break;
    }

    public void ShowLossScreenPart2(bool slowly)
    {
        if (slowly)
        {
           // AddToFadeOutT(lossButtonNo);
           // AddToFadeOutT(lossButtonRevive);
           // AddToFadeOutT(countdownImage.GetComponent<CanvasGroup>());
            AddToFadeInN(lossButtonNext);
            AddToFadeInN(lossRewardObj);
        }
        else
        {
            //lossButtonNo.alpha = 0f;
            //lossButtonNo.gameObject.SetActive(false);
            //lossButtonRevive.alpha = 0f;
            //lossButtonRevive.gameObject.SetActive(false);
          //  countdownImage.GetComponent<CanvasGroup>().alpha = 0f;
           // countdownImage.gameObject.SetActive(false);
            lossButtonNext.alpha = 1f;
            lossButtonNext.gameObject.SetActive(true);
            lossRewardObj.alpha = 1f;
            lossRewardObj.gameObject.SetActive(true);
        }

        int lossReward = 25;
        lossRewardText.text = lossReward.ToString();
        lossTextTop.text = "YOU LOSE";

        rewardD = lossReward;
    }

    private IEnumerator LossS()
    {
        SendLossEventT();
        ShowLossScreenPart2(false);
    
        AddToFadeOutT(healthGUI);
        AddToFadeOutT(settingsButton);
        AddToFadeInN(blackout2);
        AddToFadeInN(lossScreen);

        yield break;
    }

    public void OpenShopP()
    {
        AddToFadeInN(shopCanvas);
        AddToFadeInN(blackout2);
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

    public void CloseShopP()
    {
        if (inAnimation)
            return;

        AddToFadeOutT(shopCanvas);
        AddToFadeOutT(blackout2);
    }
  
    public void SkinCallbackK()
    {
        BuySkinN(false);
        RestartScene(true);
    }

    public void RestartScene(bool won)
    {
        if (inAnimation)
            return;
  
        if (won)
        {
            level++;
        }

        money += rewardD;
        SaveDataA();

        levelActive = false;
        StartCoroutine(ReloadSceneE());
    }

    private IEnumerator DelayedLossPart2T()
    {
        //lossButtonRevive.GetComponent<Button>().interactable = false;
        yield return new WaitForSecondsRealtime(2f);
        ShowLossScreenPart2(false);
        //lossButtonRevive.GetComponent<Button>().interactable = true;
        yield break;
    }


    public void ReviveE()
    {       
        levelActive = true;
        AddToFadeOutT(lossScreen);
        AddToFadeOutT(blackout2);
        AddToFadeInN(healthGUI);
        AddToFadeInN(settingsButton);
        revivedD = true;
        continuesS++;
    }

    public void RestartButtonN()
    {
        if (!levelActive)
            return;

        Debug.Log("Restart event");

        levelActive = false;
        StartCoroutine(ReloadSceneE());

    }

    private IEnumerator ReloadSceneE()
    {
        AddToFadeInN(blackout);
        yield return new WaitForSecondsRealtime(0.75f);
        SceneManager.LoadScene(0);
    }

    public void OpenSettingsS()
    {
        AddToFadeInN(settingsScreen);  
        AddToFadeInN(blackout3);

        if (PlayerPrefs.GetInt("Rated") == 1)
            rateButton.SetActive(false);

        Time.timeScale = 0f;
        Time.fixedDeltaTime = (1f / 60f) * Time.timeScale;
    }

    public void CloseSettings()
    {
        AddToFadeOutT(settingsScreen);
        AddToFadeOutT(blackout3);

        Time.timeScale = 1f;
        Time.fixedDeltaTime = (1f / 60f) * Time.timeScale;
    }

    public void CloseGdpr()
    {
        if (PlayerPrefs.GetInt("GDPRevent") == 1)
        {
            AddToFadeOutT(GDPR);
            AddToFadeInN(settingsScreen);
        }
        else
        {
            PlayerPrefs.SetInt("GDPRevent", 1);
            PlayerPrefs.Save();

            //MaxSdk.SetHasUserConsent(true);

            AddToFadeOutT(GDPR);
            AddToFadeOutT(blackout3);
            AddToFadeOutT(blackout);
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
   
    public void ExtraRewardCallbackK()
    {
        rewardD += 300;
        RestartScene(true);
    }

    public void Upgrade(int i)
    {
        if (money < upgradePrices[i] || upgradeLevels[i] >= maxLevels[i])
            return;

        money -= upgradePrices[i];
        upgradeLevels[i]++;
        SaveDataA();

        UpgradeUpdateTextsS();
    }

    private void UpgradeUpdateTextsS()
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
