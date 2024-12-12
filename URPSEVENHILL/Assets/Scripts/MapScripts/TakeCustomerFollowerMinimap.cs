using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeCustomerFollowerMinimap : MonoBehaviour {

	Vector3 TempV3;
	private Transform Customer;
	private Transform player;


	private void Start(){
		 
		  player = GameObject.FindGameObjectWithTag ("Player").transform;
		
	}
	private void Update () {
		Customer =GameObject.FindWithTag("Customer").GetComponent<Transform>();
		TempV3 =Customer.transform.position;
		TempV3.y = transform.position.y;
		transform.position = TempV3;

		transform.rotation=Quaternion.Euler(90f,player.eulerAngles.y,0f);

	}

}
