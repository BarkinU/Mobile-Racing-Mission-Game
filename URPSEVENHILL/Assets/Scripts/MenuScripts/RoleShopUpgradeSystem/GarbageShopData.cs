using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoleShopSystem{


   [System.Serializable]
   public class GarbageShopData
{
public int selectedIndex;
public int selectedRole;
public garbageRoleItem garbageRoleItems;
public garbageAchievementsItem garbageAchievementItem;

}


[System.Serializable]
public class garbageRoleItem{

    public int unlockedGarbageCapacityLevel;
    public int unlockedFastGatherLevel;
    public int unlockedChanceGatherLevel;
    public int unlockedRecyclingIncreaseLevel;
    public int garbageCapacityMaxValue;
    public int fastGatherMaxValue;
    public int chanceGatherMaxValue;
    public int recyclingIncreaseMaxValue;
    public int compressionTenur;
    public int magnetTenur;

    public GarbageRoleUpgradeInfo[] garbageSkillUpgradeLevel;
    
}

[System.Serializable]
public class GarbageRoleUpgradeInfo{
    public int unlockGarbageCapacityCost;
    public int unlockFastGatherCost;
    public int unlockChanceGatherCost;
    public int unlockRecyclingIncreaseCost;
    public int garbageCapacityValue;
    public int fastGatherValue;
    public int chanceGatherValue;
    public int recyclingIncreaseValue;
}

[System.Serializable]
public class garbageAchievementsItem{

    public int unlockedXPreciousStuffLevel;
    public int unlockedXGainMoneyRecyclingLevel;
    public int unlockedXCollectGarbageLevel;
    public int unlockedCrashXGarbageBoxLevel;
    public int unlockedXTimesEmptyGarbageLevel;
    public int unlockedXGainMoneyFromPreciousLevel;
    public int unlockedDoMaxLevelCarUpgradeLevel;
    public int unlockedOneTimeXGarbageLevel;
    public int unlockedXTimesSevenHillGarbageEmptyLevel;
    public int xPreciousStuffValue;
    public int xGainMoneyRecyclingValue;
    public int xCollectGarbageValue;
    public int crashXGarbageBoxValue;
    public int xTimesEmptyGarbageValue;
    public int xGainMoneyFromPreciousValue;
    public int doMaxLevelCarUpgradeValue;
    public int oneTimeXGarbageValue;
    public bool mostPreciousValue;
    public int mostPreciousReward;
    public bool mostPreciousReceived;
    public int xTimesSevenHillGarbageEmptyValue;

    public GarbageAchievementInfo[] garbageAchievementsUpgradeLevel;
    
}

[System.Serializable]
public class GarbageAchievementInfo{

    public int xPreciousStuffLevelValue;
    public int xGainMoneyRecyclingLevelValue;
    public int xCollectGarbageLevelValue;
    public int crashXGarbageBoxLevelValue;
    public int xTimesEmptyGarbageLevelValue;
    public int xGainMoneyFromPreciousLevelValue;
    public int doMaxLevelCarUpgradeLevelValue;
    public int oneTimeXGarbageLevelValue;
    public int xTimesSevenHillGarbageEmptyLevelValue;
    public int xPreciousStuffReward;
    public int xGainMoneyRecyclingReward;
    public int xCollectGarbageReward;
    public int crashXGarbageBoxReward;
    public int xTimesEmptyGarbageReward;
    public int xGainMoneyFromPreciousReward;
    public int doMaxLevelCarUpgradeReward;
    public int oneTimeXGarbageReward;
    public int xTimesSevenHillGarbageEmptyReward;
    
    

}
}