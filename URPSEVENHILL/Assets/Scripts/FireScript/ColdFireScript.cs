using System.Collections;
using UnityEngine;

public class ColdFireScript : MonoBehaviour
{

    public bool coldFire=false;

    private void OnTriggerEnter (Collider oyuncu) {

        if (oyuncu.CompareTag("Player")) {
            coldFire=true;
            }
        }
}
