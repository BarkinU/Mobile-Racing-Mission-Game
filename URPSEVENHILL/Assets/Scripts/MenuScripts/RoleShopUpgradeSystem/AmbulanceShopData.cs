using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoleShopSystem{


   [System.Serializable]
   public class AmbulanceShopData
{

public int selectedIndex;
public int selectedRole;
public ambulanceRoleItem ambulanceRoleItems;
public ambulanceAchievementsItem ambulanceAchievementItem;


}
[System.Serializable]
public class ambulanceRoleItem{
    public int unlockedProlongLifeLevel;
    public int unlockedElectroShockLevel;
    public int unlockedExtraWoundedLevel;
    public int unlockedHealingLevel;
    public int prolongLifeMaxValue;
    public int electroShockMaxValue;
    public int extraWoundedMaxValue;
    public int healingMaxValue;
    public int healingTenur;
    public int electroShockTenur;
    public int adrenalinTenur;

    public AmbulanceRoleUpgradeInfo[] ambulanceSkillUpgradeLevel;
}

[System.Serializable]
public class AmbulanceRoleUpgradeInfo{
    public int unlockProlongLifeCost;
    public int unlockElectroShockCost;
    public int unlockExtraWoundedCost;
    public int unlockHealingCost;
    public int prolongLifeValue;
    public int electroShockValue;
    public int extraWoundedValue;
    public int healingValue;
}

[System.Serializable]
public class ambulanceAchievementsItem{
    public int unlockedXGainMoneyFromExtraPatientLevel;
    public int unlockedXMultiplePatientWithoutDyingLevel;
    public int unlockedXUseElectroShockLevel;
    public int unlockedXPatientWithoutDyingLevel;
    public int unlockedXUseAdrenalinLevel;
    public int unlockedXPatientWithoutDyingNoAccidentLevel;
    public int unlockedXPatientFullLifeLevel;
    public int unlockedXPatientFullAdrenalinLevel;
    public int unlockedXUseHealLevel;
    public int xGainMoneyFromExtraPatientValue;
    public int xMultiplePatientWithoutDyingValue;
    public int xUseElectroShockValue;
    public int xPatientWithoutDyingValue;
    public int xUseAdrenalinValue;
    public int xPatientWithoutDyingNoAccidentValue;
    public int xPatientFullLifeValue;
    public int xPatientFullAdrenalinValue;
    public int xUseHealValue;
    public bool findTheImmortalPatientValue;
    public int findTheImmortalPatientReward;
    public bool findImmortalReceived;

    public AmbulanceAchievementsInfo[] ambulanceAchievementsUpgradeLevel;
}

[System.Serializable]
public class AmbulanceAchievementsInfo{
    public int xGainMoneyFromExtraPatientLevelValue;
    public int xMultiplePatientWithoutDyingLevelValue;
    public int xUseElectroShockLevelValue;
    public int xPatientWithoutDyingLevelValue;
    public int xUseAdrenalinLevelValue;
    public int xPatientWithoutDyingNoAccidentLevelValue;
    public int xPatientFullLifeLevelValue;
    public int xUseHealLevelValue;
    public int xPatientFullAdrenalinLevelValue;
    public int xGainMoneyFromExtraPatientReward;
    public int xMultiplePatientWithoutDyingReward;
    public int xUseElectroShockReward;
    public int xPatientWithoutDyingReward;
    public int xUseAdrenalinReward;
    public int xPatientWithoutDyingNoAccidentReward;
    public int xPatientFullLifeReward;
    public int xUseHealReward;
    public int xPatientFullAdrenalinReward;
    
}



}