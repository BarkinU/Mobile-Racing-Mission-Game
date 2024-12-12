using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRegulator : MonoBehaviour
{
    
    public GameObject BusGame;
    public GameObject GarbageGame;
    public GameObject FireGame;
    public GameObject PoliceGame;
    public GameObject AmbulanceGame;
    public GameObject TaxiGame;
    public GameObject FreeGame;
    public GameObject waterHydrants;
    private int currentRole;


    void Awake () {

        currentRole = PlayerPrefs.GetInt ("rolePointer");
        RoleSelect();

    }

    private void RoleSelect() {
        switch (currentRole) {
            case 0:
                FreeGame.SetActive (true);
                break;

            case 1:
                BusGame.SetActive (true);
                break;

            case 2:
                GarbageGame.SetActive (true);
                break;

            case 3:
                FireGame.SetActive (true);
                waterHydrants.SetActive (false);
                break;

            case 4:
                PoliceGame.SetActive (true);
                break;

            case 5:
                AmbulanceGame.SetActive (true);
                break;

            case 6:
                TaxiGame.SetActive (true);
                break;

        }
    }

    
}
