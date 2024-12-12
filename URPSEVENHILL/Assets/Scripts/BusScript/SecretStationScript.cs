using System.Collections;
using UnityEngine;

public class SecretStationScript : MonoBehaviour
{
    public bool secretStationFound=false;

    private void OnTriggerEnter (Collider oyuncu) {

        if (oyuncu.CompareTag("Player")) {
            secretStationFound=true;
            }
        }
    
}
