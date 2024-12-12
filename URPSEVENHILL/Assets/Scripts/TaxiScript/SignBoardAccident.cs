using System.Collections;
using UnityEngine;

public class SignBoardAccident : MonoBehaviour
{

public int signBoardAccidentNumber;

private void OnCollisionEnter (Collision oyuncu) {
    if(oyuncu.gameObject.tag=="SignBoard"){
        signBoardAccidentNumber++;
    }
     
}
}
