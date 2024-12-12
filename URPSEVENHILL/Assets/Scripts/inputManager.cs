using UnityEngine;
[RequireComponent(typeof (PABLO))]

[AddComponentMenu("Pablo/Input Manager")]
public class inputManager : MonoBehaviour {
	public PABLO pablo;
	public string verticalInput = "Vertical";
	public string horizontalInput = "Horizontal";
	public KeyCode handbrakeKB = KeyCode.Space;

	private void Awake() {
		pablo = GetComponent<PABLO>();
	}

}