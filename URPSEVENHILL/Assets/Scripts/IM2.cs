using UnityEngine;

public class IM2 : MonoBehaviour
{
	public PABLO pablo;
	public float accel,steer,brake,handbrake;
	public void accelInput(float input) {accel = input;}
	public void brakeInput(float input) {brake = input;}
	public void steerInput(float input) {steer = input;}
	public void handbrakeInput(float input) {handbrake = input;}

	private void Start() {
		pablo = GameObject.FindGameObjectWithTag("Player").GetComponent<PABLO>();
	}

	private void Update(){
			pablo.Inputs(steer,accel,accel,brake,handbrake);

	}

}
