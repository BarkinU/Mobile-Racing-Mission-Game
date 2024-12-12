using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using CarShopSystem;

namespace RoleShopSystem {
    public class PoliceRoleShopUI : MonoBehaviour {
        public PoliceShopData policeShopData;
        public CarShopUI carShopUI;
        BusShopData busShopData;
        public TextMeshProUGUI catchValue, catchNextValue, kidnapValue, kidnapNextValue, catchBonusValue, catchBonusNextValue, decreaseValue, decreaseNextValue, catchCost, kidnapCost, catchBonusCost, decreaseCost;
        public TextMeshProUGUI totalXpText;
        public Button catchUpgrade, kidnapUpgrade, catchBonusUpgrade, decreaseUpgrade;
        public Image catchSlider, kidnapSlider, catchBonusSlider, decreaseSlider;
        public int currentIndex = 0;
        public int currentRole = 0;
        public int selectedINdex = 0;
        public int selectedRole = 0;

        private void Start () {

            carShopUI.gameData = ReadWriteAllRoles.ReadGameProp (carShopUI.gameData);
            busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
            policeShopData = ReadWriteAllRoles.ReadPoliceProp (policeShopData);

            selectedRole = policeShopData.selectedRole;
            selectedINdex = policeShopData.selectedIndex;
            currentIndex = selectedINdex;
            totalXpText.text = " " + carShopUI.gameData.totalXp;

            catchInCrimeSceneInfoBtnMethod();
            DecreaseCatchTimeInfoBtnMethod ();
            catchBonusInfoBtnMethod ();
            decreaseThiefSpeedInfoBtnMethod ();

        }

