using System.Collections;
using RoleShopSystem;
using UnityEngine;

public class GarbageEmptying : MonoBehaviour {
    [Header ("Script Objects")]
    GarbageShopData garbageShopData;
    BusShopData busShopData;
    public GameData gameData;
    public GarbageGameManager garbageGameManage;
    private GameObject PlayerCar;
    private AccidentGarbageBox GarbageBoxAcc;
    public MostValuableStuff mostValuableStuff;
    public GameObject emptyHud;

    [Header ("Money & Counter Values")]
    private int preciousMoney = 220;
    private int moneyIncreaseRate;
    private int recycleMoney = 80;
    private int wasteMoney = 30;
    private int ironMoney = 130;
    private int garbageEmptyingCounter;

    [Header ("Chance Rate")]
    private int valuableMaterialChance = 100;
    private int recyclingGarbageChance = 300;

    [Header ("Load Time Control")]
    private float waitTime = 1000;
    private float tempWait;
    private bool isEmpty;
    private bool isCompressionEmpty;

    void Start () {
        PlayerCar = GameObject.FindWithTag ("Player");
        GarbageBoxAcc = PlayerCar.GetComponent<AccidentGarbageBox> ();

        garbageShopData = ReadWriteAllRoles.ReadGarbageProp (garbageShopData);
        gameData = ReadWriteAllRoles.ReadGameProp (gameData);
        busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);

