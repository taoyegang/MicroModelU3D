using UnityEngine;
using System.Collections;

public class RotateMgr : MonoBehaviour {

	private Transform rotateCamera;
	private enum RotateDir
	{
		Left,
		Right,
		Up,
		Down,
		None
	};
	private RotateDir dir;

	void Start () {
		rotateCamera = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClick()
	{
		switch (UIButton.current.name) {
		case "Button_Up":
			dir = RotateDir.Up;
			break;
		case "Button_Left":
			dir = RotateDir.Left;
			break;
		case "Button_Right":
			dir = RotateDir.Right;
			break;
		case "Button_Down":
			dir = RotateDir.Down;
			break;
				}
	}

	void LateUpdate()
	{
		switch (dir) {
		case RotateDir.Left:
			rotateCamera.RotateAround(Vector3.zero,Vector3.up,5.0f);
			break;
		case RotateDir.Right:
			rotateCamera.RotateAround(Vector3.zero,Vector3.up,-5.0f);
			break;
		case RotateDir.Down:
			rotateCamera.RotateAround(Vector3.zero,rotateCamera.right,-5.0f);
			break;
		case RotateDir.Up:
			rotateCamera.RotateAround(Vector3.zero,rotateCamera.right,5.0f);
			break;
				}
		dir = RotateDir.None;
	}
}
