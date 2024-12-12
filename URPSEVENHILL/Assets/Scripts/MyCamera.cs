using UnityEngine;
using UnityEngine.EventSystems;

[AddComponentMenu("Pablo/Camera")]

public class MyCamera : MonoBehaviour{
	#region Variables
	public PABLO carControl;
	private Rigidbody RB;
	private float carSpeed = 0f;

	public Camera thisCam;			// Kamera buraya atilmayacak. Kamera pivotun childi.

	// Smoothlamak icin birkac variable
	private Vector3 targetPosn, lastFollowerPosn = Vector3.zero;
	private Vector3 lastTargetPositn = Vector3.zero;

	[ShowOnly]public int ellipticalDirection = 1;
	private int lastEllipticalDirection = 1;

	[ShowOnly]public float xPosition = 7f;	
	[ShowOnly]public float yPosition = 2f;	
	[ShowOnly]public float rotnDamping = 5f;
	[ShowOnly]public float pitchAngle = 7f;	



	private Quaternion ellipticalRotn = Quaternion.identity;		// Elliptic rotation.

	internal float ellipticX = 0f;
	internal float ellipticY = 0f;

	[ShowOnly]public float minEllipticY = -20f;
	[ShowOnly]public float maxEllipticY = 80f;

	private float ellipticXSpeed = 7.5f;
	private float ellipticYSpeed = 5f;
	private float ellipticResetTimer = 0f;

	private Quaternion currentRotn = Quaternion.identity;
	private Quaternion desiredRotn = Quaternion.identity;


	public delegate void onCameraSpawned(GameObject MyCamera);
	public static event onCameraSpawned OnCameraSpawned;
	#endregion

	private void Start(){
	
		thisCam = GetComponentInChildren<Camera>();
		carControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PABLO>();
		CameraSwitch();


	}
	
	void GetTarget(){

		if(!carControl)
			return;

		RB = carControl.GetComponent<Rigidbody>();

		ResetCamera ();
		
	}
	
	public void CameraSwitch()
	{

			if(carControl.vehicleTypeAssigner == 1) 
			{
				xPosition = 3.1f;
				yPosition = 1.1f;
				pitchAngle =14f;
			}
			else if (carControl.vehicleTypeAssigner == 2)
			{
				xPosition = 4.7f;
				yPosition = 2.4f;
				pitchAngle = 19f;
			}
			else if (carControl.vehicleTypeAssigner == 3)
			{
				xPosition =7f;
				yPosition = 3.2f;
				pitchAngle = 18f;
			}else if (carControl.vehicleTypeAssigner == 4)
			{
				xPosition = 7.8f;
				yPosition = 4.95f;
				pitchAngle = 24f;
			}
			else if (carControl.vehicleTypeAssigner == 5)
			{
				xPosition = 5.9f;
				yPosition = 3.7f;
				pitchAngle = 20f;
				
			}
			else if (carControl.vehicleTypeAssigner == 6)
			{
				xPosition = 7.25f;
				yPosition = 3.7f;
				pitchAngle = 20f;
			}
			else if (carControl.vehicleTypeAssigner == 7)
			{
				xPosition = 5f;
				yPosition = 2.8f;
				pitchAngle = 19f;
			}
			else if (carControl.vehicleTypeAssigner == 8)
			{
				xPosition = 5.6f;
				yPosition = 3.1f;
				pitchAngle = 19f;
			}
			else if (carControl.vehicleTypeAssigner == 9)
			{
				xPosition = 3.3f;
				yPosition = 1.4f;
				pitchAngle =15f;
			}
			else if (carControl.vehicleTypeAssigner == 10)
			{
				xPosition = 5.65f;
				yPosition = 2.8f;
				pitchAngle = 17f;
			}
			else if (carControl.vehicleTypeAssigner == 11)
			{
				xPosition = 2.92f;
				yPosition = 1.2f;
				pitchAngle =16f;
			}
			else if (carControl.vehicleTypeAssigner == 12)
			{
				xPosition = 3f;
				yPosition = 1.5f;
				pitchAngle =17f;
			}
			else if (carControl.vehicleTypeAssigner == 13)
			{
				xPosition = 3.45f;
				yPosition = 1.6f;
				pitchAngle =17f;
			}

	}

	
	void Update(){
	


		if (!carControl || !RB){

			GetTarget();
			return;

		}

		if (!carControl.gameObject.activeSelf)
			return;

			Camera();
			Elliptic ();

		// Smoothlanmis arac surati
		carSpeed = carControl.speed;

		
	}

	void LateUpdate (){



	}
	
	void Camera(){

		if (lastEllipticalDirection != carControl.direction) {

			ellipticalDirection = carControl.direction;
			ellipticX = 0f;
			ellipticY = 0f;

		}

		lastEllipticalDirection = carControl.direction;

		desiredRotn = carControl.transform.rotation * Quaternion.AngleAxis ((ellipticalDirection == 1 ? 0 : 180) + (true ? ellipticX : 0), Vector3.up);
		desiredRotn = desiredRotn * Quaternion.AngleAxis ((true ? ellipticY : 0), Vector3.right);

		if(Input.GetKey(KeyCode.B))
			desiredRotn = desiredRotn * Quaternion.AngleAxis ((180), Vector3.up);

		// Y ekseninde rotation u sonumleme
		if(Time.time > 1)
			currentRotn = Quaternion.Lerp(currentRotn, desiredRotn, rotnDamping * Time.deltaTime);
		else
			currentRotn = desiredRotn;
		
		targetPosn = carControl.transform.position;
		targetPosn -= (currentRotn) * Vector3.forward * (xPosition * .90f);
		targetPosn += Vector3.up * (yPosition * .85f);
		
		transform.position = new Vector3(targetPosn.x,Mathf.Lerp(targetPosn.y,targetPosn.y-.2f, (RB.velocity.magnitude * 3.6f) / 200f),targetPosn.z);

		transform.LookAt(carControl.transform);
		transform.eulerAngles = new Vector3(currentRotn.eulerAngles.x + (pitchAngle * Mathf.Lerp(1f, .75f, (RB.velocity.magnitude * 3.6f) / 200f)), transform.eulerAngles.y, 0);

		lastFollowerPosn = -transform.position;
		lastTargetPositn = -targetPosn;

	}
	
	void Elliptic(){

		ellipticY = Mathf.Clamp(ellipticY, minEllipticY, maxEllipticY); //Clamping Y.

		ellipticalRotn = Quaternion.Euler(ellipticY, ellipticX, 0f);

		if(carSpeed > 10f && Mathf.Abs(ellipticX) > 1f)
			ellipticResetTimer += Time.deltaTime;

		if (carSpeed > 10f && ellipticResetTimer >= 2f) {

			ellipticX = 0f;
			ellipticY = 0f;
			ellipticResetTimer = 0f;

		}

	}

	public void OnDrag(PointerEventData pointerData){

		// Drag input which is received from UI.
		ellipticX += pointerData.delta.x * ellipticXSpeed * .02f;
		ellipticY -= pointerData.delta.y * ellipticYSpeed * .02f;
		ellipticResetTimer = 0f;

	}


	private void ResetCamera(){

		thisCam.transform.localPosition = Vector3.zero;
		thisCam.transform.localRotation = Quaternion.identity;

		lastFollowerPosn = transform.position;
		lastTargetPositn = targetPosn;

		ellipticX = 0f;
		ellipticY = 0f;

		transform.SetParent(null);

	}



	void OnEnable(){

		if(OnCameraSpawned != null)
			OnCameraSpawned (gameObject);

		lastFollowerPosn = transform.position;
		lastTargetPositn = transform.position;

	}


}