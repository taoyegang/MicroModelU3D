using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	private enum RotateDirection
	{
		UNKNOWN 	= -1,
		LEFT 		= 0,
		RIGHT 		= 1,
		UP 			= 2,
		DOWN 		= 3,
	};
	
	private RotateDirection direction;
	private Transform cameraTransform;
	private Transform translate;
	public GameObject target;
	public bool inputEnabled;
	private bool isDraged;
	private float perspectiveZoomSpeed;
	private float rotateSpeed;
	private Vector3 targetScreenSpace;
	private Vector3 mouseScreenSpace;
	private Vector3 offset;



	void Start () {
		direction = RotateDirection.UNKNOWN;
		cameraTransform = Camera.main.transform;
		translate = null;
		target = GameObject.FindGameObjectWithTag ("Target") as GameObject;
		inputEnabled = true;
		perspectiveZoomSpeed = 0.5f;
		rotateSpeed = 50.0f;
		targetScreenSpace = Vector3.zero;
		mouseScreenSpace = Vector3.zero;
		offset = Vector3.zero;
		isDraged = false;
	}
	
	// Update is called once per frame
	void Update () {
		HandleInput ();
	}

	public void HandleInput()
	{
		if (!inputEnabled)
			return;
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit = new RaycastHit();
			if (Physics.Raycast(ray, out hit, 100, 1 << LayerMask.NameToLayer("Touchable")))
			{
				GameObject hitObject = hit.transform.gameObject;
				Primitive primitive = (Primitive)hitObject.GetComponent(typeof(Primitive));
				bool isActive = primitive.getIsActive();
				Debug.Log("isActive = " + isActive);
				if(isActive) {
					Debug.Log("move");
					isDraged = true;
					translate = hit.transform;
					// 把目标物体的世界空间转换到它自身的屏幕空间坐标
					targetScreenSpace = Camera.main.WorldToScreenPoint(translate.position);
					// 存储鼠标的屏幕坐标空间
					mouseScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, targetScreenSpace.z);
					// 计算目标物体与鼠标在世界空间的偏移量 
					offset = translate.position - Camera.main.ScreenToWorldPoint(mouseScreenSpace);
					//Debug.Log("offset is " + offset);
				}


			}
		}else if (Input.GetMouseButtonUp(0)) 
		{
			isDraged = false;
			translate = null;
		}

		if (isDraged) {
			if (translate != null) {
				// 重新计算鼠标的屏幕坐标空间
				mouseScreenSpace = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, targetScreenSpace.z);
				translate.position = Camera.main.ScreenToWorldPoint (mouseScreenSpace) + offset;
				//Debug.Log ("translate position is " + translate.position);
			}
		} else {
			if (Input.touchCount == 1) {
				if (Input.touches [0].phase == TouchPhase.Moved) {
					CheckTouchDir ();
					Rotate();
				}
			}
			
			if (Input.touchCount == 2) {
				Zoom();
			}
		}
	}

	void CheckTouchDir()
	{
		float x = Input.touches [0].deltaPosition.x;
		float y = Input.touches [0].deltaPosition.y;
		CheckDir (x, y);
	}

	void CheckDir(float x, float y)
	{
		if (Mathf.Abs (x) > Mathf.Abs (y)) {
			if (x < 0) {
				direction = RotateDirection.LEFT;
			} else {
				direction = RotateDirection.RIGHT;
			}
		} else {
			if (y < 0) {
				direction = RotateDirection.DOWN;
			} else {
				direction = RotateDirection.UP;
			}
		}
	}

	void Rotate()
	{
		float rotateDegrees = 0.0f;
		if (direction == RotateDirection.LEFT) {
				rotateDegrees -= rotateSpeed * Time.deltaTime;		
		} else if (direction == RotateDirection.RIGHT) {
				rotateDegrees += rotateSpeed * Time.deltaTime;
		} else if (direction == RotateDirection.UP) {
				rotateDegrees -= rotateSpeed * Time.deltaTime;	
		} else if (direction == RotateDirection.DOWN) {
				rotateDegrees += rotateSpeed * Time.deltaTime;
		}

		/*Vector3 dirVector = transform.position - target.transform.position;
		float angle = Vector3.Angle (Vector3.forward, dirVector);
		if (Vector3.Cross (Vector3.forward, dirVector).y < 0)
				angle = -angle;
		float newAngle = Mathf.Clamp (angle + rotateDegrees, -angleMax, angleMax);
		rotateDegrees = newAngle - angle;
		Debug.Log ("rotateDegress =" + rotateDegrees);*/
		if (direction == RotateDirection.RIGHT || direction == RotateDirection.LEFT) {
			cameraTransform.RotateAround(target.transform.position, Vector3.up, rotateDegrees);

		} else {
			cameraTransform.RotateAround(target.transform.position, transform.right, rotateDegrees);
		}
		direction = RotateDirection.UNKNOWN;
	}

	void Zoom()
	{
		// Store both touches
		Touch touchZero = Input.GetTouch(0);
		Touch touchOne = Input.GetTouch(1);
		
		// Find the position in the previous frame of each touch
		Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
		Vector2 touchOnePrePos = touchOne.position - touchOne.deltaPosition;
		
		// Find the magnitude of the vector (the distance) between the touches in each frame
		float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrePos).magnitude;
		float curTouchDeltaMag = (touchZero.position - touchOne.position).magnitude;
		
		
		// Find the difference in distance between each frame
		float deltaMagDiff = prevTouchDeltaMag - curTouchDeltaMag;
		
		if(!Camera.main.isOrthoGraphic) {
			// Otherwise change the field of view based on the change in distance between the touches.
			Camera.main.fieldOfView += deltaMagDiff * perspectiveZoomSpeed;
			// Clamp the field of view to make sure it's between 0 and 180.
			Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 60.0f, 120.0f);
		}

	}
}
