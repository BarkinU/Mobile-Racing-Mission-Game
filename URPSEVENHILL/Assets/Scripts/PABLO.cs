using UnityEngine;
using System.Collections;
using CarShopSystem;

[AddComponentMenu("Pablo/Control Script")]
public class PABLO : MonoBehaviour
{
    #region	VARIABLES & DEFINITIONS

    [Header("                		           #   Main Configurations #")]
    private driveType DrivingSystem;
    private enum driveType
    {
        AWD
    }
    public controlType ControlType;
    public enum controlType
    {
        Keyboard,
        Touch

    }
    public cameraType CameraType;
    public enum cameraType
    {
        c1,
        c2,
        b1,

        b2,
        b3,
        f1,
        f2,
        g1a2,
        a1,

        g2g3,
        golf,
        a6,
        connect
    }
    public GameObject COM;
    private Rigidbody RB;
    private inputManager IM;
    [ShowOnly] public float speed;
    public float maxspeed = 300f;
    private float resetTime = 0f;   // used for reset car

    [Header("		#	Engine - Car Configurations	 #")]

    public bool engineRunning = false;
    public AnimationCurve[] engineTorqueCurve;
    public float engineTorque;
    [Range(.75f, 2f)] public float engineInertia = 1f;
    [HideInInspector] internal float baseEngineRPM;
    public float brakeTorque = 2000f;
    [ShowOnly] public float engineRPM = 5000f;
    public float minRPM = 2000;
    public float maxRPM = 8000;
    public bool ABS = true;
    public bool ABSAct = false;
    // Body Adjustments
    [Range(.5f, 20f)] public float maxAngularVelocity = 20f;

    [HideInInspector] internal bool isSleeping = false;

    [HideInInspector] public float orgMaxSpeed;


    public float antiRollFrontHorizontal = 5000f;
    public float antiRollRearHorizontal = 5000f;

    [Header("		#   Wheel Configurations #")]

    private float orgSteerAngle = 0f;
    public float highspeedsteerAngle = 25f;
    public float highspeedsteerAngleAtspeed = 110f;
    public float steerAngle = 38f;
    public colliders[] wheels = new colliders[4];
    // Gear Configs

    [HideInInspector] public float[] gears;
    public float[] targetSpeedForGear;
    public float[] maxSpeedForGear;
    public int totalGears = 6;
    [HideInInspector] public float orggearThreshold;
    [HideInInspector] public int gearNum = 1;
    [HideInInspector] public bool reverse = true;
    [HideInInspector] public bool NGear = false;
    [ShowOnly] public int direction = 1;
    [HideInInspector] internal bool reverseDrive = false;

    [Header("		#   Gear Configurations #")]

    [ShowOnly] public int currentGear = 0;
    [Range(0f, .5f)] public float gearDelay = .35f;
    [Range(0.001f, .95f)] public float gearThreshold = .85f;
    public bool automaticGearCurves = true;
    public bool automaticGearTargetSpeeds = true;
    [ShowOnly] public bool changingGear = false;


    [Header("		#  Clutch Configurations #")]

    [Range(.1f, .9f)] public float clutchInertia = .15f;

    [HideInInspector] public float gasInput = 0f;
    [HideInInspector] public float brakeInput = 0f;
    [HideInInspector] public float steerInput = 0f;
    [HideInInspector] public float handbrakeInput = 0f;
    [HideInInspector] public bool cutGas = false;
    [HideInInspector] public float clutchInput = 0f;
    [HideInInspector] public float idleInput = 0f;

    public delegate void PlayerSpawned(PABLO spawned);
    public static event PlayerSpawned playerSpawned;

    public delegate void onPlayerDestroyed(PABLO car);

    public static event onPlayerDestroyed OnPlayerDestroyed;

    private IM2 im2;

    [HideInInspector] public int vehicleTypeAssigner;
    

    [Header("GetCarProperties")]
    private CarShopData carShopData;

    public int currentRole;

    public int currentIndex;

    public GameObject rGear;

    public GameObject gearLabel;

    private float oldEngineTorque;
    
    [ShowOnly] public float localrotationX;
    private GameObject groundPoint;

    #endregion

