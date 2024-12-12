using System;
using System.Collections;
using CarShopSystem;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[Serializable]
public class ColorEvent : UnityEvent<Color> { };

public class ColorPicker : MonoBehaviour {
    RectTransform Rect;
    Texture2D ColorTexture;
    public CarShopUI carNumber;
    public ColorEvent colorChange0;
    public ColorEvent colorChange1;
    public ColorEvent colorChange2;
    public ColorEvent colorChange3;
    public ColorEvent colorChange4;
    public ColorEvent colorChange5;
    public ColorEvent colorChange6;
    public ColorEvent colorChange7;
    public ColorEvent colorChange8;
    public ColorEvent colorChange9;
    public ColorEvent colorChange10;
    public ColorEvent colorChange11;
    public ColorEvent colorChange12;
    public ColorEvent colorChange13;
    public ColorEvent colorChange14;
    public ColorEvent colorChange15;
    public ColorEvent colorChange16;

    private Color colorFromImage;
    private GameObject carPrefab;
    private GameObject[] carModifies;
    private Material prefabBodyMat;
    private bool isColored;
    private Color col1, col2, col3, col4, col5, col6, col7, col8, col9, col10, col11, col12, col13, col14, col15, col16, col17;
    public GameObject shouldBuyCarError;

    void Start () {
        Rect = GetComponent<RectTransform> ();
        ColorTexture = GetComponent<Image> ().mainTexture as Texture2D;
    }

    public void PaintCars () {
        if (PlayerPrefsExtra.GetBool ("ColP1") == true) {
            col1 = PlayerPrefsExtra.GetColor ("Col1");
            colorChange0?.Invoke (col1);
        }
        if (PlayerPrefsExtra.GetBool ("ColP2") == true) {
            col2 = PlayerPrefsExtra.GetColor ("Col2");
            colorChange1?.Invoke (col2);
        }
        if (PlayerPrefsExtra.GetBool ("ColP3") == true) {
            col3 = PlayerPrefsExtra.GetColor ("Col3");
            colorChange2?.Invoke (col3);
        }
        if (PlayerPrefsExtra.GetBool ("ColP4") == true) {
            col4 = PlayerPrefsExtra.GetColor ("Col4");
            colorChange3?.Invoke (col4);
        }
        if (PlayerPrefsExtra.GetBool ("ColP5") == true) {
            col5 = PlayerPrefsExtra.GetColor ("Col5");
            colorChange4?.Invoke (col5);
        }
        if (PlayerPrefsExtra.GetBool ("ColP6") == true) {
            col6 = PlayerPrefsExtra.GetColor ("Col6");
            colorChange5?.Invoke (col6);
        }
        if (PlayerPrefsExtra.GetBool ("ColP7") == true) {
            col7 = PlayerPrefsExtra.GetColor ("Col7");
            colorChange6?.Invoke (col7);
        }
        if (PlayerPrefsExtra.GetBool ("ColP8") == true) {
            col8 = PlayerPrefsExtra.GetColor ("Col8");
            colorChange7?.Invoke (col8);

        }
        if (PlayerPrefsExtra.GetBool ("ColP9") == true) {
            col9 = PlayerPrefsExtra.GetColor ("Col9");
            colorChange8?.Invoke (col9);
        }
        if (PlayerPrefsExtra.GetBool ("ColP10") == true) {
            col10 = PlayerPrefsExtra.GetColor ("Col10");
            colorChange9?.Invoke (col10);
        }
        if (PlayerPrefsExtra.GetBool ("ColP11") == true) {
            col11 = PlayerPrefsExtra.GetColor ("Col11");
            colorChange10?.Invoke (col11);
        }
        if (PlayerPrefsExtra.GetBool ("ColP12") == true) {
            col12 = PlayerPrefsExtra.GetColor ("Col12");
            colorChange11?.Invoke (col12);
        }
        if (PlayerPrefsExtra.GetBool ("ColP13") == true) {
            col13 = PlayerPrefsExtra.GetColor ("Col13");
            colorChange12?.Invoke (col13);
        }
        if (PlayerPrefsExtra.GetBool ("ColP14") == true) {
            col14 = PlayerPrefsExtra.GetColor ("Col14");
            colorChange13?.Invoke (col14);
        }
        if (PlayerPrefsExtra.GetBool ("ColP15") == true) {
            col15 = PlayerPrefsExtra.GetColor ("Col15");
            colorChange14?.Invoke (col15);
        }
        if (PlayerPrefsExtra.GetBool ("ColP16") == true) {
            col16 = PlayerPrefsExtra.GetColor ("Col16");
            colorChange15?.Invoke (col16);
        }
        if (PlayerPrefsExtra.GetBool ("ColP17") == true) {
            col17 = PlayerPrefsExtra.GetColor ("Col17");
            colorChange16?.Invoke (col17);
        }

    }

