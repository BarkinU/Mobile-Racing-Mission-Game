using System.Collections;
using System.Collections.Generic;
using RoleShopSystem;
using UnityEngine;

public class FireMission : MonoBehaviour {
    public float extinguishTime, extinguishedTime;
    private float tempExtin;
    FireShopData fireShopData;
    BusShopData busShopData;
    GameData gameData;
    public ColdFireScript coldFireScript;
    public FireGameManager fireGameManage;
    public CrashWaterHydrant crashWaterHydrant;
    private GameObject PlayerCar;
    public int rescuedLife;
    public bool playerExit = false;

    private float spentWater;
    private int bonusMoneyFromSavedLife;
    private bool saveOnce;
    public int fireID;

    private void Start () {

        FastPutOutFire ();
        SkillValuesCalculate ();
        PlayerCar = GameObject.FindWithTag ("Player");
        crashWaterHydrant = PlayerCar.GetComponent<CrashWaterHydrant> ();

        fireShopData = ReadWriteAllRoles.ReadFireProp (fireShopData);
        gameData = ReadWriteAllRoles.ReadGameProp (gameData);
        busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
    }

    private void OnEnable () {
        fireID = fireGameManage.spawn;
    }

    private void Update () {
        MissionContent ();
    }
    private void MissionContent () {
        if (fireGameManage.raidTime > 0) {
            FireGameManager.baseExtinguishTime -= 1 * Time.deltaTime;
            fireGameManage.raidTime -= 1 * Time.deltaTime;

            if (fireGameManage.currentFireID == 0) {
            fireGameManage.firePlace2.fillAmount = FireGameManager.baseExtinguishTime / 10;
            fireGameManage.firePlace3.fillAmount = FireGameManager.baseExtinguishTime / 10;
            if(fireGameManage.isInOrOut1==false)
            {
                extinguishTime -= 1 * Time.deltaTime;
            }
            Debug.Log("Diego5sesvar");
            }
            if (fireGameManage.currentFireID == 1) {
            fireGameManage.firePlace1.fillAmount = FireGameManager.baseExtinguishTime / 10;
            fireGameManage.firePlace3.fillAmount = FireGameManager.baseExtinguishTime / 10;
            Debug.Log("Diego6sesvar");
            if(fireGameManage.isInOrOut2==false)
            {
                extinguishTime -= 1 * Time.deltaTime;
            }
            }
            if (fireGameManage.currentFireID == 2) {
            fireGameManage.firePlace1.fillAmount = FireGameManager.baseExtinguishTime / 10;
            fireGameManage.firePlace2.fillAmount = FireGameManager.baseExtinguishTime / 10;
            Debug.Log("Diego7sesvar");
            if(fireGameManage.isInOrOut3==false)
            {
                extinguishTime -= 1 * Time.deltaTime;
            }
            }
        }else if (playerExit == true) {
            if (extinguishTime < FireGameManager.baseExtinguishTime) {
                Debug.Log("DiegoBisesvar");
                extinguishTime += 1 * Time.deltaTime;
                if (fireGameManage.currentFireID == 0) {
                    fireGameManage.firePlace1.fillAmount = extinguishTime / 10;
                    Debug.Log("Diego8sesvar");
                }
                if (fireGameManage.currentFireID == 1) {
                    fireGameManage.firePlace2.fillAmount = extinguishTime / 10;
                    Debug.Log("Diego9sesvar");
                }
                if (fireGameManage.currentFireID == 2) {
                    fireGameManage.firePlace3.fillAmount = extinguishTime / 10;
                    Debug.Log("Diego10sesvar");
                }

            } else {
                playerExit = false;
                if (fireGameManage.currentFireID == 0) {
                fireGameManage.isInOrOut1=true;
                }
                if (fireGameManage.currentFireID == 1) {
                fireGameManage.isInOrOut2=true;
                }
                if (fireGameManage.currentFireID == 2) {
                fireGameManage.isInOrOut3=true;
                }
            }


        }
    }

    private void OnTriggerEnter (Collider oyuncu) {
        if (oyuncu.CompareTag ("Player")) {
            extinguishTime = FireGameManager.baseExtinguishTime;
            fireGameManage.currentFireID=fireID;
            if (fireGameManage.extinguishCounter == 0) {
                if (fireGameManage.raidSkillCounter == 4) {
                    fireGameManage.raidButton.interactable = false;
                } else {
                    fireGameManage.raidButton.interactable = true;
                }
            }

            if (fireGameManage.currentFireID == 0) {
                fireGameManage.isInOrOut1=true;
                }
                if (fireGameManage.currentFireID == 1) {
                fireGameManage.isInOrOut2=true;
                }
                if (fireGameManage.currentFireID == 2) {
                fireGameManage.isInOrOut3=true;
                }
        }
    }

