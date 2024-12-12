using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;


public class steeringWheelControl : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{

    [HideInInspector]public bool WheelBeingHeld = false;
    public RectTransform wheel;
    private float wheelAngle = 0f;
    private float lastWheelAngle = 0f;
    private Vector2 center;
    private float maxSteerAngle = 120f;
    private float releaseSpeed = 120f;
    public float outPut;
    public IM2 carControl;
    public Button sw;

    void Start()
    {
        carControl = GameObject.FindGameObjectWithTag("IM2").GetComponent<IM2>();
        sw = GetComponent<Button>();
        maxSteerAngle = 120f;
        releaseSpeed = 160;

    }

    void FixedUpdate()
    {
        if (!WheelBeingHeld && wheelAngle != 0f)
        {
            float DeltaAngle = releaseSpeed * Time.deltaTime;
            if (Mathf.Abs(DeltaAngle) > Mathf.Abs(wheelAngle))
                wheelAngle = 0f;
            else if (wheelAngle > 0f)
                wheelAngle -= DeltaAngle;
            else
                wheelAngle += DeltaAngle;
        }
        wheel.localEulerAngles = new Vector3(0, 0, -wheelAngle);
        outPut = wheelAngle / maxSteerAngle;
        carControl.steer = outPut;



    }


    public void OnPointerDown(PointerEventData data)
    {

        WheelBeingHeld = true;
        center = RectTransformUtility.WorldToScreenPoint(data.pressEventCamera, wheel.position);
        lastWheelAngle = Vector2.Angle(Vector2.up, data.position - center);


    }
    public void OnDrag(PointerEventData data)
    {


        float NewAngle = Vector2.Angle(Vector2.up, data.position - center);
        if ((data.position - center).magnitude >= 80)
        {
            if (data.position.x > center.x)
            {
                wheelAngle += Mathf.Lerp(0, NewAngle - lastWheelAngle, Time.smoothDeltaTime * 40f);

            }

            else
                wheelAngle -= Mathf.Lerp(0, NewAngle - lastWheelAngle, Time.smoothDeltaTime * 40f);

        }
        wheelAngle = Mathf.Clamp(wheelAngle, -maxSteerAngle, maxSteerAngle);
        lastWheelAngle = NewAngle;

    }

    public void OnPointerUp(PointerEventData data)
    {
        OnDrag(data);
        WheelBeingHeld = false;

    }





}
