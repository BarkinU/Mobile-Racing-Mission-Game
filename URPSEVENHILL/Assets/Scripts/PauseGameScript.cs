using UnityEngine;
using System.Collections;

public class PauseGameScript : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public Camera mainCam;
    public GameObject pauseMenuUI;
    public GameObject pauseButton;
    public Camera bigMapcam;
    public GameObject gameCanvas,busCanvas,garbageCanvas,fireCanvas,policeCanvas,ambulanceCanvas,taxiCanvas,freeCanvas;
    private GameObject currentCanvas;
    public GameObject mapCanvas, mapCamObject;
    public GameObject mapImage;
    private Transform playerCarTrans;
    public GameObject minimapItemsInCanvas;
    public GameObject navSymbol;


    private float bigMapHeight = 1000;
    private int currentRole;

    private void Start()
    {
        playerCarTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        currentRole = PlayerPrefs.GetInt ("rolePointer");

        
        switch (currentRole) {
            case 0:
                currentCanvas=freeCanvas;
                break;

            case 1:
                currentCanvas=busCanvas;
                break;

            case 2:
                currentCanvas=garbageCanvas;
                break;

            case 3:
                currentCanvas=fireCanvas;
                break;

            case 4:
                currentCanvas=policeCanvas;
                break;

            case 5:
                currentCanvas=ambulanceCanvas;
                break;


            case 6:
                currentCanvas=taxiCanvas;
                break;

        }
        
        
        


    }
 

    public void OpenPauseMenu()
    {
        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;

    }

    void Pause()
    {
        pauseButton.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("isGarage", 0);
        PlayerPrefs.SetInt("rolePointer", 0);
        SceneLoader.Load(SceneLoader.Scene.AwakeScene);
        Debug.Log("LoadedMenu");
    }

    public void QuitGame()
    {
        PlayerPrefs.SetInt("rolePointer", 0);
        Debug.Log("ExitGame");
        Application.Quit();
    }

    public void CamOpen()
    {
        //  Time.timeScale = 0f;
        mapCamObject.SetActive(true);
        gameCanvas.SetActive(false);
        bigMapcam.enabled = true;
        mapCanvas.SetActive(true);
        mainCam.enabled = false;
        minimapItemsInCanvas.SetActive(false);
        mapImage.SetActive(true);
        pauseButton.SetActive(false);
        currentCanvas.SetActive(false);
        if(currentRole==0 || currentRole==2)
        {
            navSymbol.SetActive(true);
        }
        Vector3 newPosition = playerCarTrans.position;
        newPosition.y = playerCarTrans.position.y + bigMapHeight;
        bigMapcam.transform.position = newPosition;
        

        



    }

    public void CamClose()
    {

        gameCanvas.SetActive(true);
        bigMapcam.enabled = false;
        mapCanvas.SetActive(false);
        mainCam.enabled = true;
        minimapItemsInCanvas.SetActive(true);
        mapImage.SetActive(false);
        mapCamObject.SetActive(false);
        pauseButton.SetActive(true);
        currentCanvas.SetActive(true);
        Time.timeScale = 1f;


    }

}
