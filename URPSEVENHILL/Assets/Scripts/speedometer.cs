using UnityEngine;
using TMPro;
public class speedometer : MonoBehaviour
{
    #region Variables
    public PABLO target;

    [Header("UI")]
    public TextMeshProUGUI speedLabel; // The label that displays the speed;
    private float speed = 0.0f;
    private float time;
    private float time1;
    private float interpolateTime =.7f;
    private int gear;
    public TextMeshProUGUI gearLabel;
    #endregion

    public void Start(){

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<PABLO>();
            speedLabel.richText = true;


    }
    
    void Update()
    {
        SpeedoLabel();
        GearLabel();
    }
    
    void SpeedoLabel(){
        // 3.6f to convert in kilometers
        // ** The speed must be clamped by the car controller **

        speed = target.speed;
        time += Time.deltaTime;

        if(time > interpolateTime){
            time = 0f;
            speedLabel.text = ((int)speed)+"<size=50>km/h</size>";
           
        }
        
    }
    
    void GearLabel(){
        
        gear = target.currentGear;
        time1 += Time.deltaTime;
        if(time1 > 0.2f){
            time1=0f;
            gearLabel.text=(gear)+ "";
        }
    }
}
