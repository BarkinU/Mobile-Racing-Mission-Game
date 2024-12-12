using System.Collections;
using CarShopSystem;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public VehicleList vehicleList;

    [Header ("Car Settings")]
    private PABLO RR;
    private GameObject freeCar;
    public colliders wheelCol0;
    public colliders wheelCol1;
    public colliders wheelCol2;
    public colliders wheelCol3;
    public GameObject firstStartPosition;
    public GameObject secondStartPosition;
    private GameObject startPosition;
    public Image SWImage;
    private steeringWheelControl swControl;
    [ShowOnly] public float SWTimer = 0.0f;
    private Color swAlpha;
    private int seconds;
    private RectTransform swO;
    CarShopData carShopData;
    private int currentIndex;

    private void Awake () {
        if (PlayerPrefs.GetInt ("isFirst") == 1) {
            startPosition = firstStartPosition;
        } else {
            startPosition = secondStartPosition;
        }

        currentIndex = PlayerPrefs.GetInt ("pointer");

        Instantiate (vehicleList.vehicles[currentIndex], startPosition.transform.position, startPosition.transform.rotation);
        freeCar = GameObject.FindGameObjectWithTag ("Player");
        
        carShopData = ReadWriteAllRoles.ReadCarProp (carShopData);
        if (carShopData.roleItems[0].shopItems[currentIndex].haveModify == true) {
            wheelCol0 = freeCar.transform.GetChild (5).transform.GetChild (0).GetComponent<colliders> ();
            wheelCol1 = freeCar.transform.GetChild (5).transform.GetChild (1).GetComponent<colliders> ();
            wheelCol2 = freeCar.transform.GetChild (5).transform.GetChild (2).GetComponent<colliders> ();
            wheelCol3 = freeCar.transform.GetChild (5).transform.GetChild (3).GetComponent<colliders> ();
            StartCoroutine (CheckCarModify ());
        }

        RR = freeCar.GetComponent<PABLO> ();
        SWImage = GameObject.FindGameObjectWithTag ("steeringWheel").GetComponent<Image> ();
        swAlpha = SWImage.color;
        swControl = GameObject.FindGameObjectWithTag ("steeringWheel").GetComponent<steeringWheelControl> ();
        swO = GameObject.FindGameObjectWithTag ("steeringWheel").GetComponent<RectTransform> ();

        

    }

    void Update () {

        SteeringWheelInteractibility ();

    }
    void SteeringWheelInteractibility () {

        if (swControl.WheelBeingHeld == true) {
            SWTimer = 0f;

            swAlpha.a = 1f;
            swO.localScale = new Vector3 (1, 1, 1);

        } else {
            SWTimer += Time.deltaTime;
            seconds = (int) (SWTimer % 60);
            if (seconds > 1) {
                swAlpha.a = Mathf.Lerp (swAlpha.a, 0.5f, Time.smoothDeltaTime * 5f);
                swO.localScale = Vector3.Lerp (swO.localScale, new Vector3 (0.9f, .9f, .9f), Time.smoothDeltaTime * 5f);
            }

        }

        SWImage.color = swAlpha;

    }

    IEnumerator CheckCarModify () {
        yield return new WaitForSeconds (0.005f);

        if (carShopData.roleItems[0].shopItems[currentIndex].haveModify == true) {

            //check spoiler
            if (carShopData.roleItems[0].shopItems[currentIndex].haveSpoiler == true) {

                if (carShopData.roleItems[0].shopItems[currentIndex].lastSelectedSpoilerID != 0) {
                    freeCar.transform.GetChild (0).transform.GetChild (carShopData.roleItems[0].shopItems[currentIndex].lastSelectedSpoilerID - 1).gameObject.SetActive (true);
                }

            }

            //check skirts
            if (carShopData.roleItems[0].shopItems[currentIndex].haveSkirts == true) {
                if (carShopData.roleItems[0].shopItems[currentIndex].lastSelectedSkirtID != 0) {
                    freeCar.transform.GetChild (1).transform.GetChild (carShopData.roleItems[0].shopItems[currentIndex].lastSelectedSkirtID - 1).gameObject.SetActive (true);
                }

            }

            //check front sticker
            if (carShopData.roleItems[0].shopItems[currentIndex].haveFrontSticker == true) {

                if (carShopData.roleItems[0].shopItems[currentIndex].lastSelectedFrontStickerID != 0) {
                    freeCar.transform.GetChild (2).transform.GetChild (carShopData.roleItems[0].shopItems[currentIndex].lastSelectedFrontStickerID - 1).gameObject.SetActive (true);
                }

            }

            //check rims
            if (carShopData.roleItems[0].shopItems[currentIndex].haveRim == true) {

                if (carShopData.roleItems[0].shopItems[currentIndex].lastSelectedRimID != 0) {
                    freeCar.transform.GetChild (3).transform.GetChild (carShopData.roleItems[0].shopItems[currentIndex].lastSelectedRimID - 1).gameObject.SetActive (true);
                    freeCar.transform.GetChild (3).transform.GetChild (0).gameObject.SetActive (false);

                    wheelCol0.wheelModel = freeCar.transform.GetChild (3).transform.GetChild (carShopData.roleItems[0].shopItems[currentIndex].lastSelectedRimID - 1).transform.GetChild (0).transform;

                    wheelCol1.wheelModel = freeCar.transform.GetChild (3).transform.GetChild (carShopData.roleItems[0].shopItems[currentIndex].lastSelectedRimID - 1).transform.GetChild (1).transform;

                    wheelCol2.wheelModel = freeCar.transform.GetChild (3).transform.GetChild (carShopData.roleItems[0].shopItems[currentIndex].lastSelectedRimID - 1).transform.GetChild (2).transform;

                    wheelCol3.wheelModel = freeCar.transform.GetChild (3).transform.GetChild (carShopData.roleItems[0].shopItems[currentIndex].lastSelectedRimID - 1).transform.GetChild (3).transform;

                }

            }

            //check side sticker
            if (carShopData.roleItems[0].shopItems[currentIndex].haveSideSticker == true) {

                if (carShopData.roleItems[0].shopItems[currentIndex].lastSelectedSideStickerID != 0) {
                    freeCar.transform.GetChild (4).transform.GetChild (carShopData.roleItems[0].shopItems[currentIndex].lastSelectedSideStickerID - 1).gameObject.SetActive (true);
                }

            }

        }

    }

}