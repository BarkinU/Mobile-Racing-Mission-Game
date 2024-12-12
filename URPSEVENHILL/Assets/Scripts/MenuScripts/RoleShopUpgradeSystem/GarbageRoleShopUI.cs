using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CarShopSystem;

namespace RoleShopSystem {
    public class GarbageRoleShopUI : MonoBehaviour {
        public GarbageShopData garbageShopData;
        public CarShopUI carShopUI;
        BusShopData busShopData;
        public TextMeshProUGUI capacityValue, capacityNextValue, fastGatherValue, fastGatherNextValue, chanceGatherValue, chanceGatherNextValue, recyclingValue, recyclingNextValue, capacityCost, fastGatherCost, chanceGatherCost, recyclingCost;
        public TextMeshProUGUI totalXpText;
        public Button capacityUpgradeButton, fastGatherUpgradeButton, chanceGatherUpgradeButton, recyclingUpgradeButton;
        public Image capacitySlider,fastGatherSlider,ChanceGatherSlider,recyclingSlider;
        public int whichSkill = 0;
        public int currentIndex = 0;
        public int currentRole = 0;
        public int selectedINdex = 0;
        public int selectedRole = 0;

        private void Start () {

            carShopUI.gameData = ReadWriteAllRoles.ReadGameProp (carShopUI.gameData);
             busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);

            selectedRole = garbageShopData.selectedRole;
            selectedINdex = garbageShopData.selectedIndex;
            currentIndex = selectedINdex;
            totalXpText.text = " " + carShopUI.gameData.totalXp;

            garbageCapacityInfoBtnMethod();
            fastGatherInfoBtnMethod();
            chanceGatherInfoBtnMethod();
            recyclingIncreaseInfoBtnMethod();

        }

        public void CapacityUpgradeButtonFunc () {

            int nextGarbageCapacityLevelIndex = garbageShopData.garbageRoleItems.unlockedGarbageCapacityLevel + 1;
            if (carShopUI.gameData.totalXp >= garbageShopData.garbageRoleItems.garbageSkillUpgradeLevel[nextGarbageCapacityLevelIndex].unlockGarbageCapacityCost) {
                carShopUI.gameData.totalXp -= garbageShopData.garbageRoleItems.garbageSkillUpgradeLevel[nextGarbageCapacityLevelIndex].unlockGarbageCapacityCost;
                totalXpText.text = " " + carShopUI.gameData.totalXp;

                ReadWriteAllRoles.WriteGameProp (carShopUI.gameData);

                garbageShopData.garbageRoleItems.unlockedGarbageCapacityLevel++;

                if (busShopData.generalAchievementItem.unlockedUpgradeXRoleLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.upgradeXRoleValue++;
                    ReadWriteAllRoles.WriteBusProp (busShopData);
                }

                garbageCapacityInfoBtnMethod();
            }


        }

