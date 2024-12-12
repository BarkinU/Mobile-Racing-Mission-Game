using System.Collections.Generic;
using RoleShopSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GarbageGameManager : MonoBehaviour {

    [Header ("Script Objects")]
    private GarbageShopData garbageShopData;
    public GameData gameData;

    [Header ("Texts")]
    public TextMeshProUGUI remainingTimeText;
    public TextMeshProUGUI wasteCurrentMoneyText;
    public TextMeshProUGUI recyclingCurrentMoneyText;
    public TextMeshProUGUI valuableCurrentMoneyText;
    public TextMeshProUGUI ironCurrentMoneyText;
    public TextMeshProUGUI wasteGarbageCounterText;
    public TextMeshProUGUI totalGarbageCounterText;
    public TextMeshProUGUI valuableMaterialCounterText;
    public TextMeshProUGUI recyclingGarbageCounterText;
    public TextMeshProUGUI compressionedCounterText;
    public TextMeshProUGUI ironCounterText;
    public TextMeshProUGUI magnetTenurText;
    public TextMeshProUGUI compressionTenurText;

    [Header ("Garbage Upgrades")]
    public int garbageCapacity;
    public int fastGatherPercent;
    public int chanceGatherPercent;
    public int recyclingIncreasePercent;

    [Header ("Garbage Counters")]
    public int recyclingGarbageCounter;
    public int valuableMaterialCounter;
    public int totalGarbageCounter;
    public int tempTotalGarbage;
    public int wasteGarbageCounter;
    public int ironCounter;
    public int compressionedCounter;
    public List<GameObject> garbagePlaceFirstCityList;
    public List<GameObject> garbagePlaceFirstCityRemovedList;
    public int garbagePlaceFirstCityCounter;
    public List<GameObject> garbagePlaceSecondCityList;
    public List<GameObject> garbagePlaceSecondCityRemovedList;
    public int firstCityGarbageLength;
    public int secondCityGarbageLength;
    public int garbagePlaceSecondCityCounter;

    [Header ("Money & Xp")]
    public int wasteMoney;
    public int valuableMoney;
    public int recyclingMoney;
    public int money = 0;
    public float remainingTime;

    public int xp = 0;

    [Range (0, 6)] public int garbagePointsCounter;

    public GarbageList garbageList;

    [Header ("Car Settings")]
    public PABLO RR;
    public GameObject garbageCar;

    public GameObject firstStartPosition;
    public GameObject secondStartPosition;
    private GameObject startPosition;

    [Header ("SkillValues")]
    public int magnetSkillCounter;
    public int compressionSkillCounter;
    public int compressionTenur = 4;
    private int magnetTenur = 4;
    public Button compressionButton;
    public Button magnetButton;
    public int magnetPower;

    [Header ("MissionCompleteValues")]
    public GameObject missionCompleteScreen;
    public TextMeshProUGUI finishedTimeText;
    public TextMeshProUGUI gainedMoneyText;
    public TextMeshProUGUI gainedXpText;
    public TextMeshProUGUI completeValuableText;
    public TextMeshProUGUI completeRecycleText;
    public TextMeshProUGUI completeWasteText;
    public TextMeshProUGUI usedMagnetText;
    public TextMeshProUGUI usedCompressionText,compressionedWarning;
    public GameObject MagnetButtonEffect;
    public GameObject CompressionButtonEffect;

    private void Awake () {
        SelectCitySpawn ();
    }

    private void SelectCitySpawn () {
        if (PlayerPrefs.GetInt ("isFirst") == 1) {
            startPosition = firstStartPosition;
        } else {
            startPosition = secondStartPosition;
        }

        Instantiate (garbageList.garbageVehicles[PlayerPrefs.GetInt ("pointer")], startPosition.transform.position, startPosition.transform.rotation);

        RR = GameObject.FindGameObjectWithTag ("Player").GetComponent<PABLO> ();
        garbageCar = GameObject.FindGameObjectWithTag ("Player");
    }
    private void Start () {

        ReadValuesFromMenu ();

    }

    private void ReadValuesFromMenu () {
        garbageShopData = ReadWriteAllRoles.ReadGarbageProp (garbageShopData);
        gameData = ReadWriteAllRoles.ReadGameProp (gameData);

        garbageCapacity = garbageShopData.garbageRoleItems.garbageSkillUpgradeLevel[garbageShopData.garbageRoleItems.unlockedGarbageCapacityLevel].garbageCapacityValue;

        fastGatherPercent = garbageShopData.garbageRoleItems.garbageSkillUpgradeLevel[garbageShopData.garbageRoleItems.unlockedFastGatherLevel].fastGatherValue;

        chanceGatherPercent = garbageShopData.garbageRoleItems.garbageSkillUpgradeLevel[garbageShopData.garbageRoleItems.unlockedChanceGatherLevel].chanceGatherValue;

        recyclingIncreasePercent = garbageShopData.garbageRoleItems.garbageSkillUpgradeLevel[garbageShopData.garbageRoleItems.unlockedRecyclingIncreaseLevel].recyclingIncreaseValue;

        compressionTenur = garbageShopData.garbageRoleItems.compressionTenur;
        magnetTenur = garbageShopData.garbageRoleItems.magnetTenur;

        firstCityGarbageLength = garbagePlaceFirstCityList.Count;
        secondCityGarbageLength = garbagePlaceSecondCityList.Count;

        garbagePlaceFirstCityCounter = garbagePlaceFirstCityList.Count;
        garbagePlaceSecondCityCounter = garbagePlaceSecondCityList.Count;

        magnetTenurText.text = magnetSkillCounter + "/" + magnetTenur;
        compressionTenurText.text = compressionSkillCounter + "/" + compressionTenur;
        Time.timeScale = 1f;
    }
    private void Update () {
        TextActuator ();
    }

    private void TextActuator () {
        remainingTime+=1*Time.deltaTime;
        remainingTimeText.text=":"+(int)remainingTime;

        wasteGarbageCounterText.text = "Değersiz Çöp:" + wasteGarbageCounter;
        totalGarbageCounterText.text = "Toplam Çöp:" + totalGarbageCounter + "/" + garbageCapacity;
        valuableMaterialCounterText.text = "Değerli Eşyalar:" + valuableMaterialCounter;
        recyclingGarbageCounterText.text = "Geri Dönüşüm:" + recyclingGarbageCounter;
        ironCounterText.text = "Demir:" + ironCounter;

    }

    public void CompressionSkill () {
        compressionSkillCounter++;
        if (compressionSkillCounter <= compressionTenur) {
            StartCoroutine(CompressSkillShining());
            tempTotalGarbage = totalGarbageCounter * 1 / 4;
            compressionedCounter += tempTotalGarbage;
            totalGarbageCounter-=tempTotalGarbage;
            compressionTenurText.text = compressionSkillCounter + "/" + compressionTenur;
            compressionedCounterText.text = "Sıkıştırılmış:" + compressionedCounter;
            if (compressionSkillCounter == compressionTenur) {
                compressionButton.interactable = false;
            }

        }
    }

    public void MagnetSkill () {
        magnetSkillCounter++;
        if (magnetSkillCounter <= magnetTenur) {
            StartCoroutine(MagnetEffectShine());
            int magnetPowerIncrease = magnetPower * 20 / 100 + 100;
            magnetPower += magnetPowerIncrease;
            magnetTenurText.text = magnetSkillCounter + "/" + magnetTenur;
            if (magnetSkillCounter == magnetTenur) {
                magnetButton.interactable = false;
            }

        }
    }

    public void AgainMission () {
        PlayerPrefs.SetInt ("rolePointer", 2);
        SceneLoader.Load (SceneLoader.Scene.RealScene);
    }

    IEnumerator MagnetEffectShine()
    {
        MagnetButtonEffect.SetActive(true);

        yield return  new WaitForSeconds(0.1f);

        MagnetButtonEffect.SetActive(false);
    }

    IEnumerator CompressSkillShining()
    {
        CompressionButtonEffect.SetActive(true);

        yield return  new WaitForSeconds(0.1f);

        CompressionButtonEffect.SetActive(false);
    }

}