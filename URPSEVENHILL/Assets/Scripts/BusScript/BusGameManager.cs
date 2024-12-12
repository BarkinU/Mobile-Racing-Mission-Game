using RoleShopSystem;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class BusGameManager : MonoBehaviour {
    [Header ("Script Objects")]
    private BusShopData busShopData;
    private PABLO RR;

    [Header ("Start Rotation Point")]
    public GameObject spawnLocation1;
    public GameObject spawnLocation2;
    private GameObject spawnLocation;
    private GameObject stationFollower;
    private GameObject station;
    public GameObject secretStation;
    public GameObject lastStation;
    public GameObject secondLastStation;

    [Header ("Bus Rotation Selection")]
    public GameObject busRota1;
    public GameObject busRota2;
    public GameObject busRota3;
    public GameObject busRota4;

    public GameObject secondBusRota1;
    public GameObject secondBusRota2;
    public GameObject secondBusRota3;
    public GameObject secondBusRota4;
    public int busRota1Control = 0;
    public int busRota2Control = 0;
    public int busRota3Control = 0;
    public int busRota4Control = 0;

    [Header ("Texts")]
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI xpText;
    public TextMeshProUGUI remainingTimeText;
    public TextMeshProUGUI passengerText;
    public TextMeshProUGUI withBaggagePassengerText;
    public TextMeshProUGUI droppingPassengerText;
    public TextMeshProUGUI busCapacityText;
    public TextMeshProUGUI baggageCounterText;
    public TextMeshProUGUI discountTenurText;
    public TextMeshProUGUI arrangementTenurText,missionFailedText;

    [Header ("Passengers")]
    public float remainingTime;
    public int totalPassengerCounter;
    public int withBaggagePassengerCounter;
    public int passengerCounter;
    public int droppingPassenger = 0;

    [Header ("Money & Xp")]
    public int baggageMoney;
    public int money = 0;
    public int xp = 0;

    [Header ("Upgrade Values")]
    public int busCapacity;
    public int takeFastPassPercent;
    public int furnishedPassengerPercent;
    public int ComfortDrivePercent;

    [Header ("Upgrade Values")]
    public BusList busList;

    [Header ("Car Settings")]
    private GameObject busCar;

    public GameObject firstStartPosition;
    public GameObject secondStartPosition;
    private GameObject startPosition;

    [Range (0, 5)] public int busStationCounter;

    [Header ("MissionValues&SkillValues")]
    public int loadPassenger;
    public int dropPassenger;
    public int moneyFromPassengers = 100;
    private float discountTimerBase = 100f;
    private float discountTimer1 = 100f;
    private float discountTimer2 = 100f;
    private float discountTimer3 = 100f;
    private float discountTimer4 = 100f;
    public int discountCurrentSkillCounter;
    public int discountTenur;
    private int discountTempMoney1;
    private int discountTempMoney2;
    private int discountTempMoney3;
    private int discountTempMoney4;
    public Button discountSkillButton;
    public int passengerOnBoardCounter=1;
    public int arrangementSkillCounter;
    private int arrangementTenur;
    public Button arrangementButton;
    public int baggageCapacity;
    public int baggageCounter;
    public float withBaggagePassengerChance = 100;

    [Header("MissionCompleteValues")]
    public GameObject missionCompleteScreen;
    public TextMeshProUGUI finishedTimeText;
    public TextMeshProUGUI gainedMoneyText;
    public TextMeshProUGUI gainedXpText;
    public TextMeshProUGUI completePassengerText;
    public TextMeshProUGUI completeWithBaggagePassengerText;
    public TextMeshProUGUI completeDroppingText;
    public TextMeshProUGUI usedDiscountText;
    public TextMeshProUGUI usedArrangementText;
    public TextMeshProUGUI withBaggagePassengerMoneyText;
    public TextMeshProUGUI passengerMoneyText;
    public TextMeshProUGUI droppedPassengerText;
    public TextMeshProUGUI currentDroppedBaggage;
    public TextMeshProUGUI currentAddBaggage;
    public GameObject DiscountButtonEffect;
    public GameObject ArrangementButtonEffect;
    public Image finishedFrame;
    public bool isArrange;

    public float finishTime;

    //BigMapMarker
    public GameObject missionMarker;
    public int discountSkillCounter;
    public NavigationScript navigationScript;

    private void Awake () {
        
        int randomRota = Random.Range (1, 4);
        if (PlayerPrefs.GetInt ("isFirst") == 1) {
            startPosition = firstStartPosition;
            spawnLocation=spawnLocation1;
            
            switch (randomRota) {
            case 1:
                busRota1.SetActive (true);
                PlayerPrefs.SetInt ("bus1", 1);
                remainingTime=350;
                break;
            case 2:
                busRota2.SetActive (true);
                PlayerPrefs.SetInt ("bus2", 1);
                remainingTime=350;
                break;
            case 3:
                busRota3.SetActive (true);
                PlayerPrefs.SetInt ("bus3", 1);
                remainingTime=350;
                break;
            case 4:
                busRota4.SetActive (true);
                PlayerPrefs.SetInt ("bus4", 1);
                remainingTime=350;
                break;
        }
        } else {
            startPosition = secondStartPosition;
            spawnLocation=spawnLocation2;

            switch (randomRota) {
            case 1:
                secondBusRota1.SetActive (true);
                remainingTime=350;
                break;
            case 2:
                secondBusRota2.SetActive (true);
                remainingTime=350;
                break;
            case 3:
                secondBusRota3.SetActive (true);
                remainingTime=350;
                break;
            case 4:
                secondBusRota4.SetActive (true);
                remainingTime=350;
                break;
        }
        }

        

        
        Instantiate (busList.busVehicles[PlayerPrefs.GetInt ("pointer")], startPosition.transform.position, startPosition.transform.rotation);

        busCar = GameObject.FindGameObjectWithTag ("Player");
        RR = busCar.GetComponent<PABLO> ();

        station = Resources.Load ("BusStations") as GameObject;
        stationFollower = Resources.Load ("BusStationsFollower") as GameObject;

        loadPassenger = Random.Range (2, 8);
        dropPassenger = Random.Range (1, 6);

        
        SpawnFirstPlane ();

    }

    private void Start () {

        GetFile();
        

    }
    private void Update () {
        
        TextActuator ();
        DiscountActuator ();

    }

    private void TextActuator () {
        moneyText.text = ":  " + money;
        xpText.text = ":" + xp;
        passengerText.text = "Binen Yolcular:" + passengerCounter;
        withBaggagePassengerText.text = "Bagajlı Yolcular:" + withBaggagePassengerCounter;
        busCapacityText.text = "Kapasite:" + totalPassengerCounter + "/" + busCapacity;
        droppingPassengerText.text = "İnen Yolcular:" + droppingPassenger;
        baggageCounterText.text="Bagaj Kapasitesi:"+baggageCounter+"/"+baggageCapacity;
        remainingTimeText.text=":"+(int)remainingTime;

        if(remainingTime>0)
        {
            remainingTime-=1*Time.deltaTime;
            finishTime+=1*Time.deltaTime;
            
        }
        
    }

    private void RotaControl () {
        busRota1Control = PlayerPrefs.GetInt ("bus1");
        busRota2Control = PlayerPrefs.GetInt ("bus2");
        busRota3Control = PlayerPrefs.GetInt ("bus3");
        busRota4Control = PlayerPrefs.GetInt ("bus4");
    }

    private void SpawnFirstPlane () {
        Time.timeScale = 1f;
        GameObject.Instantiate (station, spawnLocation.transform.position, Quaternion.identity);
    }

    private void GetFile()
    {
        busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);

        busCapacity = busShopData.busRoleItems.busSkillUpgradeLevel[busShopData.busRoleItems.unlockedCapacityLevel].capacityValue;

        takeFastPassPercent = busShopData.busRoleItems.busSkillUpgradeLevel[busShopData.busRoleItems.unlockedFastPassLevel].fastPassValue;

        furnishedPassengerPercent = busShopData.busRoleItems.busSkillUpgradeLevel[busShopData.busRoleItems.unlockedChancePassLevel].chancePassValue;

        ComfortDrivePercent = busShopData.busRoleItems.busSkillUpgradeLevel[busShopData.busRoleItems.unlockedComfortDrivePriceLevel].comfortDrivePriceValue;

        discountTenur=busShopData.busRoleItems.discountTenur;
        arrangementTenur=busShopData.busRoleItems.arrangementTenur;

        discountTenurText.text=discountCurrentSkillCounter+"/"+discountTenur;
        arrangementTenurText.text=arrangementSkillCounter+"/"+arrangementTenur;
    }

    public void DiscountSkill () {
        discountCurrentSkillCounter++;

        if (discountCurrentSkillCounter <= discountTenur) {
            discountSkillCounter++;
            StartCoroutine(DiscountEffectShine());
            if (discountCurrentSkillCounter == 1) {
                loadPassenger = Random.Range (3, 9);
                dropPassenger = Random.Range (2, 7);
                passengerOnBoardCounter = 2;
                withBaggagePassengerChance*=5/4;

                discountTimer1 = discountTimerBase;

                discountTempMoney1 = moneyFromPassengers;
                moneyFromPassengers -= moneyFromPassengers * 25 / 100;
                discountTempMoney2 = moneyFromPassengers;
            } else if (discountCurrentSkillCounter == 2) {
                loadPassenger = Random.Range (6, 16);
                dropPassenger = Random.Range (4, 10);
                passengerOnBoardCounter = 3;
                withBaggagePassengerChance*=3/2;

                //değerler elle girilecek
                moneyFromPassengers -= moneyFromPassengers * 25 / 100;
                discountTempMoney3 = moneyFromPassengers;

                discountTimer2 = discountTimer1;

                discountTimer1 = discountTimerBase;
            } else if (discountCurrentSkillCounter == 3) {
                loadPassenger = Random.Range (12, 24);
                dropPassenger = Random.Range (10, 16);
                passengerOnBoardCounter = 4;
                

                moneyFromPassengers -= moneyFromPassengers * 25 / 100;
                discountTempMoney4 = moneyFromPassengers;

                discountTimer3 = discountTimer2;
                discountTimer2 = discountTimer1;

                discountTimer1 = discountTimerBase;
            } else if (discountCurrentSkillCounter == 4) {
                loadPassenger = Random.Range (16, 30);
                dropPassenger = Random.Range (14, 25);
                passengerOnBoardCounter = 5;
                

                moneyFromPassengers -= moneyFromPassengers * 25 / 100;

                discountTimer4 = discountTimer3;
                discountTimer3 = discountTimer2;
                discountTimer2 = discountTimer1;

                discountTimer1 = discountTimerBase;
            }
            
            discountTenurText.text=discountCurrentSkillCounter+"/"+discountTenur;

            if (discountCurrentSkillCounter == discountTenur) {
                discountSkillButton.interactable = false;
            }
        }
    }

    private void DiscountActuator () {
        if (discountCurrentSkillCounter > 0) {
            if (discountTimer1 > 0) {
                discountTimer1 -= 1 * Time.deltaTime;
                if (discountCurrentSkillCounter > 1) {
                    if (discountTimer2 > 0) {
                        discountTimer2 -= 1 * Time.deltaTime;
                        if (discountCurrentSkillCounter > 2) {
                            if (discountTimer3 > 0) {
                                discountTimer3 -= 1 * Time.deltaTime;
                                if (discountCurrentSkillCounter > 3) {
                                    if (discountTimer4 > 0) {
                                        discountTimer4 -= 1 * Time.deltaTime;
                                    } else {
                                        loadPassenger = Random.Range (12, 24);
                                        dropPassenger = Random.Range (10, 16);
                                        passengerOnBoardCounter = 4;
                                        moneyFromPassengers = discountTempMoney4;

                                        discountCurrentSkillCounter--;
                                    }
                                }
                            } else {
                                loadPassenger = Random.Range (6, 16);
                                dropPassenger = Random.Range (4, 10);
                                passengerOnBoardCounter = 3;
                                moneyFromPassengers = discountTempMoney3;

                                discountCurrentSkillCounter--;
                            }
                        }
                    } else {
                        loadPassenger = Random.Range (3, 9);
                        dropPassenger = Random.Range (2, 7);
                        passengerOnBoardCounter = 2;
                        withBaggagePassengerChance=withBaggagePassengerChance*2/3;
                        moneyFromPassengers = discountTempMoney2;

                        discountCurrentSkillCounter--;
                    }
                }
            } else {
                loadPassenger = Random.Range (2, 8);
                dropPassenger = Random.Range (1, 6);
                passengerOnBoardCounter = 1;
                withBaggagePassengerChance=withBaggagePassengerChance*4/5;

                moneyFromPassengers = discountTempMoney1;

                discountCurrentSkillCounter--;
            }

        }
    }

    public void ArrangementSkill () {
        arrangementSkillCounter++;

        if (arrangementSkillCounter <= arrangementTenur) {
            StartCoroutine(ArrangementSkillShining());
            int decreaseBaggage=baggageCounter/4;
            baggageCounter-=decreaseBaggage;

            arrangementTenurText.text=arrangementSkillCounter+"/"+arrangementTenur;

            if (arrangementSkillCounter == discountTenur) {
                arrangementButton.interactable = false;
            }
        }
    }

    public void AgainMission () {
        PlayerPrefs.SetInt ("rolePointer", 1);
        SceneLoader.Load (SceneLoader.Scene.RealScene);
    }


    IEnumerator DiscountEffectShine()
    {
        DiscountButtonEffect.SetActive(true);

        yield return  new WaitForSeconds(0.1f);

        DiscountButtonEffect.SetActive(false);
    }

    IEnumerator ArrangementSkillShining()
    {
        ArrangementButtonEffect.SetActive(true);

        yield return  new WaitForSeconds(0.1f);

        ArrangementButtonEffect.SetActive(false);
    }
}