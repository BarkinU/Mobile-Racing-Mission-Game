using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    private Transform player;
    [SerializeField]private float miniCamHeight=100.0f;

    private void Start(){
    player = GameObject.FindGameObjectWithTag ("Player").transform;
    
    }
    private void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.y=player.position.y+miniCamHeight;
        transform.position=newPosition;

        transform.rotation=Quaternion.Euler(90f,player.eulerAngles.y,0f);
    }
}
