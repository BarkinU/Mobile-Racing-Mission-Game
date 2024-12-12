using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using CarShopSystem;

namespace RoleShopSystem {
    public class TaxiRoleShopUI : MonoBehaviour {
        public TaxiShopData taxiShopData;
        public CarShopUI carShopUI;
        BusShopData busShopData;
        public TextMeshProUGUI longValue, closeValue, customerValue, gasolineValue, longNextValue, closeNextValue, customerNextValue, gasolineNextValue, longCost, closeCost, customerCost, gasolineCost;
        public TextMeshProUGUI totalXpText;
        public Button longUpgrade, closeUpgrade, customerUpgrade, gasolineUpgrade;
        public Image longSlider, closeSlider, customerSlider, gasolineSlider;
        public int currentIndex = 0;
        public int currentRole = 0;
        public int selectedINdex = 0;
        public int selectedRole = 0;

        private void Start () {

            carShopUI.gameData = ReadWriteAllRoles.ReadGameProp (carShopUI.gameData);
            busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);

            selectedRole = taxiShopData.selectedRole;
            selectedINdex = taxiShopData.selectedIndex;
            currentIndex = selectedINdex;
            totalXpText.text = " " + carShopUI.gameData.totalXp;

            longDistanceInfoBtnMethod ();
            closeCustomerInfoBtnMethod ();
            customerSatisfactionInfoBtnMethod ();
            gasolineReductionRateInfoBtnMethod ();

        }
        public void LongUpgradeButtonFunc () {

            int nextLongDistanceLevelIndex = taxiShopData.taxiRoleItems.unlockedLongDistanceLevel + 1;
            if (carShopUI.gameData.totalXp >= taxiShopData.taxiRoleItems.taxiSkillUpgradeLevel[nextLongDistanceLevelIndex].unlockLongDistanceCost) {
                carShopUI.gameData.totalXp -= taxiShopData.taxiRoleItems.taxiSkillUpgradeLevel[nextLongDistanceLevelIndex].unlockLongDistanceCost;
                totalXpText.text = " " + carShopUI.gameData.totalXp;

                ReadWriteAllRoles.WriteGameProp (carShopUI.gameData);

                taxiShopData.taxiRoleItems.unlockedLongDistanceLevel++;

                if (busShopData.generalAchievementItem.unlockedUpgradeXRoleLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.upgradeXRoleValue++;
                    ReadWriteAllRoles.WriteBusProp (busShopData);
                }
                longDistanceInfoBtnMethod ();
            }

        }
        public void CloseUpgradeButtonFunc () {
            int nextCloseCustomerLevelIndex = taxiShopData.taxiRoleItems.unlockedCloseCustomerLevel + 1;
            if (carShopUI.gameData.totalXp >= taxiShopData.taxiRoleItems.taxiSkillUpgradeLevel[nextCloseCustomerLevelIndex].unlockCloseCustomerCost) {
                carShopUI.gameData.totalXp -= taxiShopData.taxiRoleItems.taxiSkillUpgradeLevel[nextCloseCustomerLevelIndex].unlockCloseCustomerCost;
                totalXpText.text = " " + carShopUI.gameData.totalXp;

                ReadWriteAllRoles.WriteGameProp (carShopUI.gameData);

                taxiShopData.taxiRoleItems.unlockedCloseCustomerLevel++;

                if (busShopData.generalAchievementItem.unlockedUpgradeXRoleLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.upgradeXRoleValue++;
                    ReadWriteAllRoles.WriteBusProp (busShopData);
                }
                closeCustomerInfoBtnMethod ();
            }

        }
        public void CustomerUpgradeButtonFunc () {
            int nextCustomerSatisfactionLevelIndex = taxiShopData.taxiRoleItems.unlockedCustomerSatisfactionLevel + 1;

            if (carShopUI.gameData.totalXp >= taxiShopData.taxiRoleItems.taxiSkillUpgradeLevel[nextCustomerSatisfactionLevelIndex].unlockCustomerSatisfactionCost) {
                carShopUI.gameData.totalXp -= taxiShopData.taxiRoleItems.taxiSkillUpgradeLevel[nextCustomerSatisfactionLevelIndex].unlockCustomerSatisfactionCost;
                totalXpText.text = " " + carShopUI.gameData.totalXp;

                ReadWriteAllRoles.WriteGameProp (carShopUI.gameData);

                taxiShopData.taxiRoleItems.unlockedCustomerSatisfactionLevel++;

                if (busShopData.generalAchievementItem.unlockedUpgradeXRoleLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.upgradeXRoleValue++;
                    ReadWriteAllRoles.WriteBusProp (busShopData);
                }
                customerSatisfactionInfoBtnMethod ();
            }

        }
        public void GasolineUpgradeButtonFunc () {
            int nextGasolineReductionRateLevelIndex = taxiShopData.taxiRoleItems.unlockedGasolineReductionRateLevel + 1;

            if (carShopUI.gameData.totalXp >= taxiShopData.taxiRoleItems.taxiSkillUpgradeLevel[nextGasolineReductionRateLevelIndex].unlockGasolineReductionRateCost) {
                carShopUI.gameData.totalXp -= taxiShopData.taxiRoleItems.taxiSkillUpgradeLevel[nextGasolineReductionRateLevelIndex].unlockGasolineReductionRateCost;
                totalXpText.text = " " + carShopUI.gameData.totalXp;

                ReadWriteAllRoles.WriteGameProp (carShopUI.gameData);

                taxiShopData.taxiRoleItems.unlockedGasolineReductionRateLevel++;

                if (busShopData.generalAchievementItem.unlockedUpgradeXRoleLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.upgradeXRoleValue++;
                    ReadWriteAllRoles.WriteBusProp (busShopData);
                }
                gasolineReductionRateInfoBtnMethod ();
            }

        }
        private void longDistanceInfoBtnMethod () {
            int currentLongDistanceLevel = taxiShopData.taxiRoleItems.unlockedLongDistanceLevel;

            if (taxiShopData.taxiRoleItems.unlockedLongDistanceLevel < taxiShopData.taxiRoleItems.taxiSkillUpgradeLevel.Length - 1) {
                longUpgrade.interactable = true;
                longNextValue.text = "" + taxiShopData.taxiRoleItems.taxiSkillUpgradeLevel[currentLongDistanceLevel + 1].longDistanceValue;
                longCost.text = "Maliyet  " +
                    taxiShopData.taxiRoleItems.taxiSkillUpgradeLevel[currentLongDistanceLevel + 1].unlockLongDistanceCost;

            } else {
                longUpgrade.interactable = false;
                longNextValue.text = "Max";
                longCost.text = "Max Level ";
            }

            longSlider.fillAmount = (float) taxiShopData.taxiRoleItems.taxiSkillUpgradeLevel[currentLongDistanceLevel].longDistanceValue / taxiShopData.taxiRoleItems.longDistanceMaxValue;
            longValue.text = "" + taxiShopData.taxiRoleItems.taxiSkillUpgradeLevel[currentLongDistanceLevel].longDistanceValue;

        }

