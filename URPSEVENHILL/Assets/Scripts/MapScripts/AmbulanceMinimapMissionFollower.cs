using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbulanceMinimapMissionFollower : MonoBehaviour {

	Vector3 TempV3;
	private Transform patientPoint;
	private Transform player;

	private void Start(){
		 
		 player = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	private void Update () {
		patientPoint=GameObject.FindGameObjectWithTag("ambulanceMission").GetComponent<Transform>();
		TempV3 =patientPoint.position;
		TempV3.y = transform.position.y;
		transform.position = TempV3;
		transform.rotation=Quaternion.Euler(90f,player.eulerAngles.y,0f);
	}
}
