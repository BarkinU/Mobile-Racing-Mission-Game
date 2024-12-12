using RoleShopSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TaxiGameManager : MonoBehaviour {
    [Header ("Texts")]
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI xpText;
    public TextMeshProUGUI remainingTimeText;
    public TextMeshProUGUI gasolineText;
    public TextMeshProUGUI totalSatisfactionText;
    public TextMeshProUGUI crazyDriveTenurText;
    public TextMeshProUGUI cateringTenurText;

    [Header ("Locations Arrays")]
    public GameObject[] FirstCityCustomerLocations;
    public GameObject[] SecondCityCustomerLocations;

    [Header ("Script Objects")]
    private TaxiShopData taxiShopData;
    public GameData gameData;
    private PABLO RR;
    private Transform player;
    private GameObject customer;
    private GameObject customerFollower;

    [Header ("Upgrade Values")]
    public int longDistanceMeterPercent;
    public int longDistanceMeterValue;
    public int closeCustomerIncreaseValue;
    public int closeCustomerIncreasePercent;
    public int customerSatisfactionMoneyIncrease;
    public int gasolineReductionRate;
    private float gasolineValue = 0.8f;

    [Header ("Money & Values")]
    public int money;
    public int xp;
    public float remainingTime = 0.0f;
    public int randomCustomerNumber;
    public int customerCounter;
    public int satisfactionMoney;
    public float totalSatisfaction = 100;
    public float gasoline;
    public float noGasolineMoney;
    public int gasolineCapacity;
    private float satisfactionDecreaser;
    public bool isCustomerInCar=false;

    public TaxiList taxiList;

    [Header ("Car Settings")]
    private GameObject taxiCar;
    public GameObject firstStartPosition;
    public GameObject secondStartPosition;
    private GameObject startPosition;

    [Header ("SkillValues")]
    private int crazyDriveTenur;
    private int cateringTenur;
    public Button crazyDriveButton;
    public Button cateringButton;
    public int cateringSkillCounter;
    public int crazyDriveCurrentSkillCounter;
    private float crazyTimerBase = 20f;
    private float crazyTimer1 = 20f;
    private float crazyTimer2 = 20f;
    private float crazyTimer3 = 20f;
    private float crazyTimer4 = 20f;
    private float oldRpm;
    private float oldFStiffnessFL;
    private float oldFStiffnessFR;
    private float oldFStiffnessRR;
    private float oldFStiffnessRL;
    private float oldSStiffnessFL;
    private float oldSStiffnessFR;
    private float oldSStiffnessRR;
    private float oldSStiffnessRL;
    private float oldBrake;

    [Header ("MissionCompleteValues")]
    public GameObject missionCompleteScreen;
    public TextMeshProUGUI finishedTimeText;
    public TextMeshProUGUI gainedMoneyText;
    public TextMeshProUGUI gainedXpText;
    public TextMeshProUGUI completeSatisfactionText;
    public TextMeshProUGUI usedGasolineText;
    public TextMeshProUGUI completeTotalCustomerText;
    public TextMeshProUGUI usedCateringText;
    public TextMeshProUGUI usedCrazyDriveText,missionFailedText;
    public float usedGasoline;
    public float finishedTime;
    public int crazyDriveSkillCounter;
    public float distancePlayerCustomer;
    public NavigationScript navigationScript;
    public Image headerFrameImage;
    public GameObject CrazyDriveButtonEffect;
    public GameObject CateringButtonEffect;

    private void Awake () {

        SelectCitySpawn ();
        
    }

    private void SelectCitySpawn () {
        if (PlayerPrefs.GetInt ("isFirst") == 1) {
            startPosition = firstStartPosition;
        } else {
            startPosition = secondStartPosition;
        }

        Instantiate (taxiList.taxiVehicles[PlayerPrefs.GetInt ("pointer")], startPosition.transform.position, startPosition.transform.rotation);

        taxiCar = GameObject.FindGameObjectWithTag ("Player");

        customerFollower = Resources.Load ("CustomerFollower") as GameObject;
        RR = taxiCar.GetComponent<PABLO> ();
        customer = Resources.Load ("Customer") as GameObject;
    }
    private void Start () {
        
        GetUpgradeValues ();
        SpawnFirstCustomer ();
        

    }

    private void GetUpgradeValues () {
        taxiShopData = ReadWriteAllRoles.ReadTaxiProp (taxiShopData);

        longDistanceMeterPercent = taxiShopData.taxiRoleItems.taxiSkillUpgradeLevel[taxiShopData.taxiRoleItems.unlockedLongDistanceLevel].longDistanceValue;

        closeCustomerIncreaseValue = taxiShopData.taxiRoleItems.taxiSkillUpgradeLevel[taxiShopData.taxiRoleItems.unlockedCloseCustomerLevel].closeCustomerValue;

        customerSatisfactionMoneyIncrease = taxiShopData.taxiRoleItems.taxiSkillUpgradeLevel[taxiShopData.taxiRoleItems.unlockedCustomerSatisfactionLevel].customerSatisfactionValue;

        gasolineReductionRate = taxiShopData.taxiRoleItems.taxiSkillUpgradeLevel[taxiShopData.taxiRoleItems.unlockedGasolineReductionRateLevel].gasolineReductionRateValue;

        crazyDriveTenur = taxiShopData.taxiRoleItems.crazyDriveTenur;
        cateringTenur = taxiShopData.taxiRoleItems.cateringTenur;

        crazyDriveTenurText.text = crazyDriveCurrentSkillCounter + "/" + crazyDriveTenur;
        cateringTenurText.text = cateringSkillCounter + "/" + cateringTenur;

        oldRpm = RR.engineTorque;
        oldBrake = RR.brakeTorque;

        oldFStiffnessFR = RR.wheels[0].fStiffness;
        oldFStiffnessFL = RR.wheels[1].fStiffness;
        oldFStiffnessRR = RR.wheels[2].fStiffness;
        oldFStiffnessRL = RR.wheels[3].fStiffness;

        oldSStiffnessFL = RR.wheels[0].fStiffness;
        oldSStiffnessFR = RR.wheels[1].fStiffness;
        oldSStiffnessRR = RR.wheels[2].fStiffness;
        oldSStiffnessRL = RR.wheels[3].fStiffness;

        gasolineValue -= (gasolineValue * gasolineReductionRate) / 100;
        satisfactionDecreaser = -gasolineValue / 4;

        Time.timeScale = 1f;

    }

    private void FixedUpdate () {

        MoneyActuator ();
        CrazyDriveSkillActuator ();
        CoundDownTimer ();
    }

    private void MoneyActuator () {
        if (Input.GetKey (KeyCode.W)) {
            if (gasoline > 0) {
                gasoline -= gasolineValue * Time.fixedDeltaTime;
                usedGasoline += gasolineValue * Time.fixedDeltaTime;
            } else {
                if (isCustomerInCar == true) {
                    noGasolineMoney += gasolineValue * Time.fixedDeltaTime;
                    totalSatisfaction +=  satisfactionDecreaser*Time.deltaTime;
                }

            }
        }
        moneyText.text = " :  " + money;
        xpText.text = " :" + xp;
        gasolineText.text = "Benzin :" + (int) gasoline;

        if (remainingTime <-1) {
            totalSatisfaction -= 1 * Time.deltaTime;
        }
        totalSatisfactionText.text = "Memnuniyet :" + (int) totalSatisfaction;
    }
    private void CoundDownTimer () {
        finishedTime += Time.deltaTime;
        if (remainingTime <= 0) return;
        remainingTime -= Time.deltaTime;
        remainingTimeText.text = ":" + (int) remainingTime;
    }
    private void SpawnFirstCustomer () {
        if (PlayerPrefs.GetInt ("isFirst") == 1) {
            do {
                randomCustomerNumber = Random.Range (0, FirstCityCustomerLocations.Length-1);
                distancePlayerCustomer = Vector3.Distance (RR.gameObject.transform.position, FirstCityCustomerLocations[randomCustomerNumber].transform.position);
                Debug.Log (distancePlayerCustomer);

            } while (distancePlayerCustomer > closeCustomerIncreaseValue);

            GameObject.Instantiate (customer, FirstCityCustomerLocations[randomCustomerNumber].transform.position, Quaternion.identity);
            FirstCityCustomerLocations[randomCustomerNumber].transform.position += new Vector3 (0.0f, 200.0f, 0.0f);
            GameObject.Instantiate (customerFollower, FirstCityCustomerLocations[randomCustomerNumber].transform.position, Quaternion.Euler (90, 0, 0));
    
        } else {
            do {
                randomCustomerNumber = Random.Range (0, SecondCityCustomerLocations.Length-1);
                distancePlayerCustomer = Vector3.Distance (RR.gameObject.transform.position, SecondCityCustomerLocations[randomCustomerNumber].transform.position);
                Debug.Log (distancePlayerCustomer);

            } while (distancePlayerCustomer > closeCustomerIncreaseValue);

            GameObject.Instantiate (customer, SecondCityCustomerLocations[randomCustomerNumber].transform.position, Quaternion.identity);
            SecondCityCustomerLocations[randomCustomerNumber].transform.position += new Vector3 (0.0f, 200.0f, 0.0f);
            GameObject.Instantiate (customerFollower, SecondCityCustomerLocations[randomCustomerNumber].transform.position, Quaternion.Euler (90, 0, 0));
        
        }

    }

    public void CateringSkill () {
        cateringSkillCounter++;
        if (cateringSkillCounter <= cateringTenur) {
            StartCoroutine(CateringSkillShining());
            totalSatisfaction += 20;
            cateringTenurText.text = cateringSkillCounter + "/" + cateringTenur;

            if (cateringSkillCounter == cateringTenur) {
                cateringButton.interactable = false;
            }
        }
    }

    public void CrazyDriveSkill () {

        crazyDriveCurrentSkillCounter++;

        if (crazyDriveCurrentSkillCounter <= crazyDriveTenur) {
            StartCoroutine(CrazyDriveEffectShine());
            crazyDriveSkillCounter++;

            if (crazyDriveCurrentSkillCounter == 1) {

                RR.wheels[0].fStiffness = 1.7f;
                RR.wheels[1].fStiffness = 1.7f;
                RR.wheels[2].fStiffness = 1.7f;
                RR.wheels[3].fStiffness = 1.7f;

                RR.wheels[0].sStiffness = 1.7f;
                RR.wheels[1].sStiffness = 1.7f;
                RR.wheels[2].sStiffness = 1.7f;
                RR.wheels[3].sStiffness = 1.7f;

                RR.engineTorque = 400f;
                RR.brakeTorque = 1700f;

                crazyTimer1 = crazyTimerBase;
            } else if (crazyDriveCurrentSkillCounter == 2) {
                RR.wheels[0].fStiffness = 1.9f;
                RR.wheels[1].fStiffness = 1.9f;
                RR.wheels[2].fStiffness = 1.9f;
                RR.wheels[3].fStiffness = 1.9f;

                RR.wheels[0].sStiffness = 1.9f;
                RR.wheels[1].sStiffness = 1.9f;
                RR.wheels[2].sStiffness = 1.9f;
                RR.wheels[3].sStiffness = 1.9f;

                RR.engineTorque = 450f;
                RR.brakeTorque = 1900f;

                crazyTimer2 = crazyTimer1;
                crazyTimer1 = crazyTimerBase;
            } else if (crazyDriveCurrentSkillCounter == 3) {
                RR.wheels[0].fStiffness = 2.1f;
                RR.wheels[1].fStiffness = 2.1f;
                RR.wheels[2].fStiffness = 2.1f;
                RR.wheels[3].fStiffness = 2.1f;

                RR.wheels[0].sStiffness = 2.1f;
                RR.wheels[1].sStiffness = 2.1f;
                RR.wheels[2].sStiffness = 2.1f;
                RR.wheels[3].sStiffness = 2.1f;

                RR.engineTorque = 525f;
                RR.brakeTorque = 2100f;

                crazyTimer3 = crazyTimer2;
                crazyTimer2 = crazyTimer1;
                crazyTimer1 = crazyTimerBase;
            } else if (crazyDriveCurrentSkillCounter == 4) {
                RR.wheels[0].fStiffness = 2.4f;
                RR.wheels[1].fStiffness = 2.4f;
                RR.wheels[2].fStiffness = 2.4f;
                RR.wheels[3].fStiffness = 2.4f;

                RR.wheels[0].sStiffness = 2.4f;
                RR.wheels[1].sStiffness = 2.4f;
                RR.wheels[2].sStiffness = 2.4f;
                RR.wheels[3].sStiffness = 2.4f;

                RR.engineTorque = 625f;
                RR.brakeTorque = 2400f;

                crazyTimer4 = crazyTimer3;
                crazyTimer3 = crazyTimer2;
                crazyTimer2 = crazyTimer1;
                crazyTimer1 = crazyTimerBase;
            }

            if (crazyDriveCurrentSkillCounter == crazyDriveTenur) {
                crazyDriveButton.interactable = false;
            }

            crazyDriveTenurText.text = crazyDriveCurrentSkillCounter + "/" + crazyDriveTenur;
        }
    }

    private void CrazyDriveSkillActuator () {
        if (crazyDriveCurrentSkillCounter > 0) {
            if (crazyTimer1 > 0) {
                crazyTimer1 -= 1 * Time.deltaTime;
                if (crazyDriveCurrentSkillCounter > 1) {
                    if (crazyTimer2 > 0) {
                        crazyTimer2 -= 1 * Time.deltaTime;
                        if (crazyDriveCurrentSkillCounter > 2) {
                            if (crazyTimer3 > 0) {
                                crazyTimer3 -= 1 * Time.deltaTime;
                                if (crazyDriveCurrentSkillCounter > 3) {
                                    if (crazyTimer4 > 0) {
                                        crazyTimer4 -= 1 * Time.deltaTime;
                                    } else {
                                        RR.wheels[0].fStiffness = 2.1f;
                                        RR.wheels[1].fStiffness = 2.1f;
                                        RR.wheels[2].fStiffness = 2.1f;
                                        RR.wheels[3].fStiffness = 2.1f;

                                        RR.wheels[0].sStiffness = 2.1f;
                                        RR.wheels[1].sStiffness = 2.1f;
                                        RR.wheels[2].sStiffness = 2.1f;
                                        RR.wheels[3].sStiffness = 2.1f;

                                        RR.engineTorque = 525f;
                                        RR.brakeTorque = 2100f;

                                        crazyDriveCurrentSkillCounter--;
                                    }
                                }
                            } else {

                                RR.wheels[0].fStiffness = 1.9f;
                                RR.wheels[1].fStiffness = 1.9f;
                                RR.wheels[2].fStiffness = 1.9f;
                                RR.wheels[3].fStiffness = 1.9f;

                                RR.wheels[0].sStiffness = 1.9f;
                                RR.wheels[1].sStiffness = 1.9f;
                                RR.wheels[2].sStiffness = 1.9f;
                                RR.wheels[3].sStiffness = 1.9f;

                                RR.engineTorque = 450f;
                                RR.brakeTorque = 1900f;
                                crazyDriveCurrentSkillCounter--;
                            }
                        }
                    } else {
                        RR.wheels[0].fStiffness = 1.7f;
                        RR.wheels[1].fStiffness = 1.7f;
                        RR.wheels[2].fStiffness = 1.7f;
                        RR.wheels[3].fStiffness = 1.7f;

                        RR.wheels[0].sStiffness = 1.7f;
                        RR.wheels[1].sStiffness = 1.7f;
                        RR.wheels[2].sStiffness = 1.7f;
                        RR.wheels[3].sStiffness = 1.7f;

                        RR.engineTorque = 400f;
                        RR.brakeTorque = 1700f;

                        crazyDriveCurrentSkillCounter--;
                    }
                }
            } else {
                RR.wheels[0].fStiffness = oldFStiffnessFL;
                RR.wheels[1].fStiffness = oldFStiffnessFR;
                RR.wheels[2].fStiffness = oldFStiffnessRL;
                RR.wheels[3].fStiffness = oldFStiffnessRR;

                RR.wheels[0].sStiffness = oldSStiffnessFL;
                RR.wheels[1].sStiffness = oldSStiffnessFR;
                RR.wheels[2].sStiffness = oldSStiffnessRL;
                RR.wheels[3].sStiffness = oldSStiffnessRR;

                RR.engineTorque = oldRpm;
                RR.brakeTorque = oldBrake;

                crazyDriveCurrentSkillCounter--;
            }

        }
    }

    private void CloseCustomerCalculate () {
        int decreaseMeterForClose = (200 * closeCustomerIncreasePercent) / 100;
        closeCustomerIncreaseValue -= decreaseMeterForClose;
    }

    private void LongDistanceCalculate () {
        int increaseLongDistanceMeter = (200 * longDistanceMeterPercent) / 100;
        longDistanceMeterValue += increaseLongDistanceMeter;
    }

    public void AgainMission () {
        PlayerPrefs.SetInt ("rolePointer", 6);
        SceneLoader.Load (SceneLoader.Scene.RealScene);
        Time.timeScale = 1f;
    }

    IEnumerator CrazyDriveEffectShine()
    {
        CrazyDriveButtonEffect.SetActive(true);

        yield return  new WaitForSeconds(0.1f);

        CrazyDriveButtonEffect.SetActive(false);
    }

    IEnumerator CateringSkillShining()
    {
        CateringButtonEffect.SetActive(true);

        yield return  new WaitForSeconds(0.1f);

        CateringButtonEffect.SetActive(false);
    }

}