        public void CatchUpgradeButtonFunc () {

            int nextCatchInCrimeSceneLevelIndex = policeShopData.policeRoleItems.unlockedCatchInCrimeSceneChanceLevel + 1;
            if (carShopUI.gameData.totalXp >= policeShopData.policeRoleItems.policeSkillUpgradeLevel[nextCatchInCrimeSceneLevelIndex].unlockCatchInCrimeSceneChanceCost) {
                carShopUI.gameData.totalXp -= policeShopData.policeRoleItems.policeSkillUpgradeLevel[nextCatchInCrimeSceneLevelIndex].unlockCatchInCrimeSceneChanceCost;
                totalXpText.text = " " + carShopUI.gameData.totalXp;

                ReadWriteAllRoles.WriteGameProp (carShopUI.gameData);

                policeShopData.policeRoleItems.unlockedCatchInCrimeSceneChanceLevel++;

                if (busShopData.generalAchievementItem.unlockedUpgradeXRoleLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.upgradeXRoleValue++;
                    ReadWriteAllRoles.WriteBusProp (busShopData);
                }

                catchInCrimeSceneInfoBtnMethod();
            }

        }
        public void DecreaseCatchTimeUpgradeButtonFunc () {

            int nextCatchTimeLevel = policeShopData.policeRoleItems.unlockedDecreaseCatchTimeLevel + 1;
            if (carShopUI.gameData.totalXp >= policeShopData.policeRoleItems.policeSkillUpgradeLevel[nextCatchTimeLevel].unlockedDecreaseCatchTimeCost) {
                carShopUI.gameData.totalXp -= policeShopData.policeRoleItems.policeSkillUpgradeLevel[nextCatchTimeLevel].unlockedDecreaseCatchTimeCost;
                totalXpText.text = " " + carShopUI.gameData.totalXp;

                ReadWriteAllRoles.WriteGameProp (carShopUI.gameData);

                policeShopData.policeRoleItems.unlockedDecreaseCatchTimeLevel++;

                if (busShopData.generalAchievementItem.unlockedUpgradeXRoleLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.upgradeXRoleValue++;
                    ReadWriteAllRoles.WriteBusProp (busShopData);
                }

                DecreaseCatchTimeInfoBtnMethod ();
            }

        }
        public void CatchBonusUpgradeButtonFunc () {

            int nextCatchBonusLevelIndex = policeShopData.policeRoleItems.unlockedCatchBonusLevel + 1;

            if (carShopUI.gameData.totalXp >= policeShopData.policeRoleItems.policeSkillUpgradeLevel[nextCatchBonusLevelIndex].unlockCatchBonusCost) {
                carShopUI.gameData.totalXp -= policeShopData.policeRoleItems.policeSkillUpgradeLevel[nextCatchBonusLevelIndex].unlockCatchBonusCost;
                totalXpText.text = " " + carShopUI.gameData.totalXp;

                ReadWriteAllRoles.WriteGameProp (carShopUI.gameData);

                policeShopData.policeRoleItems.unlockedCatchBonusLevel++;

                if (busShopData.generalAchievementItem.unlockedUpgradeXRoleLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.upgradeXRoleValue++;
                    ReadWriteAllRoles.WriteBusProp (busShopData);
                }
                catchBonusInfoBtnMethod ();
            }

        }
        public void DecreaseThiefSpeedUpgradeButtonFunc () {

            int nextDecreaseThiefSpeedLevelIndex = policeShopData.policeRoleItems.unlockedDecreaseThiefSpeedLevel + 1;

            if (carShopUI.gameData.totalXp >= policeShopData.policeRoleItems.policeSkillUpgradeLevel[nextDecreaseThiefSpeedLevelIndex].unlockedDecreaseThiefSpeedCost) {
                carShopUI.gameData.totalXp -= policeShopData.policeRoleItems.policeSkillUpgradeLevel[nextDecreaseThiefSpeedLevelIndex].unlockedDecreaseThiefSpeedCost;
                totalXpText.text = " " + carShopUI.gameData.totalXp;

                ReadWriteAllRoles.WriteGameProp (carShopUI.gameData);

                policeShopData.policeRoleItems.unlockedDecreaseThiefSpeedLevel++;

                if (busShopData.generalAchievementItem.unlockedUpgradeXRoleLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                    busShopData.generalAchievementItem.upgradeXRoleValue++;
                    ReadWriteAllRoles.WriteBusProp (busShopData);
                }

                if (policeShopData.policeAchievementItem.unlockedDoMaxLevelSlowThiefLevel <= policeShopData.policeAchievementItem.policeAchievementsUpgradeLevel.Length - 1) {
                    policeShopData.policeAchievementItem.doMaxLevelSlowThiefValue++;            
                    ReadWriteAllRoles.WritePoliceProp (policeShopData);
                }

                decreaseThiefSpeedInfoBtnMethod ();
            }

        }
        private void catchInCrimeSceneInfoBtnMethod () {
            int currentCatchInCrimeSceneLevel = policeShopData.policeRoleItems.unlockedCatchInCrimeSceneChanceLevel;

            if (policeShopData.policeRoleItems.unlockedCatchInCrimeSceneChanceLevel < policeShopData.policeRoleItems.policeSkillUpgradeLevel.Length - 1) {
                catchUpgrade.interactable = true;
                catchNextValue.text = "" + policeShopData.policeRoleItems.policeSkillUpgradeLevel[currentCatchInCrimeSceneLevel + 1].catchInCrimeSceneChanceValue;
                catchCost.text = "Maliyet  " +
                    policeShopData.policeRoleItems.policeSkillUpgradeLevel[currentCatchInCrimeSceneLevel + 1].unlockCatchInCrimeSceneChanceCost;
            } else {
                catchUpgrade.interactable = false;
                catchNextValue.text = "Max";
                catchCost.text = "Max Level ";
            }

            catchSlider.fillAmount = (float)policeShopData.policeRoleItems.policeSkillUpgradeLevel[currentCatchInCrimeSceneLevel].catchInCrimeSceneChanceValue / policeShopData.policeRoleItems.catchInCrimeSceneChanceMaxValue;
            catchValue.text = "" + policeShopData.policeRoleItems.policeSkillUpgradeLevel[currentCatchInCrimeSceneLevel].catchInCrimeSceneChanceValue;

        }

