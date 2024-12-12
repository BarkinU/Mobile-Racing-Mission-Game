using System.Collections;
using System.Collections.Generic;
using RoleShopSystem;
using UnityEngine;

public class TaxiCompleteMission : MonoBehaviour {
    [Header ("Script Objects")]
    public GameObject customer;
    public TaxiGameManager taxiGameManage;
    private FindFamousCustomer findFamous;
    private SignBoardAccident signBoardAccident;
    private NoAccidentBus noAccidentTaxi;
    TaxiShopData taxiShopData;
    BusShopData busShopData;
    private GameObject[] firstCityCustomerLocations;
    private GameObject[] secondCityCustomerLocations;
    private float currentTime = 5.0f;
    public GameObject customerIndicator;
    public int randomCustomerNumber;
    public GameObject RR;
    private float distancePlayerCustomer = 0f;
    private bool saveOnce;
    private int accidentEffect;

    private void Awake () {

        RR = GameObject.FindGameObjectWithTag ("Player").gameObject;
        noAccidentTaxi = RR.GetComponent<NoAccidentBus> ();
        taxiGameManage = GameObject.FindObjectOfType<TaxiGameManager> ();
        findFamous = GameObject.FindObjectOfType<FindFamousCustomer> ();
        signBoardAccident = GameObject.FindObjectOfType<SignBoardAccident> ();
        firstCityCustomerLocations = taxiGameManage.FirstCityCustomerLocations;
        secondCityCustomerLocations = taxiGameManage.SecondCityCustomerLocations;
    }
    private void Start () {
        taxiShopData = ReadWriteAllRoles.ReadTaxiProp (taxiShopData);
        busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);

    }

    public void SpawnCustomer () {
        if (PlayerPrefs.GetInt ("isFirst") == 1) {
            do {
                randomCustomerNumber = Random.Range (0, firstCityCustomerLocations.Length);
                distancePlayerCustomer = Vector3.Distance (RR.gameObject.transform.position, firstCityCustomerLocations[randomCustomerNumber].transform.position);
                Debug.Log (distancePlayerCustomer);

            } while (distancePlayerCustomer > taxiGameManage.closeCustomerIncreaseValue);

            GameObject.Instantiate (customer, firstCityCustomerLocations[randomCustomerNumber].transform.position, Quaternion.identity);
            firstCityCustomerLocations[randomCustomerNumber].transform.position += new Vector3 (0.0f, 50.0f, 0.0f);
            GameObject.Instantiate (customerIndicator, firstCityCustomerLocations[randomCustomerNumber].transform.position, Quaternion.Euler (90, 0, 0));
            taxiGameManage.customerCounter++;
        } else {
            do {
                randomCustomerNumber = Random.Range (0, secondCityCustomerLocations.Length);
                distancePlayerCustomer = Vector3.Distance (RR.gameObject.transform.position, secondCityCustomerLocations[randomCustomerNumber].transform.position);
                Debug.Log (distancePlayerCustomer);

            } while (distancePlayerCustomer > taxiGameManage.closeCustomerIncreaseValue);

            GameObject.Instantiate (customer, secondCityCustomerLocations[randomCustomerNumber].transform.position, Quaternion.identity);
            secondCityCustomerLocations[randomCustomerNumber].transform.position += new Vector3 (0.0f, 50.0f, 0.0f);
            GameObject.Instantiate (customerIndicator, secondCityCustomerLocations[randomCustomerNumber].transform.position, Quaternion.Euler (90, 0, 0));
            taxiGameManage.customerCounter++;
        }

    }


    private void MissionRewards () {

        accidentEffect = ((noAccidentTaxi.accidentSatisfaction / 2));
        taxiGameManage.satisfactionMoney = (int) taxiGameManage.totalSatisfaction * 2 + 50 + (int) taxiGameManage.remainingTime * 2 + accidentEffect;
        taxiGameManage.satisfactionMoney += (taxiGameManage.satisfactionMoney * taxiGameManage.customerSatisfactionMoneyIncrease) / 100;
        taxiGameManage.customerCounter++;

        taxiGameManage.xp += taxiGameManage.satisfactionMoney / 4 + ((int)distancePlayerCustomer+(int)taxiGameManage.distancePlayerCustomer)/3;
        taxiGameManage.money += 500 + taxiGameManage.satisfactionMoney+(int)distancePlayerCustomer*5+(int)taxiGameManage.distancePlayerCustomer*3;
    }
    private void CustomerCounterAchievement () {
        if (taxiShopData.taxiAchievementItem.unlockedUse4TimesCrazyOneGameLevel <= taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel.Length - 1) {
            if(taxiGameManage.crazyDriveSkillCounter==4)
        {
            taxiShopData.taxiAchievementItem.use4TimesCrazyOneGameValue++;

            Debug.Log ("RepeatedlyAchieveSaved");
        }
            
        }
    }
    private void HundredSatisfactionAchievement () {
        if (taxiShopData.taxiAchievementItem.unlockedHundredPercentSatisfactionLevel <= taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel.Length - 1) {
            if (taxiGameManage.totalSatisfaction >= 100) {
                taxiShopData.taxiAchievementItem.hundredPercentSatisfactionValue++;
                Debug.Log ("HundredAchieveSaved");
            }
        }
    }
    private void FindFamousAchievement () {
        if (taxiShopData.taxiAchievementItem.findTheFamousCustomerValue == true) {
            if (findFamous.famousCustomer == true) {
                taxiShopData.taxiAchievementItem.findTheFamousCustomerValue = true;
                Debug.Log ("FindFamousAchieveSaved");
            }
        }

    }

    private void SignBoardAccidentAchievement () {
        if (taxiShopData.taxiAchievementItem.unlockedCrashSignBoardLevel <= taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel.Length - 1) {
            taxiShopData.taxiAchievementItem.crashSignBoardValue += signBoardAccident.signBoardAccidentNumber;
            Debug.Log ("SignBoardAchieveSaved");
        }
    }

    private void NoAccidentAchievement () {
        if (taxiShopData.taxiAchievementItem.unlockedXNoAccidentLevel <= taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel.Length - 1) {
            if (noAccidentTaxi.accident == false) {
                taxiShopData.taxiAchievementItem.xNoAccidentValue++;
                Debug.Log ("AccidentAchieveSaved");
            }
        }
    }

    private void SatisfactionMoneyAchievement () {
        if (taxiShopData.taxiAchievementItem.unlockedGainXMoneyFromSatisfactionLevel <= taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel.Length - 1) {
            taxiShopData.taxiAchievementItem.gainXMoneyFromSatisfactionValue += taxiGameManage.satisfactionMoney;
            Debug.Log ("SatisfactionAchieveSaved");
        }
    }

    private void NoGasolineMoneyAchievement () {
        if (taxiShopData.taxiAchievementItem.unlockedNoGasolineGiveMoneyLevel <= taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel.Length - 1) {
            taxiShopData.taxiAchievementItem.noGasolineGiveMoneyValue += (int) taxiGameManage.noGasolineMoney;
            Debug.Log ("NoGasolineAchieveSaved");
        }
    }

    private void FirstCityAchievement () {
        if (taxiShopData.taxiAchievementItem.unlockedFirstCityTravelLevel <= taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel.Length - 1) {
            if (PlayerPrefs.GetInt ("isFirst") == 1) {
                taxiShopData.taxiAchievementItem.firstCityTravelValue++;
                Debug.Log ("FirstCityTravelAchieveSaved");
            }
        }
    }

    private void UseXCateringAchievement () {

        if (taxiShopData.taxiAchievementItem.unlockedUseXCateringLevel <= taxiShopData.taxiAchievementItem.taxiAchievementsUpgradeLevel.Length - 1) {

            taxiShopData.taxiAchievementItem.useXCateringValue+=taxiGameManage.cateringSkillCounter;
            ReadWriteAllRoles.WriteTaxiProp (taxiShopData);

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
            busShopData.generalAchievementItem.gainMoneyFromMissionValue += taxiGameManage.money;
            Debug.Log ("GeneralMoneyAchieveSaved");
        }
    }
    private void GenerelXpAchievement () {
        if (busShopData.generalAchievementItem.unlockedGainXExperienceLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
            busShopData.generalAchievementItem.gainXExperienceValue += taxiGameManage.xp;
            Debug.Log ("GeneralExperienceAchieveSaved");
        }
    }

    private void GeneralSkillAchievement () {
        if (busShopData.generalAchievementItem.unlockedXUseSkillLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
            busShopData.generalAchievementItem.xUseSkillValue += taxiGameManage.cateringSkillCounter + taxiGameManage.crazyDriveCurrentSkillCounter;
        }
    }

    private void XpMoneyAchievementsSave () {

        taxiGameManage.gameData.totalMoney += taxiGameManage.money;
        taxiGameManage.gameData.totalXp += taxiGameManage.xp;

        ReadWriteAllRoles.WriteGameProp (taxiGameManage.gameData);
        ReadWriteAllRoles.WriteBusProp (busShopData);
        ReadWriteAllRoles.WriteTaxiProp (taxiShopData);
    }

    private void OnTriggerStay (Collider oyuncu) {
        if (oyuncu.tag == "Player") {
            currentTime -= 1 * Time.deltaTime;
            if (currentTime <= 0) {
                if (saveOnce == false) {
                    MissionSaveScreen ();
                }

            }
        }
    }

    private void MissionSaveScreen () {
        MissionRewards ();
        CustomerCounterAchievement ();
        HundredSatisfactionAchievement ();
        FindFamousAchievement ();
        SignBoardAccidentAchievement ();
        NoAccidentAchievement ();
        SatisfactionMoneyAchievement ();
        NoGasolineMoneyAchievement ();
        FirstCityAchievement ();
        UseXCateringAchievement ();
        GenerelMissionCompleteAchievement ();
        GenerelMoneyAchievement ();
        GenerelXpAchievement ();
        GeneralSkillAchievement ();
        XpMoneyAchievementsSave ();

        taxiGameManage.missionCompleteScreen.SetActive (true);
        taxiGameManage.gainedMoneyText.text = ":" + taxiGameManage.money;
        taxiGameManage.gainedXpText.text = ":" + taxiGameManage.xp;
        taxiGameManage.finishedTimeText.text = ":" + (int) taxiGameManage.finishedTime;
        taxiGameManage.usedGasolineText.text = "Kullanılan Benzin:" + (int) taxiGameManage.usedGasoline;
        taxiGameManage.completeSatisfactionText.text = "Toplam Memnuniyet :" + (int) taxiGameManage.totalSatisfaction;
        taxiGameManage.completeTotalCustomerText.text = "Toplam Yolcu :" + taxiGameManage.customerCounter;
        taxiGameManage.usedCateringText.text = "Kullanılan İkram :" + taxiGameManage.cateringSkillCounter;
        taxiGameManage.usedCrazyDriveText.text = "Kullanılan Çılgın Sürüş :" + taxiGameManage.crazyDriveSkillCounter;

        if(taxiGameManage.remainingTime<=0)
        {
            taxiGameManage.headerFrameImage.color=Color.red;
            taxiGameManage.missionFailedText.text="Görev Başarısız";
        }

        Destroy (GameObject.FindGameObjectWithTag ("CustomerDestinationFollower"));
        Destroy (this.gameObject);
        saveOnce = true;
        Time.timeScale = 0f;
    }

}