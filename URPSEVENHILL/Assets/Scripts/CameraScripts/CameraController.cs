using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform[] views;
    public float transitionSpeed;
    public Transform currentView;
    public bool positionChange;
    public bool rolePositionChange;
    public bool isFreecam;
    public bool isRoleCam;
    public bool isSmallRoleCam;
    public Vector3 farCam;
    private float distanceCam;
    public bool isModify;
    public Camera cameraSettings;
    

    void LateUpdate () {
        CameraChanging ();
    }

    public void BuyCarCameraPosition () {
        if (isFreecam == true) {
            currentView = views[0];
            farCam = currentView.position + new Vector3 (0.001f, 0.001f, 0.001f);
            positionChange = true;
            isModify=false;
        } else if (isRoleCam == true) {
            currentView = views[3];
            farCam = currentView.position + new Vector3 (0.001f, 0.001f, 0.001f);
            rolePositionChange = true;
        }else if(isSmallRoleCam==true)
        {
            currentView = views[5];
            farCam = currentView.position + new Vector3 (0.001f, 0.001f, 0.001f);
            rolePositionChange = true;
        }

    }

    public void UpgradeCarCameraPosition () {
        if (isFreecam == true) {
            currentView = views[1];
            farCam = currentView.position + new Vector3 (0.001f, 0.001f, 0.001f);
            positionChange = true;
        } else if (isRoleCam) {
            currentView = views[4];
            farCam = currentView.position + new Vector3 (0.001f, 0.001f, 0.001f);
            rolePositionChange = true;
        }else if(isSmallRoleCam==true)
        {
            currentView = views[6];
            farCam = currentView.position + new Vector3 (0.001f, 0.001f, 0.001f);
            rolePositionChange = true;
        }
    }
    public void ChangeColorCameraPosition () {
        if (isFreecam) {
            currentView = views[2];
            farCam = currentView.position + new Vector3 (0.001f, 0.001f, 0.001f);
            positionChange = true;
        }

    }

    public void ModifyViewTransition () {
        if (isFreecam) {
            currentView = views[9];
            farCam = currentView.position + new Vector3 (0.001f, 0.001f, 0.001f);
            positionChange = true;
            isModify=true;
            
    }
    }

    public void RimsAndSideViewTransition () {
        if (isFreecam) {
            currentView = views[10];
            farCam = currentView.position + new Vector3 (0.001f, 0.001f, 0.001f);
            positionChange = true;
            
            
    }
    }

    public void FrontStickerViewTransition () {
        if (isFreecam) {
            currentView = views[13];
            farCam = currentView.position + new Vector3 (0.001f, 0.001f, 0.001f);
            positionChange = true;
            
            
    }
    }

    public void SkirtViewTransition () {
        if (isFreecam) {
            currentView = views[11];
            farCam = currentView.position + new Vector3 (0.001f, 0.001f, 0.001f);
            positionChange = true;
            
            
    }
    }

    public void SpoilerViewTransition () {
        if (isFreecam) {
            currentView = views[12];
            farCam = currentView.position + new Vector3 (0.001f, 0.001f, 0.001f);
            positionChange = true;
            
            
    }
    }

    

    public void camPosFree () {
        transform.position = views[0].position;
        transform.rotation = views[0].rotation;
        isFreecam = true;
    }
    public void camSmallRole()
    {
        transform.position = views[5].position;
        transform.rotation = views[5].rotation;
        isSmallRoleCam = true;
        isRoleCam=false;
    }
    public void camPosRole () {
        transform.position = views[3].position;
        transform.rotation = views[3].rotation;
        isRoleCam = true;
        isSmallRoleCam=false;
    }

    private void CameraChanging () {
        if (positionChange == true || rolePositionChange == true) {
            transform.position = Vector3.Lerp (transform.position, farCam, Time.deltaTime * transitionSpeed);
            Vector3 currentAngle = new Vector3 (
                Mathf.LerpAngle (transform.rotation.eulerAngles.x, currentView.transform.rotation.eulerAngles.x, Time.deltaTime * transitionSpeed),
                Mathf.LerpAngle (transform.rotation.eulerAngles.y, currentView.transform.rotation.eulerAngles.y, Time.deltaTime * transitionSpeed),
                Mathf.LerpAngle (transform.rotation.eulerAngles.z, currentView.transform.rotation.eulerAngles.z, Time.deltaTime * transitionSpeed));

            distanceCam = Vector3.Distance (transform.position, farCam);
            transform.eulerAngles = currentAngle;
            if (distanceCam <= 0.002f) {
                positionChange = false;
                rolePositionChange = false;
            }
        }
    }

}