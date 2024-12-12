using System.Collections;
using CarShopSystem;
using RoleShopSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementRewardSystem : MonoBehaviour {
    [Header ("Get Objects")]
    BusShopData busShopData;
    public CarShopUI carShopUi;
    PoliceShopData policeShopData;
    GarbageShopData garbageShopData;
    TaxiShopData taxiShopData;
    FireShopData fireShopData;
    AmbulanceShopData ambulanceShopData;

    [Header ("Texts")]
    public TextMeshProUGUI[] valueTexts;
    public TextMeshProUGUI[] levelExplanationsTexts;
    public TextMeshProUGUI[] rewardTexts;
    public Image[] sliderImages;
    public Button[] achievementRewardButtons;
    public TextMeshProUGUI xpText, totalDiamondText, moneyText;
    public AudioClip collectAchievement;
    public AudioSource audioSource;

    private void Awake () {

    }
    private void OnEnable () {
        ReadAchievementValues ();
        xpText.text = " :" + carShopUi.gameData.totalXp;
        moneyText.text = " :" + carShopUi.gameData.totalMoney;
        totalDiamondText.text = " :" + carShopUi.gameData.totalDiamond;
        AchievementSliderValues ();
    }
    private void ReadAchievementValues () {
        garbageShopData = garbageShopData = ReadWriteAllRoles.ReadGarbageProp (garbageShopData);
        carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);
        busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
        policeShopData = policeShopData = ReadWriteAllRoles.ReadPoliceProp (policeShopData);
        fireShopData = fireShopData = ReadWriteAllRoles.ReadFireProp (fireShopData);
        ambulanceShopData = ambulanceShopData = ReadWriteAllRoles.ReadAmbulanceProp (ambulanceShopData);
        taxiShopData = taxiShopData = ReadWriteAllRoles.ReadTaxiProp (taxiShopData);
    }

    private void AchievementSliderValues () {
        //General Achievements
        sliderImages[0].fillAmount = (float) busShopData.generalAchievementItem.completeXMissionValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXMissionLevel].completeXMissionLevelValue;
        valueTexts[0].text = busShopData.generalAchievementItem.completeXMissionValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXMissionLevel].completeXMissionLevelValue;
        levelExplanationsTexts[0].text = busShopData.generalAchievementItem.unlockedCompleteXMissionLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
        rewardTexts[0].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXMissionLevel].completeXMissionReward;
        achievementRewardButtons[0].onClick.AddListener (() => CompleteXMissionAchievement ());

        sliderImages[1].fillAmount = (float) busShopData.generalAchievementItem.upgradeXRoleValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedUpgradeXRoleLevel].upgradeXRoleLevelValue;
        valueTexts[1].text = busShopData.generalAchievementItem.upgradeXRoleValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedUpgradeXRoleLevel].upgradeXRoleLevelValue;
        levelExplanationsTexts[1].text = busShopData.generalAchievementItem.unlockedUpgradeXRoleLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
        rewardTexts[1].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedUpgradeXRoleLevel].upgradeXRoleReward;
        achievementRewardButtons[1].onClick.AddListener (() => UpgradeXRoleAchievement ());

        sliderImages[2].fillAmount = (float) busShopData.generalAchievementItem.gainMoneyFromMissionValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedGainMoneyFromMissionLevel].gainMoneyFromMissionLevelValue;
        valueTexts[2].text = busShopData.generalAchievementItem.gainMoneyFromMissionValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedGainMoneyFromMissionLevel].gainMoneyFromMissionLevelValue;
        levelExplanationsTexts[2].text = busShopData.generalAchievementItem.unlockedGainMoneyFromMissionLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
        rewardTexts[2].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedGainMoneyFromMissionLevel].gainMoneyFromMissionReward;
        achievementRewardButtons[2].onClick.AddListener (() => GainXMoneyFromMissionAchievement ());

        sliderImages[3].fillAmount = (float) busShopData.generalAchievementItem.gainXExperienceValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedGainXExperienceLevel].gainXExperienceLevelValue;
        valueTexts[3].text = busShopData.generalAchievementItem.gainXExperienceValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedGainXExperienceLevel].gainXExperienceLevelValue;
        levelExplanationsTexts[3].text = busShopData.generalAchievementItem.unlockedGainXExperienceLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
        rewardTexts[3].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedGainXExperienceLevel].gainXExperienceReward;
        achievementRewardButtons[3].onClick.AddListener (() => GainXExperienceAchievement ());

        sliderImages[4].fillAmount = (float) busShopData.generalAchievementItem.buyCarValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedBuyCarLevel].buyCarLevelValue;
        valueTexts[4].text = busShopData.generalAchievementItem.buyCarValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedBuyCarLevel].buyCarLevelValue;
        levelExplanationsTexts[4].text = busShopData.generalAchievementItem.unlockedBuyCarLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
        rewardTexts[4].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedBuyCarLevel].buyCarReward;
        achievementRewardButtons[4].onClick.AddListener (() => BuyCarAchievement ());

        sliderImages[5].fillAmount = (float) busShopData.generalAchievementItem.spendXMoneyForCarUpgradeValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedSpendXMoneyForCarUpgradeLevel].spendXMoneyForCarUpgradeLevelValue;
        valueTexts[5].text = busShopData.generalAchievementItem.spendXMoneyForCarUpgradeValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedSpendXMoneyForCarUpgradeLevel].spendXMoneyForCarUpgradeLevelValue;
        levelExplanationsTexts[5].text = busShopData.generalAchievementItem.unlockedSpendXMoneyForCarUpgradeLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
        rewardTexts[5].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedSpendXMoneyForCarUpgradeLevel].spendXMoneyForCarUpgradeReward;
        achievementRewardButtons[5].onClick.AddListener (() => SpendXMoneyForCarUpgradeAchievement ());

        sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
        valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
        levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
        rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementReward;
        achievementRewardButtons[6].onClick.AddListener (() => CompleteXAchievement ());

        sliderImages[7].fillAmount = (float) busShopData.generalAchievementItem.xCarUpgradeValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedXCarUpgradeLevel].xCarUpgradeLevelValue;
        valueTexts[7].text = busShopData.generalAchievementItem.xCarUpgradeValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedXCarUpgradeLevel].xCarUpgradeLevelValue;
        levelExplanationsTexts[7].text = busShopData.generalAchievementItem.unlockedXCarUpgradeLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
        rewardTexts[7].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedXCarUpgradeLevel].xCarUpgradeReward;
        achievementRewardButtons[7].onClick.AddListener (() => XCarUpgradeAchievement ());

        sliderImages[8].fillAmount = (float) busShopData.generalAchievementItem.openXTenurValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedOpenXTenurLevel].openXTenurLevelValue;
        valueTexts[8].text = busShopData.generalAchievementItem.openXTenurValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedOpenXTenurLevel].openXTenurLevelValue;
        levelExplanationsTexts[8].text = busShopData.generalAchievementItem.unlockedOpenXTenurLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
        rewardTexts[8].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedOpenXTenurLevel].openXTenurReward;
        achievementRewardButtons[8].onClick.AddListener (() => OpenXTenur ());

        sliderImages[9].fillAmount = (float) busShopData.generalAchievementItem.xUseSkillValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedXUseSkillLevel].xUseSkillLevelValue;
        valueTexts[9].text = busShopData.generalAchievementItem.xUseSkillValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedXUseSkillLevel].xUseSkillLevelValue;
        levelExplanationsTexts[9].text = busShopData.generalAchievementItem.unlockedXUseSkillLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
        rewardTexts[9].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedXUseSkillLevel].xUseSkillReward;
        achievementRewardButtons[9].onClick.AddListener (() => UseXSkillAchievement ());

        //Bus Achievements
        sliderImages[10].fillAmount = (float) busShopData.busAchievementItem.getOnPassengerMoneyValue / (float) busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedGetOnPassengerMoneyLevel].getOnPassengerMoneyLevelValue;
        valueTexts[10].text = busShopData.busAchievementItem.getOnPassengerMoneyValue + "/" + busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedGetOnPassengerMoneyLevel].getOnPassengerMoneyLevelValue;
        levelExplanationsTexts[10].text = busShopData.busAchievementItem.unlockedGetOnPassengerMoneyLevel + "/" + (busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1);
        rewardTexts[10].text = "" + busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedGetOnPassengerMoneyLevel].getOnPassengerMoneyReward;
        achievementRewardButtons[10].onClick.AddListener (() => GetOnPassengerMoneyAchievement ());

        sliderImages[11].fillAmount = (float) busShopData.busAchievementItem.xSuitcasePassengerValue / (float) busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedXSuitcasePassengerLevel].xSuitcasePassengerLevelValue;
        valueTexts[11].text = busShopData.busAchievementItem.xSuitcasePassengerValue + "/" + busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedXSuitcasePassengerLevel].xSuitcasePassengerLevelValue;
        levelExplanationsTexts[11].text = busShopData.busAchievementItem.unlockedXSuitcasePassengerLevel + "/" + (busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1);
        rewardTexts[11].text = "" + busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedXSuitcasePassengerLevel].xSuitcasePassengerReward;
        achievementRewardButtons[11].onClick.AddListener (() => XSuitcasePassengerAchievement ());

        sliderImages[12].fillAmount = (float) busShopData.busAchievementItem.xArrangementValue / (float) busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedXArrangementLevel].xArrangementLevelValue;
        valueTexts[12].text = busShopData.busAchievementItem.xArrangementValue + "/" + busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedXArrangementLevel].xArrangementLevelValue;
        levelExplanationsTexts[12].text = busShopData.busAchievementItem.unlockedXArrangementLevel + "/" + (busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1);
        rewardTexts[12].text = "" + busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedXArrangementLevel].xArrangementReward;
        achievementRewardButtons[12].onClick.AddListener (() => UseXArrangementAchievement ());

        sliderImages[13].fillAmount = (float) busShopData.busAchievementItem.noAccidentValue / (float) busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedNoAccidentLevel].noAccidentLevelValue;
        valueTexts[13].text = busShopData.busAchievementItem.noAccidentValue + "/" + busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedNoAccidentLevel].noAccidentLevelValue;
        levelExplanationsTexts[13].text = busShopData.busAchievementItem.unlockedNoAccidentLevel + "/" + (busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1);
        rewardTexts[13].text = "" + busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedNoAccidentLevel].noAccidentReward;
        achievementRewardButtons[13].onClick.AddListener (() => NoAccidentAchievement ());

        sliderImages[14].fillAmount = (float) busShopData.busAchievementItem.xMissionCompleteAtLosBizaValue / (float) busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedXMissionCompleteAtLosBizaLevel].xMissionCompleteAtLosBizaLevelValue;
        valueTexts[14].text = busShopData.busAchievementItem.xMissionCompleteAtLosBizaValue + "/" + busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedXMissionCompleteAtLosBizaLevel].xMissionCompleteAtLosBizaLevelValue;
        levelExplanationsTexts[14].text = busShopData.busAchievementItem.unlockedXMissionCompleteAtLosBizaLevel + "/" + (busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1);
        rewardTexts[14].text = "" + busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedXMissionCompleteAtLosBizaLevel].xMissionCompleteAtLosBizaReward;
        achievementRewardButtons[14].onClick.AddListener (() => XMissionCompleteAtLosBizaAchievement ());

        sliderImages[15].fillAmount = (float) busShopData.busAchievementItem.xDropThePassengerValue / (float) busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedXDropThePassengerLevel].xDropThePassengerLevelValue;
        valueTexts[15].text = busShopData.busAchievementItem.xDropThePassengerValue + "/" + busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedXDropThePassengerLevel].xDropThePassengerLevelValue;
        levelExplanationsTexts[15].text = busShopData.busAchievementItem.unlockedXDropThePassengerLevel + "/" + (busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1);
        rewardTexts[15].text = "" + busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedXDropThePassengerLevel].xDropThePassengerReward;
        achievementRewardButtons[15].onClick.AddListener (() => XDropPassengerAchievement ());

        if (busShopData.busAchievementItem.secretBusStopValue == false) {
            sliderImages[16].fillAmount = 0.0f / 1.0f;
            levelExplanationsTexts[16].text = 0 + "/" + 1;
            valueTexts[16].text = 0 + "/" + 1;
        } else {
            sliderImages[16].fillAmount = 1.0f / 1.0f;
            levelExplanationsTexts[16].text = 1 + "/" + 1;
            valueTexts[16].text = 1 + "/" + 1;
        }
        rewardTexts[16].text = "" + busShopData.busAchievementItem.secretBusStopReward;
        achievementRewardButtons[16].onClick.AddListener (() => SecretBusStopAchievement ());

        if (busShopData.busAchievementItem.allBusRotationValue == false) {
            sliderImages[17].fillAmount = 0.0f / 1.0f;
            levelExplanationsTexts[17].text = 0 + "/" + 1;
            valueTexts[17].text = 0 + "/" + 1;
        } else {
            sliderImages[17].fillAmount = 1.0f / 1.0f;
            levelExplanationsTexts[17].text = 1 + "/" + 1;
            valueTexts[17].text = 1 + "/" + 1;
        }
        rewardTexts[17].text = "" + busShopData.busAchievementItem.allBusRotationReward;
        achievementRewardButtons[17].onClick.AddListener (() => AllBusRotationAchievement ());

        sliderImages[18].fillAmount = (float) busShopData.busAchievementItem.gainXMoneyOnXSuitcasePassengerValue / (float) busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedGainXMoneyOnXSuitcasePassengerLevel].gainXMoneyOnXSuitcasePassengerLevelValue;
        valueTexts[18].text = busShopData.busAchievementItem.gainXMoneyOnXSuitcasePassengerValue + "/" + busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedGainXMoneyOnXSuitcasePassengerLevel].gainXMoneyOnXSuitcasePassengerLevelValue;
        levelExplanationsTexts[18].text = busShopData.busAchievementItem.unlockedGainXMoneyOnXSuitcasePassengerLevel + "/" + (busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1);
        rewardTexts[18].text = "" + busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedGainXMoneyOnXSuitcasePassengerLevel].gainXMoneyOnXSuitcasePassengerReward;
        achievementRewardButtons[18].onClick.AddListener (() => GainXMoneyOnXSuitcasePassengerAchievement ());

        sliderImages[19].fillAmount = (float) busShopData.busAchievementItem.xRoleUpgradeValue / (float) busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedXRoleUpgradeLevel].xRoleUpgradeLevelValue;
        valueTexts[19].text = busShopData.busAchievementItem.xRoleUpgradeValue + "/" + busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedXRoleUpgradeLevel].xRoleUpgradeLevelValue;
        levelExplanationsTexts[19].text = busShopData.busAchievementItem.unlockedXRoleUpgradeLevel + "/" + (busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1);
        rewardTexts[19].text = "" + busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedXRoleUpgradeLevel].xRoleUpgradeReward;
        achievementRewardButtons[19].onClick.AddListener (() => XRoleUpgradeAchievement ());

        //Garbage Achievements
        sliderImages[20].fillAmount = (float) garbageShopData.garbageAchievementItem.xPreciousStuffValue / (float) garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedXPreciousStuffLevel].xPreciousStuffLevelValue;
        valueTexts[20].text = garbageShopData.garbageAchievementItem.xPreciousStuffValue + "/" + garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedXPreciousStuffLevel].xPreciousStuffLevelValue;
        levelExplanationsTexts[20].text = garbageShopData.garbageAchievementItem.unlockedXPreciousStuffLevel + "/" + (garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel.Length - 1);
        rewardTexts[20].text = "" + garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedXPreciousStuffLevel].xPreciousStuffReward;
        achievementRewardButtons[20].onClick.AddListener (() => XPreciousStuffAchievement ());

        sliderImages[21].fillAmount = (float) garbageShopData.garbageAchievementItem.xGainMoneyRecyclingValue / (float) garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedXGainMoneyRecyclingLevel].xGainMoneyRecyclingLevelValue;
        valueTexts[21].text = garbageShopData.garbageAchievementItem.xGainMoneyRecyclingValue + "/" + garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedXGainMoneyRecyclingLevel].xGainMoneyRecyclingLevelValue;
        levelExplanationsTexts[21].text = garbageShopData.garbageAchievementItem.unlockedXGainMoneyRecyclingLevel + "/" + (garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel.Length - 1);
        rewardTexts[21].text = "" + garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedXGainMoneyRecyclingLevel].xGainMoneyRecyclingReward;
        achievementRewardButtons[21].onClick.AddListener (() => XGainMoneyRecyclingAchievement ());

        sliderImages[22].fillAmount = (float) garbageShopData.garbageAchievementItem.xCollectGarbageValue / (float) garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedXCollectGarbageLevel].xCollectGarbageLevelValue;
        valueTexts[22].text = garbageShopData.garbageAchievementItem.xCollectGarbageValue + "/" + garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedXCollectGarbageLevel].xCollectGarbageLevelValue;
        levelExplanationsTexts[22].text = garbageShopData.garbageAchievementItem.unlockedXCollectGarbageLevel + "/" + (garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel.Length - 1);
        rewardTexts[22].text = "" + garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedXCollectGarbageLevel].xCollectGarbageReward;
        achievementRewardButtons[22].onClick.AddListener (() => XCollectGarbageAchievement ());

        sliderImages[23].fillAmount = (float) garbageShopData.garbageAchievementItem.crashXGarbageBoxValue / (float) garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedCrashXGarbageBoxLevel].crashXGarbageBoxLevelValue;
        valueTexts[23].text = garbageShopData.garbageAchievementItem.crashXGarbageBoxValue + "/" + garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedCrashXGarbageBoxLevel].crashXGarbageBoxLevelValue;
        levelExplanationsTexts[23].text = garbageShopData.garbageAchievementItem.unlockedCrashXGarbageBoxLevel + "/" + (garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel.Length - 1);
        rewardTexts[23].text = "" + garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedCrashXGarbageBoxLevel].crashXGarbageBoxReward;
        achievementRewardButtons[23].onClick.AddListener (() => GarbageBoxAchievement ());

        sliderImages[24].fillAmount = (float) garbageShopData.garbageAchievementItem.xTimesEmptyGarbageValue / (float) garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedXTimesEmptyGarbageLevel].xTimesEmptyGarbageLevelValue;
        valueTexts[24].text = garbageShopData.garbageAchievementItem.xTimesEmptyGarbageValue + "/" + garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedXTimesEmptyGarbageLevel].xTimesEmptyGarbageLevelValue;
        levelExplanationsTexts[24].text = garbageShopData.garbageAchievementItem.unlockedXTimesEmptyGarbageLevel + "/" + (garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel.Length - 1);
        rewardTexts[24].text = "" + garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedXTimesEmptyGarbageLevel].xTimesEmptyGarbageReward;
        achievementRewardButtons[24].onClick.AddListener (() => XTimeEmptyGarbageAchievement ());

        sliderImages[25].fillAmount = (float) garbageShopData.garbageAchievementItem.xGainMoneyFromPreciousValue / (float) garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedXGainMoneyFromPreciousLevel].xGainMoneyFromPreciousLevelValue;
        valueTexts[25].text = garbageShopData.garbageAchievementItem.xGainMoneyFromPreciousValue + "/" + garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedXGainMoneyFromPreciousLevel].xGainMoneyFromPreciousLevelValue;
        levelExplanationsTexts[25].text = garbageShopData.garbageAchievementItem.unlockedXGainMoneyFromPreciousLevel + "/" + (garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel.Length - 1);
        rewardTexts[25].text = "" + garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedXGainMoneyFromPreciousLevel].xGainMoneyFromPreciousReward;
        achievementRewardButtons[25].onClick.AddListener (() => GainMoneyFromPreciousAchievement ());

        sliderImages[26].fillAmount = (float) garbageShopData.garbageAchievementItem.doMaxLevelCarUpgradeValue / (float) garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedDoMaxLevelCarUpgradeLevel].doMaxLevelCarUpgradeLevelValue;
        valueTexts[26].text = garbageShopData.garbageAchievementItem.doMaxLevelCarUpgradeValue + "/" + garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedDoMaxLevelCarUpgradeLevel].doMaxLevelCarUpgradeLevelValue;
        levelExplanationsTexts[26].text = garbageShopData.garbageAchievementItem.unlockedDoMaxLevelCarUpgradeLevel + "/" + (garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel.Length - 1);
        rewardTexts[26].text = "" + garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedDoMaxLevelCarUpgradeLevel].doMaxLevelCarUpgradeReward;
        achievementRewardButtons[26].onClick.AddListener (() => DoMaxLevelCarUpgradeAchievement ());

        sliderImages[27].fillAmount = (float) garbageShopData.garbageAchievementItem.oneTimeXGarbageValue / (float) garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedOneTimeXGarbageLevel].oneTimeXGarbageLevelValue;
        valueTexts[27].text = garbageShopData.garbageAchievementItem.oneTimeXGarbageValue + "/" + garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedOneTimeXGarbageLevel].oneTimeXGarbageLevelValue;
        levelExplanationsTexts[27].text = garbageShopData.garbageAchievementItem.unlockedOneTimeXGarbageLevel + "/" + (garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel.Length - 1);
        rewardTexts[27].text = "" + garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedOneTimeXGarbageLevel].oneTimeXGarbageReward;
        achievementRewardButtons[27].onClick.AddListener (() => OneTimeXGarbageAchievement ());

        if (garbageShopData.garbageAchievementItem.mostPreciousValue == false) {
            sliderImages[28].fillAmount = 0.0f / 1.0f;
            levelExplanationsTexts[28].text = 0 + "/" + 1;
            valueTexts[28].text = 0 + "/" + 1;
        } else {
            sliderImages[28].fillAmount = 1.0f / 1.0f;
            levelExplanationsTexts[28].text = 1 + "/" + 1;
            valueTexts[28].text = 1 + "/" + 1;
        }
        rewardTexts[28].text = "" + garbageShopData.garbageAchievementItem.mostPreciousReward;
        achievementRewardButtons[28].onClick.AddListener (() => MostPreciousAchievement ());

        sliderImages[29].fillAmount = (float) garbageShopData.garbageAchievementItem.xTimesSevenHillGarbageEmptyValue / (float) garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedXTimesSevenHillGarbageEmptyLevel].xTimesSevenHillGarbageEmptyLevelValue;
        valueTexts[29].text = garbageShopData.garbageAchievementItem.xTimesSevenHillGarbageEmptyValue + "/" + garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedXTimesSevenHillGarbageEmptyLevel].xTimesSevenHillGarbageEmptyLevelValue;
        levelExplanationsTexts[29].text = garbageShopData.garbageAchievementItem.unlockedXTimesSevenHillGarbageEmptyLevel + "/" + (garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel.Length - 1);
        rewardTexts[29].text = "" + garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedXTimesSevenHillGarbageEmptyLevel].xTimesSevenHillGarbageEmptyReward;
        achievementRewardButtons[29].onClick.AddListener (() => SevenHillGarbageAchievement ());

        //Fire Achievemenets
        sliderImages[30].fillAmount = (float) fireShopData.fireAchievementItem.xFireExtinguishPlaceValue / (float) fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedXFireExtinguishPlaceLevel].xFireExtinguishPlaceLevelValue;
        valueTexts[30].text = fireShopData.fireAchievementItem.xFireExtinguishPlaceValue + "/" + fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedXFireExtinguishPlaceLevel].xFireExtinguishPlaceLevelValue;
        levelExplanationsTexts[30].text = fireShopData.fireAchievementItem.unlockedXFireExtinguishPlaceLevel + "/" + (fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel.Length - 1);
        rewardTexts[30].text = "" + fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedXFireExtinguishPlaceLevel].xFireExtinguishPlaceReward;
        achievementRewardButtons[30].onClick.AddListener (() => FireExtinguishAchievement ());

        sliderImages[31].fillAmount = (float) fireShopData.fireAchievementItem.xSaveLifeFromFireValue / (float) fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedXSaveLifeFromFireLevel].xSaveLifeFromFireLevelValue;
        valueTexts[31].text = fireShopData.fireAchievementItem.xSaveLifeFromFireValue + "/" + fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedXSaveLifeFromFireLevel].xSaveLifeFromFireLevelValue;
        levelExplanationsTexts[31].text = fireShopData.fireAchievementItem.unlockedXSaveLifeFromFireLevel + "/" + (fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel.Length - 1);
        rewardTexts[31].text = "" + fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedXSaveLifeFromFireLevel].xSaveLifeFromFireLevelReward;
        achievementRewardButtons[31].onClick.AddListener (() => SaveLifeFromFireAchievement ());

        sliderImages[32].fillAmount = (float) fireShopData.fireAchievementItem.xSaveCatValue / (float) fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedXSaveCatLevel].xSaveCatLevelValue;
        valueTexts[32].text = fireShopData.fireAchievementItem.xSaveCatValue + "/" + fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedXSaveCatLevel].xSaveCatLevelValue;
        levelExplanationsTexts[32].text = fireShopData.fireAchievementItem.unlockedXSaveCatLevel + "/" + (fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel.Length - 1);
        rewardTexts[32].text = "" + fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedXSaveCatLevel].xSaveCatLevelReward;
        achievementRewardButtons[32].onClick.AddListener (() => SaveCatAchievement ());

        sliderImages[33].fillAmount = (float) fireShopData.fireAchievementItem.xSpentWaterValue / (float) fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedXSpentWaterLevel].xSpentWaterLevelValue;
        valueTexts[33].text = fireShopData.fireAchievementItem.xSpentWaterValue + "/" + fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedXSpentWaterLevel].xSpentWaterLevelValue;
        levelExplanationsTexts[33].text = fireShopData.fireAchievementItem.unlockedXSpentWaterLevel + "/" + (fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel.Length - 1);
        rewardTexts[33].text = "" + fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedXSpentWaterLevel].xSpentWaterLevelReward;
        achievementRewardButtons[33].onClick.AddListener (() => SpentWaterAchievement ());

        sliderImages[34].fillAmount = (float) fireShopData.fireAchievementItem.xCrashWaterHydrantValue / (float) fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedXCrashWaterHydrantLevel].xCrashWaterHydrantLevelValue;
        valueTexts[34].text = fireShopData.fireAchievementItem.xCrashWaterHydrantValue + "/" + fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedXCrashWaterHydrantLevel].xCrashWaterHydrantLevelValue;
        levelExplanationsTexts[34].text = fireShopData.fireAchievementItem.unlockedXCrashWaterHydrantLevel + "/" + (fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel.Length - 1);
        rewardTexts[34].text = "" + fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedXCrashWaterHydrantLevel].xCrashWaterHydrantLevelReward;
        achievementRewardButtons[34].onClick.AddListener (() => CrashWaterHydrantAchievement ());

        sliderImages[35].fillAmount = (float) fireShopData.fireAchievementItem.xFillWaterTankValue / (float) fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedXFillWaterTankLevel].xFillWaterTankLevelValue;
        valueTexts[35].text = fireShopData.fireAchievementItem.xFillWaterTankValue + "/" + fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedXFillWaterTankLevel].xFillWaterTankLevelValue;
        levelExplanationsTexts[35].text = fireShopData.fireAchievementItem.unlockedXFillWaterTankLevel + "/" + (fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel.Length - 1);
        rewardTexts[35].text = "" + fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedXFillWaterTankLevel].xFillWaterTankLevelReward;
        achievementRewardButtons[35].onClick.AddListener (() => FillWaterTankAchievement ());

        sliderImages[36].fillAmount = (float) fireShopData.fireAchievementItem.gainXMoneyFromSaveLifeValue / (float) fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedGainXMoneyFromSaveLifeLevel].gainXMoneyFromSaveLifeLevelValue;
        valueTexts[36].text = fireShopData.fireAchievementItem.gainXMoneyFromSaveLifeValue + "/" + fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedGainXMoneyFromSaveLifeLevel].gainXMoneyFromSaveLifeLevelValue;
        levelExplanationsTexts[36].text = fireShopData.fireAchievementItem.unlockedGainXMoneyFromSaveLifeLevel + "/" + (fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel.Length - 1);
        rewardTexts[36].text = "" + fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedGainXMoneyFromSaveLifeLevel].gainXMoneyFromSaveLifeReward;
        achievementRewardButtons[36].onClick.AddListener (() => GainMoneyFromSaveLifeAchievement ());

        sliderImages[37].fillAmount = (float) fireShopData.fireAchievementItem.extinguishFireBeforeDeadlineValue / (float) fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedExtinguishFireBeforeDeadlineLevel].extinguishFireBeforeDeadlinLevelValue;
        valueTexts[37].text = fireShopData.fireAchievementItem.extinguishFireBeforeDeadlineValue + "/" + fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedExtinguishFireBeforeDeadlineLevel].extinguishFireBeforeDeadlinLevelValue;
        levelExplanationsTexts[37].text = fireShopData.fireAchievementItem.unlockedExtinguishFireBeforeDeadlineLevel + "/" + (fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel.Length - 1);
        rewardTexts[37].text = "" + fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedExtinguishFireBeforeDeadlineLevel].extinguishFireBeforeDeadlinReward;
        achievementRewardButtons[37].onClick.AddListener (() => ExtinguishFireBeforeDeadlineAchievement ());

        if (fireShopData.fireAchievementItem.findTheColdFireValue == false) {
            sliderImages[38].fillAmount = 0.0f / 1.0f;
            levelExplanationsTexts[38].text = 0 + "/" + 1;
            valueTexts[38].text = 0 + "/" + 1;
        } else {
            sliderImages[38].fillAmount = 1.0f / 1.0f;
            levelExplanationsTexts[38].text = 1 + "/" + 1;
            valueTexts[38].text = 1 + "/" + 1;
        }
        rewardTexts[38].text = "" + fireShopData.fireAchievementItem.findTheColdFireReward;
        achievementRewardButtons[38].onClick.AddListener (() => FindColdFireAchievement ());

        sliderImages[39].fillAmount = (float) fireShopData.fireAchievementItem.upgradeMaxRoleValue / (float) fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedUpgradeMaxRoleLevel].upgradeMaxRoleLevelValue;
        valueTexts[39].text = fireShopData.fireAchievementItem.upgradeMaxRoleValue + "/" + fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedUpgradeMaxRoleLevel].upgradeMaxRoleLevelValue;
        levelExplanationsTexts[39].text = fireShopData.fireAchievementItem.unlockedUpgradeMaxRoleLevel + "/" + (fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel.Length - 1);
        rewardTexts[39].text = "" + fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedUpgradeMaxRoleLevel].upgradeMaxRoleReward;
        achievementRewardButtons[39].onClick.AddListener (() => UpgradeMaxRoleAchievement ());

        //Police Achievements
        sliderImages[40].fillAmount = (float) policeShopData.policeAchievementItem.xCatchThiefCrimeSceneValue / (float) policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedXCatchThiefCrimeSceneLevel].xCatchThiefCrimeSceneLevelValue;
        valueTexts[40].text = policeShopData.policeAchievementItem.xCatchThiefCrimeSceneValue + "/" + policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedXCatchThiefCrimeSceneLevel].xCatchThiefCrimeSceneLevelValue;
        levelExplanationsTexts[40].text = policeShopData.policeAchievementItem.unlockedXCatchThiefCrimeSceneLevel + "/" + (policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel.Length - 1);
        rewardTexts[40].text = "" + policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedXCatchThiefCrimeSceneLevel].xCatchThiefCrimeSceneReward;
        achievementRewardButtons[40].onClick.AddListener (() => CatchThiefCrimeSceneAchievement ());

        sliderImages[41].fillAmount = (float) policeShopData.policeAchievementItem.gainXBonusMoneyFromBlockingRobberyValue / (float) policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedGainXBonusMoneyFromBlockingRobberyLevel].gainXBonusMoneyFromBlockingRobberyLevelValue;
        valueTexts[41].text = policeShopData.policeAchievementItem.gainXBonusMoneyFromBlockingRobberyValue + "/" + policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedGainXBonusMoneyFromBlockingRobberyLevel].gainXBonusMoneyFromBlockingRobberyLevelValue;
        levelExplanationsTexts[41].text = policeShopData.policeAchievementItem.unlockedGainXBonusMoneyFromBlockingRobberyLevel + "/" + (policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel.Length - 1);
        rewardTexts[41].text = "" + policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedGainXBonusMoneyFromBlockingRobberyLevel].gainXBonusMoneyFromBlockingRobberyReward;
        achievementRewardButtons[41].onClick.AddListener (() => GainBonusMoneyFromBlockingRobberyAchievement ());

        sliderImages[42].fillAmount = (float) policeShopData.policeAchievementItem.xCatchThiefValue / (float) policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedXCatchThiefLevel].xCatchThiefLevelValue;
        valueTexts[42].text = policeShopData.policeAchievementItem.xCatchThiefValue + "/" + policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedXCatchThiefLevel].xCatchThiefLevelValue;
        levelExplanationsTexts[42].text = policeShopData.policeAchievementItem.unlockedXCatchThiefLevel + "/" + (policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel.Length - 1);
        rewardTexts[42].text = "" + policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedXCatchThiefLevel].xCatchThiefReward;
        achievementRewardButtons[42].onClick.AddListener (() => CatchThiefAchievement ());

        sliderImages[43].fillAmount = (float) policeShopData.policeAchievementItem.xCatchWithScoutValue / (float) policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedXCatchWithScoutLevel].xCatchWithScoutLevelValue;
        valueTexts[43].text = policeShopData.policeAchievementItem.xCatchWithScoutValue + "/" + policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedXCatchWithScoutLevel].xCatchWithScoutLevelValue;
        levelExplanationsTexts[43].text = policeShopData.policeAchievementItem.unlockedXCatchWithScoutLevel + "/" + (policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel.Length - 1);
        rewardTexts[43].text = "" + policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedXCatchWithScoutLevel].xCatchWithScoutReward;
        achievementRewardButtons[43].onClick.AddListener (() => CatchThiefWitchScoutAchievement ());

        sliderImages[44].fillAmount = (float) policeShopData.policeAchievementItem.doMaxLevelSlowThiefValue / (float) policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedDoMaxLevelSlowThiefLevel].doMaxLevelSlowThiefLevelValue;
        valueTexts[44].text = policeShopData.policeAchievementItem.doMaxLevelSlowThiefValue + "/" + policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedDoMaxLevelSlowThiefLevel].doMaxLevelSlowThiefLevelValue;
        levelExplanationsTexts[44].text = policeShopData.policeAchievementItem.unlockedDoMaxLevelSlowThiefLevel + "/" + (policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel.Length - 1);
        rewardTexts[44].text = "" + policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedDoMaxLevelSlowThiefLevel].doMaxLevelSlowThiefReward;
        achievementRewardButtons[44].onClick.AddListener (() => DoMaxLevelThiefSpeedDecreaseAchievement ());

        sliderImages[45].fillAmount = (float) policeShopData.policeAchievementItem.xCrashThiefValue / (float) policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedXCrashThiefLevel].xCrashThiefLevelValue;
        valueTexts[45].text = policeShopData.policeAchievementItem.xCrashThiefValue + "/" + policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedXCrashThiefLevel].xCrashThiefLevelValue;
        levelExplanationsTexts[45].text = policeShopData.policeAchievementItem.unlockedXCrashThiefLevel + "/" + (policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel.Length - 1);
        rewardTexts[45].text = "" + policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedXCrashThiefLevel].xCrashThiefReward;
        achievementRewardButtons[45].onClick.AddListener (() => CrashThiefAchievement ());

        sliderImages[46].fillAmount = (float) policeShopData.policeAchievementItem.useXVanguardValue / (float) policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedUseXVanguardLevel].useXVanguardLevelValue;
        valueTexts[46].text = policeShopData.policeAchievementItem.useXVanguardValue + "/" + policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedUseXVanguardLevel].useXVanguardLevelValue;
        levelExplanationsTexts[46].text = policeShopData.policeAchievementItem.unlockedUseXVanguardLevel + "/" + (policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel.Length - 1);
        rewardTexts[46].text = "" + policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedUseXVanguardLevel].useXVanguardReward;
        achievementRewardButtons[46].onClick.AddListener (() => UseXVanguardAchievement ());

        sliderImages[47].fillAmount = (float) policeShopData.policeAchievementItem.noAccidentPoliceValue / (float) policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedNoAccidentPoliceLevel].noAccidentPoliceLevelValue;
        valueTexts[47].text = policeShopData.policeAchievementItem.noAccidentPoliceValue + "/" + policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedNoAccidentPoliceLevel].noAccidentPoliceLevelValue;
        levelExplanationsTexts[47].text = policeShopData.policeAchievementItem.unlockedNoAccidentPoliceLevel + "/" + (policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel.Length - 1);
        rewardTexts[47].text = "" + policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedNoAccidentPoliceLevel].noAccidentPoliceReward;
        achievementRewardButtons[47].onClick.AddListener (() => NoAccidentPoliceAchievement ());

        if (policeShopData.policeAchievementItem.findRobberyMoneyValue == false) {
            sliderImages[48].fillAmount = 0.0f / 1.0f;
            levelExplanationsTexts[48].text = 0 + "/" + 1;
            valueTexts[48].text = 0 + "/" + 1;
        } else {
            sliderImages[48].fillAmount = 1.0f / 1.0f;
            levelExplanationsTexts[48].text = 1 + "/" + 1;
            valueTexts[48].text = 1 + "/" + 1;
        }
        rewardTexts[48].text = "" + policeShopData.policeAchievementItem.findRobberyMoneyReward;
        achievementRewardButtons[48].onClick.AddListener (() => FindRobberyMoneyAchievement ());

        sliderImages[49].fillAmount = (float) policeShopData.policeAchievementItem.solveAllCaseInSevenhillValue / (float) policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedSolveAllCaseInSevenhillLevel].solveAllCaseInSevenhillLevelValue;
        valueTexts[49].text = policeShopData.policeAchievementItem.solveAllCaseInSevenhillValue + "/" + policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedSolveAllCaseInSevenhillLevel].solveAllCaseInSevenhillLevelValue;
        levelExplanationsTexts[49].text = policeShopData.policeAchievementItem.unlockedSolveAllCaseInSevenhillLevel + "/" + (policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel.Length - 1);
        rewardTexts[49].text = "" + policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedSolveAllCaseInSevenhillLevel].solveAllCaseInSevenhillReward;
        achievementRewardButtons[49].onClick.AddListener (() => SolveAllCaseSevenhillAchievement ());

        //Taxi Achievements
        sliderImages[50].fillAmount = (float) taxiShopData.taxiAchievementItem.crashSignBoardValue / (float) taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedCrashSignBoardLevel].crashSignBoardBoxLevelValue;
        valueTexts[50].text = taxiShopData.taxiAchievementItem.crashSignBoardValue + "/" + taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedCrashSignBoardLevel].crashSignBoardBoxLevelValue;
        levelExplanationsTexts[50].text = taxiShopData.taxiAchievementItem.unlockedCrashSignBoardLevel + "/" + (taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel.Length - 1);
        rewardTexts[50].text = "" + taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedCrashSignBoardLevel].crashSignBoardBoxReward;
        achievementRewardButtons[50].onClick.AddListener (() => CrashSignBoardAchievement ());

        sliderImages[51].fillAmount = (float) taxiShopData.taxiAchievementItem.firstCityTravelValue / (float) taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedFirstCityTravelLevel].firstCityTravelLevelValue;
        valueTexts[51].text = taxiShopData.taxiAchievementItem.firstCityTravelValue + "/" + taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedFirstCityTravelLevel].firstCityTravelLevelValue;
        levelExplanationsTexts[51].text = taxiShopData.taxiAchievementItem.unlockedFirstCityTravelLevel + "/" + (taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel.Length - 1);
        rewardTexts[51].text = "" + taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedFirstCityTravelLevel].firstCityTravelReward;
        achievementRewardButtons[51].onClick.AddListener (() => FirstCityTravelAchievement ());

        sliderImages[52].fillAmount = (float) taxiShopData.taxiAchievementItem.hundredPercentSatisfactionValue / (float) taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedHundredPercentSatisfactionLevel].hundredPercentSatisfactionLevelValue;
        valueTexts[52].text = taxiShopData.taxiAchievementItem.hundredPercentSatisfactionValue + "/" + taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedHundredPercentSatisfactionLevel].hundredPercentSatisfactionLevelValue;
        levelExplanationsTexts[52].text = taxiShopData.taxiAchievementItem.unlockedHundredPercentSatisfactionLevel + "/" + (taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel.Length - 1);
        rewardTexts[52].text = "" + taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedHundredPercentSatisfactionLevel].hundredPercentSatisfactionReward;
        achievementRewardButtons[52].onClick.AddListener (() => HundredSatisfactionPercentAchievement ());

        sliderImages[53].fillAmount = (float) taxiShopData.taxiAchievementItem.use4TimesCrazyOneGameValue / (float) taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedUse4TimesCrazyOneGameLevel].use4TimesCrazyOneGameLevelValue;
        valueTexts[53].text = taxiShopData.taxiAchievementItem.use4TimesCrazyOneGameValue + "/" + taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedUse4TimesCrazyOneGameLevel].use4TimesCrazyOneGameLevelValue;
        levelExplanationsTexts[53].text = taxiShopData.taxiAchievementItem.unlockedUse4TimesCrazyOneGameLevel + "/" + (taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel.Length - 1);
        rewardTexts[53].text = "" + taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedUse4TimesCrazyOneGameLevel].use4TimesCrazyOneGameReward;
        achievementRewardButtons[53].onClick.AddListener (() => FourCrazyDriveAchievement ());

        sliderImages[54].fillAmount = (float) taxiShopData.taxiAchievementItem.xNoAccidentValue / (float) taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedXNoAccidentLevel].xNoAccidentLevelValue;
        valueTexts[54].text = taxiShopData.taxiAchievementItem.xNoAccidentValue + "/" + taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedXNoAccidentLevel].xNoAccidentLevelValue;
        levelExplanationsTexts[54].text = taxiShopData.taxiAchievementItem.unlockedXNoAccidentLevel + "/" + (taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel.Length - 1);
        rewardTexts[54].text = "" + taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedXNoAccidentLevel].xNoAccidentReward;
        achievementRewardButtons[54].onClick.AddListener (() => NoAccidentTaxiAchievement ());

        sliderImages[55].fillAmount = (float) taxiShopData.taxiAchievementItem.useXCateringValue / (float) taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedUseXCateringLevel].useXCateringLevelValue;
        valueTexts[55].text = taxiShopData.taxiAchievementItem.useXCateringValue + "/" + taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedUseXCateringLevel].useXCateringLevelValue;
        levelExplanationsTexts[55].text = taxiShopData.taxiAchievementItem.unlockedUseXCateringLevel + "/" + (taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel.Length - 1);
        rewardTexts[55].text = "" + taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedUseXCateringLevel].useXCateringReward;
        achievementRewardButtons[55].onClick.AddListener (() => UseXCateringAchievement ());

        sliderImages[56].fillAmount = (float) taxiShopData.taxiAchievementItem.noGasolineGiveMoneyValue / (float) taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedNoGasolineGiveMoneyLevel].noGasolineGiveMoneyLevelValue;
        valueTexts[56].text = taxiShopData.taxiAchievementItem.noGasolineGiveMoneyValue + "/" + taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedNoGasolineGiveMoneyLevel].noGasolineGiveMoneyLevelValue;
        levelExplanationsTexts[56].text = taxiShopData.taxiAchievementItem.unlockedNoGasolineGiveMoneyLevel + "/" + (taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel.Length - 1);
        rewardTexts[56].text = "" + taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedNoGasolineGiveMoneyLevel].noGasolineGiveMoneyReward;
        achievementRewardButtons[56].onClick.AddListener (() => NoGasolineGiveMoneyAchievement ());

        sliderImages[57].fillAmount = (float) taxiShopData.taxiAchievementItem.gainXMoneyFromSatisfactionValue / (float) taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedGainXMoneyFromSatisfactionLevel].gainXMoneyFromSatisfactionLevelValue;
        valueTexts[57].text = taxiShopData.taxiAchievementItem.gainXMoneyFromSatisfactionValue + "/" + taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedGainXMoneyFromSatisfactionLevel].gainXMoneyFromSatisfactionLevelValue;
        levelExplanationsTexts[57].text = taxiShopData.taxiAchievementItem.unlockedGainXMoneyFromSatisfactionLevel + "/" + (taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel.Length - 1);
        rewardTexts[57].text = "" + taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedGainXMoneyFromSatisfactionLevel].gainXMoneyFromSatisfactionReward;
        achievementRewardButtons[57].onClick.AddListener (() => GainMoneySatisfactionAchievement ());

        if (taxiShopData.taxiAchievementItem.findTheFamousCustomerValue == false) {
            sliderImages[58].fillAmount = 0.0f / 1.0f;
            levelExplanationsTexts[58].text = 0 + "/" + 1;
            valueTexts[58].text = 0 + "/" + 1;
        } else {
            sliderImages[58].fillAmount = 1.0f / 1.0f;
            levelExplanationsTexts[58].text = 1 + "/" + 1;
            valueTexts[58].text = 1 + "/" + 1;
        }
        rewardTexts[58].text = "" + taxiShopData.taxiAchievementItem.findTheFamousCustomerReward;
        achievementRewardButtons[58].onClick.AddListener (() => FindTheFamousCustomerAchievement ());

        sliderImages[59].fillAmount = (float) taxiShopData.taxiAchievementItem.gasolineFullingValue / (float) taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedGasolineFullingLevel].gasolineFullingLevelValue;
        valueTexts[59].text = taxiShopData.taxiAchievementItem.gasolineFullingValue + "/" + taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedGasolineFullingLevel].gasolineFullingLevelValue;
        levelExplanationsTexts[59].text = taxiShopData.taxiAchievementItem.unlockedGasolineFullingLevel + "/" + (taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel.Length - 1);
        rewardTexts[59].text = "" + taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedGasolineFullingLevel].gasolineFullingReward;
        achievementRewardButtons[59].onClick.AddListener (() => GasolineFullingAchievement ());

        //Ambulance Achievements
        sliderImages[60].fillAmount = (float) ambulanceShopData.ambulanceAchievementItem.xGainMoneyFromExtraPatientValue / (float) ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXGainMoneyFromExtraPatientLevel].xGainMoneyFromExtraPatientLevelValue;
        valueTexts[60].text = ambulanceShopData.ambulanceAchievementItem.xGainMoneyFromExtraPatientValue + "/" + ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXGainMoneyFromExtraPatientLevel].xGainMoneyFromExtraPatientLevelValue;
        levelExplanationsTexts[60].text = ambulanceShopData.ambulanceAchievementItem.unlockedXGainMoneyFromExtraPatientLevel + "/" + (ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel.Length - 1);
        rewardTexts[60].text = "" + ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXGainMoneyFromExtraPatientLevel].xGainMoneyFromExtraPatientReward;
        achievementRewardButtons[60].onClick.AddListener (() => GainMoneyFromExtraPatientAchievement ());

        sliderImages[61].fillAmount = (float) ambulanceShopData.ambulanceAchievementItem.xMultiplePatientWithoutDyingValue / (float) ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXMultiplePatientWithoutDyingLevel].xMultiplePatientWithoutDyingLevelValue;
        valueTexts[61].text = ambulanceShopData.ambulanceAchievementItem.xMultiplePatientWithoutDyingValue + "/" + ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXMultiplePatientWithoutDyingLevel].xMultiplePatientWithoutDyingLevelValue;
        levelExplanationsTexts[61].text = ambulanceShopData.ambulanceAchievementItem.unlockedXMultiplePatientWithoutDyingLevel + "/" + (ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel.Length - 1);
        rewardTexts[61].text = "" + ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXMultiplePatientWithoutDyingLevel].xMultiplePatientWithoutDyingReward;
        achievementRewardButtons[61].onClick.AddListener (() => MultiplePatientWithoutDyingAchievement ());

        sliderImages[62].fillAmount = (float) ambulanceShopData.ambulanceAchievementItem.xUseElectroShockValue / (float) ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXUseElectroShockLevel].xUseElectroShockLevelValue;
        valueTexts[62].text = ambulanceShopData.ambulanceAchievementItem.xUseElectroShockValue + "/" + ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXUseElectroShockLevel].xUseElectroShockLevelValue;
        levelExplanationsTexts[62].text = ambulanceShopData.ambulanceAchievementItem.unlockedXUseElectroShockLevel + "/" + (ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel.Length - 1);
        rewardTexts[62].text = "" + ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXUseElectroShockLevel].xUseElectroShockReward;
        achievementRewardButtons[62].onClick.AddListener (() => UseElectroShockAchievement ());

        sliderImages[63].fillAmount = (float) ambulanceShopData.ambulanceAchievementItem.xPatientWithoutDyingValue / (float) ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXPatientWithoutDyingLevel].xPatientWithoutDyingLevelValue;
        valueTexts[63].text = ambulanceShopData.ambulanceAchievementItem.xPatientWithoutDyingValue + "/" + ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXPatientWithoutDyingLevel].xPatientWithoutDyingLevelValue;
        levelExplanationsTexts[63].text = ambulanceShopData.ambulanceAchievementItem.unlockedXPatientWithoutDyingLevel + "/" + (ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel.Length - 1);
        rewardTexts[63].text = "" + ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXPatientWithoutDyingLevel].xPatientWithoutDyingReward;
        achievementRewardButtons[63].onClick.AddListener (() => PatientWithoutDyingAchievement ());

        sliderImages[64].fillAmount = (float) ambulanceShopData.ambulanceAchievementItem.xUseAdrenalinValue / (float) ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXUseAdrenalinLevel].xUseAdrenalinLevelValue;
        valueTexts[64].text = ambulanceShopData.ambulanceAchievementItem.xUseAdrenalinValue + "/" + ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXUseAdrenalinLevel].xUseAdrenalinLevelValue;
        levelExplanationsTexts[64].text = ambulanceShopData.ambulanceAchievementItem.unlockedXUseAdrenalinLevel + "/" + (ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel.Length - 1);
        rewardTexts[64].text = "" + ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXUseAdrenalinLevel].xUseAdrenalinReward;
        achievementRewardButtons[64].onClick.AddListener (() => UseAdrenalinAchievement ());

        sliderImages[65].fillAmount = (float) ambulanceShopData.ambulanceAchievementItem.xPatientWithoutDyingNoAccidentValue / (float) ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXPatientWithoutDyingNoAccidentLevel].xPatientWithoutDyingNoAccidentLevelValue;
        valueTexts[65].text = ambulanceShopData.ambulanceAchievementItem.xPatientWithoutDyingNoAccidentValue + "/" + ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXPatientWithoutDyingNoAccidentLevel].xPatientWithoutDyingNoAccidentLevelValue;
        levelExplanationsTexts[65].text = ambulanceShopData.ambulanceAchievementItem.unlockedXPatientWithoutDyingNoAccidentLevel + "/" + (ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel.Length - 1);
        rewardTexts[65].text = "" + ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXPatientWithoutDyingNoAccidentLevel].xPatientWithoutDyingNoAccidentReward;
        achievementRewardButtons[65].onClick.AddListener (() => NoAccidentNoDyingAchievement ());

        sliderImages[66].fillAmount = (float) ambulanceShopData.ambulanceAchievementItem.xPatientFullLifeValue / (float) ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXPatientFullLifeLevel].xPatientFullLifeLevelValue;
        valueTexts[66].text = ambulanceShopData.ambulanceAchievementItem.xPatientFullLifeValue + "/" + ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXPatientFullLifeLevel].xPatientFullLifeLevelValue;
        levelExplanationsTexts[66].text = ambulanceShopData.ambulanceAchievementItem.unlockedXPatientFullLifeLevel + "/" + (ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel.Length - 1);
        rewardTexts[66].text = "" + ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXPatientFullLifeLevel].xPatientFullAdrenalinReward;
        achievementRewardButtons[66].onClick.AddListener (() => PatientFullLifeAchievement ());

        sliderImages[67].fillAmount = (float) ambulanceShopData.ambulanceAchievementItem.xPatientFullAdrenalinValue / (float) ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXUseAdrenalinLevel].xPatientFullAdrenalinLevelValue;
        valueTexts[67].text = ambulanceShopData.ambulanceAchievementItem.xPatientFullAdrenalinValue + "/" + ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXUseAdrenalinLevel].xPatientFullAdrenalinLevelValue;
        levelExplanationsTexts[67].text = ambulanceShopData.ambulanceAchievementItem.unlockedXUseAdrenalinLevel + "/" + (ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel.Length - 1);
        rewardTexts[67].text = "" + ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXUseAdrenalinLevel].xUseAdrenalinReward;
        achievementRewardButtons[67].onClick.AddListener (() => PetientFullAdrenalinAchievement ());

        sliderImages[68].fillAmount = (float) ambulanceShopData.ambulanceAchievementItem.xUseHealValue / (float) ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXUseHealLevel].xUseHealLevelValue;
        valueTexts[68].text = ambulanceShopData.ambulanceAchievementItem.xUseHealValue + "/" + ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXUseHealLevel].xUseHealLevelValue;
        levelExplanationsTexts[68].text = ambulanceShopData.ambulanceAchievementItem.unlockedXUseHealLevel + "/" + (ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel.Length - 1);
        rewardTexts[68].text = "" + ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXUseHealLevel].xUseHealReward;
        achievementRewardButtons[68].onClick.AddListener (() => UseHealAchievement ());

        if (ambulanceShopData.ambulanceAchievementItem.findTheImmortalPatientValue == false) {
            sliderImages[69].fillAmount = 0.0f / 1.0f;
            levelExplanationsTexts[69].text = 0 + "/" + 1;
            valueTexts[69].text = 0 + "/" + 1;
        } else {
            sliderImages[69].fillAmount = 1.0f / 1.0f;
            levelExplanationsTexts[69].text = 1 + "/" + 1;
            valueTexts[69].text = 1 + "/" + 1;
        }
        rewardTexts[69].text = "" + ambulanceShopData.ambulanceAchievementItem.findTheImmortalPatientReward;
        achievementRewardButtons[69].onClick.AddListener (() => FindImmortalAchievement ());
    }

    //General Achievements Function
    private void CompleteXMissionAchievement () {
        if (busShopData.generalAchievementItem.unlockedCompleteXMissionLevel < busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
            if (busShopData.generalAchievementItem.completeXMissionValue >= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXMissionLevel].completeXMissionLevelValue) {
                carShopUi.gameData.totalXp += busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXMissionLevel].completeXMissionReward;
                busShopData.generalAchievementItem.unlockedCompleteXMissionLevel++;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXMissionValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);

                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXMissionValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXMissionLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXMissionValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXMissionLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXMissionLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXMissionLevel].completeXMissionReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (busShopData.generalAchievementItem.unlockedCompleteXMissionLevel >= busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[6].text = "Max";
                }

            }
        }

    }

    private void UpgradeXRoleAchievement () {
        if (busShopData.generalAchievementItem.unlockedUpgradeXRoleLevel < busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
            if (busShopData.generalAchievementItem.upgradeXRoleValue >= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedUpgradeXRoleLevel].upgradeXRoleLevelValue) {
                carShopUi.gameData.totalXp += busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedUpgradeXRoleLevel].upgradeXRoleReward;
                busShopData.generalAchievementItem.unlockedUpgradeXRoleLevel++;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);
                busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);

                sliderImages[1].fillAmount = (float) busShopData.generalAchievementItem.upgradeXRoleValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedUpgradeXRoleLevel].upgradeXRoleLevelValue;
                valueTexts[1].text = busShopData.generalAchievementItem.upgradeXRoleValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedUpgradeXRoleLevel].upgradeXRoleLevelValue;
                levelExplanationsTexts[1].text = busShopData.generalAchievementItem.unlockedUpgradeXRoleLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[1].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedUpgradeXRoleLevel].upgradeXRoleReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (busShopData.generalAchievementItem.unlockedUpgradeXRoleLevel >= busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[1].text = "Max";
                }

            }
        }

    }
    private void GainXMoneyFromMissionAchievement () {
        if (busShopData.generalAchievementItem.unlockedGainMoneyFromMissionLevel < busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
            if (busShopData.generalAchievementItem.gainMoneyFromMissionValue >= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedGainMoneyFromMissionLevel].gainMoneyFromMissionLevelValue) {
                carShopUi.gameData.totalXp += busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedGainMoneyFromMissionLevel].gainMoneyFromMissionReward;
                busShopData.generalAchievementItem.unlockedGainMoneyFromMissionLevel++;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);
                busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);

                sliderImages[2].fillAmount = (float) busShopData.generalAchievementItem.gainMoneyFromMissionValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedGainMoneyFromMissionLevel].gainMoneyFromMissionLevelValue;
                valueTexts[2].text = busShopData.generalAchievementItem.gainMoneyFromMissionValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedGainMoneyFromMissionLevel].gainMoneyFromMissionLevelValue;
                levelExplanationsTexts[2].text = busShopData.generalAchievementItem.unlockedGainMoneyFromMissionLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[2].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedGainMoneyFromMissionLevel].gainMoneyFromMissionReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (busShopData.generalAchievementItem.unlockedGainMoneyFromMissionLevel >= busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[2].text = "Max";
                }
            }
        }

    }
    private void GainXExperienceAchievement () {
        if (busShopData.generalAchievementItem.unlockedGainXExperienceLevel < busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
            if (busShopData.generalAchievementItem.gainXExperienceValue >= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedGainXExperienceLevel].gainXExperienceLevelValue) {
                carShopUi.gameData.totalXp += busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedGainXExperienceLevel].gainXExperienceReward;
                busShopData.generalAchievementItem.unlockedGainXExperienceLevel++;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);
                busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);

                sliderImages[3].fillAmount = (float) busShopData.generalAchievementItem.gainXExperienceValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedGainXExperienceLevel].gainXExperienceLevelValue;
                valueTexts[3].text = busShopData.generalAchievementItem.gainXExperienceValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedGainXExperienceLevel].gainXExperienceLevelValue;
                levelExplanationsTexts[3].text = busShopData.generalAchievementItem.unlockedGainXExperienceLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[3].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedGainXExperienceLevel].gainXExperienceReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (busShopData.generalAchievementItem.unlockedGainXExperienceLevel >= busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[3].text = "Max";
                }
            }
        }

    }
    private void BuyCarAchievement () {
        if (busShopData.generalAchievementItem.unlockedBuyCarLevel < busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
            if (busShopData.generalAchievementItem.buyCarValue >= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedBuyCarLevel].buyCarLevelValue) {
                carShopUi.gameData.totalXp += busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedBuyCarLevel].buyCarReward;
                busShopData.generalAchievementItem.unlockedBuyCarLevel++;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);
                busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);

                sliderImages[4].fillAmount = (float) busShopData.generalAchievementItem.buyCarValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedBuyCarLevel].buyCarLevelValue;
                valueTexts[4].text = busShopData.generalAchievementItem.buyCarValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedBuyCarLevel].buyCarLevelValue;
                levelExplanationsTexts[4].text = busShopData.generalAchievementItem.unlockedBuyCarLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[4].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedBuyCarLevel].buyCarReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (busShopData.generalAchievementItem.unlockedBuyCarLevel >= busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[4].text = "Max";
                }
            }
        }

    }
    private void SpendXMoneyForCarUpgradeAchievement () {
        if (busShopData.generalAchievementItem.unlockedSpendXMoneyForCarUpgradeLevel < busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
            if (busShopData.generalAchievementItem.spendXMoneyForCarUpgradeValue >= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedSpendXMoneyForCarUpgradeLevel].spendXMoneyForCarUpgradeLevelValue) {
                carShopUi.gameData.totalXp += busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedSpendXMoneyForCarUpgradeLevel].spendXMoneyForCarUpgradeReward;
                busShopData.generalAchievementItem.unlockedSpendXMoneyForCarUpgradeLevel++;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);
                busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);

                sliderImages[5].fillAmount = (float) busShopData.generalAchievementItem.spendXMoneyForCarUpgradeValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedSpendXMoneyForCarUpgradeLevel].spendXMoneyForCarUpgradeLevelValue;
                valueTexts[5].text = busShopData.generalAchievementItem.spendXMoneyForCarUpgradeValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedSpendXMoneyForCarUpgradeLevel].spendXMoneyForCarUpgradeLevelValue;
                levelExplanationsTexts[5].text = busShopData.generalAchievementItem.unlockedSpendXMoneyForCarUpgradeLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[5].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedSpendXMoneyForCarUpgradeLevel].spendXMoneyForCarUpgradeReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (busShopData.generalAchievementItem.unlockedSpendXMoneyForCarUpgradeLevel >= busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[5].text = "Max";
                }
            }
        }

    }
    private void CompleteXAchievement () {
        if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel < busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
            if (busShopData.generalAchievementItem.completeXAchievementValue >= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue) {
                carShopUi.gameData.totalXp += busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementReward;
                busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel++;

                ReadWriteAllRoles.WriteBusProp (busShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);
                busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);

                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                audioSource.PlayOneShot (collectAchievement);

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel >= busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[6].text = "Max";
                }
            }
        }

    }
    private void XCarUpgradeAchievement () {
        if (busShopData.generalAchievementItem.unlockedXCarUpgradeLevel < busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
            if (busShopData.generalAchievementItem.xCarUpgradeValue >= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedXCarUpgradeLevel].xCarUpgradeLevelValue) {
                carShopUi.gameData.totalXp += busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedXCarUpgradeLevel].xCarUpgradeReward;
                busShopData.generalAchievementItem.unlockedXCarUpgradeLevel++;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);
                busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);

                sliderImages[7].fillAmount = (float) busShopData.generalAchievementItem.xCarUpgradeValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedXCarUpgradeLevel].xCarUpgradeLevelValue;
                valueTexts[7].text = busShopData.generalAchievementItem.xCarUpgradeValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedXCarUpgradeLevel].xCarUpgradeLevelValue;
                levelExplanationsTexts[7].text = busShopData.generalAchievementItem.unlockedXCarUpgradeLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[7].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedXCarUpgradeLevel].xCarUpgradeReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                GPGSManager.IncrementAchievement (GPGSIds.achievement_car_upgrade, 20);

                if (busShopData.generalAchievementItem.unlockedXCarUpgradeLevel >= busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[7].text = "Max";
                }

            }
        }

    }
    private void OpenXTenur () {
        if (busShopData.generalAchievementItem.unlockedOpenXTenurLevel < busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
            if (busShopData.generalAchievementItem.openXTenurValue >= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedOpenXTenurLevel].openXTenurLevelValue) {
                carShopUi.gameData.totalXp += busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedOpenXTenurLevel].openXTenurReward;
                busShopData.generalAchievementItem.unlockedOpenXTenurLevel++;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);
                busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);

                sliderImages[8].fillAmount = (float) busShopData.generalAchievementItem.openXTenurValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedOpenXTenurLevel].openXTenurLevelValue;
                valueTexts[8].text = busShopData.generalAchievementItem.openXTenurValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedOpenXTenurLevel].openXTenurLevelValue;
                levelExplanationsTexts[8].text = busShopData.generalAchievementItem.unlockedOpenXTenurLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[8].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedOpenXTenurLevel].openXTenurReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (busShopData.generalAchievementItem.unlockedOpenXTenurLevel >= busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[8].text = "Max";
                }
            }
        }

    }
    private void UseXSkillAchievement () {
        if (busShopData.generalAchievementItem.unlockedXUseSkillLevel < busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
            if (busShopData.generalAchievementItem.xUseSkillValue >= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedXUseSkillLevel].xUseSkillLevelValue) {
                carShopUi.gameData.totalXp += busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedXUseSkillLevel].xUseSkillReward;
                busShopData.generalAchievementItem.unlockedXUseSkillLevel++;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);
                busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);

                sliderImages[9].fillAmount = (float) busShopData.generalAchievementItem.xUseSkillValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedXUseSkillLevel].xUseSkillLevelValue;
                valueTexts[9].text = busShopData.generalAchievementItem.xUseSkillValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedXUseSkillLevel].xUseSkillLevelValue;
                levelExplanationsTexts[9].text = busShopData.generalAchievementItem.unlockedXUseSkillLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[9].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedXUseSkillLevel].xUseSkillReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (busShopData.generalAchievementItem.unlockedXUseSkillLevel >= (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1)) {
                    valueTexts[9].text = "Max";
                }
            }
        }

    }

    //Bus Achievements Function
    private void GetOnPassengerMoneyAchievement () {
        if (busShopData.busAchievementItem.unlockedGetOnPassengerMoneyLevel < busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1) {
            if (busShopData.busAchievementItem.getOnPassengerMoneyValue >= busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedGetOnPassengerMoneyLevel].getOnPassengerMoneyLevelValue) {
                carShopUi.gameData.totalXp += busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedGetOnPassengerMoneyLevel].getOnPassengerMoneyReward;
                busShopData.busAchievementItem.unlockedGetOnPassengerMoneyLevel++;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);
                busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);

                sliderImages[10].fillAmount = (float) busShopData.busAchievementItem.getOnPassengerMoneyValue / (float) busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedGetOnPassengerMoneyLevel].getOnPassengerMoneyLevelValue;
                valueTexts[10].text = busShopData.busAchievementItem.getOnPassengerMoneyValue + "/" + busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedGetOnPassengerMoneyLevel].getOnPassengerMoneyLevelValue;
                levelExplanationsTexts[10].text = busShopData.busAchievementItem.unlockedGetOnPassengerMoneyLevel + "/" + (busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1);
                rewardTexts[10].text = "" + busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedGetOnPassengerMoneyLevel].getOnPassengerMoneyReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (busShopData.busAchievementItem.unlockedGetOnPassengerMoneyLevel >= (busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1)) {
                    valueTexts[10].text = "Max";
                }
            }
        }

    }
    private void XSuitcasePassengerAchievement () {
        if (busShopData.busAchievementItem.unlockedXSuitcasePassengerLevel < busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1) {
            if (busShopData.busAchievementItem.xSuitcasePassengerValue >= busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedXSuitcasePassengerLevel].xSuitcasePassengerLevelValue) {
                carShopUi.gameData.totalXp += busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedXSuitcasePassengerLevel].xSuitcasePassengerReward;
                busShopData.busAchievementItem.unlockedXSuitcasePassengerLevel++;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);
                busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);

                sliderImages[11].fillAmount = (float) busShopData.busAchievementItem.xSuitcasePassengerValue / (float) busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedXSuitcasePassengerLevel].xSuitcasePassengerLevelValue;
                valueTexts[11].text = busShopData.busAchievementItem.xSuitcasePassengerValue + "/" + busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedXSuitcasePassengerLevel].xSuitcasePassengerLevelValue;
                levelExplanationsTexts[11].text = busShopData.busAchievementItem.unlockedXSuitcasePassengerLevel + "/" + (busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1);
                rewardTexts[11].text = "" + busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedXSuitcasePassengerLevel].xSuitcasePassengerReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (busShopData.busAchievementItem.unlockedXSuitcasePassengerLevel >= (busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1)) {
                    valueTexts[11].text = "Max";
                }
            }
        }

    }
    private void UseXArrangementAchievement () {
        if (busShopData.busAchievementItem.unlockedXArrangementLevel < busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1) {
            if (busShopData.busAchievementItem.xArrangementValue >= busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedGetOnPassengerMoneyLevel].xArrangementLevelValue) {
                carShopUi.gameData.totalXp += busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedGetOnPassengerMoneyLevel].xArrangementReward;
                busShopData.busAchievementItem.unlockedXArrangementLevel++;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);
                busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);

                sliderImages[12].fillAmount = (float) busShopData.busAchievementItem.xArrangementValue / (float) busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedXArrangementLevel].xArrangementLevelValue;
                valueTexts[12].text = busShopData.busAchievementItem.xArrangementValue + "/" + busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedXArrangementLevel].xArrangementLevelValue;
                levelExplanationsTexts[12].text = busShopData.busAchievementItem.unlockedXArrangementLevel + "/" + (busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1);
                rewardTexts[12].text = "" + busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedXArrangementLevel].xArrangementReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (busShopData.busAchievementItem.unlockedXArrangementLevel >= (busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1)) {
                    valueTexts[12].text = "Max";
                }
            }
        }

    }
    private void NoAccidentAchievement () {
        if (busShopData.busAchievementItem.unlockedNoAccidentLevel < busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1) {
            if (busShopData.busAchievementItem.noAccidentValue >= busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedNoAccidentLevel].noAccidentLevelValue) {
                carShopUi.gameData.totalXp += busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedNoAccidentLevel].noAccidentReward;
                busShopData.busAchievementItem.unlockedNoAccidentLevel++;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);
                busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);

                sliderImages[13].fillAmount = (float) busShopData.busAchievementItem.noAccidentValue / (float) busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedNoAccidentLevel].noAccidentLevelValue;
                valueTexts[13].text = busShopData.busAchievementItem.noAccidentValue + "/" + busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedNoAccidentLevel].noAccidentLevelValue;
                levelExplanationsTexts[13].text = busShopData.busAchievementItem.unlockedNoAccidentLevel + "/" + (busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1);
                rewardTexts[13].text = "" + busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedNoAccidentLevel].noAccidentReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (busShopData.busAchievementItem.unlockedNoAccidentLevel >= (busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1)) {
                    valueTexts[13].text = "Max";
                }
            }
        }

    }
    private void XMissionCompleteAtLosBizaAchievement () {
        if (busShopData.busAchievementItem.unlockedXMissionCompleteAtLosBizaLevel < busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1) {
            if (busShopData.busAchievementItem.xMissionCompleteAtLosBizaValue >= busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedXMissionCompleteAtLosBizaLevel].xMissionCompleteAtLosBizaLevelValue) {
                carShopUi.gameData.totalXp += busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedXMissionCompleteAtLosBizaLevel].xMissionCompleteAtLosBizaReward;
                busShopData.busAchievementItem.unlockedXMissionCompleteAtLosBizaLevel++;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);
                busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);

                sliderImages[14].fillAmount = (float) busShopData.busAchievementItem.xMissionCompleteAtLosBizaValue / (float) busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedXMissionCompleteAtLosBizaLevel].xMissionCompleteAtLosBizaLevelValue;
                valueTexts[14].text = busShopData.busAchievementItem.xMissionCompleteAtLosBizaValue + "/" + busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedXMissionCompleteAtLosBizaLevel].xMissionCompleteAtLosBizaLevelValue;
                levelExplanationsTexts[14].text = busShopData.busAchievementItem.unlockedXMissionCompleteAtLosBizaLevel + "/" + (busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1);
                rewardTexts[14].text = "" + busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedXMissionCompleteAtLosBizaLevel].xMissionCompleteAtLosBizaReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (busShopData.busAchievementItem.unlockedXMissionCompleteAtLosBizaLevel >= (busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1)) {
                    valueTexts[14].text = "Max";
                }
            }
        }

    }
    private void XDropPassengerAchievement () {
        if (busShopData.busAchievementItem.unlockedXDropThePassengerLevel < busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1) {
            if (busShopData.busAchievementItem.xDropThePassengerValue >= busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedXDropThePassengerLevel].xDropThePassengerLevelValue) {
                carShopUi.gameData.totalXp += busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedXDropThePassengerLevel].xDropThePassengerReward;
                busShopData.busAchievementItem.unlockedXDropThePassengerLevel++;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);
                busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);

                sliderImages[15].fillAmount = (float) busShopData.busAchievementItem.xDropThePassengerValue / (float) busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedXDropThePassengerLevel].xDropThePassengerLevelValue;
                valueTexts[15].text = busShopData.busAchievementItem.xDropThePassengerValue + "/" + busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedXDropThePassengerLevel].xDropThePassengerLevelValue;
                levelExplanationsTexts[15].text = busShopData.busAchievementItem.unlockedXDropThePassengerLevel + "/" + (busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1);
                rewardTexts[15].text = "" + busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedXDropThePassengerLevel].xDropThePassengerReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (busShopData.busAchievementItem.unlockedXDropThePassengerLevel >= (busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1)) {
                    valueTexts[15].text = "Max";
                }
            }
        }

    }
    private void SecretBusStopAchievement () {
        if (busShopData.busAchievementItem.secretBusReceived == false) {
            if (busShopData.busAchievementItem.secretBusStopValue == true) {

                carShopUi.gameData.totalXp += busShopData.busAchievementItem.secretBusStopReward;
                busShopData.busAchievementItem.secretBusReceived = true;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);
                busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);

                sliderImages[16].fillAmount = 1.0f / 1.0f;
                levelExplanationsTexts[16].text = 1 + "/" + 1;
                valueTexts[16].text = 1 + "/" + 1;

                xpText.text = "" + carShopUi.gameData.totalXp;

                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                GPGSManager.UnlockAchievement (GPGSIds.achievement_secret_bus_stop);

                valueTexts[16].text = "Max";

            }
        }

    }
    private void AllBusRotationAchievement () {
        if (busShopData.busAchievementItem.allRotationReceived == false) {
            if (busShopData.busAchievementItem.allBusRotationValue == true) {
                carShopUi.gameData.totalXp += busShopData.busAchievementItem.allBusRotationReward;
                busShopData.busAchievementItem.allRotationReceived = true;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);
                busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);

                sliderImages[17].fillAmount = 1.0f / 1.0f;
                levelExplanationsTexts[17].text = 1 + "/" + 1;
                valueTexts[17].text = 1 + "/" + 1;

                xpText.text = "" + carShopUi.gameData.totalXp;

                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                valueTexts[17].text = "Max";
            }
        }

    }
    private void GainXMoneyOnXSuitcasePassengerAchievement () {
        if (busShopData.busAchievementItem.unlockedGainXMoneyOnXSuitcasePassengerLevel < busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1) {
            if (busShopData.busAchievementItem.gainXMoneyOnXSuitcasePassengerValue >= busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedGainXMoneyOnXSuitcasePassengerLevel].gainXMoneyOnXSuitcasePassengerLevelValue) {
                carShopUi.gameData.totalXp += busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedGainXMoneyOnXSuitcasePassengerLevel].gainXMoneyOnXSuitcasePassengerReward;
                busShopData.busAchievementItem.unlockedGainXMoneyOnXSuitcasePassengerLevel++;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);
                busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);

                sliderImages[18].fillAmount = (float) busShopData.busAchievementItem.gainXMoneyOnXSuitcasePassengerValue / (float) busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedGainXMoneyOnXSuitcasePassengerLevel].gainXMoneyOnXSuitcasePassengerLevelValue;
                valueTexts[18].text = busShopData.busAchievementItem.gainXMoneyOnXSuitcasePassengerValue + "/" + busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedGainXMoneyOnXSuitcasePassengerLevel].gainXMoneyOnXSuitcasePassengerLevelValue;
                levelExplanationsTexts[18].text = busShopData.busAchievementItem.unlockedGainXMoneyOnXSuitcasePassengerLevel + "/" + (busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1);
                rewardTexts[18].text = "" + busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedGainXMoneyOnXSuitcasePassengerLevel].gainXMoneyOnXSuitcasePassengerReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (busShopData.busAchievementItem.unlockedGainXMoneyOnXSuitcasePassengerLevel >= (busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1)) {
                    valueTexts[18].text = "Max";
                }
            }
        }

    }
    private void XRoleUpgradeAchievement () {
        if (busShopData.busAchievementItem.unlockedXRoleUpgradeLevel < busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1) {
            if (busShopData.busAchievementItem.xRoleUpgradeValue >= busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedXRoleUpgradeLevel].xRoleUpgradeLevelValue) {
                carShopUi.gameData.totalXp += busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedXRoleUpgradeLevel].xRoleUpgradeReward;
                busShopData.busAchievementItem.unlockedXRoleUpgradeLevel++;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);
                busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);

                sliderImages[19].fillAmount = (float) busShopData.busAchievementItem.xRoleUpgradeValue / (float) busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedXRoleUpgradeLevel].xRoleUpgradeLevelValue;
                valueTexts[19].text = busShopData.busAchievementItem.xRoleUpgradeValue + "/" + busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedXRoleUpgradeLevel].xRoleUpgradeLevelValue;
                levelExplanationsTexts[19].text = busShopData.busAchievementItem.unlockedXRoleUpgradeLevel + "/" + (busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1);
                rewardTexts[19].text = "" + busShopData.busAchievementItem.busAchievementsUpgradeLevel[busShopData.busAchievementItem.unlockedXRoleUpgradeLevel].xRoleUpgradeReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (busShopData.busAchievementItem.unlockedXRoleUpgradeLevel >= (busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1)) {
                    valueTexts[19].text = "Max";
                }
            }
        }

    }

    //Garbage Achievements
    private void XPreciousStuffAchievement () {
        if (garbageShopData.garbageAchievementItem.unlockedXPreciousStuffLevel < garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel.Length - 1) {
            if (garbageShopData.garbageAchievementItem.xPreciousStuffValue >= garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedXPreciousStuffLevel].xPreciousStuffLevelValue) {
                carShopUi.gameData.totalXp += garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedXPreciousStuffLevel].xPreciousStuffReward;
                garbageShopData.garbageAchievementItem.unlockedXPreciousStuffLevel++;

                ReadWriteAllRoles.WriteGarbageProp (garbageShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                garbageShopData = ReadWriteAllRoles.ReadGarbageProp (garbageShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[20].fillAmount = (float) garbageShopData.garbageAchievementItem.xPreciousStuffValue / (float) garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedXPreciousStuffLevel].xPreciousStuffLevelValue;
                valueTexts[20].text = garbageShopData.garbageAchievementItem.xPreciousStuffValue + "/" + garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedXPreciousStuffLevel].xPreciousStuffLevelValue;
                levelExplanationsTexts[20].text = garbageShopData.garbageAchievementItem.unlockedXPreciousStuffLevel + "/" + (garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel.Length - 1);
                rewardTexts[20].text = "" + garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedXPreciousStuffLevel].xPreciousStuffReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (garbageShopData.garbageAchievementItem.unlockedXPreciousStuffLevel >= garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[20].text = "Max";
                }

            }
        }

    }

    private void XGainMoneyRecyclingAchievement () {
        if (garbageShopData.garbageAchievementItem.unlockedXGainMoneyRecyclingLevel < garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel.Length - 1) {
            if (garbageShopData.garbageAchievementItem.xGainMoneyRecyclingValue >= garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedXGainMoneyRecyclingLevel].xGainMoneyRecyclingLevelValue) {
                carShopUi.gameData.totalXp += garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedXGainMoneyRecyclingLevel].xGainMoneyRecyclingReward;
                garbageShopData.garbageAchievementItem.unlockedXGainMoneyRecyclingLevel++;

                ReadWriteAllRoles.WriteGarbageProp (garbageShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                garbageShopData = ReadWriteAllRoles.ReadGarbageProp (garbageShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[21].fillAmount = (float) garbageShopData.garbageAchievementItem.xGainMoneyRecyclingValue / (float) garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedXGainMoneyRecyclingLevel].xGainMoneyRecyclingLevelValue;
                valueTexts[21].text = garbageShopData.garbageAchievementItem.xGainMoneyRecyclingValue + "/" + garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedXGainMoneyRecyclingLevel].xGainMoneyRecyclingLevelValue;
                levelExplanationsTexts[21].text = garbageShopData.garbageAchievementItem.unlockedXGainMoneyRecyclingLevel + "/" + (garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel.Length - 1);
                rewardTexts[21].text = "" + garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedXGainMoneyRecyclingLevel].xGainMoneyRecyclingReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (garbageShopData.garbageAchievementItem.unlockedXGainMoneyRecyclingLevel >= garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[21].text = "Max";
                }

            }
        }

    }

    private void XCollectGarbageAchievement () {
        if (garbageShopData.garbageAchievementItem.unlockedXCollectGarbageLevel < garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel.Length - 1) {
            if (garbageShopData.garbageAchievementItem.xCollectGarbageValue >= garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedXCollectGarbageLevel].xCollectGarbageLevelValue) {
                carShopUi.gameData.totalXp += garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedXCollectGarbageLevel].xCollectGarbageReward;
                garbageShopData.garbageAchievementItem.unlockedXCollectGarbageLevel++;

                ReadWriteAllRoles.WriteGarbageProp (garbageShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                garbageShopData = ReadWriteAllRoles.ReadGarbageProp (garbageShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[22].fillAmount = (float) garbageShopData.garbageAchievementItem.xCollectGarbageValue / (float) garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedXCollectGarbageLevel].xCollectGarbageLevelValue;
                valueTexts[22].text = garbageShopData.garbageAchievementItem.xCollectGarbageValue + "/" + garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedXCollectGarbageLevel].xCollectGarbageLevelValue;
                levelExplanationsTexts[22].text = garbageShopData.garbageAchievementItem.unlockedXCollectGarbageLevel + "/" + (garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel.Length - 1);
                rewardTexts[22].text = "" + garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedXCollectGarbageLevel].xCollectGarbageReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (garbageShopData.garbageAchievementItem.unlockedXCollectGarbageLevel >= garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[22].text = "Max";
                }

            }
        }

    }

    private void GarbageBoxAchievement () {
        if (garbageShopData.garbageAchievementItem.unlockedCrashXGarbageBoxLevel < garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel.Length - 1) {
            if (garbageShopData.garbageAchievementItem.crashXGarbageBoxValue >= garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedCrashXGarbageBoxLevel].crashXGarbageBoxLevelValue) {
                carShopUi.gameData.totalXp += garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedCrashXGarbageBoxLevel].crashXGarbageBoxReward;
                garbageShopData.garbageAchievementItem.unlockedCrashXGarbageBoxLevel++;

                ReadWriteAllRoles.WriteGarbageProp (garbageShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                garbageShopData = ReadWriteAllRoles.ReadGarbageProp (garbageShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[23].fillAmount = (float) garbageShopData.garbageAchievementItem.crashXGarbageBoxValue / (float) garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedCrashXGarbageBoxLevel].crashXGarbageBoxLevelValue;
                valueTexts[23].text = garbageShopData.garbageAchievementItem.crashXGarbageBoxValue + "/" + garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedCrashXGarbageBoxLevel].crashXGarbageBoxLevelValue;
                levelExplanationsTexts[23].text = garbageShopData.garbageAchievementItem.unlockedCrashXGarbageBoxLevel + "/" + (garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel.Length - 1);
                rewardTexts[23].text = "" + garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedCrashXGarbageBoxLevel].crashXGarbageBoxReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (garbageShopData.garbageAchievementItem.unlockedCrashXGarbageBoxLevel >= garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[23].text = "Max";
                }

            }
        }

    }

    private void XTimeEmptyGarbageAchievement () {
        if (garbageShopData.garbageAchievementItem.unlockedXTimesEmptyGarbageLevel < garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel.Length - 1) {
            if (garbageShopData.garbageAchievementItem.xTimesEmptyGarbageValue >= garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedXTimesEmptyGarbageLevel].xTimesEmptyGarbageLevelValue) {
                carShopUi.gameData.totalXp += garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedXTimesEmptyGarbageLevel].xTimesEmptyGarbageReward;
                garbageShopData.garbageAchievementItem.unlockedXTimesEmptyGarbageLevel++;

                ReadWriteAllRoles.WriteGarbageProp (garbageShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                garbageShopData = ReadWriteAllRoles.ReadGarbageProp (garbageShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[24].fillAmount = (float) garbageShopData.garbageAchievementItem.xTimesEmptyGarbageValue / (float) garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedXTimesEmptyGarbageLevel].xTimesEmptyGarbageLevelValue;
                valueTexts[24].text = garbageShopData.garbageAchievementItem.xTimesEmptyGarbageValue + "/" + garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedXTimesEmptyGarbageLevel].xTimesEmptyGarbageLevelValue;
                levelExplanationsTexts[24].text = garbageShopData.garbageAchievementItem.unlockedXTimesEmptyGarbageLevel + "/" + (garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel.Length - 1);
                rewardTexts[24].text = "" + garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedXTimesEmptyGarbageLevel].xTimesEmptyGarbageReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (garbageShopData.garbageAchievementItem.unlockedXTimesEmptyGarbageLevel >= garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[24].text = "Max";
                }

            }
        }

    }

    private void GainMoneyFromPreciousAchievement () {
        if (garbageShopData.garbageAchievementItem.unlockedXGainMoneyFromPreciousLevel < garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel.Length - 1) {
            if (garbageShopData.garbageAchievementItem.xGainMoneyFromPreciousValue >= garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedXGainMoneyFromPreciousLevel].xGainMoneyFromPreciousLevelValue) {
                carShopUi.gameData.totalXp += garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedXGainMoneyFromPreciousLevel].xGainMoneyFromPreciousReward;
                garbageShopData.garbageAchievementItem.unlockedXGainMoneyFromPreciousLevel++;

                ReadWriteAllRoles.WriteGarbageProp (garbageShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                garbageShopData = ReadWriteAllRoles.ReadGarbageProp (garbageShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[25].fillAmount = (float) garbageShopData.garbageAchievementItem.xGainMoneyFromPreciousValue / (float) garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedXGainMoneyFromPreciousLevel].xGainMoneyFromPreciousLevelValue;
                valueTexts[25].text = garbageShopData.garbageAchievementItem.xGainMoneyFromPreciousValue + "/" + garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedXGainMoneyFromPreciousLevel].xGainMoneyFromPreciousLevelValue;
                levelExplanationsTexts[25].text = garbageShopData.garbageAchievementItem.unlockedXGainMoneyFromPreciousLevel + "/" + (garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel.Length - 1);
                rewardTexts[25].text = "" + garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedXGainMoneyFromPreciousLevel].xGainMoneyFromPreciousReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (garbageShopData.garbageAchievementItem.unlockedXGainMoneyFromPreciousLevel >= garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[25].text = "Max";
                }

            }
        }

    }

    private void DoMaxLevelCarUpgradeAchievement () {
        if (garbageShopData.garbageAchievementItem.unlockedDoMaxLevelCarUpgradeLevel < garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel.Length - 1) {
            if (garbageShopData.garbageAchievementItem.doMaxLevelCarUpgradeValue >= garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedDoMaxLevelCarUpgradeLevel].doMaxLevelCarUpgradeLevelValue) {
                carShopUi.gameData.totalXp += garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedDoMaxLevelCarUpgradeLevel].doMaxLevelCarUpgradeReward;
                garbageShopData.garbageAchievementItem.unlockedDoMaxLevelCarUpgradeLevel++;

                ReadWriteAllRoles.WriteGarbageProp (garbageShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                garbageShopData = ReadWriteAllRoles.ReadGarbageProp (garbageShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[26].fillAmount = (float) garbageShopData.garbageAchievementItem.doMaxLevelCarUpgradeValue / (float) garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedDoMaxLevelCarUpgradeLevel].doMaxLevelCarUpgradeLevelValue;
                valueTexts[26].text = garbageShopData.garbageAchievementItem.doMaxLevelCarUpgradeValue + "/" + garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedDoMaxLevelCarUpgradeLevel].doMaxLevelCarUpgradeLevelValue;
                levelExplanationsTexts[26].text = garbageShopData.garbageAchievementItem.unlockedDoMaxLevelCarUpgradeLevel + "/" + (garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel.Length - 1);
                rewardTexts[26].text = "" + garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedDoMaxLevelCarUpgradeLevel].doMaxLevelCarUpgradeReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (garbageShopData.garbageAchievementItem.unlockedDoMaxLevelCarUpgradeLevel >= garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[26].text = "Max";
                }

            }
        }

    }

    private void OneTimeXGarbageAchievement () {
        if (garbageShopData.garbageAchievementItem.unlockedOneTimeXGarbageLevel < garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel.Length - 1) {
            if (garbageShopData.garbageAchievementItem.oneTimeXGarbageValue >= garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedOneTimeXGarbageLevel].oneTimeXGarbageLevelValue) {
                carShopUi.gameData.totalXp += garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedOneTimeXGarbageLevel].oneTimeXGarbageReward;
                garbageShopData.garbageAchievementItem.unlockedOneTimeXGarbageLevel++;

                ReadWriteAllRoles.WriteGarbageProp (garbageShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                garbageShopData = ReadWriteAllRoles.ReadGarbageProp (garbageShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[27].fillAmount = (float) garbageShopData.garbageAchievementItem.oneTimeXGarbageValue / (float) garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedOneTimeXGarbageLevel].oneTimeXGarbageLevelValue;
                valueTexts[27].text = garbageShopData.garbageAchievementItem.oneTimeXGarbageValue + "/" + garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedOneTimeXGarbageLevel].oneTimeXGarbageLevelValue;
                levelExplanationsTexts[27].text = garbageShopData.garbageAchievementItem.unlockedOneTimeXGarbageLevel + "/" + (garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel.Length - 1);
                rewardTexts[27].text = "" + garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedOneTimeXGarbageLevel].oneTimeXGarbageReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (garbageShopData.garbageAchievementItem.unlockedOneTimeXGarbageLevel >= garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[27].text = "Max";
                }

            }
        }

    }

    private void MostPreciousAchievement () {
        if (garbageShopData.garbageAchievementItem.mostPreciousReceived == false) {
            if (garbageShopData.garbageAchievementItem.mostPreciousValue == true) {
                carShopUi.gameData.totalXp += garbageShopData.garbageAchievementItem.mostPreciousReward;
                garbageShopData.garbageAchievementItem.mostPreciousReceived = true;
                garbageShopData.garbageAchievementItem.unlockedXPreciousStuffLevel++;

                ReadWriteAllRoles.WriteGarbageProp (garbageShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                garbageShopData = ReadWriteAllRoles.ReadGarbageProp (garbageShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[28].fillAmount = 1.0f / 1.0f;
                levelExplanationsTexts[28].text = 1 + "/" + 1;
                valueTexts[28].text = 1 + "/" + 1;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                valueTexts[28].text = "Max";

            }
        }

    }

    private void SevenHillGarbageAchievement () {
        if (garbageShopData.garbageAchievementItem.unlockedXTimesSevenHillGarbageEmptyLevel < garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel.Length - 1) {
            if (garbageShopData.garbageAchievementItem.xTimesSevenHillGarbageEmptyValue >= garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedXTimesSevenHillGarbageEmptyLevel].xTimesSevenHillGarbageEmptyLevelValue) {
                carShopUi.gameData.totalXp += garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedXTimesSevenHillGarbageEmptyLevel].xTimesEmptyGarbageReward;
                garbageShopData.garbageAchievementItem.unlockedXTimesSevenHillGarbageEmptyLevel++;

                ReadWriteAllRoles.WriteGarbageProp (garbageShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                garbageShopData = ReadWriteAllRoles.ReadGarbageProp (garbageShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[29].fillAmount = (float) garbageShopData.garbageAchievementItem.xTimesSevenHillGarbageEmptyValue / (float) garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedXTimesSevenHillGarbageEmptyLevel].xTimesSevenHillGarbageEmptyLevelValue;
                valueTexts[29].text = garbageShopData.garbageAchievementItem.xTimesSevenHillGarbageEmptyValue + "/" + garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedXTimesSevenHillGarbageEmptyLevel].xTimesSevenHillGarbageEmptyLevelValue;
                levelExplanationsTexts[29].text = garbageShopData.garbageAchievementItem.unlockedXTimesSevenHillGarbageEmptyLevel + "/" + (garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel.Length - 1);
                rewardTexts[29].text = "" + garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel[garbageShopData.garbageAchievementItem.unlockedXTimesSevenHillGarbageEmptyLevel].xTimesSevenHillGarbageEmptyReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (garbageShopData.garbageAchievementItem.unlockedXTimesSevenHillGarbageEmptyLevel >= garbageShopData.garbageAchievementItem.garbageAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[29].text = "Max";
                }

            }
        }

    }

    //Fire Achievements
    private void FireExtinguishAchievement () {
        if (fireShopData.fireAchievementItem.unlockedXFireExtinguishPlaceLevel < fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel.Length - 1) {
            if (fireShopData.fireAchievementItem.xFireExtinguishPlaceValue >= fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedXFireExtinguishPlaceLevel].xFireExtinguishPlaceLevelValue) {
                carShopUi.gameData.totalXp += fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedXFireExtinguishPlaceLevel].xFireExtinguishPlaceReward;
                fireShopData.fireAchievementItem.unlockedXFireExtinguishPlaceLevel++;

                ReadWriteAllRoles.WriteFireProp (fireShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                fireShopData = ReadWriteAllRoles.ReadFireProp (fireShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[30].fillAmount = (float) fireShopData.fireAchievementItem.xFireExtinguishPlaceValue / (float) fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedXFireExtinguishPlaceLevel].xFireExtinguishPlaceLevelValue;
                valueTexts[30].text = fireShopData.fireAchievementItem.xFireExtinguishPlaceValue + "/" + fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedXFireExtinguishPlaceLevel].xFireExtinguishPlaceLevelValue;
                levelExplanationsTexts[30].text = fireShopData.fireAchievementItem.unlockedXFireExtinguishPlaceLevel + "/" + (fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel.Length - 1);
                rewardTexts[30].text = "" + fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedXFireExtinguishPlaceLevel].xFireExtinguishPlaceReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (fireShopData.fireAchievementItem.unlockedXFireExtinguishPlaceLevel >= fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[30].text = "Max";
                }
            }
        }

    }

    private void SaveLifeFromFireAchievement () {
        if (fireShopData.fireAchievementItem.unlockedXSaveLifeFromFireLevel < fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel.Length - 1) {
            if (fireShopData.fireAchievementItem.xSaveLifeFromFireValue >= fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedXSaveLifeFromFireLevel].xSaveLifeFromFireLevelValue) {
                carShopUi.gameData.totalXp += fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedXSaveLifeFromFireLevel].xSaveLifeFromFireLevelReward;
                fireShopData.fireAchievementItem.unlockedXSaveLifeFromFireLevel++;

                ReadWriteAllRoles.WriteFireProp (fireShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                fireShopData = ReadWriteAllRoles.ReadFireProp (fireShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[31].fillAmount = (float) fireShopData.fireAchievementItem.xSaveLifeFromFireValue / (float) fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedXSaveLifeFromFireLevel].xSaveLifeFromFireLevelValue;
                valueTexts[31].text = fireShopData.fireAchievementItem.xSaveLifeFromFireValue + "/" + fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedXSaveLifeFromFireLevel].xSaveLifeFromFireLevelValue;
                levelExplanationsTexts[31].text = fireShopData.fireAchievementItem.unlockedXSaveLifeFromFireLevel + "/" + (fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel.Length - 1);
                rewardTexts[31].text = "" + fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedXSaveLifeFromFireLevel].xSaveLifeFromFireLevelReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (fireShopData.fireAchievementItem.unlockedXSaveLifeFromFireLevel >= fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[31].text = "Max";
                }

            }
        }

    }

    private void SaveCatAchievement () {
        if (fireShopData.fireAchievementItem.unlockedXSaveCatLevel < fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel.Length - 1) {
            if (fireShopData.fireAchievementItem.xSaveCatValue >= fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedXSaveCatLevel].xSaveCatLevelValue) {
                carShopUi.gameData.totalXp += fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedXSaveCatLevel].xSaveCatLevelReward;
                fireShopData.fireAchievementItem.unlockedXSaveCatLevel++;

                ReadWriteAllRoles.WriteFireProp (fireShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                fireShopData = ReadWriteAllRoles.ReadFireProp (fireShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[32].fillAmount = (float) fireShopData.fireAchievementItem.xSaveCatValue / (float) fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedXSaveCatLevel].xSaveCatLevelValue;
                valueTexts[32].text = fireShopData.fireAchievementItem.xSaveCatValue + "/" + fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedXSaveCatLevel].xSaveCatLevelValue;
                levelExplanationsTexts[32].text = fireShopData.fireAchievementItem.unlockedXSaveCatLevel + "/" + (fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel.Length - 1);
                rewardTexts[32].text = "" + fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedXSaveCatLevel].xSaveCatLevelReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (fireShopData.fireAchievementItem.unlockedXSaveCatLevel >= fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[32].text = "Max";
                }
            }
        }

    }

    private void SpentWaterAchievement () {
        if (fireShopData.fireAchievementItem.unlockedXSpentWaterLevel < fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel.Length - 1) {
            if (fireShopData.fireAchievementItem.xSpentWaterValue >= fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedXSpentWaterLevel].xSpentWaterLevelValue) {
                carShopUi.gameData.totalXp += fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedXSpentWaterLevel].xSpentWaterLevelReward;
                fireShopData.fireAchievementItem.unlockedXSpentWaterLevel++;

                ReadWriteAllRoles.WriteFireProp (fireShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                fireShopData = ReadWriteAllRoles.ReadFireProp (fireShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[33].fillAmount = (float) fireShopData.fireAchievementItem.xSpentWaterValue / (float) fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedXSpentWaterLevel].xSpentWaterLevelValue;
                valueTexts[33].text = fireShopData.fireAchievementItem.xSpentWaterValue + "/" + fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedXSpentWaterLevel].xSpentWaterLevelValue;
                levelExplanationsTexts[33].text = fireShopData.fireAchievementItem.unlockedXSpentWaterLevel + "/" + (fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel.Length - 1);
                rewardTexts[33].text = "" + fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedXSpentWaterLevel].xSpentWaterLevelReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (fireShopData.fireAchievementItem.unlockedXSpentWaterLevel >= fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[33].text = "Max";
                }
            }
        }

    }

    private void CrashWaterHydrantAchievement () {
        if (fireShopData.fireAchievementItem.unlockedXCrashWaterHydrantLevel < fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel.Length - 1) {
            if (fireShopData.fireAchievementItem.xCrashWaterHydrantValue >= fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedXCrashWaterHydrantLevel].xCrashWaterHydrantLevelValue) {
                carShopUi.gameData.totalXp += fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedXCrashWaterHydrantLevel].xCrashWaterHydrantLevelReward;
                fireShopData.fireAchievementItem.unlockedXCrashWaterHydrantLevel++;

                ReadWriteAllRoles.WriteFireProp (fireShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                fireShopData = ReadWriteAllRoles.ReadFireProp (fireShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[34].fillAmount = (float) fireShopData.fireAchievementItem.xCrashWaterHydrantValue / (float) fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedXCrashWaterHydrantLevel].xCrashWaterHydrantLevelValue;
                valueTexts[34].text = fireShopData.fireAchievementItem.xCrashWaterHydrantValue + "/" + fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedXCrashWaterHydrantLevel].xCrashWaterHydrantLevelValue;
                levelExplanationsTexts[34].text = fireShopData.fireAchievementItem.unlockedXCrashWaterHydrantLevel + "/" + (fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel.Length - 1);
                rewardTexts[34].text = "" + fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedXCrashWaterHydrantLevel].xCrashWaterHydrantLevelReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (fireShopData.fireAchievementItem.unlockedXCrashWaterHydrantLevel >= fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[34].text = "Max";
                }
            }
        }

    }

    private void FillWaterTankAchievement () {
        if (fireShopData.fireAchievementItem.unlockedXFillWaterTankLevel < fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel.Length - 1) {
            if (fireShopData.fireAchievementItem.xFillWaterTankValue >= fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedXFillWaterTankLevel].xFillWaterTankLevelValue) {
                carShopUi.gameData.totalXp += fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedXFillWaterTankLevel].xFillWaterTankLevelReward;
                fireShopData.fireAchievementItem.unlockedXFillWaterTankLevel++;

                ReadWriteAllRoles.WriteFireProp (fireShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                fireShopData = ReadWriteAllRoles.ReadFireProp (fireShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[35].fillAmount = (float) fireShopData.fireAchievementItem.xFillWaterTankValue / (float) fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedXFillWaterTankLevel].xFillWaterTankLevelValue;
                valueTexts[35].text = fireShopData.fireAchievementItem.xFillWaterTankValue + "/" + fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedXFillWaterTankLevel].xFillWaterTankLevelValue;
                levelExplanationsTexts[35].text = fireShopData.fireAchievementItem.unlockedXFillWaterTankLevel + "/" + (fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel.Length - 1);
                rewardTexts[35].text = "" + fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedXFillWaterTankLevel].xFillWaterTankLevelReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (fireShopData.fireAchievementItem.unlockedXFillWaterTankLevel >= fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[35].text = "Max";
                }
            }
        }

    }

    private void GainMoneyFromSaveLifeAchievement () {
        if (fireShopData.fireAchievementItem.unlockedGainXMoneyFromSaveLifeLevel < fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel.Length - 1) {
            if (fireShopData.fireAchievementItem.xFireExtinguishPlaceValue >= fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedGainXMoneyFromSaveLifeLevel].gainXMoneyFromSaveLifeLevelValue) {
                carShopUi.gameData.totalXp += fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedGainXMoneyFromSaveLifeLevel].gainXMoneyFromSaveLifeReward;
                fireShopData.fireAchievementItem.unlockedGainXMoneyFromSaveLifeLevel++;

                ReadWriteAllRoles.WriteFireProp (fireShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                fireShopData = ReadWriteAllRoles.ReadFireProp (fireShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[36].fillAmount = (float) fireShopData.fireAchievementItem.gainXMoneyFromSaveLifeValue / (float) fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedGainXMoneyFromSaveLifeLevel].gainXMoneyFromSaveLifeLevelValue;
                valueTexts[36].text = fireShopData.fireAchievementItem.gainXMoneyFromSaveLifeValue + "/" + fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedGainXMoneyFromSaveLifeLevel].gainXMoneyFromSaveLifeLevelValue;
                levelExplanationsTexts[36].text = fireShopData.fireAchievementItem.unlockedGainXMoneyFromSaveLifeLevel + "/" + (fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel.Length - 1);
                rewardTexts[36].text = "" + fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedGainXMoneyFromSaveLifeLevel].gainXMoneyFromSaveLifeReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (fireShopData.fireAchievementItem.unlockedGainXMoneyFromSaveLifeLevel >= fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[36].text = "Max";
                }
            }
        }

    }

    private void ExtinguishFireBeforeDeadlineAchievement () {
        if (fireShopData.fireAchievementItem.unlockedExtinguishFireBeforeDeadlineLevel < fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel.Length - 1) {
            if (fireShopData.fireAchievementItem.extinguishFireBeforeDeadlineValue >= fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedExtinguishFireBeforeDeadlineLevel].extinguishFireBeforeDeadlinLevelValue) {
                carShopUi.gameData.totalXp += fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedExtinguishFireBeforeDeadlineLevel].extinguishFireBeforeDeadlinReward;
                fireShopData.fireAchievementItem.unlockedExtinguishFireBeforeDeadlineLevel++;

                ReadWriteAllRoles.WriteFireProp (fireShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                fireShopData = ReadWriteAllRoles.ReadFireProp (fireShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[37].fillAmount = (float) fireShopData.fireAchievementItem.extinguishFireBeforeDeadlineValue / (float) fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedExtinguishFireBeforeDeadlineLevel].extinguishFireBeforeDeadlinLevelValue;
                valueTexts[37].text = fireShopData.fireAchievementItem.extinguishFireBeforeDeadlineValue + "/" + fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedExtinguishFireBeforeDeadlineLevel].extinguishFireBeforeDeadlinLevelValue;
                levelExplanationsTexts[37].text = fireShopData.fireAchievementItem.unlockedExtinguishFireBeforeDeadlineLevel + "/" + (fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel.Length - 1);
                rewardTexts[37].text = "" + fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedExtinguishFireBeforeDeadlineLevel].extinguishFireBeforeDeadlinReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (fireShopData.fireAchievementItem.unlockedExtinguishFireBeforeDeadlineLevel >= fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[37].text = "Max";
                }
            }
        }

    }
    private void FindColdFireAchievement () {
        if (fireShopData.fireAchievementItem.findColdReceived == false) {
            if (fireShopData.fireAchievementItem.findTheColdFireValue == true) {
                carShopUi.gameData.totalXp += fireShopData.fireAchievementItem.findTheColdFireReward;
                fireShopData.fireAchievementItem.findColdReceived = true;

                ReadWriteAllRoles.WriteFireProp (fireShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                fireShopData = ReadWriteAllRoles.ReadFireProp (fireShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[38].fillAmount = 1.0f / 1.0f;
                levelExplanationsTexts[38].text = 1 + "/" + 1;
                valueTexts[38].text = 1 + "/" + 1;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                valueTexts[38].text = "Max";

            }
        }

    }
    private void UpgradeMaxRoleAchievement () {
        if (fireShopData.fireAchievementItem.unlockedUpgradeMaxRoleLevel < fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel.Length - 1) {
            if (fireShopData.fireAchievementItem.upgradeMaxRoleValue >= fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedUpgradeMaxRoleLevel].upgradeMaxRoleLevelValue) {
                carShopUi.gameData.totalXp += fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedUpgradeMaxRoleLevel].upgradeMaxRoleReward;
                fireShopData.fireAchievementItem.unlockedUpgradeMaxRoleLevel++;

                ReadWriteAllRoles.WriteFireProp (fireShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                fireShopData = ReadWriteAllRoles.ReadFireProp (fireShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[39].fillAmount = (float) fireShopData.fireAchievementItem.upgradeMaxRoleValue / (float) fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedUpgradeMaxRoleLevel].upgradeMaxRoleLevelValue;
                valueTexts[39].text = fireShopData.fireAchievementItem.upgradeMaxRoleValue + "/" + fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedUpgradeMaxRoleLevel].upgradeMaxRoleLevelValue;
                levelExplanationsTexts[39].text = fireShopData.fireAchievementItem.unlockedUpgradeMaxRoleLevel + "/" + (fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel.Length - 1);
                rewardTexts[39].text = "" + fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel[fireShopData.fireAchievementItem.unlockedUpgradeMaxRoleLevel].upgradeMaxRoleReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (fireShopData.fireAchievementItem.unlockedUpgradeMaxRoleLevel >= fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[39].text = "Max";
                }
            }
        }

    }

    //Police Achievements
    private void CatchThiefCrimeSceneAchievement () {
        if (policeShopData.policeAchievementItem.unlockedXCatchThiefCrimeSceneLevel < policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel.Length - 1) {
            if (policeShopData.policeAchievementItem.xCatchThiefCrimeSceneValue >= policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedXCatchThiefCrimeSceneLevel].xCatchThiefCrimeSceneLevelValue) {
                carShopUi.gameData.totalXp += policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedXCatchThiefCrimeSceneLevel].xCatchThiefCrimeSceneReward;
                policeShopData.policeAchievementItem.unlockedXCatchThiefCrimeSceneLevel++;

                ReadWriteAllRoles.WritePoliceProp (policeShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                policeShopData = ReadWriteAllRoles.ReadPoliceProp (policeShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[40].fillAmount = (float) policeShopData.policeAchievementItem.xCatchThiefCrimeSceneValue / (float) policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedXCatchThiefCrimeSceneLevel].xCatchThiefCrimeSceneLevelValue;
                valueTexts[40].text = policeShopData.policeAchievementItem.xCatchThiefCrimeSceneValue + "/" + policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedXCatchThiefCrimeSceneLevel].xCatchThiefCrimeSceneLevelValue;
                levelExplanationsTexts[40].text = policeShopData.policeAchievementItem.unlockedXCatchThiefCrimeSceneLevel + "/" + (policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel.Length - 1);
                rewardTexts[40].text = "" + policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedXCatchThiefCrimeSceneLevel].xCatchThiefCrimeSceneReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (policeShopData.policeAchievementItem.unlockedXCatchThiefCrimeSceneLevel >= policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[40].text = "Max";
                }

            }
        }

    }

    private void GainBonusMoneyFromBlockingRobberyAchievement () {
        if (policeShopData.policeAchievementItem.unlockedGainXBonusMoneyFromBlockingRobberyLevel < policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel.Length - 1) {
            if (policeShopData.policeAchievementItem.gainXBonusMoneyFromBlockingRobberyValue >= policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedGainXBonusMoneyFromBlockingRobberyLevel].gainXBonusMoneyFromBlockingRobberyLevelValue) {
                carShopUi.gameData.totalXp += policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedGainXBonusMoneyFromBlockingRobberyLevel].gainXBonusMoneyFromBlockingRobberyReward;
                policeShopData.policeAchievementItem.unlockedGainXBonusMoneyFromBlockingRobberyLevel++;

                ReadWriteAllRoles.WritePoliceProp (policeShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                policeShopData = ReadWriteAllRoles.ReadPoliceProp (policeShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[41].fillAmount = (float) policeShopData.policeAchievementItem.gainXBonusMoneyFromBlockingRobberyValue / (float) policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedGainXBonusMoneyFromBlockingRobberyLevel].gainXBonusMoneyFromBlockingRobberyLevelValue;
                valueTexts[41].text = policeShopData.policeAchievementItem.gainXBonusMoneyFromBlockingRobberyValue + "/" + policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedGainXBonusMoneyFromBlockingRobberyLevel].gainXBonusMoneyFromBlockingRobberyLevelValue;
                levelExplanationsTexts[41].text = policeShopData.policeAchievementItem.unlockedGainXBonusMoneyFromBlockingRobberyLevel + "/" + (policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel.Length - 1);
                rewardTexts[41].text = "" + policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedGainXBonusMoneyFromBlockingRobberyLevel].gainXBonusMoneyFromBlockingRobberyReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (policeShopData.policeAchievementItem.unlockedGainXBonusMoneyFromBlockingRobberyLevel >= policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[41].text = "Max";
                }
            }
        }

    }

    private void CatchThiefAchievement () {
        if (policeShopData.policeAchievementItem.unlockedXCatchThiefLevel < policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel.Length - 1) {
            if (policeShopData.policeAchievementItem.xCatchThiefValue >= policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedXCatchThiefLevel].xCatchThiefLevelValue) {
                carShopUi.gameData.totalXp += policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedXCatchThiefLevel].xCatchThiefReward;
                policeShopData.policeAchievementItem.unlockedXCatchThiefLevel++;

                ReadWriteAllRoles.WritePoliceProp (policeShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                policeShopData = ReadWriteAllRoles.ReadPoliceProp (policeShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[42].fillAmount = (float) policeShopData.policeAchievementItem.xCatchThiefValue / (float) policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedXCatchThiefLevel].xCatchThiefLevelValue;
                valueTexts[42].text = policeShopData.policeAchievementItem.xCatchThiefValue + "/" + policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedXCatchThiefLevel].xCatchThiefLevelValue;
                levelExplanationsTexts[42].text = policeShopData.policeAchievementItem.unlockedXCatchThiefLevel + "/" + (policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel.Length - 1);
                rewardTexts[42].text = "" + policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedXCatchThiefLevel].xCatchThiefReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (policeShopData.policeAchievementItem.unlockedXCatchThiefLevel >= policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[42].text = "Max";
                }
            }
        }

    }

    private void CatchThiefWitchScoutAchievement () {
        if (policeShopData.policeAchievementItem.unlockedXCatchWithScoutLevel < policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel.Length - 1) {
            if (policeShopData.policeAchievementItem.xCatchWithScoutValue >= policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedXCatchWithScoutLevel].xCatchWithScoutLevelValue) {
                carShopUi.gameData.totalXp += policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedXCatchWithScoutLevel].xCatchWithScoutReward;
                policeShopData.policeAchievementItem.unlockedXCatchWithScoutLevel++;

                ReadWriteAllRoles.WritePoliceProp (policeShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                policeShopData = ReadWriteAllRoles.ReadPoliceProp (policeShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[43].fillAmount = (float) policeShopData.policeAchievementItem.xCatchWithScoutValue / (float) policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedXCatchWithScoutLevel].xCatchWithScoutLevelValue;
                valueTexts[43].text = policeShopData.policeAchievementItem.xCatchWithScoutValue + "/" + policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedXCatchWithScoutLevel].xCatchWithScoutLevelValue;
                levelExplanationsTexts[43].text = policeShopData.policeAchievementItem.unlockedXCatchWithScoutLevel + "/" + (policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel.Length - 1);
                rewardTexts[43].text = "" + policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedXCatchWithScoutLevel].xCatchWithScoutReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (policeShopData.policeAchievementItem.unlockedXCatchWithScoutLevel >= policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[43].text = "Max";
                }
            }
        }

    }

    private void DoMaxLevelThiefSpeedDecreaseAchievement () {
        if (policeShopData.policeAchievementItem.unlockedDoMaxLevelSlowThiefLevel < policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel.Length - 1) {
            if (policeShopData.policeAchievementItem.doMaxLevelSlowThiefValue >= policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedDoMaxLevelSlowThiefLevel].doMaxLevelSlowThiefLevelValue) {
                carShopUi.gameData.totalXp += policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedDoMaxLevelSlowThiefLevel].doMaxLevelSlowThiefReward;
                policeShopData.policeAchievementItem.unlockedDoMaxLevelSlowThiefLevel++;

                ReadWriteAllRoles.WritePoliceProp (policeShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                policeShopData = ReadWriteAllRoles.ReadPoliceProp (policeShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[44].fillAmount = (float) policeShopData.policeAchievementItem.doMaxLevelSlowThiefValue / (float) policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedDoMaxLevelSlowThiefLevel].doMaxLevelSlowThiefLevelValue;
                valueTexts[44].text = policeShopData.policeAchievementItem.doMaxLevelSlowThiefValue + "/" + policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedDoMaxLevelSlowThiefLevel].doMaxLevelSlowThiefLevelValue;
                levelExplanationsTexts[44].text = policeShopData.policeAchievementItem.unlockedDoMaxLevelSlowThiefLevel + "/" + (policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel.Length - 1);
                rewardTexts[44].text = "" + policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedDoMaxLevelSlowThiefLevel].doMaxLevelSlowThiefReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (policeShopData.policeAchievementItem.unlockedDoMaxLevelSlowThiefLevel >= policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[44].text = "Max";
                }
            }
        }

    }

    private void CrashThiefAchievement () {
        if (policeShopData.policeAchievementItem.unlockedXCrashThiefLevel < policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel.Length - 1) {
            if (policeShopData.policeAchievementItem.xCrashThiefValue >= policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedXCrashThiefLevel].xCrashThiefLevelValue) {
                carShopUi.gameData.totalXp += policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedXCrashThiefLevel].xCrashThiefReward;
                policeShopData.policeAchievementItem.unlockedXCrashThiefLevel++;

                ReadWriteAllRoles.WritePoliceProp (policeShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                policeShopData = ReadWriteAllRoles.ReadPoliceProp (policeShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[45].fillAmount = (float) policeShopData.policeAchievementItem.xCrashThiefValue / (float) policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedXCrashThiefLevel].xCrashThiefLevelValue;
                valueTexts[45].text = policeShopData.policeAchievementItem.xCrashThiefValue + "/" + policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedXCrashThiefLevel].xCrashThiefLevelValue;
                levelExplanationsTexts[45].text = policeShopData.policeAchievementItem.unlockedXCrashThiefLevel + "/" + (policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel.Length - 1);
                rewardTexts[45].text = "" + policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedXCrashThiefLevel].xCrashThiefReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (policeShopData.policeAchievementItem.unlockedXCrashThiefLevel >= policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[45].text = "Max";
                }
            }
        }

    }

    private void UseXVanguardAchievement () {
        if (policeShopData.policeAchievementItem.unlockedUseXVanguardLevel < policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel.Length - 1) {
            if (policeShopData.policeAchievementItem.useXVanguardValue >= policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedUseXVanguardLevel].useXVanguardLevelValue) {
                carShopUi.gameData.totalXp += policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedUseXVanguardLevel].useXVanguardReward;
                policeShopData.policeAchievementItem.unlockedUseXVanguardLevel++;

                ReadWriteAllRoles.WritePoliceProp (policeShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                policeShopData = ReadWriteAllRoles.ReadPoliceProp (policeShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[46].fillAmount = (float) policeShopData.policeAchievementItem.useXVanguardValue / (float) policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedUseXVanguardLevel].useXVanguardLevelValue;
                valueTexts[46].text = policeShopData.policeAchievementItem.useXVanguardValue + "/" + policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedUseXVanguardLevel].useXVanguardLevelValue;
                levelExplanationsTexts[46].text = policeShopData.policeAchievementItem.unlockedUseXVanguardLevel + "/" + (policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel.Length - 1);
                rewardTexts[46].text = "" + policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedUseXVanguardLevel].useXVanguardReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (policeShopData.policeAchievementItem.unlockedUseXVanguardLevel >= policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[46].text = "Max";
                }
            }
        }

    }

    private void NoAccidentPoliceAchievement () {
        if (policeShopData.policeAchievementItem.unlockedNoAccidentPoliceLevel < policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel.Length - 1) {
            if (policeShopData.policeAchievementItem.noAccidentPoliceValue >= policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedNoAccidentPoliceLevel].noAccidentPoliceLevelValue) {
                carShopUi.gameData.totalXp += policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedNoAccidentPoliceLevel].noAccidentPoliceReward;
                policeShopData.policeAchievementItem.unlockedNoAccidentPoliceLevel++;

                ReadWriteAllRoles.WritePoliceProp (policeShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                policeShopData = ReadWriteAllRoles.ReadPoliceProp (policeShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[47].fillAmount = (float) policeShopData.policeAchievementItem.noAccidentPoliceValue / (float) policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedNoAccidentPoliceLevel].noAccidentPoliceLevelValue;
                valueTexts[47].text = policeShopData.policeAchievementItem.noAccidentPoliceValue + "/" + policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedNoAccidentPoliceLevel].noAccidentPoliceLevelValue;
                levelExplanationsTexts[47].text = policeShopData.policeAchievementItem.unlockedNoAccidentPoliceLevel + "/" + (policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel.Length - 1);
                rewardTexts[47].text = "" + policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedNoAccidentPoliceLevel].noAccidentPoliceReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (policeShopData.policeAchievementItem.unlockedNoAccidentPoliceLevel >= policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[47].text = "Max";
                }
            }
        }

    }

    private void FindRobberyMoneyAchievement () {
        if (policeShopData.policeAchievementItem.findRobberyReceived == false) {
            if (policeShopData.policeAchievementItem.findRobberyMoneyValue == true) {
                carShopUi.gameData.totalXp += policeShopData.policeAchievementItem.findRobberyMoneyReward;
                policeShopData.policeAchievementItem.findRobberyReceived = true;

                ReadWriteAllRoles.WritePoliceProp (policeShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                policeShopData = ReadWriteAllRoles.ReadPoliceProp (policeShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[48].fillAmount = 1.0f / 1.0f;
                levelExplanationsTexts[48].text = 1 + "/" + 1;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                valueTexts[48].text = "Max";

            }
        }

    }

    private void SolveAllCaseSevenhillAchievement () {
        if (policeShopData.policeAchievementItem.unlockedSolveAllCaseInSevenhillLevel < policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel.Length - 1) {
            if (policeShopData.policeAchievementItem.solveAllCaseInSevenhillValue >= policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedSolveAllCaseInSevenhillLevel].solveAllCaseInSevenhillLevelValue) {
                carShopUi.gameData.totalXp += policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedSolveAllCaseInSevenhillLevel].solveAllCaseInSevenhillReward;
                policeShopData.policeAchievementItem.unlockedSolveAllCaseInSevenhillLevel++;

                ReadWriteAllRoles.WritePoliceProp (policeShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                policeShopData = ReadWriteAllRoles.ReadPoliceProp (policeShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[49].fillAmount = (float) policeShopData.policeAchievementItem.solveAllCaseInSevenhillValue / (float) policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedSolveAllCaseInSevenhillLevel].solveAllCaseInSevenhillLevelValue;
                valueTexts[49].text = policeShopData.policeAchievementItem.solveAllCaseInSevenhillValue + "/" + policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedSolveAllCaseInSevenhillLevel].solveAllCaseInSevenhillLevelValue;
                levelExplanationsTexts[49].text = policeShopData.policeAchievementItem.unlockedSolveAllCaseInSevenhillLevel + "/" + (policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel.Length - 1);
                rewardTexts[49].text = "" + policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel[policeShopData.policeAchievementItem.unlockedSolveAllCaseInSevenhillLevel].solveAllCaseInSevenhillReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (policeShopData.policeAchievementItem.unlockedSolveAllCaseInSevenhillLevel >= policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[49].text = "Max";
                }
            }
        }

    }

    //Taxi Achievements
    private void CrashSignBoardAchievement () {
        if (taxiShopData.taxiAchievementItem.unlockedCrashSignBoardLevel < taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel.Length - 1) {
            if (taxiShopData.taxiAchievementItem.crashSignBoardValue >= taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedCrashSignBoardLevel].crashSignBoardBoxLevelValue) {
                carShopUi.gameData.totalXp += taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedCrashSignBoardLevel].crashSignBoardBoxReward;
                taxiShopData.taxiAchievementItem.unlockedCrashSignBoardLevel++;

                ReadWriteAllRoles.WriteTaxiProp (taxiShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                taxiShopData = ReadWriteAllRoles.ReadTaxiProp (taxiShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[50].fillAmount = (float) taxiShopData.taxiAchievementItem.crashSignBoardValue / (float) taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedCrashSignBoardLevel].crashSignBoardBoxLevelValue;
                valueTexts[50].text = taxiShopData.taxiAchievementItem.crashSignBoardValue + "/" + taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedCrashSignBoardLevel].crashSignBoardBoxLevelValue;
                levelExplanationsTexts[50].text = taxiShopData.taxiAchievementItem.unlockedCrashSignBoardLevel + "/" + (taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel.Length - 1);
                rewardTexts[50].text = "" + taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedCrashSignBoardLevel].crashSignBoardBoxReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (taxiShopData.taxiAchievementItem.unlockedCrashSignBoardLevel >= taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[50].text = "Max";
                }

            }
        }

    }

    private void FirstCityTravelAchievement () {
        if (taxiShopData.taxiAchievementItem.unlockedFirstCityTravelLevel < taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel.Length - 1) {
            if (taxiShopData.taxiAchievementItem.firstCityTravelValue >= taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedFirstCityTravelLevel].firstCityTravelLevelValue) {
                carShopUi.gameData.totalXp += taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedFirstCityTravelLevel].firstCityTravelReward;
                taxiShopData.taxiAchievementItem.unlockedFirstCityTravelLevel++;

                ReadWriteAllRoles.WriteTaxiProp (taxiShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                taxiShopData = ReadWriteAllRoles.ReadTaxiProp (taxiShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[51].fillAmount = (float) taxiShopData.taxiAchievementItem.firstCityTravelValue / (float) taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedFirstCityTravelLevel].firstCityTravelLevelValue;
                valueTexts[51].text = taxiShopData.taxiAchievementItem.firstCityTravelValue + "/" + taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedFirstCityTravelLevel].firstCityTravelLevelValue;
                levelExplanationsTexts[51].text = taxiShopData.taxiAchievementItem.unlockedFirstCityTravelLevel + "/" + (taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel.Length - 1);
                rewardTexts[51].text = "" + taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedFirstCityTravelLevel].firstCityTravelReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (taxiShopData.taxiAchievementItem.unlockedFirstCityTravelLevel >= taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[51].text = "Max";
                }
            }
        }

    }

    private void HundredSatisfactionPercentAchievement () {
        if (taxiShopData.taxiAchievementItem.unlockedHundredPercentSatisfactionLevel < taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel.Length - 1) {
            if (taxiShopData.taxiAchievementItem.hundredPercentSatisfactionValue >= taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedHundredPercentSatisfactionLevel].hundredPercentSatisfactionLevelValue) {
                carShopUi.gameData.totalXp += taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedHundredPercentSatisfactionLevel].hundredPercentSatisfactionReward;
                taxiShopData.taxiAchievementItem.unlockedHundredPercentSatisfactionLevel++;

                ReadWriteAllRoles.WriteTaxiProp (taxiShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                taxiShopData = ReadWriteAllRoles.ReadTaxiProp (taxiShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[52].fillAmount = (float) taxiShopData.taxiAchievementItem.hundredPercentSatisfactionValue / (float) taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedHundredPercentSatisfactionLevel].hundredPercentSatisfactionLevelValue;
                valueTexts[52].text = taxiShopData.taxiAchievementItem.hundredPercentSatisfactionValue + "/" + taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedHundredPercentSatisfactionLevel].hundredPercentSatisfactionLevelValue;
                levelExplanationsTexts[52].text = taxiShopData.taxiAchievementItem.unlockedHundredPercentSatisfactionLevel + "/" + (taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel.Length - 1);
                rewardTexts[52].text = "" + taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedHundredPercentSatisfactionLevel].hundredPercentSatisfactionReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (taxiShopData.taxiAchievementItem.unlockedHundredPercentSatisfactionLevel >= taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[52].text = "Max";
                }
            }
        }

    }

    private void FourCrazyDriveAchievement () {
        if (taxiShopData.taxiAchievementItem.unlockedUse4TimesCrazyOneGameLevel < taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel.Length - 1) {
            if (taxiShopData.taxiAchievementItem.use4TimesCrazyOneGameValue >= taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedUse4TimesCrazyOneGameLevel].use4TimesCrazyOneGameLevelValue) {
                carShopUi.gameData.totalXp += taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedUse4TimesCrazyOneGameLevel].use4TimesCrazyOneGameReward;
                taxiShopData.taxiAchievementItem.unlockedUse4TimesCrazyOneGameLevel++;

                ReadWriteAllRoles.WriteTaxiProp (taxiShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                taxiShopData = ReadWriteAllRoles.ReadTaxiProp (taxiShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[53].fillAmount = (float) taxiShopData.taxiAchievementItem.use4TimesCrazyOneGameValue / (float) taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedUse4TimesCrazyOneGameLevel].use4TimesCrazyOneGameLevelValue;
                valueTexts[53].text = taxiShopData.taxiAchievementItem.use4TimesCrazyOneGameValue + "/" + taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedUse4TimesCrazyOneGameLevel].use4TimesCrazyOneGameLevelValue;
                levelExplanationsTexts[53].text = taxiShopData.taxiAchievementItem.unlockedUse4TimesCrazyOneGameLevel + "/" + (taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel.Length - 1);
                rewardTexts[53].text = "" + taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedUse4TimesCrazyOneGameLevel].use4TimesCrazyOneGameReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (taxiShopData.taxiAchievementItem.unlockedUse4TimesCrazyOneGameLevel >= taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[53].text = "Max";
                }
            }
        }

    }

    private void NoAccidentTaxiAchievement () {
        if (taxiShopData.taxiAchievementItem.unlockedXNoAccidentLevel < taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel.Length - 1) {
            if (taxiShopData.taxiAchievementItem.xNoAccidentValue >= taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedXNoAccidentLevel].xNoAccidentLevelValue) {
                carShopUi.gameData.totalXp += taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedXNoAccidentLevel].xNoAccidentReward;
                taxiShopData.taxiAchievementItem.unlockedXNoAccidentLevel++;

                ReadWriteAllRoles.WriteTaxiProp (taxiShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                taxiShopData = ReadWriteAllRoles.ReadTaxiProp (taxiShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[54].fillAmount = (float) taxiShopData.taxiAchievementItem.xNoAccidentValue / (float) taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedXNoAccidentLevel].xNoAccidentLevelValue;
                valueTexts[54].text = taxiShopData.taxiAchievementItem.xNoAccidentValue + "/" + taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedXNoAccidentLevel].xNoAccidentLevelValue;
                levelExplanationsTexts[54].text = taxiShopData.taxiAchievementItem.unlockedXNoAccidentLevel + "/" + (taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel.Length - 1);
                rewardTexts[54].text = "" + taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedXNoAccidentLevel].xNoAccidentReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (taxiShopData.taxiAchievementItem.unlockedXNoAccidentLevel >= taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[54].text = "Max";
                }
            }
        }

    }

    private void UseXCateringAchievement () {
        if (taxiShopData.taxiAchievementItem.unlockedUseXCateringLevel < taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel.Length - 1) {
            if (taxiShopData.taxiAchievementItem.useXCateringValue >= taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedUseXCateringLevel].useXCateringLevelValue) {
                carShopUi.gameData.totalXp += taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedUseXCateringLevel].useXCateringReward;
                taxiShopData.taxiAchievementItem.unlockedUseXCateringLevel++;

                ReadWriteAllRoles.WriteTaxiProp (taxiShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                taxiShopData = ReadWriteAllRoles.ReadTaxiProp (taxiShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[55].fillAmount = (float) taxiShopData.taxiAchievementItem.useXCateringValue / (float) taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedUseXCateringLevel].useXCateringLevelValue;
                valueTexts[55].text = taxiShopData.taxiAchievementItem.useXCateringValue + "/" + taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedUseXCateringLevel].useXCateringLevelValue;
                levelExplanationsTexts[55].text = taxiShopData.taxiAchievementItem.unlockedUseXCateringLevel + "/" + (taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel.Length - 1);
                rewardTexts[55].text = "" + taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedUseXCateringLevel].useXCateringReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (taxiShopData.taxiAchievementItem.unlockedUseXCateringLevel >= taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[55].text = "Max";
                }
            }
        }

    }

    private void NoGasolineGiveMoneyAchievement () {
        if (taxiShopData.taxiAchievementItem.unlockedNoGasolineGiveMoneyLevel < taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel.Length - 1) {
            if (taxiShopData.taxiAchievementItem.noGasolineGiveMoneyValue >= taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedNoGasolineGiveMoneyLevel].noGasolineGiveMoneyLevelValue) {
                carShopUi.gameData.totalXp += taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedNoGasolineGiveMoneyLevel].noGasolineGiveMoneyReward;
                taxiShopData.taxiAchievementItem.unlockedNoGasolineGiveMoneyLevel++;

                ReadWriteAllRoles.WriteTaxiProp (taxiShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                taxiShopData = ReadWriteAllRoles.ReadTaxiProp (taxiShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[56].fillAmount = (float) taxiShopData.taxiAchievementItem.noGasolineGiveMoneyValue / (float) taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedNoGasolineGiveMoneyLevel].noGasolineGiveMoneyLevelValue;
                valueTexts[56].text = taxiShopData.taxiAchievementItem.noGasolineGiveMoneyValue + "/" + taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedNoGasolineGiveMoneyLevel].noGasolineGiveMoneyLevelValue;
                levelExplanationsTexts[56].text = taxiShopData.taxiAchievementItem.unlockedNoGasolineGiveMoneyLevel + "/" + (taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel.Length - 1);
                rewardTexts[56].text = "" + taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedNoGasolineGiveMoneyLevel].noGasolineGiveMoneyReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (taxiShopData.taxiAchievementItem.unlockedNoGasolineGiveMoneyLevel >= taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[56].text = "Max";
                }
            }
        }

    }

    private void GainMoneySatisfactionAchievement () {
        if (taxiShopData.taxiAchievementItem.unlockedGainXMoneyFromSatisfactionLevel < taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel.Length - 1) {
            if (taxiShopData.taxiAchievementItem.gainXMoneyFromSatisfactionValue >= taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedGainXMoneyFromSatisfactionLevel].gainXMoneyFromSatisfactionLevelValue) {
                carShopUi.gameData.totalXp += taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedGainXMoneyFromSatisfactionLevel].gainXMoneyFromSatisfactionReward;
                taxiShopData.taxiAchievementItem.unlockedGainXMoneyFromSatisfactionLevel++;

                ReadWriteAllRoles.WriteTaxiProp (taxiShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                taxiShopData = ReadWriteAllRoles.ReadTaxiProp (taxiShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[57].fillAmount = (float) taxiShopData.taxiAchievementItem.gainXMoneyFromSatisfactionValue / (float) taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedGainXMoneyFromSatisfactionLevel].gainXMoneyFromSatisfactionLevelValue;
                valueTexts[57].text = taxiShopData.taxiAchievementItem.gainXMoneyFromSatisfactionValue + "/" + taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedGainXMoneyFromSatisfactionLevel].gainXMoneyFromSatisfactionLevelValue;
                levelExplanationsTexts[57].text = taxiShopData.taxiAchievementItem.unlockedGainXMoneyFromSatisfactionLevel + "/" + (taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel.Length - 1);
                rewardTexts[57].text = "" + taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedGainXMoneyFromSatisfactionLevel].gainXMoneyFromSatisfactionReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (taxiShopData.taxiAchievementItem.unlockedGainXMoneyFromSatisfactionLevel >= taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[57].text = "Max";
                }
            }
        }

    }

    private void FindTheFamousCustomerAchievement () {
        if (taxiShopData.taxiAchievementItem.findFamousReceived) {
            if (taxiShopData.taxiAchievementItem.findTheFamousCustomerValue == true) {
                carShopUi.gameData.totalXp += taxiShopData.taxiAchievementItem.findTheFamousCustomerReward;

                ReadWriteAllRoles.WriteTaxiProp (taxiShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                taxiShopData = ReadWriteAllRoles.ReadTaxiProp (taxiShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[58].fillAmount = 1.0f / 1.0f;
                levelExplanationsTexts[58].text = 1 + "/" + 1;
                valueTexts[58].text = 1 + "/" + 1;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                valueTexts[58].text = "Max";
            }
        }

    }

    private void GasolineFullingAchievement () {
        if (taxiShopData.taxiAchievementItem.unlockedGasolineFullingLevel < taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel.Length - 1) {
            if (taxiShopData.taxiAchievementItem.gasolineFullingValue >= taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedGasolineFullingLevel].gasolineFullingLevelValue) {
                carShopUi.gameData.totalXp += taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedGasolineFullingLevel].gasolineFullingReward;
                taxiShopData.taxiAchievementItem.unlockedGasolineFullingLevel++;

                ReadWriteAllRoles.WriteTaxiProp (taxiShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                taxiShopData = ReadWriteAllRoles.ReadTaxiProp (taxiShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[59].fillAmount = (float) taxiShopData.taxiAchievementItem.gasolineFullingValue / (float) taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedGasolineFullingLevel].gasolineFullingLevelValue;
                valueTexts[59].text = taxiShopData.taxiAchievementItem.gasolineFullingValue + "/" + taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedGasolineFullingLevel].gasolineFullingLevelValue;
                levelExplanationsTexts[59].text = taxiShopData.taxiAchievementItem.unlockedGasolineFullingLevel + "/" + (taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel.Length - 1);
                rewardTexts[59].text = "" + taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel[taxiShopData.taxiAchievementItem.unlockedGasolineFullingLevel].gasolineFullingReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (taxiShopData.taxiAchievementItem.unlockedGasolineFullingLevel >= taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[59].text = "Max";
                }
            }
        }

    }

    //Ambulance Achievements
    private void GainMoneyFromExtraPatientAchievement () {
        if (ambulanceShopData.ambulanceAchievementItem.unlockedXGainMoneyFromExtraPatientLevel < ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel.Length - 1) {
            if (ambulanceShopData.ambulanceAchievementItem.xGainMoneyFromExtraPatientValue >= ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXGainMoneyFromExtraPatientLevel].xGainMoneyFromExtraPatientLevelValue) {
                carShopUi.gameData.totalXp += ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXGainMoneyFromExtraPatientLevel].xGainMoneyFromExtraPatientReward;
                ambulanceShopData.ambulanceAchievementItem.unlockedXGainMoneyFromExtraPatientLevel++;

                ReadWriteAllRoles.WriteAmbulanceProp (ambulanceShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                ambulanceShopData = ReadWriteAllRoles.ReadAmbulanceProp (ambulanceShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[60].fillAmount = (float) ambulanceShopData.ambulanceAchievementItem.xGainMoneyFromExtraPatientValue / (float) ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXGainMoneyFromExtraPatientLevel].xGainMoneyFromExtraPatientLevelValue;
                valueTexts[60].text = ambulanceShopData.ambulanceAchievementItem.xGainMoneyFromExtraPatientValue + "/" + ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXGainMoneyFromExtraPatientLevel].xGainMoneyFromExtraPatientLevelValue;
                levelExplanationsTexts[60].text = ambulanceShopData.ambulanceAchievementItem.unlockedXGainMoneyFromExtraPatientLevel + "/" + (ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel.Length - 1);
                rewardTexts[60].text = "" + ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXGainMoneyFromExtraPatientLevel].xGainMoneyFromExtraPatientReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (ambulanceShopData.ambulanceAchievementItem.unlockedXGainMoneyFromExtraPatientLevel >= ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[60].text = "Max";
                }

            }
        }

    }

    private void MultiplePatientWithoutDyingAchievement () {
        if (ambulanceShopData.ambulanceAchievementItem.unlockedXMultiplePatientWithoutDyingLevel < ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel.Length - 1) {
            if (ambulanceShopData.ambulanceAchievementItem.xMultiplePatientWithoutDyingValue >= ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXMultiplePatientWithoutDyingLevel].xMultiplePatientWithoutDyingLevelValue) {
                carShopUi.gameData.totalXp += ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXMultiplePatientWithoutDyingLevel].xMultiplePatientWithoutDyingReward;
                ambulanceShopData.ambulanceAchievementItem.unlockedXMultiplePatientWithoutDyingLevel++;

                ReadWriteAllRoles.WriteAmbulanceProp (ambulanceShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                ambulanceShopData = ReadWriteAllRoles.ReadAmbulanceProp (ambulanceShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[61].fillAmount = (float) ambulanceShopData.ambulanceAchievementItem.xMultiplePatientWithoutDyingValue / (float) ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXMultiplePatientWithoutDyingLevel].xMultiplePatientWithoutDyingLevelValue;
                valueTexts[61].text = ambulanceShopData.ambulanceAchievementItem.xMultiplePatientWithoutDyingValue + "/" + ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXMultiplePatientWithoutDyingLevel].xMultiplePatientWithoutDyingLevelValue;
                levelExplanationsTexts[61].text = ambulanceShopData.ambulanceAchievementItem.unlockedXMultiplePatientWithoutDyingLevel + "/" + (ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel.Length - 1);
                rewardTexts[61].text = "" + ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXMultiplePatientWithoutDyingLevel].xMultiplePatientWithoutDyingReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (ambulanceShopData.ambulanceAchievementItem.unlockedXMultiplePatientWithoutDyingLevel >= ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[61].text = "Max";
                }
            }
        }

    }

    private void UseElectroShockAchievement () {
        if (ambulanceShopData.ambulanceAchievementItem.unlockedXUseElectroShockLevel < ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel.Length - 1) {
            if (ambulanceShopData.ambulanceAchievementItem.xUseElectroShockValue >= ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXUseElectroShockLevel].xUseElectroShockLevelValue) {
                carShopUi.gameData.totalXp += ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXUseElectroShockLevel].xUseElectroShockReward;
                ambulanceShopData.ambulanceAchievementItem.unlockedXUseElectroShockLevel++;

                ReadWriteAllRoles.WriteAmbulanceProp (ambulanceShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                ambulanceShopData = ReadWriteAllRoles.ReadAmbulanceProp (ambulanceShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[62].fillAmount = (float) ambulanceShopData.ambulanceAchievementItem.xUseElectroShockValue / (float) ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXUseElectroShockLevel].xUseElectroShockLevelValue;
                valueTexts[62].text = ambulanceShopData.ambulanceAchievementItem.xUseElectroShockValue + "/" + ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXUseElectroShockLevel].xUseElectroShockLevelValue;
                levelExplanationsTexts[62].text = ambulanceShopData.ambulanceAchievementItem.unlockedXUseElectroShockLevel + "/" + (ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel.Length - 1);
                rewardTexts[62].text = "" + ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXUseElectroShockLevel].xUseElectroShockReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (ambulanceShopData.ambulanceAchievementItem.unlockedXUseElectroShockLevel >= ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[62].text = "Max";
                }
            }
        }

    }

    private void PatientWithoutDyingAchievement () {
        if (ambulanceShopData.ambulanceAchievementItem.unlockedXPatientWithoutDyingLevel < ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel.Length - 1) {
            if (ambulanceShopData.ambulanceAchievementItem.xPatientWithoutDyingValue >= ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXPatientWithoutDyingLevel].xPatientWithoutDyingLevelValue) {
                carShopUi.gameData.totalXp += ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXPatientWithoutDyingLevel].xPatientWithoutDyingReward;
                ambulanceShopData.ambulanceAchievementItem.unlockedXPatientWithoutDyingLevel++;

                ReadWriteAllRoles.WriteAmbulanceProp (ambulanceShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                ambulanceShopData = ReadWriteAllRoles.ReadAmbulanceProp (ambulanceShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[63].fillAmount = (float) ambulanceShopData.ambulanceAchievementItem.xPatientWithoutDyingValue / (float) ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXPatientWithoutDyingLevel].xPatientWithoutDyingLevelValue;
                valueTexts[63].text = ambulanceShopData.ambulanceAchievementItem.xPatientWithoutDyingValue + "/" + ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXPatientWithoutDyingLevel].xPatientWithoutDyingLevelValue;
                levelExplanationsTexts[63].text = ambulanceShopData.ambulanceAchievementItem.unlockedXPatientWithoutDyingLevel + "/" + (ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel.Length - 1);
                rewardTexts[63].text = "" + ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXPatientWithoutDyingLevel].xPatientWithoutDyingReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (ambulanceShopData.ambulanceAchievementItem.unlockedXPatientWithoutDyingLevel >= ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[63].text = "Max";
                }
            }
        }

    }

    private void UseAdrenalinAchievement () {
        if (ambulanceShopData.ambulanceAchievementItem.unlockedXUseAdrenalinLevel < ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel.Length - 1) {
            if (ambulanceShopData.ambulanceAchievementItem.xUseAdrenalinValue >= ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXUseAdrenalinLevel].xUseAdrenalinLevelValue) {
                carShopUi.gameData.totalXp += ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXUseAdrenalinLevel].xUseAdrenalinReward;
                ambulanceShopData.ambulanceAchievementItem.unlockedXUseAdrenalinLevel++;

                ReadWriteAllRoles.WriteAmbulanceProp (ambulanceShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                ambulanceShopData = ReadWriteAllRoles.ReadAmbulanceProp (ambulanceShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[64].fillAmount = (float) ambulanceShopData.ambulanceAchievementItem.xUseAdrenalinValue / (float) ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXUseAdrenalinLevel].xUseAdrenalinLevelValue;
                valueTexts[64].text = ambulanceShopData.ambulanceAchievementItem.xUseAdrenalinValue + "/" + ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXUseAdrenalinLevel].xUseAdrenalinLevelValue;
                levelExplanationsTexts[64].text = ambulanceShopData.ambulanceAchievementItem.unlockedXUseAdrenalinLevel + "/" + (ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel.Length - 1);
                rewardTexts[64].text = "" + ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXUseAdrenalinLevel].xUseAdrenalinReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (ambulanceShopData.ambulanceAchievementItem.unlockedXUseAdrenalinLevel >= ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[64].text = "Max";
                }
            }
        }

    }

    private void NoAccidentNoDyingAchievement () {
        if (ambulanceShopData.ambulanceAchievementItem.unlockedXPatientWithoutDyingNoAccidentLevel < ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel.Length - 1) {
            if (ambulanceShopData.ambulanceAchievementItem.xPatientWithoutDyingNoAccidentValue >= ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXPatientWithoutDyingNoAccidentLevel].xPatientWithoutDyingNoAccidentLevelValue) {
                carShopUi.gameData.totalXp += ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXPatientWithoutDyingNoAccidentLevel].xPatientWithoutDyingNoAccidentReward;
                ambulanceShopData.ambulanceAchievementItem.unlockedXPatientWithoutDyingNoAccidentLevel++;

                ReadWriteAllRoles.WriteAmbulanceProp (ambulanceShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                ambulanceShopData = ReadWriteAllRoles.ReadAmbulanceProp (ambulanceShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[65].fillAmount = (float) ambulanceShopData.ambulanceAchievementItem.xPatientWithoutDyingNoAccidentValue / (float) ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXPatientWithoutDyingNoAccidentLevel].xPatientWithoutDyingNoAccidentLevelValue;
                valueTexts[65].text = ambulanceShopData.ambulanceAchievementItem.xPatientWithoutDyingNoAccidentValue + "/" + ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXPatientWithoutDyingNoAccidentLevel].xPatientWithoutDyingNoAccidentLevelValue;
                levelExplanationsTexts[65].text = ambulanceShopData.ambulanceAchievementItem.unlockedXPatientWithoutDyingNoAccidentLevel + "/" + (ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel.Length - 1);
                rewardTexts[65].text = "" + ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXPatientWithoutDyingNoAccidentLevel].xPatientWithoutDyingNoAccidentReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (ambulanceShopData.ambulanceAchievementItem.unlockedXPatientWithoutDyingNoAccidentLevel >= ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[65].text = "Max";
                }
            }
        }

    }

    private void PatientFullLifeAchievement () {
        if (ambulanceShopData.ambulanceAchievementItem.unlockedXPatientFullLifeLevel < ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel.Length - 1) {
            if (ambulanceShopData.ambulanceAchievementItem.xPatientFullLifeValue >= ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXPatientFullLifeLevel].xPatientFullLifeLevelValue) {
                carShopUi.gameData.totalXp += ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXPatientFullLifeLevel].xPatientFullLifeReward;
                ambulanceShopData.ambulanceAchievementItem.unlockedXPatientFullLifeLevel++;

                ReadWriteAllRoles.WriteAmbulanceProp (ambulanceShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                ambulanceShopData = ReadWriteAllRoles.ReadAmbulanceProp (ambulanceShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[66].fillAmount = (float) ambulanceShopData.ambulanceAchievementItem.xPatientFullLifeValue / (float) ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXPatientFullLifeLevel].xPatientFullLifeLevelValue;
                valueTexts[66].text = ambulanceShopData.ambulanceAchievementItem.xPatientFullLifeValue + "/" + ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXPatientFullLifeLevel].xPatientFullLifeLevelValue;
                levelExplanationsTexts[66].text = ambulanceShopData.ambulanceAchievementItem.unlockedXPatientFullLifeLevel + "/" + (ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel.Length - 1);
                rewardTexts[66].text = "" + ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXPatientFullLifeLevel].xPatientFullAdrenalinReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (ambulanceShopData.ambulanceAchievementItem.unlockedXPatientFullLifeLevel >= ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[66].text = "Max";
                }
            }
        }

    }

    private void PetientFullAdrenalinAchievement () {
        if (ambulanceShopData.ambulanceAchievementItem.unlockedXPatientFullAdrenalinLevel < ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel.Length - 1) {
            if (ambulanceShopData.ambulanceAchievementItem.xPatientFullAdrenalinValue >= ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXPatientFullAdrenalinLevel].xPatientFullAdrenalinLevelValue) {
                carShopUi.gameData.totalXp += ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXPatientFullAdrenalinLevel].xPatientFullAdrenalinReward;
                ambulanceShopData.ambulanceAchievementItem.unlockedXPatientFullAdrenalinLevel++;

                ReadWriteAllRoles.WriteAmbulanceProp (ambulanceShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                ambulanceShopData = ReadWriteAllRoles.ReadAmbulanceProp (ambulanceShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[67].fillAmount = (float) ambulanceShopData.ambulanceAchievementItem.xPatientFullAdrenalinValue / (float) ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXUseAdrenalinLevel].xPatientFullAdrenalinLevelValue;
                valueTexts[67].text = ambulanceShopData.ambulanceAchievementItem.xPatientFullAdrenalinValue + "/" + ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXUseAdrenalinLevel].xPatientFullAdrenalinLevelValue;
                levelExplanationsTexts[67].text = ambulanceShopData.ambulanceAchievementItem.unlockedXUseAdrenalinLevel + "/" + (ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel.Length - 1);
                rewardTexts[67].text = "" + ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXUseAdrenalinLevel].xUseAdrenalinReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (ambulanceShopData.ambulanceAchievementItem.unlockedXPatientFullAdrenalinLevel >= ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[67].text = "Max";
                }
            }
        }

    }

    private void UseHealAchievement () {
        if (ambulanceShopData.ambulanceAchievementItem.unlockedXUseHealLevel < ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel.Length - 1) {
            if (ambulanceShopData.ambulanceAchievementItem.xUseHealValue >= ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXUseHealLevel].xUseHealLevelValue) {
                carShopUi.gameData.totalXp += ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXUseHealLevel].xUseHealReward;
                ambulanceShopData.ambulanceAchievementItem.unlockedXUseHealLevel++;

                ReadWriteAllRoles.WriteAmbulanceProp (ambulanceShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                ambulanceShopData = ReadWriteAllRoles.ReadAmbulanceProp (ambulanceShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[68].fillAmount = (float) ambulanceShopData.ambulanceAchievementItem.xUseHealValue / (float) ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXUseHealLevel].xUseHealLevelValue;
                valueTexts[68].text = ambulanceShopData.ambulanceAchievementItem.xUseHealValue + "/" + ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXUseHealLevel].xUseHealLevelValue;
                levelExplanationsTexts[68].text = ambulanceShopData.ambulanceAchievementItem.unlockedXUseHealLevel + "/" + (ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel.Length - 1);
                rewardTexts[68].text = "" + ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel[ambulanceShopData.ambulanceAchievementItem.unlockedXUseHealLevel].xUseHealReward;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);

                if (ambulanceShopData.ambulanceAchievementItem.unlockedXUseHealLevel >= ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel.Length - 1) {
                    valueTexts[68].text = "Max";
                }
            }
        }

    }

    private void FindImmortalAchievement () {
        if (ambulanceShopData.ambulanceAchievementItem.findImmortalReceived == false) {
            if (ambulanceShopData.ambulanceAchievementItem.findTheImmortalPatientValue == true) {
                carShopUi.gameData.totalXp += ambulanceShopData.ambulanceAchievementItem.findTheImmortalPatientReward;

                ReadWriteAllRoles.WriteAmbulanceProp (ambulanceShopData);
                ReadWriteAllRoles.WriteGameProp (carShopUi.gameData);

                ambulanceShopData = ReadWriteAllRoles.ReadAmbulanceProp (ambulanceShopData);
                carShopUi.gameData = ReadWriteAllRoles.ReadGameProp (carShopUi.gameData);

                sliderImages[69].fillAmount = 1.0f / 1.0f;
                levelExplanationsTexts[69].text = 1 + "/" + 1;
                valueTexts[69].text = 1 + "/" + 1;
                xpText.text = "" + carShopUi.gameData.totalXp;

                if (busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.completeXAchievementValue++;
                }

                ReadWriteAllRoles.WriteBusProp (busShopData);
                busShopData = busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
                sliderImages[6].fillAmount = (float) busShopData.generalAchievementItem.completeXAchievementValue / (float) busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                valueTexts[6].text = busShopData.generalAchievementItem.completeXAchievementValue + "/" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXAchievementLevelValue;
                levelExplanationsTexts[6].text = busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel + "/" + (busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1);
                rewardTexts[6].text = "" + busShopData.generalAchievementItem.generalAchievementsUpgradeLevel[busShopData.generalAchievementItem.unlockedCompleteXAchievementLevel].completeXMissionReward;

                audioSource.PlayOneShot (collectAchievement);
                valueTexts[69].text = "Max";

            }

        }
    }

}