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
	private UIButton _buttonRotateLeft;
	private UIButton _buttonRotateRight;
	private UIButton _buttonRotateUp;
	private UIButton _buttonRotateDown;
	float _freeTime;

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
		_buttonRotateLeft = transform.parent.Find ("UI Root/MainUIPanel/ButtonRotateLeft").GetComponent<UIButton> ();
		_buttonRotateRight = transform.parent.Find ("UI Root/MainUIPanel/ButtonRotateRight").GetComponent<UIButton> ();
		_buttonRotateUp = transform.parent.Find ("UI Root/MainUIPanel/ButtonRotateUp").GetComponent<UIButton> ();
		_buttonRotateDown = transform.parent.Find ("UI Root/MainUIPanel/ButtonRotateDown").GetComponent<UIButton> ();
		_freeTime = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		HandleInput ();
		ShowRotationButton ();
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
				if(isActive) {
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
			_freeTime = 3.0f;
		}

		if (isDraged) {
			if (translate != null) {
				// 重新计算鼠标的屏幕坐标空间
				mouseScreenSpace = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, targetScreenSpace.z);
				translate.position = Camera.main.ScreenToWorldPoint (mouseScreenSpace) + offset;
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

	void ShowRotationButton()
	{
		if (translate != null) {
			if(_buttonRotateLeft.transform.gameObject.activeSelf == false){
				_buttonRotateLeft.transform.gameObject.SetActive(true);	
				_buttonRotateRight.transform.gameObject.SetActive(true);	
				_buttonRotateUp.transform.gameObject.SetActive(true);	
				_buttonRotateDown.transform.gameObject.SetActive(true);	
			}
			Vector3 screenPos = Camera.main.WorldToScreenPoint(translate.position);
			screenPos = screenPos + new Vector3(-Screen.width * 0.5f, -Screen.height * 0.5f, 0);
			Debug.Log("screenPos is" + screenPos);
			_buttonRotateLeft.transform.localPosition = new Vector3(screenPos.x - 100, screenPos.y, screenPos.z);
			_buttonRotateRight.transform.localPosition = new Vector3(screenPos.x + 100, screenPos.y, screenPos.z);
			_buttonRotateUp.transform.localPosition = new Vector3(screenPos.x, screenPos.y + 100, screenPos.z);
			_buttonRotateDown.transform.localPosition = new Vector3(screenPos.x, screenPos.y - 100, screenPos.z);
		}
		else if(_buttonRotateLeft.transform.gameObject.activeSelf && _freeTime <= 0.0f)	{
			_buttonRotateLeft.transform.gameObject.SetActive(false);	
			_buttonRotateRight.transform.gameObject.SetActive(false);	
			_buttonRotateUp.transform.gameObject.SetActive(false);	
			_buttonRotateDown.transform.gameObject.SetActive(false);	
		}
		else if(_freeTime >= 0.0f) {
			_freeTime -= Time.deltaTime;
		}
	}

	public void HideRotationButton()
	{
		_freeTime = 0.0f;
		_buttonRotateLeft.transform.gameObject.SetActive(false);	
		_buttonRotateRight.transform.gameObject.SetActive(false);	
		_buttonRotateUp.transform.gameObject.SetActive(false);	
		_buttonRotateDown.transform.gameObject.SetActive(false);	
		Debug.Log ("hideRotationButton");
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
