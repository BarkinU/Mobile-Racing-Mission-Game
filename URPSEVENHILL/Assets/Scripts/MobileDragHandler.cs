
#pragma warning disable 0414

using UnityEngine;
using UnityEngine.EventSystems;

public class MobileDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler{

	private bool isPressing = false;

	public void OnDrag(PointerEventData data){

		isPressing = true;

		MySceneManager.Instance.activeCamera.OnDrag (data);

	}

	public void OnEndDrag(PointerEventData data){

		isPressing = false;

	}

}
