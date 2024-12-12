using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using CarShopSystem;

namespace RoleShopSystem {
    public class FireRoleShopUI : MonoBehaviour {
        public FireShopData fireShopData;
        BusShopData busShopData;
        public CarShopUI carShopUI;
        public TextMeshProUGUI fastValueText, fastNextValueText, waterValueText, waterNextValueText, rescueValueText, rescueNextValueText, AddValueText, AddNextValueText, fastCost, waterCost, rescueCost, addCost;
        public TextMeshProUGUI totalXpText;
        public Button fastUpgrade, waterUpgrade, rescueUpgrade, addUpgrade;
        public Image fastSlider, waterSlider, rescueSlider, addSlider;
        public int currentIndex = 0;
        public int selectedINdex = 0;
        public int selectedRole = 0;

        private void Start () {

            carShopUI.gameData = ReadWriteAllRoles.ReadGameProp (carShopUI.gameData);
            busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
            fireShopData = ReadWriteAllRoles.ReadFireProp (fireShopData);

            selectedRole = fireShopData.selectedRole;
            selectedINdex = fireShopData.selectedIndex;
            currentIndex = selectedINdex;
            totalXpText.text = " " + carShopUI.gameData.totalXp;

            fastPutOutInfoBtnMethod ();
            waterTankCapacityInfoBtnMethod ();
            rescueLifeBonusInfoBtnMethod ();
            additionalHoseInfoBtnMethod ();

        }

