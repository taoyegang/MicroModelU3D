using UnityEngine;
using System.Collections;

public class CameraRotateLeft : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick() {
		Debug.Log("Rotate Left");
		Camera.main.transform.RotateAround (Vector3.zero, Vector3.up, 5);
	}
}
