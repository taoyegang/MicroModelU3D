using UnityEngine;
using System.Collections;

public class CameraRotateUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnClick() {
		Debug.Log("Rotate Up");
		Camera.main.transform.RotateAround (Vector3.zero, Vector3.right, 5);
	}
}
