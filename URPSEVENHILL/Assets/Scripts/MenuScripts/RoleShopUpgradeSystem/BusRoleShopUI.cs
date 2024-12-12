using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using CarShopSystem;

namespace RoleShopSystem {
    public class BusRoleShopUI : MonoBehaviour {
        public BusShopData busShopData;
        public CarShopUI carShopUI;
        public TextMeshProUGUI capacityValueText, capacityNextValueText, fastPassValueText, fastPassNextValueText, chancePassValueText, chancePassNextValueText, comfortDriveValueText, comfortDriveNextValueText, capacityCostText, fastPassCostText, chancePastCostText, comfortDriveCostText;
        public TextMeshProUGUI totalXpText;
        public Button capacityUpgradeBtn, fastPassUpgradeBtn, chancePassUpgradeBtn, comfortDrivePriceUpgradeBtn;
        public Image capacitySlider, fastPassSlider, chancePassSlider, comfortDrivePriveSlider;
        public int currentIndex = 0;
        public int selectedINdex = 0;
        public int selectedRole = 0;

        private void Start () {

            carShopUI.gameData = ReadWriteAllRoles.ReadGameProp (carShopUI.gameData);
             busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);

            selectedRole = busShopData.selectedRole;
            selectedINdex = busShopData.selectedIndex;
            currentIndex = selectedINdex;
            totalXpText.text = " " + carShopUI.gameData.totalXp;

            capacityInfoBtnMethod ();
            fastPassInfoBtnMethod ();
            chancePassInfoBtnMethod ();
            comfortDrivePriceInfoBtnMethod ();

        }

        public void CapacityUpgradeButtonFunc () {
            int nextCapacityLevelIndex = busShopData.busRoleItems.unlockedCapacityLevel + 1;
            if (carShopUI.gameData.totalXp >= busShopData.busRoleItems.busSkillUpgradeLevel[nextCapacityLevelIndex].unlockCapacityCost) {
                carShopUI.gameData.totalXp -= busShopData.busRoleItems.busSkillUpgradeLevel[nextCapacityLevelIndex].unlockCapacityCost;
                totalXpText.text = " " + carShopUI.gameData.totalXp;

                ReadWriteAllRoles.WriteGameProp (carShopUI.gameData);

                busShopData.busRoleItems.unlockedCapacityLevel++;

                if (busShopData.generalAchievementItem.unlockedUpgradeXRoleLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.upgradeXRoleValue++;
                    ReadWriteAllRoles.WriteBusProp (busShopData);
                }

                capacityInfoBtnMethod ();

            }

        }

        public void FastPassUpgradeButtonFunc () {
            int nextFastLevelIndex = busShopData.busRoleItems.unlockedFastPassLevel + 1;
            if (carShopUI.gameData.totalXp >= busShopData.busRoleItems.busSkillUpgradeLevel[nextFastLevelIndex].unlockFastPassCost) {
                carShopUI.gameData.totalXp -= busShopData.busRoleItems.busSkillUpgradeLevel[nextFastLevelIndex].unlockFastPassCost;
                totalXpText.text = " " + carShopUI.gameData.totalXp;

                ReadWriteAllRoles.WriteGameProp (carShopUI.gameData);

                busShopData.busRoleItems.unlockedFastPassLevel++;

                if (busShopData.generalAchievementItem.unlockedUpgradeXRoleLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.upgradeXRoleValue++;
                    ReadWriteAllRoles.WriteBusProp (busShopData);
                }

                fastPassInfoBtnMethod ();
            }

        }

