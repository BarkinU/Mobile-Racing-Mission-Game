using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEngine : MonoBehaviour {

    #region Variables
    public Transform path;
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
    private float targetSteerAngle = 0;
    public int currentNode;

    //botPlayerRelation
    private GameObject player;
    ObjectPooler objectPooler;
    private float distanceFromPlayer = 300;
    public int botNumber;
    public float bdis;
    [ShowOnly] public bool inverseCar;
    private float time;
    private bool checker = true;
    public WheelFrictionCurve friction;
    private int currentRole;
    private String currentSceneMission = "", freeMission = "racePoints", busSceneMission = "busMission", garbageSceneMission = "GarbageMission", fireSceneMission = "fireSpawn", policeSceneMission = "PoliceMission", ambulanceSceneMission = "ambulanceMission", taxiSceneMission = "Customer";
    public int oldMaxSpeed;
    public RaceEnterPoint raceScript;

    #endregion

    private void OnEnable () {
                oldMaxSpeed = maxSpeed;

        currentRole = PlayerPrefs.GetInt ("rolePointer");
        StartCoroutine (CheckRole ());

        GetComponent<Rigidbody> ().centerOfMass = centerOfMass;
        RB = GetComponent<Rigidbody> ();
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform> ();

        nodes = new List<Transform> ();

        for (int i = 0; i < pathTransforms.Length; i++) {
            if (pathTransforms[i] != path.transform) {
                nodes.Add (pathTransforms[i]);
            }
        }

        //DistanceControl
        player = GameObject.FindGameObjectWithTag ("Player");
        objectPooler = ObjectPooler.instance;

        friction = wheelFR.forwardFriction;
        friction.stiffness = 6;
        wheelFR.forwardFriction = friction;

        friction = wheelRR.forwardFriction;
        friction.stiffness = 6;
        wheelRR.forwardFriction = friction;
        friction = wheelFL.forwardFriction;
        friction.stiffness = 6;
        wheelFL.forwardFriction = friction;
        friction = wheelRL.forwardFriction;
        friction.stiffness = 6;
        wheelRL.forwardFriction = friction;
        wheelFL.sidewaysFriction = friction;
        friction = wheelFL.sidewaysFriction;
        friction.stiffness = 6;
        wheelRL.sidewaysFriction = friction;
        friction = wheelRL.sidewaysFriction;
        friction.stiffness = 6;
        wheelRR.sidewaysFriction = friction;
        friction = wheelRR.sidewaysFriction;
        friction.stiffness = 6;
        wheelFR.sidewaysFriction = friction;
        friction = wheelFR.sidewaysFriction;
        friction.stiffness = 6;

        
        if(raceScript == null)
        {

            raceScript = GameObject.FindObjectOfType<RaceEnterPoint>();
            if(raceScript == null)
            StartCoroutine (LookCoroutine ());
            
            return;

        }

        else
        {

            StartCoroutine (LookCoroutine ());

        }

        maxSpeed = oldMaxSpeed;

    }
    

    IEnumerator LookCoroutine () {
        
        yield return new WaitForSeconds (.2f);

        transform.LookAt (nodes[currentNode + 1]);
            
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

            }
        }
    }

    private void ApplySteer () {

        Vector3 relativeVector = transform.InverseTransformPoint (nodes[currectNode].position);
        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle * Time.smoothDeltaTime * 100f;
        targetSteerAngle = newSteer;

    }

    void resetBrokenBot () {

    if (currentSpeed < 5f) {
        time += Time.deltaTime;

        if (time > 4 && (raceScript == null)) {

            if (objectPooler.poolDictionary2.Count < 40) {
                objectPooler.DisableBots (botNumber);
            }
            time = 0f;

        }
        
        else if (time > 4 && raceScript != null)
        {

            transform.rotation.SetLookRotation(nodes[currentNode].transform.position);
            transform.position = new Vector3(nodes[currentNode + 1].transform.position.x,nodes[currentNode + 1].transform.position.y +1,nodes[currentNode + 1].transform.position.z);
            time = 0f;

        }
    }

    }

    void OnCollisionStay (Collision other) {
        if (other.gameObject.tag == "aii") {
            if (Time.frameCount % 140 == 0) {
                if (other.gameObject.GetComponent<CarEngine> ().botNumber != botNumber) {

                    if (objectPooler.poolDictionary2.Count < 40) {
                        objectPooler.DisableBots (botNumber);
                    }
                }
            }

        }

    }

    private void Update () {
        
        CheckWaypointDistance ();
        Sensors ();

    }

    private void FixedUpdate () {

        currentSpeed = RB.velocity.magnitude * 6f;
        
        Drive ();

        ApplySteer ();

        LerpToSteerAngle ();

        resetBrokenBot ();

        DistanceControl ();

        Braking ();

        if(Mathf.Abs(wheelFL.steerAngle) > 9 && currentSpeed > 30)
        isBraking = true;

        else
        {
        isBraking = false;
        }


    }

    private void Sensors () {

        RaycastHit hit;
        Vector3 sensorStartPos = transform.position;
        sensorStartPos += transform.forward * frontSensorPosition.z;
        sensorStartPos += transform.up * frontSensorPosition.y;

        //front left sensor
        sensorStartPos += transform.forward;

        if (Physics.Raycast (sensorStartPos, transform.forward, out hit, sensorLenght)) {

            if (hit.collider.CompareTag ("aii") == true || hit.collider.CompareTag ("Player") == true) {

                isBraking = true;
                Debug.DrawLine (sensorStartPos, hit.point);

            }
            if (hit.collider.CompareTag ("catPoint") == true) {

                maxSpeed = (oldMaxSpeed / 4);
                isBraking = true;
                Debug.DrawLine (sensorStartPos, hit.point);

            }

            if (hit.collider.CompareTag ("path") == true) {

                isBraking = false;
                Debug.DrawLine (sensorStartPos, hit.point);

            }

            if (hit.collider.CompareTag (currentSceneMission) == true) {

                isBraking = false;
                Debug.DrawLine (sensorStartPos, hit.point);

            }

            if (hit.collider.CompareTag ("aii") == false && hit.collider.CompareTag ("catPoint") == false && hit.collider.CompareTag ("path") == false) {

                isBraking = false;
                Debug.DrawLine (sensorStartPos, hit.point);
                maxSpeed = oldMaxSpeed;

            }

        } else {

            maxSpeed = oldMaxSpeed;

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

            wheelRL.brakeTorque = Mathf.Lerp(0,maxBrakeTorque,Time.deltaTime*100);
            wheelRR.brakeTorque = Mathf.Lerp(0,maxBrakeTorque,Time.deltaTime*100);
            wheelFL.brakeTorque = Mathf.Lerp(0,maxBrakeTorque,Time.deltaTime*100);
            wheelFR.brakeTorque = Mathf.Lerp(0,maxBrakeTorque,Time.deltaTime*100);

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

            wheelRL.brakeTorque = Mathf.Lerp(0,maxBrakeTorque,Time.deltaTime*100);
            wheelRR.brakeTorque = Mathf.Lerp(0,maxBrakeTorque,Time.deltaTime*100);
            wheelFL.brakeTorque = Mathf.Lerp(0,maxBrakeTorque,Time.deltaTime*100);
            wheelFR.brakeTorque = Mathf.Lerp(0,maxBrakeTorque,Time.deltaTime*100);

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

    private void DistanceControl () {
        bdis = Vector3.Distance (player.transform.position, transform.position);
        if (Vector3.Distance (player.transform.position, transform.position) > distanceFromPlayer) {

            if (objectPooler.poolDictionary2.Count < 40) {
                Debug.Log (botNumber + "BotNumber");

                objectPooler.DisableBots (botNumber);
                Debug.Log (objectPooler.poolDictionary2.Count + "Pool2Count");
            }
        }
    }

    /////////////////////////////////////////////////////////////////////////
    IEnumerator CheckRole () {

        switch (currentRole) {
            case 0:
                currentSceneMission = freeMission;
                break;
            case 1:
                currentSceneMission = busSceneMission;
                break;

            case 2:
                currentSceneMission = garbageSceneMission;
                break;

            case 3:
                currentSceneMission = fireSceneMission;
                break;

            case 4:
                currentSceneMission = policeSceneMission;
                break;

            case 5:
                currentSceneMission = ambulanceSceneMission;
                break;

            case 6:
                currentSceneMission = taxiSceneMission;
                break;

        }
        yield break;
    }
    private void OnDisable() {

        maxSpeed = oldMaxSpeed;
        
    }
}