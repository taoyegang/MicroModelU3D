using UnityEngine;
using System.Collections;

public class CollisionController : MonoBehaviour {

	public bool isActive;
	// Use this for initialization
	void Start () {
		//Debug.Log ("name =" + name);
		isActive = true;
	}
	
	void OnTriggerEnter(Collider other)
	{
		Debug.Log ("OnTriggerEnter =" + other.name);
		Debug.Log ("name = " + name);

		if (other.name == name) {
		
			Debug.Log ("match");
			gameObject.renderer.material.color = Color.red;
			other.gameObject.renderer.enabled = false;
		}

		Debug.Log ("match");
		gameObject.renderer.material.color = Color.red;
		other.gameObject.renderer.material.color = Color.yellow;
	}
	
	
	void OnTriggerExit(Collider other)
	{
		Debug.Log ("OnTriggerExit =" + other.name);
	}
	
	public bool getIsActive()
	{
		return isActive;
	}

	void Update()
	{

	}
}