    void Update () {
        if (RectTransformUtility.RectangleContainsScreenPoint (Rect, Input.mousePosition)) {

            Vector2 delta;
            RectTransformUtility.ScreenPointToLocalPointInRectangle (Rect, Input.mousePosition, null, out delta);
            string debug = "mousePosition =" + Input.mousePosition;

            debug += "<br>delta=" + delta;

            float width = Rect.rect.width;
            float height = Rect.rect.height;
            delta += new Vector2 (width * .5f, height * .5f);
            debug += "<br>offet delta=" + delta;

            float x = Mathf.Clamp (delta.x / width, 0f, 1f);
            float y = Mathf.Clamp (delta.y / height, 0f, 1f);
            debug += "<br>x=" + x + "y=" + y;

            int texX = Mathf.RoundToInt (x * ColorTexture.width);
            int texY = Mathf.RoundToInt (y * ColorTexture.height);
            debug += "<br>texX=" + texX + "texY=" + texY;

            colorFromImage = ColorTexture.GetPixel (texX, texY);

            if (Input.GetMouseButtonDown (0)) {
                carPrefab = GameObject.FindGameObjectWithTag ("body");
                carModifies = GameObject.FindGameObjectsWithTag ("Modify");
                for(int i=0; i<carModifies.Length; i++)
                {
                    carModifies[i].GetComponent<MeshRenderer>().materials[0].color=colorFromImage;
                }
                
                switch (carNumber.currentIndex) {
                    case 0:
                        prefabBodyMat = carPrefab.GetComponent<MeshRenderer> ().materials[1];
                        prefabBodyMat.color = colorFromImage;
                        isColored = true;
                        break;
                    case 1:
                        prefabBodyMat = carPrefab.GetComponent<MeshRenderer> ().materials[2];
                        prefabBodyMat.color = colorFromImage;
                        isColored = true;
                        break;
                    case 2:
                        prefabBodyMat = carPrefab.GetComponent<MeshRenderer> ().materials[2];
                        prefabBodyMat.color = colorFromImage;
                        isColored = true;
                        break;
                    case 3:
                        prefabBodyMat = carPrefab.GetComponent<MeshRenderer> ().materials[2];
                        prefabBodyMat.color = colorFromImage;
                        isColored = true;
                        break;
                    case 4:
                        prefabBodyMat = carPrefab.GetComponent<MeshRenderer> ().materials[1];
                        prefabBodyMat.color = colorFromImage;
                        isColored = true;
                        break;
                    case 5:
                        prefabBodyMat = carPrefab.GetComponent<MeshRenderer> ().materials[1];
                        prefabBodyMat.color = colorFromImage;
                        isColored = true;
                        break;
                    case 6:
                        prefabBodyMat = carPrefab.GetComponent<MeshRenderer> ().materials[2];
                        prefabBodyMat.color = colorFromImage;
                        isColored = true;
                        break;
                    case 7:
                        prefabBodyMat = carPrefab.GetComponent<MeshRenderer> ().materials[1];
                        prefabBodyMat.color = colorFromImage;
                        isColored = true;
                        break;
                    case 8:
                        prefabBodyMat = carPrefab.GetComponent<MeshRenderer> ().materials[2];
                        prefabBodyMat.color = colorFromImage;
                        isColored = true;
                        break;
                    case 9:
                        prefabBodyMat = carPrefab.GetComponent<MeshRenderer> ().materials[2];
                        prefabBodyMat.color = colorFromImage;
                        isColored = true;
                        break;
                    case 10:
                        prefabBodyMat = carPrefab.GetComponent<MeshRenderer> ().materials[1];
                        prefabBodyMat.color = colorFromImage;
                        isColored = true;
                        break;
                    case 11:
                        prefabBodyMat = carPrefab.GetComponent<MeshRenderer> ().materials[1];
                        prefabBodyMat.color = colorFromImage;
                        isColored = true;
                        break;
                    case 12:
                        prefabBodyMat = carPrefab.GetComponent<MeshRenderer> ().materials[2];
                        prefabBodyMat.color = colorFromImage;
                        isColored = true;
                        break;
                    case 13:
                        prefabBodyMat = carPrefab.GetComponent<MeshRenderer> ().materials[1];
                        prefabBodyMat.color = colorFromImage;
                        isColored = true;
                        break;
                    case 14:
                        prefabBodyMat = carPrefab.GetComponent<MeshRenderer> ().materials[0];
                        prefabBodyMat.color = colorFromImage;
                        isColored = true;
                        break;
                    case 15:
                        prefabBodyMat = carPrefab.GetComponent<MeshRenderer> ().materials[1];
                        prefabBodyMat.color = colorFromImage;
                        isColored = true;
                        break;
                    case 16:
                        prefabBodyMat = carPrefab.GetComponent<MeshRenderer> ().materials[2];
                        prefabBodyMat.color = colorFromImage;
                        isColored = true;
                        break;

                    default:
                        Console.WriteLine ("Error");
                        break;
                }

            }

        }
    }

