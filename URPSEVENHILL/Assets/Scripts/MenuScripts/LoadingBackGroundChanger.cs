using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBackGroundChanger : MonoBehaviour {
    public Sprite free;
    public Sprite bus;
    public Sprite garbage;
    public Sprite fire;
    public Sprite police;
    public Sprite taxi;
    public Sprite ambulance;

    void Start () {
        BackGroundChanger ();
    }

    private void BackGroundChanger () {
        switch (PlayerPrefs.GetInt ("rolePointer")) {
            case 0:
                GetComponent<Image> ().sprite = free;
                break;

            case 1:
                GetComponent<Image> ().sprite = bus;
                break;

            case 2:
                GetComponent<Image> ().sprite = garbage;
                break;

            case 3:
                GetComponent<Image> ().sprite = fire;
                break;

            case 4:
                GetComponent<Image> ().sprite = police;
                break;

            case 5:
                GetComponent<Image> ().sprite = ambulance;
                break;

            case 6:
                GetComponent<Image> ().sprite = taxi;
                break;

            default:
                Debug.Log ("Error");
                break;
        }
    }

}