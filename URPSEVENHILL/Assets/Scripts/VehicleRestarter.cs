using UnityEngine;
using System.Collections;



public class VehicleRestarter : MonoBehaviour
{
    public Rigidbody RB;
    public PABLO pablo;
   	private bool cooldown = false;
    public void Start ()
    {
		wait();
		pablo = GameObject.FindGameObjectWithTag("Player").GetComponent<PABLO>();
		RB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
    }
	public void InstantRestart()
	{
				if ( cooldown == false ) 
				{   
					pablo.transform.rotation = Quaternion.Euler (0f, pablo.transform.eulerAngles.y, 0f);
					pablo.transform.position = new Vector3(pablo.transform.position.x+2, pablo.transform.position.y + 2, pablo.transform.position.z);
					Invoke("ResetCooldown",1.0f);
        			cooldown = true;
				}
			}

	void ResetCooldown()
	{
    	cooldown = false;
 	}
	IEnumerator wait(){
		yield return new WaitForEndOfFrame();

	}
	 
	

}
