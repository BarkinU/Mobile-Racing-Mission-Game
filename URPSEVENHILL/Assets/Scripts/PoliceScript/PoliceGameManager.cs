using RoleShopSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PoliceGameManager : MonoBehaviour {

    [Header ("Get Objects")]
    public Transform thiefPath;
    private GameObject policeMissionPoint;
    public GameObject PoliceMissionMarker;
    public GameObject thiefMarker;
    [SerializeField] private PoliceMission policeMission;
    private PABLO RR;
    public Button catchButton;
    PoliceShopData policeShopData;
    BusShopData busShopData;
    GameData gameData;
    private GameObject thiefObject;
    private CrashThief crashThief;
    private RobberyMoney robberyMoney;
    private NoAccidentBus accidentPolice;

    [Header ("Texts")]
    public TextMeshProUGUI remainingTimeText;
    public TextMeshProUGUI remainingTimeTopPanelText;
    public TextMeshProUGUI catchingTimeText;
    public TextMeshProUGUI followUpTimeText;
    public TextMeshProUGUI policeThiefDistanceText;
    public TextMeshProUGUI chaseCounterText;
    public TextMeshProUGUI vanguardCounterText;

    [Header ("Crime Location Settings")]
    public GameObject[] firstCityCrimeLocations;
    public GameObject[] firstCityAiSpawnLocations;
    public GameObject[] firstPath;
    public GameObject[] secondCityCrimeLocations;
    public GameObject[] secondCityAiSpawnLocations;
    public GameObject[] secondPath;
    private float firstCrimeDistance = 0.0f;
    private float secondCrimeDistance = 0.0f;
    private float policeThiefDistance;
    public int randomCrime;

    [Header ("Upgrade Values")]
    public int decreaseThiefSpeed;
    private int catchTimeDecrease;
    public int catchMoneyIncrease;
    private int catchCrimeSceneChance;
    private int totalChance;
    [Header ("Time&Money Calculate")]
    public float remainingTimeCrime;
    public float finishingTime;
    public bool isEscaped;
    public bool isCaughtFromScout;
    private int money;
    private int xp;
    private int extraMoneyForCrimeScene;
    private int mainExtraMoney;
    private int scoutMoney;
    private int mainCatchTime = 10;
    private float catchingTime = 5;
    public bool isCatchCrimeScene;
    public bool isFinished;

    public PoliceList policeList;

    [Header ("Car Settings")]
    public GameObject policeCar;

    public GameObject firstStartPosition;
    public GameObject secondStartPosition;
    private GameObject startPosition;

    [Header ("SkillValues")]
    private int chaseTenur = 4;
    private int vanguardTenur = 4;
    public Button chaseButton;
    public Button vanguardButton;
    private int chaseSkillCounter;
    private int vanguardSkillCounter;
    private bool isChasing;
    private float mainChaseTime = 15f;
    private float chaseTime = 15f;
    private int bringDownGangChance;
    private int totalBringDownChance;
    private bool isGangCatched;
    public GameObject catchCloser;
    public GameObject chaseOpener;

    [Header ("MissionCompleteValues")]
    public GameObject missionCompleteScreen;
    public TextMeshProUGUI finishedTimeText;
    public TextMeshProUGUI gainedMoneyText;
    public TextMeshProUGUI gainedXpText;
    public TextMeshProUGUI completeBeforeRobberyTimeText;
    public TextMeshProUGUI completeFollowTimeText;
    public TextMeshProUGUI completeAfterRobberyText;
    public TextMeshProUGUI usedVanguardText;
    public TextMeshProUGUI usedChaseText, missionFailedText;
    public Image headerFrameImage;
    public int beforeRobberyTime;
    private int afterRobberyTime;
    public bool bigMapThiefIsOk = false;
    public NavigationScript navigationScript;
    public GameObject VanguardButtonEffect;
    public GameObject ChaseButtonEffect;
    private void Awake () {
        SelectCitySpawn ();
    }

    private void SelectCitySpawn () {
        if (PlayerPrefs.GetInt ("isFirst") == 1) {
            startPosition = firstStartPosition;
        } else {
            startPosition = secondStartPosition;
        }

        Instantiate (policeList.policeVehicles[PlayerPrefs.GetInt ("pointer")], startPosition.transform.position, startPosition.transform.rotation);

        policeCar = GameObject.FindGameObjectWithTag ("Player");
        RR = policeCar.GetComponent<PABLO> ();
        crashThief = RR.GetComponent<CrashThief> ();
        robberyMoney = GameObject.FindObjectOfType<RobberyMoney> ();
        policeMissionPoint = Resources.Load ("PoliceMission") as GameObject;
        policeMission = policeMissionPoint.GetComponent<PoliceMission> ();
        accidentPolice = GameObject.FindObjectOfType<NoAccidentBus> ();
        SpawnFirstCrime ();

    }
    private void Start () {
        ReadValuesFromMenu ();
    }

    private void ReadValuesFromMenu () {

        gameData = ReadWriteAllRoles.ReadGameProp (gameData);
        busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
        policeShopData = ReadWriteAllRoles.ReadPoliceProp (policeShopData);

        decreaseThiefSpeed = policeShopData.policeRoleItems.policeSkillUpgradeLevel[policeShopData.policeRoleItems.unlockedDecreaseThiefSpeedLevel].decreaseThiefSpeedValue;

        catchTimeDecrease = policeShopData.policeRoleItems.policeSkillUpgradeLevel[policeShopData.policeRoleItems.unlockedDecreaseCatchTimeLevel].decreaseCatchTimeValue;

        catchMoneyIncrease = policeShopData.policeRoleItems.policeSkillUpgradeLevel[policeShopData.policeRoleItems.unlockedCatchBonusLevel].catchBonusValue;

        catchCrimeSceneChance = policeShopData.policeRoleItems.policeSkillUpgradeLevel[policeShopData.policeRoleItems.unlockedCatchInCrimeSceneChanceLevel].catchInCrimeSceneChanceValue;

        chaseTenur = policeShopData.policeRoleItems.chaseTenur;
        vanguardTenur = policeShopData.policeRoleItems.vanguardTenur;

        vanguardCounterText.text = vanguardSkillCounter + "/" + vanguardTenur;
        chaseCounterText.text = chaseSkillCounter + "/" + chaseTenur;

        mainCatchTime -= (mainCatchTime * catchTimeDecrease / 100);
        catchingTime = mainCatchTime;
        mainExtraMoney += (mainExtraMoney * catchMoneyIncrease / 100);
        extraMoneyForCrimeScene += mainExtraMoney;
        scoutMoney = mainExtraMoney * 3;

        Time.timeScale = 1f;
    }

    private void Update () {
        CatchEvents ();
    }

    private void CatchEvents () {
        if (remainingTimeCrime > 0) {
            remainingTimeCrime -= Time.deltaTime;
            finishingTime += Time.deltaTime;
        }
        if (isEscaped == true) {

            thiefObject = GameObject.FindGameObjectWithTag ("AiThief").gameObject;
            policeThiefDistance = Vector3.Distance (policeCar.transform.position, thiefObject.transform.position);
            if (remainingTimeCrime > 0) {
                if (policeThiefDistance <= 50.0f) {
                    if (isChasing == false) {
                        catchingTime -= 1 * Time.deltaTime;
                        catchingTimeText.text = "Catch Time : " + catchingTime;
                        if (catchingTime <= 0) {
                            catchButton.interactable = true;
                            chaseButton.interactable = true;
                            catchingTime = 0;
                        }
                    } else {
                        chaseTime -= 1 * Time.deltaTime;
                        if (chaseTime < 0) {
                            totalBringDownChance = Random.Range (0, 1001);
                            if (totalBringDownChance < bringDownGangChance) {
                                if (isFinished == false) {
                                    isGangCatched = true;
                                    FinishedMission ();
                                }
                            } else {
                                if (isFinished == false) {
                                    FinishedMission ();
                                }
                            }

                        }
                    }

                } else {
                    catchButton.interactable = false;
                    catchingTime = mainCatchTime;
                }
            } else {
                if (isFinished == false) {
                    afterRobberyTime = (int) remainingTimeCrime;
                    FinishedMission ();
                }
            }
        } else {
            if (isCatchCrimeScene == true) {
                if (isFinished == false) {
                    FinishedMission ();
                }
            }
        }

        remainingTimeText.text = "Kalan Süre :" + (int) remainingTimeCrime;
        remainingTimeTopPanelText.text = " " + (int) remainingTimeCrime;
        policeThiefDistanceText.text = "Hırsız Uzaklığı :" + policeThiefDistance;
        followUpTimeText.text = "Takip Süresi :" + (int) chaseTime;
    }
    public void CatchButtonEvent () {
        catchButton.interactable = false;
        if (isFinished == false) {
            FinishedMission ();

        }
    }
    private void SpawnFirstCrime () {
        if (PlayerPrefs.GetInt ("isFirst") == 1) {
            randomCrime = Random.Range (0, firstCityCrimeLocations.Length);
            firstCityCrimeLocations[randomCrime].SetActive (true);
            GameObject.Instantiate (policeMissionPoint, firstCityCrimeLocations[randomCrime].transform.position, Quaternion.identity);
            firstPath[randomCrime].SetActive (true);
            thiefPath = firstPath[randomCrime].transform;

            firstCrimeDistance = Vector3.Distance (policeCar.transform.position, firstCityCrimeLocations[randomCrime].transform.position);
            remainingTimeCrime = firstCrimeDistance / 8;
            if(remainingTimeCrime>100)
            {
                remainingTimeCrime -=remainingTimeCrime / 3;
            }else
            {
                remainingTimeCrime +=remainingTimeCrime / 4;
            }
        } else {
            randomCrime = Random.Range (0, secondCityCrimeLocations.Length);
            secondCityCrimeLocations[randomCrime].SetActive (true);
            GameObject.Instantiate (policeMissionPoint, secondCityCrimeLocations[randomCrime].transform.position, Quaternion.identity);
            secondPath[randomCrime].SetActive (true);
            thiefPath = secondPath[randomCrime].transform;

            secondCrimeDistance = Vector3.Distance (policeCar.transform.position, secondCityCrimeLocations[randomCrime].transform.position);
            remainingTimeCrime = secondCrimeDistance / 8;
            if(remainingTimeCrime>100)
            {
                remainingTimeCrime -=remainingTimeCrime/7/2;
            }else
            {
                remainingTimeCrime +=remainingTimeCrime / 4;
            }
        }

    }
    public void VanguardButtonEvent () {
        vanguardSkillCounter++;

        if (vanguardSkillCounter <= vanguardTenur) {
            StartCoroutine(VanguardEffectShine());
            totalChance = Random.Range (0, 1001);
            if (totalChance <= catchCrimeSceneChance) {
                isCaughtFromScout = true;
                money += scoutMoney;
                if (isFinished == false) {
                    beforeRobberyTime = (int) finishingTime;
                    FinishedMission ();
                }
            }

            vanguardCounterText.text = vanguardSkillCounter + "/" + vanguardTenur;
            if (vanguardSkillCounter == vanguardTenur) {
                vanguardButton.interactable = false;
            }
        }

    }

    public void ChaseButtonEvent () {
        chaseSkillCounter++;
        if (chaseSkillCounter <= chaseTenur) {
            StartCoroutine(ChaseSkillShining());
            if (chaseSkillCounter == 1) {
                isChasing = true;
                chaseOpener.SetActive (true);
                catchCloser.SetActive (false);
            }
            chaseCounterText.text = chaseSkillCounter + "/" + chaseTenur;
            bringDownGangChance += 200;
            if (chaseSkillCounter == chaseTenur) {
                chaseButton.interactable = false;
            }

        }
    }

    private void XpMoneyIncrease () {
        if (isCaughtFromScout == false) {
            money += extraMoneyForCrimeScene;
        }
        if (isGangCatched == true) {
            money += 1000;
        }
        money += 500 + (int) remainingTimeCrime*3+(int)firstCrimeDistance/3;
        xp += 50 + (int)firstCrimeDistance/15;

    }

    private void CrimeSceneBeforeCatchAchievement () {
        if (policeShopData.policeAchievementItem.unlockedXCatchWithScoutLevel <= policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel.Length - 1) {
            if (isCaughtFromScout == true) {
                policeShopData.policeAchievementItem.xCatchWithScoutValue++;
                Debug.Log ("ScoutAchieve");
            }
        }

    }

    private void CatchFugitiveThiefAchievement () {
        if (policeShopData.policeAchievementItem.unlockedXCatchThiefLevel <= policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel.Length - 1) {
            if (isEscaped == true) {
                policeShopData.policeAchievementItem.xCatchThiefValue++;
                Debug.Log ("CatchFugutiveAchieve");
            }
        }
    }
    private void CatchCrimeSceneAchievement () {
        if (policeShopData.policeAchievementItem.unlockedXCatchThiefCrimeSceneLevel <= policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel.Length - 1) {
            if (isCatchCrimeScene == true) {
                policeShopData.policeAchievementItem.xCatchThiefCrimeSceneValue++;
                Debug.Log ("CatchCrimeSceneAchieve");
            }
        }
    }

    private void GainBonusMoneyFromBlockingRobberyAchievement () {
        if (policeShopData.policeAchievementItem.unlockedGainXBonusMoneyFromBlockingRobberyLevel <= policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel.Length - 1) {
            if (isEscaped == false) {
                policeShopData.policeAchievementItem.gainXBonusMoneyFromBlockingRobberyValue += extraMoneyForCrimeScene;
                Debug.Log ("GainBonusMoneyAchieve");
            }
        }

    }

    private void CrashThiefAchievement () {
        if (policeShopData.policeAchievementItem.unlockedXCrashThiefLevel <= policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel.Length - 1) {
            policeShopData.policeAchievementItem.xCrashThiefValue += crashThief.CrashThiefNumber;
            Debug.Log ("CrashThiefAchieve");
        }
    }

    private void FindRobberyMoneyInMapAchievement () {
        if (robberyMoney.robberyMoneyFound == true) {
            policeShopData.policeAchievementItem.findRobberyMoneyValue = true;
            Debug.Log ("RobberyMoneyFound");
        }
    }
    private void SevenhillRobberyAchievement () {
        if (policeShopData.policeAchievementItem.unlockedSolveAllCaseInSevenhillLevel <= policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel.Length - 1) {
            policeShopData.policeAchievementItem.solveAllCaseInSevenhillValue++;
            Debug.Log ("SevenHillRobberyMoney");
        }
    }

    private void NoAccidentPoliceAchievement () {
        if (policeShopData.policeAchievementItem.unlockedNoAccidentPoliceLevel <= policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel.Length - 1) {
            if (accidentPolice.accident == false) {
                policeShopData.policeAchievementItem.noAccidentPoliceValue++;
            }
        }
    }
    private void UseXVanguardSkillAchievement () {
        if (policeShopData.policeAchievementItem.unlockedUseXVanguardLevel <= policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel.Length - 1) {

            policeShopData.policeAchievementItem.useXVanguardValue += vanguardSkillCounter;
            ReadWriteAllRoles.WritePoliceProp (policeShopData);
            Debug.Log ("PoliceNewCarAchieveSaved");

        }
    }

    private void GenerelMissionCompleteAchievement () {
        if (busShopData.generalAchievementItem.unlockedCompleteXMissionLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
            busShopData.generalAchievementItem.completeXMissionValue++;
            Debug.Log ("GeneralMissionCompleteAchieveSaved");
        }
    }
    private void GenerelMoneyAchievement () {
        if (busShopData.generalAchievementItem.unlockedGainMoneyFromMissionLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
            busShopData.generalAchievementItem.gainMoneyFromMissionValue += money;
            Debug.Log ("GeneralMoneyAchieveSaved");
        }
    }
    private void GenerelXpAchievement () {
        if (busShopData.generalAchievementItem.unlockedGainXExperienceLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
            busShopData.generalAchievementItem.gainXExperienceValue += xp;
            Debug.Log ("GeneralExperienceAchieveSaved");
        }
    }

    private void GeneralChaseSkillAchievement () {
        if (busShopData.generalAchievementItem.unlockedXUseSkillLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
            busShopData.generalAchievementItem.xUseSkillValue += vanguardSkillCounter + chaseSkillCounter;
        }
    }

    private void XpMoneyAchievementsSaveAndLevelControl () {

        if (remainingTimeCrime >= 0) {
            money += money/4;
            xp += xp/3;
        }

        gameData.totalMoney += money;
        gameData.totalXp += xp;

        ReadWriteAllRoles.WriteGameProp (gameData);
        ReadWriteAllRoles.WriteBusProp (busShopData);
        ReadWriteAllRoles.WritePoliceProp (policeShopData);
    }

    private void FinishedMission () {

        XpMoneyIncrease ();
        CrimeSceneBeforeCatchAchievement ();
        CatchFugitiveThiefAchievement ();
        CatchCrimeSceneAchievement ();
        GainBonusMoneyFromBlockingRobberyAchievement ();
        CrashThiefAchievement ();
        FindRobberyMoneyInMapAchievement ();
        SevenhillRobberyAchievement ();
        NoAccidentPoliceAchievement ();
        UseXVanguardSkillAchievement ();
        GenerelXpAchievement ();
        GenerelMoneyAchievement ();
        GenerelMissionCompleteAchievement ();
        GeneralChaseSkillAchievement ();
        XpMoneyAchievementsSaveAndLevelControl ();

        afterRobberyTime = (int) finishingTime - beforeRobberyTime;

        if(chaseSkillCounter>0)
        {
            mainChaseTime=15;

        }else
        {
            mainChaseTime=0;
        }

        missionCompleteScreen.SetActive (true);
        gainedMoneyText.text = ":" + money;
        gainedXpText.text = ":" + xp;
        finishedTimeText.text = ":" + (int) finishingTime;
        usedChaseText.text = "Kullanılan Takip :" + chaseSkillCounter;
        usedVanguardText.text = "Kullanılan Öncü Birlik :" + vanguardSkillCounter;
        completeBeforeRobberyTimeText.text = "Soygun Öncesi Süre:" + beforeRobberyTime;
        completeFollowTimeText.text = "Toplam Takip Süresi :" + mainChaseTime;
        completeAfterRobberyText.text = "Soygun Sonrası Tutuklama Süresi:" + afterRobberyTime;
        isFinished = true;

        if (remainingTimeCrime <= 0) {
            missionFailedText.text = "Görev Başarısız";
            headerFrameImage.color = Color.red;
        }

        Time.timeScale = 0f;
    }

    public void AgainMission () {
        PlayerPrefs.SetInt ("rolePointer", 4);
        SceneLoader.Load (SceneLoader.Scene.RealScene);
    }

    IEnumerator VanguardEffectShine()
    {
        VanguardButtonEffect.SetActive(true);

        yield return  new WaitForSeconds(0.1f);

        VanguardButtonEffect.SetActive(false);
    }

    IEnumerator ChaseSkillShining()
    {
        ChaseButtonEffect.SetActive(true);

        yield return  new WaitForSeconds(0.1f);

        ChaseButtonEffect.SetActive(false);
    }
}