using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    ObjectPooler objectPooler;

    private void Start(){
      //  objectPooler = ObjectPooler.instance;
    }

    void SpawnBots()
    {
        //objectPooler.SpawnFromPool("bot",new Vector3(0,0,60f), Quaternion.identity);
    }
}