        public void FastUpgradeButtonFunc () {

            int nextFastPutOutLevelIndex = fireShopData.fireRoleItems.unlockedFastPutOutLevel + 1;
            if (carShopUI.gameData.totalXp >= fireShopData.fireRoleItems.fireSkillUpgradeLevel[nextFastPutOutLevelIndex].unlockFastPutOutCost) {
                carShopUI.gameData.totalXp -= fireShopData.fireRoleItems.fireSkillUpgradeLevel[nextFastPutOutLevelIndex].unlockFastPutOutCost;
                totalXpText.text = " " + carShopUI.gameData.totalXp;

                ReadWriteAllRoles.WriteGameProp (carShopUI.gameData);

                fireShopData.fireRoleItems.unlockedFastPutOutLevel++;

                if (busShopData.generalAchievementItem.unlockedUpgradeXRoleLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.upgradeXRoleValue++;
                    ReadWriteAllRoles.WriteBusProp (busShopData);
                }
                fastPutOutInfoBtnMethod ();
            }

        }
        public void WaterUpgradeButtonFunc () {

            int nextWaterTankCapacityLevelIndex = fireShopData.fireRoleItems.unlockedWaterTankCapacityLevel + 1;
            if (carShopUI.gameData.totalXp >= fireShopData.fireRoleItems.fireSkillUpgradeLevel[nextWaterTankCapacityLevelIndex].unlockWaterTankCapacityCost) {
                carShopUI.gameData.totalXp -= fireShopData.fireRoleItems.fireSkillUpgradeLevel[nextWaterTankCapacityLevelIndex].unlockWaterTankCapacityCost;
                totalXpText.text = " " + carShopUI.gameData.totalXp;

                ReadWriteAllRoles.WriteGameProp (carShopUI.gameData);

                fireShopData.fireRoleItems.unlockedWaterTankCapacityLevel++;

                if (busShopData.generalAchievementItem.unlockedUpgradeXRoleLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.upgradeXRoleValue++;
                    ReadWriteAllRoles.WriteBusProp (busShopData);
                }

                waterTankCapacityInfoBtnMethod ();
            }

        }
        public void RescueUpgradeButtonFunc () {

            int nextRescueLifeBonusLevelIndex = fireShopData.fireRoleItems.unlockedRescueLifeBonusLevel + 1;

            if (carShopUI.gameData.totalXp >= fireShopData.fireRoleItems.fireSkillUpgradeLevel[nextRescueLifeBonusLevelIndex].unlockRescueLifeBonusCost) {
                carShopUI.gameData.totalXp -= fireShopData.fireRoleItems.fireSkillUpgradeLevel[nextRescueLifeBonusLevelIndex].unlockRescueLifeBonusCost;
                totalXpText.text = " " + carShopUI.gameData.totalXp;

                ReadWriteAllRoles.WriteGameProp (carShopUI.gameData);

                fireShopData.fireRoleItems.unlockedRescueLifeBonusLevel++;

                if (busShopData.generalAchievementItem.unlockedUpgradeXRoleLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.upgradeXRoleValue++;
                    ReadWriteAllRoles.WriteBusProp (busShopData);
                }

                rescueLifeBonusInfoBtnMethod ();
            }

        }
        public void AddUpgradeButtonFunc () {

            int nextAdditionalHoseLevelIndex = fireShopData.fireRoleItems.unlockedAdditionalHoseLevel + 1;

            if (carShopUI.gameData.totalXp >= fireShopData.fireRoleItems.fireSkillUpgradeLevel[nextAdditionalHoseLevelIndex].unlockAdditionalHoseCost) {
                carShopUI.gameData.totalXp -= fireShopData.fireRoleItems.fireSkillUpgradeLevel[nextAdditionalHoseLevelIndex].unlockAdditionalHoseCost;
                totalXpText.text = " " + carShopUI.gameData.totalXp;

                ReadWriteAllRoles.WriteGameProp (carShopUI.gameData);

                fireShopData.fireRoleItems.unlockedAdditionalHoseLevel++;

                if (busShopData.generalAchievementItem.unlockedUpgradeXRoleLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.upgradeXRoleValue++;
                    ReadWriteAllRoles.WriteBusProp (busShopData);
                }

                additionalHoseInfoBtnMethod ();

            }

        }
        private void fastPutOutInfoBtnMethod () {
            int currentFastPutOutLevel = fireShopData.fireRoleItems.unlockedFastPutOutLevel;

            if (fireShopData.fireRoleItems.unlockedFastPutOutLevel < fireShopData.fireRoleItems.fireSkillUpgradeLevel.Length - 1) {
                fastUpgrade.interactable = true;
                fastNextValueText.text = "" + fireShopData.fireRoleItems.fireSkillUpgradeLevel[currentFastPutOutLevel + 1].fastPutOutValue;
                fastCost.text = "Maliyet  " +
                    fireShopData.fireRoleItems.fireSkillUpgradeLevel[currentFastPutOutLevel + 1].unlockFastPutOutCost;
            } else {
                fastUpgrade.interactable = false;
                fastNextValueText.text = "Max";
                fastCost.text = "Max Level ";

                if (fireShopData.fireAchievementItem.unlockedUpgradeMaxRoleLevel <= fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel.Length - 1) {
                    fireShopData.fireAchievementItem.upgradeMaxRoleValue++;
                    ReadWriteAllRoles.WriteFireProp (fireShopData);
                }

            }

            fastSlider.fillAmount = (float) fireShopData.fireRoleItems.fireSkillUpgradeLevel[currentFastPutOutLevel].fastPutOutValue / fireShopData.fireRoleItems.fastPutOutMaxValue;
            fastValueText.text = "" + fireShopData.fireRoleItems.fireSkillUpgradeLevel[currentFastPutOutLevel].fastPutOutValue;

        }

