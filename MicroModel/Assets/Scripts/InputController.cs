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
	// Use this for initialization
	void Start () {
		dir = RotateDir.None;
		rotateCamera = Camera.main.transform;

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
		float moveDiff = 0.5f;
		switch (dir) {
		case RotateDir.Left:
			rotateCamera.RotateAround(Vector3.zero,Vector3.up,-moveDiff);
			break;
		case RotateDir.Right:
			rotateCamera.RotateAround(Vector3.zero,Vector3.up,moveDiff);
			break;
		case RotateDir.Down:
			rotateCamera.RotateAround(Vector3.zero,rotateCamera.right,moveDiff);
			break;
		case RotateDir.Up:
			rotateCamera.RotateAround(Vector3.zero,rotateCamera.right,-moveDiff);
			break;
		}
		dir = RotateDir.None;
	}
}
