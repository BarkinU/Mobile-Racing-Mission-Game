using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefFollowerMarker : MonoBehaviour {

	public float MinimapSize;
	Vector3 TempV3;
	private Transform thiefFollower;
	private Transform player;

	private void Start(){
		 player = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	private void Update () {
		thiefFollower =GameObject.FindWithTag("AiThief").GetComponent<Transform>();
		TempV3 = thiefFollower.transform.position;
		TempV3.y = transform.position.y;
		transform.position = TempV3;

		transform.rotation=Quaternion.Euler(90f,player.eulerAngles.y,0f);


	}

}