        public void FastGatherUpgradeButtonFunc () {
            int nextFastGatherLevelIndex = garbageShopData.garbageRoleItems.unlockedFastGatherLevel + 1;
            if (carShopUI.gameData.totalXp >= garbageShopData.garbageRoleItems.garbageSkillUpgradeLevel[nextFastGatherLevelIndex].unlockFastGatherCost) {
                carShopUI.gameData.totalXp -= garbageShopData.garbageRoleItems.garbageSkillUpgradeLevel[nextFastGatherLevelIndex].unlockFastGatherCost;
                totalXpText.text = " " + carShopUI.gameData.totalXp;

                ReadWriteAllRoles.WriteGameProp (carShopUI.gameData);

                garbageShopData.garbageRoleItems.unlockedFastGatherLevel++;

                if (busShopData.generalAchievementItem.unlockedUpgradeXRoleLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.upgradeXRoleValue++;
                    ReadWriteAllRoles.WriteBusProp (busShopData);
                }

                fastGatherInfoBtnMethod();
            }

        }
        public void ChanceGatherUpgradeButtonFunc () {
            int nextChanceGatherLevelIndex = garbageShopData.garbageRoleItems.unlockedChanceGatherLevel + 1;

            if (carShopUI.gameData.totalXp >= garbageShopData.garbageRoleItems.garbageSkillUpgradeLevel[nextChanceGatherLevelIndex].unlockChanceGatherCost) {
                carShopUI.gameData.totalXp -= garbageShopData.garbageRoleItems.garbageSkillUpgradeLevel[nextChanceGatherLevelIndex].unlockChanceGatherCost;
                totalXpText.text = " " + carShopUI.gameData.totalXp;

                ReadWriteAllRoles.WriteGameProp (carShopUI.gameData);

                garbageShopData.garbageRoleItems.unlockedChanceGatherLevel++;

                if (busShopData.generalAchievementItem.unlockedUpgradeXRoleLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.upgradeXRoleValue++;
                    ReadWriteAllRoles.WriteBusProp (busShopData);
                }

                chanceGatherInfoBtnMethod();
            }


        }
        public void RecyclingUpgradeButtonFunc () {
            int nextRecyclingIncreaseLevelIndex = garbageShopData.garbageRoleItems.unlockedRecyclingIncreaseLevel + 1;

            if (carShopUI.gameData.totalXp >= garbageShopData.garbageRoleItems.garbageSkillUpgradeLevel[nextRecyclingIncreaseLevelIndex].unlockRecyclingIncreaseCost) {
                carShopUI.gameData.totalXp -= garbageShopData.garbageRoleItems.garbageSkillUpgradeLevel[nextRecyclingIncreaseLevelIndex].unlockRecyclingIncreaseCost;
                totalXpText.text = " " + carShopUI.gameData.totalXp;

                ReadWriteAllRoles.WriteGameProp (carShopUI.gameData);

                garbageShopData.garbageRoleItems.unlockedRecyclingIncreaseLevel++;

                if (busShopData.generalAchievementItem.unlockedUpgradeXRoleLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.upgradeXRoleValue++;
                     ReadWriteAllRoles.WriteBusProp (busShopData);
                }

                recyclingIncreaseInfoBtnMethod();
            }

            

        }
        private void garbageCapacityInfoBtnMethod () {
            int currentGarbageCapacityLevel = garbageShopData.garbageRoleItems.unlockedGarbageCapacityLevel;

            if (garbageShopData.garbageRoleItems.unlockedGarbageCapacityLevel < garbageShopData.garbageRoleItems.garbageSkillUpgradeLevel.Length - 1) {
                capacityUpgradeButton.interactable = true;
                capacityNextValue.text = "" + garbageShopData.garbageRoleItems.garbageSkillUpgradeLevel[currentGarbageCapacityLevel+1].garbageCapacityValue;
                capacityCost.text = "Maliyet  " +
                    garbageShopData.garbageRoleItems.garbageSkillUpgradeLevel[currentGarbageCapacityLevel+1].unlockGarbageCapacityCost;
            } else {
                capacityUpgradeButton.interactable = false;
                capacityCost.text = "Max Level ";
                capacityNextValue.text="Max";
            }

            capacitySlider.fillAmount = (float)garbageShopData.garbageRoleItems.garbageSkillUpgradeLevel[currentGarbageCapacityLevel].garbageCapacityValue/garbageShopData.garbageRoleItems.garbageCapacityMaxValue;
            capacityValue.text = "" + garbageShopData.garbageRoleItems.garbageSkillUpgradeLevel[currentGarbageCapacityLevel].garbageCapacityValue;

        }

