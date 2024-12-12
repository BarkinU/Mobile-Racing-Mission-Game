using System.Collections;
using System.IO;
using CarShopSystem;
using RoleShopSystem;
using UnityEngine;

public class SaveLoadData : MonoBehaviour {
    [SerializeField]
    private CarShopUI carShopUI = null;

    [SerializeField]
    private BusRoleShopUI busShopUI = null;
    [SerializeField]
    private GarbageRoleShopUI garbageShopUI = null;
    [SerializeField]
    private FireRoleShopUI fireShopUI = null;
    [SerializeField]
    private PoliceRoleShopUI policeShopUI = null;
    [SerializeField]
    private TaxiRoleShopUI taxiShopUI = null;
    [SerializeField]
    private AmbulanceRoleShopUI ambulanceShopUI = null;
    [SerializeField]
    private GameRoleShopUI gameRoleShopUI = null;
    [SerializeField]

    public static int key = 45298;

    public void Initialized () {
        if (PlayerPrefs.GetInt ("GameStarted") == 1) {
            LoadData ();
        } else {
            SaveData ();
            PlayerPrefs.SetInt ("GameStarted", 1);
        }
    }

    private void Update () {
#if UNITY_EDITOR        
        if (Input.GetKeyDown (KeyCode.Space)) {
            SaveData ();
        }

        if (Input.GetKeyDown (KeyCode.U)) {
            ClearData ();
        }
#endif
    }