        SkillValuesCalculate ();
        ValuableMaterialChanceCalculate ();

    }

    private void SkillValuesCalculate () {
        valuableMaterialChance += (valuableMaterialChance * garbageGameManage.chanceGatherPercent) / 100;
        moneyIncreaseRate = (garbageGameManage.xp * garbageGameManage.recyclingIncreasePercent) / 100;

    }

    private void ValuableMaterialChanceCalculate () {
        int valuableMaterial = Random.Range (0, 1001);
        if (valuableMaterial <= valuableMaterialChance) {

        }
    }

    private void EmptyGarbage () {

        int chanceTotal = Random.Range (0, 1001);

        if (waitTime <= tempWait - 1 && waitTime > 0 && garbageGameManage.totalGarbageCounter > 0) {

            if (chanceTotal <= valuableMaterialChance) {

                garbageGameManage.money += preciousMoney;
                garbageGameManage.valuableMoney += preciousMoney;
                garbageGameManage.valuableMaterialCounter++;
                garbageGameManage.xp += 25;
                StartCoroutine (CoroutineValuable ());

                Debug.Log ("Lucky Day");
            } else if (chanceTotal > valuableMaterialChance && chanceTotal <= recyclingGarbageChance) {

                garbageGameManage.recyclingGarbageCounter++;
                garbageGameManage.money += recycleMoney;
                garbageGameManage.recyclingMoney += recycleMoney;
                garbageGameManage.xp += 8;
                StartCoroutine (CoroutineRecycling ());

            } else {
                garbageGameManage.wasteGarbageCounter++;
                garbageGameManage.money += wasteMoney;
                garbageGameManage.wasteMoney += wasteMoney;
                garbageGameManage.xp += 5;
                StartCoroutine (CoroutineWaste ());
            }

            if (garbageGameManage.totalGarbageCounter >= 0) {
                garbageGameManage.totalGarbageCounter--;
                garbageEmptyingCounter++;
            }

            tempWait--;
            if (garbageGameManage.totalGarbageCounter <= 0) {

                garbageGameManage.xp += 10;

            }

            if (isCompressionEmpty == false && garbageGameManage.totalGarbageCounter <= 0) {
                    garbageGameManage.totalGarbageCounter += garbageGameManage.compressionedCounter;
                    garbageGameManage.compressionedWarning.gameObject.SetActive (true);
                    isCompressionEmpty=true;
            }

        } else {
            if (garbageGameManage.totalGarbageCounter == 0) {
                MissionCompleteSaveFunction ();
            }
        }
    }

    IEnumerator CoroutineValuable () {
        garbageGameManage.valuableCurrentMoneyText.text = "+" + preciousMoney;
        yield return new WaitForSeconds (0.3f); // 1.5 saniye bekle
        garbageGameManage.valuableCurrentMoneyText.text = " ";
    }
    IEnumerator CoroutineWaste () {
        garbageGameManage.wasteCurrentMoneyText.text = "+" + wasteMoney;
        yield return new WaitForSeconds (0.3f); // 1.5 saniye bekle
        garbageGameManage.wasteCurrentMoneyText.text = " ";
    }
    IEnumerator CoroutineRecycling () {
        garbageGameManage.recyclingCurrentMoneyText.text = "+" + recycleMoney;
        yield return new WaitForSeconds (0.3f); // 1.5 saniye bekle
        garbageGameManage.recyclingCurrentMoneyText.text = " ";
    }

    private void ValuableStuffAchievement () {
        if (garbageShopData.garbageAchievementItem.unlockedXPreciousStuffLevel <= garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel.Length - 1) {
            garbageShopData.garbageAchievementItem.xPreciousStuffValue += garbageGameManage.valuableMaterialCounter;
            Debug.Log ("ValuableAchieveSaved");
        }
    }

    private void AccidentGarbageBoxAchievement () {
        if (garbageShopData.garbageAchievementItem.unlockedXPreciousStuffLevel <= garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel.Length - 1) {
            garbageShopData.garbageAchievementItem.crashXGarbageBoxValue += GarbageBoxAcc.garbageBoxAccidentNumber;
            Debug.Log ("CrashGarbageAchieveSaved");
        }
    }

    private void MoneyFromRecyclingAchievement () {
        if (garbageShopData.garbageAchievementItem.unlockedXPreciousStuffLevel <= garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel.Length - 1) {
            garbageShopData.garbageAchievementItem.xGainMoneyRecyclingValue += garbageGameManage.recyclingMoney;
            Debug.Log ("RecyclingMoneyAchieveSaved");
        }

    }

    private void CollectGarbageAchievement () {
        if (garbageShopData.garbageAchievementItem.unlockedXPreciousStuffLevel <= garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel.Length - 1) {
            garbageShopData.garbageAchievementItem.xCollectGarbageValue += garbageGameManage.totalGarbageCounter;
            Debug.Log ("CollectGarbageAchieveSaved");
        }
    }

    private void MostValuableAchieve () {
        if (garbageShopData.garbageAchievementItem.mostPreciousValue == false) {
            if (mostValuableStuff.mostValuableFound == true) {
                garbageShopData.garbageAchievementItem.mostPreciousValue = true;
                Debug.Log ("MostPreciousAchieveSaved");
            }
        }

    }

    private void GarbageEmptyingAchievement () {
        if (garbageShopData.garbageAchievementItem.unlockedXTimesEmptyGarbageLevel <= garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel.Length - 1) {
            if (isEmpty == true) {
                garbageShopData.garbageAchievementItem.xTimesEmptyGarbageValue++;
                Debug.Log ("GarbageEmptyAchieveSaved");
            }
        }

    }

    private void PreciousStuffMoneyAchievement () {
        if (garbageShopData.garbageAchievementItem.unlockedXGainMoneyFromPreciousLevel <= garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel.Length - 1) {
            garbageShopData.garbageAchievementItem.xGainMoneyFromPreciousValue += garbageGameManage.valuableMoney;
            Debug.Log ("PreciousStuffMoneyAchieveSaved");
        }
    }

    private void MoreThanXGarbageEmptyAchievement () {
        if (garbageShopData.garbageAchievementItem.unlockedOneTimeXGarbageLevel <= garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel.Length - 1) {
            if (garbageEmptyingCounter >= 75) {
                garbageShopData.garbageAchievementItem.oneTimeXGarbageValue++;
                Debug.Log ("MoreThanXGarbageAchieveSaved");
            }
        }
    }

    private void SevenHillCityAchievement () {
        if (garbageShopData.garbageAchievementItem.unlockedXTimesSevenHillGarbageEmptyLevel <= garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel.Length - 1) {
            if (GarbageBoxAcc.isSevenHill == true) {
                garbageShopData.garbageAchievementItem.xTimesSevenHillGarbageEmptyValue++;
                Debug.Log ("FirstCityAchieveSaved");
                GarbageBoxAcc.isSevenHill = false;
                GarbageBoxAcc.isLosBiza = false;
            }
        }
    }

    private void GeneralSkillAchievement () {
        if (busShopData.generalAchievementItem.unlockedXUseSkillLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
            busShopData.generalAchievementItem.xUseSkillValue += garbageGameManage.magnetSkillCounter + garbageGameManage.compressionSkillCounter;
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
            busShopData.generalAchievementItem.gainMoneyFromMissionValue += garbageGameManage.money;
            Debug.Log ("GeneralMoneyAchieveSaved");
        }
    }
    private void GenerelXpAchievement () {
        if (busShopData.generalAchievementItem.unlockedGainXExperienceLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
            busShopData.generalAchievementItem.gainXExperienceValue += garbageGameManage.xp;
            Debug.Log ("GeneralExperienceAchieveSaved");
        }
    }

    private void SaveAchievements () {

        gameData.totalMoney += garbageGameManage.money;
        gameData.totalXp += garbageGameManage.xp;

        ReadWriteAllRoles.WriteGarbageProp (garbageShopData);
        ReadWriteAllRoles.WriteBusProp (busShopData);
        ReadWriteAllRoles.WriteGameProp (gameData);
    }
    private void OnTriggerEnter (Collider oyuncu) {
        if (oyuncu.CompareTag ("Player")) {
            emptyHud.SetActive (true);
            tempWait = waitTime;
            CollectGarbageAchievement ();
        }
    }
    private void OnTriggerStay (Collider oyuncu) {
        if (oyuncu.CompareTag ("Player")) {
            if (isEmpty == false) {
                waitTime -= 3 * Time.deltaTime;
                EmptyGarbage ();

            }
        }
    }

    private void MissionCompleteSaveFunction () {
        waitTime = 0;
        ValuableStuffAchievement ();
        AccidentGarbageBoxAchievement ();
        MoneyFromRecyclingAchievement ();
        PreciousStuffMoneyAchievement ();
        MostValuableAchieve ();
        GarbageEmptyingAchievement ();
        MoreThanXGarbageEmptyAchievement ();
        SevenHillCityAchievement ();
        GenerelMissionCompleteAchievement ();
        GenerelMoneyAchievement ();
        GeneralSkillAchievement ();
        GenerelXpAchievement ();
        SaveAchievements ();

        garbageGameManage.missionCompleteScreen.SetActive (true);
        garbageGameManage.gainedMoneyText.text = ":" + garbageGameManage.money + "$ +" + (ironMoney * garbageGameManage.ironCounter) + "$" + "=" + (garbageGameManage.money + ironMoney * garbageGameManage.ironCounter) + "$";
        garbageGameManage.gainedXpText.text = ":" + garbageGameManage.xp;
        garbageGameManage.finishedTimeText.text = ":" + (int) garbageGameManage.remainingTime;
        garbageGameManage.completeValuableText.text = "Değerli Eşyalar:" + garbageGameManage.valuableMaterialCounter;
        garbageGameManage.completeRecycleText.text = "Gerİ Dönüşüm:" + garbageGameManage.recyclingGarbageCounter;
        garbageGameManage.completeWasteText.text = "Değersiz Çöp:" + garbageGameManage.wasteGarbageCounter;
        garbageGameManage.usedCompressionText.text = "Kullanılan Sıkıştırma:" + garbageGameManage.compressionSkillCounter;
        garbageGameManage.usedMagnetText.text = "Kullanılan Manyetik Alan:" + garbageGameManage.magnetSkillCounter;

        Time.timeScale = 0f;

        isEmpty = true;
    }

    private void OnTriggerExit (Collider oyuncu) {
        if (oyuncu.CompareTag ("Player")) {
            waitTime = 1000;
            tempWait = waitTime;
            isEmpty = false;
            emptyHud.SetActive (false);
        }
    }
}