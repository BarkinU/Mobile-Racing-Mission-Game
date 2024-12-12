using System.Collections;
using UnityEngine;

public class FindFamousCustomer : MonoBehaviour
{

    public bool famousCustomer=false;

    private void OnTriggerEnter (Collider oyuncu) {

        if (oyuncu.tag == "Player") {
            famousCustomer=true;
            }
        }
}
