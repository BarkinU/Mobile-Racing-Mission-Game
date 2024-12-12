using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(WheelCollider))]
[AddComponentMenu("Pablo/Wheel Collider")]

public class colliders : MonoBehaviour{
	#region Variables
	private WheelCollider _wheelCollider;
	public WheelCollider wheelCollider{ get { if(_wheelCollider == null) 
						_wheelCollider = GetComponent<WheelCollider>();
						return _wheelCollider;
		}}

	private PABLO pablo;
	private Rigidbody RB;
	private List <colliders> allWC = new List<colliders>();
	public Transform wheelModel;
	private float wheelRotn = 0f;
	internal float wheelRPMtoSpeed = 0f;

	public float compression;
		[Header(" WheelCollider  Configurations")]
	[Range(0f,1f)]public float radius = 0.38f;
	[Range(0.01f, 100f)]public float wheelMass = 35f;
	[Range(0.00f, 1f)]public float forceAppPointDistance = 0.001f;
	[Range(0f, 0.5f)]public float suspensionDistance = 0f;
	[Range(0.01f, 1f)]public float wheelDampingRate = 0.1f;



		[Header(" 			Forward Friction Configurations")]

	private float fExtremumSlip= 0.2f;
	private float fExtremumValue= 1f;
	private float fAsymptoteSlip= 1f;
	private float fAsymptoteValue= 1f;
	[Range(0.01f, 10f)]public float fStiffness= 1f;

		[Header(" 			Sideway Friction Configurations")]
	private float sExtremumSlip= 0.2f;
	private float sExtremumValue= 1f;
	private float sAsymptoteSlip= 0.5f;
	private float sAsymptoteValue= 0.75f;
	[Range(0.01f, 10f)]public float sStiffness= 1f;
	public IM2 im2;
	private float ali=0f;
	[ShowOnly]public float smoothSteering = 60f;
	public WheelFrictionCurve sidewaysFriction;
	public WheelFrictionCurve forwardFriction;
	#endregion
	
	void Awake(){
		pablo = GetComponentInParent<PABLO>();
		RB = pablo.GetComponent<Rigidbody>();
		
		
		
	}
	void Start(){


		allWC = pablo.GetComponentsInChildren<colliders>().ToList();
		wheelCollider.mass = RB.mass / 20f;
		WheelColliderConfig();
		im2 = GameObject.FindGameObjectWithTag("IM2").GetComponent<IM2>();

	}
	
	
	public void WheelColliderConfig(){	
			WheelHit hit;
			wheelCollider.GetGroundHit(out hit);			
	
			forwardFriction = _wheelCollider.forwardFriction;
			sidewaysFriction = _wheelCollider.sidewaysFriction;
			

			_wheelCollider.GetComponent<WheelCollider>();
			_wheelCollider.radius = radius;

	
			forwardFriction.extremumSlip = fExtremumSlip;
			forwardFriction.extremumValue = fExtremumValue;
			forwardFriction.asymptoteSlip = fAsymptoteSlip;
			forwardFriction.asymptoteValue = fAsymptoteValue;
			forwardFriction.stiffness = fStiffness;

			sidewaysFriction.extremumSlip = sExtremumSlip;
			sidewaysFriction.extremumValue = sExtremumValue;
			sidewaysFriction.asymptoteSlip = sAsymptoteSlip;
			sidewaysFriction.asymptoteValue = sAsymptoteValue;
			sidewaysFriction.stiffness = sStiffness;
			_wheelCollider.suspensionDistance = suspensionDistance;
			_wheelCollider.forceAppPointDistance = -forceAppPointDistance;
			_wheelCollider.mass = wheelMass;
			_wheelCollider.wheelDampingRate = wheelDampingRate;

			_wheelCollider.sidewaysFriction = sidewaysFriction;
			_wheelCollider.forwardFriction = forwardFriction;
				

			
			}
	
	void ApplyMotorTorque(float torque){

		if(OverTorque())
			torque = 0;
				wheelCollider.motorTorque = ((torque * (1 - pablo.clutchInput)) * pablo._gasInput) * (pablo.engineTorqueCurve[pablo.currentGear].Evaluate(wheelRPMtoSpeed * pablo.direction) * pablo.direction);
		if(pablo.speed < pablo.maxspeed)
				wheelCollider.motorTorque = ((torque * (1 - pablo.clutchInput)) * pablo._gasInput) * (pablo.engineTorqueCurve[pablo.currentGear].Evaluate(wheelRPMtoSpeed * pablo.direction) * pablo.direction);

	}

	void Update(){

		WheelColliderConfig();
		WheelAlign ();
		if (this == pablo.wheels[0]  || this == pablo.wheels[1] )
		ApplySteering ();

	}
	
