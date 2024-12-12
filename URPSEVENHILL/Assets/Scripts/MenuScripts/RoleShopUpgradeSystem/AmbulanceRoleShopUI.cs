using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using CarShopSystem;

namespace RoleShopSystem {
    public class AmbulanceRoleShopUI : MonoBehaviour {
        public AmbulanceShopData ambulanceShopData;
        BusShopData busShopData;
        public CarShopUI carShopUI;
        public TextMeshProUGUI prolongValueText, prolongNextValueText, electroValueText, electroNextValueText, extraValueText, extraNextValueText, healingValueText, healingNextValueText, prolongCostText, electroCostText, extraCostText, healingCostText;
        public TextMeshProUGUI totalXpText;
        public Button prolongUpgradeButton, electroUpgradeButton, extraUpgradeButton, healingUpgradeButton;
        public Image prolongSlider, electroSlider, extraSlider, healingSlider;
        public int currentIndex = 0;
        public int selectedINdex = 0;
        public int selectedRole = 0;

        private void Start () {

            carShopUI.gameData = ReadWriteAllRoles.ReadGameProp (carShopUI.gameData);
            busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);

            selectedRole = ambulanceShopData.selectedRole;
            selectedINdex = ambulanceShopData.selectedIndex;
            currentIndex = selectedINdex;
            totalXpText.text = " " + carShopUI.gameData.totalXp;

            prolongLifeInfoBtnMethod ();
            ElectroShockInfoBtnMethod ();
            extraWoundedInfoBtnMethod ();
            healingInfoBtnMethod ();

        }
        public void ProlongUpgradeButtonFunc () {

            int nextProlongLifeLevelIndex = ambulanceShopData.ambulanceRoleItems.unlockedProlongLifeLevel + 1;
            if (carShopUI.gameData.totalXp >= ambulanceShopData.ambulanceRoleItems.ambulanceSkillUpgradeLevel[nextProlongLifeLevelIndex].unlockProlongLifeCost) {
                carShopUI.gameData.totalXp -= ambulanceShopData.ambulanceRoleItems.ambulanceSkillUpgradeLevel[nextProlongLifeLevelIndex].unlockProlongLifeCost;
                totalXpText.text = " " + carShopUI.gameData.totalXp;

                ReadWriteAllRoles.WriteGameProp (carShopUI.gameData);

                ambulanceShopData.ambulanceRoleItems.unlockedProlongLifeLevel++;

                if (busShopData.generalAchievementItem.unlockedUpgradeXRoleLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.upgradeXRoleValue++;
                    ReadWriteAllRoles.WriteBusProp (busShopData);
                }

                prolongLifeInfoBtnMethod();
            }


        }

        public void ElectroShockUpgradeButtonFunc () {
            int nextElectroShockLevelIndex = ambulanceShopData.ambulanceRoleItems.unlockedElectroShockLevel + 1;
            if (carShopUI.gameData.totalXp >= ambulanceShopData.ambulanceRoleItems.ambulanceSkillUpgradeLevel[nextElectroShockLevelIndex].unlockElectroShockCost) {
                carShopUI.gameData.totalXp -= ambulanceShopData.ambulanceRoleItems.ambulanceSkillUpgradeLevel[nextElectroShockLevelIndex].unlockElectroShockCost;
                totalXpText.text = " " + carShopUI.gameData.totalXp;

                ReadWriteAllRoles.WriteGameProp (carShopUI.gameData);

                ambulanceShopData.ambulanceRoleItems.unlockedElectroShockLevel++;

                if (busShopData.generalAchievementItem.unlockedUpgradeXRoleLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.upgradeXRoleValue++;
                    ReadWriteAllRoles.WriteBusProp (busShopData);
                }

                ElectroShockInfoBtnMethod();
            }

        }