    void Awake()
    {

        carShopData = ReadWriteAllRoles.ReadCarProp(carShopData);

        currentIndex=PlayerPrefs.GetInt("pointer");
        currentRole=PlayerPrefs.GetInt("rolePointer");

        IM = GetComponent<inputManager>();
        RB = GetComponent<Rigidbody>();
        im2 = GameObject.FindGameObjectWithTag("IM2").GetComponent<IM2>();

        RB.maxAngularVelocity = maxAngularVelocity;


        wheels = GetComponentsInChildren<colliders>();

        orgSteerAngle = steerAngle;

        SetTorqueCurves();
        SwitchVehicleCameraType();

        engineRunning = true;

        int currentSpeedLevel = carShopData.roleItems[currentRole].shopItems[currentIndex].unlockedSpeedLevel;
        maxspeed = carShopData.roleItems[currentRole].shopItems[currentIndex].carLevel[currentSpeedLevel].speedValue;

        int currentBrakingLevel = carShopData.roleItems[currentRole].shopItems[currentIndex].unlockedBrakingLevel;
        brakeTorque += (carShopData.roleItems[currentRole].shopItems[currentIndex].carLevel[currentBrakingLevel].brakingValue) * 500;

        int currentAccelerationLevel = carShopData.roleItems[currentRole].shopItems[currentIndex].unlockedAccelerationLevel;
        engineTorque += (carShopData.roleItems[currentRole].shopItems[currentIndex].carLevel[currentAccelerationLevel].accelerationValue) * 50;

        int currentHandlingLevel = carShopData.roleItems[currentRole].shopItems[currentIndex].unlockedHandlingLevel;
        wheels[0].fStiffness += (carShopData.roleItems[currentRole].shopItems[currentIndex].carLevel[currentHandlingLevel].accelerationValue) * 0.2f;
        wheels[1].fStiffness += (carShopData.roleItems[currentRole].shopItems[currentIndex].carLevel[currentHandlingLevel].accelerationValue) * 0.2f;
        wheels[2].fStiffness += (carShopData.roleItems[currentRole].shopItems[currentIndex].carLevel[currentHandlingLevel].accelerationValue) * 0.2f;
        wheels[3].fStiffness += (carShopData.roleItems[currentRole].shopItems[currentIndex].carLevel[currentHandlingLevel].accelerationValue) * 0.2f;
        wheels[0].sStiffness += (carShopData.roleItems[currentRole].shopItems[currentIndex].carLevel[currentHandlingLevel].accelerationValue) * 0.2f;
        wheels[1].sStiffness += (carShopData.roleItems[currentRole].shopItems[currentIndex].carLevel[currentHandlingLevel].accelerationValue) * 0.2f;
        wheels[2].sStiffness += (carShopData.roleItems[currentRole].shopItems[currentIndex].carLevel[currentHandlingLevel].accelerationValue) * 0.2f;
        wheels[3].sStiffness += (carShopData.roleItems[currentRole].shopItems[currentIndex].carLevel[currentHandlingLevel].accelerationValue) * 0.2f;

    }
    private GameObject speedoMeter;

    void Start()
    {
        speedoMeter = GameObject.FindGameObjectWithTag("speedoMeter");
        StartCoroutine(PlayerSpawn());
        groundPoint = GameObject.FindGameObjectWithTag("groundPoint");
        rGear.SetActive(false);

    }

    public void SwitchVehicleCameraType()
    {

        switch (CameraType)
        {
            case cameraType.c1:
                vehicleTypeAssigner = 1;
                break;
            case cameraType.g1a2:
                vehicleTypeAssigner = 2;
                break;
            case cameraType.b3:
                vehicleTypeAssigner = 3;
                break;
            case cameraType.b2:
                vehicleTypeAssigner = 4;
                break;
            case cameraType.f2:
                vehicleTypeAssigner = 5;
                break;
            case cameraType.b1:
                vehicleTypeAssigner = 6;
                break;
            case cameraType.f1:
                vehicleTypeAssigner = 7;
                break;
            case cameraType.g2g3:
                vehicleTypeAssigner = 8;
                break;
            case cameraType.c2:
                vehicleTypeAssigner = 9;
                break;
            case cameraType.a1:
                vehicleTypeAssigner = 10;
                break;
            case cameraType.golf:
                vehicleTypeAssigner = 11;
                break;
            case cameraType.a6:
                vehicleTypeAssigner = 12;
                break;
            case cameraType.connect:
                vehicleTypeAssigner = 13;
                break;

        }

    }
    
