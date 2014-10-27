using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {

	private enum RotateDir
	{
		Left,
		Right,
		Up,
		Down,
		None
	};
	private RotateDir dir;
	private Transform rotateCamera;
	private GameObject gongzi;
	// Use this for initialization
	void Start () {
		dir = RotateDir.None;
		rotateCamera = Camera.main.transform;
		gongzi = GameObject.Find ("Anchor Point (GongZi)");
	}
	
	// Update is called once per frame
	void Update () {
				if (Input.touchCount == 1) {
						if (Input.touches [0].phase == TouchPhase.Moved) {
								CheckDir ();
						}
				}
		}

	void CheckDir()
	{
				float x = Input.touches [0].deltaPosition.x;
				float y = Input.touches [0].deltaPosition.y;
				if (Mathf.Abs (x) > Mathf.Abs (y)) {
						if (x < 0) {
								dir = RotateDir.Left;
						} else {
								dir = RotateDir.Right;
						}
				} else {
						if (y < 0) {
								dir = RotateDir.Down;
						} else {
								dir = RotateDir.Up;
						}
				}
		}

	void LateUpdate()
	{
		float moveDiff = 2.0f;

		Vector3 centerPosition = gongzi.transform.position;

		switch (dir) {
		case RotateDir.Left:
			rotateCamera.RotateAround(centerPosition,Vector3.up,-moveDiff);
			break;
		case RotateDir.Right:
			rotateCamera.RotateAround(centerPosition,Vector3.up,moveDiff);
			break;
		case RotateDir.Down:
			rotateCamera.RotateAround(centerPosition,rotateCamera.right,moveDiff);
			break;
		case RotateDir.Up:
			rotateCamera.RotateAround(centerPosition,rotateCamera.right,-moveDiff);
			break;
		}
		dir = RotateDir.None;
	}
}