        private void fastGatherInfoBtnMethod () {
            int currentFastGatherLevel = garbageShopData.garbageRoleItems.unlockedFastGatherLevel;

            if (garbageShopData.garbageRoleItems.unlockedFastGatherLevel < garbageShopData.garbageRoleItems.garbageSkillUpgradeLevel.Length - 1) {
                fastGatherUpgradeButton.interactable = true;
                fastGatherCost.text = "Maliyet  " +
                    garbageShopData.garbageRoleItems.garbageSkillUpgradeLevel[currentFastGatherLevel+1].unlockFastGatherCost;
                fastGatherNextValue.text = "" + garbageShopData.garbageRoleItems.garbageSkillUpgradeLevel[currentFastGatherLevel+1].fastGatherValue;
            } else {
                fastGatherUpgradeButton.interactable = false;
                fastGatherCost.text = "Max Level ";
                fastGatherNextValue.text = "Max";
            }

            fastGatherSlider.fillAmount = (float)garbageShopData.garbageRoleItems.garbageSkillUpgradeLevel[currentFastGatherLevel].fastGatherValue/garbageShopData.garbageRoleItems.fastGatherMaxValue;
            fastGatherValue.text = "" + garbageShopData.garbageRoleItems.garbageSkillUpgradeLevel[currentFastGatherLevel].fastGatherValue;

        }

        private void chanceGatherInfoBtnMethod () {
            int currentChanceGatherLevel = garbageShopData.garbageRoleItems.unlockedChanceGatherLevel;

            if (garbageShopData.garbageRoleItems.unlockedChanceGatherLevel < garbageShopData.garbageRoleItems.garbageSkillUpgradeLevel.Length - 1) {
                chanceGatherUpgradeButton.interactable = true;
                chanceGatherCost.text = "Maliyet  " +
                    garbageShopData.garbageRoleItems.garbageSkillUpgradeLevel[currentChanceGatherLevel+1].unlockChanceGatherCost;
                chanceGatherNextValue.text = "" + garbageShopData.garbageRoleItems.garbageSkillUpgradeLevel[currentChanceGatherLevel+1].chanceGatherValue;
            } else {
                chanceGatherUpgradeButton.interactable = false;
                chanceGatherCost.text = "Max Level";
                chanceGatherNextValue.text="Max";
            }

            ChanceGatherSlider.fillAmount = (float)garbageShopData.garbageRoleItems.garbageSkillUpgradeLevel[currentChanceGatherLevel].chanceGatherValue/garbageShopData.garbageRoleItems.chanceGatherMaxValue;
            chanceGatherValue.text = "" + garbageShopData.garbageRoleItems.garbageSkillUpgradeLevel[currentChanceGatherLevel].chanceGatherValue;

        }

        private void recyclingIncreaseInfoBtnMethod () {
            int currentRecyclingIncreaseLevel = garbageShopData.garbageRoleItems.unlockedRecyclingIncreaseLevel;

            if (garbageShopData.garbageRoleItems.unlockedRecyclingIncreaseLevel < garbageShopData.garbageRoleItems.garbageSkillUpgradeLevel.Length - 1) {
                recyclingUpgradeButton.interactable = true;
                recyclingCost.text = "Maliyet  " +
                    garbageShopData.garbageRoleItems.garbageSkillUpgradeLevel[currentRecyclingIncreaseLevel+1].unlockRecyclingIncreaseCost;
                recyclingNextValue.text = "" + garbageShopData.garbageRoleItems.garbageSkillUpgradeLevel[currentRecyclingIncreaseLevel+1].recyclingIncreaseValue;
            } else {
                recyclingUpgradeButton.interactable = false;
                recyclingCost.text = "Max Level ";
                recyclingNextValue.text = "Max";
            }

            recyclingSlider.fillAmount = (float)garbageShopData.garbageRoleItems.garbageSkillUpgradeLevel[currentRecyclingIncreaseLevel].recyclingIncreaseValue / garbageShopData.garbageRoleItems.recyclingIncreaseMaxValue;
            recyclingValue.text = "" + garbageShopData.garbageRoleItems.garbageSkillUpgradeLevel[currentRecyclingIncreaseLevel].recyclingIncreaseValue;
        }

    }

}