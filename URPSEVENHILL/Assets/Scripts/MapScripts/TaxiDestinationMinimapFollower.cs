using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaxiDestinationMinimapFollower : MonoBehaviour {

	Vector3 TempV3;
	private Transform CustomerDestination;
	private Transform player;


	private void Start(){

		  player = GameObject.FindGameObjectWithTag ("Player").transform;
		
	}
	private void Update () {
		CustomerDestination =GameObject.FindWithTag("CustomerDestination").GetComponent<Transform>();
		TempV3 =CustomerDestination.transform.position;
		TempV3.y = transform.position.y;
		transform.position = TempV3;

		transform.rotation=Quaternion.Euler(90f,player.eulerAngles.y,0f);
	}
}
