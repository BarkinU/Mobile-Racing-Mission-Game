using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnMarkers : MonoBehaviour
{

private float toggle;
private float sasd;
private Vector3 thisObject;

void Start(){
    
    thisObject = transform.localScale;
    
}
private void LateUpdate() {
    
    sasd += Time.deltaTime;
    if(sasd > .1f)
    {
        if (toggle >= 0 && toggle <= 5)
        {
            toggle += 1;
            transform.localScale = new Vector3(thisObject.x+0.002f,thisObject.y+0.004f, 0);
            sasd = 0;
        
        }
        else if (toggle < 0)
        {
            toggle += 1;
            transform.localScale = new Vector3(thisObject.x-0.00f,thisObject.y-0.004f, 0);
            sasd = 0;
        
        }
        else {
            toggle = -5;
            sasd = 0;

        }
    }

}




}
