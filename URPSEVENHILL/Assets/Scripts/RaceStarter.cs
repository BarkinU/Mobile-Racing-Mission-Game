using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceStarter : MonoBehaviour
{

    public RaceEnterPoint raceEnterPoint;

    void OnTriggerEnter(Collider other){

        if(other.CompareTag("Player")){
            
            raceEnterPoint.gameObject.SetActive(true);
            
            if(raceEnterPoint.raceStarted)
            {
                
                gameObject.SetActive(false);

            }
        }
        
    }

    void OnTriggerExit(Collider other){

        if(raceEnterPoint.raceStarted == false)
        {

            raceEnterPoint.gameObject.SetActive(false);

        }
        
    }

}