    private void OnTriggerExit (Collider oyuncu) {
        if (oyuncu.CompareTag ("Player")) {
            playerExit = true;
            if (fireGameManage.currentFireID == 0) {
                fireGameManage.isInOrOut1=false;
                }
                if (fireGameManage.currentFireID == 1) {
                fireGameManage.isInOrOut2=false;
                }
                if (fireGameManage.currentFireID == 2) {
                fireGameManage.isInOrOut3=false;
                }
        }
    }
    private void XpAndMoneyIncrease () {

        fireGameManage.money += (100 + (fireGameManage.rescueLifeBonus * (fireGameManage.catLife + fireGameManage.humanLife)));
        fireGameManage.gameData.totalMoney += fireGameManage.money;
        fireGameManage.gameData.totalXp += fireGameManage.xp;

        ReadWriteAllRoles.WriteGameProp (fireGameManage.gameData);

    }
    private void SkillValuesCalculate () {
        float additionalDec = extinguishTime * fireGameManage.additionalHose / 100;
        extinguishTime -= additionalDec;
    }

    private void FastPutOutFire () {
        float decExtinguishTime = (FireGameManager.baseExtinguishTime * fireGameManage.fastPutOutPercent) / 100;
        FireGameManager.baseExtinguishTime -= decExtinguishTime;
    }
    private void UpgradeHose () {

    }
    private void DecreaseWaterTank () {
        if (fireGameManage.water > 0) {
            fireGameManage.water -= 5 * Time.fixedDeltaTime;
            spentWater += 3 * Time.fixedDeltaTime;
        } else {
            fireGameManage.money -= 3;
        }
    }

    private void SaveLifeControl () {
        if (fireGameManage.remainingTime >= 40) {
            fireGameManage.catLife += 2;
            fireGameManage.humanLife += 2;

            fireGameManage.catCounterText.text = "Kurtarılan Kediler:" + fireGameManage.catLife;
            fireGameManage.humanCounterText.text = "Kurtarılan İnsanlar:" + fireGameManage.humanLife;
            bonusMoneyFromSavedLife += (100 + (fireGameManage.rescueLifeBonus * (fireGameManage.catLife + fireGameManage.humanLife)));
            fireGameManage.money += (100 + (fireGameManage.rescueLifeBonus * (fireGameManage.catLife + fireGameManage.humanLife)) + fireGameManage.humanMoney * fireGameManage.humanLife + fireGameManage.catLife * fireGameManage.catMoney) + (int) fireGameManage.distancePlayerToFire * 3 / 4;
            fireGameManage.xp += 50 + (fireGameManage.humanMoney * fireGameManage.humanLife + fireGameManage.catLife * fireGameManage.catMoney) / 12 + (int) fireGameManage.distancePlayerToFire / 10;
        } else if (fireGameManage.remainingTime >= 30) {
            fireGameManage.catLife++;
            fireGameManage.humanLife += 2;

            fireGameManage.catCounterText.text = "Kurtarılan Kediler:" + fireGameManage.catLife;
            fireGameManage.humanCounterText.text = "Kurtarılan İnsanlar:" + fireGameManage.humanLife;
            bonusMoneyFromSavedLife += (100 + (fireGameManage.rescueLifeBonus * (fireGameManage.catLife + fireGameManage.humanLife)));
            fireGameManage.money += (100 + (fireGameManage.rescueLifeBonus * (fireGameManage.catLife + fireGameManage.humanLife)) + fireGameManage.humanMoney * fireGameManage.humanLife + fireGameManage.catLife * fireGameManage.catMoney) + (int) fireGameManage.distancePlayerToFire * 3 / 4;
            fireGameManage.xp += 50 + (fireGameManage.humanMoney * fireGameManage.humanLife + fireGameManage.catLife * fireGameManage.catMoney) / 12 + (int) fireGameManage.distancePlayerToFire / 10;
        } else if (fireGameManage.remainingTime >= 20) {
            fireGameManage.humanLife += 2;

            fireGameManage.humanCounterText.text = "Kurtarılan İnsanlar:" + fireGameManage.humanLife;
            bonusMoneyFromSavedLife += (100 + (fireGameManage.rescueLifeBonus * fireGameManage.humanLife));
            fireGameManage.money += (100 + (fireGameManage.rescueLifeBonus * fireGameManage.humanLife) + fireGameManage.humanMoney * fireGameManage.humanLife + fireGameManage.catLife * fireGameManage.catMoney) + (int) fireGameManage.distancePlayerToFire * 3 / 4;
            fireGameManage.xp += 50 + (fireGameManage.humanMoney * fireGameManage.humanLife + fireGameManage.catLife * fireGameManage.catMoney) / 12 + (int) fireGameManage.distancePlayerToFire / 10;
        } else if (fireGameManage.remainingTime >= 10) {
            fireGameManage.humanLife++;

            fireGameManage.humanCounterText.text = "Kurtarılan İnsanlar:" + fireGameManage.humanLife;
            bonusMoneyFromSavedLife += (100 + (fireGameManage.rescueLifeBonus * fireGameManage.humanLife));
            fireGameManage.money += (100 + (fireGameManage.rescueLifeBonus * fireGameManage.humanLife) + fireGameManage.humanMoney * fireGameManage.humanLife + fireGameManage.catLife * fireGameManage.catMoney) + (int) fireGameManage.distancePlayerToFire * 3 / 4;
            fireGameManage.xp += 50 + (fireGameManage.humanMoney * fireGameManage.humanLife + fireGameManage.catLife * fireGameManage.catMoney) / 12 + (int) fireGameManage.distancePlayerToFire / 10;
        } else {
            fireGameManage.money += 1000 + (int) fireGameManage.distancePlayerToFire / 5;
            fireGameManage.xp += 125;
        }

    }