        private void waterTankCapacityInfoBtnMethod () {
            int currentWaterTankCapacityLevel = fireShopData.fireRoleItems.unlockedWaterTankCapacityLevel;

            if (fireShopData.fireRoleItems.unlockedWaterTankCapacityLevel < fireShopData.fireRoleItems.fireSkillUpgradeLevel.Length - 1) {
                waterUpgrade.interactable = true;
                waterNextValueText.text = "" + fireShopData.fireRoleItems.fireSkillUpgradeLevel[currentWaterTankCapacityLevel + 1].waterTankCapacityValue;
                waterCost.text = "Maliyet  " +
                    fireShopData.fireRoleItems.fireSkillUpgradeLevel[currentWaterTankCapacityLevel + 1].unlockWaterTankCapacityCost;
            } else {
                waterUpgrade.interactable = false;
                waterNextValueText.text = "Max";
                waterCost.text = "Max Level ";

                fireShopData.fireAchievementItem.upgradeMaxRoleValue++;
                ReadWriteAllRoles.WriteFireProp (fireShopData);
            }

            waterSlider.fillAmount = (float) fireShopData.fireRoleItems.fireSkillUpgradeLevel[currentWaterTankCapacityLevel].waterTankCapacityValue / fireShopData.fireRoleItems.waterTankCapacityMaxValue;
            waterValueText.text = "" + fireShopData.fireRoleItems.fireSkillUpgradeLevel[currentWaterTankCapacityLevel].waterTankCapacityValue;

        }

        private void rescueLifeBonusInfoBtnMethod () {

            int currentRescueLifeBonusLevel = fireShopData.fireRoleItems.unlockedRescueLifeBonusLevel;

            if (fireShopData.fireRoleItems.unlockedRescueLifeBonusLevel < fireShopData.fireRoleItems.fireSkillUpgradeLevel.Length - 1) {

                rescueUpgrade.interactable = true;
                rescueNextValueText.text = "" + fireShopData.fireRoleItems.fireSkillUpgradeLevel[currentRescueLifeBonusLevel + 1].rescueLifeBonusValue;
                rescueCost.text = "Maliyet  " +
                    fireShopData.fireRoleItems.fireSkillUpgradeLevel[currentRescueLifeBonusLevel + 1].unlockRescueLifeBonusCost;
            } else {
                rescueUpgrade.interactable = false;
                rescueNextValueText.text = "Max";
                rescueCost.text = "Max Level ";

                fireShopData.fireAchievementItem.upgradeMaxRoleValue++;
                ReadWriteAllRoles.WriteFireProp (fireShopData);
            }

            rescueSlider.fillAmount = (float) fireShopData.fireRoleItems.fireSkillUpgradeLevel[currentRescueLifeBonusLevel].rescueLifeBonusValue / fireShopData.fireRoleItems.rescueLifeBonusMaxValue;
            rescueValueText.text = "" + fireShopData.fireRoleItems.fireSkillUpgradeLevel[currentRescueLifeBonusLevel].rescueLifeBonusValue;

        }

        private void additionalHoseInfoBtnMethod () {
            int currentAdditionalHoseLevel = fireShopData.fireRoleItems.unlockedAdditionalHoseLevel;
            if (fireShopData.fireRoleItems.unlockedAdditionalHoseLevel < fireShopData.fireRoleItems.fireSkillUpgradeLevel.Length - 1) {
                addUpgrade.interactable = true;
                AddNextValueText.text = "" + fireShopData.fireRoleItems.fireSkillUpgradeLevel[currentAdditionalHoseLevel + 1].additionalHoseValue;
                addCost.text = "Maliyet  " +
                    fireShopData.fireRoleItems.fireSkillUpgradeLevel[currentAdditionalHoseLevel + 1].unlockAdditionalHoseCost;
            } else {
                addUpgrade.interactable = false;
                AddNextValueText.text = "Max";
                addCost.text = "Max Level ";

                fireShopData.fireAchievementItem.upgradeMaxRoleValue++;
                ReadWriteAllRoles.WriteFireProp (fireShopData);
            }

            addSlider.fillAmount = (float) fireShopData.fireRoleItems.fireSkillUpgradeLevel[currentAdditionalHoseLevel].additionalHoseValue / fireShopData.fireRoleItems.additionalHoseMaxValue;
            AddValueText.text = "" + fireShopData.fireRoleItems.fireSkillUpgradeLevel[currentAdditionalHoseLevel].additionalHoseValue;

        }

    }

}