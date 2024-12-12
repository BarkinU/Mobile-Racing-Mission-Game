using System.Collections;
using RoleShopSystem;
using UnityEngine;

public class BusMission : MonoBehaviour {
    [Header ("Script Objects")]
    private GameObject busPlane;
    public BusGameManager busGameManage;
    public GameObject[] busSpawnLocations;
    public float waitTime = 10.0f;
    public int xpIncreaseRate;

    private bool isLoaded = false;
    private float tempWait;
    private float dropTempWait;
    private int baseComfort = 5;
    private float reset = 0.3f;

    private void Awake () {
        busGameManage = GameObject.FindObjectOfType<BusGameManager> ();
        busPlane = Resources.Load ("BusStations") as GameObject;
    }
    private void Start () {
        busSpawnLocations = GameObject.FindGameObjectsWithTag ("busMissionSpawn");
        SkillValuesCalculate ();
        TakeFastPassCalculus ();

    }

    private void SkillValuesCalculate () {
        busGameManage.withBaggagePassengerChance += (busGameManage.withBaggagePassengerChance * busGameManage.furnishedPassengerPercent) / 100;
        xpIncreaseRate = (baseComfort * busGameManage.ComfortDrivePercent) / 100;

    }

    private void DropThePassenger () {
        int DropBaggageChance = Random.Range (0, 1001);
        if (busGameManage.totalPassengerCounter > 15) {
            if (busGameManage.dropPassenger > 0) {
                if (waitTime <= dropTempWait - 1) {

                    busGameManage.totalPassengerCounter -= busGameManage.passengerOnBoardCounter;
                    busGameManage.droppingPassenger -= busGameManage.passengerOnBoardCounter;
                    dropTempWait--;
                    busGameManage.dropPassenger -= busGameManage.passengerOnBoardCounter;

                    if (DropBaggageChance <= busGameManage.withBaggagePassengerChance) {
                        if (busGameManage.baggageCounter > 0) {
                            busGameManage.baggageCounter -= busGameManage.passengerOnBoardCounter * 4;
                        }
                        if(busGameManage.baggageCounter<0)
                        {
                            busGameManage.baggageCounter=0;
                        }
                        StartCoroutine(CoroutineBaggageDropped());
                    }
                    StartCoroutine (CoroutineDropped ());

                }
            }

        }
    }
    private void TakeFastPassCalculus () {
        float decwaitTime = (waitTime * busGameManage.takeFastPassPercent) / 100;
        waitTime -= decwaitTime;
    }

    private void PassengerLoading () {

        int totalWithBaggagePassengerChance = Random.Range (0, 1001);
        if (busGameManage.totalPassengerCounter < busGameManage.busCapacity) {

            if (busGameManage.loadPassenger > 0) {
                if (waitTime <= tempWait - 1) {
                    if (busGameManage.baggageCounter < busGameManage.baggageCapacity) {
                        if (totalWithBaggagePassengerChance <= busGameManage.withBaggagePassengerChance) {

                            busGameManage.money += busGameManage.moneyFromPassengers;
                            busGameManage.baggageMoney += busGameManage.moneyFromPassengers;
                            StartCoroutine (CoroutineBaggage ());
                            StartCoroutine(CoroutineBaggageAdd());
                            XpAndMoneyIncrease ();
                            busGameManage.withBaggagePassengerCounter += busGameManage.passengerOnBoardCounter;
                            busGameManage.baggageCounter += busGameManage.passengerOnBoardCounter * 4;
                            if (busGameManage.isArrange == false) {
                                busGameManage.arrangementButton.interactable = true;
                                busGameManage.isArrange = true;
                            }
                            if (busGameManage.baggageCounter > busGameManage.baggageCapacity) {
                                busGameManage.baggageCounter = busGameManage.baggageCapacity;
                            }
                            
                            Debug.Log ("Lucky Day");
                        } else {
                            XpAndMoneyIncrease ();
                            busGameManage.passengerCounter += busGameManage.passengerOnBoardCounter;
                            StartCoroutine (CoroutinePassenger ());
                        }
                    } else {
                        XpAndMoneyIncrease ();
                        busGameManage.passengerCounter += busGameManage.passengerOnBoardCounter;
                        StartCoroutine (CoroutinePassenger ());

                    }
                    busGameManage.totalPassengerCounter += busGameManage.passengerOnBoardCounter;
                    tempWait--;
                    busGameManage.loadPassenger -= busGameManage.passengerOnBoardCounter;

                    if (busGameManage.totalPassengerCounter > busGameManage.busCapacity) {
                        busGameManage.totalPassengerCounter=busGameManage.busCapacity;
                    }

                }
            } else {
                SpawnBusPlane ();
                Destroy (this.gameObject);
            }

        }else if(busGameManage.dropPassenger <= 0){
            SpawnBusPlane ();
                Destroy (this.gameObject);
        }

    }

    private void XpAndMoneyIncrease () {
        busGameManage.xp += (6 + xpIncreaseRate);
        busGameManage.money += 32;
    }

