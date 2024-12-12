using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoleShopSystem {

    [System.Serializable]
    public class BusShopData {
        public int selectedIndex;
        public int selectedRole;
        public busRoleItem busRoleItems;
        public busAchievementsItem busAchievementItem;
        public generalAchievementsItem generalAchievementItem;
    }

    [System.Serializable]
    public class busRoleItem {
        public int unlockedCapacityLevel;
        public int unlockedFastPassLevel;
        public int unlockedChancePassLevel;
        public int unlockedComfortDrivePriceLevel;
        public int capacityMaxValue;
        public int fastPassMaxValue;
        public int chancePassMaxValue;
        public int comfortDrivePriceMaxValue;
        public int discountTenur;
        public int arrangementTenur;
        public BusRoleUpgradeInfo[] busSkillUpgradeLevel;

    }

    [System.Serializable]
    public class BusRoleUpgradeInfo {
        public int unlockCapacityCost;
        public int unlockFastPassCost;
        public int unlockChancePassCost;
        public int unlockComfortDrivePriceCost;
        public int capacityValue;
        public int fastPassValue;
        public int chancePassValue;
        public int comfortDrivePriceValue;
    }

    [System.Serializable]
    public class busAchievementsItem {
        public int unlockedGetOnPassengerMoneyLevel;
        public int unlockedXSuitcasePassengerLevel;
        public int unlockedXArrangementLevel;
        public int unlockedNoAccidentLevel;
        public int unlockedXMissionCompleteAtLosBizaLevel;
        public int unlockedXDropThePassengerLevel;
        public int unlockedXRoleUpgradeLevel;
        public int unlockedGainXMoneyOnXSuitcasePassengerLevel;
        public int getOnPassengerMoneyValue;
        public int xSuitcasePassengerValue;
        public int xArrangementValue;
        public int noAccidentValue;
        public int xMissionCompleteAtLosBizaValue;
        public int xDropThePassengerValue;
        public bool secretBusStopValue;
        public bool allBusRotationValue;
        public int secretBusStopReward;
        public int allBusRotationReward;
        public bool secretBusReceived;
        public bool allRotationReceived;
        public int gainXMoneyOnXSuitcasePassengerValue;
        public int xRoleUpgradeValue;

        public BusAchievementInfo[] busAchievementsUpgradeLevel;

    }

    [System.Serializable]
    public class BusAchievementInfo {

        public int getOnPassengerMoneyLevelValue;
        public int xSuitcasePassengerLevelValue;
        public int xArrangementLevelValue;
        public int noAccidentLevelValue;
        public int xMissionCompleteAtLosBizaLevelValue;
        public int xDropThePassengerLevelValue;
        public int xRoleUpgradeLevelValue;
        public int gainXMoneyOnXSuitcasePassengerLevelValue;
        public int getOnPassengerMoneyReward;
        public int xSuitcasePassengerReward;
        public int xArrangementReward;
        public int noAccidentReward;
        public int xMissionCompleteAtLosBizaReward;
        public int xDropThePassengerReward;
        public int xRoleUpgradeReward;
        public int gainXMoneyOnXSuitcasePassengerReward;

    }

    [System.Serializable]
    public class generalAchievementsItem {
        public int unlockedCompleteXMissionLevel;
        public int unlockedUpgradeXRoleLevel;
        public int unlockedGainMoneyFromMissionLevel;
        public int unlockedSpendXMoneyForCarUpgradeLevel;
        public int unlockedCompleteXAchievementLevel;
        public int unlockedBuyCarLevel;
        public int unlockedXCarUpgradeLevel;
        public int unlockedOpenXTenurLevel;
        public int unlockedXUseSkillLevel;
        public int unlockedGainXExperienceLevel;
        public int completeXMissionValue;
        public int upgradeXRoleValue;
        public int gainMoneyFromMissionValue;
        public int gainXExperienceValue;
        public int buyCarValue;
        public int spendXMoneyForCarUpgradeValue;
        public int completeXAchievementValue;
        public int xCarUpgradeValue;
        public int openXTenurValue;
        public int xUseSkillValue;

        public GeneralAchievementInfo[] generalAchievementsUpgradeLevel;

    }

    [System.Serializable]
    public class GeneralAchievementInfo {

        public int completeXMissionLevelValue;
        public int upgradeXRoleLevelValue;
        public int gainMoneyFromMissionLevelValue;
        public int gainXExperienceLevelValue;
        public int completeXAchievementLevelValue;
        public int buyCarLevelValue;
        public int spendXMoneyForCarUpgradeLevelValue;
        public int xCarUpgradeLevelValue;
        public int openXTenurLevelValue;
        public int xUseSkillLevelValue;
        public int completeXMissionReward;
        public int upgradeXRoleReward;
        public int gainMoneyFromMissionReward;
        public int gainXExperienceReward;
        public int completeXAchievementReward;
        public int buyCarReward;
        public int spendXMoneyForCarUpgradeReward;
        public int xCarUpgradeReward;
        public int openXTenurReward;
        public int xUseSkillReward;

    }
}