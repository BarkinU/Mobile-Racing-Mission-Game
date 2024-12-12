using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefEngine : MonoBehaviour {
    #region Variables
    public float maxSteerAngle = 45f;
    public float turnSpeed = 5f;
    public WheelCollider wheelFL;
    public WheelCollider wheelFR;
    public WheelCollider wheelRL;
    public WheelCollider wheelRR;
    public float maxMotorTorque = 600f;
    public float maxBrakeTorque = 6000f;
    public float currentSpeed;
    public int maxSpeed = 600;
    public Vector3 centerOfMass;
    [HideInInspector] public Rigidbody RB;
    public bool isBraking = false;
    [Range (0, 10)] public int distanceOffset = 5;

    [Header ("Sensors")]
    public float sensorLenght = 6f;
    public Vector3 frontSensorPosition = new Vector3 (0f, 0.2f, 0.5f);
    [HideInInspector] public List<Transform> nodes;
    private int currectNode = 0;
    public bool avoiding = false;
    private float targetSteerAngle = 0;
    public int currentNode;

    [ShowOnly] public bool inverseCar;
    private float time;
    private float startTimer;
    private bool checker = true;
    public WheelFrictionCurve friction;
    public PoliceGameManager policeGM;
    public int oldMaxSpeed;
    public Transform[] pathTransforms;
    #endregion
    private void Awake () {

        policeGM = GameObject.FindObjectOfType<PoliceGameManager> ();
    }
    private void OnEnable () {
        oldMaxSpeed = maxSpeed;
        GetComponent<Rigidbody> ().centerOfMass = centerOfMass;
        RB = GetComponent<Rigidbody> ();
        pathTransforms = GameObject.FindGameObjectWithTag("thiefPath").GetComponentsInChildren<Transform>();

        nodes = new List<Transform> ();

        for (int i = 0; i < pathTransforms.Length; i++) {
            if (pathTransforms[i] != policeGM.thiefPath.transform) {
                nodes.Add (pathTransforms[i]);
            }
        }


    }
    IEnumerator wait () {
        yield return new WaitForSeconds (0.1f);

    }

    private void CheckWaypointDistance () {
        Vector3 position = transform.position;
        float distance = Mathf.Infinity;

        for (int i = 0; i < nodes.Count; i++) {

            Vector3 difference = nodes[i].position - position;
            float currentDistance = difference.magnitude;

            if (currentDistance < distance) {
                currentNode = i;
                if ((i + distanceOffset) >= nodes.Count) {

                    nodes[currectNode] = nodes[0];
                    distance = currentDistance;
                } else {
                    nodes[currectNode] = nodes[i + distanceOffset];
                    distance = currentDistance;
                }

                /*  if (checker == true)
                  {
                      transform.LookAt(nodes[currentNode + 1]);
                      checker = false;
                  }
                  */
            }
        }
    }

    private void ApplySteer () {
        if (avoiding) {
            return;
        }
        Vector3 relativeVector = transform.InverseTransformPoint (nodes[currectNode].position);
        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle * Time.smoothDeltaTime * 70f;
        targetSteerAngle = newSteer;

    }

    private void Update () {
        CheckWaypointDistance ();
        Sensors ();
        Braking ();

    }
    private void FixedUpdate () {
        currentSpeed = RB.velocity.magnitude * 6f;

        Drive ();
        ApplySteer ();
        LerpToSteerAngle ();
    }

    private void Sensors () {

        RaycastHit hit;
        Vector3 sensorStartPos = transform.position;
        sensorStartPos += transform.forward * frontSensorPosition.z;
        sensorStartPos += transform.up * frontSensorPosition.y;
        float avoidMultiplier = 0;
        avoiding = false;

        //front left sensor
        sensorStartPos += transform.forward;
        if (Physics.Raycast (sensorStartPos, transform.forward, out hit, sensorLenght)) {

            if (hit.collider.CompareTag ("aiMesh") == true || hit.collider.CompareTag ("Player") == true) {

                isBraking = true;
                avoiding = false;
                Debug.DrawLine (sensorStartPos, hit.point);

            }
            if (hit.collider.CompareTag ("catPoint") == true) {

                maxSpeed = (oldMaxSpeed / 4);
                isBraking = true;
                avoiding = false;
                Debug.DrawLine (sensorStartPos, hit.point);

            }

            if (hit.collider.CompareTag ("path") == true) {

                isBraking = false;
                avoiding = false;
                Debug.DrawLine (sensorStartPos, hit.point);

            }

            if (hit.collider.CompareTag ("PoliceMission") == true) {

                isBraking = false;
                avoiding = false;
                Debug.DrawLine (sensorStartPos, hit.point);

            }

            if (hit.collider.CompareTag ("aiMesh") == false && hit.collider.CompareTag ("catPoint") == false && hit.collider.CompareTag ("path") == false) {

                isBraking = false;
                avoiding = true;
                avoidMultiplier += 5f;
                Debug.DrawLine (sensorStartPos, hit.point);
                maxSpeed = oldMaxSpeed;

            }

        } else {

            maxSpeed = oldMaxSpeed;
            isBraking = false;

        }

        if (avoiding) {
            targetSteerAngle = maxSteerAngle * avoidMultiplier;
            isBraking = true;
        }
    }

    private void Drive () {

        if (transform.eulerAngles.z < 300 && transform.eulerAngles.z > 60) {
            inverseCar = true;

        } else
            inverseCar = false;
        if (currentSpeed < maxSpeed && !isBraking) {
            wheelFL.motorTorque = maxMotorTorque;
            wheelFR.motorTorque = maxMotorTorque;
            wheelRL.motorTorque = maxMotorTorque;
            wheelRR.motorTorque = maxMotorTorque;
        }

        /* bool hitDetect;
         WheelHit hit;
         hitDetect = wheelFL.GetGroundHit(out hit);

         if(hitDetect==false && inverseCar==false)
         {
             RB.AddExplosionForce(-2000,transform.localPosition,1f,0,ForceMode.Impulse);
         }
         */

    }

    private void Braking () {
        if ((isBraking == true) && currentSpeed > 20) {

            wheelRL.brakeTorque = maxBrakeTorque;
            wheelRR.brakeTorque = maxBrakeTorque;
            wheelFL.brakeTorque = maxBrakeTorque;
            wheelFR.brakeTorque = maxBrakeTorque;

            wheelRL.motorTorque = 0;
            wheelRR.motorTorque = 0;
            wheelFL.motorTorque = 0;
            wheelFR.motorTorque = 0;

        } else {

            wheelRL.brakeTorque = 0;
            wheelRR.brakeTorque = 0;
            wheelFL.brakeTorque = 0;
            wheelFR.brakeTorque = 0;

        }

        if (currentSpeed > maxSpeed) {

            wheelRL.motorTorque = 0;
            wheelRR.motorTorque = 0;
            wheelFL.motorTorque = 0;
            wheelFR.motorTorque = 0;

            if (currentSpeed > maxSpeed + 5) {

                wheelRL.brakeTorque = maxBrakeTorque;
                wheelRR.brakeTorque = maxBrakeTorque;
                wheelFL.brakeTorque = maxBrakeTorque;
                wheelFR.brakeTorque = maxBrakeTorque;

            } else {

                wheelRL.brakeTorque = 0;
                wheelRR.brakeTorque = 0;
                wheelFL.brakeTorque = 0;
                wheelFR.brakeTorque = 0;

            }
        } else {

            wheelRL.motorTorque = maxMotorTorque;
            wheelRR.motorTorque = maxMotorTorque;
            wheelFL.motorTorque = maxMotorTorque;
            wheelFR.motorTorque = maxMotorTorque;

        }

    }

    private void LerpToSteerAngle () {
        wheelFL.steerAngle = Mathf.Lerp (wheelFL.steerAngle, targetSteerAngle, Time.deltaTime * turnSpeed);
        wheelFR.steerAngle = Mathf.Lerp (wheelFR.steerAngle, targetSteerAngle, Time.deltaTime * turnSpeed);
    }

    private void returnCorner () {
        if (wheelFL.steerAngle >= 5f || wheelFL.steerAngle <= -5f) {
            if (currentSpeed < 25)
                isBraking = false;
            else
                isBraking = true;

        } else {

            isBraking = false;

        }
    }

}