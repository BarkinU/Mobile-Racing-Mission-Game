using System.Collections;
using UnityEngine;

public class TaxiGetPassenger : MonoBehaviour {
    public GameObject customerDestination;
    public TaxiGameManager taxiGameManage;
    public GameObject[] firstCityCustomerDestinationsLocations;
    public GameObject[] secondCityCustomerDestinationsLocations;
    private float currentTime = 3.0f;
    public GameObject customerIndicator;
    public int randomPassengerLocation;
    public GameObject taxiPlayer;
    private float distancePlayerCustomer = 0f;

    private void Awake () {
        taxiPlayer = GameObject.FindGameObjectWithTag ("Player");
        taxiGameManage = GameObject.FindObjectOfType<TaxiGameManager> ();
        firstCityCustomerDestinationsLocations = taxiGameManage.FirstCityCustomerLocations;
        secondCityCustomerDestinationsLocations = taxiGameManage.SecondCityCustomerLocations;
        taxiGameManage.totalSatisfaction = 100;

    }

    private void SpawnCustomerDestination () {
        if (PlayerPrefs.GetInt ("isFirst") == 1) {
            do {
                randomPassengerLocation = Random.Range (0, firstCityCustomerDestinationsLocations.Length);
                distancePlayerCustomer = Vector3.Distance (taxiPlayer.gameObject.transform.position, firstCityCustomerDestinationsLocations[randomPassengerLocation].transform.position);
                Debug.Log (distancePlayerCustomer);

            } while (distancePlayerCustomer < taxiGameManage.longDistanceMeterValue);

            GameObject.Instantiate (customerDestination, firstCityCustomerDestinationsLocations[randomPassengerLocation].transform.position, Quaternion.identity);
            firstCityCustomerDestinationsLocations[randomPassengerLocation].transform.position += new Vector3 (0.0f, 200.0f, 0.0f);
            GameObject.Instantiate (customerIndicator, firstCityCustomerDestinationsLocations[randomPassengerLocation].transform.position, Quaternion.Euler (90, 0, 0));

            float timeCalculate = Vector3.Distance (GameObject.FindGameObjectWithTag("Player").transform.position, firstCityCustomerDestinationsLocations[randomPassengerLocation].transform.position);
            taxiGameManage.remainingTime = (timeCalculate / 10) + 15;

            taxiGameManage.isCustomerInCar=true;
            taxiGameManage.navigationScript.getSecondMission=true;
            taxiGameManage.cateringButton.interactable=true;

        } else {

            do {

                randomPassengerLocation = Random.Range (0, secondCityCustomerDestinationsLocations.Length);
                distancePlayerCustomer = Vector3.Distance (taxiPlayer.gameObject.transform.position, secondCityCustomerDestinationsLocations[randomPassengerLocation].transform.position);
                Debug.Log (distancePlayerCustomer);

            } while (distancePlayerCustomer < taxiGameManage.longDistanceMeterValue);

            GameObject.Instantiate (customerDestination, secondCityCustomerDestinationsLocations[randomPassengerLocation].transform.position, Quaternion.identity);
            secondCityCustomerDestinationsLocations[randomPassengerLocation].transform.position += new Vector3 (0.0f, 200.0f, 0.0f);
            GameObject.Instantiate (customerIndicator, secondCityCustomerDestinationsLocations[randomPassengerLocation].transform.position, Quaternion.Euler (90, 0, 0));

            float timeCalculate = Vector3.Distance (GameObject.FindGameObjectWithTag("Player").transform.position, secondCityCustomerDestinationsLocations[randomPassengerLocation].transform.position);
            taxiGameManage.remainingTime = (timeCalculate / 10) + 15;

            taxiGameManage.isCustomerInCar=true;
            taxiGameManage.navigationScript.getSecondMission=true;
            taxiGameManage.cateringButton.interactable=true;
        }

    }

    private void OnTriggerStay (Collider oyuncu) {
        if (oyuncu.tag == "Player") {
            currentTime -= 1 * Time.deltaTime;
            if (currentTime <= 0) {

                SpawnCustomerDestination ();
                Destroy (GameObject.FindGameObjectWithTag ("CustomerFollower"));
                Destroy (this.gameObject);
                Destroy (GameObject.FindGameObjectWithTag ("Finish"));
            }
        }
    }

}