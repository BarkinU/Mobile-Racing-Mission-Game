using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trafficLightGreen : MonoBehaviour {

    public Material matRed;
    public Material matGreen;

	// Use this for initialization
	void Start () {

        Invoke("m1", 1.0f);
		
	}
	
    void m1()
    {
        GetComponent<Renderer>().material = matRed;

        Invoke("m2", 10.0f);

    }

    void m2()
    {
        GetComponent<Renderer>().material = matGreen;

        Invoke("m1", 1.0f);

    }

    // Update is called once per frame
    void Update () {
		
	}
}
