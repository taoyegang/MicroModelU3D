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
			isActive = false;
			// 为什么position不正确
			//Debug.Log("other position " + other.transform.position);
			//transform.position = other.transform.position;
			
			//Destroy(gameObject);
			Color color = other.renderer.material.color;
			other.renderer.material.color = new Color(color.r, color.g, color.b, 1.0f);
		}
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
