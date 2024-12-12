using System.Collections;
using CarShopSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GarageLogin : MonoBehaviour {
    public GameObject menuCanvas;
    public GameObject RoleGarageCanvas;
    public GameObject RoleGarageCanvasToBackMenu;
    public GameObject freeCanvas;
    public GameObject carCam;
    public GameObject otherCam;
    public GameObject menuBackGroundCanvas;
    public GameObject busRole;
    public GameObject garbageRole;
    public GameObject fireRole;
    public GameObject roleDrive;
    public GameObject roleBack;
    public GameObject policeRole;
    public GameObject ambulanceRole;
    public GameObject taxiRole;
    public GameObject colorChangerImage,roleMenuButtons,freeMenuButtons,startFreeButton,startRoleButton;

    public BusList busList;
    public AmbulanceList ambulanceList;
    public GarbageList garbageList;
    public PoliceList policeList;
    public FireList fireList;
    public TaxiList taxiList;
    public GameObject toRotate;
    public CarShopUI carShop;
    public MenuCameraMovement menuCam;
    public CameraController camCont;
    public GameObject moneySystem;

    // called zero
    void Awake () {
        Debug.Log ("Awake");

    }

    // called first
    void OnEnable () {
        Debug.Log ("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded (Scene scene, LoadSceneMode mode) {
        if (PlayerPrefs.GetInt ("isGarage") == 1) {
            
            RoleGarageCanvas.SetActive (true);
            freeCanvas.SetActive (true);
            menuCam.isRoleCarSpawn = true;
            carCam.SetActive (true);
            moneySystem.SetActive (true);
            menuBackGroundCanvas.SetActive (true);
            RoleGarageCanvasToBackMenu.SetActive (false);
            colorChangerImage.SetActive(false);
            menuCanvas.SetActive (false);
            roleDrive.SetActive(false);
            roleBack.SetActive(false);
            otherCam.SetActive (false);
            roleMenuButtons.SetActive (true);
            freeMenuButtons.SetActive (false);
            startFreeButton.SetActive(false);
            startRoleButton.SetActive(true);
          
            
            
            PlayerPrefs.SetInt ("isGarage", 0);
            PlayerPrefs.SetInt("NoMapSelection",1);

            if (PlayerPrefs.GetInt ("rolePointer") == 1) {
                camCont.camPosRole ();

                busRole.SetActive (true);
                garbageRole.SetActive (false);
                fireRole.SetActive (false);
                policeRole.SetActive (false);
                ambulanceRole.SetActive (false);
                taxiRole.SetActive (false);

                
                GameObject childObject = Instantiate (busList.busVehicles[0], camCont.views[8].position, toRotate.transform.rotation) as GameObject;
                PlayerPrefs.SetInt ("pointer", 0);
                carShop.ChangeListIntoRole ();

            }

            if (PlayerPrefs.GetInt ("rolePointer") == 2) {

                camCont.camPosRole ();

                busRole.SetActive (false);
                garbageRole.SetActive (true);
                fireRole.SetActive (false);
                policeRole.SetActive (false);
                ambulanceRole.SetActive (false);
                taxiRole.SetActive (false);

                
                GameObject childObject = Instantiate (garbageList.garbageVehicles[0], camCont.views[8].position, toRotate.transform.rotation) as GameObject;
                PlayerPrefs.SetInt ("pointer", 0);
                carShop.ChangeListIntoRole ();

            }

            if (PlayerPrefs.GetInt ("rolePointer") == 3) {

                camCont.camPosRole ();

                busRole.SetActive (false);
                garbageRole.SetActive (false);
                fireRole.SetActive (true);
                policeRole.SetActive (false);
                ambulanceRole.SetActive (false);
                taxiRole.SetActive (false);

                
                GameObject childObject = Instantiate (fireList.fireVehicles[0], camCont.views[8].position, toRotate.transform.rotation) as GameObject;
                PlayerPrefs.SetInt ("pointer", 0);
                carShop.ChangeListIntoRole ();

            }

            if (PlayerPrefs.GetInt ("rolePointer") == 4) {

                camCont.camSmallRole ();

                busRole.SetActive (false);
                garbageRole.SetActive (false);
                fireRole.SetActive (false);
                policeRole.SetActive (true);
                ambulanceRole.SetActive (false);
                taxiRole.SetActive (false);

                
                GameObject childObject = Instantiate (policeList.policeVehicles[0],camCont.views[7].position, toRotate.transform.rotation) as GameObject;
                PlayerPrefs.SetInt ("pointer", 0);
                carShop.ChangeListIntoRole ();

            }

            if (PlayerPrefs.GetInt ("rolePointer") == 5) {


                camCont.camPosRole ();

                busRole.SetActive (false);
                garbageRole.SetActive (false);
                fireRole.SetActive (false);
                policeRole.SetActive (false);
                ambulanceRole.SetActive (true);
                taxiRole.SetActive (false);

                
                GameObject childObject = Instantiate (ambulanceList.ambulanceVehicles[0], camCont.views[8].position, toRotate.transform.rotation) as GameObject;
                PlayerPrefs.SetInt ("pointer", 0);
                carShop.ChangeListIntoRole ();

            }

            if (PlayerPrefs.GetInt ("rolePointer") == 6) {

                camCont.camSmallRole ();

                busRole.SetActive (false);
                garbageRole.SetActive (false);
                fireRole.SetActive (false);
                policeRole.SetActive (false);
                ambulanceRole.SetActive (false);
                taxiRole.SetActive (true);

                
                GameObject childObject = Instantiate (taxiList.taxiVehicles[0], camCont.views[7].position, toRotate.transform.rotation) as GameObject;
                PlayerPrefs.SetInt ("pointer", 0);
                carShop.ChangeListIntoRole ();
            }

        }
    }

    // called third
    void Start () {
        Debug.Log ("Start");
    }

    // called when the game is terminated
    void OnDisable () {
        Debug.Log ("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}