using System.Collections;
using UnityEngine;

public class MostValuableStuff : MonoBehaviour
{
    public bool mostValuableFound=false;

    private void OnTriggerEnter (Collider oyuncu) {

        if (oyuncu.CompareTag("Player")) {
            mostValuableFound=true;
            }
    }
    

}
