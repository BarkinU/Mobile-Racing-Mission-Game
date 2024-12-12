using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GarbageMission : MonoBehaviour {
    [Header ("Script Objects")]
    public GarbageGameManager garbageGameManage;

    [Header ("Load Time Control")]
    private float waitTime ;
    private float tempWait;
    private bool isLoaded = false;
    private int loadedGarbage;
    private int ironGarbage;
    private GameObject garbageObject;
    private int randomGarbage;

    private void Start () {

    }
    private void Awake () {
        garbageGameManage = GameObject.FindObjectOfType<GarbageGameManager> ();
        garbageObject = Resources.Load ("Garbage") as GameObject;
        
        loadedGarbage = Random.Range (1, 7);
        ironGarbage = Random.Range (1, 5);
        waitTime=loadedGarbage;
        FastGathering();
    }

    private void FastGathering () {
        float decwaitTime = (waitTime * garbageGameManage.fastGatherPercent) / 100;
        waitTime -= decwaitTime;
    }


    private void GarbageCollectFirstCity () {
        

        int ironTotalChance;
        if (garbageGameManage.totalGarbageCounter < garbageGameManage.garbageCapacity) {
            if (loadedGarbage > 0) {
                if (waitTime <= tempWait - 1) {
                    ironTotalChance = Random.Range (0, 1001);
                    garbageGameManage.totalGarbageCounter++;
                    tempWait--;
                    loadedGarbage--;
                    ironGarbage--;
                    if(garbageGameManage.magnetPower>ironTotalChance)
                    {
                        garbageGameManage.ironCounter++;
                    }
                }
            }
        }
    }

    private void RemoveAndAddList () {
        
        if (PlayerPrefs.GetInt ("isFirst") == 1) {
            garbageGameManage.garbagePlaceFirstCityList.IndexOf (this.gameObject);
            Debug.Log (garbageGameManage.garbagePlaceFirstCityList.IndexOf (this.gameObject));
            garbageGameManage.garbagePlaceFirstCityList.Remove (this.gameObject);
            garbageGameManage.garbagePlaceFirstCityRemovedList.Add (this.gameObject);
            this.gameObject.SetActive (false);
            

            if (garbageGameManage.garbagePlaceFirstCityCounter < garbageGameManage.firstCityGarbageLength / 3) {
                randomGarbage=Random.Range(0,garbageGameManage.garbagePlaceFirstCityRemovedList.Count);
                garbageGameManage.garbagePlaceFirstCityRemovedList[randomGarbage].SetActive(true);
                garbageGameManage.garbagePlaceFirstCityRemovedList.Remove (garbageGameManage.garbagePlaceFirstCityRemovedList[randomGarbage]);
                garbageGameManage.garbagePlaceFirstCityList.Add (garbageGameManage.garbagePlaceFirstCityRemovedList[randomGarbage]);
                
                
            }

            garbageGameManage.garbagePlaceFirstCityCounter--;
        } else {
            garbageGameManage.garbagePlaceSecondCityList.IndexOf (this.gameObject);
            Debug.Log (garbageGameManage.garbagePlaceSecondCityList.IndexOf (this.gameObject));
            garbageGameManage.garbagePlaceSecondCityList.Remove (this.gameObject);
            this.gameObject.SetActive (false);
            garbageGameManage.garbagePlaceSecondCityRemovedList.Add (this.gameObject);

            if (garbageGameManage.garbagePlaceSecondCityCounter < garbageGameManage.secondCityGarbageLength / 3) {
                randomGarbage=Random.Range(0,garbageGameManage.garbagePlaceSecondCityRemovedList.Count);
                garbageGameManage.garbagePlaceSecondCityRemovedList[randomGarbage].SetActive(true);
                garbageGameManage.garbagePlaceSecondCityRemovedList.Remove (garbageGameManage.garbagePlaceSecondCityRemovedList[randomGarbage]);
                garbageGameManage.garbagePlaceSecondCityList.Add (garbageGameManage.garbagePlaceSecondCityRemovedList[randomGarbage]);
            }

            garbageGameManage.garbagePlaceSecondCityCounter--;
        }
        
        isLoaded = true;
    }

    private void OnTriggerEnter (Collider oyuncu) {
        if (oyuncu.CompareTag("Player")) {
            tempWait = waitTime;
            if(garbageGameManage.compressionedCounter!=garbageGameManage.compressionTenur)
            {
                garbageGameManage.compressionButton.interactable=true;
            }else{
                garbageGameManage.compressionButton.interactable=false;
            }
            
        }
    }
    private void OnTriggerStay (Collider oyuncu) {
        if (oyuncu.CompareTag("Player")) {

            if (isLoaded == false) {
                waitTime -= 1 * Time.deltaTime;
                GarbageCollectFirstCity ();
                if (waitTime <= 0) {
                    RemoveAndAddList ();
                }

            }
        }
    }

}