    void Update()
    {

        Inputs(im2.steer, im2.accel, im2.accel, im2.brake, im2.handbrake);
        if(Time.frameCount % 2 == 0)
        localrotationX = Mathf.Abs(transform.rotation.eulerAngles.x);
        RB.centerOfMass = COM.transform.localPosition;

    }

    void FixedUpdate()
    {

        Engine();
        GearBox();
        Clutch();
        RevLimiter();
        AntiRollBars();

    }

    void LateUpdate()
    {
        if((Time.frameCount % 8) == 0)
        DetectDistanceToGround();
        ResetCar();


    }

    public void Inputs(float steer, float accel, float Accel, float brake, float handbrake)
    {

        switch (ControlType)
        {
            case controlType.Touch:

                gasInput = accel = Mathf.Clamp(0, accel, 0);
                brakeInput = brake = Mathf.Clamp(0, brake, 0);
                steer = steerInput = Mathf.Clamp(steer, -1, 1);
                handbrake = handbrakeInput = Mathf.Clamp(0, handbrake, 0);
                break;

            case controlType.Keyboard:
                gasInput = Input.GetAxis(IM.verticalInput);
                brakeInput = Mathf.Clamp01(-Input.GetAxis(IM.verticalInput));
                handbrakeInput = Input.GetKey(IM.handbrakeKB) ? 1f : 0f;
                steerInput = Input.GetAxis(IM.horizontalInput);
                break;

        }

    }

    void AntiRollBars()
    {

        #region Horizontal

        WheelHit FrontWheelHit;

        float travelFL = 1.0f;
        float travelFR = 1.0f;

        bool groundedFL = wheels[0].wheelCollider.GetGroundHit(out FrontWheelHit);

        if (groundedFL)
            travelFL = (-wheels[0].transform.InverseTransformPoint(FrontWheelHit.point).y - wheels[0].wheelCollider.radius) / wheels[0].wheelCollider.suspensionDistance;

        bool groundedFR = wheels[1].wheelCollider.GetGroundHit(out FrontWheelHit);

        if (groundedFR)
            travelFR = (-wheels[1].transform.InverseTransformPoint(FrontWheelHit.point).y - wheels[1].wheelCollider.radius) / wheels[1].wheelCollider.suspensionDistance;

        float antiRollForceFrontHorizontal = (travelFL - travelFR) * antiRollFrontHorizontal;

        if (groundedFL)
            RB.AddForceAtPosition(wheels[0].transform.up * -antiRollForceFrontHorizontal, wheels[0].transform.position);
        if (groundedFR)
            RB.AddForceAtPosition(wheels[1].transform.up * antiRollForceFrontHorizontal, wheels[1].transform.position);

        WheelHit RearWheelHit;

        float travelRL = 1.0f;
        float travelRR = 1.0f;

        bool groundedRL = wheels[2].wheelCollider.GetGroundHit(out RearWheelHit);

        if (groundedRL)
            travelRL = (-wheels[2].transform.InverseTransformPoint(RearWheelHit.point).y - wheels[2].wheelCollider.radius) / wheels[2].wheelCollider.suspensionDistance;

        bool groundedRR = wheels[3].wheelCollider.GetGroundHit(out RearWheelHit);

        if (groundedRR)
            travelRR = (-wheels[3].transform.InverseTransformPoint(RearWheelHit.point).y - wheels[3].wheelCollider.radius) / wheels[3].wheelCollider.suspensionDistance;

        float antiRollForceRearHorizontal = (travelRL - travelRR) * antiRollRearHorizontal;

        if (groundedRL)
            RB.AddForceAtPosition(wheels[2].transform.up * -antiRollForceRearHorizontal, wheels[2].transform.position);
        if (groundedRR)
            RB.AddForceAtPosition(wheels[3].transform.up * antiRollForceRearHorizontal, wheels[3].transform.position);

        #endregion


    }
    
    IEnumerator PlayerSpawn()
    {
        Transform gearLabelGO = speedoMeter.transform.Find("gearLabel");
        Transform rGearGO = speedoMeter.transform.Find("rGear");
        
        currentGear = 1;
        changingGear = false;

        rGear = rGearGO.gameObject;
        gearLabel = gearLabelGO.gameObject;

        oldEngineTorque = engineTorque;

        if (playerSpawned != null)
            playerSpawned(this);
        
        yield break;

    }

