using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAreaMinimapFollower : MonoBehaviour {

	Vector3 TempV3;
	private GameObject fireAreaPoint;
	private Transform player;

	private void Start () {

		
		player = GameObject.FindGameObjectWithTag ("Player").transform;

	}
	private void Update () {
		fireAreaPoint = GameObject.FindGameObjectWithTag ("fireSpawn");
		TempV3 = fireAreaPoint.transform.position;
		TempV3.y = transform.position.y;
		transform.position = TempV3;

		transform.rotation=Quaternion.Euler(90f,player.eulerAngles.y,0f);
		
	}
}