        private void closeCustomerInfoBtnMethod () {
            int currentCloseCustomerLevel = taxiShopData.taxiRoleItems.unlockedCloseCustomerLevel;

            if (taxiShopData.taxiRoleItems.unlockedCloseCustomerLevel < taxiShopData.taxiRoleItems.taxiSkillUpgradeLevel.Length - 1) {
                closeUpgrade.interactable = true;
                closeNextValue.text = "" + taxiShopData.taxiRoleItems.taxiSkillUpgradeLevel[currentCloseCustomerLevel + 1].closeCustomerValue;
                closeCost.text = "Maliyet  " +
                    taxiShopData.taxiRoleItems.taxiSkillUpgradeLevel[currentCloseCustomerLevel + 1].unlockCloseCustomerCost;
            } else {
                closeUpgrade.interactable = false;
                closeNextValue.text = "Max";
                closeCost.text = "Max Level ";
            }

            closeSlider.fillAmount =-taxiShopData.taxiRoleItems.closeCustomerMaxValue / -(float) taxiShopData.taxiRoleItems.taxiSkillUpgradeLevel[currentCloseCustomerLevel].closeCustomerValue ;
            closeValue.text = "" + taxiShopData.taxiRoleItems.taxiSkillUpgradeLevel[currentCloseCustomerLevel].closeCustomerValue;

        }

        private void customerSatisfactionInfoBtnMethod () {
            int currentCustomerSatisfactionLevel = taxiShopData.taxiRoleItems.unlockedCustomerSatisfactionLevel;

            if (taxiShopData.taxiRoleItems.unlockedCustomerSatisfactionLevel < taxiShopData.taxiRoleItems.taxiSkillUpgradeLevel.Length - 1) {
                customerUpgrade.interactable = true;
                customerNextValue.text = "" + taxiShopData.taxiRoleItems.taxiSkillUpgradeLevel[currentCustomerSatisfactionLevel + 1].customerSatisfactionValue;
                customerCost.text = "Maliyet  " +
                    taxiShopData.taxiRoleItems.taxiSkillUpgradeLevel[currentCustomerSatisfactionLevel + 1].unlockCustomerSatisfactionCost;
            } else {
                customerUpgrade.interactable = false;
                customerNextValue.text = "Max";
                customerCost.text = "Max Level";
            }

            customerSlider.fillAmount = (float) taxiShopData.taxiRoleItems.taxiSkillUpgradeLevel[currentCustomerSatisfactionLevel].customerSatisfactionValue / taxiShopData.taxiRoleItems.customerSatisfactionMaxValue;
            customerValue.text = "" + taxiShopData.taxiRoleItems.taxiSkillUpgradeLevel[currentCustomerSatisfactionLevel].customerSatisfactionValue;

        }

        private void gasolineReductionRateInfoBtnMethod () {
            int currentGasolineReductionRateLevel = taxiShopData.taxiRoleItems.unlockedGasolineReductionRateLevel;

            if (taxiShopData.taxiRoleItems.unlockedGasolineReductionRateLevel < taxiShopData.taxiRoleItems.taxiSkillUpgradeLevel.Length - 1) {
                gasolineUpgrade.interactable = true;
                gasolineNextValue.text = "" + taxiShopData.taxiRoleItems.taxiSkillUpgradeLevel[currentGasolineReductionRateLevel + 1].gasolineReductionRateValue;
                gasolineCost.text = "Maliyet  " +
                    taxiShopData.taxiRoleItems.taxiSkillUpgradeLevel[currentGasolineReductionRateLevel + 1].unlockGasolineReductionRateCost;
            } else {
                gasolineUpgrade.interactable = false;
                gasolineNextValue.text = "Max";
                gasolineCost.text = "Max Level ";
            }

            gasolineSlider.fillAmount = (float) taxiShopData.taxiRoleItems.taxiSkillUpgradeLevel[currentGasolineReductionRateLevel].gasolineReductionRateValue / taxiShopData.taxiRoleItems.gasolineReductionRateMaxValue;
            gasolineValue.text = "" + taxiShopData.taxiRoleItems.taxiSkillUpgradeLevel[currentGasolineReductionRateLevel].gasolineReductionRateValue;

        }

    }

}