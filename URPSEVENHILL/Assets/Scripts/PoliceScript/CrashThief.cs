using System.Collections;
using UnityEngine;

public class CrashThief : MonoBehaviour
{
public int CrashThiefNumber;
private void OnCollisionEnter (Collision oyuncu) {
    if(oyuncu.gameObject.tag=="AiThief"){
        CrashThiefNumber++;
    }
     
}
}
