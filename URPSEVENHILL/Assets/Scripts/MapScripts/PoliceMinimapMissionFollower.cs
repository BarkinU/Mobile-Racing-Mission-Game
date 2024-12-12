using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceMinimapMissionFollower : MonoBehaviour {

	Vector3 TempV3;
	private Transform crimeLocationFollower;
	private Transform player;


	private void Start () {

		player = GameObject.FindGameObjectWithTag ("Player").transform;
		crimeLocationFollower = GameObject.FindWithTag ("PoliceMission").GetComponent<Transform> ();
		TempV3 = crimeLocationFollower.transform.position;
		TempV3.y = transform.position.y;
		transform.position = TempV3;

		transform.rotation = Quaternion.Euler (90f, player.eulerAngles.y, 0f);
	}


}