        private void DecreaseCatchTimeInfoBtnMethod () {
            int currentCatchTimeLevel = policeShopData.policeRoleItems.unlockedDecreaseCatchTimeLevel;

            if (policeShopData.policeRoleItems.unlockedDecreaseCatchTimeLevel < policeShopData.policeRoleItems.policeSkillUpgradeLevel.Length - 1) {
                kidnapUpgrade.interactable = true;
                kidnapNextValue.text = "" + policeShopData.policeRoleItems.policeSkillUpgradeLevel[currentCatchTimeLevel + 1].decreaseCatchTimeValue;
                kidnapCost.text = "Maliyet  " +
                    policeShopData.policeRoleItems.policeSkillUpgradeLevel[currentCatchTimeLevel + 1].unlockedDecreaseCatchTimeCost;

            } else {
                kidnapUpgrade.interactable = false;
                kidnapNextValue.text = "Max";
                kidnapCost.text = "Max Level ";
            }

            kidnapSlider.fillAmount = (float)policeShopData.policeRoleItems.policeSkillUpgradeLevel[currentCatchTimeLevel].decreaseCatchTimeValue / policeShopData.policeRoleItems.decreaseCatchTimeMaxValue;
            kidnapValue.text = "" + policeShopData.policeRoleItems.policeSkillUpgradeLevel[currentCatchTimeLevel].decreaseCatchTimeValue;

        }

        private void catchBonusInfoBtnMethod () {
            int currentCatchBonusLevel = policeShopData.policeRoleItems.unlockedCatchBonusLevel;

            if (policeShopData.policeRoleItems.unlockedCatchBonusLevel < policeShopData.policeRoleItems.policeSkillUpgradeLevel.Length - 1) {
                catchBonusUpgrade.interactable = true;
                catchBonusNextValue.text = "" + policeShopData.policeRoleItems.policeSkillUpgradeLevel[currentCatchBonusLevel + 1].catchBonusValue;
                catchBonusCost.text = "Maliyet  " +
                    policeShopData.policeRoleItems.policeSkillUpgradeLevel[currentCatchBonusLevel + 1].unlockCatchBonusCost;

            } else {
                catchBonusUpgrade.interactable = false;
                catchBonusNextValue.text = "Max";
                catchBonusCost.text = "Max Level";
            }

            catchBonusSlider.fillAmount = (float)policeShopData.policeRoleItems.policeSkillUpgradeLevel[currentCatchBonusLevel].catchBonusValue / policeShopData.policeRoleItems.catchBonusMaxValue;
            catchBonusValue.text = "" + policeShopData.policeRoleItems.policeSkillUpgradeLevel[currentCatchBonusLevel].catchBonusValue;

        }

        private void decreaseThiefSpeedInfoBtnMethod () {
            int currentDecreaseThiefSpeedLevel = policeShopData.policeRoleItems.unlockedDecreaseThiefSpeedLevel;

            if (policeShopData.policeRoleItems.unlockedDecreaseThiefSpeedLevel < policeShopData.policeRoleItems.policeSkillUpgradeLevel.Length - 1) {
                decreaseUpgrade.interactable = true;
                decreaseNextValue.text = "" + policeShopData.policeRoleItems.policeSkillUpgradeLevel[currentDecreaseThiefSpeedLevel + 1].decreaseThiefSpeedValue;
                decreaseCost.text = "Maliyet  " +
                    policeShopData.policeRoleItems.policeSkillUpgradeLevel[currentDecreaseThiefSpeedLevel + 1].unlockedDecreaseThiefSpeedCost;
            } else {
                decreaseUpgrade.interactable = false;
                decreaseNextValue.text = "Max";
                decreaseCost.text = "Max Level ";
            }

            decreaseSlider.fillAmount = (float)policeShopData.policeRoleItems.policeSkillUpgradeLevel[currentDecreaseThiefSpeedLevel].decreaseThiefSpeedValue / policeShopData.policeRoleItems.decreaseThiefSpeedMaxValue;
            decreaseValue.text = "" + policeShopData.policeRoleItems.policeSkillUpgradeLevel[currentDecreaseThiefSpeedLevel].decreaseThiefSpeedValue;

        }


    }

}