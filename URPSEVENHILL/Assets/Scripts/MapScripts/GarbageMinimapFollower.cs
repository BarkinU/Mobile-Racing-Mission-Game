using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageMinimapFollower : MonoBehaviour
{
	Vector3 TempV3;
	private Transform garbageEmptyArea;
	private Transform player;
	private Transform emptySelect;

	private void Start(){
		 
		 player = GameObject.FindGameObjectWithTag ("Player").transform;
		 if (PlayerPrefs.GetInt ("isFirst") == 1) {
            emptySelect = GameObject.FindWithTag("LosBiza").GetComponent<Transform>();
        } else {
            emptySelect = GameObject.FindWithTag("SevenHill").GetComponent<Transform>();
        }
		
		garbageEmptyArea =emptySelect;
		TempV3 = garbageEmptyArea.position;
		TempV3.y = transform.position.y;
		transform.position = TempV3;

		transform.rotation=Quaternion.Euler(90f,player.eulerAngles.y,0f);
	}
}
