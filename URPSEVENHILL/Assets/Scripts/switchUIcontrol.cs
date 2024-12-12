using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class switchUIcontrol : MonoBehaviour
{
    [SerializeField] public Button previousButton;
    [SerializeField] public Button nextButton;
    [ShowOnly]public int index=0;
    [ShowOnly][Range(0,1)]public int switchValue;
    public GameObject arrows;
    public GameObject steeringWheel;
    public GameObject menuSW;
    public GameObject menuARROWS;

    void Awake(){
        switchValue = PlayerPrefs.GetInt("myValue");

        if(switchValue == 0)
        {
            steeringWheel.gameObject.SetActive(true);
            arrows.gameObject.SetActive(false);
            menuSW.gameObject.SetActive(true);
            menuARROWS.gameObject.SetActive(false);
            nextButton.interactable = (true);
            previousButton.interactable = (false);
            
        }

        if(switchValue == 1)
        {
            steeringWheel.gameObject.SetActive(false);
            arrows.gameObject.SetActive(true);
            menuARROWS.gameObject.SetActive(true);
            menuSW.gameObject.SetActive(false);
            nextButton.interactable = (false);
            previousButton.interactable = (true);

        }
        
        nextButton.onClick.AddListener(TaskOnClick);
        previousButton.onClick.AddListener(TaskOnClick);
        myIndex = PlayerPrefs.GetInt("DROPDOWNSKY");
        SetQuality(myIndex);



    }
    
    public void ChangeControl(int _change)
    {
        switchValue+= _change;
        SelectControl(switchValue);
        PlayerPrefs.SetInt("myValue",switchValue);
        
    }
    
    public void SelectControl(int _index){
        nextButton.interactable = (_index == 0);
        previousButton.interactable = (_index == 1);

        if(_index == 0)
        {
            menuSW.gameObject.SetActive(true);
        }
            if(_index == 1)
        {
            menuARROWS.gameObject.SetActive(true);
        }
        switchValue = _index;
        PlayerPrefs.SetInt("myValue",switchValue);

    }
    
    void TaskOnClick()
    {
        SelectControl(PlayerPrefs.GetInt("myValue"));
        index = PlayerPrefs.GetInt("myValue");
        if(index == 0){
        menuSW.gameObject.SetActive(true);
        menuARROWS.gameObject.SetActive(false);
        }

        if(index == 1){
        menuARROWS.gameObject.SetActive(true);
        menuSW.gameObject.SetActive(false);
        }

        nextButton.interactable = (index == 0 );
        previousButton.interactable = (index == 1);

            if(index == 1)
        {
            arrows.gameObject.SetActive(true);
            steeringWheel.gameObject.SetActive(false);
            Debug.Log("değer: "+1);
            switchValue = index;
        }
        if (index == 0)
        {
            steeringWheel.gameObject.SetActive(true);
            arrows.gameObject.SetActive(false);
            Debug.Log("değer: "+0);
            switchValue = index;
        }
        
        PlayerPrefs.SetInt("myValue",switchValue);



    }
    private int myIndex;
        public void SetQuality(int qualityIndex){
        
        QualitySettings.SetQualityLevel(qualityIndex);
        

    }
    public TMP_Dropdown dropDown;
     private void Start()
     {
         dropDown.value = PlayerPrefs.GetInt("DROPDOWNSKY");
 
         dropDown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropDown); });//dropDown listener
     }
     public void DropdownItemSelected(TMP_Dropdown dropdown)//a delegate , a method which is invoked when the value of the dropdown is changed
     {
         switch(dropdown.value)
         {
             case 0: PlayerPrefs.SetInt("DROPDOWNSKY", 0);
                 break;
             case 1: PlayerPrefs.SetInt("DROPDOWNSKY", 1);
                 break;
             case 2:PlayerPrefs.SetInt("DROPDOWNSKY", 2);
                 break;
             case 3:PlayerPrefs.SetInt("DROPDOWNSKY", 3);
                 break;

         }
     }


}