    public void payChangeMatColor () {
        if (isColored == true) {

            if (carNumber.gameData.totalMoney >= 1000) {
                switch (carNumber.currentIndex) {
                    case 0:
                        if (carNumber.carShopData.roleItems[0].shopItems[carNumber.currentIndex].isUnlocked == true) {
                            colorChange0?.Invoke (prefabBodyMat.color);
                            PlayerPrefsExtra.SetColor ("Col1", prefabBodyMat.color);
                            carNumber.gameData.totalMoney -= 2000;
                            carNumber.totalMoneyText.text = " " + carNumber.gameData.totalMoney;

                            ReadWriteAllRoles.WriteGameProp (carNumber.gameData);
                            PlayerPrefsExtra.SetBool ("ColP1", true);
                            isColored = false; //Update col & save
                        } else {
                            shouldBuyCarError.SetActive (true);
                        }

                        break;
                    case 1:
                        if (carNumber.carShopData.roleItems[0].shopItems[carNumber.currentIndex].isUnlocked == true) {
                            colorChange1?.Invoke (prefabBodyMat.color);
                            PlayerPrefsExtra.SetColor ("Col2", prefabBodyMat.color);
                            carNumber.gameData.totalMoney -= 2000;
                            carNumber.totalMoneyText.text = " " + carNumber.gameData.totalMoney;

                            ReadWriteAllRoles.WriteGameProp (carNumber.gameData);
                            PlayerPrefsExtra.SetBool ("ColP2", true);
                            isColored = false;
                        } else {
                            shouldBuyCarError.SetActive (true);
                        }
                        break;
                    case 2:
                        if (carNumber.carShopData.roleItems[0].shopItems[carNumber.currentIndex].isUnlocked == true) {
                            colorChange2?.Invoke (prefabBodyMat.color);
                            PlayerPrefsExtra.SetColor ("Col3", prefabBodyMat.color);
                            carNumber.gameData.totalMoney -= 2000;
                            carNumber.totalMoneyText.text = " " + carNumber.gameData.totalMoney;

                            ReadWriteAllRoles.WriteGameProp (carNumber.gameData);
                            PlayerPrefsExtra.SetBool ("ColP3", true);
                            isColored = false;
                        } else {
                            shouldBuyCarError.SetActive (true);
                        }
                        break;
                    case 3:
                        if (carNumber.carShopData.roleItems[0].shopItems[carNumber.currentIndex].isUnlocked == true) {
                            colorChange3?.Invoke (prefabBodyMat.color);
                            PlayerPrefsExtra.SetColor ("Col4", prefabBodyMat.color);
                            carNumber.gameData.totalMoney -= 2000;
                            carNumber.totalMoneyText.text = " " + carNumber.gameData.totalMoney;

                            ReadWriteAllRoles.WriteGameProp (carNumber.gameData);
                            PlayerPrefsExtra.SetBool ("ColP4", true);
                            isColored = false;
                        } else {
                            shouldBuyCarError.SetActive (true);
                        }
                        break;

                    case 4:
                        if (carNumber.carShopData.roleItems[0].shopItems[carNumber.currentIndex].isUnlocked == true) {
                            colorChange4?.Invoke (prefabBodyMat.color);
                            PlayerPrefsExtra.SetColor ("Col5", prefabBodyMat.color);
                            carNumber.gameData.totalMoney -= 2000;
                            carNumber.totalMoneyText.text = " " + carNumber.gameData.totalMoney;

                            ReadWriteAllRoles.WriteGameProp (carNumber.gameData);
                            PlayerPrefsExtra.SetBool ("ColP5", true);
                            isColored = false;
                        } else {
                            shouldBuyCarError.SetActive (true);
                        }
                        break;
                    case 5:
                        if (carNumber.carShopData.roleItems[0].shopItems[carNumber.currentIndex].isUnlocked == true) {
                            colorChange5?.Invoke (prefabBodyMat.color);
                            PlayerPrefsExtra.SetColor ("Col6", prefabBodyMat.color);
                            carNumber.gameData.totalMoney -= 2000;
                            carNumber.totalMoneyText.text = " " + carNumber.gameData.totalMoney;

                            ReadWriteAllRoles.WriteGameProp (carNumber.gameData);
                            PlayerPrefsExtra.SetBool ("ColP6", true);
                            isColored = false;
                        } else {
                            shouldBuyCarError.SetActive (true);
                        }
                        break;
                    case 6:
                        if (carNumber.carShopData.roleItems[0].shopItems[carNumber.currentIndex].isUnlocked == true) {
                            colorChange6?.Invoke (prefabBodyMat.color);
                            PlayerPrefsExtra.SetColor ("Col7", prefabBodyMat.color);
                            carNumber.gameData.totalMoney -= 2000;
                            carNumber.totalMoneyText.text = " " + carNumber.gameData.totalMoney;

                            ReadWriteAllRoles.WriteGameProp (carNumber.gameData);
                            PlayerPrefsExtra.SetBool ("ColP7", true);
                            isColored = false;
                        } else {
                            shouldBuyCarError.SetActive (true);
                        }
                        break;
                    case 7:
                        if (carNumber.carShopData.roleItems[0].shopItems[carNumber.currentIndex].isUnlocked == true) {
                            colorChange7?.Invoke (prefabBodyMat.color);
                            PlayerPrefsExtra.SetColor ("Col8", prefabBodyMat.color);
                            carNumber.gameData.totalMoney -= 2000;
                            carNumber.totalMoneyText.text = " " + carNumber.gameData.totalMoney;

                            ReadWriteAllRoles.WriteGameProp (carNumber.gameData);
                            PlayerPrefsExtra.SetBool ("ColP8", true);
                            isColored = false;
                        } else {
                            shouldBuyCarError.SetActive (true);
                        }
                        break;
                    case 8:
                        if (carNumber.carShopData.roleItems[0].shopItems[carNumber.currentIndex].isUnlocked == true) {
                            colorChange8?.Invoke (prefabBodyMat.color);
                            PlayerPrefsExtra.SetColor ("Col9", prefabBodyMat.color);
                            carNumber.gameData.totalMoney -= 2000;
                            carNumber.totalMoneyText.text = " " + carNumber.gameData.totalMoney;

                            ReadWriteAllRoles.WriteGameProp (carNumber.gameData);
                            PlayerPrefsExtra.SetBool ("ColP9", true);
                            isColored = false;
                        } else {
                            shouldBuyCarError.SetActive (true);
                        }
                        break;
                    case 9:
                        if (carNumber.carShopData.roleItems[0].shopItems[carNumber.currentIndex].isUnlocked == true) {
                            colorChange9?.Invoke (prefabBodyMat.color);
                            PlayerPrefsExtra.SetColor ("Col10", prefabBodyMat.color);
                            carNumber.gameData.totalMoney -= 2000;
                            carNumber.totalMoneyText.text = " " + carNumber.gameData.totalMoney;

                            ReadWriteAllRoles.WriteGameProp (carNumber.gameData);
                            PlayerPrefsExtra.SetBool ("ColP10", true);
                            isColored = false;
                        } else {
                            shouldBuyCarError.SetActive (true);
                        }
                        break;
                    case 10:
                        if (carNumber.carShopData.roleItems[0].shopItems[carNumber.currentIndex].isUnlocked == true) {
                            colorChange10?.Invoke (prefabBodyMat.color);
                            PlayerPrefsExtra.SetColor ("Col11", prefabBodyMat.color);
                            carNumber.gameData.totalMoney -= 2000;
                            carNumber.totalMoneyText.text = " " + carNumber.gameData.totalMoney;

                            ReadWriteAllRoles.WriteGameProp (carNumber.gameData);
                            PlayerPrefsExtra.SetBool ("ColP11", true);
                            isColored = false;
                        } else {
                            shouldBuyCarError.SetActive (true);
                        }
                        break;
                    case 11:
                        if (carNumber.carShopData.roleItems[0].shopItems[carNumber.currentIndex].isUnlocked == true) {
                            colorChange11?.Invoke (prefabBodyMat.color);
                            PlayerPrefsExtra.SetColor ("Col12", prefabBodyMat.color);
                            carNumber.gameData.totalMoney -= 2000;
                            carNumber.totalMoneyText.text = " " + carNumber.gameData.totalMoney;

                            ReadWriteAllRoles.WriteGameProp (carNumber.gameData);
                            PlayerPrefsExtra.SetBool ("ColP12", true);
                            isColored = false;
                        } else {
                            shouldBuyCarError.SetActive (true);
                        }
                        break;
                    case 12:
                        if (carNumber.carShopData.roleItems[0].shopItems[carNumber.currentIndex].isUnlocked == true) {

                            colorChange12?.Invoke (prefabBodyMat.color);
                            PlayerPrefsExtra.SetColor ("Col13", prefabBodyMat.color);
                            carNumber.gameData.totalMoney -= 2000;
                            carNumber.totalMoneyText.text = " " + carNumber.gameData.totalMoney;

                            ReadWriteAllRoles.WriteGameProp (carNumber.gameData);
                            PlayerPrefsExtra.SetBool ("ColP13", true);
                            isColored = false;
                        } else {
                            shouldBuyCarError.SetActive (true);
                        }
                        break;
                    case 13:
                        if (carNumber.carShopData.roleItems[0].shopItems[carNumber.currentIndex].isUnlocked == true) {

                            colorChange13?.Invoke (prefabBodyMat.color);
                            PlayerPrefsExtra.SetColor ("Col14", prefabBodyMat.color);
                            carNumber.gameData.totalMoney -= 2000;
                            carNumber.totalMoneyText.text = " " + carNumber.gameData.totalMoney;

                            ReadWriteAllRoles.WriteGameProp (carNumber.gameData);
                            PlayerPrefsExtra.SetBool ("ColP14", true);
                            isColored = false;
                        } else {
                            shouldBuyCarError.SetActive (true);
                        }
                        break;
                    case 14:
                        if (carNumber.carShopData.roleItems[0].shopItems[carNumber.currentIndex].isUnlocked == true) {

                            colorChange14?.Invoke (prefabBodyMat.color);
                            PlayerPrefsExtra.SetColor ("Col15", prefabBodyMat.color);
                            carNumber.gameData.totalMoney -= 2000;
                            carNumber.totalMoneyText.text = " " + carNumber.gameData.totalMoney;

                            ReadWriteAllRoles.WriteGameProp (carNumber.gameData);
                            PlayerPrefsExtra.SetBool ("ColP15", true);
                            isColored = false;
                        } else {
                            shouldBuyCarError.SetActive (true);
                        }
                        break;
                    case 15:
                        if (carNumber.carShopData.roleItems[0].shopItems[carNumber.currentIndex].isUnlocked == true) {

                            colorChange15?.Invoke (prefabBodyMat.color);
                            PlayerPrefsExtra.SetColor ("Col16", prefabBodyMat.color);
                            carNumber.gameData.totalMoney -= 2000;
                            carNumber.totalMoneyText.text = " " + carNumber.gameData.totalMoney;

                            ReadWriteAllRoles.WriteGameProp (carNumber.gameData);
                            PlayerPrefsExtra.SetBool ("ColP16", true);
                            isColored = false;
                        } else {
                            shouldBuyCarError.SetActive (true);
                        }
                        break;
                    case 16:
                        if (carNumber.carShopData.roleItems[0].shopItems[carNumber.currentIndex].isUnlocked == true) {

                            colorChange16?.Invoke (prefabBodyMat.color);
                            PlayerPrefsExtra.SetColor ("Col17", prefabBodyMat.color);
                            carNumber.gameData.totalMoney -= 2000;
                            carNumber.totalMoneyText.text = " " + carNumber.gameData.totalMoney;

                            ReadWriteAllRoles.WriteGameProp (carNumber.gameData);
                            PlayerPrefsExtra.SetBool ("ColP17", true);
                            isColored = false;
                        } else {
                            shouldBuyCarError.SetActive (true);
                        }
                        break;

                    default:
                        Console.WriteLine ("Error");
                        break;
                }

            }
        }
    }
}