        public void ExtraUpgradeButtonFunc () {

            int nextExtraWoundedLevelIndex = ambulanceShopData.ambulanceRoleItems.unlockedExtraWoundedLevel + 1;

            if (carShopUI.gameData.totalXp >= ambulanceShopData.ambulanceRoleItems.ambulanceSkillUpgradeLevel[nextExtraWoundedLevelIndex].unlockExtraWoundedCost) {
                carShopUI.gameData.totalXp -= ambulanceShopData.ambulanceRoleItems.ambulanceSkillUpgradeLevel[nextExtraWoundedLevelIndex].unlockExtraWoundedCost;
                totalXpText.text = " " + carShopUI.gameData.totalXp;

                ReadWriteAllRoles.WriteGameProp (carShopUI.gameData);

                ambulanceShopData.ambulanceRoleItems.unlockedExtraWoundedLevel++;

                if (busShopData.generalAchievementItem.unlockedUpgradeXRoleLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.upgradeXRoleValue++; 
                    ReadWriteAllRoles.WriteBusProp (busShopData);   
                }

                extraWoundedInfoBtnMethod();
            }

        }
        public void HealingUpgradeButtonFunc () {
            int nextHealingLevelIndex = ambulanceShopData.ambulanceRoleItems.unlockedHealingLevel + 1;

            if (carShopUI.gameData.totalXp >= ambulanceShopData.ambulanceRoleItems.ambulanceSkillUpgradeLevel[nextHealingLevelIndex].unlockHealingCost) {
                carShopUI.gameData.totalXp -= ambulanceShopData.ambulanceRoleItems.ambulanceSkillUpgradeLevel[nextHealingLevelIndex].unlockHealingCost;
                totalXpText.text = " " + carShopUI.gameData.totalXp;

                ReadWriteAllRoles.WriteGameProp (carShopUI.gameData);

                ambulanceShopData.ambulanceRoleItems.unlockedHealingLevel++;

                if (busShopData.generalAchievementItem.unlockedUpgradeXRoleLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.upgradeXRoleValue++;
                    ReadWriteAllRoles.WriteBusProp (busShopData);
                }

                healingInfoBtnMethod();
            }


        }

        private void prolongLifeInfoBtnMethod () {
            int currentProlongLifeLevel = ambulanceShopData.ambulanceRoleItems.unlockedProlongLifeLevel;

            if (ambulanceShopData.ambulanceRoleItems.unlockedProlongLifeLevel < ambulanceShopData.ambulanceRoleItems.ambulanceSkillUpgradeLevel.Length - 1) {
                prolongUpgradeButton.interactable = true;
                prolongNextValueText.text = "" + ambulanceShopData.ambulanceRoleItems.ambulanceSkillUpgradeLevel[currentProlongLifeLevel + 1].prolongLifeValue;
                prolongCostText.text = "Maliyet  " +
                    ambulanceShopData.ambulanceRoleItems.ambulanceSkillUpgradeLevel[currentProlongLifeLevel + 1].unlockProlongLifeCost;
            } else {
                prolongUpgradeButton.interactable = false;
                prolongNextValueText.text = "Max";
                prolongCostText.text = "Max Level ";
            }

            prolongSlider.fillAmount = (float)ambulanceShopData.ambulanceRoleItems.ambulanceSkillUpgradeLevel[currentProlongLifeLevel].prolongLifeValue / ambulanceShopData.ambulanceRoleItems.prolongLifeMaxValue;
            prolongValueText.text = "" + ambulanceShopData.ambulanceRoleItems.ambulanceSkillUpgradeLevel[currentProlongLifeLevel].prolongLifeValue;
            

        }

