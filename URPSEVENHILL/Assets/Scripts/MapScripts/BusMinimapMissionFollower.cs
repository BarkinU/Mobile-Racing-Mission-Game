using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusMinimapMissionFollower : MonoBehaviour {

	Vector3 TempV3;
	private Transform busMissionPlaneTrans;
	private Transform player;

	private void Start(){
		 
		 player = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	private void Update () {
		busMissionPlaneTrans =GameObject.FindWithTag("busMission").GetComponent<Transform>();
		TempV3 = busMissionPlaneTrans.position;
		TempV3.y = transform.position.y;
		transform.position = TempV3;

		transform.rotation=Quaternion.Euler(90f,player.eulerAngles.y,0f);

	}
}
