using UnityEngine;
using System.Collections;

public class CameraRotateDown : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnClick() {
		//if (Camera.main.transform.position.y > 0.1) {
			Camera.main.transform.RotateAround (Vector3.zero, Vector3.right, -5);
		//}
	}
}
