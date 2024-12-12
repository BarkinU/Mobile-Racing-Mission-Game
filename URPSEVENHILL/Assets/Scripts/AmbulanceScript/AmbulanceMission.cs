using System.Collections;
using System.Collections.Generic;
using RoleShopSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AmbulanceMission : MonoBehaviour {

    public GameObject Patient;
    AmbulanceShopData ambulanceShopData;
    BusShopData busShopData;
    private ImmortalHumanScript immortal;
    public AmbulanceGameManager ambulanceGameManage;
    private NoAccidentBus accidentAmbulance;
    private float currentTime = 3.0f;
    public int extraPatientMoney;
    private int extraWoundCounter;
    private int fullHealthCounter;
    private int fullAdrenalinCounter;
    private bool saveOnce;

    void Awake () {

        ambulanceGameManage = GameObject.FindObjectOfType<AmbulanceGameManager> ();
        immortal = GameObject.FindObjectOfType<ImmortalHumanScript> ();
        accidentAmbulance = GameObject.FindObjectOfType<NoAccidentBus> ();

        ambulanceShopData = ReadWriteAllRoles.ReadAmbulanceProp (ambulanceShopData);
        busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);

    }

    public void SpawnHospitalPoint () {

        GameObject.Instantiate (Patient, ambulanceGameManage.startPosition.transform.position, Quaternion.identity);
        ambulanceGameManage.startPosition.transform.position += new Vector3 (0.0f, 50.0f, 0.0f);;

    }

    private void ExtraMoneyForExtraWoundedCalculate () {
        extraPatientMoney += ambulanceGameManage.extraMoneyOfExtraWounded;
        extraPatientMoney *= extraWoundCounter;
    }
    private void GetPatientInAmbulance () {
        if (ambulanceGameManage.woundedCounter < ambulanceGameManage.patientCounter + 1) {
            if (ambulanceGameManage.woundedCounter == 1) {
                ambulanceGameManage.patientSymbol1.SetActive (true);
                ambulanceGameManage.isPatient1 = true;
                ambulanceGameManage.patientFollower.transform.position = ambulanceGameManage.patientSymbol2.transform.position;
                ambulanceGameManage.navigationRouter.getSecondMission = true;
            }
            if (ambulanceGameManage.woundedCounter == 2) {
                ambulanceGameManage.patientSymbol2.SetActive (true);
                ambulanceGameManage.isPatient2 = true;
                ambulanceGameManage.patientFollower.transform.position = ambulanceGameManage.patientSymbol3.transform.position;
                ambulanceGameManage.navigationRouter.getSecondMission = true;
            }
            if (ambulanceGameManage.woundedCounter == 3) {
                ambulanceGameManage.patientSymbol3.SetActive (true);
                ambulanceGameManage.isPatient3 = true;
                ambulanceGameManage.patientFollower.transform.position = ambulanceGameManage.startPosition.transform.position;
                ambulanceGameManage.navigationRouter.getSecondMission = true;

            }
            if (ambulanceGameManage.woundedCounter == ambulanceGameManage.patientCounter) {
                SpawnHospitalPoint ();
            }

        } else {
            ExtraWoundedControl ();
            ExtraMoneyForExtraWoundedCalculate ();
            EarnXpMoney ();
            ExtraPatientAchievement ();
            UseElectroShockAchievement ();
            UseAdrenalinAchievement ();
            UseHealAchievement ();
            ExtraWoundedAchievement ();
            ImmortalHumanAchievement ();
            NoAccidentSaveLife ();
            FullHealthLifeAchievement ();
            FullAdrenalinAchievement ();
            SavePatientLifeAchievement ();
            GenerelMissionCompleteAchievement ();
            GenerelMoneyAchievement ();
            GenerelXpAchievement ();
            GeneralSkillAchievement ();
            SaveMoneyXpAndAchievement ();
            MissionCompleteScreen ();
        }

    }

    private void MissionCompleteScreen () {
        Destroy (GameObject.FindGameObjectWithTag ("ambulanceMissionFollower"));
        ambulanceGameManage.missionCompleteScreen.SetActive (true);
        ambulanceGameManage.finishedTimeText.text = ":" + (int) ambulanceGameManage.finishTime;
        ambulanceGameManage.gainedXpText.text = ":" + ambulanceGameManage.xp;
        ambulanceGameManage.gainedMoneyText.text = ":" + ambulanceGameManage.money;
        ambulanceGameManage.savedLivesText.text = "Kurtarılan Hastalar:" + ambulanceGameManage.allWounded;
        ambulanceGameManage.deadPatientCounterText.text = "Kurtarılamayan Hastalar:" + ambulanceGameManage.deathCounter;
        ambulanceGameManage.usedElectroShockText.text = "Kullanılan Elektroşok:" + ambulanceGameManage.electroShockCounter;
        ambulanceGameManage.usedAdrenalinText.text = "Kullanılan Adrenalin:" + ambulanceGameManage.adrenalinCounter;
        ambulanceGameManage.usedHealText.text = "Kullanılan İyileştirme:" + ambulanceGameManage.healCounter;
        ambulanceGameManage.moneyText.text = ":" + ambulanceGameManage.money;
        ambulanceGameManage.xpText.text = ":" + ambulanceGameManage.xp;
        if (ambulanceGameManage.remainingTime <= 0) {
            ambulanceGameManage.missionFailedText.text = "Görev Başarısız";
            ambulanceGameManage.headerFrameImage.color = Color.red;
        }
        Time.timeScale = 0f;
    }
    private void ExtraWoundedControl () {

        if (ambulanceGameManage.patientCurrentHealth1 > 0) {
            ambulanceGameManage.allWounded++;
        }

        if (ambulanceGameManage.patientCounter > 1) {
            if (ambulanceGameManage.patientCurrentHealth2 > 0) {
                extraWoundCounter++;
                ambulanceGameManage.allWounded++;
            }
        }

        if (ambulanceGameManage.patientCounter > 2) {
            if (ambulanceGameManage.patientCurrentHealth3 > 0) {
                extraWoundCounter++;
                ambulanceGameManage.allWounded++;
            }
        }

        if (ambulanceGameManage.patientCurrentHealth1 >= ambulanceGameManage.maxHealth - 10) {
            fullHealthCounter++;
        }
        if (ambulanceGameManage.patientCurrentHealth2 >= ambulanceGameManage.maxHealth - 10) {
            fullHealthCounter++;
        }
        if (ambulanceGameManage.patientCurrentHealth3 >= ambulanceGameManage.maxHealth - 10) {
            fullHealthCounter++;
        }

        if (ambulanceGameManage.patientCurrentAdrenalin1 >= ambulanceGameManage.maxHealth - 10) {
            fullAdrenalinCounter++;
        }
        if (ambulanceGameManage.patientCurrentAdrenalin2 >= ambulanceGameManage.maxHealth - 10) {
            fullAdrenalinCounter++;
        }
        if (ambulanceGameManage.patientCurrentAdrenalin3 >= ambulanceGameManage.maxHealth - 10) {
            fullAdrenalinCounter++;
        }
    }
    private void EarnXpMoney () {
        
        ambulanceGameManage.money = 700 + extraPatientMoney + ((int) ambulanceGameManage.remainingTime *2+(int)ambulanceGameManage.distancePlayerPatient*3/2);
        ambulanceGameManage.xp = 100 + (extraPatientMoney / 6) + ((int) ambulanceGameManage.remainingTime / 4)+(int)ambulanceGameManage.distancePlayerPatient/6;

        ambulanceGameManage.gameData.totalMoney += ambulanceGameManage.money;
        ambulanceGameManage.gameData.totalXp += ambulanceGameManage.xp;

        //  GPGSManager.AddScoreToLeaderBoard(GPGSIds.leaderboard_leaderboard, ambulanceGameManage.money);
    }
    private void ExtraPatientAchievement () {
        if (ambulanceShopData.ambulanceAchievementItem.unlockedXGainMoneyFromExtraPatientLevel <= ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel.Length - 1) {
            ambulanceShopData.ambulanceAchievementItem.xGainMoneyFromExtraPatientValue += extraPatientMoney;
            Debug.Log ("SignBoardAchieveSaved");
        }
    }
    private void SavePatientLifeAchievement () {
        if (ambulanceShopData.ambulanceAchievementItem.unlockedXPatientWithoutDyingLevel <= ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel.Length - 1) {
            ambulanceShopData.ambulanceAchievementItem.xPatientWithoutDyingValue += extraWoundCounter + 1;
            Debug.Log ("PatientLifeAchieSaved");
        }
    }

    private void UseElectroShockAchievement () {
        if (ambulanceShopData.ambulanceAchievementItem.unlockedXUseElectroShockLevel <= ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel.Length - 1) {
            ambulanceShopData.ambulanceAchievementItem.xUseElectroShockValue += ambulanceGameManage.electroShockCounter;
            Debug.Log ("ElectroShockAchieSaved");
        }
    }

    private void UseAdrenalinAchievement () {
        if (ambulanceShopData.ambulanceAchievementItem.unlockedXUseAdrenalinLevel <= ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel.Length - 1) {
            ambulanceShopData.ambulanceAchievementItem.xUseAdrenalinValue += ambulanceGameManage.adrenalinCounter;
            Debug.Log ("AdrenalinAchieSaved");
        }
    }

    private void UseHealAchievement () {
        if (ambulanceShopData.ambulanceAchievementItem.unlockedXUseHealLevel <= ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel.Length - 1) {
            ambulanceShopData.ambulanceAchievementItem.xUseHealValue += ambulanceGameManage.healCounter;
            Debug.Log ("HealAchieSaved");
        }
    }

    private void ExtraWoundedAchievement () {
        if (ambulanceShopData.ambulanceAchievementItem.unlockedXMultiplePatientWithoutDyingLevel <= ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel.Length - 1) {
            if (extraWoundCounter > 0) {
                ambulanceShopData.ambulanceAchievementItem.xMultiplePatientWithoutDyingValue++;
            }
            Debug.Log ("HealAchieSaved");
        }
    }

    private void ImmortalHumanAchievement () {
        if (immortal.immortalHumanFound == true) {
            ambulanceShopData.ambulanceAchievementItem.findTheImmortalPatientValue = true;
        }
        Debug.Log ("ImmortalAchieSaved");
    }

    private void NoAccidentSaveLife () {
        if (ambulanceShopData.ambulanceAchievementItem.unlockedXPatientWithoutDyingNoAccidentLevel <= ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel.Length - 1) {
            if (accidentAmbulance.accident == false) {
                if (ambulanceGameManage.allWounded > 0) {
                    ambulanceShopData.ambulanceAchievementItem.xPatientWithoutDyingNoAccidentValue++;
                }
            }
            Debug.Log ("NoAccidentNoDeathAchieveSaved");
        }
    }

    private void FullHealthLifeAchievement () {
        if (ambulanceShopData.ambulanceAchievementItem.unlockedXPatientFullLifeLevel <= ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel.Length - 1) {
            ambulanceShopData.ambulanceAchievementItem.xPatientFullLifeValue += fullHealthCounter;
            Debug.Log ("FullHealthAchieveSaved");
        }
    }

    private void FullAdrenalinAchievement () {
        if (ambulanceShopData.ambulanceAchievementItem.unlockedXPatientFullAdrenalinLevel <= ambulanceShopData.ambulanceAchievementItem.ambulanceAchievementsUpgradeLevel.Length - 1) {
            ambulanceShopData.ambulanceAchievementItem.xPatientFullAdrenalinValue += fullAdrenalinCounter;
            Debug.Log ("FullAdrenalinAchieveSaved");
        }
    }
    private void GenerelMissionCompleteAchievement () {
        if (busShopData.generalAchievementItem.unlockedCompleteXMissionLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
            busShopData.generalAchievementItem.completeXMissionValue++;
            Debug.Log ("GeneralMissionCompleteAchieveSaved");
        }
    }
    private void GenerelMoneyAchievement () {
        if (busShopData.generalAchievementItem.unlockedGainMoneyFromMissionLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
            busShopData.generalAchievementItem.gainMoneyFromMissionValue += ambulanceGameManage.money;
            Debug.Log ("GeneralMoneyAchieveSaved");
        }
    }
    private void GenerelXpAchievement () {
        if (busShopData.generalAchievementItem.unlockedGainXExperienceLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
            busShopData.generalAchievementItem.gainXExperienceValue += ambulanceGameManage.xp;
            Debug.Log ("GeneralExperienceAchieveSaved");
        }
    }

    private void GeneralSkillAchievement () {
        if (busShopData.generalAchievementItem.unlockedXUseSkillLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
            busShopData.generalAchievementItem.xUseSkillValue += ambulanceGameManage.healCounter + ambulanceGameManage.electroShockCounter + ambulanceGameManage.adrenalinCounter;
        }
    }

    private void SaveMoneyXpAndAchievement () {

        ReadWriteAllRoles.WriteAmbulanceProp (ambulanceShopData);
        ReadWriteAllRoles.WriteBusProp (busShopData);
        ReadWriteAllRoles.WriteGameProp (ambulanceGameManage.gameData);

    }

    private void OnTriggerStay (Collider oyuncu) {

        if (oyuncu.CompareTag ("Player")) {
            currentTime -= 1 * Time.deltaTime;
            if (currentTime <= 0) {
                if (saveOnce == false) {
                    ambulanceGameManage.woundedCounter++;
                    GetPatientInAmbulance ();
                    Destroy (this.gameObject);
                    saveOnce = true;
                }

            }
        }
    }

}