        public void ChancePassUpgradeButtonFunc () {
            int nextChanceLevelIndex = busShopData.busRoleItems.unlockedChancePassLevel + 1;

            if (carShopUI.gameData.totalXp >= busShopData.busRoleItems.busSkillUpgradeLevel[nextChanceLevelIndex].unlockChancePassCost) {
                carShopUI.gameData.totalXp -= busShopData.busRoleItems.busSkillUpgradeLevel[nextChanceLevelIndex].unlockChancePassCost;
                totalXpText.text = " " + carShopUI.gameData.totalXp;

                ReadWriteAllRoles.WriteGameProp (carShopUI.gameData);

                busShopData.busRoleItems.unlockedChancePassLevel++;

                if (busShopData.generalAchievementItem.unlockedUpgradeXRoleLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.upgradeXRoleValue++;
                    ReadWriteAllRoles.WriteBusProp (busShopData);
                }

                chancePassInfoBtnMethod ();
            }

        }

        public void ComfortDriveUpgradeButtonFunc () {
            int nextComfortLevelIndex = busShopData.busRoleItems.unlockedComfortDrivePriceLevel + 1;

            if (carShopUI.gameData.totalXp >= busShopData.busRoleItems.busSkillUpgradeLevel[nextComfortLevelIndex].unlockComfortDrivePriceCost) {
                carShopUI.gameData.totalXp -= busShopData.busRoleItems.busSkillUpgradeLevel[nextComfortLevelIndex].unlockComfortDrivePriceCost;
                totalXpText.text = " " + carShopUI.gameData.totalXp;

                ReadWriteAllRoles.WriteGameProp (carShopUI.gameData);

                busShopData.busRoleItems.unlockedComfortDrivePriceLevel++;

                if (busShopData.generalAchievementItem.unlockedUpgradeXRoleLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.upgradeXRoleValue++;
                    ReadWriteAllRoles.WriteBusProp (busShopData);
                }

                comfortDrivePriceInfoBtnMethod ();
            }

        }
        private void capacityInfoBtnMethod () {
            int currentcapacityLevel = busShopData.busRoleItems.unlockedCapacityLevel;

            if (busShopData.busRoleItems.unlockedCapacityLevel < busShopData.busRoleItems.busSkillUpgradeLevel.Length - 1) {
                capacityUpgradeBtn.interactable = true;
                capacityNextValueText.text = "" + busShopData.busRoleItems.busSkillUpgradeLevel[currentcapacityLevel + 1].capacityValue;
                capacityCostText.text = "Maliyet  " +
                    busShopData.busRoleItems.busSkillUpgradeLevel[currentcapacityLevel + 1].unlockCapacityCost;
            } else {
                capacityUpgradeBtn.interactable = false;
                capacityNextValueText.text = "Max";
                capacityCostText.text = "Max Level ";
                if (busShopData.busAchievementItem.unlockedXRoleUpgradeLevel <= busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1) {
                    busShopData.busAchievementItem.xRoleUpgradeValue++;
                    ReadWriteAllRoles.WriteBusProp (busShopData);
                }
            }
            capacitySlider.fillAmount = (float) busShopData.busRoleItems.busSkillUpgradeLevel[currentcapacityLevel].capacityValue / busShopData.busRoleItems.capacityMaxValue;
            capacityValueText.text = "" + busShopData.busRoleItems.busSkillUpgradeLevel[currentcapacityLevel].capacityValue;

        }

        private void fastPassInfoBtnMethod () {
            int currentfastPassLevel = busShopData.busRoleItems.unlockedFastPassLevel;

            if (busShopData.busRoleItems.unlockedFastPassLevel < busShopData.busRoleItems.busSkillUpgradeLevel.Length - 1) {
                fastPassUpgradeBtn.interactable = true;
                fastPassNextValueText.text = "" + busShopData.busRoleItems.busSkillUpgradeLevel[currentfastPassLevel + 1].fastPassValue;
                fastPassCostText.text = "Maliyet  " +
                    busShopData.busRoleItems.busSkillUpgradeLevel[currentfastPassLevel + 1].unlockFastPassCost;
            } else {
                fastPassUpgradeBtn.interactable = false;
                fastPassNextValueText.text = "Max";
                fastPassCostText.text = "Max Level ";
                if (busShopData.busAchievementItem.unlockedXRoleUpgradeLevel <= busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1) {
                    busShopData.busAchievementItem.xRoleUpgradeValue++;
                    ReadWriteAllRoles.WriteBusProp (busShopData);
                }
            }
            fastPassSlider.fillAmount = (float) busShopData.busRoleItems.busSkillUpgradeLevel[currentfastPassLevel].fastPassValue / busShopData.busRoleItems.fastPassMaxValue;
            fastPassValueText.text = "" + busShopData.busRoleItems.busSkillUpgradeLevel[currentfastPassLevel].fastPassValue;

        }

