using System.Collections;
using UnityEngine;

public class AccidentGarbageBox : MonoBehaviour
{
public int garbageBoxAccidentNumber;
public bool isSevenHill;
public bool isLosBiza;

private void OnCollisionEnter (Collision oyuncu) {
    if(oyuncu.gameObject.tag=="cop"){
        garbageBoxAccidentNumber++;
    }
     
}
private void OnTriggerEnter (Collider oy){
    if(oy.gameObject.tag=="SevenHill"){
        isSevenHill=true;
    }
     if(oy.gameObject.tag=="LozBiza"){
        isLosBiza=true;
    }
}
  
}
