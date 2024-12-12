using System.Collections;
using UnityEngine;

public class ImmortalHumanScript : MonoBehaviour
{

     public bool immortalHumanFound=false;

    private void OnTriggerEnter (Collider oyuncu) {

        if (oyuncu.CompareTag("Player")) {
            immortalHumanFound=true;
            }
        }
}