        private void chancePassInfoBtnMethod () {
            int currentchancePassLevel = busShopData.busRoleItems.unlockedChancePassLevel;
            if (busShopData.busRoleItems.unlockedChancePassLevel < busShopData.busRoleItems.busSkillUpgradeLevel.Length - 1) {
                chancePassUpgradeBtn.interactable = true;
                chancePassNextValueText.text = "" + busShopData.busRoleItems.busSkillUpgradeLevel[currentchancePassLevel + 1].chancePassValue;
                chancePastCostText.text = "Maliyet  " +
                    busShopData.busRoleItems.busSkillUpgradeLevel[currentchancePassLevel + 1].unlockChancePassCost;
            } else {
                chancePassUpgradeBtn.interactable = false;
                chancePassNextValueText.text = "Max";
                chancePastCostText.text = "Max Level";
                if (busShopData.busAchievementItem.unlockedXRoleUpgradeLevel <= busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1) {
                    busShopData.busAchievementItem.xRoleUpgradeValue++;
                     ReadWriteAllRoles.WriteBusProp (busShopData);
                }
            }

            chancePassSlider.fillAmount = (float) busShopData.busRoleItems.busSkillUpgradeLevel[currentchancePassLevel].chancePassValue / busShopData.busRoleItems.chancePassMaxValue;
            chancePassValueText.text = "" + busShopData.busRoleItems.busSkillUpgradeLevel[currentchancePassLevel].chancePassValue;

        }

        private void comfortDrivePriceInfoBtnMethod () {
            int currentcomfortDrivePriceLevel = busShopData.busRoleItems.unlockedComfortDrivePriceLevel;
            if (busShopData.busRoleItems.unlockedComfortDrivePriceLevel < busShopData.busRoleItems.busSkillUpgradeLevel.Length - 1) {
                comfortDrivePriceUpgradeBtn.interactable = true;
                comfortDriveNextValueText.text = "" + busShopData.busRoleItems.busSkillUpgradeLevel[currentcomfortDrivePriceLevel + 1].comfortDrivePriceValue;
                comfortDriveCostText.text = "Maliyet  " +
                    busShopData.busRoleItems.busSkillUpgradeLevel[currentcomfortDrivePriceLevel + 1].unlockComfortDrivePriceCost;
            } else {
                comfortDrivePriceUpgradeBtn.interactable = false;
                comfortDriveNextValueText.text = "Max";
                comfortDriveCostText.text = "Max Level ";
                if (busShopData.busAchievementItem.unlockedXRoleUpgradeLevel <= busShopData.busAchievementItem.busAchievementsUpgradeLevel.Length - 1) {
                    busShopData.busAchievementItem.xRoleUpgradeValue++;
                    ReadWriteAllRoles.WriteBusProp (busShopData);
                }
            }

            comfortDrivePriveSlider.fillAmount = (float) busShopData.busRoleItems.busSkillUpgradeLevel[currentcomfortDrivePriceLevel].comfortDrivePriceValue / busShopData.busRoleItems.comfortDrivePriceMaxValue;
            comfortDriveValueText.text = "" + busShopData.busRoleItems.busSkillUpgradeLevel[currentcomfortDrivePriceLevel].comfortDrivePriceValue;

        }

    }

}