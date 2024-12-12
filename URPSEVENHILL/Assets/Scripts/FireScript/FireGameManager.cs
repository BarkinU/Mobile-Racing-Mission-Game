
using System.Collections;
using System.Collections.Generic;
using RoleShopSystem;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FireGameManager : MonoBehaviour {

    [Header ("Fire Locations")]
    public GameObject[] firstCityFireExtinguishLocations;
    public GameObject[] firstCityFireAreaLocations;
    public GameObject[] fireExtinguishPointLocations;
    public GameObject[] secondCityFireExtinguishLocations;
    public GameObject[] secondCityFireAreaLocations;
    public GameObject fireCube;
    public GameObject fireArea;
    public int spawn = 0;
    public int randomFireLocation;
    public FireMission fireMissions;
    private FireShopData fireShopData;
    public GameData gameData;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI xpText;
    public TextMeshProUGUI waterText;
    public TextMeshProUGUI waterTankText;
    public TextMeshProUGUI remainingTimeText;
    public TextMeshProUGUI catCounterText;
    public TextMeshProUGUI humanCounterText;
    public TextMeshProUGUI raidTenurText;
    public TextMeshProUGUI helicopterTenurText;
    [Header ("Upgrade Values")]
    public int money;
    public int xp;

    public int fastPutOutPercent;
    public float water;
    public int waterTankCapacity;
    public int rescueLifeBonus;
    public int additionalHose;
    public int extinguishCounter;
    public float remainingTime;
    public int catLife;
    public int humanLife;
    public int humanMoney;
    public int catMoney;
    public float distancePlayerToFire;
    public float finishedTime;
    public int currentFireID=10;

    public FireList fireList;

    [Header ("Car Settings")]
    public PABLO RR;
    private GameObject fireCar;

    public GameObject firstStartPosition;
    public GameObject secondStartPosition;
    private GameObject startPosition;

    [Header ("Fire Skill Values")]
    public static float baseExtinguishTime = 10;
    public int raidSkillCounter;
    public int helicopterSkillCounter;
    private int helicopterTenur = 4;
    private int raidTenur = 4;
    public Button helicopterButton;
    public Button raidButton;
    public float raidTime;
    public Image firePlace1;
    public Image firePlace2;
    public Image firePlace3;
    public bool isHelicopter;
    public int fireCounter;
    [Header ("MissionCompleteValues")]
    public GameObject missionCompleteScreen;
    public TextMeshProUGUI finishedTimeText;
    public TextMeshProUGUI gainedMoneyText;
    public TextMeshProUGUI gainedXpText;
    public TextMeshProUGUI completeSavedPeopleText;
    public TextMeshProUGUI completeSavedCatsText;
    public TextMeshProUGUI completeUsedWaterText;
    public TextMeshProUGUI usedRaidText;
    public TextMeshProUGUI usedHelicopterText, missionFailedText;
    public Image headerFrame;
    public GameObject HelicopterButtonEffect;
    public GameObject RaidButtonEffect;

    //BigMapMarker
    public GameObject missionMarker;
    public NavigationScript navigationScript;
    public bool isInOrOut1=true;
    public bool isInOrOut2=true;
    public bool isInOrOut3=true;

    private void Awake () {
        SelectCitySpawn ();
    }
    private void SelectCitySpawn () {
        randomFireLocation = Random.Range (0, firstCityFireExtinguishLocations.Length);
        

        if (PlayerPrefs.GetInt ("isFirst") == 1) {
            startPosition = firstStartPosition;
            Instantiate (fireList.fireVehicles[PlayerPrefs.GetInt ("pointer")], startPosition.transform.position, startPosition.transform.rotation);
            firstCityFireExtinguishLocations[randomFireLocation].SetActive (true);
            fireExtinguishPointLocations = GameObject.FindGameObjectsWithTag ("firePoint");
            fireCar = GameObject.FindGameObjectWithTag ("Player");
            SpawnFirstCityFireExtinguishPoints ();
        } else {
            startPosition = secondStartPosition;
            Instantiate (fireList.fireVehicles[PlayerPrefs.GetInt ("pointer")], startPosition.transform.position, startPosition.transform.rotation);
            secondCityFireExtinguishLocations[randomFireLocation].SetActive (true);
            fireExtinguishPointLocations = GameObject.FindGameObjectsWithTag ("firePoint");
            fireCar = GameObject.FindGameObjectWithTag ("Player");
            SpawnSecondCityFireExtinguishPoints ();
        }
        RR = fireCar.GetComponent<PABLO> ();
        
    }
    private void Start () {
        ReadValuesFromMenu ();
    }

    private void ReadValuesFromMenu () {
        fireShopData = ReadWriteAllRoles.ReadFireProp (fireShopData);
        gameData = ReadWriteAllRoles.ReadGameProp (gameData);

        fastPutOutPercent = fireShopData.fireRoleItems.fireSkillUpgradeLevel[fireShopData.fireRoleItems.unlockedFastPutOutLevel].fastPutOutValue;

        waterTankCapacity = fireShopData.fireRoleItems.fireSkillUpgradeLevel[fireShopData.fireRoleItems.unlockedWaterTankCapacityLevel].waterTankCapacityValue;

        rescueLifeBonus = fireShopData.fireRoleItems.fireSkillUpgradeLevel[fireShopData.fireRoleItems.unlockedRescueLifeBonusLevel].rescueLifeBonusValue;

        additionalHose = fireShopData.fireRoleItems.fireSkillUpgradeLevel[fireShopData.fireRoleItems.unlockedAdditionalHoseLevel].additionalHoseValue;
        helicopterTenur = fireShopData.fireRoleItems.helicopterTenur;
        raidTenur = fireShopData.fireRoleItems.raidTenur;
        raidTenurText.text = raidSkillCounter + "/" + raidTenur;
        helicopterTenurText.text = helicopterSkillCounter + "/" + helicopterTenur;
        Time.timeScale = 1f;
    }

    private void Update () {
        WaterTimeMoneyCalculating ();
    }

    private void WaterTimeMoneyCalculating () {
        if (remainingTime > 0) {
            remainingTime -= 1 * Time.deltaTime;
            finishedTime += 1 * Time.deltaTime;
        }

        moneyText.text = ":  " + money;
        xpText.text = ":" + xp;
        waterText.text = "Su:" + (int) water;
        waterTankText.text = "Su Tankı Kapasitesi:" + waterTankCapacity;
        remainingTimeText.text = ":" + (int) remainingTime;
    }
    private void SpawnFirstCityFireExtinguishPoints () {

        for (spawn = 0; spawn < fireExtinguishPointLocations.Length; spawn++) {
            GameObject.Instantiate (fireCube, fireExtinguishPointLocations[spawn].transform.position, Quaternion.identity);            
        }
        GameObject.Instantiate (fireArea, firstCityFireAreaLocations[randomFireLocation].transform.position, Quaternion.identity);
        distancePlayerToFire = Vector3.Distance (fireCar.transform.position, firstCityFireAreaLocations[randomFireLocation].transform.position);
        remainingTime = distancePlayerToFire/8;
        if(remainingTime>=100)
        { 
            remainingTime-=remainingTime/7/2;
        }else
        {
            remainingTime+=remainingTime/4;
        }
        Destroy (fireCube);
        Destroy (fireArea);

        fireMissions = FindObjectOfType<FireMission> ();

    }


    private void SpawnSecondCityFireExtinguishPoints () {

        for (spawn = 0; spawn < fireExtinguishPointLocations.Length; spawn++) {
            GameObject.Instantiate (fireCube, fireExtinguishPointLocations[spawn].transform.position, Quaternion.identity);            
        }
        GameObject.Instantiate (fireArea, secondCityFireAreaLocations[randomFireLocation].transform.position, Quaternion.identity);
         distancePlayerToFire = Vector3.Distance (fireCar.transform.position, secondCityFireAreaLocations[randomFireLocation].transform.position);
        remainingTime = distancePlayerToFire/8;
        if(remainingTime>=100)
        {
            remainingTime-=remainingTime/7/2;
        }else
        {
            remainingTime+=remainingTime/4;
        }
        Destroy (fireCube);
        Destroy (fireArea);

        fireMissions = FindObjectOfType<FireMission> ();

    }

    public void HelicopterSkill () {
        helicopterSkillCounter++;
        if (helicopterSkillCounter <= helicopterTenur) {
            StartCoroutine(HelicopterSkillEffectShining());
            baseExtinguishTime--;

            isHelicopter = true;
            helicopterTenurText.text = helicopterSkillCounter + "/" + helicopterTenur;
            fireMissions.extinguishTime--;
            if (currentFireID == 0 && isInOrOut1==false) {
                firePlace2.fillAmount = baseExtinguishTime / 10;
                firePlace3.fillAmount = baseExtinguishTime / 10;  
            } else if (currentFireID == 1 && isInOrOut2==false) {
                firePlace1.fillAmount = baseExtinguishTime / 10;
                firePlace3.fillAmount = baseExtinguishTime / 10;
            }else if (currentFireID == 2 && isInOrOut3==false) {
                firePlace1.fillAmount = baseExtinguishTime / 10;
                firePlace2.fillAmount = baseExtinguishTime / 10;
            }else{
                firePlace1.fillAmount = baseExtinguishTime / 10;
                firePlace2.fillAmount = baseExtinguishTime / 10;
                firePlace3.fillAmount = baseExtinguishTime / 10;  
            }

            if (helicopterSkillCounter == helicopterTenur) {
                helicopterButton.interactable = false;
            }

        }
    }

    public void RaidSkill () {
        raidSkillCounter++;
        if (raidSkillCounter <= raidTenur) {
            StartCoroutine(RaidSkillEffectShining());
            raidTenurText.text = raidSkillCounter + "/" + raidTenur;
            raidTime++;
            if (raidSkillCounter == raidTenur) {
                raidButton.interactable = false;
            }

        }
    }

    public void AgainMission () {
        PlayerPrefs.SetInt ("rolePointer", 3);
        SceneLoader.Load (SceneLoader.Scene.RealScene);
    }

    public void MarkerActivate () {
        missionMarker.SetActive (true);
    }
    public void MarkerDeactivate () {
        missionMarker.SetActive (false);
    }

    IEnumerator HelicopterSkillEffectShining()
    {
        HelicopterButtonEffect.SetActive(true);

        yield return  new WaitForSeconds(0.1f);

        HelicopterButtonEffect.SetActive(false);
    }

    IEnumerator RaidSkillEffectShining()
    {
        RaidButtonEffect.SetActive(true);

        yield return  new WaitForSeconds(0.1f);

        RaidButtonEffect.SetActive(false);
    }

}