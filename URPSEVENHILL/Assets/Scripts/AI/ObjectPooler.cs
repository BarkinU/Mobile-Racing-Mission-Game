using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {

    [System.Serializable]
    public class Pool {
        public int botId;
        public GameObject prefab;

    }
    #region Singleton
    public static ObjectPooler instance;
    private void Awake () {
        instance = this;
    }

    #endregion
    public List<Pool> pools;
    public static int botID;
    public Dictionary<int, GameObject> poolDictionary;
    public Dictionary<int, GameObject> poolDictionary2;
    void Start () {

        poolDictionary = new Dictionary<int, GameObject> ();

        poolDictionary2 = new Dictionary<int, GameObject> ();

        foreach (Pool pool in pools) {

            GameObject obj = Instantiate (pool.prefab);
            obj.SetActive (false);
            
            poolDictionary.Add (pool.botId, obj);
            pool.botId++;

        }
    }

    public GameObject SpawnFromPool (int botId, Vector3 position, Quaternion rotation,Transform currentPath) {

        if (!poolDictionary.ContainsKey (botId)) {
            Debug.LogWarning ("Pool with type : " + botId + " doesn't exist .");
            return null;
        }
        Debug.Log (botId+"SpawnedBotId");
        GameObject objectToSpawn = poolDictionary[botId];
        objectToSpawn.GetComponent<CarEngine>().path=currentPath;
        objectToSpawn.SetActive (true);
        objectToSpawn.GetComponent<CarEngine>().botNumber=botId;
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolDictionary2.Add (botId, objectToSpawn);
        poolDictionary.Remove (botId);

        return objectToSpawn;

    }

    public GameObject DisableBots (int botId) {

        if (!poolDictionary2.ContainsKey (botId)) 
        {
            
            Debug.LogWarning ("Pool with type : " + botId + " doesn't exist .");
            return null;

        }

        Debug.Log (botId+"DisabledBotId");
        GameObject objectToSpawn = poolDictionary2[botId];

        poolDictionary.Add (botId, objectToSpawn);
        poolDictionary2.Remove (botId);
        objectToSpawn.SetActive (false);

        return objectToSpawn;

    }

}