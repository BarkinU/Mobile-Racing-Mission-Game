using System.Collections;
using System.Collections.Generic;
using RoleShopSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CarShopSystem {
    public class CarShopUI : MonoBehaviour {
        public CarShopData carShopData;
        private TaxiShopData taxiShopData;
        private GarbageShopData garbageShopData;
        private PoliceShopData policeShopData;
        private BusShopData busShopData;
        public GameData gameData;
        private AmbulanceShopData ambulanceShopData;
        private FireShopData fireShopData;

        public VehicleList listOfVehicles;
        public BusList busList;
        public AmbulanceList ambulanceList;
        public GarbageList garbageList;
        public PoliceList policeList;
        public FireList fireList;
        public TaxiList taxiList;
        public TempVehicleList tempVehicleList;
        public GameObject toRotate, warningNotEnoughDiamond, warningTranslateMoney, warningTranslateDiamond;
        public TextMeshProUGUI unlockBtnText, speedCostText, accelerationCostText, handlingCostText, brakingCostText, carNameText, modifyUnlockButtonText;
        public TextMeshProUGUI speedValueText, accelerationValueText, handlingValueText, brakingValueText, totalMoneyText, totalDiamondText, totalXpText, currentDiamondText, currentMoneyText, currentDiamondText2, currentMoneyText2;
        public TextMeshProUGUI discountTenurText, arrangementTenurText, magnetTenurText, compressionTenurText, raidTenurText, helicopterTenurText, chaseTenurText, vanguardTenurText, electroShockTenurText, adrenalinTenurText, healTenurText, crazyDriveTenurText, cateringTenurText;
        public Button unlockBtn, nextBtn, previousBtn, speedUpgradeBtn, accelerationUpgradeBtn, handlingUpgradeBtn, brakingUpgradeBtn;
        public Image speedSlider;
        public Image accelerationSlider;
        public Image handlingSlider;
        public Image brakingSlider;
        public Image lockedUnlocked, lockedUnlockedModify;
        public Sprite lockedSprite;
        public Sprite unlockedSprite, unlockedModifySprite;
        public SaveLoadData saveLoadData;
        public int currentIndex = 0;
        public int currentRole = 0;
        public int selectedINdex = 0;
        private int skillId;
        private int skillsCost = 10, maxTenur = 4;
        private int currentDiamondAmount = 1, currentMoneyAmount = 10000, currentDiamondAmount2 = 1, currentMoneyAmount2 = 10000, selectedModify, currentSelectedSpoilerID = 0, currentSelectedSkirtID = 0, currentSelectedFrontStickerID = 0, currentSelectedRimID = 0, currentSelectedSideStickerID = 0;
        public Button discountButton, arrangementButton, compressionButton, magnetButton, helicopterButton, raidButton, vanguardButton, chaseButton, electroshockButton, adrenalinButton, healButton, crazyButton, cateringButton, buyInteractable, modifyMenuButton, skirtButton, skirtModifySelectionButton;

        //if Locked Don't Enter
        public GameObject chooseMapCanvas, freeCanvas, roleCanvas, backFree, backRole, freeStart, roleStart, buyModifyGO, selectModifyGO, checkCarOwner, notEnoughMoneyModify, notEnoughMoneyCar;
        public CameraController camCont;

        public TextMeshProUGUI skillYesText;
        public TextMeshProUGUI translterYesText;

        private void Awake () {
            saveLoadData.Initialized ();
        }
        private void Start () {

            saveLoadData.LoadData ();
            gameData = ReadWriteAllRoles.ReadGameProp (gameData);
            taxiShopData = ReadWriteAllRoles.ReadTaxiProp (taxiShopData);
            garbageShopData = ReadWriteAllRoles.ReadGarbageProp (garbageShopData);

            busShopData = ReadWriteAllRoles.ReadBusProp (busShopData);
            policeShopData = ReadWriteAllRoles.ReadPoliceProp (policeShopData);
            fireShopData = ReadWriteAllRoles.ReadFireProp (fireShopData);
            ambulanceShopData = ReadWriteAllRoles.ReadAmbulanceProp (ambulanceShopData);

            currentRole = PlayerPrefs.GetInt ("rolePointer");

            selectedINdex = carShopData.selectedIndex;
            currentIndex = PlayerPrefs.GetInt ("pointer");

            SetCarInfo ();

            if (currentIndex == 0) previousBtn.interactable = false;
            if (currentIndex == carShopData.roleItems[currentRole].shopItems.Length - 1) nextBtn.interactable = false;

            UnlockBtnStatus ();
            SpeedUpgradeBtnStatus ();
            AccUpgradeBtnStatus ();
            HandlingUpgradeBtnStatus ();
            BrakingUpgradeBtnStatus ();
            EnterSkillValues ();

        }
        private void OnEnable () {
            unlockBtn.onClick.AddListener (() => UnlockSelectBtnMethod ());
            speedUpgradeBtn.onClick.AddListener (() => SpeedUpgradeBtnMethod ());
            accelerationUpgradeBtn.onClick.AddListener (() => AccelerationUpgradeBtnMethod ());
            handlingUpgradeBtn.onClick.AddListener (() => HandlingUpgradeBtnMethod ());
            brakingUpgradeBtn.onClick.AddListener (() => BrakingUpgradeBtnMethod ());
            nextBtn.onClick.AddListener (() => NextBtnMethod ());
            previousBtn.onClick.AddListener (() => PreviousBtnMethod ());
        }

        private void SetCarInfo () {
            carNameText.text = carShopData.roleItems[currentRole].shopItems[currentIndex].itemName;

            int currentSpeedLevel = carShopData.roleItems[currentRole].shopItems[currentIndex].unlockedSpeedLevel;
            speedValueText.text = "hız : " + carShopData.roleItems[currentRole].shopItems[currentIndex].carLevel[currentSpeedLevel].speedValue;

            speedSlider.fillAmount = (float) carShopData.roleItems[currentRole].shopItems[currentIndex].carLevel[currentSpeedLevel].speedValue / (float) carShopData.roleItems[currentRole].shopItems[currentIndex].speedMaxValue;

            int currentAccelerationLevel = carShopData.roleItems[currentRole].shopItems[currentIndex].unlockedAccelerationLevel;
            accelerationValueText.text = "hızlanma : " + carShopData.roleItems[currentRole].shopItems[currentIndex].carLevel[currentAccelerationLevel].accelerationValue;

            accelerationSlider.fillAmount = (float) carShopData.roleItems[currentRole].shopItems[currentIndex].carLevel[currentAccelerationLevel].accelerationValue / (float) carShopData.roleItems[currentRole].shopItems[currentIndex].accelerationMaxValue;

            int currentHandlingLevel = carShopData.roleItems[currentRole].shopItems[currentIndex].unlockedHandlingLevel;
            handlingValueText.text = "kontrol :" + carShopData.roleItems[currentRole].shopItems[currentIndex].carLevel[currentHandlingLevel].handlingValue;

            handlingSlider.fillAmount = (float) carShopData.roleItems[currentRole].shopItems[currentIndex].carLevel[currentHandlingLevel].handlingValue / (float) carShopData.roleItems[currentRole].shopItems[currentIndex].handlingMaxValue;

            int currentBrakingLevel = carShopData.roleItems[currentRole].shopItems[currentIndex].unlockedBrakingLevel;
            brakingValueText.text = "fren :" + carShopData.roleItems[currentRole].shopItems[currentIndex].carLevel[currentBrakingLevel].brakingValue;

            brakingSlider.fillAmount = (float) carShopData.roleItems[currentRole].shopItems[currentIndex].carLevel[currentBrakingLevel].brakingValue / (float) carShopData.roleItems[currentRole].shopItems[currentIndex].brakingMaxValue;

            UnlockBtnStatus ();
            SpeedUpgradeBtnStatus ();
            AccUpgradeBtnStatus ();
            HandlingUpgradeBtnStatus ();
            BrakingUpgradeBtnStatus ();
        }

        public void NextBtnMethod () {
            if (currentIndex < listOfVehicles.vehicles.Length - 1) {
                Destroy (GameObject.FindGameObjectWithTag ("Player"));
                currentIndex++;

                PlayerPrefs.SetInt ("pointer", currentIndex);
                if (PlayerPrefs.GetInt ("rolePointer") == 1 || PlayerPrefs.GetInt ("rolePointer") == 2 || PlayerPrefs.GetInt ("rolePointer") == 3 || PlayerPrefs.GetInt ("rolePointer") == 5) {
                    GameObject childObject = Instantiate (listOfVehicles.vehicles[currentIndex], camCont.views[8].position, toRotate.transform.rotation) as GameObject;
                    childObject.transform.parent = toRotate.transform;
                } else {
                    GameObject childObject = Instantiate (listOfVehicles.vehicles[currentIndex], camCont.views[7].position, toRotate.transform.rotation) as GameObject;
                    childObject.transform.parent = toRotate.transform;
                }
                SetCarInfo ();

                CheckWhichRole ();
                StartCoroutine (CheckCarModify ());

                if (currentIndex == carShopData.roleItems[currentRole].shopItems.Length - 1) {
                    nextBtn.interactable = false;
                }

                if (!previousBtn.interactable) {
                    previousBtn.interactable = true;
                }

                UnlockBtnStatus ();
                SpeedUpgradeBtnStatus ();
                AccUpgradeBtnStatus ();
                HandlingUpgradeBtnStatus ();
                BrakingUpgradeBtnStatus ();
            }
        }

        private void PreviousBtnMethod () {
            if (currentIndex > 0) {
                Destroy (GameObject.FindGameObjectWithTag ("Player"));
                currentIndex--;
                PlayerPrefs.SetInt ("pointer", currentIndex);
                if (PlayerPrefs.GetInt ("rolePointer") == 1 || PlayerPrefs.GetInt ("rolePointer") == 2 || PlayerPrefs.GetInt ("rolePointer") == 3 || PlayerPrefs.GetInt ("rolePointer") == 5) {
                    GameObject childObject = Instantiate (listOfVehicles.vehicles[currentIndex], camCont.views[8].position, toRotate.transform.rotation) as GameObject;
                    childObject.transform.parent = toRotate.transform;
                } else {
                    GameObject childObject = Instantiate (listOfVehicles.vehicles[currentIndex], camCont.views[7].position, toRotate.transform.rotation) as GameObject;
                    childObject.transform.parent = toRotate.transform;
                }

                SetCarInfo ();

                CheckWhichRole ();
                StartCoroutine (CheckCarModify ());

                if (currentIndex == 0) {
                    previousBtn.interactable = false;
                }

                if (!nextBtn.interactable) {
                    nextBtn.interactable = true;
                }

                UnlockBtnStatus ();
                SpeedUpgradeBtnStatus ();
                AccUpgradeBtnStatus ();
                HandlingUpgradeBtnStatus ();
                BrakingUpgradeBtnStatus ();
            }
        }

        private void UnlockSelectBtnMethod () {
            bool yesSelected = false;

            if (carShopData.roleItems[currentRole].shopItems[currentIndex].isUnlocked) {
                yesSelected = true;
            } else {
                if (gameData.totalMoney >= carShopData.roleItems[currentRole].shopItems[currentIndex].unlockCost) {
                    gameData.totalMoney -= carShopData.roleItems[currentRole].shopItems[currentIndex].unlockCost;
                    totalMoneyText.text = " " + gameData.totalMoney;
                    yesSelected = true;
                    carShopData.roleItems[currentRole].shopItems[currentIndex].isUnlocked = true;

                    //Start Check
                    if (currentRole == 0) {
                        freeStart.SetActive (true);
                    } else {
                        roleStart.SetActive (true);
                    }

                    ReadWriteAllRoles.WriteCarProp (carShopData);
                    ReadWriteAllRoles.WriteGameProp (gameData);

                    if (busShopData.generalAchievementItem.unlockedBuyCarLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {

                        busShopData.generalAchievementItem.buyCarValue++;
                        ReadWriteAllRoles.WriteBusProp (busShopData);

                    }

                    SpeedUpgradeBtnStatus ();
                    AccUpgradeBtnStatus ();
                    HandlingUpgradeBtnStatus ();
                    BrakingUpgradeBtnStatus ();
                } else {
                    notEnoughMoneyCar.SetActive (true);
                }
            }

            if (yesSelected) {
                unlockBtnText.text = "satın alınmış";
                lockedUnlocked.sprite = unlockedSprite;
                selectedINdex = currentIndex;
                carShopData.selectedIndex = selectedINdex;
                unlockBtn.interactable = false;
                buyInteractable.interactable = false;

            }
        }

        private void SpeedUpgradeBtnMethod () {
            int nextLevelIndex = carShopData.roleItems[currentRole].shopItems[currentIndex].unlockedSpeedLevel + 1;

            if (gameData.totalMoney >= carShopData.roleItems[currentRole].shopItems[currentIndex].carLevel[nextLevelIndex].unlockSpeedCost) {
                gameData.totalMoney -= carShopData.roleItems[currentRole].shopItems[currentIndex].carLevel[nextLevelIndex].unlockSpeedCost;
                int spendMoneyCost = carShopData.roleItems[currentRole].shopItems[currentIndex].carLevel[nextLevelIndex].unlockSpeedCost;
                totalMoneyText.text = " " + gameData.totalMoney;
                carShopData.roleItems[currentRole].shopItems[currentIndex].unlockedSpeedLevel++;

                ReadWriteAllRoles.WriteCarProp (carShopData);
                ReadWriteAllRoles.WriteGameProp (gameData);

                if (busShopData.generalAchievementItem.unlockedXCarUpgradeLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {

                    busShopData.generalAchievementItem.xCarUpgradeValue++;
                    ReadWriteAllRoles.WriteBusProp (busShopData);

                }

                if (busShopData.generalAchievementItem.unlockedSpendXMoneyForCarUpgradeLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {

                    busShopData.generalAchievementItem.spendXMoneyForCarUpgradeValue += spendMoneyCost;
                    ReadWriteAllRoles.WriteBusProp (busShopData);

                }

                if (carShopData.roleItems[currentRole].shopItems[currentIndex].unlockedSpeedLevel < carShopData.roleItems[currentRole].shopItems[currentIndex].carLevel.Length - 1) {
                    speedCostText.text = "" +
                        carShopData.roleItems[currentRole].shopItems[currentIndex].carLevel[nextLevelIndex + 1].unlockSpeedCost;
                } else {
                    speedUpgradeBtn.interactable = false;
                    speedCostText.text = "Son Seviye ";

                    if (currentRole == 2) {
                        garbageShopData.garbageAchievementItem.doMaxLevelCarUpgradeValue++;
                        ReadWriteAllRoles.WriteGarbageProp (garbageShopData);

                    }
                }

                SetCarInfo ();
            }
        }

        private void AccelerationUpgradeBtnMethod () {
            int nextLevelIndex = carShopData.roleItems[currentRole].shopItems[currentIndex].unlockedAccelerationLevel + 1;

            if (gameData.totalMoney >= carShopData.roleItems[currentRole].shopItems[currentIndex].carLevel[nextLevelIndex].unlockAccCost) {
                gameData.totalMoney -= carShopData.roleItems[currentRole].shopItems[currentIndex].carLevel[nextLevelIndex].unlockAccCost;
                int spendMoneyCost = carShopData.roleItems[currentRole].shopItems[currentIndex].carLevel[nextLevelIndex].unlockAccCost;
                totalMoneyText.text = " " + gameData.totalMoney;
                carShopData.roleItems[currentRole].shopItems[currentIndex].unlockedAccelerationLevel++;

                ReadWriteAllRoles.WriteCarProp (carShopData);
                ReadWriteAllRoles.WriteGameProp (gameData);

                if (busShopData.generalAchievementItem.unlockedXCarUpgradeLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {

                    busShopData.generalAchievementItem.xCarUpgradeValue++;
                    ReadWriteAllRoles.WriteBusProp (busShopData);

                }

                if (busShopData.generalAchievementItem.unlockedSpendXMoneyForCarUpgradeLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {

                    busShopData.generalAchievementItem.spendXMoneyForCarUpgradeValue += spendMoneyCost;
                    ReadWriteAllRoles.WriteBusProp (busShopData);

                }

                if (carShopData.roleItems[currentRole].shopItems[currentIndex].unlockedAccelerationLevel < carShopData.roleItems[currentRole].shopItems[currentIndex].carLevel.Length - 1) {
                    accelerationCostText.text = "" +
                        carShopData.roleItems[currentRole].shopItems[currentIndex].carLevel[nextLevelIndex + 1].unlockAccCost;
                } else {
                    accelerationUpgradeBtn.interactable = false;
                    accelerationCostText.text = "Max Lvl ";

                    if (currentRole == 2) {
                        garbageShopData.garbageAchievementItem.doMaxLevelCarUpgradeValue++;
                        ReadWriteAllRoles.WriteGarbageProp (garbageShopData);

                    }
                }

                SetCarInfo ();

            }
        }

        private void HandlingUpgradeBtnMethod () {
            int nextLevelIndex = carShopData.roleItems[currentRole].shopItems[currentIndex].unlockedHandlingLevel + 1;

            if (gameData.totalMoney >= carShopData.roleItems[currentRole].shopItems[currentIndex].carLevel[nextLevelIndex].unlockHandlingCost) {
                gameData.totalMoney -= carShopData.roleItems[currentRole].shopItems[currentIndex].carLevel[nextLevelIndex].unlockHandlingCost;
                int spendMoneyCost = carShopData.roleItems[currentRole].shopItems[currentIndex].carLevel[nextLevelIndex].unlockHandlingCost;
                totalMoneyText.text = " " + gameData.totalMoney;
                carShopData.roleItems[currentRole].shopItems[currentIndex].unlockedHandlingLevel++;

                ReadWriteAllRoles.WriteCarProp (carShopData);
                ReadWriteAllRoles.WriteGameProp (gameData);

                if (busShopData.generalAchievementItem.unlockedXCarUpgradeLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {

                    busShopData.generalAchievementItem.xCarUpgradeValue++;
                    ReadWriteAllRoles.WriteBusProp (busShopData);

                }

                if (busShopData.generalAchievementItem.unlockedSpendXMoneyForCarUpgradeLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {

                    busShopData.generalAchievementItem.spendXMoneyForCarUpgradeValue += spendMoneyCost;
                    ReadWriteAllRoles.WriteBusProp (busShopData);

                }

                if (carShopData.roleItems[currentRole].shopItems[currentIndex].unlockedHandlingLevel < carShopData.roleItems[currentRole].shopItems[currentIndex].carLevel.Length - 1) {
                    handlingCostText.text = "" +
                        carShopData.roleItems[currentRole].shopItems[currentIndex].carLevel[nextLevelIndex + 1].unlockHandlingCost;
                } else {
                    handlingUpgradeBtn.interactable = false;
                    handlingCostText.text = "Max Lvl ";

                    if (currentRole == 2) {
                        garbageShopData.garbageAchievementItem.doMaxLevelCarUpgradeValue++;
                        ReadWriteAllRoles.WriteGarbageProp (garbageShopData);
                    }
                }

                SetCarInfo ();
            }
        }

        private void BrakingUpgradeBtnMethod () {
            int nextLevelIndex = carShopData.roleItems[currentRole].shopItems[currentIndex].unlockedBrakingLevel + 1;

            if (gameData.totalMoney >= carShopData.roleItems[currentRole].shopItems[currentIndex].carLevel[nextLevelIndex].unlockBrakingCost) {
                gameData.totalMoney -= carShopData.roleItems[currentRole].shopItems[currentIndex].carLevel[nextLevelIndex].unlockBrakingCost;
                int spendMoneyCost = carShopData.roleItems[currentRole].shopItems[currentIndex].carLevel[nextLevelIndex].unlockBrakingCost;
                totalMoneyText.text = " " + gameData.totalMoney;
                carShopData.roleItems[currentRole].shopItems[currentIndex].unlockedBrakingLevel++;

                ReadWriteAllRoles.WriteCarProp (carShopData);
                ReadWriteAllRoles.WriteGameProp (gameData);

                if (busShopData.generalAchievementItem.unlockedXCarUpgradeLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {

                    busShopData.generalAchievementItem.xCarUpgradeValue++;
                    ReadWriteAllRoles.WriteBusProp (busShopData);

                }

                if (busShopData.generalAchievementItem.unlockedSpendXMoneyForCarUpgradeLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {

                    busShopData.generalAchievementItem.spendXMoneyForCarUpgradeValue += spendMoneyCost;
                    ReadWriteAllRoles.WriteBusProp (busShopData);

                }

                if (carShopData.roleItems[currentRole].shopItems[currentIndex].unlockedBrakingLevel < carShopData.roleItems[currentRole].shopItems[currentIndex].carLevel.Length - 1) {
                    brakingCostText.text = "" +
                        carShopData.roleItems[currentRole].shopItems[currentIndex].carLevel[nextLevelIndex + 1].unlockBrakingCost;
                } else {
                    brakingUpgradeBtn.interactable = false;
                    brakingCostText.text = "Max Lvl ";

                    if (currentRole == 2) {
                        garbageShopData.garbageAchievementItem.doMaxLevelCarUpgradeValue++;
                        ReadWriteAllRoles.WriteGarbageProp (garbageShopData);
                    }
                }

                SetCarInfo ();
            }
        }

        private void UnlockBtnStatus () {
            if (carShopData.roleItems[currentRole].shopItems[currentIndex].isUnlocked) {

                unlockBtn.interactable = false;
                buyInteractable.interactable = false;
                unlockBtnText.text = "sahip";
                lockedUnlocked.sprite = unlockedSprite;

                if (currentRole == 0) {
                    freeStart.SetActive (true);
                } else {
                    roleStart.SetActive (true);
                }

            } else {
                unlockBtn.interactable = true;
                buyInteractable.interactable = true;
                unlockBtnText.text = "maliyet " + carShopData.roleItems[currentRole].shopItems[currentIndex].unlockCost;
                lockedUnlocked.sprite = lockedSprite;

                if (currentRole == 0) {
                    freeStart.SetActive (false);
                } else {
                    roleStart.SetActive (false);
                }
            }

        }

        private void SpeedUpgradeBtnStatus () {
            if (carShopData.roleItems[currentRole].shopItems[currentIndex].isUnlocked) {
                if (carShopData.roleItems[currentRole].shopItems[currentIndex].unlockedSpeedLevel < carShopData.roleItems[currentRole].shopItems[currentIndex].carLevel.Length - 1) {
                    int nextLevelIndex = carShopData.roleItems[currentRole].shopItems[currentIndex].unlockedSpeedLevel + 1;
                    speedUpgradeBtn.interactable = true;
                    speedCostText.text = "" +
                        carShopData.roleItems[currentRole].shopItems[currentIndex].carLevel[nextLevelIndex].unlockSpeedCost;
                } else {
                    speedUpgradeBtn.interactable = false;
                    speedCostText.text = "son seviye";
                }
            } else {
                speedUpgradeBtn.interactable = false;
                speedCostText.text = "kilitli";
            }
        }
        private void AccUpgradeBtnStatus () {
            if (carShopData.roleItems[currentRole].shopItems[currentIndex].isUnlocked) {
                if (carShopData.roleItems[currentRole].shopItems[currentIndex].unlockedAccelerationLevel < carShopData.roleItems[currentRole].shopItems[currentIndex].carLevel.Length - 1) {
                    int nextLevelIndex = carShopData.roleItems[currentRole].shopItems[currentIndex].unlockedAccelerationLevel + 1;
                    accelerationUpgradeBtn.interactable = true;

                    accelerationCostText.text = "" +
                        carShopData.roleItems[currentRole].shopItems[currentIndex].carLevel[nextLevelIndex].unlockAccCost;
                } else {
                    accelerationUpgradeBtn.interactable = false;
                    accelerationCostText.text = "son seviye";
                }
            } else {
                accelerationUpgradeBtn.interactable = false;
                accelerationCostText.text = "kilitli";
            }
        }

        private void HandlingUpgradeBtnStatus () {
            if (carShopData.roleItems[currentRole].shopItems[currentIndex].isUnlocked) {
                if (carShopData.roleItems[currentRole].shopItems[currentIndex].unlockedHandlingLevel < carShopData.roleItems[currentRole].shopItems[currentIndex].carLevel.Length - 1) {
                    int nextLevelIndex = carShopData.roleItems[currentRole].shopItems[currentIndex].unlockedHandlingLevel + 1;
                    handlingUpgradeBtn.interactable = true;

                    handlingCostText.text = "" +
                        carShopData.roleItems[currentRole].shopItems[currentIndex].carLevel[nextLevelIndex].unlockHandlingCost;
                } else {
                    handlingUpgradeBtn.interactable = false;
                    handlingCostText.text = "son seviye";
                }
            } else {
                handlingUpgradeBtn.interactable = false;
                handlingCostText.text = "kilitli";
            }
        }

        private void BrakingUpgradeBtnStatus () {
            if (carShopData.roleItems[currentRole].shopItems[currentIndex].isUnlocked) {
                if (carShopData.roleItems[currentRole].shopItems[currentIndex].unlockedBrakingLevel < carShopData.roleItems[currentRole].shopItems[currentIndex].carLevel.Length - 1) {
                    int nextLevelIndex = carShopData.roleItems[currentRole].shopItems[currentIndex].unlockedBrakingLevel + 1;
                    brakingUpgradeBtn.interactable = true;

                    brakingCostText.text = "" +
                        carShopData.roleItems[currentRole].shopItems[currentIndex].carLevel[nextLevelIndex].unlockBrakingCost;
                } else {
                    brakingUpgradeBtn.interactable = false;
                    brakingCostText.text = "son seviye";
                }
            } else {
                brakingUpgradeBtn.interactable = false;
                brakingCostText.text = "kilitli";
            }
        }

        public void ChangeListIntoRole () {

            StartCoroutine (CheckCarModify ());

            if (PlayerPrefs.GetInt ("rolePointer") == 0) {
                currentRole = PlayerPrefs.GetInt ("rolePointer");
                currentIndex = 0;
                previousBtn.interactable = false;
                nextBtn.interactable = true;
                if (tempVehicleList.tempVehicles.Length <= 1) {
                    nextBtn.interactable = false;
                }
                listOfVehicles.vehicles = tempVehicleList.tempVehicles;

                SetCarInfo ();
            }

            if (PlayerPrefs.GetInt ("rolePointer") == 1) {
                currentRole = PlayerPrefs.GetInt ("rolePointer");
                currentIndex = 0;
                previousBtn.interactable = false;
                nextBtn.interactable = true;
                camCont.rolePositionChange = false;
                if (busList.busVehicles.Length <= 1) {
                    nextBtn.interactable = false;
                }
                listOfVehicles.vehicles = busList.busVehicles;

                SetCarInfo ();

            }

            if (PlayerPrefs.GetInt ("rolePointer") == 2) {
                currentRole = PlayerPrefs.GetInt ("rolePointer");
                currentIndex = 0;
                previousBtn.interactable = false;
                nextBtn.interactable = true;
                camCont.rolePositionChange = false;
                if (garbageList.garbageVehicles.Length <= 1) {
                    nextBtn.interactable = false;
                }
                listOfVehicles.vehicles = garbageList.garbageVehicles;

                SetCarInfo ();
            }

            if (PlayerPrefs.GetInt ("rolePointer") == 3) {
                currentRole = PlayerPrefs.GetInt ("rolePointer");
                currentIndex = 0;
                previousBtn.interactable = false;
                nextBtn.interactable = true;

                camCont.rolePositionChange = false;
                if (fireList.fireVehicles.Length <= 1) {
                    nextBtn.interactable = false;
                }
                listOfVehicles.vehicles = fireList.fireVehicles;

                SetCarInfo ();
            }

            if (PlayerPrefs.GetInt ("rolePointer") == 4) {
                currentRole = PlayerPrefs.GetInt ("rolePointer");
                currentIndex = 0;
                nextBtn.interactable = true;
                previousBtn.interactable = false;
                camCont.rolePositionChange = false;
                if (policeList.policeVehicles.Length <= 1) {
                    nextBtn.interactable = false;
                }
                listOfVehicles.vehicles = policeList.policeVehicles;

                SetCarInfo ();
            }

            if (PlayerPrefs.GetInt ("rolePointer") == 5) {
                currentRole = PlayerPrefs.GetInt ("rolePointer");
                currentIndex = 0;
                nextBtn.interactable = true;
                previousBtn.interactable = false;
                camCont.rolePositionChange = false;
                if (ambulanceList.ambulanceVehicles.Length <= 1) {
                    nextBtn.interactable = false;
                }
                listOfVehicles.vehicles = ambulanceList.ambulanceVehicles;

                SetCarInfo ();
            }

            if (PlayerPrefs.GetInt ("rolePointer") == 6) {
                currentRole = PlayerPrefs.GetInt ("rolePointer");
                currentIndex = 0;
                previousBtn.interactable = false;
                nextBtn.interactable = true;
                camCont.rolePositionChange = false;
                if (taxiList.taxiVehicles.Length <= 1) {
                    nextBtn.interactable = false;
                }
                listOfVehicles.vehicles = taxiList.taxiVehicles;

                SetCarInfo ();
            }

        }

        public void BuyDiamond1 () {
            gameData.totalDiamond += 20;
            ReadWriteAllRoles.WriteGameProp (gameData);
            gameData = ReadWriteAllRoles.ReadGameProp (gameData);
            totalDiamondText.text = " :" + gameData.totalDiamond;

        }
        public void BuyDiamond2 () {
            gameData.totalDiamond += 50;
            ReadWriteAllRoles.WriteGameProp (gameData);
            gameData = ReadWriteAllRoles.ReadGameProp (gameData);
            totalDiamondText.text = " :" + gameData.totalDiamond;
        }
        public void BuyDiamond3 () {
            gameData.totalDiamond += 150;
            ReadWriteAllRoles.WriteGameProp (gameData);
            gameData = ReadWriteAllRoles.ReadGameProp (gameData);
            totalDiamondText.text = " :" + gameData.totalDiamond;
        }
        public void BuyDiamond4 () {
            gameData.totalDiamond += 390;
            ReadWriteAllRoles.WriteGameProp (gameData);
            gameData = ReadWriteAllRoles.ReadGameProp (gameData);
            totalDiamondText.text = " :" + gameData.totalDiamond;
        }

        private void IncreaseDiscountTenur () {
            if (busShopData.busRoleItems.discountTenur < maxTenur) {
                if (gameData.totalDiamond >= skillsCost) {
                    busShopData.busRoleItems.discountTenur++;
                    discountTenurText.text = busShopData.busRoleItems.discountTenur + " / " + maxTenur;
                    ReadWriteAllRoles.WriteGameProp (gameData);
                    ReadWriteAllRoles.WriteBusProp (busShopData);
                    totalDiamondText.text = " :" + gameData.totalDiamond;
                    if (busShopData.busRoleItems.discountTenur == maxTenur) {
                        discountButton.interactable = false;
                    }

                    if (busShopData.generalAchievementItem.unlockedOpenXTenurLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                        busShopData.generalAchievementItem.openXTenurValue++;
                    }

                    ReadWriteAllRoles.WriteBusProp (busShopData);
                } else {
                    warningNotEnoughDiamond.SetActive (true);
                }
            }

        }

        private void IncreaseArrangementTenur () {
            if (busShopData.busRoleItems.arrangementTenur < maxTenur) {
                if (gameData.totalDiamond >= skillsCost) {
                    gameData.totalDiamond -= skillsCost;
                    busShopData.busRoleItems.arrangementTenur++;
                    arrangementTenurText.text = busShopData.busRoleItems.arrangementTenur + " / " + maxTenur;
                    ReadWriteAllRoles.WriteGameProp (gameData);
                    ReadWriteAllRoles.WriteBusProp (busShopData);
                    totalDiamondText.text = " :" + gameData.totalDiamond;
                    if (busShopData.busRoleItems.arrangementTenur == maxTenur) {
                        arrangementButton.interactable = false;
                    }

                    if (busShopData.generalAchievementItem.unlockedOpenXTenurLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                        busShopData.generalAchievementItem.openXTenurValue++;
                    }

                    ReadWriteAllRoles.WriteBusProp (busShopData);

                } else {
                    warningNotEnoughDiamond.SetActive (true);
                }
            }

        }

        private void IncreaseMagnetTenur () {
            if (garbageShopData.garbageRoleItems.magnetTenur < maxTenur) {
                if (gameData.totalDiamond >= skillsCost) {
                    gameData.totalDiamond -= skillsCost;
                    garbageShopData.garbageRoleItems.magnetTenur++;
                    magnetTenurText.text = garbageShopData.garbageRoleItems.magnetTenur + " / " + maxTenur;
                    ReadWriteAllRoles.WriteGameProp (gameData);
                    ReadWriteAllRoles.WriteGarbageProp (garbageShopData);
                    totalDiamondText.text = " :" + gameData.totalDiamond;
                    if (garbageShopData.garbageRoleItems.magnetTenur == maxTenur) {
                        magnetButton.interactable = false;
                    }

                    if (busShopData.generalAchievementItem.unlockedOpenXTenurLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                        busShopData.generalAchievementItem.openXTenurValue++;
                    }

                    ReadWriteAllRoles.WriteBusProp (busShopData);
                } else {
                    warningNotEnoughDiamond.SetActive (true);
                }
            }

        }

        private void IncreaseCompressionTenur () {
            if (garbageShopData.garbageRoleItems.compressionTenur < maxTenur) {
                if (gameData.totalDiamond >= skillsCost) {
                    gameData.totalDiamond -= skillsCost;
                    garbageShopData.garbageRoleItems.compressionTenur++;
                    compressionTenurText.text = garbageShopData.garbageRoleItems.compressionTenur + " / " + maxTenur;
                    ReadWriteAllRoles.WriteGameProp (gameData);
                    ReadWriteAllRoles.WriteGarbageProp (garbageShopData);
                    totalDiamondText.text = " :" + gameData.totalDiamond;
                    if (garbageShopData.garbageRoleItems.compressionTenur == maxTenur) {
                        compressionButton.interactable = false;
                    }

                    if (busShopData.generalAchievementItem.unlockedOpenXTenurLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                        busShopData.generalAchievementItem.openXTenurValue++;
                    }

                    ReadWriteAllRoles.WriteBusProp (busShopData);
                } else {
                    warningNotEnoughDiamond.SetActive (true);
                }

            }

        }

        private void IncreaseHelicopterTenur () {
            if (fireShopData.fireRoleItems.helicopterTenur < maxTenur) {
                if (gameData.totalDiamond >= skillsCost) {
                    gameData.totalDiamond -= skillsCost;
                    fireShopData.fireRoleItems.helicopterTenur++;
                    helicopterTenurText.text = fireShopData.fireRoleItems.helicopterTenur + " / " + maxTenur;
                    ReadWriteAllRoles.WriteGameProp (gameData);
                    ReadWriteAllRoles.WriteFireProp (fireShopData);
                    totalDiamondText.text = " :" + gameData.totalDiamond;
                    if (fireShopData.fireRoleItems.helicopterTenur == maxTenur) {
                        helicopterButton.interactable = false;
                    }

                    if (busShopData.generalAchievementItem.unlockedOpenXTenurLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                        busShopData.generalAchievementItem.openXTenurValue++;
                    }

                    ReadWriteAllRoles.WriteBusProp (busShopData);
                } else {
                    warningNotEnoughDiamond.SetActive (true);
                }
            }

        }

        private void IncreaseRaidTenur () {
            if (fireShopData.fireRoleItems.raidTenur < maxTenur) {
                if (gameData.totalDiamond >= skillsCost) {
                    gameData.totalDiamond -= skillsCost;
                    fireShopData.fireRoleItems.raidTenur++;
                    raidTenurText.text = fireShopData.fireRoleItems.raidTenur + " / " + maxTenur;
                    ReadWriteAllRoles.WriteGameProp (gameData);
                    ReadWriteAllRoles.WriteFireProp (fireShopData);
                    totalDiamondText.text = " :" + gameData.totalDiamond;
                    if (fireShopData.fireRoleItems.raidTenur == maxTenur) {
                        raidButton.interactable = false;
                    }

                    if (busShopData.generalAchievementItem.unlockedOpenXTenurLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                        busShopData.generalAchievementItem.openXTenurValue++;
                    }

                    ReadWriteAllRoles.WriteBusProp (busShopData);
                } else {
                    warningNotEnoughDiamond.SetActive (true);
                }
            }

        }

        private void IncreaseChaseTenur () {
            if (policeShopData.policeRoleItems.chaseTenur < maxTenur) {
                if (gameData.totalDiamond >= skillsCost) {
                    gameData.totalDiamond -= skillsCost;
                    policeShopData.policeRoleItems.chaseTenur++;
                    chaseTenurText.text = policeShopData.policeRoleItems.chaseTenur + " / " + maxTenur;
                    ReadWriteAllRoles.WriteGameProp (gameData);
                    ReadWriteAllRoles.WritePoliceProp (policeShopData);
                    totalDiamondText.text = " :" + gameData.totalDiamond;
                    if (policeShopData.policeRoleItems.chaseTenur == maxTenur) {
                        chaseButton.interactable = false;
                    }

                    if (busShopData.generalAchievementItem.unlockedOpenXTenurLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                        busShopData.generalAchievementItem.openXTenurValue++;
                    }

                    ReadWriteAllRoles.WriteBusProp (busShopData);
                } else {
                    warningNotEnoughDiamond.SetActive (true);
                }
            }

        }

        private void IncreaseVanguardTenur () {
            if (policeShopData.policeRoleItems.vanguardTenur < maxTenur) {
                if (gameData.totalDiamond >= skillsCost) {
                    gameData.totalDiamond -= skillsCost;
                    policeShopData.policeRoleItems.vanguardTenur++;
                    vanguardTenurText.text = policeShopData.policeRoleItems.vanguardTenur + " / " + maxTenur;
                    ReadWriteAllRoles.WriteGameProp (gameData);
                    ReadWriteAllRoles.WritePoliceProp (policeShopData);
                    totalDiamondText.text = " :" + gameData.totalDiamond;
                    if (policeShopData.policeRoleItems.vanguardTenur == maxTenur) {
                        vanguardButton.interactable = false;
                    }

                    if (busShopData.generalAchievementItem.unlockedOpenXTenurLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                        busShopData.generalAchievementItem.openXTenurValue++;
                    }

                    ReadWriteAllRoles.WriteBusProp (busShopData);
                } else {
                    warningNotEnoughDiamond.SetActive (true);
                }
            }

        }

        private void IncreaseElectroShockTenur () {
            if (ambulanceShopData.ambulanceRoleItems.electroShockTenur < maxTenur) {
                if (gameData.totalDiamond >= skillsCost) {
                    gameData.totalDiamond -= skillsCost;
                    ambulanceShopData.ambulanceRoleItems.electroShockTenur++;
                    electroShockTenurText.text = ambulanceShopData.ambulanceRoleItems.electroShockTenur + " / " + maxTenur;
                    ReadWriteAllRoles.WriteGameProp (gameData);
                    ReadWriteAllRoles.WriteAmbulanceProp (ambulanceShopData);
                    totalDiamondText.text = " :" + gameData.totalDiamond;
                    if (ambulanceShopData.ambulanceRoleItems.electroShockTenur == maxTenur) {
                        electroshockButton.interactable = false;
                    }

                    if (busShopData.generalAchievementItem.unlockedOpenXTenurLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                        busShopData.generalAchievementItem.openXTenurValue++;
                    }

                    ReadWriteAllRoles.WriteBusProp (busShopData);
                } else {
                    warningNotEnoughDiamond.SetActive (true);
                }
            }

        }

        private void IncreaseAdrenalinTenur () {
            if (ambulanceShopData.ambulanceRoleItems.adrenalinTenur < maxTenur) {
                if (gameData.totalDiamond >= skillsCost) {
                    gameData.totalDiamond -= skillsCost;
                    ambulanceShopData.ambulanceRoleItems.adrenalinTenur++;
                    adrenalinTenurText.text = ambulanceShopData.ambulanceRoleItems.adrenalinTenur + " / " + maxTenur;

                    ReadWriteAllRoles.WriteGameProp (gameData);
                    ReadWriteAllRoles.WriteAmbulanceProp (ambulanceShopData);
                    totalDiamondText.text = " :" + gameData.totalDiamond;
                    if (ambulanceShopData.ambulanceRoleItems.adrenalinTenur == maxTenur) {
                        adrenalinButton.interactable = false;
                    }

                    if (busShopData.generalAchievementItem.unlockedOpenXTenurLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                        busShopData.generalAchievementItem.openXTenurValue++;
                    }

                    ReadWriteAllRoles.WriteBusProp (busShopData);
                } else {
                    warningNotEnoughDiamond.SetActive (true);
                }
            }

        }

        private void IncreaseHealTenur () {
            if (ambulanceShopData.ambulanceRoleItems.healingTenur < maxTenur) {
                if (gameData.totalDiamond >= skillsCost) {
                    gameData.totalDiamond -= skillsCost;
                    ambulanceShopData.ambulanceRoleItems.healingTenur++;
                    healTenurText.text = ambulanceShopData.ambulanceRoleItems.healingTenur + " / " + maxTenur;
                    ReadWriteAllRoles.WriteGameProp (gameData);
                    ReadWriteAllRoles.WriteAmbulanceProp (ambulanceShopData);
                    totalDiamondText.text = " :" + gameData.totalDiamond;
                    if (ambulanceShopData.ambulanceRoleItems.healingTenur == maxTenur) {
                        healButton.interactable = false;
                    }

                    if (busShopData.generalAchievementItem.unlockedOpenXTenurLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                        busShopData.generalAchievementItem.openXTenurValue++;
                    }

                    ReadWriteAllRoles.WriteBusProp (busShopData);
                } else {
                    warningNotEnoughDiamond.SetActive (true);
                }
            }

        }

        private void IncreaseCrazyDriveTenur () {
            if (taxiShopData.taxiRoleItems.crazyDriveTenur < maxTenur) {
                if (gameData.totalDiamond >= skillsCost) {
                    gameData.totalDiamond -= skillsCost;
                    taxiShopData.taxiRoleItems.crazyDriveTenur++;
                    crazyDriveTenurText.text = taxiShopData.taxiRoleItems.crazyDriveTenur + " / " + maxTenur;
                    ReadWriteAllRoles.WriteGameProp (gameData);
                    ReadWriteAllRoles.WriteTaxiProp (taxiShopData);
                    totalDiamondText.text = " :" + gameData.totalDiamond;
                    if (taxiShopData.taxiRoleItems.crazyDriveTenur == maxTenur) {
                        crazyButton.interactable = false;
                    }

                    if (busShopData.generalAchievementItem.unlockedOpenXTenurLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                        busShopData.generalAchievementItem.openXTenurValue++;
                    }

                    ReadWriteAllRoles.WriteBusProp (busShopData);
                } else {
                    warningNotEnoughDiamond.SetActive (true);
                }
            }

        }

        private void IncreaseCateringTenur () {
            if (taxiShopData.taxiRoleItems.cateringTenur < maxTenur) {
                if (gameData.totalDiamond >= skillsCost) {
                    gameData.totalDiamond -= skillsCost;
                    taxiShopData.taxiRoleItems.cateringTenur++;
                    cateringTenurText.text = taxiShopData.taxiRoleItems.cateringTenur + " / " + maxTenur;
                    ReadWriteAllRoles.WriteGameProp (gameData);
                    ReadWriteAllRoles.WriteTaxiProp (taxiShopData);
                    totalDiamondText.text = " :" + gameData.totalDiamond;
                    if (taxiShopData.taxiRoleItems.cateringTenur == maxTenur) {
                        cateringButton.interactable = false;
                    }

                    if (busShopData.generalAchievementItem.unlockedOpenXTenurLevel <= busShopData.generalAchievementItem.generalAchievementsUpgradeLevel.Length - 1) {
                        busShopData.generalAchievementItem.openXTenurValue++;
                    }

                    ReadWriteAllRoles.WriteBusProp (busShopData);
                } else {
                    warningNotEnoughDiamond.SetActive (true);
                }
            }

        }

        private void EnterSkillValues () {
            totalMoneyText.text = " :" + gameData.totalMoney;
            totalDiamondText.text = " :" + gameData.totalDiamond;
            totalXpText.text = " :" + gameData.totalXp;
            currentDiamondText.text = " :" + currentDiamondAmount;
            currentMoneyText.text = " :" + currentMoneyAmount;
            currentDiamondText2.text = " :" + currentDiamondAmount2;
            currentMoneyText2.text = " :" + currentMoneyAmount2;

            discountTenurText.text = busShopData.busRoleItems.discountTenur + " / " + maxTenur;
            arrangementTenurText.text = busShopData.busRoleItems.arrangementTenur + " / " + maxTenur;
            magnetTenurText.text = garbageShopData.garbageRoleItems.magnetTenur + " / " + maxTenur;
            compressionTenurText.text = garbageShopData.garbageRoleItems.compressionTenur + " / " + maxTenur;
            raidTenurText.text = fireShopData.fireRoleItems.raidTenur + " / " + maxTenur;
            helicopterTenurText.text = fireShopData.fireRoleItems.helicopterTenur + " / " + maxTenur;
            chaseTenurText.text = policeShopData.policeRoleItems.chaseTenur + " / " + maxTenur;
            vanguardTenurText.text = policeShopData.policeRoleItems.vanguardTenur + " / " + maxTenur;
            electroShockTenurText.text = ambulanceShopData.ambulanceRoleItems.electroShockTenur + " / " + maxTenur;
            adrenalinTenurText.text = ambulanceShopData.ambulanceRoleItems.adrenalinTenur + " / " + maxTenur;
            healTenurText.text = ambulanceShopData.ambulanceRoleItems.healingTenur + " / " + maxTenur;
            crazyDriveTenurText.text = taxiShopData.taxiRoleItems.crazyDriveTenur + " / " + maxTenur;
            cateringTenurText.text = taxiShopData.taxiRoleItems.cateringTenur + " / " + maxTenur;
        }

        public void ConverterIncreaseDiamond () {
            if (currentDiamondAmount2 > 0) {
                currentDiamondAmount2++;
                currentMoneyAmount2 += 10000;
                currentMoneyText2.text = " :" + currentMoneyAmount2;
                currentDiamondText2.text = " :" + currentDiamondAmount2;
            }

        }

        public void ConverterDecreaseDiamond () {
            if (currentDiamondAmount2 > 1) {
                currentDiamondAmount2--;
                currentMoneyAmount2 -= 10000;
                currentMoneyText2.text = " :" + currentMoneyAmount2;
                currentDiamondText2.text = " :" + currentDiamondAmount2;
            }
        }

        public void ConverterIncreaseMoney () {
            if (currentMoneyAmount > 0) {
                currentDiamondAmount++;
                currentMoneyAmount += 10000;
                currentMoneyText.text = " :" + currentMoneyAmount;
                currentDiamondText.text = " :" + currentDiamondAmount;
            }
        }

        public void ConverterDecreaseMoney () {
            if (currentMoneyAmount > 10000) {
                currentDiamondAmount--;
                currentMoneyAmount -= 10000;
                currentMoneyText.text = " :" + currentMoneyAmount;
                currentDiamondText.text = " :" + currentDiamondAmount;
            }
        }

        public void MoneyToDiamondTranslater () {
            if (gameData.totalMoney >= currentMoneyAmount) {
                gameData.totalMoney -= currentMoneyAmount;
                gameData.totalDiamond += currentDiamondAmount;
                currentMoneyAmount = 10000;
                currentDiamondAmount = 1;
                currentMoneyText.text = " :" + currentMoneyAmount;
                currentDiamondText.text = " :" + currentDiamondAmount;
                ReadWriteAllRoles.WriteGameProp (gameData);
                totalMoneyText.text = " :" + gameData.totalMoney;
                totalDiamondText.text = " :" + gameData.totalDiamond;

            } else {
                warningTranslateMoney.SetActive (true);
                currentMoneyAmount = 10000;
                currentDiamondAmount = 1;
                currentMoneyText.text = " :" + currentMoneyAmount;
                currentDiamondText.text = " :" + currentDiamondAmount;
            }
        }

        public void DiamondToMoneyTranslater () {
            if (gameData.totalDiamond >= currentDiamondAmount2) {
                gameData.totalMoney += currentMoneyAmount2;
                gameData.totalDiamond -= currentDiamondAmount2;
                currentMoneyAmount2 = 10000;
                currentDiamondAmount2 = 1;
                currentMoneyText2.text = " :" + currentMoneyAmount2;
                currentDiamondText2.text = " :" + currentDiamondAmount2;
                ReadWriteAllRoles.WriteGameProp (gameData);
                totalMoneyText.text = " :" + gameData.totalMoney;
                totalDiamondText.text = " :" + gameData.totalDiamond;

            } else {
                warningTranslateDiamond.SetActive (true);
                currentMoneyText2.text = " :" + currentMoneyAmount2;
                currentDiamondText2.text = " :" + currentDiamondAmount2;
                currentDiamondAmount2=1;
                currentMoneyAmount2=10000;
            }
        }

        public void TranslterTextChangerMoneyToDia () {
            translterYesText.text = currentMoneyAmount + " para karşılığında " + currentDiamondAmount + " elmas almak istiyor musunuz?";
        }

        public void TranslterTextChangerDiaToMoney () {
            translterYesText.text = currentDiamondAmount2 + " elmas  karşılığında " + currentMoneyAmount2 + " para almak istiyor musunuz?";
        }
        public void IfLockedDontStartRole () {
            if (PlayerPrefs.GetInt ("NoMapSelection") == 1) {
                if (carShopData.roleItems[currentRole].shopItems[currentIndex].isUnlocked == true) {
                    SceneLoader.Load (SceneLoader.Scene.RealScene);
                    PlayerPrefs.SetInt ("NoMapSelection", 0);
                }
            } else {
                if (carShopData.roleItems[currentRole].shopItems[currentIndex].isUnlocked == true) {
                    chooseMapCanvas.SetActive (true);
                    freeCanvas.SetActive (false);
                    roleCanvas.SetActive (false);
                    backRole.SetActive (true);
                } else {
                    chooseMapCanvas.SetActive (false);
                }
            }

        }

        public void IfLockedDontStartFree () {
            if (carShopData.roleItems[currentRole].shopItems[currentIndex].isUnlocked == true) {
                chooseMapCanvas.SetActive (true);
                freeCanvas.SetActive (false);
                roleCanvas.SetActive (false);
                backFree.SetActive (true);
            } else {
                chooseMapCanvas.SetActive (false);
            }
        }

        public void RouteBuyCar () {
            Destroy (GameObject.FindGameObjectWithTag ("Player"));
            GameObject childObject = Instantiate (listOfVehicles.vehicles[currentIndex], camCont.views[7].position, toRotate.transform.rotation) as GameObject;
            childObject.transform.parent = toRotate.transform;
            StartCoroutine (CheckCarModify ());
            SetCarInfo ();
        }

        public void WhichBtnClicked (int buttonNumb) {
            skillId = buttonNumb;
            switch (skillId) {
                case 1:
                    skillYesText.text = "20 Elmas Karşılığında İndirim Yeteneğinin Kullanım Hakkını Arttırmak İster misiniz?";
                    break;

                case 2:
                    skillYesText.text = "20 Elmas Karşılığında  Düzen Getiren Yeteneğinin Kullanım Hakkını Arttırmak İster misiniz?";
                    break;

                case 3:
                    skillYesText.text = "20 Elmas Karşılığında  Manyetik Alan Yeteneğinin Kullanım Hakkını Arttırmak İster misiniz?";
                    break;

                case 4:
                    skillYesText.text = "20 Elmas Karşılığında  Sıkıştırma Yeteneğinin Kullanım Hakkını Arttırmak İster misiniz?";
                    break;

                case 5:
                    skillYesText.text = "20 Elmas Karşılığında  Helikopter Yeteneğinin Kullanım Hakkını Arttırmak İster misiniz?";
                    break;

                case 6:
                    skillYesText.text = "20 Elmas Karşılığında  İş Birliği Yeteneğinin Kullanım Hakkını Arttırmak İster misiniz?";
                    break;

                case 7:
                    skillYesText.text = "20 Elmas Karşılığında  Takip Yeteneğinin Kullanım Hakkını Arttırmak İster misiniz?";
                    break;

                case 8:
                    skillYesText.text = "20 Elmas Karşılığında  Öncü Birlik Yeteneğinin Kullanım Hakkını Arttırmak İster misiniz?";
                    break;

                case 9:
                    skillYesText.text = "20 Elmas Karşılığında  Elektroşok Yeteneğinin Kullanım Hakkını Arttırmak İster misiniz?";
                    break;

                case 10:
                    skillYesText.text = "20 Elmas Karşılığında  Adrenalin Yeteneğinin Kullanım Hakkını Arttırmak İster misiniz?";
                    break;

                case 11:
                    skillYesText.text = "20 Elmas Karşılığında  İlk Yardım Yeteneğinin Kullanım Hakkını Arttırmak İster misiniz?";
                    break;

                case 12:
                    skillYesText.text = "20 Elmas Karşılığında  Çılgın Sürüş Yeteneğinin Kullanım Hakkını Arttırmak İster misiniz?";
                    break;

                case 13:
                    skillYesText.text = "20 Elmas Karşılığında  İkram Yeteneğinin Kullanım Hakkını Arttırmak İster misiniz?";
                    break;
            }
        }

        public void YesBuyButton () {
            switch (skillId) {
                case 1:
                    IncreaseDiscountTenur ();

                    break;

                case 2:
                    IncreaseArrangementTenur ();

                    break;

                case 3:
                    IncreaseMagnetTenur ();

                    break;

                case 4:
                    IncreaseCompressionTenur ();

                    break;

                case 5:
                    IncreaseHelicopterTenur ();

                    break;

                case 6:
                    IncreaseRaidTenur ();

                    break;

                case 7:
                    IncreaseChaseTenur ();

                    break;

                case 8:
                    IncreaseVanguardTenur ();

                    break;

                case 9:
                    IncreaseElectroShockTenur ();

                    break;

                case 10:
                    IncreaseAdrenalinTenur ();

                    break;

                case 11:
                    IncreaseHealTenur ();

                    break;

                case 12:
                    IncreaseCrazyDriveTenur ();

                    break;

                case 13:
                    IncreaseCateringTenur ();

                    break;
            }
        }

        IEnumerator CheckCarModify () {
            yield return new WaitForSeconds (0.005f);

            if (carShopData.roleItems[currentRole].shopItems[currentIndex].haveModify == true) {

                modifyMenuButton.interactable = true;

                //check spoiler
                if (carShopData.roleItems[currentRole].shopItems[currentIndex].haveSpoiler == true) {

                    if (carShopData.roleItems[currentRole].shopItems[currentIndex].lastSelectedSpoilerID != 0 && carShopData.roleItems[currentRole].shopItems[currentIndex].modifyInfo[carShopData.roleItems[currentRole].shopItems[currentIndex].lastSelectedSpoilerID].unlockedSpoiler == true) {
                        GameObject.FindGameObjectWithTag ("Player").transform.GetChild (0).transform.GetChild (carShopData.roleItems[currentRole].shopItems[currentIndex].lastSelectedSpoilerID - 1).gameObject.SetActive (true);
                    }

                }

                //check skirts
                if (carShopData.roleItems[currentRole].shopItems[currentIndex].haveSkirts == true) {
                    if (carShopData.roleItems[currentRole].shopItems[currentIndex].lastSelectedSkirtID != 0 && carShopData.roleItems[currentRole].shopItems[currentIndex].modifyInfo[carShopData.roleItems[currentRole].shopItems[currentIndex].lastSelectedSkirtID].unlockedSkirt == true) {
                        GameObject.FindGameObjectWithTag ("Player").transform.GetChild (1).transform.GetChild (carShopData.roleItems[currentRole].shopItems[currentIndex].lastSelectedSkirtID - 1).gameObject.SetActive (true);
                    }

                }

                //check front sticker
                if (carShopData.roleItems[currentRole].shopItems[currentIndex].haveFrontSticker == true) {

                    if (carShopData.roleItems[currentRole].shopItems[currentIndex].lastSelectedFrontStickerID != 0 && carShopData.roleItems[currentRole].shopItems[currentIndex].modifyInfo[carShopData.roleItems[currentRole].shopItems[currentIndex].lastSelectedFrontStickerID].unlockedFrontSticker == true) {
                        GameObject.FindGameObjectWithTag ("Player").transform.GetChild (2).transform.GetChild (carShopData.roleItems[currentRole].shopItems[currentIndex].lastSelectedFrontStickerID - 1).gameObject.SetActive (true);
                    }

                }

                //check rims
                if (carShopData.roleItems[currentRole].shopItems[currentIndex].haveRim == true) {

                    if (carShopData.roleItems[currentRole].shopItems[currentIndex].lastSelectedRimID != 0 && carShopData.roleItems[currentRole].shopItems[currentIndex].modifyInfo[carShopData.roleItems[currentRole].shopItems[currentIndex].lastSelectedRimID].unlockedRim == true) {
                        GameObject.FindGameObjectWithTag ("Player").transform.GetChild (3).transform.GetChild (carShopData.roleItems[currentRole].shopItems[currentIndex].lastSelectedRimID - 1).gameObject.SetActive (true);
                        GameObject.FindGameObjectWithTag ("Player").transform.GetChild (3).transform.GetChild (0).gameObject.SetActive (false);
                    } else {

                    }

                }

                //check side sticker
                if (carShopData.roleItems[currentRole].shopItems[currentIndex].haveSideSticker == true) {

                    if (carShopData.roleItems[currentRole].shopItems[currentIndex].lastSelectedSideStickerID != 0 && carShopData.roleItems[currentRole].shopItems[currentIndex].modifyInfo[carShopData.roleItems[currentRole].shopItems[currentIndex].lastSelectedSideStickerID].unlockedSideSticker == true) {
                        GameObject.FindGameObjectWithTag ("Player").transform.GetChild (4).transform.GetChild (carShopData.roleItems[currentRole].shopItems[currentIndex].lastSelectedSideStickerID - 1).gameObject.SetActive (true);
                    }

                }

            } else {
                modifyMenuButton.interactable = false;
            }

        }

        public void CheckWhichRole () {
            if (carShopData.roleItems[currentRole].shopItems[currentIndex].isUnlocked == true) {
                if (currentRole == 0) {
                    freeStart.SetActive (true);

                } else {
                    roleStart.SetActive (true);
                }

            } else {
                freeStart.SetActive (false);
                roleStart.SetActive (false);
            }
        }

        public void SpoilerButtonEvent () {
            selectedModify = 0;
            if (carShopData.roleItems[currentRole].shopItems[currentIndex].lastSelectedSpoilerID != 0) {
                GameObject.FindGameObjectWithTag ("Player").transform.GetChild (selectedModify).transform.GetChild (carShopData.roleItems[currentRole].shopItems[currentIndex].lastSelectedSpoilerID - 1).gameObject.SetActive (false);
            }

            currentSelectedSpoilerID = 0;

            CheckSpoilerModifyUnlockedStatus ();
        }

        public void SkirtsButtonEvent () {
            selectedModify = 1;
            if (carShopData.roleItems[currentRole].shopItems[currentIndex].lastSelectedSkirtID != 0) {
                GameObject.FindGameObjectWithTag ("Player").transform.GetChild (selectedModify).transform.GetChild (carShopData.roleItems[currentRole].shopItems[currentIndex].lastSelectedSkirtID - 1).gameObject.SetActive (false);
            }
            currentSelectedSkirtID = 0;

            CheckSkirtModifyUnlockedStatus ();

        }

        public void FrontStickersButtonEvent () {
            selectedModify = 2;
            if (carShopData.roleItems[currentRole].shopItems[currentIndex].lastSelectedFrontStickerID != 0) {
                GameObject.FindGameObjectWithTag ("Player").transform.GetChild (selectedModify).transform.GetChild (carShopData.roleItems[currentRole].shopItems[currentIndex].lastSelectedFrontStickerID - 1).gameObject.SetActive (false);
            }
            currentSelectedFrontStickerID = 0;

            CheckFrontStickerModifyUnlockedStatus ();

        }

        public void RimsButtonEvent () {
            selectedModify = 3;
            if (carShopData.roleItems[currentRole].shopItems[currentIndex].lastSelectedRimID != 0) {
                GameObject.FindGameObjectWithTag ("Player").transform.GetChild (selectedModify).transform.GetChild (carShopData.roleItems[currentRole].shopItems[currentIndex].lastSelectedRimID - 1).gameObject.SetActive (false);
                GameObject.FindGameObjectWithTag ("Player").transform.GetChild (selectedModify).transform.GetChild (0).gameObject.SetActive (true);
            } else {
                GameObject.FindGameObjectWithTag ("Player").transform.GetChild (selectedModify).transform.GetChild (0).gameObject.SetActive (true);
            }
            currentSelectedRimID = 1;

            CheckRimModifyUnlockedStatus ();

        }

        public void SideStickersButtonEvent () {
            selectedModify = 4;
            if (carShopData.roleItems[currentRole].shopItems[currentIndex].lastSelectedSideStickerID != 0) {
                GameObject.FindGameObjectWithTag ("Player").transform.GetChild (selectedModify).transform.GetChild (carShopData.roleItems[currentRole].shopItems[currentIndex].lastSelectedSideStickerID - 1).gameObject.SetActive (false);
            }
            currentSelectedSideStickerID = 0;

            CheckSideStickerModifyUnlockedStatus ();

        }

        public void SpoilerSelect (int selectedSpoilerID) {

            if (selectedSpoilerID == 0) {
                if (currentSelectedSpoilerID != 0) {
                    GameObject.FindGameObjectWithTag ("Player").transform.GetChild (selectedModify).transform.GetChild (currentSelectedSpoilerID - 1).gameObject.SetActive (false);
                    currentSelectedSpoilerID = selectedSpoilerID;
                    Debug.Log ("selected=0 , current!=0");
                }
            } else {

                if (currentSelectedSpoilerID != 0) {
                    GameObject.FindGameObjectWithTag ("Player").transform.GetChild (selectedModify).transform.GetChild (currentSelectedSpoilerID - 1).gameObject.SetActive (false);
                    currentSelectedSpoilerID = selectedSpoilerID;
                    GameObject.FindGameObjectWithTag ("Player").transform.GetChild (selectedModify).transform.GetChild (selectedSpoilerID - 1).gameObject.SetActive (true);
                    Debug.Log ("selected!=0 , current!=0");
                } else {
                    currentSelectedSpoilerID = selectedSpoilerID;
                    GameObject.FindGameObjectWithTag ("Player").transform.GetChild (selectedModify).transform.GetChild (selectedSpoilerID - 1).gameObject.SetActive (true);
                    Debug.Log ("selected!=0 , current=0");
                }

            }

            CheckSpoilerModifyUnlockedStatus ();

        }

        public void SkirtSelect (int selectedSkirtID) {

            if (selectedSkirtID == 0) {
                if (currentSelectedSkirtID != 0) {
                    GameObject.FindGameObjectWithTag ("Player").transform.GetChild (selectedModify).transform.GetChild (currentSelectedSkirtID - 1).gameObject.SetActive (false);
                    currentSelectedSpoilerID = selectedSkirtID;
                    Debug.Log ("selected=0 , current!=0");
                }
            } else {
                if (currentSelectedSkirtID != 0) {
                    GameObject.FindGameObjectWithTag ("Player").transform.GetChild (selectedModify).transform.GetChild (currentSelectedSkirtID - 1).gameObject.SetActive (false);
                    currentSelectedSkirtID = selectedSkirtID;
                    GameObject.FindGameObjectWithTag ("Player").transform.GetChild (selectedModify).transform.GetChild (selectedSkirtID - 1).gameObject.SetActive (true);
                    Debug.Log ("selected!=0 , current!=0");
                } else {
                    currentSelectedSkirtID = selectedSkirtID;
                    GameObject.FindGameObjectWithTag ("Player").transform.GetChild (selectedModify).transform.GetChild (selectedSkirtID - 1).gameObject.SetActive (true);
                    Debug.Log ("selected!=0 , current=0");
                }
            }

            CheckSkirtModifyUnlockedStatus ();

        }

        public void FrontStickerSelect (int selectedFrontStickerID) {

            if (selectedFrontStickerID == 0) {
                if (currentSelectedFrontStickerID != 0) {
                    GameObject.FindGameObjectWithTag ("Player").transform.GetChild (selectedModify).transform.GetChild (currentSelectedFrontStickerID - 1).gameObject.SetActive (false);
                    selectModifyGO.SetActive (true);
                    buyModifyGO.SetActive (false);
                    currentSelectedSpoilerID = selectedFrontStickerID;
                    Debug.Log ("selected=0 , current!=0");
                }
            } else {
                if (currentSelectedFrontStickerID != 0) {
                    GameObject.FindGameObjectWithTag ("Player").transform.GetChild (selectedModify).transform.GetChild (currentSelectedFrontStickerID - 1).gameObject.SetActive (false);
                    currentSelectedFrontStickerID = selectedFrontStickerID;
                    GameObject.FindGameObjectWithTag ("Player").transform.GetChild (selectedModify).transform.GetChild (selectedFrontStickerID - 1).gameObject.SetActive (true);
                    Debug.Log ("selected!=0 , current!=0");
                } else {
                    currentSelectedFrontStickerID = selectedFrontStickerID;
                    GameObject.FindGameObjectWithTag ("Player").transform.GetChild (selectedModify).transform.GetChild (selectedFrontStickerID - 1).gameObject.SetActive (true);
                    Debug.Log ("selected!=0 , current=0");

                }
            }

            CheckFrontStickerModifyUnlockedStatus ();

        }

        public void RimSelect (int selectedRimID) {

            if (selectedRimID == 0) {
                if (currentSelectedRimID != 0) {
                    GameObject.FindGameObjectWithTag ("Player").transform.GetChild (selectedModify).transform.GetChild (0).gameObject.SetActive (true);
                    GameObject.FindGameObjectWithTag ("Player").transform.GetChild (selectedModify).transform.GetChild (currentSelectedRimID - 1).gameObject.SetActive (false);
                    currentSelectedSpoilerID = selectedRimID;
                    Debug.Log ("selected=0 , current!=0");
                } else {
                    GameObject.FindGameObjectWithTag ("Player").transform.GetChild (selectedModify).transform.GetChild (0).gameObject.SetActive (true);
                    currentSelectedRimID = 0;
                    Debug.Log ("selected=0 , current=0");
                }
            } else {

                if (currentSelectedRimID != 0) {

                    GameObject.FindGameObjectWithTag ("Player").transform.GetChild (selectedModify).transform.GetChild (currentSelectedRimID - 1).gameObject.SetActive (false);
                    currentSelectedRimID = selectedRimID;
                    GameObject.FindGameObjectWithTag ("Player").transform.GetChild (selectedModify).transform.GetChild (selectedRimID - 1).gameObject.SetActive (true);
                    Debug.Log ("selected!=0 , current!=0");

                } else {
                    currentSelectedRimID = selectedRimID;
                    GameObject.FindGameObjectWithTag ("Player").transform.GetChild (selectedModify).transform.GetChild (0).gameObject.SetActive (false);
                    GameObject.FindGameObjectWithTag ("Player").transform.GetChild (selectedModify).transform.GetChild (selectedRimID - 1).gameObject.SetActive (true);
                    Debug.Log ("selected!=0 , current=0");
                }
            }

            CheckRimModifyUnlockedStatus ();

        }

        public void SideStickerSelect (int selectedSideStickerID) {

            if (selectedSideStickerID == 0) {
                if (currentSelectedSideStickerID != 0) {
                    GameObject.FindGameObjectWithTag ("Player").transform.GetChild (selectedModify).transform.GetChild (currentSelectedSideStickerID - 1).gameObject.SetActive (false);
                    currentSelectedSpoilerID = selectedSideStickerID;
                    Debug.Log ("selected=0 , current!=0");
                }
            } else {
                if (currentSelectedSideStickerID != 0) {
                    GameObject.FindGameObjectWithTag ("Player").transform.GetChild (selectedModify).transform.GetChild (currentSelectedSideStickerID - 1).gameObject.SetActive (false);
                    currentSelectedSideStickerID = selectedSideStickerID;
                    GameObject.FindGameObjectWithTag ("Player").transform.GetChild (selectedModify).transform.GetChild (selectedSideStickerID - 1).gameObject.SetActive (true);
                    Debug.Log ("selected!=0 , current!=0");
                } else {
                    currentSelectedSideStickerID = selectedSideStickerID;
                    GameObject.FindGameObjectWithTag ("Player").transform.GetChild (selectedModify).transform.GetChild (selectedSideStickerID - 1).gameObject.SetActive (true);
                    Debug.Log ("selected!=0 , current=0");

                }
            }

            CheckSideStickerModifyUnlockedStatus ();

        }

        public void UnlockSelectedModify () {
            switch (selectedModify) {
                case 0:
                    if (carShopData.roleItems[currentRole].shopItems[currentIndex].isUnlocked == true) {
                        if (gameData.totalMoney >= carShopData.roleItems[currentRole].shopItems[currentIndex].modifyInfo[currentSelectedSpoilerID].SpoilerCost) {
                            carShopData.roleItems[currentRole].shopItems[currentIndex].lastSelectedSpoilerID = currentSelectedSpoilerID;
                            gameData.totalMoney -= carShopData.roleItems[currentRole].shopItems[currentIndex].modifyInfo[currentSelectedSpoilerID].SpoilerCost;
                            carShopData.roleItems[currentRole].shopItems[currentIndex].modifyInfo[currentSelectedSpoilerID].unlockedSpoiler = true;
                            ReadWriteAllRoles.WriteGameProp (gameData);
                            ReadWriteAllRoles.WriteCarProp (carShopData);
                            carShopData = ReadWriteAllRoles.ReadCarProp (carShopData);
                            CheckSpoilerModifyUnlockedStatus ();
                        } else {
                            notEnoughMoneyModify.SetActive (true);
                        }

                    } else {
                        checkCarOwner.SetActive (true);
                    }

                    break;

                case 1:
                    if (carShopData.roleItems[currentRole].shopItems[currentIndex].isUnlocked == true) {
                        if (gameData.totalMoney >= carShopData.roleItems[currentRole].shopItems[currentIndex].modifyInfo[currentSelectedSkirtID].SkirtCost) {
                            carShopData.roleItems[currentRole].shopItems[currentIndex].lastSelectedSkirtID = currentSelectedSkirtID;
                            gameData.totalMoney -= carShopData.roleItems[currentRole].shopItems[currentIndex].modifyInfo[currentSelectedSkirtID].SkirtCost;
                            carShopData.roleItems[currentRole].shopItems[currentIndex].modifyInfo[currentSelectedSkirtID].unlockedSkirt = true;

                            ReadWriteAllRoles.WriteGameProp (gameData);
                            ReadWriteAllRoles.WriteCarProp (carShopData);
                            carShopData = ReadWriteAllRoles.ReadCarProp (carShopData);

                            CheckSkirtModifyUnlockedStatus ();
                        } else {
                            notEnoughMoneyModify.SetActive (true);
                        }

                    } else {
                        checkCarOwner.SetActive (true);
                    }
                    break;

                case 2:
                    if (carShopData.roleItems[currentRole].shopItems[currentIndex].isUnlocked == true) {
                        if (gameData.totalMoney >= carShopData.roleItems[currentRole].shopItems[currentIndex].modifyInfo[currentSelectedFrontStickerID].FrontStickerCost) {
                            carShopData.roleItems[currentRole].shopItems[currentIndex].lastSelectedFrontStickerID = currentSelectedFrontStickerID;
                            gameData.totalMoney -= carShopData.roleItems[currentRole].shopItems[currentIndex].modifyInfo[currentSelectedFrontStickerID].FrontStickerCost;
                            carShopData.roleItems[currentRole].shopItems[currentIndex].modifyInfo[currentSelectedFrontStickerID].unlockedFrontSticker = true;
                            ReadWriteAllRoles.WriteGameProp (gameData);
                            ReadWriteAllRoles.WriteCarProp (carShopData);
                            carShopData = ReadWriteAllRoles.ReadCarProp (carShopData);

                            CheckFrontStickerModifyUnlockedStatus ();
                        } else {
                            notEnoughMoneyModify.SetActive (true);
                        }
                    } else {
                        checkCarOwner.SetActive (true);
                    }
                    break;

                case 3:
                    if (carShopData.roleItems[currentRole].shopItems[currentIndex].isUnlocked == true) {
                        if (gameData.totalMoney >= carShopData.roleItems[currentRole].shopItems[currentIndex].modifyInfo[currentSelectedRimID].RimCost) {
                            carShopData.roleItems[currentRole].shopItems[currentIndex].lastSelectedRimID = currentSelectedRimID;
                            gameData.totalMoney -= carShopData.roleItems[currentRole].shopItems[currentIndex].modifyInfo[currentSelectedRimID].RimCost;
                            carShopData.roleItems[currentRole].shopItems[currentIndex].modifyInfo[currentSelectedRimID].unlockedRim = true;
                            ReadWriteAllRoles.WriteGameProp (gameData);
                            ReadWriteAllRoles.WriteCarProp (carShopData);
                            carShopData = ReadWriteAllRoles.ReadCarProp (carShopData);

                            CheckRimModifyUnlockedStatus ();
                        } else {
                            notEnoughMoneyModify.SetActive (true);
                        }

                    } else {
                        checkCarOwner.SetActive (true);
                    }
                    break;

                case 4:
                    if (carShopData.roleItems[currentRole].shopItems[currentIndex].isUnlocked == true) {
                        if (gameData.totalMoney >= carShopData.roleItems[currentRole].shopItems[currentIndex].modifyInfo[currentSelectedSideStickerID].SideStickerCost) {
                            carShopData.roleItems[currentRole].shopItems[currentIndex].lastSelectedSideStickerID = currentSelectedSideStickerID;
                            gameData.totalMoney -= carShopData.roleItems[currentRole].shopItems[currentIndex].modifyInfo[currentSelectedSideStickerID].SideStickerCost;
                            carShopData.roleItems[currentRole].shopItems[currentIndex].modifyInfo[currentSelectedSideStickerID].unlockedSideSticker = true;
                            ReadWriteAllRoles.WriteGameProp (gameData);
                            ReadWriteAllRoles.WriteCarProp (carShopData);
                            carShopData = ReadWriteAllRoles.ReadCarProp (carShopData);

                            CheckSideStickerModifyUnlockedStatus ();
                        } else {
                            notEnoughMoneyModify.SetActive (true);
                        }

                    } else {
                        checkCarOwner.SetActive (true);
                    }
                    break;
            }

        }

        public void SelectItem () {
            switch (selectedModify) {
                case 0:
                    carShopData.roleItems[currentRole].shopItems[currentIndex].lastSelectedSpoilerID = currentSelectedSpoilerID;
                    ReadWriteAllRoles.WriteCarProp (carShopData);
                    carShopData = ReadWriteAllRoles.ReadCarProp (carShopData);
                    break;

                case 1:
                    carShopData.roleItems[currentRole].shopItems[currentIndex].lastSelectedSkirtID = currentSelectedSkirtID;
                    ReadWriteAllRoles.WriteCarProp (carShopData);
                    carShopData = ReadWriteAllRoles.ReadCarProp (carShopData);
                    break;

                case 2:
                    carShopData.roleItems[currentRole].shopItems[currentIndex].lastSelectedFrontStickerID = currentSelectedFrontStickerID;
                    ReadWriteAllRoles.WriteCarProp (carShopData);
                    carShopData = ReadWriteAllRoles.ReadCarProp (carShopData);
                    break;

                case 3:
                    carShopData.roleItems[currentRole].shopItems[currentIndex].lastSelectedRimID = currentSelectedRimID;
                    ReadWriteAllRoles.WriteCarProp (carShopData);
                    carShopData = ReadWriteAllRoles.ReadCarProp (carShopData);
                    break;

                case 4:
                    carShopData.roleItems[currentRole].shopItems[currentIndex].lastSelectedSideStickerID = currentSelectedSideStickerID;
                    ReadWriteAllRoles.WriteCarProp (carShopData);
                    carShopData = ReadWriteAllRoles.ReadCarProp (carShopData);
                    break;
            }

        }
        public void BackModifyFromMenu () {
            switch (selectedModify) {
                case 0:
                    if (carShopData.roleItems[currentRole].shopItems[currentIndex].lastSelectedSpoilerID != 0) {
                        if (currentSelectedSpoilerID != 0) {
                            GameObject.FindGameObjectWithTag ("Player").transform.GetChild (selectedModify).transform.GetChild (currentSelectedSpoilerID - 1).gameObject.SetActive (false);
                        }

                        GameObject.FindGameObjectWithTag ("Player").transform.GetChild (selectedModify).transform.GetChild (carShopData.roleItems[currentRole].shopItems[currentIndex].lastSelectedSpoilerID - 1).gameObject.SetActive (true);

                    } else {
                        if (currentSelectedSpoilerID != 0) {
                            GameObject.FindGameObjectWithTag ("Player").transform.GetChild (selectedModify).transform.GetChild (currentSelectedSpoilerID - 1).gameObject.SetActive (false);
                        }
                    }
                    break;

                case 1:
                    if (carShopData.roleItems[currentRole].shopItems[currentIndex].lastSelectedSkirtID != 0) {
                        if (currentSelectedSkirtID != 0) {
                            GameObject.FindGameObjectWithTag ("Player").transform.GetChild (selectedModify).transform.GetChild (currentSelectedSkirtID - 1).gameObject.SetActive (false);
                        }

                        GameObject.FindGameObjectWithTag ("Player").transform.GetChild (selectedModify).transform.GetChild (carShopData.roleItems[currentRole].shopItems[currentIndex].lastSelectedSkirtID - 1).gameObject.SetActive (true);

                    } else {
                        if (currentSelectedSkirtID != 0) {
                            GameObject.FindGameObjectWithTag ("Player").transform.GetChild (selectedModify).transform.GetChild (currentSelectedSkirtID - 1).gameObject.SetActive (false);
                        }
                    }
                    break;

                case 2:
                    if (carShopData.roleItems[currentRole].shopItems[currentIndex].lastSelectedFrontStickerID != 0) {
                        if (currentSelectedFrontStickerID != 0) {
                            GameObject.FindGameObjectWithTag ("Player").transform.GetChild (selectedModify).transform.GetChild (currentSelectedFrontStickerID - 1).gameObject.SetActive (false);
                        }

                        GameObject.FindGameObjectWithTag ("Player").transform.GetChild (selectedModify).transform.GetChild (carShopData.roleItems[currentRole].shopItems[currentIndex].lastSelectedFrontStickerID - 1).gameObject.SetActive (true);

                    } else {
                        if (currentSelectedFrontStickerID != 0) {
                            GameObject.FindGameObjectWithTag ("Player").transform.GetChild (selectedModify).transform.GetChild (currentSelectedFrontStickerID - 1).gameObject.SetActive (false);
                        }

                    }
                    break;

                case 3:
                    if (carShopData.roleItems[currentRole].shopItems[currentIndex].lastSelectedRimID != 0) {
                        if (currentSelectedRimID != 0) {
                            GameObject.FindGameObjectWithTag ("Player").transform.GetChild (selectedModify).transform.GetChild (currentSelectedRimID - 1).gameObject.SetActive (false);
                        } else {
                            GameObject.FindGameObjectWithTag ("Player").transform.GetChild (selectedModify).transform.GetChild (currentSelectedRimID).gameObject.SetActive (false);
                        }
                        GameObject.FindGameObjectWithTag ("Player").transform.GetChild (selectedModify).transform.GetChild (carShopData.roleItems[currentRole].shopItems[currentIndex].lastSelectedRimID - 1).gameObject.SetActive (true);
                        Debug.Log ("selected!=0 , unlocked=yes");

                    } else {
                        if (currentSelectedRimID != 0) {
                            GameObject.FindGameObjectWithTag ("Player").transform.GetChild (selectedModify).transform.GetChild (currentSelectedRimID - 1).gameObject.SetActive (false);
                            GameObject.FindGameObjectWithTag ("Player").transform.GetChild (selectedModify).transform.GetChild (0).gameObject.SetActive (true);
                        } else {
                            GameObject.FindGameObjectWithTag ("Player").transform.GetChild (selectedModify).transform.GetChild (currentSelectedRimID - 1).gameObject.SetActive (false);
                        }

                        Debug.Log ("selected==0 , unlocked=yes");
                    }
                    break;

                case 4:
                    if (carShopData.roleItems[currentRole].shopItems[currentIndex].lastSelectedSideStickerID != 0) {
                        if (currentSelectedSideStickerID != 0) {
                            GameObject.FindGameObjectWithTag ("Player").transform.GetChild (selectedModify).transform.GetChild (currentSelectedSideStickerID - 1).gameObject.SetActive (false);
                        }

                        GameObject.FindGameObjectWithTag ("Player").transform.GetChild (selectedModify).transform.GetChild (carShopData.roleItems[currentRole].shopItems[currentIndex].lastSelectedSideStickerID - 1).gameObject.SetActive (true);

                    } else {
                        if (currentSelectedSideStickerID != 0) {
                            GameObject.FindGameObjectWithTag ("Player").transform.GetChild (selectedModify).transform.GetChild (currentSelectedSideStickerID - 1).gameObject.SetActive (false);
                        }

                    }
                    break;
            }

            camCont.ModifyViewTransition ();
        }

        public void CloseSkirtButton () {
            if (carShopData.roleItems[currentRole].shopItems[currentIndex].haveSkirts == false) {
                skirtModifySelectionButton.interactable = false;
            } else {
                skirtModifySelectionButton.interactable = true;
            }
        }

        public void CheckSkirtModifyUnlockedStatus () {
            if (carShopData.roleItems[currentRole].shopItems[currentIndex].modifyInfo[currentSelectedSkirtID].unlockedSkirt == true) {
                buyModifyGO.SetActive (false);
                selectModifyGO.SetActive (true);
                lockedUnlockedModify.sprite = unlockedModifySprite;
                modifyUnlockButtonText.text = "Açık";
            } else {
                buyModifyGO.SetActive (true);
                selectModifyGO.SetActive (false);
                lockedUnlockedModify.sprite = lockedSprite;
                modifyUnlockButtonText.text = "" + carShopData.roleItems[currentRole].shopItems[currentIndex].modifyInfo[currentSelectedSkirtID].SkirtCost + "$";
            }

        }

        public void CheckSpoilerModifyUnlockedStatus () {
            if (carShopData.roleItems[currentRole].shopItems[currentIndex].modifyInfo[currentSelectedSpoilerID].unlockedSpoiler == true) {
                buyModifyGO.SetActive (false);
                selectModifyGO.SetActive (true);
                lockedUnlockedModify.sprite = unlockedModifySprite;
                modifyUnlockButtonText.text = "Açık";
            } else {
                buyModifyGO.SetActive (true);
                selectModifyGO.SetActive (false);
                lockedUnlockedModify.sprite = lockedSprite;
                modifyUnlockButtonText.text = "" + carShopData.roleItems[currentRole].shopItems[currentIndex].modifyInfo[currentSelectedSpoilerID].SpoilerCost + "$";
            }

        }

        public void CheckSideStickerModifyUnlockedStatus () {
            if (carShopData.roleItems[currentRole].shopItems[currentIndex].modifyInfo[currentSelectedSideStickerID].unlockedSideSticker == true) {
                buyModifyGO.SetActive (false);
                selectModifyGO.SetActive (true);
                lockedUnlockedModify.sprite = unlockedModifySprite;
                modifyUnlockButtonText.text = "Açık";
            } else {
                buyModifyGO.SetActive (true);
                selectModifyGO.SetActive (false);
                lockedUnlockedModify.sprite = lockedSprite;
                modifyUnlockButtonText.text = "" + carShopData.roleItems[currentRole].shopItems[currentIndex].modifyInfo[currentSelectedSideStickerID].SideStickerCost + "$";
            }

        }

        public void CheckFrontStickerModifyUnlockedStatus () {
            if (carShopData.roleItems[currentRole].shopItems[currentIndex].modifyInfo[currentSelectedFrontStickerID].unlockedFrontSticker == true) {
                buyModifyGO.SetActive (false);
                selectModifyGO.SetActive (true);
                lockedUnlockedModify.sprite = unlockedModifySprite;
                modifyUnlockButtonText.text = "Açık";
            } else {
                buyModifyGO.SetActive (true);
                selectModifyGO.SetActive (false);
                lockedUnlockedModify.sprite = lockedSprite;
                modifyUnlockButtonText.text = "" + carShopData.roleItems[currentRole].shopItems[currentIndex].modifyInfo[currentSelectedFrontStickerID].FrontStickerCost + "$";
            }

        }

        public void CheckRimModifyUnlockedStatus () {
            if (carShopData.roleItems[currentRole].shopItems[currentIndex].modifyInfo[currentSelectedRimID].unlockedRim == true) {

                buyModifyGO.SetActive (false);
                selectModifyGO.SetActive (true);
                lockedUnlockedModify.sprite = unlockedModifySprite;
                modifyUnlockButtonText.text = "Açık";
            } else {
                buyModifyGO.SetActive (true);
                selectModifyGO.SetActive (false);
                lockedUnlockedModify.sprite = lockedSprite;
                modifyUnlockButtonText.text = "" + carShopData.roleItems[currentRole].shopItems[currentIndex].modifyInfo[currentSelectedRimID].RimCost + "$";
            }

        }

    }

}