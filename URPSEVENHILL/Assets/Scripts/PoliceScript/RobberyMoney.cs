using System.Collections;
using UnityEngine;

public class RobberyMoney : MonoBehaviour
{

    public bool robberyMoneyFound=false;

    private void OnTriggerEnter (Collider oyuncu) {

        if (oyuncu.CompareTag("Player")) {
            robberyMoneyFound=true;
            }
    }
}
