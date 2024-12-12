using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarShopSystem 
{

    [System.Serializable]
    public class CarShopData 
    {
        public int selectedIndex;
        public int selectedRole=0;
        public RoleItem[] roleItems;
    }

    [System.Serializable]
    public class RoleItem {

        public string roleName;
        public ShopItem[] shopItems;

    }

    [System.Serializable]
        public class ShopItem {

        public string itemName;
        public bool isUnlocked;
        public int unlockCost;
        public int unlockedSpeedLevel;
        public int unlockedAccelerationLevel;
        public int unlockedHandlingLevel;
        public int unlockedBrakingLevel;
        public int speedMaxValue;
        public int accelerationMaxValue;
        public int handlingMaxValue;
        public int brakingMaxValue;
        
        


        public int lastSelectedFrontStickerID;
        public int lastSelectedSideStickerID;
        public int lastSelectedSpoilerID;
        public int lastSelectedSkirtID;
        public int lastSelectedRimID;

        public bool haveModify;
        public bool haveFrontSticker;
        public bool haveSideSticker;
        public bool haveSpoiler;
        public bool haveSkirts;
        public bool haveRim;
        
        public CarInfo[] carLevel;
        public ModifyInfo[] modifyInfo;

    }

    [System.Serializable]
    public class CarInfo {
        public int unlockSpeedCost;
        public int unlockAccCost;
        public int unlockHandlingCost;
        public int unlockBrakingCost;
        public int speedValue;
        public int accelerationValue;
        public int handlingValue;
        public int brakingValue;
    }


    [System.Serializable]
    public class ModifyInfo {

        public int SpoilerCost;
        public int FrontStickerCost;
        public int SideStickerCost;
        public int SkirtCost;
        public int RimCost;

        public bool unlockedFrontSticker;
        public bool unlockedSideSticker;
        public bool unlockedSpoiler;
        public bool unlockedSkirt;
        public bool unlockedRim;
    }
}