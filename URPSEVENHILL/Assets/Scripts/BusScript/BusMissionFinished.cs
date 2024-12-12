using System.Collections;
using RoleShopSystem;
using UnityEngine;

public class BusMissionFinished : MonoBehaviour {
    [Header ("Script Objects")]
    private BusShopData busShopData;
    public GameData gameData;
    public BusGameManager busGameManage;
    public SecretStationScript secret;
    private GameObject PlayerCar;
    private NoAccidentBus accidentBus;

    [Header ("Load Time Control")]
    private float waitTime = 2.0f;
    private bool saveOnce = false;

    void Start () {
        PlayerCar = GameObject.FindWithTag ("Player");
        accidentBus = PlayerCar.GetComponent<NoAccidentBus> ();

        busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
        gameData = ReadWriteAllRoles.ReadGameProp (gameData);
    }

    private void MissionFinished () {
        SaveMoneyAndXp ();
        NoAccidentAchievement ();
        ArrangementAchievement ();
        WithBaggagePassengerAchievement ();
        FirstCityAchievement ();
        DropPassengerAchievement ();
        SecretAchievement ();
        MoneyFromPassengersAchievement ();
        AllRotationAchievement ();
        BaggagePassengerMoneyAchievement ();
        GenerelMissionCompleteAchievement ();
        GenerelMoneyAchievement ();
        GenerelXpAchievement ();
        GeneralSkillAchievement ();
        SaveAchievements ();
        FinishedValues ();
        saveOnce = true;

    }

    private void FinishedValues () {
        busGameManage.missionCompleteScreen.SetActive (true);
        busGameManage.gainedMoneyText.text = ":" + busGameManage.money;
        busGameManage.gainedXpText.text = ":" +busGameManage.xp;
        busGameManage.finishedTimeText.text = ":" + (int)busGameManage.finishTime;
        busGameManage.completePassengerText.text = "Yolcular:" + busGameManage.passengerCounter;
        busGameManage.completeWithBaggagePassengerText.text = "Bagajlı Yolcular:" + busGameManage.withBaggagePassengerCounter;
        busGameManage.completeDroppingText.text = "İnen Yolcular:" + busGameManage.droppingPassenger;
        busGameManage.usedDiscountText.text = "Kullanılan İndirim:" + busGameManage.discountCurrentSkillCounter;
        busGameManage.usedArrangementText.text = "Kullanılan Düzen Getiren:" + busGameManage.arrangementSkillCounter;
        if(busGameManage.remainingTime<=0)
        {
            busGameManage.finishedFrame.color=Color.red;
            busGameManage.missionFailedText.text="Görev Başarısız";
        }
        Destroy (GameObject.FindWithTag ("busMissionFollower"));
        Time.timeScale = 0f;
    }

    private void OnTriggerStay (Collider oyuncu) {
        if (oyuncu.CompareTag("Player")) {
            waitTime -= 1 * Time.deltaTime;

            if (waitTime <= 0) {
                if (saveOnce == false) {
                    MissionFinished ();
                }
            }
        }

    }
    private void SaveMoneyAndXp () {
        busGameManage.money += (int) busGameManage.remainingTime *5;
        gameData.totalMoney += busGameManage.money;
        gameData.totalXp += busGameManage.xp +10*busGameManage.ComfortDrivePercent;
    }

    private void NoAccidentAchievement () {
        if (busShopData.busAchievementItem.unlockedNoAccidentLevel <= busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1) {
            if (accidentBus.accident == false) {
                busShopData.busAchievementItem.noAccidentValue++;
                Debug.Log ("AccAchieveSaved");
            }
        }
    }
    private void ArrangementAchievement () {
        if (busShopData.busAchievementItem.unlockedXArrangementLevel <= busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1) {

            busShopData.busAchievementItem.xArrangementValue += busGameManage.arrangementSkillCounter;
            Debug.Log ("SatAchieveSaved");

        }
    }
    private void WithBaggagePassengerAchievement () {
        if (busShopData.busAchievementItem.unlockedXSuitcasePassengerLevel <= busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1) {
            busShopData.busAchievementItem.xSuitcasePassengerValue += busGameManage.withBaggagePassengerCounter;
            Debug.Log ("SuitcaseAchieveSaved");

        }
    }

    private void FirstCityAchievement () {
        if (busShopData.busAchievementItem.unlockedXMissionCompleteAtLosBizaLevel <= busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1) {
            if (accidentBus.isLosBiza == true) {
                busShopData.busAchievementItem.xMissionCompleteAtLosBizaValue++;
                Debug.Log ("FirstCityAchieveSaved");
                accidentBus.isSevenHill = false;
                accidentBus.isLosBiza = false;

            }
        }

    }

    private void DropPassengerAchievement () {
        if (busShopData.busAchievementItem.unlockedXDropThePassengerLevel <= busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1) {
            busShopData.busAchievementItem.xDropThePassengerValue += (busGameManage.droppingPassenger) * -1;
            Debug.Log ("DropAchieveSaved");

        }
    }
    private void SecretAchievement () {
        if (busShopData.busAchievementItem.secretBusStopValue == true) {
            if (secret.secretStationFound == true) {
                busShopData.busAchievementItem.secretBusStopValue = true;
                Debug.Log ("SecretAchieveSaved");
            }
        }
    }

    private void MoneyFromPassengersAchievement () {
        if (busShopData.busAchievementItem.unlockedGetOnPassengerMoneyLevel <= busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1) {
            busShopData.busAchievementItem.getOnPassengerMoneyValue += busGameManage.money;

        }

    }
    private void AllRotationAchievement () {
        if (busShopData.busAchievementItem.allBusRotationValue == false) {
            if (busGameManage.busRota1Control == 1 && busGameManage.busRota2Control == 1 && busGameManage.busRota3Control == 1 && busGameManage.busRota4Control == 1) {
                busShopData.busAchievementItem.allBusRotationValue = true;
                Debug.Log ("AllRotationCompletedAchieveSaved");
            }
        }
    }
    private void BaggagePassengerMoneyAchievement () {
        if (busShopData.busAchievementItem.unlockedGainXMoneyOnXSuitcasePassengerLevel <= busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1) {
            busShopData.busAchievementItem.gainXMoneyOnXSuitcasePassengerValue += busGameManage.baggageMoney;

            Debug.Log ("SuitcasePassengerMoneyAchieveSaved");
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
            busShopData.generalAchievementItem.gainMoneyFromMissionValue += busGameManage.money;
            Debug.Log ("GeneralMoneyAchieveSaved");
        }
    }
    private void GenerelXpAchievement () {
        if (busShopData.generalAchievementItem.unlockedGainXExperienceLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
            busShopData.generalAchievementItem.gainXExperienceValue += busGameManage.xp;
            Debug.Log ("GeneralExperienceAchieveSaved");
        }
    }

    private void GeneralSkillAchievement () {
        if (busShopData.generalAchievementItem.unlockedXUseSkillLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
            busShopData.generalAchievementItem.xUseSkillValue += busGameManage.arrangementSkillCounter + busGameManage.discountSkillCounter;
        }
    }
    private void SaveAchievements () {

        ReadWriteAllRoles.WriteBusProp (busShopData);
        ReadWriteAllRoles.WriteGameProp (gameData);
    }

}