using System.Collections;
using UnityEngine;

public class CarFollowerMarker : MonoBehaviour
{

     private Transform player;
    private float carFollowerDistancePlayer=50f;
    private void Start(){
    player = GameObject.FindGameObjectWithTag ("Player").transform;
    
    }
    private void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.y=player.position.y+carFollowerDistancePlayer;
        transform.position=newPosition;

        transform.rotation=Quaternion.Euler(90f,player.eulerAngles.y,0f);
    }
}
