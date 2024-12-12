using System.Collections;
using System.Collections.Generic;
using RoleShopSystem;
using UnityEngine;

public class RaceFinishState : MonoBehaviour {

    public RaceEnterPoint raceScript;
    public int finishPosition = 0;
    public int totalRaceReward;
    GameData gameData;

    void OnEnable () {
        finishPosition = 0;
        gameData = ReadWriteAllRoles.ReadGameProp (gameData);
    }
    void OnTriggerEnter (Collider other) {

        if (other.CompareTag ("Player")) {

            raceScript.RaceFinished = true;

            if (raceScript.RaceFinished) {

                if (finishPosition != 2) {

                    finishPosition = 1;

                    totalRaceReward = raceScript.carDifficultyLevel * 750 + raceScript.driverDifficultLevel * 1000;
                    gameData.totalMoney += totalRaceReward;
                    ReadWriteAllRoles.ReadGameProp (gameData);

                }
                raceScript.RaceFinishedState ();

            }

        } else if (other.CompareTag ("aii")) {

            finishPosition = 2;

        }

    }
}