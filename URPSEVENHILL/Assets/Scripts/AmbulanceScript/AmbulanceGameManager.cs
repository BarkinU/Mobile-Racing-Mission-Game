using RoleShopSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AmbulanceGameManager : MonoBehaviour {
    [Header ("GetObjects")]
    private GameObject patient;
    public GameObject patientFollower;
    public GameObject[] firstCityAccidentLocations;
    public GameObject[] secondCityAccidentLocations;
    private GameObject[] patientPlaces;

    private AmbulanceShopData ambulanceShopData;
    public GameData gameData;

    [Header ("Texts")]
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI xpText;
    public TextMeshProUGUI remainingTimeText;
    public TextMeshProUGUI electroShockCounterText;
    public TextMeshProUGUI adrenalinCounterText;
    public TextMeshProUGUI healCounterText;
    [Header ("UpgradeValues")]
    private int prolongLifeTime;
    public int extraMoneyOfExtraWounded;
    private int healingIncrease;
    private int electroShockIncrease;

    [Header ("Patient&MoneyValues")]
    public int patientCounter;
    public int money;
    public int xp;
    public float remainingTime;
    public float finishTime;
    private float woundedLife;
    public int woundedCounter;
    public GameObject patientSymbol1;
    public GameObject patientSymbol2;
    public GameObject patientSymbol3;
    public float patientCurrentHealth1;
    public float patientCurrentHealth2;
    public float patientCurrentHealth3;
    public float patientCurrentAdrenalin1;
    public float patientCurrentAdrenalin2;
    public float patientCurrentAdrenalin3;
    public int maxHealth = 100;

    [Header ("HealthBars")]
    public Image patient1;
    public Image patient2;
    public Image patient3;
    public Image adrenalinArmor1;
    public Image adrenalinArmor2;
    public Image adrenalinArmor3,headerFrameImage;
    public bool isPatient1;
    public bool isPatient2;
    public bool isPatient3;
    public GameObject skull1;
    public GameObject skull2;
    public GameObject skull3;
    public GameObject aliveFrames1,aliveFrames2,aliveFrames3;

    [Header ("SpawnValues")]
    public int randomAccidentLocation;
    private int bigCaseChance = 300;
    [Header ("Skill Bar&Values")]
    public int electroShockCounter;
    public int adrenalinCounter;
    public int healCounter;
    private int electroShockTenure;
    private int adrenalinTenure;
    private int healTenure;
    public bool isElectroShockUse;
    public bool isAdrenalinUse;
    public bool isHealUse;
    public int adrenalinValue = 60;
    public int healValue = 40;
    private int electroShockValue;
    private float deathTime1 = 5;
    private float deathTime2 = 10;
    private float deathTime3 = 10;

    public Button electroShockButton;
    public Button adrenalinButton;
    public Button healButton;

    [Header ("PlayerSpawn")]
    private PABLO RR;
    public GameObject ambulanceCar;
    public AmbulanceList ambulanceList;
    public GameObject firstStartPosition;
    public GameObject secondStartPosition;
    public GameObject startPosition;

    [Header ("MissionCompleteStuff")]
    public GameObject missionCompleteScreen;
    public TextMeshProUGUI finishedTimeText;
    public TextMeshProUGUI gainedMoneyText;
    public TextMeshProUGUI gainedXpText;
    public TextMeshProUGUI savedLivesText;
    public TextMeshProUGUI deadPatientCounterText;
    public TextMeshProUGUI usedHealText;
    public TextMeshProUGUI usedElectroShockText;
    public TextMeshProUGUI usedAdrenalinText,missionFailedText;
    public int deathCounter;
    public float distancePlayerPatient;
    //
    public NavigationScript navigationRouter;
    public int allWounded;
    public GameObject AdrenalinButtonEffect;
    public GameObject ElectroshockButtonEffect;
    public GameObject HealButtonEffect;

    void Awake () {
        GetTagsObject ();
    }
    void Start () {
        SpawnPatients ();
        WoundedLifeCalculate ();
        ReadEnterValues ();
        CalculateRemainingTime ();
    }

    private void FixedUpdate () {
        MoneyXp ();
        PatientsHealthBar ();
    }

    private void GetTagsObject () {

        if (PlayerPrefs.GetInt ("isFirst") == 1) {
            startPosition = firstStartPosition;
            randomAccidentLocation = Random.Range (0, firstCityAccidentLocations.Length);
            firstCityAccidentLocations[randomAccidentLocation].SetActive (true);
        } else {
            startPosition = secondStartPosition;
            randomAccidentLocation = Random.Range (0, secondCityAccidentLocations.Length);
            secondCityAccidentLocations[randomAccidentLocation].SetActive (true);

        }

        Instantiate (ambulanceList.ambulanceVehicles[PlayerPrefs.GetInt ("pointer")], startPosition.transform.position, startPosition.transform.rotation);
        patient = Resources.Load ("GetPatient") as GameObject;
        patientFollower = Resources.Load ("GetPatientMap") as GameObject;
        patientPlaces = GameObject.FindGameObjectsWithTag ("patientSpawn");
        ambulanceCar = GameObject.FindGameObjectWithTag ("Player");
        RR = ambulanceCar.GetComponent<PABLO> ();

    }

    private void ReadEnterValues () {
        ambulanceShopData = ReadWriteAllRoles.ReadAmbulanceProp (ambulanceShopData);
        gameData = ReadWriteAllRoles.ReadGameProp (gameData);

        prolongLifeTime = ambulanceShopData.ambulanceRoleItems.ambulanceSkillUpgradeLevel[ambulanceShopData.ambulanceRoleItems.unlockedProlongLifeLevel].prolongLifeValue;

        electroShockIncrease = ambulanceShopData.ambulanceRoleItems.ambulanceSkillUpgradeLevel[ambulanceShopData.ambulanceRoleItems.unlockedElectroShockLevel].electroShockValue;

        extraMoneyOfExtraWounded = ambulanceShopData.ambulanceRoleItems.ambulanceSkillUpgradeLevel[ambulanceShopData.ambulanceRoleItems.unlockedExtraWoundedLevel].extraWoundedValue;

        healingIncrease = ambulanceShopData.ambulanceRoleItems.ambulanceSkillUpgradeLevel[ambulanceShopData.ambulanceRoleItems.unlockedHealingLevel].healingValue;
        adrenalinTenure = ambulanceShopData.ambulanceRoleItems.adrenalinTenur;
        electroShockTenure = ambulanceShopData.ambulanceRoleItems.electroShockTenur;
        healTenure = ambulanceShopData.ambulanceRoleItems.healingTenur;

        adrenalinCounterText.text = " " + adrenalinCounter + "/" + adrenalinTenure;
        electroShockCounterText.text = " " + electroShockCounter + "/" + electroShockTenure;
        healCounterText.text = " " + healCounter + "/" + healTenure;

        electroShockValue += electroShockIncrease;
        healValue += healingIncrease;
    }
    private void WoundedLifeCalculate () {
        patientCurrentHealth1 = Random.Range (30, 51);
        patientCurrentHealth2 = Random.Range (15, 41);
        patientCurrentHealth3 = Random.Range (5, 31);

        patientCurrentAdrenalin1 = 0;
        patientCurrentAdrenalin2 = 0;
        patientCurrentAdrenalin3 = 0;

    }

    private void CalculateRemainingTime () {
        if (PlayerPrefs.GetInt ("isFirst") == 1) {
            distancePlayerPatient = Vector3.Distance (ambulanceCar.transform.position, firstCityAccidentLocations[randomAccidentLocation].transform.position);
            remainingTime = distancePlayerPatient / 5;
            if(remainingTime>=100)
            {
                 remainingTime-=remainingTime/7/2;
            }else
            {
                remainingTime+=remainingTime/4;
            }
        } else {
            distancePlayerPatient = Vector3.Distance (ambulanceCar.transform.position, secondCityAccidentLocations[randomAccidentLocation].transform.position);
            remainingTime = distancePlayerPatient / 5;
            if(remainingTime>=100)
            {
                 remainingTime-=remainingTime/7/2;
            }else
            {
                 remainingTime+=remainingTime/4;
            }
        }
    }

    private void MoneyXp () {
        if (remainingTime >= 0) {
            remainingTime -= 1 * Time.deltaTime;
            finishTime+=1*Time.deltaTime;
        }

        remainingTimeText.text = ":" + (int) remainingTime;
    }

    private void PatientsHealthBar () {
        if (isPatient1 == true) {
            adrenalinArmor1.fillAmount = patientCurrentAdrenalin1 / maxHealth;
            patient1.fillAmount = patientCurrentHealth1 / maxHealth;
            if (patientCurrentAdrenalin1 > 0) {
                patientCurrentAdrenalin1 -= 3 * Time.deltaTime;
            } else if (patientCurrentHealth1 > 0) {
                patientCurrentHealth1 -= 1 * Time.deltaTime;
            } else {
                if (deathTime1 > 0) {
                    deathTime1 -= 1 * Time.deltaTime;
                    if (deathTime1 <= 0) {
                        patientSymbol1.SetActive (false);
                        skull1.SetActive (true);
                        deathCounter++;
                    }
                }

            }
        }

        if (isPatient2 == true) {
            adrenalinArmor2.fillAmount = patientCurrentAdrenalin2 / maxHealth;
            patient2.fillAmount = patientCurrentHealth2 / maxHealth;
            if (patientCurrentAdrenalin2 > 0) {
                patientCurrentAdrenalin2 -= 3 * Time.deltaTime;
            } else if (patientCurrentHealth2 > 0) {
                patientCurrentHealth2 -= 1 * Time.deltaTime;
            } else {
                if (deathTime2 > 0) {
                    deathTime2 -= 1 * Time.deltaTime;
                    if (deathTime2 <= 0) {
                        patientSymbol2.SetActive (false);
                        skull2.SetActive (true);
                        deathCounter++;
                    }
                }
            }
        }

        if (isPatient3 == true) {
            adrenalinArmor3.fillAmount = patientCurrentAdrenalin3 / maxHealth;
            patient3.fillAmount = patientCurrentHealth3 / maxHealth;
            if (patientCurrentAdrenalin3 > 0) {
                patientCurrentAdrenalin3 -= 3 * Time.deltaTime;
            } else if (patientCurrentHealth3 > 0) {
                patientCurrentHealth3 -= 1 * Time.deltaTime;
            } else {
                if (deathTime3 > 0) {
                    deathTime3 -= 1 * Time.deltaTime;
                    if (deathTime3 <= 0) {
                        patientSymbol3.SetActive (false);
                        skull3.SetActive (true);
                        deathCounter++;
                    }
                }
            }
        }
    }
    public void SpawnPatients () {
        Time.timeScale = 1f;
        int extraPatient = Random.Range (0, 1001);

        if (extraPatient < bigCaseChance) {
            patientCounter = 3;
        } else if (extraPatient < bigCaseChance * 2) {
            patientCounter = 2;
        } else {
            patientCounter = 1;
        }

        if (PlayerPrefs.GetInt ("isFirst") == 1) {
            for (int i = 0; i < patientCounter; i++) {
                GameObject.Instantiate (patient, patientPlaces[i].transform.position, Quaternion.identity);
            }

            patientPlaces[0].transform.position += new Vector3 (0.0f, 11.0f, 0.0f);
            GameObject.Instantiate (patientFollower, patientPlaces[0].transform.position, Quaternion.identity);
        } else {
            for (int i = 0; i < patientCounter; i++) {
                GameObject.Instantiate (patient, patientPlaces[i].transform.position, Quaternion.identity);
            }

            patientPlaces[0].transform.position += new Vector3 (0.0f, 11.0f, 0.0f);
            GameObject.Instantiate (patientFollower, patientPlaces[0].transform.position, Quaternion.identity);
        }
    }

    public void ElektroShockButtonEvent () {
        isElectroShockUse = true;
        isAdrenalinUse = false;
        isHealUse = false;
        StartCoroutine(ElectroshockSkillShining());

        aliveFrames1.SetActive(true);
        aliveFrames2.SetActive(true);
        aliveFrames3.SetActive(true);
    }
    public void AdrenalinButtonEvent () {
        isAdrenalinUse = true;
        isHealUse = false;
        isElectroShockUse = false;
        StartCoroutine(AdrenalinEffectShine());

        aliveFrames1.SetActive(true);
        aliveFrames2.SetActive(true);
        aliveFrames3.SetActive(true);
    }
    public void HealButtonEvent () {
        isHealUse = true;
        isElectroShockUse = false;
        isAdrenalinUse = false;
        StartCoroutine(HealSkillShining());

        aliveFrames1.SetActive(true);
        aliveFrames2.SetActive(true);
        aliveFrames3.SetActive(true);
    }
    public void Patient1ButtonEvents () {
        if (isElectroShockUse == true) {
            if (patientCurrentHealth1 <= 0) {
                patientCurrentHealth1 += 20;
                electroShockCounter++;
                deathTime1 = 10;
                electroShockCounterText.text = " " + electroShockCounter + "/" + electroShockTenure;
                if (electroShockCounter >= electroShockTenure) {
                    electroShockButton.interactable = false;
                    isElectroShockUse = false;
                }
            }
        }

        if (isAdrenalinUse == true) {
            if (patientCurrentHealth1 > 0) {
                patientCurrentAdrenalin1 += 60;
                if (patientCurrentAdrenalin1 >= 100) {
                    patientCurrentAdrenalin1 = 100;
                }
                adrenalinCounter++;
                adrenalinCounterText.text = " " + adrenalinCounter + "/" + adrenalinTenure;
                if (adrenalinCounter >= adrenalinTenure) {
                    adrenalinButton.interactable = false;
                    isAdrenalinUse = false;
                }
            }
        }

        if (isHealUse == true) {
            if (patientCurrentHealth1 > 0 && patientCurrentHealth1 < maxHealth) {
                patientCurrentHealth1 += healValue;
                if (patientCurrentHealth1 >= 100) {
                    patientCurrentHealth1 = 100;
                }
                healCounter++;
                healCounterText.text = " " + healCounter + "/" + healTenure;
                if (healCounter >= healTenure) {
                    healButton.interactable = false;
                    isHealUse = false;
                }
            }
        }
        
        aliveFrames1.SetActive(false);
        aliveFrames2.SetActive(false);
        aliveFrames3.SetActive(false);

    }
    public void Patient2ButtonEvents () {
        if (isElectroShockUse == true) {
            if (patientCurrentHealth2 <= 0) {
                patientCurrentHealth2 += 20;
                electroShockCounter++;
                deathTime2 = 10;
                electroShockCounterText.text = " " + electroShockCounter + "/" + electroShockTenure;
                if (electroShockCounter >= electroShockTenure) {
                    electroShockButton.interactable = false;
                    isElectroShockUse = false;
                }

            }
        }

        if (isAdrenalinUse == true) {
            if (patientCurrentHealth2 > 0) {
                patientCurrentAdrenalin2 += 60;
                if (patientCurrentAdrenalin2 >= 100) {
                    patientCurrentAdrenalin2 = 100;
                }
                adrenalinCounter++;
                adrenalinCounterText.text = " " + adrenalinCounter + "/" + adrenalinTenure;
                if (adrenalinCounter >= adrenalinTenure) {
                    adrenalinButton.interactable = false;
                    isAdrenalinUse = false;
                }
            }
        }

        if (isHealUse == true) {
            if (patientCurrentHealth2 > 0 && patientCurrentHealth2 < maxHealth) {
                patientCurrentHealth2 += healValue;
                if (patientCurrentHealth2 >= 100) {
                    patientCurrentHealth2 = 100;
                }
                healCounter++;
                healCounterText.text = " " + healCounter + "/" + healTenure;
                if (healCounter >= healTenure) {
                    healButton.interactable = false;
                    isHealUse = false;
                }
            }
        }

        aliveFrames1.SetActive(false);
        aliveFrames2.SetActive(false);
        aliveFrames3.SetActive(false);
    }
    public void Patient3ButtonEvents () {
        if (isElectroShockUse == true) {
            if (patientCurrentHealth3 <= 0) {
                patientCurrentHealth3 += 20;
                electroShockCounter++;
                deathTime3 = 10;
                electroShockCounterText.text = " " + electroShockCounter + "/" + electroShockTenure;
                if (electroShockCounter >= electroShockTenure) {
                    electroShockButton.interactable = false;
                    isElectroShockUse = false;
                }

            }
        }

        if (isAdrenalinUse == true) {
            if (patientCurrentHealth3 > 0) {
                patientCurrentAdrenalin3 += 60;
                if (patientCurrentAdrenalin3 >= 100) {
                    patientCurrentAdrenalin3 = 100;
                }
                adrenalinCounter++;
                adrenalinCounterText.text = " " + adrenalinCounter + "/" + adrenalinTenure;
                if (adrenalinCounter >= adrenalinTenure) {
                    adrenalinButton.interactable = false;
                    isAdrenalinUse = false;
                }
            }
        }

        if (isHealUse == true) {
            if (patientCurrentHealth3 > 0 && patientCurrentHealth3 < maxHealth) {
                patientCurrentHealth3 += healValue;
                healCounter++;
                if (patientCurrentHealth3 >= 100) {
                    patientCurrentHealth3 = 100;
                }
                healCounterText.text = " " + healCounter + "/" + healTenure;
                if (healCounter >= healTenure) {
                    healButton.interactable = false;
                    isHealUse = false;
                }
            }
        }

        aliveFrames1.SetActive(false);
        aliveFrames2.SetActive(false);
        aliveFrames3.SetActive(false);
    }
    private void Shocking () {
        if (woundedLife <= 0) {
            woundedLife += electroShockIncrease;
        }
    }

    public void AgainMission () {
        PlayerPrefs.SetInt ("rolePointer", 5);
        SceneLoader.Load (SceneLoader.Scene.RealScene);
    }


    IEnumerator AdrenalinEffectShine()
    {
        AdrenalinButtonEffect.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        AdrenalinButtonEffect.SetActive(false);
    }

    IEnumerator ElectroshockSkillShining()
    {
        ElectroshockButtonEffect.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        ElectroshockButtonEffect.SetActive(false);
    }



    IEnumerator HealSkillShining()
    {
        HealButtonEffect.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        HealButtonEffect.SetActive(false);
    }


}