	void FixedUpdate (){

		if (!pablo.enabled)
			return;

		if (pablo.isSleeping)
			return;

		wheelRPMtoSpeed = (((wheelCollider.rpm * wheelCollider.radius) / 2.9f)) * RB.transform.lossyScale.y;

		ApplyMotorTorque (pablo.engineTorque);
		

#region Braking, ABS.

		// Apply Handbrake if this wheel is one of the rear wheels.
		if (pablo.handbrakeInput > .5f) {

			if (this == pablo.wheels[2] || this == pablo.wheels[3] )
				ApplyBrakeTorque ((pablo.brakeTorque * 1.5f) * pablo.handbrakeInput);

		} else {

			// Apply Braking to all wheels.
			if (this == pablo.wheels[0]  || this == pablo.wheels[1] || this == pablo.wheels[2]  || this == pablo.wheels[3] )
				ApplyBrakeTorque (pablo.brakeTorque * (Mathf.Clamp (pablo._brakeInput, 0f, 1f)));
			else
				ApplyBrakeTorque (pablo.brakeTorque * (Mathf.Clamp (pablo._brakeInput, 0f, 1f) / 2f));

		}

		#endregion

	}
    
	public void WheelAlign (){

		WheelHit GroundHit;
		bool grounded = wheelCollider.GetGroundHit(out GroundHit );

		float newCompression = compression;

		if (grounded)
			newCompression = 1f - ((Vector3.Dot(transform.position - GroundHit.point, transform.up) - (wheelCollider.radius * transform.lossyScale.y)) / wheelCollider.suspensionDistance);
		else
			newCompression = wheelCollider.suspensionDistance;

		compression = Mathf.Lerp (compression, newCompression, Time.deltaTime * 50f);

		// posn of the wheel model
		wheelModel.position = transform.position;
		wheelModel.position += (transform.up * (compression - 1.0f) * wheelCollider.suspensionDistance);

		// x axis rotn of wheel
		wheelRotn += wheelCollider.rpm * 6f * Time.deltaTime;
		wheelModel.rotation = (transform.rotation) * Quaternion.Euler(wheelRotn, wheelCollider.steerAngle, transform.rotation.z);


		// gizmos for wheel slips and forces
		float extension = (-wheelCollider.transform.InverseTransformPoint(GroundHit.point).y - (wheelCollider.radius * transform.lossyScale.y)) / wheelCollider.suspensionDistance;
		Debug.DrawLine(GroundHit.point, GroundHit.point + transform.up * (GroundHit.force / RB.mass), extension <= 0.0 ? Color.magenta : Color.white);
		Debug.DrawLine(GroundHit.point, GroundHit.point - transform.forward * GroundHit.forwardSlip * 2f, Color.green);
		Debug.DrawLine(GroundHit.point, GroundHit.point - transform.right * GroundHit.sidewaysSlip * 2f, Color.red);

	}

	public void ApplySteering(){

		ali = Mathf.Clamp(pablo.steerAngle * pablo._steerInput,-pablo.steerAngle,pablo.steerAngle );
		smoothSteering = smoothSteering - (pablo.speed * 0.17f);
	
		if (im2.steer >= 0 || im2.steer <= 0)
		
		{

			

				wheelCollider.steerAngle =  Mathf.MoveTowardsAngle(wheelCollider.steerAngle,ali,1);

				
/*			
			else if(pablo.speed > 80 && pablo.speed < 115)
			{

				smoothSteering = 40f;
				wheelCollider.steerAngle =  Mathf.MoveTowardsAngle(wheelCollider.steerAngle,ali,smoothSteering*Time.smoothDeltaTime);

			}

			else if(pablo.speed > 115 && pablo.speed < 150)
			{ 
				
				smoothSteering = 20f;
				wheelCollider.steerAngle =  Mathf.MoveTowardsAngle(wheelCollider.steerAngle,ali,smoothSteering*Time.smoothDeltaTime);
			 
			}

			else if(pablo.speed > 160)
			{

				smoothSteering = 10f;
				wheelCollider.steerAngle =  Mathf.MoveTowardsAngle(wheelCollider.steerAngle,ali,smoothSteering*Time.smoothDeltaTime);

			}
			else 
			{
				
				smoothSteering = 60f;
				wheelCollider.steerAngle =  Mathf.MoveTowardsAngle(wheelCollider.steerAngle,ali,smoothSteering*Time.smoothDeltaTime);

			}*/
			return;
		}

	}
	
	void ApplyBrakeTorque(float brake){

		if(pablo.ABS && pablo.handbrakeInput <= .1f){

			WheelHit hit;
			wheelCollider.GetGroundHit(out hit);

			if((Mathf.Abs(hit.forwardSlip) * Mathf.Clamp01(brake)) >= 0.6f){ //Bu ayar tekeri kitlemek icin duzenlendi
				pablo.ABSAct = true;
				brake = 0;
			}else{
				pablo.ABSAct = false;
			}

		}

		wheelCollider.brakeTorque = brake;

	}
    
	bool OverTorque(){

		if(pablo.speed > pablo.maxspeed || !pablo.engineRunning)
			return true;

		return false;

	}

}