    private void OnApplicationPause (bool pause) {
#if !UNITY_EDITOR        
        SaveData ();
#endif       
    }
    private void SaveData () {
        string carShopDataString = JsonUtility.ToJson (carShopUI.carShopData);

        string gameDataString = JsonUtility.ToJson (gameRoleShopUI.gameData);

        string busShopDataString = JsonUtility.ToJson (busShopUI.busShopData);

        string garbageShopDataString = JsonUtility.ToJson (garbageShopUI.garbageShopData);

        string fireShopDataString = JsonUtility.ToJson (fireShopUI.fireShopData);

        string policeShopDataString = JsonUtility.ToJson (policeShopUI.policeShopData);

        string taxiShopDataString = JsonUtility.ToJson (taxiShopUI.taxiShopData);

        string ambulanceShopDataString = JsonUtility.ToJson (ambulanceShopUI.ambulanceShopData);

        try {
            string carTempPath = Path.Combine (Application.persistentDataPath + "/CarShopData.txt");
            System.IO.File.WriteAllText (carTempPath, SecureHelper.EncryptDecrypt (carShopDataString, key));
            Debug.Log ("CarShopSaved");

            string gameTempPath = Path.Combine (Application.persistentDataPath + "/GameData.txt");
            System.IO.File.WriteAllText (gameTempPath, SecureHelper.EncryptDecrypt (gameDataString, key));
            Debug.Log ("GameDataSaved");

            string busTempPath = Path.Combine (Application.persistentDataPath + "/BusShopData.txt");
            Debug.Log ("BusShopSaved");
            System.IO.File.WriteAllText (busTempPath, SecureHelper.EncryptDecrypt (busShopDataString, key));

            string garbageTempPath = Path.Combine (Application.persistentDataPath + "/GarbageShopData.txt");
            System.IO.File.WriteAllText (garbageTempPath, SecureHelper.EncryptDecrypt (garbageShopDataString, key));
            Debug.Log ("GarbageShopSaved");

            string fireTempPath = Path.Combine (Application.persistentDataPath + "/FireShopData.txt");
            System.IO.File.WriteAllText (fireTempPath, SecureHelper.EncryptDecrypt (fireShopDataString, key));
            Debug.Log ("FireShopSaved");

            string policeTempPath = Path.Combine (Application.persistentDataPath + "/PoliceShopData.txt");
            System.IO.File.WriteAllText (policeTempPath, SecureHelper.EncryptDecrypt (policeShopDataString, key));
            Debug.Log ("PoliceShopSaved");

            string taxiTempPath = Path.Combine (Application.persistentDataPath + "/TaxiShopData.txt");
            System.IO.File.WriteAllText (taxiTempPath, SecureHelper.EncryptDecrypt (taxiShopDataString, key));
            Debug.Log ("TaxiShopSaved");

            string ambulanceTempPath = Path.Combine (Application.persistentDataPath + "/AmbulanceShopData.txt");
            System.IO.File.WriteAllText (ambulanceTempPath, SecureHelper.EncryptDecrypt (ambulanceShopDataString, key));
            Debug.Log ("AmbulanceShopSaved");

        } catch (System.Exception e) {
            Debug.Log ("Error Saving Data :" + e);
            throw;

        }
    }
    public void LoadData () {
        try {
            string checkCarShopData = Path.Combine (Application.persistentDataPath + "/CarShopData.txt");
            //if (System.IO.File.Exists(checkCarShopData)==false) {
            //!Directory.Exists(checkCarShopData)
            //|| PlayerPrefs.GetInt("loadOnce")==0
            if (System.IO.File.Exists (checkCarShopData) == false || PlayerPrefs.GetInt ("loadOnce") == 0) {
                busShopUI.busShopData = new BusShopData ();
                var busJsonOnce = Resources.Load<TextAsset> ("Records/BusShopData");
                string busOnce = busJsonOnce.text;
                busOnce = SecureHelper.EncryptDecrypt (busOnce, key);
                JsonUtility.FromJsonOverwrite (busOnce, busShopUI.busShopData);
                Debug.Log ("FirstinnnnnnnnnnnFirstttttttttttt");

                carShopUI.carShopData = new CarShopData ();
                var carJsonOnce = Resources.Load<TextAsset> ("Records/CarShopData");
                string carOnce = carJsonOnce.text;
                Debug.Log ("CarShopLoaded");
                carOnce = SecureHelper.EncryptDecrypt (carOnce, key);
                JsonUtility.FromJsonOverwrite (carOnce, carShopUI.carShopData);

                gameRoleShopUI.gameData = new GameData ();
                var gameJsonOnce = Resources.Load<TextAsset> ("Records/GameData");
                string gameJson = gameJsonOnce.text;
                Debug.Log ("GameDataLoaded");
                gameJson = SecureHelper.EncryptDecrypt (gameJson, key);
                JsonUtility.FromJsonOverwrite (gameJson, gameRoleShopUI.gameData);

                garbageShopUI.garbageShopData = new GarbageShopData ();
                var garbageJsonOnce = Resources.Load<TextAsset> ("Records/GarbageShopData");
                string garbageJson = garbageJsonOnce.text;
                Debug.Log ("GarbageShopLoaded");
                garbageJson = SecureHelper.EncryptDecrypt (garbageJson, key);
                JsonUtility.FromJsonOverwrite (garbageJson, garbageShopUI.garbageShopData);

                fireShopUI.fireShopData = new FireShopData ();
                var fireJsonOnce = Resources.Load<TextAsset> ("Records/FireShopData");
                string fireJson = fireJsonOnce.text;
                Debug.Log ("FireShopLoaded");
                fireJson = SecureHelper.EncryptDecrypt (fireJson, key);
                JsonUtility.FromJsonOverwrite (fireJson, fireShopUI.fireShopData);

                policeShopUI.policeShopData = new PoliceShopData ();
                var policeJsonOnce = Resources.Load<TextAsset> ("Records/PoliceShopData");
                string policeJson = policeJsonOnce.text;
                Debug.Log ("PoliceShopLoaded");
                policeJson = SecureHelper.EncryptDecrypt (policeJson, key);
                Debug.Log (policeJson);
                JsonUtility.FromJsonOverwrite (policeJson, policeShopUI.policeShopData);

                taxiShopUI.taxiShopData = new TaxiShopData ();
                var taxiJsonOnce = Resources.Load<TextAsset> ("Records/TaxiShopData");
                string taxiJson = taxiJsonOnce.text;
                Debug.Log ("TaxiShopLoaded");
                taxiJson = SecureHelper.EncryptDecrypt (taxiJson, key);
                JsonUtility.FromJsonOverwrite (taxiJson, taxiShopUI.taxiShopData);

                ambulanceShopUI.ambulanceShopData = new AmbulanceShopData ();
                var ambulanceJsonOnce = Resources.Load<TextAsset> ("Records/AmbulanceShopData");
                string ambulanceJson = ambulanceJsonOnce.text;
                Debug.Log ("AmbulanceShopLoaded");
                ambulanceJson = SecureHelper.EncryptDecrypt (ambulanceJson, key);
                JsonUtility.FromJsonOverwrite (ambulanceJson, ambulanceShopUI.ambulanceShopData);

                SaveData();

                PlayerPrefs.SetInt ("loadOnce", 1);

            } else {
                carShopUI.carShopData = new CarShopData ();
                string carTempPath = Path.Combine (Application.persistentDataPath + "/CarShopData.txt");
                string carShopDataString = System.IO.File.ReadAllText (carTempPath);
                Debug.Log ("CarShopLoaded");

                carShopDataString = SecureHelper.EncryptDecrypt (carShopDataString, key);
                JsonUtility.FromJsonOverwrite (carShopDataString, carShopUI.carShopData);

                gameRoleShopUI.gameData = new GameData ();
                string gameTempPath = Path.Combine (Application.persistentDataPath + "/GameData.txt");
                string gameDataString = System.IO.File.ReadAllText (gameTempPath);
                Debug.Log ("GameDataLoaded");

                gameDataString = SecureHelper.EncryptDecrypt (gameDataString, key);
                JsonUtility.FromJsonOverwrite (gameDataString, gameRoleShopUI.gameData);

                busShopUI.busShopData = new BusShopData ();
                string busTempPath = Path.Combine (Application.persistentDataPath + "/BusShopData.txt");
                string busShopDataString = System.IO.File.ReadAllText (busTempPath);
                Debug.Log ("BusShopLoaded");

                busShopDataString = SecureHelper.EncryptDecrypt (busShopDataString, key);
                JsonUtility.FromJsonOverwrite (busShopDataString, busShopUI.busShopData);

                garbageShopUI.garbageShopData = new GarbageShopData ();
                string garbageTempPath = Path.Combine (Application.persistentDataPath + "/GarbageShopData.txt");
                string garbageShopDataString = System.IO.File.ReadAllText (garbageTempPath);
                Debug.Log ("GarbageShopLoaded");

                garbageShopDataString = SecureHelper.EncryptDecrypt (garbageShopDataString, key);
                JsonUtility.FromJsonOverwrite (garbageShopDataString, garbageShopUI.garbageShopData);

                fireShopUI.fireShopData = new FireShopData ();
                string fireTempPath = Path.Combine (Application.persistentDataPath + "/FireShopData.txt");
                string fireShopDataString = System.IO.File.ReadAllText (fireTempPath);
                Debug.Log ("FireShopLoaded");

                fireShopDataString = SecureHelper.EncryptDecrypt (fireShopDataString, key);
                JsonUtility.FromJsonOverwrite (fireShopDataString, fireShopUI.fireShopData);

                policeShopUI.policeShopData = new PoliceShopData ();
                string policeTempPath = Path.Combine (Application.persistentDataPath + "/PoliceShopData.txt");
                string policeShopDataString = System.IO.File.ReadAllText (policeTempPath);
                Debug.Log ("PoliceShopLoaded");

                policeShopDataString = SecureHelper.EncryptDecrypt (policeShopDataString, key);
                Debug.Log (policeShopDataString);
                JsonUtility.FromJsonOverwrite (policeShopDataString, policeShopUI.policeShopData);

                taxiShopUI.taxiShopData = new TaxiShopData ();
                string taxiTempPath = Path.Combine (Application.persistentDataPath + "/TaxiShopData.txt");
                string taxiShopDataString = System.IO.File.ReadAllText (taxiTempPath);
                Debug.Log ("TaxiShopLoaded");

                taxiShopDataString = SecureHelper.EncryptDecrypt (taxiShopDataString, key);
                JsonUtility.FromJsonOverwrite (taxiShopDataString, taxiShopUI.taxiShopData);

                ambulanceShopUI.ambulanceShopData = new AmbulanceShopData ();
                string ambulanceTempPath = Path.Combine (Application.persistentDataPath + "/AmbulanceShopData.txt");
                string ambulanceShopDataString = System.IO.File.ReadAllText (ambulanceTempPath);
                Debug.Log ("AmbulanceShopLoaded");

                ambulanceShopDataString = SecureHelper.EncryptDecrypt (ambulanceShopDataString, key);
                JsonUtility.FromJsonOverwrite (ambulanceShopDataString, ambulanceShopUI.ambulanceShopData);

            }
        } catch (System.Exception e) {
            Debug.Log ("Error Loading Data :" + e);
            throw;
        }
    }
    private void ClearData () {
        PlayerPrefs.SetInt ("loadOnce", 0);
    }

}