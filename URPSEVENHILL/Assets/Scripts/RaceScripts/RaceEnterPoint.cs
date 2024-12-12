using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class RaceEnterPoint : MonoBehaviour
{

    #region Definitions

    [Header("                 Race & Player Objects")]
    public GameObject raceQuestion;
    public GameObject raceSpeedLimit;
    private PABLO player;
    public GameObject playerPosition;
    public GameObject aiPosition;
    private GameObject aiSelectedCar;



    [Header("                         Difficulties")]
    public Button zorDriver;
    public Button ortaDriver;
    public Button kolayDriver;
    public Button zorAraba;
    public Button ortaAraba;
    public Button kolayAraba;
     public GameObject DriverDifficultyText;
    public GameObject CarDifficultyText;
    [ShowOnly]public int driverDifficultLevel;
    [ShowOnly]public int carDifficultyLevel;



    [Header("                      Bools & Others")]

    public Button yesButton;
    public Button noButton;
    public bool checker = false;
    private Vector3 oldScale;
    public TextMeshProUGUI transitionTimeTEXT;
    public bool coroutineChecker = false;
    [Range(0,100)]public float lerpTime;
    public float realTime = 3f;
    public Rigidbody RB;
    public Rigidbody aiRB;
    private bool realTimeBool = false;
    public TextMeshProUGUI startingInSeconds;
    public GameObject[] fastCars;
    public GameObject[] normalCars;
    public GameObject[] slowCars;

    public WheelFrictionCurve frictionRace;
    public GameObject videoCamera;
    public GameObject panel;
    public Transform racePath;
    private float timer;
    private GameObject raceInstantiate;


    [Header("                   Race Checkpoints ")]
    public LineRenderer raceLineRenderer;
    public GameObject[] wayPointMarkers;
    public GameObject raceColliders;
    private BoxCollider raceScriptCollider;
    public bool RaceFinished = false;
    public RaceFinishState finishState;
    private int finalPosition;
    public TextMeshProUGUI winner,gainedMoneyFromRace;
    public int number = 0;
    public GameObject aiSpawner;
    public GameObject[] catPoints;
    private Vector3 distance;
    public float distanceMagnitude;
    public bool raceStarted = false;
    public bool raceStarted2 = false;
    private CarEngine carEngineScript;
    private float outOfWayTimer;
    private float resetTime;
    public GameObject startCollider;
    public GameObject starterCube;
    private float retarder;
    public WheelCollider[] wheelColliders;
    public Button quitRace;
    public GameObject quitQuestion;
    private float carTimer;
    public Button QuitYesButton;
    public Button QuitNoButton;
    public int globalQuitInteger;
    public TextMeshProUGUI finalizingRace;
    private float finalizingCount = 3;
    public MeshRenderer thisMesh;
    #endregion

    private void Start(){

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PABLO>();
        RB =GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        raceScriptCollider = GetComponent<BoxCollider>();
        RaceFinished = false;
        
    }

    void LateUpdate()
    {
    

        if(raceStarted2)
        {
            startCollider.SetActive(true);
            if(lerpTime<=100 && coroutineChecker == false)
            StartCoroutine(TransitionTime());
        
            if(lerpTime>=100 && coroutineChecker == true){
            StopCoroutine(TransitionTime());
            
            }
        }

        if(realTimeBool)
            {
            
                realTime -= Time.deltaTime;

                startingInSeconds.gameObject.SetActive(true);
                startingInSeconds.text = " - " + Mathf.Round(realTime) + " - " ;

                carEngineScript.path = racePath;
                carEngineScript.enabled = true;
                
                WayPointFunction();

                if(realTime <= 0)
                {
                    raceStarted = true;
                    aiRB = raceInstantiate.GetComponent<Rigidbody>();
                    RB.constraints = RigidbodyConstraints.None;
                    aiRB.constraints = RigidbodyConstraints.None;
                    
                    realTime = 3f;
                    realTimeBool = false;

                    startingInSeconds.gameObject.SetActive(false);

                    carEngineScript.path = racePath;
                    
                }
            }

            if(raceStarted){

  
                raceStarted2 = false;

        }
            if(globalQuitInteger == 1 ){

            finalizingCount -= Time.deltaTime;
            finalizingRace.gameObject.SetActive(true);
            finalizingRace.text = "Yarış " + "<b><i>" + Mathf.Round(finalizingCount) + "</i></b>"  + " saniye içinde bitiyor...";
            Time.timeScale = 1;
            
            if(finalizingCount < 0)
            {
                finalizingRace.gameObject.SetActive(false);
                finalizingCount = 3;
                globalQuitInteger = 0;

            }

            }
            if(globalQuitInteger ==2){
            Time.timeScale = 1;
            globalQuitInteger = 0;
            }


    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.CompareTag("Player") && checker == false)
        {
        
            raceSpeedLimit.gameObject.SetActive(true);
            RaceFinished = false;

        }
    }

    private void OnTriggerStay(Collider other){

        if(other.CompareTag("Player"))
        {
            Vector3 oldScale;
            if(player.speed < 10 && raceSpeedLimit.gameObject.activeSelf)
            {

                raceSpeedLimit.gameObject.SetActive(false);
                checker = true;

            }
            
            if(player.speed < 10 && checker == true && !zorAraba.gameObject.activeSelf)
            {

                raceQuestion.gameObject.SetActive(true);

                oldScale = raceQuestion.transform.localScale;
                raceQuestion.transform.localScale = new Vector3(Mathf.Lerp(raceQuestion.transform.localScale.x,1,Time.deltaTime*2),Mathf.Lerp(raceQuestion.transform.localScale.y,1,Time.deltaTime*2),0);

                yesButton.onClick.AddListener(YesButtonOnClick);
                noButton.onClick.AddListener(NoButtonOnClick);
 
                yesButton.gameObject.SetActive(true);
                noButton.gameObject.SetActive(true);


            }
            if(player.speed >10)
            {
    
                raceQuestion.gameObject.SetActive(false);
                raceSpeedLimit.gameObject.SetActive(true);

            }
            
        }

    }
   
    private void OnTriggerExit(Collider other)
    {

        if(other.CompareTag("Player"))
        {

            raceQuestion.gameObject.SetActive(false);
            zorDriver.gameObject.SetActive(false);
            ortaDriver.gameObject.SetActive(false);
            kolayDriver.gameObject.SetActive(false);
            zorAraba.gameObject.SetActive(false);
            ortaAraba.gameObject.SetActive(false);
            kolayAraba.gameObject.SetActive(false);
            yesButton.gameObject.SetActive(false);
            noButton.gameObject.SetActive(false);
            CarDifficultyText.gameObject.SetActive(false);
            DriverDifficultyText.gameObject.SetActive(false);
            raceSpeedLimit.gameObject.SetActive(false);
            CarDifficultySelection(0);
            DriverDifficultySelection(0);
            raceQuestion.transform.localScale = oldScale;
            transitionTimeTEXT.gameObject.SetActive(false);
            coroutineChecker = false;
            
        }
    }
    
    void YesButtonOnClick()
    {

        zorDriver.gameObject.SetActive(true);
        ortaDriver.gameObject.SetActive(true);
        kolayDriver.gameObject.SetActive(true);

        zorAraba.gameObject.SetActive(true);
        ortaAraba.gameObject.SetActive(true);
        kolayAraba.gameObject.SetActive(true);

        CarDifficultyText.gameObject.SetActive(true);
        DriverDifficultyText.gameObject.SetActive(true);

        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);

        raceQuestion.gameObject.SetActive(false);
        raceQuestion.gameObject.SetActive(false);
        raceSpeedLimit.gameObject.SetActive(false);

        checker = false;

        zorAraba.onClick.AddListener(delegate() {CarDifficultySelection(3);}); // used functions as a variable to detect difficulties
        ortaAraba.onClick.AddListener(delegate() {CarDifficultySelection(2);}); 
        kolayAraba.onClick.AddListener(delegate() {CarDifficultySelection(1);});
        
        zorDriver.onClick.AddListener(delegate() {DriverDifficultySelection(3);});
        ortaDriver.onClick.AddListener(delegate() {DriverDifficultySelection(2);});
        kolayDriver.onClick.AddListener(delegate() {DriverDifficultySelection(1);});

        quitRace.onClick.AddListener(QuitButton);
        QuitYesButton.onClick.AddListener(delegate() {QuitRace(1);});
        QuitNoButton.onClick.AddListener(delegate() {QuitRace(2);});

        GameObject[] allAi = GameObject.FindGameObjectsWithTag("aii");


        int j;
        for(j=0;j<allAi.Length;j++)
        Destroy(allAi[j]);

        int i;
        for(i = 0; i<catPoints.Length;i++){
        catPoints[i].SetActive(false);
        }
        
        Time.timeScale = 0.1f;



    }

    void QuitButton(){

        Time.timeScale=0.01f;
        QuitYesButton.gameObject.SetActive(true);
        QuitNoButton.gameObject.SetActive(true);
        quitQuestion.gameObject.SetActive(true);


    }

    void NoButtonOnClick(){

            zorDriver.gameObject.SetActive(false);
            ortaDriver.gameObject.SetActive(false);
            kolayDriver.gameObject.SetActive(false);
            zorAraba.gameObject.SetActive(false);
            ortaAraba.gameObject.SetActive(false);
            kolayAraba.gameObject.SetActive(false);
            raceQuestion.gameObject.SetActive(false);
            raceSpeedLimit.gameObject.SetActive(false);
            yesButton.gameObject.SetActive(false);
            noButton.gameObject.SetActive(false);
            CarDifficultyText.gameObject.SetActive(false);
            DriverDifficultyText.gameObject.SetActive(false);
            checker = false;
            CarDifficultySelection(0);
            DriverDifficultySelection(0);
            yesButton.gameObject.SetActive(false);
            noButton.gameObject.SetActive(false);

    }

    public void CarDifficultySelection(int GetDifficulty=0)
    {

        carDifficultyLevel = GetDifficulty;

        if(carDifficultyLevel != 0){

            SpecifiedCar();
           
            zorAraba.gameObject.SetActive(false);
            ortaAraba.gameObject.SetActive(false);
            kolayAraba.gameObject.SetActive(false);

            CarDifficultyText.gameObject.SetActive(false);
            zorDriver.gameObject.GetComponent<Button>().interactable = true;
            ortaDriver.gameObject.GetComponent<Button>().interactable = true;
            kolayDriver.gameObject.GetComponent<Button>().interactable = true;

        }

    }

    public void DriverDifficultySelection(int GetDifficulty=0)
    {

        driverDifficultLevel = GetDifficulty;

        if(GetDifficulty != 0){
            if(!raceInstantiate)
            {
            SpecifiedDriver();


            }

            zorDriver.gameObject.SetActive(false);
            ortaDriver.gameObject.SetActive(false);
            kolayDriver.gameObject.SetActive(false);
            DriverDifficultyText.gameObject.SetActive(false);

        }

    }
    void RaceTransition(){

        transitionTimeTEXT.gameObject.SetActive(false);

        player.transform.position = playerPosition.gameObject.transform.position;
        player.transform.rotation = playerPosition.gameObject.transform.rotation;




   
        raceColliders.SetActive(true);

        raceScriptCollider.enabled = false; 
        
        aiSpawner.gameObject.SetActive(false);

        finishState.gameObject.SetActive(true);
        thisMesh.enabled = false;
        finishState.finishPosition = 0;
       

    }
    
    IEnumerator TransitionTime(){

        if(driverDifficultLevel !=0 && carDifficultyLevel != 0){
        transitionTimeTEXT.gameObject.SetActive(true);
        lerpTime += Mathf.Lerp(0,100,Time.deltaTime* Random.Range(1f,3f));
        transitionTimeTEXT.text = ("Yarış hazırlanıyor % " + Mathf.Round(lerpTime) +"...");
        var videoPlayer = videoCamera.GetComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.Play();
        panel.SetActive(true);
                        
            if(lerpTime >= 100){

                Time.timeScale=1;
                coroutineChecker = true;
                lerpTime = 0;
                videoPlayer.Stop();
                panel.SetActive(false);
                realTimeBool = true;


                
                quitRace.gameObject.SetActive(true);
                RaceTransition();

                yield return new WaitForSeconds(.1f);
                RB.constraints = RigidbodyConstraints.FreezeAll;
                aiRB = raceInstantiate.GetComponent<Rigidbody>();
                aiRB.constraints = RigidbodyConstraints.FreezeAll;

            }

        }

    }
    
    void SpecifiedCar(){

        if(carDifficultyLevel == 1)
        {

            aiSelectedCar = slowCars[Random.Range(0,4)];

        }

        else if (carDifficultyLevel == 2)
        {

            aiSelectedCar = normalCars[Random.Range(0,4)];

        }

        else if (carDifficultyLevel == 3)
        {

            aiSelectedCar = fastCars[Random.Range(0,4)];

        }

    }
    
    void SpecifiedDriver(){
        
        if(driverDifficultLevel == 1 )
        {
            
            raceInstantiate = Instantiate(aiSelectedCar,aiPosition.transform.position + Vector3.up,aiPosition.transform.rotation);                            
            carEngineScript = raceInstantiate.GetComponent<CarEngine>();

            wheelColliders = raceInstantiate.GetComponentsInChildren<WheelCollider>();

            carEngineScript.enabled = false;

            carEngineScript.maxSpeed +=(carDifficultyLevel * 80);
            carEngineScript.maxBrakeTorque +=1000;
            carEngineScript.maxMotorTorque +=(carDifficultyLevel * 75f);

            frictionRace = carEngineScript.wheelFL.sidewaysFriction;
            frictionRace.stiffness = frictionRace.stiffness+1;

            carEngineScript.sensorLenght = 20f;

            raceStarted2 = true;
            
        }

        else if(driverDifficultLevel == 2)
        {

            raceInstantiate = Instantiate(aiSelectedCar,aiPosition.transform.position+ Vector3.up,aiPosition.transform.rotation);                            

            carEngineScript = raceInstantiate.GetComponent<CarEngine>();

            wheelColliders = raceInstantiate.GetComponentsInChildren<WheelCollider>();
            
            carEngineScript.enabled = false;

            carEngineScript.maxSpeed +=(carDifficultyLevel * 80);
            carEngineScript.maxBrakeTorque +=3000;
            carEngineScript.maxMotorTorque +=(carDifficultyLevel * 75f);


            frictionRace = carEngineScript.wheelFL.sidewaysFriction;
            frictionRace.stiffness = frictionRace.stiffness+2;

            carEngineScript.sensorLenght = 30f;

            raceStarted2 = true;

        }
        
        else if(driverDifficultLevel == 3)
        {

            raceInstantiate = Instantiate(aiSelectedCar,aiPosition.transform.position+ Vector3.up,aiPosition.transform.rotation);                            
            carEngineScript = raceInstantiate.GetComponent<CarEngine>();

            wheelColliders = raceInstantiate.GetComponentsInChildren<WheelCollider>();

            carEngineScript.enabled = false;

            carEngineScript.maxSpeed +=(carDifficultyLevel * 80);
            carEngineScript.maxBrakeTorque +=5000;
            carEngineScript.maxMotorTorque +=(carDifficultyLevel * 75f);

            frictionRace = carEngineScript.wheelFL.sidewaysFriction;
            frictionRace.stiffness = frictionRace.stiffness+3;

            carEngineScript.sensorLenght = 40f;

            raceStarted2 = true;
            
        }
        

         
    }
    
    void WayPointFunction(){

        raceLineRenderer.positionCount = wayPointMarkers.Length;

        int i;
        for(i = 0; i < raceLineRenderer.positionCount; i++){
            
            raceLineRenderer.SetPosition(i, new Vector3 (wayPointMarkers[i].transform.position.x, wayPointMarkers[i].transform.position.y, wayPointMarkers[i].transform.position.z));

        }

    }

    IEnumerator endCoroutine(){

        startCollider.SetActive(false);

        yield return new WaitForSeconds(3f);
        finalizingRace.gameObject.SetActive(false);
        winner.gameObject.SetActive(false);
        gainedMoneyFromRace.gameObject.SetActive(false);
        raceColliders.SetActive(false);
        raceStarted = false;
        raceLineRenderer.positionCount = 0;
        raceScriptCollider.enabled = true;

        Destroy(raceInstantiate);
        aiSpawner.gameObject.SetActive(true);
        winner.gameObject.SetActive(false);
        gainedMoneyFromRace.gameObject.SetActive(false);
        raceStarted2 = false;
        finishState.gameObject.SetActive(false);
        coroutineChecker = false;
        starterCube.SetActive(true);
        quitRace.gameObject.SetActive(false);
        globalQuitInteger = 0;
        thisMesh.enabled = false;
        finalizingRace.gameObject.SetActive(false);
        thisMesh.enabled = true;
        gameObject.SetActive(false); // last thing to do
             int i;
        for(i = 0; i<catPoints.Length;i++){
            catPoints[i].SetActive(true);
        }

    }

    public void RaceFinishedState(){
        
        winner.gameObject.SetActive(true);
        gainedMoneyFromRace.gameObject.SetActive(true);

        if(finishState.finishPosition == 1)
        {

            winner.text = "Yarışı Kazandın!";
            gainedMoneyFromRace.text="Yarış Kazancınız :"+finishState.totalRaceReward;
            gainedMoneyFromRace.color=Color.green;
            winner.color = Color.green;

        }

        else
        {

            winner.text = "Yarışı Kaybettin!";
            gainedMoneyFromRace.text="Yarıştan bir kazancın olmadı bir sonraki sefere!";
            winner.color = Color.red;
            gainedMoneyFromRace.color=Color.red;

        }

        
        int i;
        for(i = 0; i<catPoints.Length;i++){
            catPoints[i].SetActive(true);
        }

        StartCoroutine(endCoroutine());

    }

 

    void QuitRace(int quitInteger){

        if(quitInteger == 1)
        {
            Time.timeScale = 1f;
            globalQuitInteger = quitInteger;
            quitQuestion.gameObject.SetActive(false);
            QuitYesButton.gameObject.SetActive(false);
            QuitNoButton.gameObject.SetActive(false);
            StartCoroutine(endCoroutine());

        }

        else if ( quitInteger == 2)
        {
            
            globalQuitInteger = quitInteger;
            quitQuestion.gameObject.SetActive(false);
            QuitYesButton.gameObject.SetActive(false);
            QuitNoButton.gameObject.SetActive(false);

            Time.timeScale = 1;

        }
        
    }

}