    private void OnTriggerStay (Collider oyuncu) {
        if (oyuncu.CompareTag ("Player")) {
            if (fireGameManage.water > 0) {
                extinguishedTime += 1 * Time.deltaTime;

                if (fireGameManage.isHelicopter == true) {
                    extinguishTime--;
                    fireGameManage.isHelicopter = false;
                }
                if (fireGameManage.currentFireID == 0) {
                    if (fireGameManage.raidTime > 0) {
                        extinguishTime -= 2 * Time.deltaTime;
                    } else {
                        extinguishTime -= 1 * Time.deltaTime;
                    }
                    Debug.Log("Diegoikisesvar");
                    fireGameManage.firePlace1.fillAmount = extinguishTime / 10;
                }
                if (fireGameManage.currentFireID == 1) {
                    if (fireGameManage.raidTime > 0) {
                        extinguishTime -= 2 * Time.deltaTime;
                    } else {
                        extinguishTime -= 1 * Time.deltaTime;
                    }
                    Debug.Log("Diegoüçsesvar");
                    fireGameManage.firePlace2.fillAmount = extinguishTime / 10;
                }
                if (fireGameManage.currentFireID == 2) {
                    if (fireGameManage.raidTime > 0) {
                        extinguishTime -= 2 * Time.deltaTime;
                    } else {
                        extinguishTime -= 1 * Time.deltaTime;
                    }
                    Debug.Log("Diegodörtsesvar");
                    fireGameManage.firePlace3.fillAmount = extinguishTime / 10;
                }
                DecreaseWaterTank ();
                if (extinguishTime <= 0) {
                    if (saveOnce == false) {
                        if (fireID == 0) {
                            fireGameManage.firePlace1.enabled = false;
                            fireGameManage.navigationScript.getSecondMission = true;
                        }
                        if (fireID == 1) {
                            fireGameManage.firePlace2.enabled = false;
                            fireGameManage.navigationScript.getSecondMission = true;
                        }
                        if (fireID == 2) {
                            fireGameManage.firePlace3.enabled = false;
                            fireGameManage.navigationScript.getSecondMission = true;
                        }
                        fireGameManage.extinguishCounter++;
                        Destroy (this.gameObject);
                        if (fireGameManage.extinguishCounter == 3) {
                            MissionCompleteSaveFunction ();
                        }
                    }

                }
            }

        }
    }

    private void MissionCompleteSaveFunction () {
        SaveLifeControl ();
        XpAndMoneyIncrease ();
        SpentWaterAchievement ();
        SaveCatLifeAchievement ();
        SaveLifeAchievement ();
        ExtinguishFireAchievement ();
        CrashXWaterHydrantAchievement ();
        BonusMoneyFromBonusSavedAchievement ();
        ColdFireAchievement ();
        SpecificRemainingTimeAchievement ();
        GenerelMissionCompleteAchievement ();
        GenerelXpAchievement ();
        GenerelMoneyAchievement ();
        GeneralSkillAchievement ();
        SaveXpMoneyAndAchievements ();

        fireGameManage.missionCompleteScreen.SetActive (true);
        fireGameManage.gainedMoneyText.text = ":" + fireGameManage.money;
        fireGameManage.gainedXpText.text = ":" + fireGameManage.xp;
        fireGameManage.finishedTimeText.text = ":" + (int) fireGameManage.finishedTime;
        fireGameManage.usedHelicopterText.text = "Kullanılan Helikopter:" + fireGameManage.helicopterSkillCounter;
        fireGameManage.usedRaidText.text = "Kullanılan İş Birliği:" + fireGameManage.raidSkillCounter;
        fireGameManage.completeSavedCatsText.text = "Kurtarılan Kediler:" + fireGameManage.catLife;
        fireGameManage.completeUsedWaterText.text = "Kullanılan Su:" + (int) spentWater;
        fireGameManage.completeSavedPeopleText.text = "Kurtarılan İnsanlar:" + fireGameManage.humanLife;
        FireGameManager.baseExtinguishTime = 10;
        if (fireGameManage.remainingTime <= 0) {
            fireGameManage.headerFrame.color = Color.red;
            fireGameManage.missionFailedText.text = "Görev Başarısız";
        }

        Time.timeScale = 0f;

        saveOnce = true;
    }
    private void SpentWaterAchievement () {
        if (fireShopData.fireAchievementItem.unlockedXSpentWaterLevel <= fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel.Length - 1) {
            fireShopData.fireAchievementItem.xSpentWaterValue += (int) spentWater;
            Debug.Log ("SpentWaterAchievements Saved");
        }
    }

