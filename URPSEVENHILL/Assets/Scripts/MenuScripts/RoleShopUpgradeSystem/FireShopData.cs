using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoleShopSystem{


   [System.Serializable]
   public class FireShopData
{
public int selectedIndex;
public int selectedRole;
public fireRoleItem fireRoleItems;
public fireAchievementsItem fireAchievementItem;

}


[System.Serializable]
public class fireRoleItem{

    public int unlockedFastPutOutLevel;
    public int unlockedWaterTankCapacityLevel;
    public int unlockedRescueLifeBonusLevel;
    public int unlockedAdditionalHoseLevel;
    public int fastPutOutMaxValue;
    public int waterTankCapacityMaxValue;
    public int rescueLifeBonusMaxValue;
    public int additionalHoseMaxValue;
    public int helicopterTenur;
    public int raidTenur;
    public FireRoleUpgradeInfo[] fireSkillUpgradeLevel;
    
}

[System.Serializable]
public class FireRoleUpgradeInfo{
    public int unlockFastPutOutCost;
    public int unlockWaterTankCapacityCost;
    public int unlockRescueLifeBonusCost;
    public int unlockAdditionalHoseCost;
    public int fastPutOutValue;
    public int waterTankCapacityValue;
    public int rescueLifeBonusValue;
    public int additionalHoseValue;
}

[System.Serializable]
public class fireAchievementsItem{

    public int unlockedXFireExtinguishPlaceLevel;
    public int unlockedXSaveLifeFromFireLevel;
    public int unlockedXSaveCatLevel;
    public int unlockedXSpentWaterLevel;
    public int unlockedXCrashWaterHydrantLevel;
    public int unlockedXFillWaterTankLevel;
    public int unlockedGainXMoneyFromSaveLifeLevel;
    public int unlockedExtinguishFireBeforeDeadlineLevel;
    public int unlockedUpgradeMaxRoleLevel;
    public int xFireExtinguishPlaceValue;
    public int xSaveLifeFromFireValue;
    public int xSaveCatValue;
    public int xSpentWaterValue;
    public int xCrashWaterHydrantValue;
    public int xFillWaterTankValue;
    public int gainXMoneyFromSaveLifeValue;
    public int extinguishFireBeforeDeadlineValue; 
    public bool findTheColdFireValue;
    public int findTheColdFireReward;
    public bool findColdReceived;
    public int upgradeMaxRoleValue;
    public FireAchievementsInfo[] fireAchievementsUpgradeLevel;
    
}

[System.Serializable]
public class FireAchievementsInfo{
    public int xFireExtinguishPlaceLevelValue;
    public int xSaveLifeFromFireLevelValue;
    public int xSaveCatLevelValue;
    public int xSpentWaterLevelValue;
    public int xCrashWaterHydrantLevelValue;
    public int xFillWaterTankLevelValue;
    public int gainXMoneyFromSaveLifeLevelValue;
    public int extinguishFireBeforeDeadlinLevelValue;
    public int upgradeMaxRoleLevelValue;
    public int xFireExtinguishPlaceReward;
    public int xSaveLifeFromFireLevelReward;
    public int xSaveCatLevelReward;
    public int xSpentWaterLevelReward;
    public int xCrashWaterHydrantLevelReward;
    public int xFillWaterTankLevelReward;
    public int gainXMoneyFromSaveLifeReward;
    public int extinguishFireBeforeDeadlinReward;
    public int upgradeMaxRoleReward;
   
}

}