    internal float _gasInput
    {
        get
        {


            if (!changingGear && !cutGas)
                return (direction == 1 ? Mathf.Clamp01(gasInput) : Mathf.Clamp01(brakeInput));
            else
                return 0f;

        }
        set { gasInput = value; }
    }

    internal float _steerInput
    {
        get
        {

            return steerInput;

        }
    }

    internal float _brakeInput
    {
        get
        {



            if (!cutGas)
                return (direction == 1 ? Mathf.Clamp01(brakeInput) : Mathf.Clamp01(gasInput));
            else
                return 0f;

        }
        set { brakeInput = value; }
    }
    public void SetTorqueCurves()
    {

        if (maxSpeedForGear == null)
            maxSpeedForGear = new float[totalGears];

        if (targetSpeedForGear == null)
            targetSpeedForGear = new float[totalGears - 1];

        if (maxSpeedForGear != null && maxSpeedForGear.Length != totalGears)
            maxSpeedForGear = new float[totalGears];

        if (targetSpeedForGear != null && targetSpeedForGear.Length != totalGears - 1)
            targetSpeedForGear = new float[totalGears - 1];

        for (int j = 1; j < totalGears; j++)
            maxSpeedForGear[j] = Mathf.Lerp(0f, maxspeed * 1f, (float)(j + 1) / (float)(totalGears));

        if (automaticGearTargetSpeeds)
        {

            for (int g = 0; g < totalGears - 1; g++)
                targetSpeedForGear[g] = Mathf.Lerp(0, maxspeed * 1.6f * Mathf.Lerp(0f, 1f, gearThreshold), ((float)(g + 1) / (float)(totalGears)));

        }

        if (automaticGearCurves)
        {

            if (orgMaxSpeed != maxspeed || orggearThreshold != gearThreshold)
            {

                engineTorqueCurve = new AnimationCurve[totalGears];

                currentGear = 0;

                for (int i = 0; i < engineTorqueCurve.Length; i++)
                    engineTorqueCurve[i] = new AnimationCurve(new Keyframe(0, 1));

                for (int i = 0; i < totalGears; i++)
                {

                    engineTorqueCurve[i].MoveKey(0, new Keyframe(0, Mathf.Lerp(1f, .05f, (float)(i + 1) / (float)totalGears)));
                    engineTorqueCurve[i].AddKey(Mathf.Lerp(0, maxspeed * 1, ((float)(i) / (float)(totalGears))), Mathf.Lerp(1f, .5f, ((float)(i) / (float)(totalGears))));
                    engineTorqueCurve[i].AddKey(Mathf.Lerp(0, maxspeed * 1, ((float)(i + 1) / (float)(totalGears))), .15f);
                    engineTorqueCurve[i].AddKey(Mathf.Lerp(0, maxspeed * 1, ((float)(i + 1) / (float)(totalGears))) * 2f, -3f);
                    engineTorqueCurve[i].postWrapMode = WrapMode.Clamp;



                    orgMaxSpeed = maxspeed;
                    orggearThreshold = gearThreshold;

                }

            }

        }

    }

    void Clutch()
    {

        if (engineRunning)
            idleInput = Mathf.Lerp(1f, 0f, baseEngineRPM / minRPM);
        else
            idleInput = 0f;

        if (gearNum == 0)
        {

            clutchInput = Mathf.Lerp(clutchInput, (Mathf.Lerp(1f, (Mathf.Lerp(clutchInertia, 0f, ((wheels[2].wheelRPMtoSpeed + wheels[3].wheelRPMtoSpeed) / 2f) / targetSpeedForGear[0])), Mathf.Abs(_gasInput))), Time.fixedDeltaTime * 5f);

        }
        else
        {

            if (changingGear)
                clutchInput = Mathf.Lerp(clutchInput, 1, Time.fixedDeltaTime * 5f);
            else
                clutchInput = Mathf.Lerp(clutchInput, 0, Time.fixedDeltaTime * 5f);

        }

        if (cutGas || handbrakeInput >= .1f)
            clutchInput = 1f;

        if (NGear)
            clutchInput = 1f;

        clutchInput = Mathf.Clamp01(clutchInput);

    }

