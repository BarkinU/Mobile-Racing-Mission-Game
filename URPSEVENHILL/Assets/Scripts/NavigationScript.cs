using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;
[RequireComponent (typeof (LineRenderer))]

public class NavigationScript : MonoBehaviour {

    public Camera cam;
    public NavMeshPath myPath;
    public Vector3 worldPosition;
    public LineRenderer myLineR;
    public Vector3 player;
    public bool navEnable;
    public int navCounter = 1;
    public GameObject marker;
    private Vector3 randomPlayerPoint;
    private int currentRole;
    private Vector3 missionPosition;
    public bool getSecondMission;
    private bool getAiNavi;
    public Button navButton;
    private RaycastHit hit2;
    public GameObject bigMap;


    void Awake () {

    }
    void Start () {
        myPath = new NavMeshPath ();

        currentRole = PlayerPrefs.GetInt ("rolePointer");
        StartCoroutine (CheckRole ());

    }

    IEnumerator CheckRole () {
        yield return new WaitForSeconds (0.2f);
        switch (currentRole) {
            case 1:
                missionPosition = GameObject.FindGameObjectWithTag ("busMission").transform.position;
                break;

            case 2:
                currentRole = 0;
                break;

            case 3:
                missionPosition = GameObject.FindGameObjectWithTag ("fireSpawn").transform.position;
                break;

            case 4:
                missionPosition = GameObject.FindGameObjectWithTag ("PoliceMission").transform.position;
                break;

            case 5:
                missionPosition = GameObject.FindGameObjectWithTag ("ambulanceMission").transform.position;
                break;

            case 6:
                missionPosition = GameObject.FindGameObjectWithTag ("Customer").transform.position;
                break;

        }
    }

    bool RandomPoint (Vector3 center, float range, out Vector3 result) {

        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition (randomPoint, out hit, 1000, NavMesh.AllAreas)) {
            result = hit.position;
            return true;
        }
        result = Vector3.zero;
        return false;
    }
    void LateUpdate () {

        if (currentRole == 0 && navEnable == true ) {

                MyPath ();
                DrawLine ();

        } else {
            if (getSecondMission == true) {
                switch (currentRole) {
                    case 1:
                        missionPosition = GameObject.FindGameObjectWithTag ("busMission").transform.position;
                        break;

                    case 2:
                        currentRole = 0;
                        break;

                    case 3:
                        missionPosition = GameObject.FindGameObjectWithTag ("fireSpawn").transform.position;
                        break;

                    case 4:
                        missionPosition = GameObject.FindGameObjectWithTag ("AiThief").transform.position;
                        getAiNavi=true;
                        break;

                    case 5:
                        missionPosition = GameObject.FindGameObjectWithTag ("ambulanceMission").transform.position;
                        break;

                    case 6:
                        missionPosition = GameObject.FindGameObjectWithTag ("CustomerDestination").transform.position;
                        break;

                }

                getSecondMission = false;
            }
            else
            {
                MyPathMission ();
                DrawLine ();
            }

        }

    }

    public void NavigationEnabled () {

        navCounter++;
        if (navCounter % 2 == 0) {
        
            navEnable = true;
            marker.gameObject.SetActive(true);
            navButton.image.color=new Color(255f,255f,255f,255f);
        
        }
        else
        {
            
            myLineR.positionCount = 0;
            navEnable = false;
            marker.gameObject.SetActive(false);
            navButton.image.color=new Color(0f,255f,43f,255f);

        }

    }

    void MyPathMission () {

        player = GameObject.FindGameObjectWithTag ("Player").transform.position;
        if (getAiNavi == true) {
            if (currentRole == 4) {
                missionPosition = GameObject.FindGameObjectWithTag ("AiThief").transform.position;
            }
        }
        
        NavMesh.CalculatePath (new Vector3 (player.x, missionPosition.y, player.z), missionPosition, NavMesh.AllAreas, myPath);

    }
    public float Timer;
    void MyPath () {
           if(bigMap.activeSelf)
           Debug.Log("bigmapactif");
           else
           {
               Debug.Log("bigmapinactif");
           }
            player = GameObject.FindGameObjectWithTag ("Player").transform.position;
            Ray ray2 = cam.ScreenPointToRay (Input.mousePosition);
            Physics.Raycast (ray2, out hit2, 10000);

            if (Input.GetMouseButtonDown (0) && bigMap.gameObject.activeSelf ) {
                 // hit2.collider.CompareTag ("path") && 

                Vector3 point;
                RandomPoint (hit2.point, 50, out point);
                worldPosition = point;

            Debug.Log ("BigMap Activated");
            }
            else if(hit2.collider == null)
            {

            }

            
            
            if(navEnable)
            {
                Timer += Time.deltaTime;
                
                if(Timer < .9f ){

                    marker.gameObject.SetActive(true);
                    
                }
                else if(Timer > .9f){

                    marker.gameObject.SetActive(false);
                    if(Timer >1.8f)
                    Timer = 0;

                }

                
                NavMesh.CalculatePath (new Vector3 (player.x, worldPosition.y, player.z), worldPosition, NavMesh.AllAreas, myPath);

            }
            
            else
            Debug.Log ("BigMap Deactivated");
    return;


    }

    void DrawLine () {
        if(myPath.corners.Length == 0)
        return;
        myLineR.positionCount = myPath.corners.Length;
    
        for (int i = 1; i < myLineR.positionCount; i++) 
        {

            myLineR.SetPosition (0, new Vector3 (player.x, player.y + 155, player.z));
            myLineR.SetPosition (i, new Vector3 (myPath.corners[i].x, myPath.corners[i].y + 155, myPath.corners[i].z));


            marker.transform.position = new Vector3 (myPath.corners[(myPath.corners.Length - 1)].x, myPath.corners[(myPath.corners.Length - 1)].y + 155, myPath.corners[(myPath.corners.Length - 1)].z);

        }

        if (currentRole == 0) {

            if (myLineR.positionCount != 0 && Vector3.Distance (player, myPath.corners[(myPath.corners.Length-1)]) < 25f) {

                navEnable = false;
                myLineR.positionCount =0;
                marker.gameObject.SetActive(false);
                
            }
        }

    }



}