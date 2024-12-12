using System.Collections;
using UnityEngine;

public class NoAccidentBus : MonoBehaviour
{

public bool accident=false;
public int accidentNumber;
private int accidentEffect;
public int accidentSatisfaction=100;
public bool taxiAccident;
 public bool isSevenHill;
public bool isLosBiza;
private TaxiGameManager taxiGameManage;
private void Start()
{
    taxiGameManage=FindObjectOfType<TaxiGameManager>();
}
private void OnCollisionEnter (Collision oyuncu) {
        accident=true;
        accidentNumber++;
        accidentEffect=(accidentSatisfaction*7/2)/100;
        accidentSatisfaction-=accidentEffect;
        taxiAccident=true;
        if(PlayerPrefs.GetInt("rolePointer")==6)
        {
         if(taxiGameManage.isCustomerInCar==true)
        {
             taxiGameManage.totalSatisfaction-=accidentEffect;
        }   
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
