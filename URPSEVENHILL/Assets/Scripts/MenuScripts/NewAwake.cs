using System.Collections;
using System.Collections.Generic;
using CarShopSystem;
using RoleShopSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewAwake : MonoBehaviour
{


    [Header("Deafault Canvas")]
    public GameObject menuCanvas;

    [Header("Vehicle Select Canvas")]
    public GameObject vehicleSelectCanvas;
    public VehicleList listOfVehicles;
    public TempVehicleList tempListOfVehicles;
    public BusList busList;
    public AmbulanceList ambulanceList;
    public GarbageList garbageList;
    public PoliceList policeList;
    public FireList fireList;
    public TaxiList taxiList;
    public GameObject toRotate;
    public ColorPicker colorPicker;
    [HideInInspector] public int vehiclePointer = 0;

    [SerializeField]
    private CarShopUI shopSystem = null;

    public MenuCameraMovement menuCam;
    public CameraController cameraController;


    private void Awake()
    {
        menuCanvas.SetActive(true);
        vehicleSelectCanvas.SetActive(false);


        if (PlayerPrefs.GetInt("isGarage") == 0)
        {
            PlayerPrefs.SetInt("rolePointer", 0);
            PlayerPrefs.SetInt("isFirst", 0);
        }
        PlayerPrefs.SetInt("pointer", 0);
        
        colorPicker.PaintCars();
        


    }

    private void Start()
    {  
        tempListOfVehicles.tempVehicles = listOfVehicles.vehicles;
    }

    public void backButton()
    {
        vehicleSelectCanvas.SetActive(false);
        menuCanvas.SetActive(true);
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        shopSystem.currentIndex = 0;
        shopSystem.nextBtn.interactable = true;
        shopSystem.previousBtn.interactable = false;

    }

    public void menuCanvasStartButton()
    {
        menuCanvas.SetActive(false);
        vehicleSelectCanvas.SetActive(true);
    }

    public void vehicleSelectCanvasStartButton()
    {
        menuCanvas.SetActive(true);
        vehicleSelectCanvas.SetActive(false);
    }

    public void upgradesCanvasButton()
    {
        vehicleSelectCanvas.SetActive(false);

    }

    public void FreeDriveCar()
    {

    }
    public void busCar()
    {
        menuCam.isRoleCarSpawn = true;
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        PlayerPrefs.SetInt("rolePointer", 1);
        vehiclePointer = 0;
        GameObject childObject = Instantiate(busList.busVehicles[vehiclePointer], cameraController.views[8].position, toRotate.transform.rotation) as GameObject;
        PlayerPrefs.SetInt("pointer", vehiclePointer);

    }

    public void garbageCar()
    {
        PlayerPrefs.SetInt("rolePointer", 2);
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        vehiclePointer = 0;
        GameObject childObject = Instantiate(garbageList.garbageVehicles[vehiclePointer], cameraController.views[8].position, toRotate.transform.rotation) as GameObject;
        PlayerPrefs.SetInt("pointer", vehiclePointer);
    }
    public void fireCar()
    {
        PlayerPrefs.SetInt("rolePointer", 3);
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        vehiclePointer = 0;
        GameObject childObject = Instantiate(fireList.fireVehicles[vehiclePointer], cameraController.views[8].position, toRotate.transform.rotation) as GameObject;
        PlayerPrefs.SetInt("pointer", vehiclePointer);

    }

    public void policeCar()
    {
        PlayerPrefs.SetInt("rolePointer", 4);
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        vehiclePointer = 0;
        GameObject childObject = Instantiate(policeList.policeVehicles[vehiclePointer], cameraController.views[7].position, toRotate.transform.rotation) as GameObject;
        PlayerPrefs.SetInt("pointer", vehiclePointer);

    }
    public void ambulanceCar()
    {
        PlayerPrefs.SetInt("rolePointer", 5);
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        vehiclePointer = 0;
        GameObject childObject = Instantiate(ambulanceList.ambulanceVehicles[vehiclePointer], cameraController.views[8].position, toRotate.transform.rotation) as GameObject;
        PlayerPrefs.SetInt("pointer", vehiclePointer);

    }
    public void taxiCar()
    {
        PlayerPrefs.SetInt("rolePointer", 6);
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        vehiclePointer = 0;
        GameObject childObject = Instantiate(taxiList.taxiVehicles[vehiclePointer], cameraController.views[7].position, toRotate.transform.rotation) as GameObject;
        PlayerPrefs.SetInt("pointer", vehiclePointer);


    }

    public void CarSpawn()
    {
        PlayerPrefs.SetInt("rolePointer", 0);
        GameObject childObject = Instantiate(listOfVehicles.vehicles[vehiclePointer], cameraController.views[7].position, toRotate.transform.rotation) as GameObject;
        menuCam.isCarSpawn = true;
    }


    public void StartGameButtonFirst()
    {
        PlayerPrefs.SetInt("isFirst", 1);

        SceneLoader.Load(SceneLoader.Scene.RealScene);
        
    }

    public void StartGameButtonSecond()
    {
        PlayerPrefs.SetInt("isFirst", 0);

        SceneLoader.Load(SceneLoader.Scene.RealScene);
    }

    public void camNull()
    {
        menuCam.isCarSpawn = false;
        menuCam.isRoleCarSpawn = false;

        PlayerPrefs.SetInt("rolePointer", 0);
        PlayerPrefs.SetInt("pointer", 0);
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        listOfVehicles.vehicles = tempListOfVehicles.tempVehicles;
        cameraController.isRoleCam = false;
        cameraController.isFreecam = false;
        cameraController.positionChange = false;
        cameraController.rolePositionChange = false;
    }

    public void camStop()
    {
        menuCam.isCarSpawn = false;
        menuCam.isRoleCarSpawn = false;
    }

    public void roleCarToRoleUpgrade()
    {

    }

    public void QuitButton()
    {
        Application.Quit();
        Debug.Log("Quit");
    }



}