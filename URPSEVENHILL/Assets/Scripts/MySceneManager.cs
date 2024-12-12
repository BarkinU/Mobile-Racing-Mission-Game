using UnityEngine;

[AddComponentMenu("Pablo/Scene Manager")]

public class MySceneManager : MonoBehaviour {

	private PABLO activeVehicle;
	public MyCamera activeCamera;
	private float originalTimeScale = 1f;

	#if ENTEREXIT
	public ENTEREXITPlayer activePlayer;
	#endif

	void Awake(){

		MyCamera.OnCameraSpawned += MyCamera_OnSpawned;

		PABLO.playerSpawned += spawned;

		#if ENTEREXIT
		ENTEREXITPlayer.OnPlayerSpawned += ENTEREXITPlayer_OnPlayerSpawned;
		#endif

		// Getting default time scale
		originalTimeScale = Time.timeScale;

		
	}

	private void Start(){
		activeVehicle = GameObject.FindGameObjectWithTag("Player").GetComponent<PABLO>();  
	}

	#region ONSPAWNED

	void spawned (PABLO spawned){


		#if ENTEREXIT
		if (spawned.gameObject.GetComponent<EnterExitPlayer> ())
			spawned.gameObject.GetComponent<EnterExitVehicle> ().correspondingCamera = activeCamera.gameObject;
		#endif

	}

	void MyCamera_OnSpawned (GameObject MyCamera){

		activeCamera = MyCamera.GetComponent<MyCamera>();

	}

	#if ENTEREXIT
	void ENTEREXITPlayer (ENTEREXITPlayer player){

		activePlayer = player;

	}
	#endif
	#endregion
	
	void OnDisable(){

		MyCamera.OnCameraSpawned -= MyCamera_OnSpawned;
		#if ENTEREXIT
		ENTEREXITPlayer.OnPlayerSpawned -= ENTEREXITPlayerOnPlayerSpawned;
		ENTEREXITPlayer.OnPlayerSpawned -= ENTEREXITPlayer_OnPlayerSpawned;
		#endif

	}

	#region singleton
	private static MySceneManager instance;
	public static MySceneManager Instance{
		
		get{
			
			if (instance == null) {

				instance = FindObjectOfType<MySceneManager> ();

				if (instance == null) {
					
					GameObject sceneManager = new GameObject ("Scene Manager");
					instance = sceneManager.AddComponent<MySceneManager> ();

				}
			}
			return instance;
		}
	}
	#endregion

}
