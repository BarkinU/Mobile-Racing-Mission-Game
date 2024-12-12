using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuCameraMovement : MonoBehaviour {
    [SerializeField] private Camera cam;
    private Transform target;

    private Vector3 previousPosition;
    public bool isCarSpawn;
    public bool isRoleCarSpawn;
    public CameraController cameraController;
    private float bigVehicleCamFar = -8.6f;
    private float smallVehicleCamFar = -3.7f;
    private float roleSmallCamFar = -5f;
    private float modifyCamCar=-2.5f;
    

    void Start () { }

    void Update () {
        if (Input.GetMouseButtonDown (0)) {
            if (EventSystem.current.IsPointerOverGameObject ()) {
                return;
            } 

            previousPosition = cam.ScreenToViewportPoint (Input.mousePosition);
            

        }

        if (Input.GetMouseButton (0)) {

            if (EventSystem.current.IsPointerOverGameObject ()) {
                return;
            }

            if (isCarSpawn) {
                if (cameraController.positionChange == false) {
                    target = GameObject.FindWithTag ("Player").transform;

                    Vector3 direction = previousPosition - cam.ScreenToViewportPoint (Input.mousePosition);

                    cam.transform.position = cameraController.views[7].transform.position;

                    cam.transform.Rotate (new Vector3 (100, 0, 0), direction.y);
                    cam.transform.Rotate (new Vector3 (0, 0.1f, 0), -direction.x * 180, Space.World);

                    if(cameraController.isModify==true)
                    {
                        cam.transform.Translate (new Vector3 (0, 0, modifyCamCar));
                    }else
                    {
                        cam.transform.Translate (new Vector3 (0, 0, smallVehicleCamFar));
                    }
                    
                    previousPosition = cam.ScreenToViewportPoint (Input.mousePosition);
                }

            } else if (isRoleCarSpawn) {
                if (cameraController.rolePositionChange == false) {
                    target = GameObject.FindWithTag ("Player").transform;

                    Vector3 direction = previousPosition - cam.ScreenToViewportPoint (Input.mousePosition);

                    cam.transform.position = cameraController.views[7].transform.position;

                    cam.transform.Rotate (new Vector3 (100, 0, 0), direction.y);
                    cam.transform.Rotate (new Vector3 (0, 0.1f, 0), -direction.x * 180, Space.World);

                    if (PlayerPrefs.GetInt ("rolePointer") == 6 || PlayerPrefs.GetInt ("rolePointer") == 4) {
                        cam.transform.Translate (new Vector3 (0, 0, roleSmallCamFar));
                    } else {
                        cam.transform.Translate (new Vector3 (1f, 0, bigVehicleCamFar));
                    }

                    previousPosition = cam.ScreenToViewportPoint (Input.mousePosition);
                }

            }
        }

    }
}