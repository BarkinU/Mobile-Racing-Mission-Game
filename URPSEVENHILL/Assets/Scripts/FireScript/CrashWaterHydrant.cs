using System.Collections;
using UnityEngine;

public class CrashWaterHydrant : MonoBehaviour
{
public int accidentWaterHydrant;

private void OnCollisionEnter (Collision oyuncu) {
    if(oyuncu.gameObject.tag=="WaterHydrant"){
        accidentWaterHydrant++;
    }
     
}
}
