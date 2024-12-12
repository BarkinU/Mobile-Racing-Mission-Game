using UnityEngine;

public class objectSounds : MonoBehaviour
{ 
    public AudioClip hit;
    private AudioSource hitsource;
    private void Awake() {

        hitsource = gameObject.GetComponent<AudioSource>();
        hitsource.clip = hit;
        hitsource.spatialBlend = 0.5f;
        hitsource.maxDistance = 30f;    
        hitsource.minDistance =0.1f;

        

    }

private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Player"))
        hitsource.Play();
		
	 }


}