    private void SpawnBusPlane () {
        if (busGameManage.busStationCounter != busSpawnLocations.Length - 1) {
            int randomValueForDroppedPassenger = Random.Range (2, 5);
            busGameManage.busStationCounter++;
            if (busGameManage.discountCurrentSkillCounter == 0) {
                busGameManage.loadPassenger = Random.Range (2, 8);
                if (busGameManage.busStationCounter % randomValueForDroppedPassenger == 0) {
                    busGameManage.dropPassenger = Random.Range (3, 9);
                } else {
                    busGameManage.dropPassenger = Random.Range (1, 6);
                }

                busGameManage.passengerOnBoardCounter = 1;
            } else if (busGameManage.discountCurrentSkillCounter == 1) {
                busGameManage.loadPassenger = Random.Range (4, 12);
                if (busGameManage.busStationCounter % randomValueForDroppedPassenger == 0) {
                    busGameManage.dropPassenger = Random.Range (4, 12);
                } else {
                    busGameManage.dropPassenger = Random.Range (2, 7);
                }

                busGameManage.passengerOnBoardCounter = 2;
            } else if (busGameManage.discountCurrentSkillCounter == 2) {
                busGameManage.loadPassenger = Random.Range (6, 18);
                if (busGameManage.busStationCounter % randomValueForDroppedPassenger == 0) {
                    busGameManage.dropPassenger = Random.Range (6, 14);
                } else {
                    busGameManage.dropPassenger = Random.Range (4, 10);
                }

                busGameManage.passengerOnBoardCounter = 3;
            } else if (busGameManage.discountCurrentSkillCounter == 3) {
                busGameManage.loadPassenger = Random.Range (12, 24);
                if (busGameManage.busStationCounter % randomValueForDroppedPassenger == 0) {
                    busGameManage.dropPassenger = Random.Range (12, 18);
                } else {
                    busGameManage.dropPassenger = Random.Range (10, 16);
                }

                busGameManage.passengerOnBoardCounter = 4;
            } else if (busGameManage.discountCurrentSkillCounter == 4) {
                busGameManage.loadPassenger = Random.Range (16, 32);
                if (busGameManage.busStationCounter % randomValueForDroppedPassenger == 0) {
                    busGameManage.dropPassenger = Random.Range (16, 28);
                } else {
                    busGameManage.dropPassenger = Random.Range (14, 25);
                }

                busGameManage.passengerOnBoardCounter = 5;
            }

            GameObject.Instantiate (busPlane, busSpawnLocations[busGameManage.busStationCounter].transform.position, Quaternion.identity);
            busSpawnLocations[busGameManage.busStationCounter].transform.position += new Vector3 (0.0f, 20.0f, 0.0f);

            busGameManage.withBaggagePassengerMoneyText.text = " ";
            busGameManage.passengerMoneyText.text = " ";
            busGameManage.droppedPassengerText.text = " ";
            busGameManage.currentAddBaggage.text = " ";
            busGameManage.currentDroppedBaggage.text = " ";

            busGameManage.navigationScript.getSecondMission = true;
        } else {
            if (PlayerPrefs.GetInt ("isFirst") == 1) {
                busGameManage.navigationScript.getSecondMission = true;
                busGameManage.lastStation.SetActive (true);
                busGameManage.withBaggagePassengerMoneyText.text = " ";
                busGameManage.passengerMoneyText.text = " ";
                busGameManage.droppedPassengerText.text = " ";
                busGameManage.currentAddBaggage.text = " ";
                busGameManage.currentDroppedBaggage.text = " ";

            } else {
                busGameManage.navigationScript.getSecondMission = true;
                busGameManage.secondLastStation.SetActive (true);
                busGameManage.withBaggagePassengerMoneyText.text = " ";
                busGameManage.passengerMoneyText.text = " ";
                busGameManage.droppedPassengerText.text = " ";
                busGameManage.currentAddBaggage.text = " ";
                busGameManage.currentDroppedBaggage.text = " ";

            }
        }
    }
    private void OnTriggerEnter (Collider oyuncu) {
        if (oyuncu.CompareTag ("Player")) {
            tempWait = waitTime;
            dropTempWait=waitTime;
        }
    }
    private void OnTriggerStay (Collider oyuncu) {

        if (oyuncu.CompareTag ("Player")) {

            if (isLoaded == false) {
                waitTime -= 1 * Time.deltaTime;
                DropThePassenger ();
                PassengerLoading ();

                if (waitTime <= 0) {
                    SpawnBusPlane ();
                    Destroy (this.gameObject);
                    isLoaded = true;
                }

            }
        }
    }
    private void OnTriggerExit (Collider oyuncu) {
        if (oyuncu.CompareTag ("Player")) {

            SpawnBusPlane ();
            Destroy (this.gameObject);

        }
    }

    IEnumerator CoroutineBaggage () {
        busGameManage.withBaggagePassengerMoneyText.text = "+" + busGameManage.passengerOnBoardCounter;
        yield return new WaitForSeconds (0.5f); // 1.5 saniye bekle
        busGameManage.withBaggagePassengerMoneyText.text = " ";
    }
    IEnumerator CoroutinePassenger () {
        busGameManage.passengerMoneyText.text = "+" + busGameManage.passengerOnBoardCounter;
        yield return new WaitForSeconds (0.4f); // 1.5 saniye bekle
        busGameManage.passengerMoneyText.text = " ";
    }

    IEnumerator CoroutineDropped () {
        busGameManage.droppedPassengerText.text = "+" + busGameManage.passengerOnBoardCounter;
        yield return new WaitForSeconds (0.2f); // 1.5 saniye bekle
        busGameManage.droppedPassengerText.text = " ";
    }

    IEnumerator CoroutineBaggageDropped () {
        busGameManage.currentDroppedBaggage.text = "+" + busGameManage.passengerOnBoardCounter;
        yield return new WaitForSeconds (0.5f); // 1.5 saniye bekle
        busGameManage.currentDroppedBaggage.text = " ";
    }

    IEnumerator CoroutineBaggageAdd () {
        busGameManage.currentAddBaggage.text = "+" + busGameManage.passengerOnBoardCounter;
        yield return new WaitForSeconds (0.5f); // 1.5 saniye bekle
        busGameManage.currentAddBaggage.text = " ";
    }

}