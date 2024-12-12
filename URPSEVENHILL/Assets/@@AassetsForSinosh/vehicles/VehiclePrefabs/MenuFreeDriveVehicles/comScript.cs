using System.Collections;
using UnityEngine;

public class comScript : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;

     [SerializeField]
    private Vector3 com ;
    private void Awake(){
        rb.centerOfMass=com;
    }
}