        private void ElectroShockInfoBtnMethod () {
            int currentElectroShockLevel = ambulanceShopData.ambulanceRoleItems.unlockedElectroShockLevel;

            if (ambulanceShopData.ambulanceRoleItems.unlockedElectroShockLevel < ambulanceShopData.ambulanceRoleItems.ambulanceSkillUpgradeLevel.Length - 1) {
                electroUpgradeButton.interactable = true;
                electroNextValueText.text = "" + ambulanceShopData.ambulanceRoleItems.ambulanceSkillUpgradeLevel[currentElectroShockLevel + 1].electroShockValue;
                electroCostText.text = "Maliyet  " +
                    ambulanceShopData.ambulanceRoleItems.ambulanceSkillUpgradeLevel[currentElectroShockLevel + 1].unlockElectroShockCost;
            } else {
                electroUpgradeButton.interactable = false;
                electroNextValueText.text = "Max";
                electroCostText.text = "Max Level ";
            }

            electroSlider.fillAmount = (float)ambulanceShopData.ambulanceRoleItems.ambulanceSkillUpgradeLevel[currentElectroShockLevel].electroShockValue/ambulanceShopData.ambulanceRoleItems.electroShockMaxValue;
            electroValueText.text = "" + ambulanceShopData.ambulanceRoleItems.ambulanceSkillUpgradeLevel[currentElectroShockLevel].electroShockValue;

        }

        private void extraWoundedInfoBtnMethod () {
            int currentExtraWoundedLevel = ambulanceShopData.ambulanceRoleItems.unlockedExtraWoundedLevel;
            if (ambulanceShopData.ambulanceRoleItems.unlockedExtraWoundedLevel < ambulanceShopData.ambulanceRoleItems.ambulanceSkillUpgradeLevel.Length - 1) {
                extraUpgradeButton.interactable = true;
                extraNextValueText.text = "" + ambulanceShopData.ambulanceRoleItems.ambulanceSkillUpgradeLevel[currentExtraWoundedLevel + 1].extraWoundedValue;
                extraCostText.text = "Maliyet  " +
                    ambulanceShopData.ambulanceRoleItems.ambulanceSkillUpgradeLevel[currentExtraWoundedLevel + 1].unlockExtraWoundedCost;
            } else {
                extraUpgradeButton.interactable = false;
                extraNextValueText.text = "Max";
                extraCostText.text = "Max Level";
            }

            extraSlider.fillAmount = (float)ambulanceShopData.ambulanceRoleItems.ambulanceSkillUpgradeLevel[currentExtraWoundedLevel].extraWoundedValue/ambulanceShopData.ambulanceRoleItems.extraWoundedMaxValue;
            extraValueText.text = "" + ambulanceShopData.ambulanceRoleItems.ambulanceSkillUpgradeLevel[currentExtraWoundedLevel].extraWoundedValue;

        }

        private void healingInfoBtnMethod () {
            int currentHealingLevel = ambulanceShopData.ambulanceRoleItems.unlockedHealingLevel;
            if (ambulanceShopData.ambulanceRoleItems.unlockedHealingLevel < ambulanceShopData.ambulanceRoleItems.ambulanceSkillUpgradeLevel.Length - 1) {
                healingUpgradeButton.interactable = true;
                healingCostText.text = "Maliyet  " +
                    ambulanceShopData.ambulanceRoleItems.ambulanceSkillUpgradeLevel[currentHealingLevel + 1].unlockHealingCost;
                healingNextValueText.text = "" + ambulanceShopData.ambulanceRoleItems.ambulanceSkillUpgradeLevel[currentHealingLevel + 1].healingValue;
            } else {
                healingUpgradeButton.interactable = false;
                healingCostText.text = "Max Level ";
                healingNextValueText.text = "Max";
            }

            healingSlider.fillAmount = (float)ambulanceShopData.ambulanceRoleItems.ambulanceSkillUpgradeLevel[currentHealingLevel].healingValue / ambulanceShopData.ambulanceRoleItems.healingMaxValue;
            healingValueText.text = "" + ambulanceShopData.ambulanceRoleItems.ambulanceSkillUpgradeLevel[currentHealingLevel].healingValue;

        }

    }

}