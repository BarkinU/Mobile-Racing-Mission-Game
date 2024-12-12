using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoleShopSystem{


   [System.Serializable]
   public class PoliceShopData
{
public int selectedIndex;
public int selectedRole;
public policeRoleItem policeRoleItems;
public policeAchievementsItem policeAchievementItem;

}

[System.Serializable]
public class policeRoleItem{

    public int unlockedCatchInCrimeSceneChanceLevel;
    public int unlockedDecreaseThiefSpeedLevel;
    public int unlockedCatchBonusLevel;
    public int unlockedDecreaseCatchTimeLevel;
    public int catchInCrimeSceneChanceMaxValue;
    public int decreaseThiefSpeedMaxValue;
    public int catchBonusMaxValue;
    public int decreaseCatchTimeMaxValue;
    public int chaseTenur;
    public int vanguardTenur;

    public PoliceRoleUpgradeInfo[] policeSkillUpgradeLevel;
    
}


[System.Serializable]
public class PoliceRoleUpgradeInfo{
    public int unlockCatchInCrimeSceneChanceCost;
    public int unlockedDecreaseThiefSpeedCost;
    public int unlockCatchBonusCost;
    public int unlockedDecreaseCatchTimeCost;
    public int catchInCrimeSceneChanceValue;
    public int decreaseThiefSpeedValue;
    public int catchBonusValue;
    public int decreaseCatchTimeValue;
}

[System.Serializable]
public class policeAchievementsItem{

    public int unlockedXCatchThiefCrimeSceneLevel;
    public int unlockedGainXBonusMoneyFromBlockingRobberyLevel;
    public int unlockedXCatchThiefLevel;
    public int unlockedXCatchWithScoutLevel;
    public int unlockedDoMaxLevelSlowThiefLevel;
    public int unlockedXCrashThiefLevel;
    public int unlockedUseXVanguardLevel;
    public int unlockedNoAccidentPoliceLevel;
    public int unlockedSolveAllCaseInSevenhillLevel;
    public int xCatchThiefCrimeSceneValue;
    public int gainXBonusMoneyFromBlockingRobberyValue;
    public int xCatchThiefValue;
    public int xCatchWithScoutValue;
    public int doMaxLevelSlowThiefValue;
    public int xCrashThiefValue;
    public int useXVanguardValue;
    public int noAccidentPoliceValue;
    public bool findRobberyMoneyValue;
    public int findRobberyMoneyReward;
    public bool findRobberyReceived;
    public int solveAllCaseInSevenhillValue;

    public PoliceAchievementInfo[] policeAchievementsUpgradeLevel;
    
}


[System.Serializable]
public class PoliceAchievementInfo{
    public int xCatchThiefCrimeSceneLevelValue;
    public int gainXBonusMoneyFromBlockingRobberyLevelValue;
    public int xCatchThiefLevelValue;
    public int xCatchWithScoutLevelValue;
    public int doMaxLevelSlowThiefLevelValue;
    public int xCrashThiefLevelValue;
    public int useXVanguardLevelValue;
    public int noAccidentPoliceLevelValue;
    public int solveAllCaseInSevenhillLevelValue;
    public int xCatchThiefCrimeSceneReward;
    public int gainXBonusMoneyFromBlockingRobberyReward;
    public int xCatchThiefReward;
    public int xCatchWithScoutReward;
    public int doMaxLevelSlowThiefReward;
    public int xCrashThiefReward;
    public int useXVanguardReward;
    public int noAccidentPoliceReward;
    public int solveAllCaseInSevenhillReward;
   
}

}