    void GearBox()
    {

        //Reversing Bool.

        if (brakeInput > .9f && transform.InverseTransformDirection(RB.velocity).z < 1f && reverseDrive && !changingGear && direction != -1)
        {
            StartCoroutine(ChangeGear(0));


        }
        else if (brakeInput < .1f && transform.InverseTransformDirection(RB.velocity).z > -1f && direction == -1 && !changingGear)
            StartCoroutine(ChangeGear(1));



        if (currentGear < totalGears - 1 && !changingGear)
        {
            if (speed >= (targetSpeedForGear[currentGear] * 1.65f) && wheels[0].wheelCollider.rpm > 0)
            {

                StartCoroutine(ChangeGear(currentGear + 1));

            }
        }

        if (currentGear > 1)
        {

            if (!changingGear)
            {

                if (speed < (targetSpeedForGear[currentGear - 1] ) && direction != -1)
                {
                    StartCoroutine(ChangeGear(currentGear - 1));
                }
            }
        }
    }

    void Engine()
    {

        //Speed.
        speed = RB.velocity.magnitude * 6f;


        //Steer Limit.
        steerAngle = Mathf.Lerp(orgSteerAngle, highspeedsteerAngle, (speed / highspeedsteerAngleAtspeed));

        float wheelRPM = DrivingSystem == driveType.AWD ? (wheels[0].wheelRPMtoSpeed + wheels[1].wheelRPMtoSpeed) : (wheels[2].wheelRPMtoSpeed + wheels[3].wheelRPMtoSpeed);

        baseEngineRPM = Mathf.Clamp(Mathf.MoveTowards(baseEngineRPM, (maxRPM * 1.1f) *
            (Mathf.Clamp01(Mathf.Lerp(0f, 1f, (1f - clutchInput) *
                (((wheelRPM * direction) / 2f) / maxSpeedForGear[currentGear])) +
                (((_gasInput) * clutchInput) + idleInput)))
                                                     , engineInertia * 100f), 0f, maxRPM * 1.1f);

        engineRPM = Mathf.Lerp(engineRPM, baseEngineRPM, Mathf.Lerp(Time.fixedDeltaTime * 5f, Time.fixedDeltaTime * 50f, (baseEngineRPM / maxRPM)*10));

        //Auto Reverse Bool.
        if (reverse)
        {

            reverseDrive = true;

        }
        else
        {

            if (_brakeInput < .5f && speed < 5)
                reverseDrive = true;
            else if (_brakeInput > 0 && transform.InverseTransformDirection(RB.velocity).z > 1f)
                reverseDrive = false;

        }
        #region  drag
        if (gasInput == 0)
        {
            RB.drag = .075f;
        }
        else
        {
            RB.drag = 0;
        }
/*
        if ((localrotationX < 352f && localrotationX > 330))
        {
            engineTorque = Mathf.Lerp(engineTorque, oldEngineTorque + 500, Time.smoothDeltaTime / 10);

        }
        else if (localrotationX > 0 && localrotationX < 15 && direction == -1 && brakeInput > .5f)
        {

            engineTorque = Mathf.Lerp(engineTorque, oldEngineTorque + 800, Time.smoothDeltaTime);

        }

        else
            engineTorque = oldEngineTorque;*/

        #endregion

    }

    public IEnumerator ChangeGear(int gear = 1)
    {

        changingGear = true;

        yield return new WaitForSeconds(gearDelay);

        if (gear == 0)
        {

            rGear.SetActive(true);
            gearLabel.SetActive(false);

            if (!NGear)
                direction = -1;
            else
                direction = 0;

        }
        else
        {

            currentGear = gear;
            rGear.SetActive(false);
            gearLabel.SetActive(true);

            if (!NGear)
                direction = 1;
            else
                direction = 0;

        }

        changingGear = false;

    }

    private void RevLimiter()
    {

        if ((engineRPM >= maxRPM))
            cutGas = true;
        else if (engineRPM < (maxRPM * .95f))
            cutGas = false;

    }

    private void ResetCar()
    {

        if (speed < 5 && !RB.isKinematic)
        {

            if (transform.eulerAngles.z < 300 && transform.eulerAngles.z > 60)
            {
                resetTime += Time.deltaTime;
                if (resetTime > 3)
                {
                    transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
                    transform.position = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
                    resetTime = 0f;
                }
            }

        }

    }

    void DetectDistanceToGround(){

        if((transform.position.y - groundPoint.transform.position.y ) > 100)
        {

            engineTorque = 20;

        }
        else
        {

            engineTorque = oldEngineTorque;
            
        }
    }

}









