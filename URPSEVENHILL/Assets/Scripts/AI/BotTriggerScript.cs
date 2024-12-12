using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BotTriggerScript : MonoBehaviour {

    ObjectPooler objectPooler;
    private int randomBotId;
    public Transform[] botSpawnPoints;
    public Transform[] path;
    public GameObject[] raceScripts;
    public  List<GameObject> activeRaceScripts;
    private int isAdded=0;


    private void Start () {

        objectPooler = ObjectPooler.instance;

    }

    void LateUpdate(){

        SeekTarget();
        if(Time.frameCount %10 == 0)
        Funct();

    }
    void Funct(){

        if(activeRaceScripts.Count == 1){
            if(activeRaceScripts[0].GetComponent<RaceEnterPoint>().RaceFinished == true)
            activeRaceScripts = new List<GameObject>();
        }

    }


    private void OnTriggerEnter (Collider player) {

        if (player.CompareTag("Player") && (activeRaceScripts.Count == 0) ) 
        {

            if (objectPooler.poolDictionary.Count > 0) {

                for (int i = 0; i < botSpawnPoints.Length; i++) {

                    for (int j = 0; j < botSpawnPoints.Length; j++) {

                        randomBotId = Random.Range (1,40);

                        Debug.Log(randomBotId+"TriggeredRandomBotNumber");

                        if (objectPooler.poolDictionary.ContainsKey (randomBotId)) {
                            break;
                        }

                    }

                    objectPooler.SpawnFromPool (randomBotId, botSpawnPoints[i].position, Quaternion.identity, path[i]);

                    Debug.Log (objectPooler.poolDictionary.Count+"Pool1Count");

                }

            }
        
        }

    }
    
    void SeekTarget()
    {

        foreach (GameObject raceScript in raceScripts)
        {

            if (raceScript.activeInHierarchy && isAdded == 0)
            {
                isAdded +=1;
                activeRaceScripts.Add(raceScript);

            }
    
        }
 
    }


}