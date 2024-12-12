using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoleShopSystem{


   [System.Serializable]
   public class TaxiShopData
{
public int selectedIndex;
public int selectedRole;
public taxiRoleItem taxiRoleItems;
public taxiAchievementsItem taxiAchievementItem;

}

[System.Serializable]
public class taxiRoleItem{

    public int unlockedLongDistanceLevel;
    public int unlockedCloseCustomerLevel;
    public int unlockedCustomerSatisfactionLevel;
    public int unlockedGasolineReductionRateLevel;
    public int longDistanceMaxValue;
    public int closeCustomerMaxValue;
    public int customerSatisfactionMaxValue;
    public int gasolineReductionRateMaxValue;
    public int crazyDriveTenur;
    public int cateringTenur;

    public TaxiRoleUpgradeInfo[] taxiSkillUpgradeLevel;
    
}

[System.Serializable]
public class TaxiRoleUpgradeInfo{
    public int unlockLongDistanceCost;
    public int unlockCloseCustomerCost;
    public int unlockCustomerSatisfactionCost;
    public int unlockGasolineReductionRateCost;
    public int longDistanceValue;
    public int closeCustomerValue;
    public int customerSatisfactionValue;
    public int gasolineReductionRateValue;
}

[System.Serializable]
public class taxiAchievementsItem{

    public int unlockedCrashSignBoardLevel;
    public int unlockedFirstCityTravelLevel;
    public int unlockedHundredPercentSatisfactionLevel;
    public int unlockedUse4TimesCrazyOneGameLevel;
    public int unlockedXNoAccidentLevel;
    public int unlockedUseXCateringLevel;
    public int unlockedNoGasolineGiveMoneyLevel;
    public int unlockedGainXMoneyFromSatisfactionLevel;
    public int unlockedGasolineFullingLevel;
    public int crashSignBoardValue;
    public int firstCityTravelValue;
    public int hundredPercentSatisfactionValue;
    public int use4TimesCrazyOneGameValue;
    public int xNoAccidentValue;
    public int useXCateringValue;
    public int noGasolineGiveMoneyValue;
    public int gainXMoneyFromSatisfactionValue;
    public bool findTheFamousCustomerValue;
    public int findTheFamousCustomerReward;
    public bool findFamousReceived;
    public int gasolineFullingValue;


    public TaxiAchievementInfo[] taxiAchievementsUpgradeLevel;
    
}

[System.Serializable]
public class TaxiAchievementInfo{

    public int crashSignBoardBoxLevelValue;
    public int firstCityTravelLevelValue;
    public int hundredPercentSatisfactionLevelValue;
    public int use4TimesCrazyOneGameLevelValue;
    public int xNoAccidentLevelValue;
    public int useXCateringLevelValue;
    public int noGasolineGiveMoneyLevelValue;
    public int gainXMoneyFromSatisfactionLevelValue;
    public int gasolineFullingLevelValue;
    public int crashSignBoardBoxReward;
    public int firstCityTravelReward;
    public int hundredPercentSatisfactionReward;
    public int use4TimesCrazyOneGameReward;
    public int xNoAccidentReward;
    public int useXCateringReward;
    public int noGasolineGiveMoneyReward;
    public int gainXMoneyFromSatisfactionReward;
    public int gasolineFullingReward;
    
    
}


}