    private void SaveCatLifeAchievement () {
        if (fireShopData.fireAchievementItem.unlockedXSaveCatLevel <= fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel.Length - 1) {
            fireShopData.fireAchievementItem.xSaveCatValue += fireGameManage.catLife;
            Debug.Log ("FireAchievements Saved");
        }
    }
    private void SaveLifeAchievement () {
        if (fireShopData.fireAchievementItem.unlockedXSaveLifeFromFireLevel <= fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel.Length - 1) {
            fireShopData.fireAchievementItem.xSaveLifeFromFireValue += fireGameManage.catLife + fireGameManage.humanLife;
            Debug.Log ("FireAchievements Saved");
        }
    }

    private void ExtinguishFireAchievement () {
        if (fireShopData.fireAchievementItem.unlockedXFireExtinguishPlaceLevel <= fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel.Length - 1) {
            fireShopData.fireAchievementItem.xFireExtinguishPlaceValue += fireGameManage.extinguishCounter;
            Debug.Log ("ExtinguishAchieSaved");
        }

    }

    private void CrashXWaterHydrantAchievement () {
        if (fireShopData.fireAchievementItem.unlockedXFireExtinguishPlaceLevel <= fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel.Length - 1) {
            fireShopData.fireAchievementItem.xCrashWaterHydrantValue += crashWaterHydrant.accidentWaterHydrant;
            Debug.Log ("HydrantAchieveSaved");
        }
    }
    private void BonusMoneyFromBonusSavedAchievement () {
        if (fireShopData.fireAchievementItem.unlockedXFireExtinguishPlaceLevel <= fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel.Length - 1) {
            fireShopData.fireAchievementItem.gainXMoneyFromSaveLifeValue += bonusMoneyFromSavedLife;
            Debug.Log ("SavedLifeMoneyAchieveSaved");
        }
    }

    private void ColdFireAchievement () {
        if (fireShopData.fireAchievementItem.findTheColdFireValue == false) {
            if (coldFireScript.coldFire == true) {
                fireShopData.fireAchievementItem.findTheColdFireValue = true;
                Debug.Log ("FindColdFireAchieveSaved");
            }
        }

    }

    private void SpecificRemainingTimeAchievement () {
        if (fireShopData.fireAchievementItem.unlockedExtinguishFireBeforeDeadlineLevel <= fireShopData.fireAchievementItem.fireAchievementsUpgradeLevel.Length - 1) {
            if (fireGameManage.remainingTime >= 15) {
                fireShopData.fireAchievementItem.extinguishFireBeforeDeadlineValue++;
                Debug.Log ("RemainingTimeDeadlineAchieveSaved");
            }
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
            busShopData.generalAchievementItem.gainMoneyFromMissionValue += fireGameManage.money;
            Debug.Log ("GeneralMoneyAchieveSaved");
        }
    }
    private void GenerelXpAchievement () {
        if (busShopData.generalAchievementItem.unlockedGainXExperienceLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
            busShopData.generalAchievementItem.gainXExperienceValue += fireGameManage.xp;
            Debug.Log ("GeneralExperienceAchieveSaved");
        }
    }

    private void GeneralSkillAchievement () {
        if (busShopData.generalAchievementItem.unlockedXUseSkillLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
            busShopData.generalAchievementItem.xUseSkillValue += fireGameManage.raidSkillCounter + fireGameManage.helicopterSkillCounter;
        }
    }
    private void SaveXpMoneyAndAchievements () {

        ReadWriteAllRoles.WriteFireProp (fireShopData);
        ReadWriteAllRoles.WriteBusProp (busShopData);
    }

}