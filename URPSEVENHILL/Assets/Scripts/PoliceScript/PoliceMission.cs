using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceMission : MonoBehaviour {
    private float currentTime = 3.0f;
    public GameObject thief;
    public PoliceGameManager policeGM;
    private ThiefEngine thiefScript;
    public int extraMoneyForCrimeScene;
    private bool doOnce;

    private void Awake () {
        policeGM = GameObject.FindObjectOfType<PoliceGameManager> ();
        thief = Resources.Load ("ThiefAi") as GameObject;
        extraMoneyForCrimeScene = policeGM.catchMoneyIncrease;

    }

    public void SpawnThief () {
        if (PlayerPrefs.GetInt ("isFirst") == 1) {
            if (policeGM.isCaughtFromScout == false) {
                if (policeGM.remainingTimeCrime <= 0) {
                    policeGM.bigMapThiefIsOk = true;
                    policeGM.firstCityAiSpawnLocations[policeGM.randomCrime].SetActive (true);
                    GameObject.Instantiate (thief, policeGM.firstCityAiSpawnLocations[policeGM.randomCrime].transform.position, Quaternion.identity);
                    policeGM.remainingTimeCrime = Vector3.Distance (policeGM.policeCar.transform.position, policeGM.firstCityAiSpawnLocations[policeGM.randomCrime].transform.position) / 6;
                    if (policeGM.remainingTimeCrime > 100) {
                        policeGM.remainingTimeCrime -= policeGM.remainingTimeCrime / 3;
                    } else {
                        policeGM.remainingTimeCrime += policeGM.remainingTimeCrime / 4;
                    }
                    thiefScript = thief.GetComponent<ThiefEngine> ();
                    thiefScript.maxSpeed -= policeGM.decreaseThiefSpeed;
                    policeGM.isEscaped = true;
                    policeGM.beforeRobberyTime = (int) policeGM.finishingTime;
                    policeGM.thiefMarker.SetActive (true);
                    policeGM.navigationScript.getSecondMission = true;
                } else {
                    policeGM.beforeRobberyTime = (int) policeGM.finishingTime;
                    policeGM.isCatchCrimeScene = true;
                }
            }
        } else {
            if (policeGM.isCaughtFromScout == false) {
                if (policeGM.remainingTimeCrime <= 0) {
                    policeGM.bigMapThiefIsOk = true;
                    policeGM.secondCityAiSpawnLocations[policeGM.randomCrime].SetActive (true);
                    GameObject.Instantiate (thief, policeGM.secondCityAiSpawnLocations[policeGM.randomCrime].transform.position, Quaternion.identity);
                    policeGM.remainingTimeCrime = Vector3.Distance (policeGM.policeCar.transform.position, policeGM.secondCityAiSpawnLocations[policeGM.randomCrime].transform.position) / 6;
                    if (policeGM.remainingTimeCrime > 100) {
                        policeGM.remainingTimeCrime -= policeGM.remainingTimeCrime / 3;
                    } else {
                        policeGM.remainingTimeCrime += policeGM.remainingTimeCrime / 4;
                    }
                    thiefScript = thief.GetComponent<ThiefEngine> ();
                    thiefScript.maxSpeed -= policeGM.decreaseThiefSpeed;
                    policeGM.isEscaped = true;
                    policeGM.beforeRobberyTime = (int) policeGM.finishingTime;
                    policeGM.thiefMarker.SetActive (true);
                    policeGM.navigationScript.getSecondMission = true;
                } else {
                    policeGM.beforeRobberyTime = (int) policeGM.finishingTime;
                    policeGM.isCatchCrimeScene = true;
                }
            }
        }

    }

    private void OnTriggerStay (Collider oyuncu) {
        if (oyuncu.CompareTag ("Player")) {
            currentTime -= 1 * Time.deltaTime;
            if (currentTime <= 0) {
                if (doOnce == false) {
                    SpawnThief ();
                    Destroy (this.gameObject);
                    Destroy (policeGM.PoliceMissionMarker);
                    doOnce = true;
                }

            }
        }
    }
}