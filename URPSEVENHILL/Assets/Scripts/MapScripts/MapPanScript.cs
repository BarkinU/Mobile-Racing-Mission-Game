#pragma warning disable 0414

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class MapPanScript : MonoBehaviour //IDragHandler, IEndDragHandler 
{
    [SerializeField]
    private Camera cam;
    private Vector3 dragOrigin;
    
    [SerializeField]
    private float zooomStep, miniCamSize, maxCamSize;

    [SerializeField]
    [HideInInspector]public SpriteRenderer mapRenderer;

    private float mapMinZ, mapMaxZ, mapMinX, mapMaxX;
 

    private void Awake () {
        

        mapMinZ = mapRenderer.transform.position.z - mapRenderer.bounds.size.z / 2f;
        mapMaxZ = mapRenderer.transform.position.z + mapRenderer.bounds.size.z / 2f;


        mapMinX = mapRenderer.transform.position.x - mapRenderer.bounds.size.x / 2f;
        mapMaxX = mapRenderer.transform.position.x + mapRenderer.bounds.size.x / 2f;

    }

    // Update is called once per frame
    void Update () {
        
        PanCamera ();
    }

    private void PanCamera () {
        //save position of mouse in world space when drag starts (first time clicked)
        if (Input.GetMouseButtonDown (0)) {

            dragOrigin = cam.ScreenToWorldPoint (Input.mousePosition);

        }
        //calculate distance between drag origin and new position if it is still held down
        if (Input.GetMouseButton (0)) {

            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint (Input.mousePosition);

            
           cam.transform.position = ClampCamera (cam.transform.position + difference);
           //cam.transform.position+=difference;
        }
    }

    public void ZoomIn () {
        float newSize = cam.orthographicSize - zooomStep;

        cam.orthographicSize = Mathf.Clamp (newSize, miniCamSize, maxCamSize);

        cam.transform.position = ClampCamera (cam.transform.position);
    }


    public void ZoomOut () {
        float newSize = cam.orthographicSize + zooomStep;

        cam.orthographicSize = Mathf.Clamp (newSize, miniCamSize, maxCamSize);

        cam.transform.position = ClampCamera (cam.transform.position);
    }

    public Vector3 ClampCamera (Vector3 targetPosition) {

        float camHeight = cam.orthographicSize;
        float camWidth = cam.orthographicSize * cam.aspect ;

        float minX = mapMinX + camWidth;
        float maxX = mapMaxX - camWidth;
        float minZ = mapMinZ + camHeight;
        float maxZ = mapMaxZ - camHeight;

        float newX = Mathf.Clamp (targetPosition.x, minX, maxX);
        float newZ = Mathf.Clamp (targetPosition.z, minZ, maxZ);


        return new Vector3 (newX,targetPosition.y, newZ);
    }


    





}