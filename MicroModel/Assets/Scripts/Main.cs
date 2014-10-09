using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	private Transform translate;
	private Vector3 offset;
	private Vector3 mouseScreenSpace;
	private Vector3 targetScreenSpace;
	private bool isZoomed;
	private bool isPinched;
	private Vector3 camOriPos;
	private float fingerDistnace;
	private float camDistance;

	// Use this for initialization
	void Start () {
		translate = null;
		offset = Vector3.zero;
		mouseScreenSpace = Vector3.zero;
		targetScreenSpace = Vector3.zero;
		isZoomed = false;
		isPinched = false;
		camOriPos = Camera.main.transform.position;
		fingerDistnace = 0.0f;
		camDistance = 0.0f;

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit = new RaycastHit();
			if (Physics.Raycast(ray, out hit, 100, 1 << LayerMask.NameToLayer("Touchable")))
			{
				isZoomed = true;
				translate = hit.transform;
				// 把目标物体的世界空间转换到它自身的屏幕空间坐标
				targetScreenSpace = Camera.main.WorldToScreenPoint(translate.position);
				// 存储鼠标的屏幕坐标空间
				mouseScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, targetScreenSpace.z);
				// 计算目标物体与鼠标在世界空间的偏移量
				offset = translate.position - Camera.main.ScreenToWorldPoint(mouseScreenSpace);
				Debug.Log("offset is " + offset);
			}
		}else if (Input.GetMouseButtonUp(0))
		{
			isZoomed = false;
			translate = null;
		}

		if (translate != null)
		{
			// 重新计算鼠标的屏幕坐标空间
			mouseScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, targetScreenSpace.z);
			translate.position = Camera.main.ScreenToWorldPoint(mouseScreenSpace) + offset;
			Debug.Log("translate position is " + translate.position);
		}


		if (isZoomed == true) {
			ZoomIn();
		} else {
			ZoomOut();
		}

		if (Input.touchCount > 1)
		{
			if (Input.touches[0].phase == TouchPhase.Began || Input.touches[1].phase == TouchPhase.Began)
			{
				isPinched = true;
				camOriPos = Camera.main.transform.position;
				fingerDistnace = Vector3.SqrMagnitude(Input.touches[0].position - Input.touches[1].position);
			}
			else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[1].phase == TouchPhase.Ended)
			{
				isPinched = false;
			}
			else if (Input.touches[0].phase == TouchPhase.Moved || Input.touches[1].phase == TouchPhase.Moved)
			{
				Pinch(Input.touches[0].position, Input.touches[1].position);
			}
			
		}
	}

	private void ZoomIn() {
		Camera.main.fieldOfView = Mathf.Lerp (Camera.main.fieldOfView, 20, Time.deltaTime * 5);	 

	}

	private void ZoomOut() {
		Camera.main.fieldOfView = Mathf.Lerp (Camera.main.fieldOfView, 60, Time.deltaTime * 5);	

	}

	private void Pinch(Vector3 pos1,Vector3 pos2) {
		float distance = Vector3.SqrMagnitude (pos1 - pos2);
		camDistance = (distance - fingerDistnace) / fingerDistnace;
		camDistance = Mathf.Clamp (camDistance , -2, 5);
	}

	void LateUpdate() {
		if (isPinched) {
			Camera.main.transform.position = camOriPos + Camera.main.transform.TransformDirection (Vector3.forward) * camDistance;
		}
	}
	
}
